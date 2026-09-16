using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class ApartmentConfiguration : IEntityTypeConfiguration<Apartment>
	{
		public void Configure(EntityTypeBuilder<Apartment> b)
		{
			b.ToTable("Apartments");
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_Apartments_Id_TenantId");

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.RoomName)
				.HasColumnName("room_name")
				.HasMaxLength(50)
				.IsRequired();

			b.Property(x => x.Floor)
				.HasColumnName("floor")
				.IsRequired();

			b.Property(x => x.Area)
				.HasColumnName("area")
				.HasPrecision(10, 2)
				.IsRequired();

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(ApartmentStatusEnum.AVAILABLE)
				.IsRequired();

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(tenant => tenant.Apartments)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.ResidentImportBatchId).HasColumnName("resident_import_batch_id");
			b.HasOne(x => x.ResidentImportBatch)
				.WithMany(x => x.ImportedApartments)
				.HasForeignKey(x => new { x.ResidentImportBatchId, x.TenantId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.HasIndex(p => new { p.TenantId, p.RoomName })
				.IsUnique()
				.HasDatabaseName("IX_Apartment_TenantId_RoomName");
		}
	}
}
