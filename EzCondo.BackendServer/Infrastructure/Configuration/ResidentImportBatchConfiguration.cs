using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class ResidentImportBatchConfiguration : IEntityTypeConfiguration<ResidentImportBatch>
	{
		public void Configure(EntityTypeBuilder<ResidentImportBatch> b)
		{
			b.ToTable("Resident_Import_Batches", table => table.HasCheckConstraint(
				"CK_ResidentImportBatches_Counts",
				"\"total_rows\" >= 0 AND \"successful_rows\" >= 0 AND \"failed_rows\" >= 0 AND " +
				"\"successful_rows\" + \"failed_rows\" <= \"total_rows\""));
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_ResidentImportBatches_Id_TenantId");

			b.Property(x => x.Id).HasColumnName("id").HasDefaultValueSql("gen_random_uuid()");
			b.Property(x => x.OriginalFileName).HasColumnName("original_file_name").HasMaxLength(255).IsRequired();
			b.Property(x => x.FileUrl).HasColumnName("file_url").HasMaxLength(1000).IsRequired();
			b.Property(x => x.FileHash).HasColumnName("file_hash").HasMaxLength(128).IsRequired();
			b.Property(x => x.Status).HasColumnName("status").HasDefaultValue(ImportBatchStatusEnum.PENDING).IsRequired();
			b.Property(x => x.TotalRows).HasColumnName("total_rows").HasDefaultValue(0).IsRequired();
			b.Property(x => x.SuccessfulRows).HasColumnName("successful_rows").HasDefaultValue(0).IsRequired();
			b.Property(x => x.FailedRows).HasColumnName("failed_rows").HasDefaultValue(0).IsRequired();
			b.Property(x => x.CompletedAt).HasColumnName("completed_at");
			b.Property(x => x.CreatedAt).HasColumnName("created_at").HasDefaultValueSql("now()").IsRequired();
			b.Property(x => x.UpdatedAt).HasColumnName("updated_at");

			b.Property(x => x.TenantId).HasColumnName("tenant_id").IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(x => x.ResidentImportBatches)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.UploadedByMembershipId).HasColumnName("uploaded_by_membership_id").IsRequired();
			b.HasOne(x => x.UploadedByMembership)
				.WithMany(x => x.ResidentImportBatches)
				.HasForeignKey(x => new { x.UploadedByMembershipId, x.TenantId })
				.HasPrincipalKey(x => new { x.Id, x.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.HasIndex(x => new { x.TenantId, x.FileHash }).IsUnique();
		}
	}
}
