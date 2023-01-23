using System.IO;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;
using System.Text.Json;
using System;
using Microsoft.Extensions.Hosting.Internal;

namespace TollFee.Api.Config
{
    public class ConfigService : IConfigService
    {


        public void SetYear(int year)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build()
                .Get<Config>();

            config.ConfigurableYear = year;

            var jsonWriteOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                
            };

            var newJson = JsonSerializer.Serialize(config, jsonWriteOptions);

            var appSettingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json");
            File.WriteAllText(appSettingsPath, newJson);
            
        }
    }
}
