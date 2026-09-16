using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class AuthSessionConfiguration : IEntityTypeConfiguration<AuthSession>
	{
		public void Configure(EntityTypeBuilder<AuthSession> b)
		{
			b.ToTable("Auth_Sessions", table =>
			{
				table.HasCheckConstraint(
					"CK_AuthSessions_Expiry",
					"\"expires_at\" > \"created_at\"");
				table.HasCheckConstraint(
					"CK_AuthSessions_TenantContext",
					"(\"tenant_membership_id\" IS NULL AND \"tenant_id\" IS NULL) OR " +
					"(\"tenant_membership_id\" IS NOT NULL AND \"tenant_id\" IS NOT NULL)");
			});
			b.HasKey(x => x.Id);

			b.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
			b.Property(x => x.RefreshTokenHash).HasColumnName("refresh_token_hash").HasMaxLength(255).IsRequired();
			b.Property(x => x.DeviceName).HasColumnName("device_name").HasMaxLength(150);
			b.Property(x => x.IpAddress).HasColumnName("ip_address").HasMaxLength(45);
			b.Property(x => x.UserAgent).HasColumnName("user_agent").HasMaxLength(1000);
			b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
			b.Property(x => x.ExpiresAt).HasColumnName("expires_at").IsRequired();
			b.Property(x => x.RevokedAt).HasColumnName("revoked_at");

			b.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
			b.HasOne(x => x.User)
				.WithMany(x => x.AuthSessions)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.TenantId).HasColumnName("tenant_id");
			b.Property(x => x.TenantMembershipId).HasColumnName("tenant_membership_id");
			b.HasOne(x => x.TenantMembership)
				.WithMany(x => x.AuthSessions)
				.HasForeignKey(x => new { x.TenantMembershipId, x.TenantId, x.UserId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId, x.UserId })
				.OnDelete(DeleteBehavior.Restrict);

			b.HasIndex(x => x.RefreshTokenHash).IsUnique();
			b.HasIndex(x => x.ExpiresAt);
			b.HasIndex(x => new { x.TenantMembershipId, x.TenantId, x.UserId });
		}
	}
}
