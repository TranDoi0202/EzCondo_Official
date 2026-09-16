using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class TenantMembershipConfiguration : IEntityTypeConfiguration<TenantMembership>
	{
		public void Configure(EntityTypeBuilder<TenantMembership> b)
		{
			b.ToTable("Tenant_Memberships", table =>
				table.HasCheckConstraint(
					"CK_TenantMemberships_Lifecycle",
					"(\"status\" = 'invited' AND \"invited_at\" IS NOT NULL AND \"activated_at\" IS NULL AND \"revoked_at\" IS NULL) OR " +
					"(\"status\" = 'active' AND \"activated_at\" IS NOT NULL AND \"revoked_at\" IS NULL) OR " +
					"(\"status\" = 'suspended' AND \"revoked_at\" IS NULL) OR " +
					"(\"status\" = 'revoked' AND \"revoked_at\" IS NOT NULL)"));
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_TenantMemberships_Id_TenantId");
			b.HasAlternateKey(x => new { x.Id, x.TenantId, x.UserId })
				.HasName("AK_TenantMemberships_Id_TenantId_UserId");

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");
			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(MembershipStatusEnum.INVITED)
				.IsRequired();
			b.Property(x => x.InvitedAt).HasColumnName("invited_at");
			b.Property(x => x.ActivatedAt).HasColumnName("activated_at");
			b.Property(x => x.RevokedAt).HasColumnName("revoked_at");
			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();
			b.Property(x => x.UpdatedAt).HasColumnName("updated_at");

			b.Property(x => x.TenantId).HasColumnName("tenant_id").IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(x => x.TenantMemberships)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
			b.HasOne(x => x.User)
				.WithMany(x => x.TenantMemberships)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.RoleId).HasColumnName("role_id").IsRequired();
			b.HasOne(x => x.Role)
				.WithMany(x => x.TenantMemberships)
				.HasForeignKey(x => x.RoleId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.JobPositionId).HasColumnName("job_position_id");
			b.HasOne(x => x.JobPosition)
				.WithMany(x => x.TenantMemberships)
				.HasForeignKey(x => new { x.JobPositionId, x.TenantId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.InvitedByMembershipId).HasColumnName("invited_by_membership_id");
			b.HasOne(x => x.InvitedByMembership)
				.WithMany(x => x.InvitedMemberships)
				.HasForeignKey(x => new { x.InvitedByMembershipId, x.TenantId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.HasIndex(x => new { x.TenantId, x.UserId })
				.IsUnique()
				.HasDatabaseName("IX_TenantMemberships_TenantId_UserId");
		}
	}
}
