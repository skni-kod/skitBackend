using MediatR;
using skit.Core.Identity.DTO;

namespace skit.Application.Identity.Commands.SignUpCompany;

public sealed record SignUpCommand(
    string Email,
    string Password,
    string ConfirmedPassword) : IRequest<JsonWebToken>;
