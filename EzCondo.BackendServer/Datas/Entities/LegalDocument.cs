namespace EzCondo.BackendServer.Datas.Entities
{
	public class LegalDocument : IAuditableEntity, ISoftDeletableEntity
	{
		public Guid Id { get; set; }
		public required string DocCategory { get; set; }
		public required string FileUrl { get; set; }
		public DateTime? IssueDate { get; set; } //Ngày phát hành tài liệu
		public DateTime? ExpiryDate { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
	}
}
