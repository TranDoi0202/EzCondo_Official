using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
	{
		public void Configure(EntityTypeBuilder<RolePermission> b)
		{
			b.ToTable("Role_Permission");
			b.HasKey(x => new {x.RoleId, x.PermissionId});

			b.Property(x => x.RoleId)
				.HasColumnName("role_id");
			b.HasOne(x => x.Role)
				.WithMany(r => r.RolePermissions)
				.HasForeignKey(x => x.RoleId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.PermissionId)
				.HasColumnName("permission_id");
			b.HasOne(x => x.Permission)
				.WithMany(p => p.RolePermissions)
				.HasForeignKey(x => x.PermissionId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
