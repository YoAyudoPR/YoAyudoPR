using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Infrastructure.Repositories;
using YouAyudoPR.Web.Domain.Entities;
using YouAyudoPR.Web.Domain.Repositories;

namespace YoAyudoPR.Web.Infrastructure.Services
{
    public class UserService: IUserService
    {
        private readonly IGenericRepository<User> _userRepository;

        public UserService(railwayContext context)
        {
            _userRepository = new GenericRepository<User>(context);
        }

        public async Task Create(UserCreateRequest model, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Initial = model.Initial,
                Secondlastname = model.SecondLastName,
                Email = model.Email, 
                Phone = model.Phone,
                Resetpassword = false,
                Isdeleted = false
            };


            //TODO: Add password hash
            //var salt = _passwordSecurityService.CreateSalt();
            //var hashedPassword = _passwordSecurityService.CreatePasswordHash(model.Password, salt);

            //user.PasswordSalt = salt;
            //user.PasswordHash = hashedPassword;
            

            await _userRepository.AddAndSaveAsync(user, cancellationToken);
        }

        public async Task<IEnumerable<UserResponse>> FindAll(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var users = await _userRepository.FindAllAsync(predicate, cancellationToken);

            var userList = users.Select(x => new UserResponse
            {
                FirstName = x.Firstname,
                LastName = x.Lastname,
                Initial = x.Initial,
                SecondLastName = x.Secondlastname,
                Email = x.Email,
                ResetPassword = x.Resetpassword,
                IsDeleted = x.Isdeleted
            });

            return userList;
        }

        public async Task<UserResponse> FirstByConditionAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstByConditionAsync(predicate, cancellationToken);

            var userInfo = new UserResponse
            {
                FirstName = user.Firstname,
                LastName = user.Lastname,
                Initial = user.Initial,
                SecondLastName = user.Secondlastname,
                Email = user.Email,
                Phone = user.Phone,
                ResetPassword = user.Resetpassword,
                IsDeleted = user.Isdeleted
            };

            return userInfo;
        }
    }
}
