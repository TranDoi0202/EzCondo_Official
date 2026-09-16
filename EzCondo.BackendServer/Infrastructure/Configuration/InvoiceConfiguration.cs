using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
	{
		public void Configure(EntityTypeBuilder<Invoice> b)
		{
			b.ToTable("Invoices");
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_Invoices_Id_TenantId");

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.Title)
				.HasColumnName("title")
				.HasMaxLength(255)
				.IsRequired();

			b.Property(x => x.Type)
				.HasColumnName("type")
				.IsRequired();

			b.Property(x => x.Amount)
				.HasColumnName("amount")
				.HasPrecision(18, 2)
				.IsRequired();
			b.ToTable(t => t.HasCheckConstraint(
					"CK_Invoice_Amount",
					"\"amount\" > 0"
				));

			b.Property(x => x.AmountPaid)
				.HasColumnName("amount_paid")
				.HasPrecision(18, 2)
				.HasDefaultValue(0)
				.IsRequired();

			b.Property(x => x.AmountRemaining)
				.HasColumnName("amount_remaining")
				.HasPrecision(18, 2);

			b.Property(x => x.DueDate)
				.HasColumnName("due_date")
				.IsRequired();

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(InvoiceStatusEnum.PENDING)
				.IsRequired();

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(t => t.Invoices)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.BatchId)
				.HasColumnName("batch_id");
			b.HasOne(x => x.InvoiceBatch)
				.WithMany(i => i.Invoices)
				.HasForeignKey(x => new { x.BatchId, x.TenantId })
				.HasPrincipalKey(i => new { i.Id, i.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.ApartmentId)
				.HasColumnName("apartment_id")
				.IsRequired();
			b.HasOne(x => x.Apartment)
				.WithMany(a => a.Invoices)
				.HasForeignKey(x => new { x.ApartmentId, x.TenantId })
				.HasPrincipalKey(a => new { a.Id, a.TenantId })
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
