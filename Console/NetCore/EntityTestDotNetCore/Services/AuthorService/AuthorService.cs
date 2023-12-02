using EntityTestDotNetCore.Shared;
using EntityTestDotNetCore.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityTestDotNetCore.Services
{
    public class AuthorService<Author> : BaseService<Author>, IAuthorService<Author> where Author : class
    {
        /// <inheritdoc />
        public AuthorService(PubsContext context) : base(context)
        {
        }
    }
}
