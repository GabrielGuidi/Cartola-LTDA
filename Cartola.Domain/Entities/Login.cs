using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Cartola.Domain.Entities
{
    public class Login
    {
        public Login(string userEmail, string password)
        {
            Payload = new Payload(userEmail, password);
        }

        [JsonPropertyName("payload")]
        public Payload Payload { get; set; }

        [JsonPropertyName("captcha")]
        public string Captcha { get; set; } = "";
    }

    public class Payload
    {
        public Payload(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("serviceId")]
        public int ServiceId { get; set; } = 6860;
    }
}
