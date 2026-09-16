using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class PlatformUserRoleConfiguration : IEntityTypeConfiguration<PlatformUserRole>
	{
		public void Configure(EntityTypeBuilder<PlatformUserRole> b)
		{
			b.ToTable("Platform_User_Roles");
			b.HasKey(x => new { x.UserId, x.RoleId });
			b.Property(x => x.UserId).HasColumnName("user_id");
			b.Property(x => x.RoleId).HasColumnName("role_id");

			b.HasOne(x => x.User)
				.WithMany(x => x.PlatformRoles)
				.HasForeignKey(x => x.UserId)
				.OnDelete(DeleteBehavior.Cascade);
			b.HasOne(x => x.Role)
				.WithMany(x => x.PlatformUsers)
				.HasForeignKey(x => x.RoleId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
