﻿using skit.Core.Offers.Enums;

namespace skit.Application.Offers.Queries.DTO;

public sealed class OfferDetailsDto
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? DateFrom { get; set; }
    public DateTimeOffset? DateTo { get; set; }
    public List<OfferSeniority> Seniorities { get; set; }
    public List<OfferWorkLocation> WorkLocation { get; set; }
    public string CompanyName { get; set; }
    public string? CompanyDescription { get; set; }
    public Guid CompanyId { get; set; }
    public List<string> Cities { get; set; }
    public List<SalaryDto> Salaries { get; set; }
    public List<TechnologyDto> Technologies { get; set; }
}