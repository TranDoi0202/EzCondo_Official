using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class ContractConfiguration : IEntityTypeConfiguration<Contract>
	{
		public void Configure(EntityTypeBuilder<Contract> b)
		{
			b.ToTable("Contracts");
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_Contracts_Id_TenantId");

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.ContractNumber)
				.HasColumnName("contract_number")
				.HasMaxLength(50)
				.IsRequired();
			b.HasIndex(x => new { x.TenantId, x.ContractNumber })
				.IsUnique()
				.HasDatabaseName("IX_Contracts_TenantId_ContractNumber");

			b.Property(x => x.StartDate)
				.HasColumnName("start_date")
				.IsRequired();

			b.Property(x => x.EndDate)
				.HasColumnName("end_date")
				.IsRequired();
			b.ToTable(table => table.HasCheckConstraint(
				"CK_Contracts_DateRange",
				"\"end_date\" > \"start_date\""));

			b.Property(x => x.MonthlyRent)
				.HasColumnName("month_rent")
				.HasPrecision(18, 2)
				.HasDefaultValue(0)
				.IsRequired();

			b.Property(x => x.DepositAmount)
				.HasColumnName("deposit_amount")
				.HasPrecision(18, 2)
				.HasDefaultValue(0)
				.IsRequired();

			b.Property(x => x.Status)
				.HasColumnName("status")
				.HasDefaultValue(ContractStatusEnum.ACTIVE)
				.IsRequired();

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(tenant => tenant.Contracts)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.ApartmentId)
				.HasColumnName("apartment_id")
				.IsRequired();
			b.HasOne(x => x.Apartment)
				.WithMany(apartment => apartment.Contracts)
				.HasForeignKey(x => new { x.ApartmentId, x.TenantId })
				.HasPrincipalKey(apartment => new { apartment.Id, apartment.TenantId })
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.ResidentImportBatchId).HasColumnName("resident_import_batch_id");
			b.HasOne(x => x.ResidentImportBatch)
				.WithMany(x => x.ImportedContracts)
				.HasForeignKey(x => new { x.ResidentImportBatchId, x.TenantId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

		}
	}
}
