using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class ContractResident : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string FullName { get; set; }
		public required string PhoneNumber { get; set; }
		public required string NormalizedPhoneNumber { get; set; }
		public string? Email { get; set; }
		public string? NormalizedEmail { get; set; }
		public ResidentTypeEnum ResidentType { get; set; }
		public bool IsPrimary { get; set; }
		public DateTime? ActivatedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid ContractId { get; set; }
		public required Contract Contract { get; set; }
		public Guid? TenantMembershipId { get; set; }
		public TenantMembership? TenantMembership { get; set; }
		public Guid? ResidentImportBatchId { get; set; }
		public ResidentImportBatch? ResidentImportBatch { get; set; }

		public ICollection<OtpChallenge> OtpChallenges { get; set; } = new List<OtpChallenge>();
	}
}
