using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class AuthEventConfiguration : IEntityTypeConfiguration<AuthEvent>
	{
		public void Configure(EntityTypeBuilder<AuthEvent> b)
		{
			b.ToTable("Auth_Events", table => table.HasCheckConstraint(
				"CK_AuthEvents_MembershipContext",
				"\"tenant_membership_id\" IS NULL OR (\"tenant_id\" IS NOT NULL AND \"user_id\" IS NOT NULL)"));
			b.HasKey(x => x.Id);

			b.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
			b.Property(x => x.EventType).HasColumnName("event_type").IsRequired();
			b.Property(x => x.IsSuccessful).HasColumnName("is_successful").IsRequired();
			b.Property(x => x.FailureReason).HasColumnName("failure_reason").HasMaxLength(500);
			b.Property(x => x.IpAddress).HasColumnName("ip_address").HasMaxLength(45);
			b.Property(x => x.UserAgent).HasColumnName("user_agent").HasMaxLength(1000);
			b.Property(x => x.OccurredAt).HasColumnName("occurred_at").HasDefaultValueSql("now()").IsRequired();

			b.Property(x => x.UserId).HasColumnName("user_id");
			b.HasOne(x => x.User)
				.WithMany(x => x.AuthEvents)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.TenantId).HasColumnName("tenant_id");
			b.HasOne(x => x.Tenant)
				.WithMany(x => x.AuthEvents)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.TenantMembershipId).HasColumnName("tenant_membership_id");
			b.HasOne(x => x.TenantMembership)
				.WithMany(x => x.AuthEvents)
				.HasForeignKey(x => new { x.TenantMembershipId, x.TenantId, x.UserId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId, x.UserId })
				.OnDelete(DeleteBehavior.Restrict);

			b.HasIndex(x => x.OccurredAt).IsDescending(true);
			b.HasIndex(x => new { x.UserId, x.OccurredAt }).IsDescending(false, true);
			b.HasIndex(x => new { x.TenantId, x.OccurredAt }).IsDescending(false, true);
			b.HasIndex(x => new { x.TenantMembershipId, x.TenantId, x.UserId });
		}
	}
}
