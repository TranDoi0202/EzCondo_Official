using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<User>
	{
		public void Configure(EntityTypeBuilder<User> b)
		{
			b.ToTable("Users", table => table.HasCheckConstraint(
				"CK_Users_EmailOrPhone",
				"\"normalized_email\" IS NOT NULL OR \"normalized_phone_number\" IS NOT NULL"));
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.Email)
				.HasColumnName("email")
				.HasMaxLength(254);

			b.Property(x => x.NormalizedEmail)
				.HasColumnName("normalized_email")
				.HasMaxLength(254);
			b.HasIndex(x => x.NormalizedEmail)
				.IsUnique()
				.HasFilter("\"is_deleted\" = false AND \"normalized_email\" IS NOT NULL");

			b.Property(x => x.PhoneNumber)
				.HasColumnName("phone_number")
				.HasMaxLength(20);

			b.Property(x => x.NormalizedPhoneNumber)
				.HasColumnName("normalized_phone_number")
				.HasMaxLength(20);
			b.HasIndex(x => x.NormalizedPhoneNumber)
				.IsUnique()
				.HasFilter("\"is_deleted\" = false AND \"normalized_phone_number\" IS NOT NULL");

			b.Property(x => x.PasswordHash)
				.HasColumnName("password_hash")
				.HasMaxLength(255);

			b.Property(x => x.FullName)
				.HasColumnName("full_name")
				.HasMaxLength(150);

			b.Property(x => x.AvatarUrl)
				.HasColumnName("avatar_url")
				.HasMaxLength(500);

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(UserStatusEnum.ACTIVE)
				.IsRequired();

			b.Property(x => x.EmailVerifiedAt)
				.HasColumnName("email_verified_at");

			b.Property(x => x.PhoneVerifiedAt)
				.HasColumnName("phone_verified_at");

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
