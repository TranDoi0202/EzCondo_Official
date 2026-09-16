using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class ComplaintConfiguration : IEntityTypeConfiguration<Complaint>
	{
		public void Configure(EntityTypeBuilder<Complaint> b)
		{
			b.ToTable("Complaints");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.Title)
				.HasColumnName("title")
				.HasMaxLength(255)
				.IsRequired();

			b.Property(x => x.Content)
				.HasColumnName("content")
				.IsRequired();

			b.Property(x => x.AiRoutedTo)
				.HasColumnName("AI_routed_to")
				.HasMaxLength(100);

			b.Property(x => x.AiSentiment)
				.HasColumnName("AI_sentiment")
				.HasMaxLength(50);

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(ComplaintStatusEnum.PENDING)
				.IsRequired();

			b.Property(x => x.ResidentMembershipId)
				.HasColumnName("resident_membership_id")
				.IsRequired();
			b.HasOne(x => x.ResidentMembership)
				.WithMany(u => u.Complaints)
				.HasForeignKey(x => new { x.ResidentMembershipId, x.TenantId })
				.HasPrincipalKey(m => new { m.Id, m.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(t => t.Complaints)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
