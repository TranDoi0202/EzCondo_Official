using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class CommandConfiguration : IEntityTypeConfiguration<Command>
	{
		public void Configure(EntityTypeBuilder<Command> b)
		{
			b.ToTable("Commands");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)	
				.HasColumnName("id")
				.HasMaxLength(50)
				.IsRequired();

			b.Property(x => x.Name)
				.HasColumnName("name")
				.HasMaxLength(100)
				.IsRequired();
		}
	}
}
