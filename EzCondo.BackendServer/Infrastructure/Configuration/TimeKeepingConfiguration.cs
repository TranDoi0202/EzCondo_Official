using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class TimeKeepingConfiguration : IEntityTypeConfiguration<TimeKeeping>
	{
		public void Configure(EntityTypeBuilder<TimeKeeping> b)
		{
			b.ToTable("Time_Keepings");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.CheckIn)
				.HasColumnName("check_in")
				.IsRequired();

			b.Property(x => x.CheckOut)
				.HasColumnName("check_out");

			b.Property(x => x.IsAiVerified)
				.HasColumnName("is_AI_verified")
				.HasDefaultValue(false)
				.IsRequired();

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(tenant => tenant.TimeKeepings)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.TenantMembershipId)
				.HasColumnName("tenant_membership_id")
				.IsRequired();
			b.HasOne(x => x.TenantMembership)
				.WithMany(user => user.TimeKeepings)
				.HasForeignKey(x => new { x.TenantMembershipId, x.TenantId })
				.HasPrincipalKey(m => new { m.Id, m.TenantId })
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
