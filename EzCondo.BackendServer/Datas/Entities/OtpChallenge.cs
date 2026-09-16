using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class OtpChallenge : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string PhoneNumber { get; set; }
		public required string NormalizedPhoneNumber { get; set; }
		public required string CodeHash { get; set; }
		public OtpPurposeEnum Purpose { get; set; }
		public int AttemptCount { get; set; }
		public int MaxAttempts { get; set; }
		public DateTime ExpiresAt { get; set; }
		public DateTime? ConsumedAt { get; set; }
		public DateTime LastSentAt { get; set; }
		public string? RequestedIp { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid? TenantId { get; set; }
		public Tenant? Tenant { get; set; }
		public Guid? ContractResidentId { get; set; }
		public ContractResident? ContractResident { get; set; }
		public Guid? StaffInvitationId { get; set; }
		public StaffInvitation? StaffInvitation { get; set; }
	}
}
