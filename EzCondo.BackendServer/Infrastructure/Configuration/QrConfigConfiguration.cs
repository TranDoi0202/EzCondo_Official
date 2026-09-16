using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class QrConfigConfiguration : IEntityTypeConfiguration<QrConfig>
	{
		public void Configure(EntityTypeBuilder<QrConfig> b)
		{
			b.ToTable("Qr_Configs");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.Type)
				.HasColumnName("type")
				.HasMaxLength(50)
				.IsRequired();

			b.Property(x => x.SecretKey)
				.HasColumnName("secret_key")
				.HasMaxLength(255)
				.IsRequired();

			b.Property(x => x.RefreshInterval)
				.HasColumnName("refresh_interval")
				.HasDefaultValue(30)
				.IsRequired();

			b.Property(x => x.IsActive)
				.HasColumnName("is_active")
				.HasDefaultValue(true)
				.IsRequired();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(tenant => tenant.QrConfigs)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
