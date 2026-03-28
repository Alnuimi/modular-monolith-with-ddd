using System.Security.Claims;
using Blocks.Exceptions;
using Microsoft.AspNetCore.Identity;
using Blocks.AspNetCore;
using Auth.Application;
using Auth.Persistence.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Auth.API.Features.Users.Login;

[AllowAnonymous]
[HttpPost("login")]
public class LoginEndpoint(PersonRepository _personRepository,UserManager<User> _userManager, SignInManager<User> _signInManager,TokenFactory _tokenFactory) : Endpoint<LoginCommand, LoginResponse>
{
        public override async Task HandleAsync(LoginCommand command, CancellationToken ct)
    {
        var person = Guard.NotFound(
             await _personRepository.GetByEmailAsync(command.Email, ct)
             );
        var user = Guard.NotFound(person.User);

        var result = await _signInManager.CheckPasswordSignInAsync(user, command.Password, lockoutOnFailure: false);
        if(!result.Succeeded)
        {
            throw new BadRequestException($"invalid credentials for user with email {command.Email}.");
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        // Generate JWT token
        var jwtToken = _tokenFactory.GenerateJwtToken(user.Id.ToString(), user.Person.FullName, command.Email, userRoles, additionalClaims: Array.Empty<Claim>());
        var refreshToken = _tokenFactory.GenerateRefreshToken(HttpContext.GetClientIpAddress());
        user.AddRefreshToken(refreshToken);
        
        await _userManager.UpdateAsync(user);
        await SendAsync(new LoginResponse(command.Email, jwtToken, refreshToken.Token), cancellation: ct);
    }

    
}