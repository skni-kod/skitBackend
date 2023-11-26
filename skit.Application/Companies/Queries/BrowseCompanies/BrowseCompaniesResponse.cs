﻿using skit.Application.Companies.Queries.DTO;
using skit.Shared.Abstractions.Models;

namespace skit.Application.Companies.Queries.BrowseCompanies;

public sealed record BrowseCompaniesResponse(PaginatedList<CompanyDto> Companies);