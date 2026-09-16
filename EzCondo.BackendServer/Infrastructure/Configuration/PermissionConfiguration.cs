using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class PermissionConfiguration : IEntityTypeConfiguration<Permission>
	{
		public void Configure(EntityTypeBuilder<Permission> b)
		{
			b.ToTable("Permissions");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.Scope)
				.HasColumnName("scope")
				.IsRequired();

			b.Property(x => x.Description)
				.HasColumnName("description")
				.HasMaxLength(255);

			b.Property(x => x.IsDeleted)
				.HasColumnName("is_deleted")
				.HasDefaultValue(false)
				.IsRequired();

			b.Property(x => x.DeletedAt)
				.HasColumnName("deleted_at");

			b.Property(x => x.FunctionId)
				.HasColumnName("function_id")
				.HasMaxLength(100)
				.IsRequired();
			b.HasOne(x => x.Function)
				.WithMany()
				.HasForeignKey(x => x.FunctionId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.CommandId)
				.HasColumnName("command_id")
				.HasMaxLength(50)
				.IsRequired();
			b.HasOne(x => x.Command)
				.WithMany()
				.HasForeignKey(x => x.CommandId)
				.OnDelete(DeleteBehavior.Restrict);

			b.HasIndex(p => new { p.FunctionId, p.CommandId })
				.IsUnique()
				.HasFilter("\"is_deleted\" = false")
				.HasDatabaseName("IX_Permissions_FunctionId_CommandId");
		}
	}
}
