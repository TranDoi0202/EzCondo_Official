using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class InvoiceBatchConfiguration : IEntityTypeConfiguration<InvoiceBatch>
	{
		public void Configure(EntityTypeBuilder<InvoiceBatch> b)
		{
			b.ToTable("Invoice_Batches");
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_InvoiceBatches_Id_TenantId");

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.BillingMonth)
				.HasColumnName("billing_month")
				.HasMaxLength(7)
				.IsRequired();

			b.Property(x => x.TotalAmount)
				.HasPrecision(18, 2)
				.HasColumnName("total_amount")
				.HasDefaultValue(0)
				.IsRequired();

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.ApprovedAt)
				.HasColumnName("approved_at");

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(t => t.InvoicesBatches)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(InvoiceBatchStatusEnum.DRAFT)
				.IsRequired();

			b.Property(x => x.PreparedByMembershipId)
				.HasColumnName("prepared_by_membership_id")
				.IsRequired();
			b.HasOne(x => x.PreparedByMembership)
				.WithMany(p => p.PreparedInvoiceBatches)
				.HasForeignKey(x => new { x.PreparedByMembershipId, x.TenantId })
				.HasPrincipalKey(m => new { m.Id, m.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.ApprovedByMembershipId)
				.HasColumnName("approved_by_membership_id");
			b.HasOne(x => x.ApprovedByMembership)
				.WithMany(a => a.ApprovedInvoiceBatches)
				.HasForeignKey(x => new { x.ApprovedByMembershipId, x.TenantId })
				.HasPrincipalKey(m => new { m.Id, m.TenantId })
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
