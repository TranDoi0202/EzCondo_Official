using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicket>
	{
		public void Configure(EntityTypeBuilder<SupportTicket> b)
		{
			b.ToTable("Support_Tickets");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.Description)
				.HasColumnName("description")
				.IsRequired();

			b.Property(x => x.IssueCategory)
				.HasColumnName("issue_category")
				.IsRequired();

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(TicketStatusEnum.OPEN)
				.IsRequired();

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.ResolvedAt)
				.HasColumnName("resolved_at");

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(t => t.SupportTickets)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.CreatedByMembershipId)
				.HasColumnName("created_by_membership_id")
				.IsRequired();
			b.HasOne(x => x.CreatedByMembership)
				.WithMany(u => u.SupportTickets)
				.HasForeignKey(x => new { x.CreatedByMembershipId, x.TenantId })
				.HasPrincipalKey(m => new { m.Id, m.TenantId })
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
