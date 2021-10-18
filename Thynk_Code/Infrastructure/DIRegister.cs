using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Thynk_Code.Data;
using Thynk_Code.Data.Interfaces;
using Thynk_Code.Model.DbContext;
using Thynk_Code.Model.Entities;
using Thynk_Code.Service.Factory;
using Thynk_Code.Service.Implementations;
using Thynk_Code.Service.Interfaces;

namespace Thynk_Code.Infrastructure
{
    public static class DIRegister
    {
        public static IServiceCollection RegisterDependencies(this IServiceCollection serviceCollection)
        {


            #region Scoped Services

            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork<ApplicationDbContext>>();


            #endregion


            #region Transient Services

            #region Services Without Interface
            serviceCollection.AddTransient<UserManager<ApplicationUser>>();
            
            #endregion

            #region Services With Interface
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IServiceFactory, ServiceFactory>();
            serviceCollection.AddTransient<IPcrService, PcrService>();

            #endregion

            #endregion

            #region Singleton Services
            #endregion

            return serviceCollection;
        }
    }
}
