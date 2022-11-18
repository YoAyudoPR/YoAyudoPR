using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Domain.Entities;

namespace YoAyudoPR.Web.Application.Services
{
    public interface IUserService
    {
        Task<User?> Authenticate(string email, string password, CancellationToken cancellationToken = default);
        Task<string> GenerateJWT(User user, string secret, CancellationToken cancellationToken = default);
        Task<IEnumerable<UserResponse>> FindAll(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
        Task<UserResponse> FirstByConditionAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
        Task Create(UserCreateRequest model, CancellationToken cancellationToken);
        Task Update(UserUpdateRequest model, CancellationToken cancellationToken);
        Task Delete(Guid guid, CancellationToken cancellationToken);
    }
}
