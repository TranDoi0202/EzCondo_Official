using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class TenantLicenseConfiguration : IEntityTypeConfiguration<TenantLicense>
	{
		public void Configure(EntityTypeBuilder<TenantLicense> b)
		{
			b.ToTable("Tenant_Licenses");
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_TenantLicenses_Id_TenantId");

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.ContractNumber)
				.HasColumnName("contract_number")
				.HasMaxLength(50)
				.IsRequired();
			b.HasIndex(x => x.ContractNumber)
				.IsUnique()
				.HasFilter("\"is_deleted\" = false");

			b.Property(x => x.StartDate)
				.HasColumnName("start_date")
				.IsRequired();

			b.Property(x => x.EndDate)
				.HasColumnName("end_date")
				.IsRequired();

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(LicenseStatusEnum.ACTIVE)
				.IsRequired();

			b.Property(x => x.IsDeleted)
				.HasColumnName("is_deleted")
				.HasDefaultValue(false)
				.IsRequired();

			b.Property(x => x.DeletedAt)
				.HasColumnName("deleted_at");

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.UpdatedAt)
				.HasColumnName("updated_at");

			b.Property(x => x.LicensePackageId)
				.HasColumnName("license_package_id");
			b.HasOne(x => x.LicensePackage)
				.WithMany()
				.HasForeignKey(x => x.LicensePackageId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id");
			b.HasOne(x => x.Tenant)
				.WithMany(tenant => tenant.TenantLicenses)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
