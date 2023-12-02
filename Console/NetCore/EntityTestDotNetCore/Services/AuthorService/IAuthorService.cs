using EntityTestDotNetCore.Shared.Models;
using EntityTestDotNetCore.Shared;

namespace EntityTestDotNetCore.Services
{
    public interface IAuthorService<Author> : IBaseService<Author> where Author : class
    {

    }
}
