using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class JobPositionConfiguration : IEntityTypeConfiguration<JobPosition>
	{
		public void Configure(EntityTypeBuilder<JobPosition> b)
		{
			b.ToTable("Job_Positions");
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_JobPositions_Id_TenantId");

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.Name)
				.HasColumnName("name")
				.HasMaxLength(150)
				.IsRequired();

			b.Property(x => x.Description)
				.HasColumnName("description")
				.HasMaxLength(500);

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

			b.Property(x => x.DepartmentId)
				.HasColumnName("department_id")
				.IsRequired();
			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Department)
				.WithMany(d => d.JobPositions)
				.HasForeignKey(x => new { x.DepartmentId, x.TenantId })
				.HasPrincipalKey(d => new { d.Id, d.TenantId })
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
