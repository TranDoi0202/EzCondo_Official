using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class LicensePackageConfiguration : IEntityTypeConfiguration<LicensePackage>
	{
		public void Configure(EntityTypeBuilder<LicensePackage> b)
		{
			b.ToTable("License_Packages");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.PackageName)
				.HasColumnName("package_name")
				.HasMaxLength(150)
				.IsRequired();
			b.HasIndex(x => x.PackageName)
				.IsUnique()
				.HasFilter("\"is_deleted\" = false");

			b.Property(x => x.Price)
				.HasColumnName("price")
				.HasPrecision(18, 2)
				.IsRequired();

			b.Property(x => x.BillingCycle)
				.HasColumnName("billing_cycle")
				.IsRequired();

			b.OwnsOne(x => x.Features, f =>
			{
				f.ToJson("features");
			});
			b.Navigation(x => x.Features).IsRequired();

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
		}
	}
}
