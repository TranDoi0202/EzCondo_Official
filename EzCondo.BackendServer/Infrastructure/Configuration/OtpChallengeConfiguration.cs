using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class OtpChallengeConfiguration : IEntityTypeConfiguration<OtpChallenge>
	{
		public void Configure(EntityTypeBuilder<OtpChallenge> b)
		{
			b.ToTable("Otp_Challenges", table =>
			{
				table.HasCheckConstraint(
					"CK_OtpChallenges_AttemptRange",
					"\"attempt_count\" >= 0 AND \"max_attempts\" > 0 AND \"attempt_count\" <= \"max_attempts\"");
				table.HasCheckConstraint(
					"CK_OtpChallenges_Target",
					"(\"purpose\" = 'resident_activation' AND \"contract_resident_id\" IS NOT NULL AND \"staff_invitation_id\" IS NULL AND \"tenant_id\" IS NOT NULL) OR " +
					"(\"purpose\" = 'staff_activation' AND \"staff_invitation_id\" IS NOT NULL AND \"contract_resident_id\" IS NULL AND \"tenant_id\" IS NOT NULL) OR " +
					"(\"purpose\" = 'login' AND \"contract_resident_id\" IS NULL AND \"staff_invitation_id\" IS NULL)");
				table.HasCheckConstraint(
					"CK_OtpChallenges_Expiry",
					"\"expires_at\" > \"created_at\"");
			});
			b.HasKey(x => x.Id);

			b.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
			b.Property(x => x.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20).IsRequired();
			b.Property(x => x.NormalizedPhoneNumber).HasColumnName("normalized_phone_number").HasMaxLength(20).IsRequired();
			b.Property(x => x.CodeHash).HasColumnName("code_hash").HasMaxLength(255).IsRequired();
			b.Property(x => x.Purpose).HasColumnName("purpose").IsRequired();
			b.Property(x => x.AttemptCount).HasColumnName("attempt_count").HasDefaultValue(0).IsRequired();
			b.Property(x => x.MaxAttempts).HasColumnName("max_attempts").HasDefaultValue(5).IsRequired();
			b.Property(x => x.ExpiresAt).HasColumnName("expires_at").IsRequired();
			b.Property(x => x.ConsumedAt).HasColumnName("consumed_at");
			b.Property(x => x.LastSentAt).HasColumnName("last_sent_at").IsRequired();
			b.Property(x => x.RequestedIp).HasColumnName("requested_ip").HasMaxLength(45);
			b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
			b.Property(x => x.UpdatedAt).HasColumnName("updated_at");

			b.Property(x => x.TenantId).HasColumnName("tenant_id");
			b.HasOne(x => x.Tenant)
				.WithMany(x => x.OtpChallenges)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.ContractResidentId).HasColumnName("contract_resident_id");
			b.HasOne(x => x.ContractResident)
				.WithMany(x => x.OtpChallenges)
				.HasForeignKey(x => new { x.ContractResidentId, x.TenantId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.StaffInvitationId).HasColumnName("staff_invitation_id");
			b.HasOne(x => x.StaffInvitation)
				.WithMany(x => x.OtpChallenges)
				.HasForeignKey(x => new { x.StaffInvitationId, x.TenantId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.HasIndex(x => x.ExpiresAt);
			b.HasIndex(x => new { x.NormalizedPhoneNumber, x.CreatedAt }).IsDescending(false, true);
			b.HasIndex(x => new { x.ContractResidentId, x.TenantId });
			b.HasIndex(x => new { x.StaffInvitationId, x.TenantId });
		}
	}
}
