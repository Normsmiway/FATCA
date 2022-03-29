using Microsoft.Extensions.Configuration;

namespace Fatca.Core.Configurations
{

    public static class ConfigurationInsaller
    {
        public static TModel GetOptions<TModel>(this IConfiguration configuration, string section) where TModel : new()
        {
            var model = new TModel();
            configuration.GetSection(section).Bind(model);

            return model;
        }
    }

}
