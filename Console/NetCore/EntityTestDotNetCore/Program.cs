using EntityTestDotNetCore.Shared;
using Microsoft.Extensions.DependencyInjection;
using EntityTestDotNetCore.Services;
using EntityTestDotNetCore.Shared.Models;
using System.Linq.Expressions;
using EntityTestDotNetCore.Services.CustomerService;
using Microsoft.EntityFrameworkCore;

//using Microsoft.AspNetCore.Builder;

namespace EntityTestDotNetCore
{
    internal class Program
    {
        private static void WriteError(string msg)
        {
            ConsoleColor currentColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\n{msg}");
        }

        static void Main(string[] args)
        {
            //ServiceProvider serviceProvider = DependencyInjection.DependencyInjection.RegisterServices(args);
            using (ServiceProvider serviceProvider = DependencyInjection.RegisterServices(args))
            {
                IAuthorService<Author> authorService = serviceProvider.GetService<IAuthorService<Author>>();
                ICustomerService<Customer> customerServcie = serviceProvider.GetService<ICustomerService<Customer>>();

                #region Authentification and Database-Connection

                bool success = false;

                try
                {
                    using (var response = Task.Run<ServiceResponse<bool>>(async () => await authorService.CanDbConnectAsync()))
                    {
                        response.Wait();

                        if (!response.Result.Success)
                        {
                            if (string.IsNullOrEmpty(response.Result.Message))
                            {
                                WriteError($"It was not possible to create a database connection!\n");
                            }
                            else
                            {
                                WriteError($"{(!string.IsNullOrEmpty(response.Result.Message) ? $"\n{response.Result.Message}\n" : null)}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nDatabase connection exist.");
                            success = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    WriteError($"Error during connection Test!\n" +
                               $"{ex.Message}\n\n");
                }

                #endregion

                #region Load

                #region Load Customer

                if (success)
                {
                    try
                    {
                        Func<IQueryable<Customer>, IQueryable<Customer>>? customerInclude = null;
                        Expression<Func<Customer, bool>>? customerFilter = null;

                        using (var response = Task.Run<ServiceResponse<List<Customer>>>(async () => await customerServcie.GetListAsync(customerInclude, customerFilter)))
                        {
                            response.Wait();

                            var result = response.Result;

                            List<Customer>? customers = null;
                            if (result.Success)
                            {
                                customers = result.Data;

                                if (customers?.Count > 0)
                                {
                                    Console.WriteLine($"\n#####################################");
                                    Console.WriteLine($"Found {customers.Count} Customers.");
                                    Console.WriteLine($"#####################################\n");

                                    foreach (var item in customers)
                                    {
                                        Console.WriteLine($"\n---------------------");
                                        Console.WriteLine($"Id: \"{item.CustomerId}\"");
                                        Console.WriteLine($"Contractname: \"{item.ContactName}\"");
                                        Console.WriteLine($"CompanyName: \" {item.CompanyName}\"");
                                        Console.WriteLine($"\n---------------------");
                                    }
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(response.Result.Message))
                                {
                                    WriteError($"It was not possible to load Authors!\n");
                                }
                                else
                                {
                                    WriteError($"{(!string.IsNullOrEmpty(response.Result.Message) ? $"{response.Result.Message}\r" : null)}");
                                }

                                success = false;
                            }
                        }

                        //ILogger logger = serviceProvider.GetService<ILogger<Program>>();
                        //logger.LogInformation("Github api url is: {githubApiUrl}", configuration["github:apiUrl"]);

                    }
                    catch (Exception ex)
                    {
                        WriteError($"Error during Data-Retrieve Test!\n" +
                               $"{ex.Message}\n\n");
                        success = false;
                    }

                    #endregion

                }

                if(success)
                {
                    try
                    {
                        Expression<Func<Author, bool>>? authorFilter = null;

                        using (var response = Task.Run<ServiceResponse<List<Author>>>(async () => await authorService.GetListAsync(a => a.Include(ta => ta.Titleauthors), authorFilter)))
                        {
                            response.Wait();

                            var result = response.Result;

                            List<Author>? authorList = null;
                            if (result.Success)
                            {
                                authorList = result.Data;

                                if (authorList?.Count > 0)
                                {
                                    Console.WriteLine($"\n#####################################");
                                    Console.WriteLine($"Found {authorList.Count} Authors.");
                                    Console.WriteLine($"#####################################\n");

                                    foreach (var item in authorList)
                                    {
                                        Console.WriteLine($"\n---------------------");
                                        Console.WriteLine($"Id: \"{item.AuId}\"");
                                        Console.WriteLine($"Firstname: \"{item.AuFname}\"");
                                        Console.WriteLine($"Lastname: \"{item.AuLname}\"");
                                        if (item.Titleauthors != null)
                                        {
                                            Console.WriteLine($"--------------");
                                            Console.WriteLine($"   --- TitleAuthors ---");
                                            Console.WriteLine($"   Count: {item.Titleauthors.Count}");
                                            Console.WriteLine($"--------------");
                                        }
                                        Console.WriteLine($"\n---------------------");
                                    }
                                }
                            }
                            else
                            {
                                if (string.IsNullOrEmpty(response.Result.Message))
                                {
                                    WriteError($"It was not possible to load Authors!\n");
                                }
                                else
                                {
                                    WriteError($"!{(string.IsNullOrEmpty(response.Result.Message) ? $"{response.Result.Message}\r" : null)}");
                                }
                                success = false;
                            }
                        }

                        //ILogger logger = serviceProvider.GetService<ILogger<Program>>();
                        //logger.LogInformation("Github api url is: {githubApiUrl}", configuration["github:apiUrl"]);

                    }
                    catch (Exception ex)
                    {
                        WriteError($"Error during Data-Retrieve Test!\n" +
                               $"{ex.Message}\n\n");
                        success = false;
                    }
                }
            }

            #endregion

            //serviceProvider.Dispose();

            Console.WriteLine("\n\nContinue with any key...");
            Console.ReadKey();
        }
    }
}