using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 
/// FCM Token'ları Nedir?
/// FCM token'ları, Firebase tarafından her bir cihaz veya uygulama kurulumuna atanan benzersiz tanımlayıcılardır.
/// Bu token'lar, sunucuların belirli bir cihaza veya kullanıcıya bildirim göndermesini sağlar.
/// 

// 2. FCM Token'ları Ne Sıklıkta Değişir?
// A.Uygulama Yeniden Yüklenmesi veya Güncellenmesi
// B. Cihazın FCM İstemci Yazılımının Yenilenmesi
// C. Kullanıcının Cihaz Ayarlarını Değiştirmesi
// D. Güvenlik Nedenleriyle Token'ın Yenilenmesi
// E. Cihazın Yeni Bir Hesapla Senkronize Edilmesi

// Bu durumlarda eski token unsubscribe olmalıdır.

/// </summary>
public class FirebasePushNotifiactionSender
{
    /// <summary>
    /// tek bir mobile token e push notification gönderir.
    /// </summary>
    /// <param name="serviceAccountJson"></param>
    /// <param name="deviceToken"></param>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static async Task SendPushViaHttpV1Async(
        string serviceAccountJson,
        string deviceToken,
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

    /// <summary>
    /// mobile token leri notification topic e subscribe eder.
    /// </summary>
    /// <param name="serviceAccountJson"></param>
    /// <param name="topic"></param>
    /// <param name="deviceTokens"></param>
    /// <returns></returns>
    public static async Task SubscribeTokensToTopicAsync(
           string serviceAccountJson,
           string topic,
           List<string> deviceTokens
       )
    {
        // JSON'dan projectId oku
        dynamic serviceAccountObject = JsonConvert.DeserializeObject(serviceAccountJson)!;
        string project_id = serviceAccountObject.project_id;

        // GoogleCredentials oluştur
        var credential = GoogleCredential
            .FromJson(serviceAccountJson)
            .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

        // Access Token al
        var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

        // FCM batchAdd endpoint
        var url = $"https://fcm.googleapis.com/v1/projects/{project_id}/tokens:batchAdd";

        // Payload oluştur
        var payload = new
        {
            to = $"/topics/{topic}",
            registration_tokens = deviceTokens
        };

        var json = JsonConvert.SerializeObject(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await httpClient.PostAsync(url, content);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"[FCM BatchAdd] Hata: {response.StatusCode} => {responseBody}");
        }
        else
        {
            Console.WriteLine($"[FCM BatchAdd] Başarılı: {responseBody}");
        }
    }

    /// <summary>
    /// belirli bir topic e subscripbe olmuş kullanıcılara 
    /// push notification gönderir
    /// </summary>
    /// <param name="serviceAccountJson"></param>
    /// <param name="topic"></param>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public static async Task SendPushToTopicAsync(
       string serviceAccountJson,
       string topic,
       string title,
       string body,
       object? data = null
   )
    {
        // JSON'dan projectId oku
        dynamic serviceAccountObject = JsonConvert.DeserializeObject(serviceAccountJson)!;
        string project_id = serviceAccountObject.project_id;

        // GoogleCredentials oluştur
        var credential = GoogleCredential
            .FromJson(serviceAccountJson)
            .CreateScoped("https://www.googleapis.com/auth/firebase.messaging");

        // Access Token al
        var accessToken = await credential.UnderlyingCredential.GetAccessTokenForRequestAsync();

        // FCM v1 endpoint
        var url = $"https://fcm.googleapis.com/v1/projects/{project_id}/messages:send";

        var payload = new
        {
            message = new
            {
                topic = topic,
                notification = new
                {
                    title = title,
                    body = body
                },
                data = data
            }
        };

        var json = JsonConvert.SerializeObject(payload);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await httpClient.PostAsync(url, content);
        var responseBody = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"[FCM] Hata: {response.StatusCode} => {responseBody}");
        }
        else
        {
            Console.WriteLine($"[FCM] Başarılı: {responseBody}");
        }
    }


    /// <summary>
    /// Belirli bir cihaz token'ını belirtilen topic'ten abonelikten çıkarır.
    /// </summary>
    /// <param name="serviceAccountJson">Service Account JSON içeriği</param>
    /// <param name="topic">Abonelikten çıkarılacak topic adı</param>
    /// <param name="deviceToken">Abonelikten çıkarılacak cihazın FCM token'ı</param>
    /// <returns>Asenkron işlem</returns>
    public static async Task UnSubscribeTokenToTopicAsync(string serviceAccountJson, string topic, string deviceToken)
    {
        try
        {
            // FirebaseApp'ın zaten başlatılıp başlatılmadığını kontrol et
            if (FirebaseApp.DefaultInstance == null)
            {
                var credential = GoogleCredential.FromJson(serviceAccountJson);
                FirebaseApp.Create(new AppOptions
                {
                    Credential = credential
                });
            }

            var messaging = FirebaseMessaging.DefaultInstance;

            // Cihaz token'ını topic'ten kaldırma
            var response = await messaging.UnsubscribeFromTopicAsync(new List<string> { deviceToken }, topic);

            Console.WriteLine($"Başarıyla {response.SuccessCount} cihaz topic'e aboneliği kaldırıldı.");
            if (response.FailureCount > 0)
            {
                Console.WriteLine($"{response.FailureCount} cihazın aboneliği kaldırılamadı.");
                foreach (var error in response.Errors)
                {
                    Console.WriteLine($"Hata: {error}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Abonelik kaldırma hatası: {ex.Message}");
        }
    }


}
