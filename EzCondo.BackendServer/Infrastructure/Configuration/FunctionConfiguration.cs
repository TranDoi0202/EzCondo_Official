using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class FunctionConfiguration : IEntityTypeConfiguration<Function>
	{
		public void Configure(EntityTypeBuilder<Function> b)
		{
			b.ToTable("Functions");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasMaxLength(100);

			b.Property(x => x.Name)
				.HasColumnName("name")
				.HasMaxLength(150)
				.IsRequired();

			b.Property(x => x.ParentId)
				.HasColumnName("parent_id")
				.HasMaxLength(100);
			b.HasOne(x => x.Parent)						//true?
				.WithMany(parent => parent.Children)
				.HasForeignKey(x => x.ParentId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.Url)
				.HasColumnName("url")
				.HasMaxLength(255);
		}
	}
}
