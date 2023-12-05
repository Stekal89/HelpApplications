using EntityTestDotNetCore.Shared.Models;
using EntityTestDotNetCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.Extensions.DependencyInjection.ServiceCollection;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using EntityTestDotNetCore.Services.CustomerService;

namespace EntityTestDotNetCore.Shared
{
    public static class DependencyInjection
    {
        public static IConfiguration SetupConfiguration(string[] args)
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();
        }

        public static ServiceProvider RegisterServices(string[] args)
        {
            var serviceCollection = new ServiceCollection();

            //serviceCollection.AddTransient<IAuthorService, AuthorService>();

            //serviceCollection.AddScoped<IBaseService<TEntity>, BaseService<TEntity>>();

            serviceCollection.AddScoped<IAuthorService, AuthorService>();
            serviceCollection.AddScoped<ICustomerService, CustomerService>();

            IConfiguration configuration = SetupConfiguration(args);

            serviceCollection.AddDbContext<PubsContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });


            serviceCollection.AddSingleton(configuration);

            return serviceCollection.BuildServiceProvider();
        }
    }
}
