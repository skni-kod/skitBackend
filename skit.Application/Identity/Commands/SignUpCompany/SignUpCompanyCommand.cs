using MediatR;

namespace skit.Application.Identity.Commands.SignUpCompany;

public sealed record SignUpCompanyCommand(
    string Email,
    string CompanyName,
    string Password,
    string ConfirmedPassword) : IRequest;
