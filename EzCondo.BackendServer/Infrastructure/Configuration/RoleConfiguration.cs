using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class RoleConfiguration : IEntityTypeConfiguration<Role>
	{
		public void Configure(EntityTypeBuilder<Role> b)
		{
			b.ToTable("Roles");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.RoleName)
				.HasColumnName("role_name")
				.IsRequired();

			b.Property(x => x.RoleScope)
				.HasColumnName("role_scope")
				.IsRequired();

			b.Property(x => x.HierarchyLevel)
				.HasColumnName("hierarchy_level")
				.IsRequired();

			b.Property(x => x.IsDeleted)
				.HasColumnName("is_deleted")
				.HasDefaultValue(false)
				.IsRequired();

			b.Property(x => x.DeletedAt)
				.HasColumnName("deleted_at");

			b.HasIndex(x => new { x.RoleName, x.RoleScope })
				.IsUnique()
				.HasFilter("\"is_deleted\" = false");
		}
	}
}
