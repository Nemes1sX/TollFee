using System.IO;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace TollFee.Api.Config
{
    public class ConfigService : IConfigService
    {
        private readonly CustomConfig _customConfig;

        public ConfigService(CustomConfig customConfig)
        {
            _customConfig = customConfig;
        }

        public void SetYear(int year)
        {
            //_configs.ConfigurableYear = year;
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build()
                .Get<CustomConfig>();

            config.ConfigurableYear = year;

            var jsonWriteOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                
            };

            var newJson = JsonSerializer.Serialize(config, jsonWriteOptions);

            var appSettingsPath = Path.Combine(Directory.GetCurrentDirectory(), "customsettings.json");
            File.WriteAllText(appSettingsPath, newJson);
            
        }
    }
}
