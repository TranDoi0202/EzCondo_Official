using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class PayoutBatchConfiguration : IEntityTypeConfiguration<PayoutBatch>
	{
		public void Configure(EntityTypeBuilder<PayoutBatch> b)
		{
			b.ToTable("Payout_Batches");
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_PayoutBatches_Id_TenantId");

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.PayoutMonth)
				.HasColumnName("payout_month")
				.IsRequired();

			b.Property(x => x.PayoutYear)
				.HasColumnName("payout_year")
				.IsRequired();

			b.Property(x => x.TotalAmount)
				.HasColumnName("total_amount")
				.HasPrecision(18, 2)
				.IsRequired();

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(PayoutBatchStatusEnum.PENDING)
				.IsRequired();

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.ReferenceId)
				.HasColumnName("reference_id")
				.HasMaxLength(100)
				.IsRequired();
			b.HasIndex(x => x.ReferenceId)
				.IsUnique();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(t => t.PayoutBatches)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.CreatedByMembershipId)
				.HasColumnName("created_by_membership_id")
				.IsRequired();
			b.HasOne(x => x.CreatedByMembership)
				.WithMany(u => u.PayoutBatches)
				.HasForeignKey(x => new { x.CreatedByMembershipId, x.TenantId })
				.HasPrincipalKey(m => new { m.Id, m.TenantId })
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
