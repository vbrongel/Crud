namespace API.Configuration
{
    public static class HostEnvironment
    {
        public static IConfigurationBuilder RootAppSettingsConfiguration(IHostEnvironment hostEnvironment)
        {
            return new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();
        }

    }
}
