using EntityTestDotNetCore.Shared;
using EntityTestDotNetCore.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityTestDotNetCore.Services
{
    public class AuthorService : BaseService<Author>, IAuthorService
    {
        /// <inheritdoc />
        public AuthorService(PubsContext context) : base(context)
        {
        }
    }
}
