using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class Contract : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string ContractNumber { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public Decimal MonthlyRent { get; set; } //tiền thuê định kỳ/Phí căn hộ
		public Decimal DepositAmount { get; set; } //Tiền đặt cọc
		public ContractStatusEnum Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid ApartmentId { get; set; }
		public required Apartment Apartment { get; set; }
		public Guid? ResidentImportBatchId { get; set; }
		public ResidentImportBatch? ResidentImportBatch { get; set; }

		public ICollection<ContractResident> ContractResidents { get; set; } = new List<ContractResident>();
	}
}
