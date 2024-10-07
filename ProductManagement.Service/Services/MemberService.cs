using Microsoft.Extensions.Configuration;
using ProductManagement.Repository;
using ProductManagement.Repository.Models;
using ProductManagement.Service.BussinessModels.AuthenModels;
using ProductManagement.Service.Interfaces;
using ProductManagement.Service.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Service.Services
{
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;

        public MemberService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<AuthenModel> LoginWithEmailPassword(string email, string password)
        {
            try
            {
                var existUser = await _unitOfWork.MemberRepository.GetMemberByEmail(email);
                if (existUser == null)
                {
                    return new AuthenModel
                    {
                        HttpCode = 401,
                        Message = "Account does not exist"
                    };
                }

                var verifyUser = PasswordUtils.VerifyPassword(password, existUser.PasswordHash);

                if (verifyUser)
                {

                    var accessToken = GenerateAccessToken(email, existUser);
                    var refreshToken = GenerateRefreshToken(email);

                    _unitOfWork.Save();

                    return new AuthenModel
                    {
                        HttpCode = 200,
                        AccessToken = accessToken,
                        RefreshToken = refreshToken
                    };
                }
                return new AuthenModel
                {
                    HttpCode = 401,
                    Message = "Incorrect email or password"
                };

            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> RegisterAsync(SignUpModel model)
        {
            try
            {
                Member newUser = new Member
                {
                    Email = model.Email,
                    FullName = model.FullName,
                    Role = "ADMIN"
                };

                var existUser = await _unitOfWork.MemberRepository.GetMemberByEmail(newUser.Email);

                if (existUser != null)
                {
                    throw new Exception("Account is already exist");
                }

                // hash password
                newUser.PasswordHash = PasswordUtils.HashPassword(model.Password);

                await _unitOfWork.MemberRepository.AddAsync(newUser);

                _unitOfWork.Save();
                return true;
            }
            catch
            {
                throw;
            }
        }

        private string GenerateAccessToken(string email, Member user)
        {
            var role = user.Role.ToUpper();

            var authClaims = new List<Claim>();

            if (role != null)
            {
                authClaims.Add(new Claim(ClaimTypes.Email, email));
                authClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }
            var accessToken = GenerateJsonWebToken.CreateToken(authClaims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(accessToken);
        }

        private string GenerateRefreshToken(string email)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, email),
            };
            var refreshToken = GenerateJsonWebToken.CreateRefreshToken(claims, _configuration, DateTime.UtcNow);
            return new JwtSecurityTokenHandler().WriteToken(refreshToken).ToString();
        }
    }
}
