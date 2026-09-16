using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class ResidentImportBatch : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string OriginalFileName { get; set; }
		public required string FileUrl { get; set; }
		public required string FileHash { get; set; }
		public ImportBatchStatusEnum Status { get; set; }
		public int TotalRows { get; set; }
		public int SuccessfulRows { get; set; }
		public int FailedRows { get; set; }
		public DateTime? CompletedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid UploadedByMembershipId { get; set; }
		public required TenantMembership UploadedByMembership { get; set; }

		public ICollection<Apartment> ImportedApartments { get; set; } = new List<Apartment>();
		public ICollection<Contract> ImportedContracts { get; set; } = new List<Contract>();
		public ICollection<ContractResident> ImportedResidents { get; set; } = new List<ContractResident>();
	}
}
