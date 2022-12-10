using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using YoAyudoPR.Web.Application.Dtos;
using YoAyudoPR.Web.Application.Services;
using YoAyudoPR.Web.Infrastructure.Repositories;
using YoAyudoPR.Web.Domain.Entities;
using YoAyudoPR.Web.Domain.Repositories;
using YoAyudoPR.Web.Domain.Security;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace YoAyudoPR.Web.Infrastructure.Services
{
    public class UserService: IUserService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly PasswordSecurity _passwordSecurityService;

        public UserService(railwayContext context)
        {
            _userRepository = new GenericRepository<User>(context);
            _passwordSecurityService = new PasswordSecurity();
        }

        public async Task<User?> Authenticate(string email, string password, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.FirstByConditionTrackingAsync(x => x.Email == email, cancellationToken);

            if (user == null || user?.Isdeleted == true || user?.Isactive == false)
            {
                return user;
            }

            var hashedPassword = _passwordSecurityService.CreatePasswordHash(password, user!.Passwordsalt);

            if (email == user.Email && hashedPassword == user.Passwordhash)
            {
                return user;
            }

            return null;
        }

        public async Task Create(UserCreateRequest model, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Guid = Guid.NewGuid(),
                Firstname = model.FirstName,
                Lastname = model.LastName,
                Initial = model.Initial,
                Secondlastname = model.SecondLastName,
                Email = model.Email, 
                Phone = model.Phone,
                Resetpassword = false,
                Isdeleted = false,
                Isactive = true
            };
            
            var salt = _passwordSecurityService.CreateSalt();
            var hashedPassword = _passwordSecurityService.CreatePasswordHash(model.Password, salt);

            user.Passwordsalt = salt;
            user.Passwordhash = hashedPassword;


            await _userRepository.AddAndSaveAsync(user, cancellationToken);
        }

        public async Task Delete(Guid guid, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FindByGuidAsync(guid);
            
            if (user != null)
            {
                user!.Isdeleted = true;

                await _userRepository.UpdateAndSaveAsync(user, cancellationToken);
            }
        }

        public async Task<IEnumerable<UserResponse>> FindAll(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var users = await _userRepository.FindAllAsync(predicate, cancellationToken);

            var userList = users.Select(x => new UserResponse
            {
                Guid = x.Guid.GetValueOrDefault(),
                FirstName = x.Firstname,
                LastName = x.Lastname,
                Initial = x.Initial,
                SecondLastName = x.Secondlastname,
                Email = x.Email,
                Phone = x.Phone,
                ResetPassword = x.Resetpassword,
                IsDeleted = x.Isdeleted,
                IsActive = x.Isactive,
            });

            return userList;
        }

        public async Task<UserResponse> FirstByConditionAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstByConditionAsync(predicate, cancellationToken);

            var userInfo = new UserResponse
            {
                Guid = user.Guid.GetValueOrDefault(),
                FirstName = user.Firstname,
                LastName = user.Lastname,
                Initial = user.Initial,
                SecondLastName = user.Secondlastname,
                Email = user.Email,
                Phone = user.Phone,
                ResetPassword = user.Resetpassword,
                IsDeleted = user.Isdeleted,
                IsActive = user.Isactive
            };

            return userInfo;
        }

        public async Task<string> GenerateJWT(User user, string secret, CancellationToken cancellationToken = default)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("userguid", user!.Guid.GetValueOrDefault().ToString()),
                    new Claim("username", user.Email),
                }),
                Expires = DateTime.UtcNow.AddHours(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public async Task Update(UserUpdateRequest model, CancellationToken cancellationToken)
        {
            var user = await _userRepository.FirstByConditionAsync(x => x.Guid == model.Guid, cancellationToken);

            user.Firstname = model.FirstName;
            user.Lastname = model.LastName;
            user.Initial = model.Initial;
            user.Secondlastname = model.SecondLastName;
            user.Email = model.Email;
            user.Phone = model.Phone;

            if (string.IsNullOrEmpty(model.Password) == false)
            {
                var salt = _passwordSecurityService.CreateSalt();
                var hashedPassword = _passwordSecurityService.CreatePasswordHash(model.Password, salt);

                user.Passwordsalt = salt;
                user.Passwordhash = hashedPassword;
            }

            await _userRepository.UpdateAndSaveAsync(user, cancellationToken);
        }
    }
}
