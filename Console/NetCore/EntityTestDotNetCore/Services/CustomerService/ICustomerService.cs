using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTestDotNetCore.Services.CustomerService
{
    public interface ICustomerService<Customer> : IBaseService<Customer> where Customer : class
    {
    }
}
