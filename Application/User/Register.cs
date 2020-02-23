using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Application.Errors;
using Application.Interfaces;
using Application.User.Validators;
using Domain;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistance;


namespace Application.User
{
    public class Register
    {
        public class Command : IRequest<User>
        {
            public string DisplayName { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
            }
        }

        public class Handler : IRequestHandler<Command, User>
        {
            private readonly UserManager<AppUser> userManager;
            private readonly IJwtGenerator jwtGenerator;
            private readonly DataContext context;
            public Handler(DataContext Context, UserManager<AppUser> UserManager, IJwtGenerator JwtGenerator)
            { this.jwtGenerator = JwtGenerator; this.userManager = UserManager; this.context = Context; }


            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await context.Users.Where(u => u.Email == request.Email).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already in use" });

                if (await context.Users.Where(u => u.UserName == request.UserName).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already in use" });

                var user = new AppUser { DisplayName = request.DisplayName, Email = request.Email, UserName = request.UserName };
                var result = await userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded) throw new Exception("Problem creating user");

                return new User
                {
                    DisplayName = user.DisplayName,
                    UserName = user.UserName,
                    Token = jwtGenerator.CreateToken(user),
                    Image = user.Photos.FirstOrDefault(x => x.IsMain)?.Url
                };
            }
        }
    }
}