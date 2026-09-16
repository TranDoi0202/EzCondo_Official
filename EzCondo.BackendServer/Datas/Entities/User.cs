using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class User : IAuditableEntity, ISoftDeletableEntity
	{
		public Guid Id { get; set; }
		public string? Email { get; set; }
		public string? NormalizedEmail { get; set; }
		public string? PhoneNumber { get; set; }
		public string? NormalizedPhoneNumber { get; set; }
		public string? PasswordHash { get; set; }
		public string? FullName { get; set; }
		public string? AvatarUrl { get; set; }
		public UserStatusEnum Status { get; set; }
		public DateTime? EmailVerifiedAt { get; set; }
		public DateTime? PhoneVerifiedAt { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public ICollection<TenantMembership> TenantMemberships { get; set; } = new List<TenantMembership>();
		public ICollection<PlatformUserRole> PlatformRoles { get; set; } = new List<PlatformUserRole>();
		public ICollection<StaffInvitation> SentInvitations { get; set; } = new List<StaffInvitation>();
		public ICollection<StaffInvitation> AcceptedInvitations { get; set; } = new List<StaffInvitation>();
		public ICollection<AuthSession> AuthSessions { get; set; } = new List<AuthSession>();
		public ICollection<AuthEvent> AuthEvents { get; set; } = new List<AuthEvent>();
	}
}
