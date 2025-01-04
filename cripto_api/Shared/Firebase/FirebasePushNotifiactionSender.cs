using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


public class FirebasePushNotifiactionSender
{
    public static async Task SendPushViaHttpV1Async(
        string serviceAccountJson, // Service account JSON içeriği
        string deviceToken,        // Kullanıcının/cihazın FCM token değeri
        string title,
        string body,
        object? data = null
    )
    {

        // 1) JSON'dan projectId oku
        dynamic serviceAccountObject = JsonConvert.DeserializeObject(serviceAccountJson)!;
        string project_id = serviceAccountObject.project_id;

        // 1) GoogleCredentials oluştur
        //    - Dosyayı diskten okuyorsanız: File.ReadAllText("serviceAccount.json")
        //    - Burada parametre ile alıyorsanız direkt string içeriği gelebilir.
        var credential = GoogleCredential
            .FromJson(serviceAccountJson)
            .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

        // 2) Access Token al
        var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

        // 3) FCM v1 endpoint -> https://fcm.googleapis.com/v1/projects/PROJECT_ID/messages:send
        var url = $"https://fcm.googleapis.com/v1/projects/{project_id}/messages:send";

        // 4) Gönderilecek gövde (message)
        // message nesnesindeki "token", bildirimin gideceği cihaza ait token'dır.
        var payload = new
        {
            message = new
            {
                token = deviceToken,
                notification = new
                {
                    title = title,
                    body = body
                },
                // Opsiyonel ek veri
                data = data
            }
        };

        // 5) JSON'a dönüştür
        var json = JsonConvert.SerializeObject(payload);

        // 6) HTTP isteğini hazırlayalım
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // 7) POST isteği at
        var response = await httpClient.PostAsync(url, content);
        var responseBody = await response.Content.ReadAsStringAsync();

        // 8) Sonucu kontrol
        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"[FCM] Hata: {response.StatusCode} => {responseBody}");
        }
        else
        {
            Console.WriteLine($"[FCM] Başarılı: {responseBody}");
        }
    }
}
