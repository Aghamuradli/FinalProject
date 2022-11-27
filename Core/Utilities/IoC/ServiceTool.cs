using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.IoC
{
    public static class ServiceTool
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        public static IServiceCollection Create(IServiceCollection services) //.Net'in ServiceCollection'nin servic'lerini build et =>
                                                                             //Qisasi bu kod webAPI'de yaratdigimiz Injectionlari yaratmag ucundur
        {
            ServiceProvider = services.BuildServiceProvider();
            return services;
        }
    }
}
