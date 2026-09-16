namespace EzCondo.BackendServer.Datas.Entities
{
	public class AuthSession
	{
		public Guid Id { get; set; }
		public required string RefreshTokenHash { get; set; }
		public string? DeviceName { get; set; }
		public string? IpAddress { get; set; }
		public string? UserAgent { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime ExpiresAt { get; set; }
		public DateTime? RevokedAt { get; set; }

		public Guid UserId { get; set; }
		public required User User { get; set; }
		public Guid? TenantId { get; set; }
		public Guid? TenantMembershipId { get; set; }
		public TenantMembership? TenantMembership { get; set; }
	}
}
