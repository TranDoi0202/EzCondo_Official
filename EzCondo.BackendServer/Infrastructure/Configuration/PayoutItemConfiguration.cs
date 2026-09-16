using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class PayoutItemConfiguration : IEntityTypeConfiguration<PayoutItem>
	{
		public void Configure(EntityTypeBuilder<PayoutItem> b)
		{
			b.ToTable("Payout_Items");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.ToBin)
				.HasColumnName("to_bin")
				.HasMaxLength(20)
				.IsRequired();

			b.Property(x => x.ToAccountNumber)
				.HasColumnName("to_account_number")
				.IsRequired();

			b.Property(x => x.Amount)
				.HasColumnName("amount")
				.HasPrecision(18, 2)
				.IsRequired();

			b.Property(x => x.ReferenceId)
				.HasColumnName("reference_id")
				.HasMaxLength(100)
				.IsRequired();

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(PayoutItemStatusEnum.PENDING)
				.IsRequired();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();

			b.Property(x => x.BatchId)
				.HasColumnName("batch_id")
				.IsRequired();
			b.HasOne(x => x.PayoutBatch)
				.WithMany(p => p.PayoutItems)
				.HasForeignKey(x => new { x.BatchId, x.TenantId })
				.HasPrincipalKey(p => new { p.Id, p.TenantId })
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.PayrollDetailId)
				.HasColumnName("payroll_detail_id")
				.IsRequired();
			b.HasOne(x => x.PayrollDetail)
				.WithMany(p => p.PayoutItems)
				.HasForeignKey(x => new { x.PayrollDetailId, x.TenantId })
				.HasPrincipalKey(p => new { p.Id, p.TenantId })
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
