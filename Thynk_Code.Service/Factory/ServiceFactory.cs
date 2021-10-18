using System;
using Thynk_Code.Service.Interfaces;

namespace Thynk_Code.Service.Factory
{
    public class ServiceFactory : IServiceFactory
    {

        IServiceProvider ServiceProvider { get; set; }
        public ServiceFactory(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }


        public T GetService<T>() where T : class
        {
            T service = ServiceProvider.GetService(typeof(T)) as T;
            if (service == null)
                throw new Exception("Type Not Supported");
            return service;
        }

    }
}
