using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class PeriodicReportConfiguration : IEntityTypeConfiguration<PeriodicReport>
	{
		public void Configure(EntityTypeBuilder<PeriodicReport> b)
		{
			b.ToTable("Periodic_Reports");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.ReportPeriod)
				.HasColumnName("report_period")
				.IsRequired();

			b.Property(x => x.ReportType)
				.HasColumnName("report_type")
				.IsRequired();

			b.Property(x => x.AttachedFileUrl)
				.HasColumnName("attached_file_url")
				.HasMaxLength(500);

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(ReportStatusEnum.SUBMITTED)
				.IsRequired();

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.ReviewedAt)
				.HasColumnName("reviewed_at");

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(t => t.PeriodicReports)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.PreparedByMembershipId)
				.HasColumnName("prepared_by_membership_id")
				.IsRequired();
			b.HasOne(x => x.PreparedByMembership)
				.WithMany(t => t.PeriodicReports)
				.HasForeignKey(x => new { x.PreparedByMembershipId, x.TenantId })
				.HasPrincipalKey(m => new { m.Id, m.TenantId })
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
