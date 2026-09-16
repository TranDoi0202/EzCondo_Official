namespace EzCondo.BackendServer.Datas.Entities
{
	public class TimeKeeping : IAuditableEntity
	{
		public Guid Id { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime? CheckOut { get; set; }
		public bool IsAiVerified { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid TenantMembershipId { get; set; }
		public required TenantMembership TenantMembership { get; set; }
	}
}
