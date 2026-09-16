using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
	{
		public void Configure(EntityTypeBuilder<Payroll> b)
		{
			b.ToTable("Payrolls");
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_Payrolls_Id_TenantId");

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.Title)
				.HasColumnName("title")
				.HasMaxLength(255)
				.IsRequired();

			b.Property(x => x.DateMonth)
				.HasColumnName("date_month")
				.IsRequired();
			b.ToTable(t => t.HasCheckConstraint(
					"CK_Payrolls_DateMonth_Range",
					"\"date_month\" >= 1 AND \"date_month\" <= 12"
				));

			b.Property(x => x.DateYear)
				.HasColumnName("date_year")
				.IsRequired();

			b.Property(x => x.TotalAmount)
				.HasColumnName("total_amount")
				.HasPrecision(18, 2)
				.IsRequired();
			b.ToTable(t => t.HasCheckConstraint(
					"CK_Payrolls_TotalAmount_NonNegative",
					"\"total_amount\" >= 0"
				));

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(PayrollStatusEnum.DRAFT)
				.IsRequired();

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(tenant => tenant.Payrolls)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.CreatedByMembershipId)
				.HasColumnName("created_by_membership_id")
				.IsRequired();
			b.HasOne(x => x.CreatedByMembership)
				.WithMany(user => user.CreatedPayrolls)
				.HasForeignKey(x => new { x.CreatedByMembershipId, x.TenantId })
				.HasPrincipalKey(m => new { m.Id, m.TenantId })
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
