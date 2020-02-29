using EMSApp.Core.DTO;
using EMSApp.Core.DTO.Responses;
using EMSApp.Core.Entities;
using EMSApp.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EMSApp.Infrastructure.Auth
{
    public class JwtAuthResponseFactory : IJwtAuthResponseFactory
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ITokenFactory _tokenFactory;
        private readonly IHostRepository _hostRepository;

        public JwtAuthResponseFactory(IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ITokenFactory tokenFactory,
            IHostRepository hostRepository
            )
        {
            _configuration = configuration;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenFactory = tokenFactory;
            _hostRepository = hostRepository;
        }
        public async Task<AuthResponse> GenerateAuthResponseForUser(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration.GetSection("JwtSettings")["SecretKey"];
            var tokenLifeTime = _configuration.GetSection("JwtSettings")["TokenLifeTime"];
            var secretKeyBytes = Encoding.ASCII.GetBytes(secretKey);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtCustomClaimNames.Id, user.Id.ToString()),
            };

            var userClaims = await _userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);

            var userRoles = await _userManager.GetRolesAsync(user);
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));

                var role = await _roleManager.FindByNameAsync(userRole);
                if (role == null) continue;

                var roleClaims = await _roleManager.GetClaimsAsync(role);

                foreach (var roleClaim in roleClaims)
                {
                    if (claims.Contains(roleClaim)) continue;

                    claims.Add(roleClaim);
                }
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(TimeSpan.Parse(tokenLifeTime)),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var newToken = _tokenFactory.GenerateToken();
            var refreshToken = new RefreshToken
            {
                Token = newToken,
                JwtId = token.Id,
                ApplicationUserId = user.Id,
                ExpiresOn = DateTime.UtcNow.AddMonths(1)
            };

            _hostRepository.Create(refreshToken);
            await _hostRepository.SaveAsync();

            return new AuthResponse { 
                AccessToken = new AccessToken(tokenHandler.WriteToken(token), 120),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
