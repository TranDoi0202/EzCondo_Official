using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class LegalDocumentConfiguration : IEntityTypeConfiguration<LegalDocument>
	{
		public void Configure(EntityTypeBuilder<LegalDocument> b)
		{
			b.ToTable("Legal_Documents");
			b.HasKey(x => x.Id);

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.DocCategory)
				.HasColumnName("doc_category")
				.HasMaxLength(100)
				.IsRequired();

			b.Property(x => x.FileUrl)
				.HasColumnName("file_url")
				.HasMaxLength(500)
				.IsRequired();

			b.Property(x => x.IssueDate)
				.HasColumnName("issue_date");

			b.Property(x => x.ExpiryDate)
				.HasColumnName("expiry_date");

			b.Property(x => x.IsDeleted)
				.HasColumnName("is_deleted")
				.HasDefaultValue(false)
				.IsRequired();

			b.Property(x => x.DeletedAt)
				.HasColumnName("deleted_at");

			b.Property(x => x.CreatedAt)
				.HasColumnName("created_at")
				.HasDefaultValueSql("now()")
				.IsRequired();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();
			b.HasOne(x => x.Tenant)
				.WithMany(tenant => tenant.LegalDocuments)
				.HasForeignKey(x => x.TenantId)
				.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
