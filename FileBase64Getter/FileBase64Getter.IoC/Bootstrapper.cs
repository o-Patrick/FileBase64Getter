using FileBase64Getter.Framework.LogManagement;
using FileBase64Getter.Framework.LogManagement.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Service.FileBase64Getter;

namespace FileBase64Getter.IoC
{
    public static class Bootstrapper
    {
        public static void AddIoC(this IServiceCollection services)
        {
            services.AddScoped<ILogHandler, LogHandler>();
            services.AddScoped<FileBase64GetterService>();
        }
    }
}
