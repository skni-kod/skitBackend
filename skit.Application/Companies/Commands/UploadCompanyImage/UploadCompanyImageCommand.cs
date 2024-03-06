using MediatR;
using Microsoft.AspNetCore.Http;

namespace skit.Application.Companies.Commands.UploadCompanyImage;

public sealed record UploadCompanyImageCommand(IFormFile File) : IRequest;
