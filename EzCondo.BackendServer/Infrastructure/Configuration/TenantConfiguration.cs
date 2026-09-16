using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
	{
		public void Configure(EntityTypeBuilder<Tenant> b)
		{
			b.ToTable("Tenants");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.Code)
				.HasColumnName("code")
				.HasMaxLength(30)
				.IsRequired();
			b.HasIndex(x => x.Code).IsUnique();

			b.Property(x => x.Slug)
				.HasColumnName("slug")
				.HasMaxLength(100)
				.IsRequired();
			b.HasIndex(x => x.Slug).IsUnique();

			b.Property(x => x.TenantName)
				.HasColumnName("tenant_name")
				.HasMaxLength(255)
				.IsRequired();

			b.Property(x => x.ContactName)
				.HasColumnName("contact_name")
				.HasMaxLength(100)
				.IsRequired();

			b.Property(x => x.ContactPhone)
				.HasColumnName("contact_phone")
				.HasMaxLength(20)
				.IsRequired();

			b.Property(x => x.ContactEmail)
				.HasColumnName("contact_email")
				.HasMaxLength(100)
				.IsRequired();

			b.Property(x => x.Address)
				.HasColumnName("address")
				.IsRequired();

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(TenantStatusEnum.ACTIVE)
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
		}
	}
}
