using Microsoft.Extensions.DependencyInjection;
using Service.FileBase64Getter;

namespace FileBase64Getter.IoC
{
    public static class Bootstrapper
    {
        public static void AddIoC(this IServiceCollection services)
        {
            services.AddScoped<FileBase64GetterService>();
        }
    }
}
