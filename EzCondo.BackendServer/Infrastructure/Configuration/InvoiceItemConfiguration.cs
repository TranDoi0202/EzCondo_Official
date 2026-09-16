using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
	{
		public void Configure(EntityTypeBuilder<InvoiceItem> b)
		{
			b.ToTable("Invoice_Items");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.ItemName)
				.HasColumnName("item_name")
				.HasMaxLength(150)
				.IsRequired();

			b.Property(x => x.Quantity)
				.HasColumnName("quantity")
				.HasPrecision(10, 2)
				.IsRequired();
			b.ToTable(x => x.HasCheckConstraint(
					"CK_InvoiceItem_Quantity",
					"\"quantity\" > 0"
				));

			b.Property(x => x.UnitPrice)
				.HasColumnName("unit_price")
				.HasPrecision(18, 2)
				.IsRequired();

			b.Property(x => x.Amount)
				.HasColumnName("amount")
				.HasPrecision(18, 2)
				.IsRequired();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();

			b.Property(x => x.InvoiceId)
				.HasColumnName("invoice_id")
				.IsRequired();
			b.HasOne(x => x.Invoice)
				.WithMany(i => i.InvoiceItems)
				.HasForeignKey(x => new { x.InvoiceId, x.TenantId })
				.HasPrincipalKey(i => new { i.Id, i.TenantId })
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.ServiceConfigId)
				.HasColumnName("service_config_id");
			b.HasOne(x => x.ServiceConfig)
				.WithMany()
				.HasForeignKey(x => new { x.ServiceConfigId, x.TenantId })
				.HasPrincipalKey(s => new { s.Id, s.TenantId })
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
