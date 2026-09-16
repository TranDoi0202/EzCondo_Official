using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class AuthEvent
	{
		public Guid Id { get; set; }
		public AuthEventTypeEnum EventType { get; set; }
		public bool IsSuccessful { get; set; }
		public string? FailureReason { get; set; }
		public string? IpAddress { get; set; }
		public string? UserAgent { get; set; }
		public DateTime OccurredAt { get; set; }

		public Guid? UserId { get; set; }
		public User? User { get; set; }
		public Guid? TenantId { get; set; }
		public Tenant? Tenant { get; set; }
		public Guid? TenantMembershipId { get; set; }
		public TenantMembership? TenantMembership { get; set; }
	}
}
