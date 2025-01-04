using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Application.Settings;

public class FirebasePushNotificationConfig
{
    [JsonProperty("type")]
    required public string type { get; set; }

    [JsonProperty("project_id")]
    required public string project_id { get; set; }

    [JsonProperty("private_key_id")]
    required public string private_key_id { get; set; }

    [JsonProperty("private_key")]
    required public string private_key { get; set; }

    [JsonProperty("client_email")]
    required public string client_email { get; set; }

    [JsonProperty("client_id")]
    required public string client_id { get; set; }

    [JsonProperty("auth_uri")]
    required public string auth_uri { get; set; }

    [JsonProperty("token_uri")]
    required public string token_uri { get; set; }

    [JsonProperty("auth_provider_x509_cert_url")]
    required public string auth_provider_x509_cert_url { get; set; }

    [JsonProperty("client_x509_cert_url")]
    required public string client_x509_cert_url { get; set; }

    [JsonProperty("universe_domain")]
    required public string universe_domain { get; set; }
}
