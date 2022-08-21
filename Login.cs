using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
namespace AduloRolloGrowe
{
    public class GroweClientConfig
    {
        public bool IsEnabled { get; set; }
        public string GroweAPIUrl { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Timeout { get; set; }
    }
    public class Login
    {
        public bool IsEnabled { get; set; }
        public string GroweAPIUrl { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public string Timeout { get; set; }

        public Login()
        {
            ReadLoginInformation();
        }
        private void ReadLoginInformation()
        {
            var config = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json").Build();
            var section = config.GetSection(nameof(GroweClientConfig));
            var groweClientConfig = section.Get<GroweClientConfig>();
            IsEnabled = groweClientConfig.IsEnabled;
            GroweAPIUrl = groweClientConfig.GroweAPIUrl;
            User = groweClientConfig.User;
            Password = groweClientConfig.Password;
        }
        public bool Save()
        {
            var groweClientConfig = new GroweClientConfig()
            {
                IsEnabled = IsEnabled,
                GroweAPIUrl = GroweAPIUrl,
                User = User,
                Password = Password,
                Timeout = Timeout
            };
            string json = JsonSerializer.Serialize(groweClientConfig);
            System.IO.File.WriteAllText("appsettings.json", json);
            return true;
        }

    }
}
