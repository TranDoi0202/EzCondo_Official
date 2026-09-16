using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class StaffInvitation : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string FullName { get; set; }
		public string? Email { get; set; }
		public string? NormalizedEmail { get; set; }
		public string? PhoneNumber { get; set; }
		public string? NormalizedPhoneNumber { get; set; }
		public required string TokenHash { get; set; }
		public InvitationStatusEnum Status { get; set; }
		public DateTime ExpiresAt { get; set; }
		public DateTime? AcceptedAt { get; set; }
		public DateTime? RevokedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid RoleId { get; set; }
		public required Role Role { get; set; }
		public Guid? JobPositionId { get; set; }
		public JobPosition? JobPosition { get; set; }
		public Guid? InvitedByMembershipId { get; set; }
		public TenantMembership? InvitedByMembership { get; set; }
		public Guid InvitedByUserId { get; set; }
		public required User InvitedByUser { get; set; }
		public Guid? AcceptedByUserId { get; set; }
		public User? AcceptedByUser { get; set; }
		public Guid? AcceptedMembershipId { get; set; }
		public TenantMembership? AcceptedMembership { get; set; }

		public ICollection<OtpChallenge> OtpChallenges { get; set; } = new List<OtpChallenge>();
	}
}
