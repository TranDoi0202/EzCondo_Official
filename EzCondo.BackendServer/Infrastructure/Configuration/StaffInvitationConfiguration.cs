using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class StaffInvitationConfiguration : IEntityTypeConfiguration<StaffInvitation>
	{
		public void Configure(EntityTypeBuilder<StaffInvitation> b)
		{
			b.ToTable("Staff_Invitations", table =>
			{
				table.HasCheckConstraint(
					"CK_StaffInvitations_EmailOrPhone",
					"\"normalized_email\" IS NOT NULL OR \"normalized_phone_number\" IS NOT NULL");
				table.HasCheckConstraint(
					"CK_StaffInvitations_Lifecycle",
					"(\"status\" = 'pending' AND \"accepted_at\" IS NULL AND \"revoked_at\" IS NULL AND \"accepted_membership_id\" IS NULL) OR " +
					"(\"status\" = 'accepted' AND \"accepted_at\" IS NOT NULL AND \"accepted_by_user_id\" IS NOT NULL AND \"accepted_membership_id\" IS NOT NULL AND \"revoked_at\" IS NULL) OR " +
					"(\"status\" = 'expired' AND \"accepted_at\" IS NULL AND \"accepted_membership_id\" IS NULL) OR " +
					"(\"status\" = 'revoked' AND \"revoked_at\" IS NOT NULL AND \"accepted_at\" IS NULL AND \"accepted_membership_id\" IS NULL)");
				table.HasCheckConstraint(
					"CK_StaffInvitations_Expiry",
					"\"expires_at\" > \"created_at\"");
			});
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_StaffInvitations_Id_TenantId");

			b.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
			b.Property(x => x.FullName).HasColumnName("full_name").HasMaxLength(150).IsRequired();
			b.Property(x => x.Email).HasColumnName("email").HasMaxLength(254);
			b.Property(x => x.NormalizedEmail).HasColumnName("normalized_email").HasMaxLength(254);
			b.Property(x => x.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20);
			b.Property(x => x.NormalizedPhoneNumber).HasColumnName("normalized_phone_number").HasMaxLength(20);
			b.Property(x => x.TokenHash).HasColumnName("token_hash").HasMaxLength(255).IsRequired();
			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(InvitationStatusEnum.PENDING)
				.IsRequired();
			b.Property(x => x.ExpiresAt).HasColumnName("expires_at").IsRequired();
			b.Property(x => x.AcceptedAt).HasColumnName("accepted_at");
			b.Property(x => x.RevokedAt).HasColumnName("revoked_at");
			b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
			b.Property(x => x.UpdatedAt).HasColumnName("updated_at");

			b.Property(x => x.TenantId).HasColumnName("tenant_id").IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(x => x.StaffInvitations)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.RoleId).HasColumnName("role_id").IsRequired();
			b.HasOne(x => x.Role)
				.WithMany(x => x.StaffInvitations)
				.HasForeignKey(x => x.RoleId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.JobPositionId).HasColumnName("job_position_id");
			b.HasOne(x => x.JobPosition)
				.WithMany(x => x.StaffInvitations)
				.HasForeignKey(x => new { x.JobPositionId, x.TenantId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.InvitedByMembershipId).HasColumnName("invited_by_membership_id");
			b.HasOne(x => x.InvitedByMembership)
				.WithMany(x => x.SentInvitations)
				.HasForeignKey(x => new { x.InvitedByMembershipId, x.TenantId, x.InvitedByUserId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId, x.UserId })
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.InvitedByUserId).HasColumnName("invited_by_user_id").IsRequired();
			b.HasOne(x => x.InvitedByUser)
				.WithMany(x => x.SentInvitations)
				.HasForeignKey(x => x.InvitedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.AcceptedByUserId).HasColumnName("accepted_by_user_id");
			b.HasOne(x => x.AcceptedByUser)
				.WithMany(x => x.AcceptedInvitations)
				.HasForeignKey(x => x.AcceptedByUserId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.AcceptedMembershipId).HasColumnName("accepted_membership_id");
			b.HasOne(x => x.AcceptedMembership)
				.WithMany()
				.HasForeignKey(x => new { x.AcceptedMembershipId, x.TenantId, x.AcceptedByUserId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId, x.UserId })
				.OnDelete(DeleteBehavior.Restrict);

			b.HasIndex(x => x.TokenHash).IsUnique();
			b.HasIndex(x => x.ExpiresAt);
			b.HasIndex(x => new { x.TenantId, x.NormalizedEmail })
				.IsUnique()
				.HasFilter("\"status\" = 'pending' AND \"normalized_email\" IS NOT NULL");
			b.HasIndex(x => new { x.TenantId, x.NormalizedPhoneNumber })
				.IsUnique()
				.HasFilter("\"status\" = 'pending' AND \"normalized_phone_number\" IS NOT NULL");
		}
	}
}
