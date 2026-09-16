namespace EzCondo.BackendServer.Datas.Entities
{
	public class Department : IAuditableEntity, ISoftDeletableEntity
	{
		public Guid Id { get; set; }
		public required string Name { get; set; } // Ví dụ: "Ban Kỹ thuật", "Ban An ninh", "Ban Quản lý"
		public string? Description { get; set; }

		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		// Khóa ngoại trỏ về Tenant (Chung cư)
		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }

		public ICollection<JobPosition> JobPositions { get; set; } = new List<JobPosition>();
	}
}
