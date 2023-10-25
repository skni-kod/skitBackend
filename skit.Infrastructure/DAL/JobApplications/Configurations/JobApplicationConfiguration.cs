using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using skit.Core.JobApplications.Entities;

namespace skit.Infrastructure.DAL.JobApplications.Configurations;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.HasQueryFilter(x => x.DeletedById == null);
    }
}