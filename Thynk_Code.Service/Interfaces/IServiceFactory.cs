using System;
namespace Thynk_Code.Service.Interfaces
{
    public interface IServiceFactory
    {
        T GetService<T>() where T : class;

    }
}
