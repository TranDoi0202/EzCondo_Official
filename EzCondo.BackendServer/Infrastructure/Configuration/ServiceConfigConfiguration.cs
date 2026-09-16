using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class ServiceConfigConfiguration : IEntityTypeConfiguration<ServiceConfig>
	{
		public void Configure(EntityTypeBuilder<ServiceConfig> b)
		{
			b.ToTable("Service_Configs");
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_ServiceConfigs_Id_TenantId");

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.ServiceCode)
				.HasColumnName("service_code")
				.HasMaxLength(50)
				.IsRequired();

			b.Property(x => x.ServiceName)
				.HasColumnName("service_name")
				.HasMaxLength(150)
				.IsRequired();

			b.Property(x => x.Unit)
				.HasColumnName("unit")
				.HasMaxLength(20)
				.IsRequired();

			b.Property(x => x.UnitPrice)
				.HasColumnName("unit_price")
				.HasPrecision(18, 2)
				.IsRequired();

			b.Property(x => x.VatRate)
				.HasColumnName("vat_rate")
				.HasPrecision(5, 2)
				.HasDefaultValue(0)
				.IsRequired();

			b.Property(x => x.IsDeleted)
				.HasColumnName("is_deleted")
				.HasDefaultValue(false)
				.IsRequired();

			b.Property(x => x.DeletedAt)
				.HasColumnName("deleted_at");

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(tenant => tenant.ServiceConfigs)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
