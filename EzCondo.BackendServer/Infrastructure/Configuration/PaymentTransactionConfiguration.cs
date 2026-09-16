using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class PaymentTransactionConfiguration : IEntityTypeConfiguration<PaymentTransaction>
	{
		public void Configure(EntityTypeBuilder<PaymentTransaction> b)
		{
			b.ToTable("Payment_Transactions");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.OrderCode)
				.HasColumnName("order_code")
				.IsRequired();
			b.HasIndex(x => x.OrderCode)
				.IsUnique();

			b.Property(x => x.TransactionFlow)
				.HasColumnName("transaction_flow")
				.IsRequired();

			b.Property(x => x.PaymentLinkId)
				.HasColumnName("payment_link_id")
				.HasMaxLength(100);

			b.Property(x => x.Amount)
				.HasColumnName("amount")
				.HasPrecision(18, 2)
				.IsRequired();

			b.Property(x => x.Purpose)
				.HasColumnName("purpose")
				.HasMaxLength(255)
				.IsRequired();

			b.Property(x => x.CheckoutUrl)
				.HasColumnName("checkout_url")
				.HasMaxLength(500);

			b.Property(x => x.PayOsReference)
				.HasColumnName("payos_reference")
				.HasMaxLength(100);

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(PaymentTransactionStatusEnum.PENDING)
				.IsRequired();

			b.Property(x => x.PaidAt)
				.HasColumnName("paid_at");

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(t => t.PaymentTransactions)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.InvoiceId)
				.HasColumnName("invoice_id");
			b.HasOne(x => x.Invoice)
				.WithMany(i => i.PaymentTransactions)
				.HasForeignKey(x => new { x.InvoiceId, x.TenantId })
				.HasPrincipalKey(i => new { i.Id, i.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.TenantLicenseID)
				.HasColumnName("tenant_license_id");
			b.HasOne(x => x.TenantLicense)
				.WithMany(t => t.PaymentTransactions)
				.HasForeignKey(x => new { x.TenantLicenseID, x.TenantId })
				.HasPrincipalKey(t => new { t.Id, t.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.InitiatedByMembershipId)
				.HasColumnName("initiated_by_membership_id")
				.IsRequired();
			b.HasOne(x => x.InitiatedByMembership)
				.WithMany(u => u.PaymentTransactions)
				.HasForeignKey(x => new { x.InitiatedByMembershipId, x.TenantId })
				.HasPrincipalKey(m => new { m.Id, m.TenantId })
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
