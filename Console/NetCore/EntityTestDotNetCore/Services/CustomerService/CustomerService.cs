using EntityTestDotNetCore.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityTestDotNetCore.Services.CustomerService
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        /// <inheritdoc />
        public CustomerService(PubsContext context) : base(context)
        {
        }
    }
}
