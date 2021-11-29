using Microsoft.Extensions.Configuration;
 

namespace GmeCore.ImportRemitFiles.IntegrationTests
{
    public class Configuration
    {
        public static IConfigurationRoot GetConfiguration()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
             

            IConfigurationRoot configurationRoot = configurationBuilder.Build();           

            return configurationRoot;
        }
    }
}
