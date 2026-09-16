using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class SystemIssueConfiguration : IEntityTypeConfiguration<SystemIssue>
	{
		public void Configure(EntityTypeBuilder<SystemIssue> b)
		{
			b.ToTable("System_Issues");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.Title)
				.HasColumnName("title")
				.HasMaxLength(255)
				.IsRequired();

			b.Property(x => x.Description)
				.HasColumnName("description");

			b.Property(x => x.Severity)
				.HasColumnName("severity")
				.IsRequired();

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(IssueStatusEnum.OPEN)
				.IsRequired();

			b.Property(x => x.ReportedAt)
				.HasColumnName("reported_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.ResolvedAt)
				.HasColumnName("resolved_at");

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id");
			b.HasOne(x => x.Tenant)
				.WithMany(t => t.SystemIssues)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
