using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class AiWorkFlowConfiguration : IEntityTypeConfiguration<AiWorkflow>
	{
		public void Configure(EntityTypeBuilder<AiWorkflow> b)
		{
			b.ToTable("AI_Work_Flow");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.Title)
				.HasColumnName("title")
				.HasMaxLength(255)
				.IsRequired();

			b.OwnsOne(x => x.TaskPlannerData, tp =>
			{
				tp.ToJson("task_planner_data");
				tp.OwnsMany(x => x.Task);
			});
			b.Navigation(x => x.TaskPlannerData).IsRequired();

			b.Property(x => x.IsApproved)
				.HasColumnName("is_approved")
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
				.WithMany(t => t.AiWorkflows)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.HitlApprovedByMembershipId)
				.HasColumnName("hitl_approved_by_membership_id");
			b.HasOne(x => x.ApprovedByMembership)
				.WithMany(u => u.AiWorkflows)
				.HasForeignKey(x => new { x.HitlApprovedByMembershipId, x.TenantId })
				.HasPrincipalKey(m => new { m.Id, m.TenantId })
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
