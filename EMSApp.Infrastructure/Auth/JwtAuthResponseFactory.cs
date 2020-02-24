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

namespace EMSApp.Infrastructure.Auth
{
    public class JwtAuthResponseFactory : IJwtAuthResponseFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ITokenFactory _tokenFactory;

        public JwtAuthResponseFactory(IOptions<JwtSettings> jwtSettings,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            ITokenFactory tokenFactory,
            IServiceProvider serviceProvider
            )
        {
            _serviceProvider = serviceProvider;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _tokenFactory = tokenFactory;
        }
        public async Task<AuthResponse> GenerateAuthResponseForUser(ApplicationUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

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
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifeTime),
                SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
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

            var appRepository = _serviceProvider.GetService<IAppRepository>();
             appRepository.Create(refreshToken);
            await appRepository.SaveAsync();

            return new AuthResponse { 
                AccessToken = new AccessToken(tokenHandler.WriteToken(token), 120),
                RefreshToken = refreshToken.Token
            };
        }
    }
}
