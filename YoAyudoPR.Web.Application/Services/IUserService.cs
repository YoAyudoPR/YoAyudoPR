using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoAyudoPR.Web.Application.Dtos;
using YouAyudoPR.Web.Domain.Entities;

namespace YoAyudoPR.Web.Application.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponse>> FindAll(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
        Task<UserResponse> FirstByConditionAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken);
        Task Create(UserCreateRequest model, CancellationToken cancellationToken);
    }
}
