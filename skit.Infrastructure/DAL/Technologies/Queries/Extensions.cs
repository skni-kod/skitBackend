﻿using skit.Application.Technologies.DTO;
using skit.Core.Technologies.Entities;

namespace skit.Infrastructure.DAL.Technologies.Queries;

internal static class Extensions
{
    public static TechnologiesDto AsDto(this Technology technology, string? imageUrl)
    {
        return new()
        {
            Id = technology.Id,
            Name = technology.Name,
            ImageUrl = imageUrl
        };
    }
}