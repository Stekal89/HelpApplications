using EntityTestDotNetCore.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTestDotNetCore.Services.CustomerService
{
    public class CustomerService<Customer> : BaseService<Customer>, ICustomerService<Customer> where Customer : class
    {
        /// <inheritdoc />
        public CustomerService(PubsContext context) : base(context)
        {
        }
    }
}
