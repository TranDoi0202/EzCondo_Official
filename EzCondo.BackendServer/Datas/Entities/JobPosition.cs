namespace EzCondo.BackendServer.Datas.Entities
{
	public class JobPosition : IAuditableEntity, ISoftDeletableEntity
	{
		public Guid Id { get; set; }
		public required string Name { get; set; } // Ví dụ: "Kỹ sư trưởng", "Bảo vệ ca ngày", "Kế toán viên"
		public string? Description { get; set; }

		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		// Khóa ngoại trỏ về Department (Phòng ban)
		public Guid DepartmentId { get; set; }
		public required Department Department { get; set; }
		public Guid TenantId { get; set; }

		public ICollection<TenantMembership> TenantMemberships { get; set; } = new List<TenantMembership>();
		public ICollection<StaffInvitation> StaffInvitations { get; set; } = new List<StaffInvitation>();
	}
}
