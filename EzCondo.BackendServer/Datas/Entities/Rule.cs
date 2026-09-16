namespace EzCondo.BackendServer.Datas.Entities
{
	public class Rule : IAuditableEntity, ISoftDeletableEntity
	{
		public Guid Id { get; set; }
		public required string Title { get; set; }
		public required string Content { get; set; }
		public bool IsActive { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
	}
}
