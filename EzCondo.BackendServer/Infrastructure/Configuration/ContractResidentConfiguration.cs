using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class ContractResidentConfiguration : IEntityTypeConfiguration<ContractResident>
	{
		public void Configure(EntityTypeBuilder<ContractResident> b)
		{
			b.ToTable("Contract_Residents", table => table.HasCheckConstraint(
				"CK_ContractResidents_Activation",
				"(\"tenant_membership_id\" IS NULL AND \"activated_at\" IS NULL) OR " +
				"(\"tenant_membership_id\" IS NOT NULL AND \"activated_at\" IS NOT NULL)"));
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_ContractResidents_Id_TenantId");

			b.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
			b.Property(x => x.FullName).HasColumnName("full_name").HasMaxLength(150).IsRequired();
			b.Property(x => x.PhoneNumber).HasColumnName("phone_number").HasMaxLength(20).IsRequired();
			b.Property(x => x.NormalizedPhoneNumber).HasColumnName("normalized_phone_number").HasMaxLength(20).IsRequired();
			b.Property(x => x.Email).HasColumnName("email").HasMaxLength(254);
			b.Property(x => x.NormalizedEmail).HasColumnName("normalized_email").HasMaxLength(254);
			b.Property(x => x.ResidentType).HasColumnName("resident_type").IsRequired();
			b.Property(x => x.IsPrimary).HasColumnName("is_primary").HasDefaultValue(false).IsRequired();
			b.Property(x => x.ActivatedAt).HasColumnName("activated_at");
			b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
			b.Property(x => x.UpdatedAt).HasColumnName("updated_at");

			b.Property(x => x.TenantId).HasColumnName("tenant_id").IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(x => x.ContractResidents)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.ContractId).HasColumnName("contract_id").IsRequired();
			b.HasOne(x => x.Contract)
				.WithMany(x => x.ContractResidents)
				.HasForeignKey(x => new { x.ContractId, x.TenantId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId })
				.OnDelete(DeleteBehavior.Cascade);

			b.Property(x => x.TenantMembershipId).HasColumnName("tenant_membership_id");
			b.HasOne(x => x.TenantMembership)
				.WithMany(x => x.ContractResidents)
				.HasForeignKey(x => new { x.TenantMembershipId, x.TenantId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.ResidentImportBatchId).HasColumnName("resident_import_batch_id");
			b.HasOne(x => x.ResidentImportBatch)
				.WithMany(x => x.ImportedResidents)
				.HasForeignKey(x => new { x.ResidentImportBatchId, x.TenantId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.HasIndex(x => new { x.ContractId, x.NormalizedPhoneNumber }).IsUnique();
			b.HasIndex(x => new { x.TenantId, x.NormalizedPhoneNumber });
			b.HasIndex(x => new { x.TenantMembershipId, x.TenantId });
		}
	}
}
