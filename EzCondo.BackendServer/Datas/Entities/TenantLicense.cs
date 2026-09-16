using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class TenantLicense : IAuditableEntity, ISoftDeletableEntity
	{
		public Guid Id { get; set; }
		public required string ContractNumber { get; set; } //Số hợp đồng bản quyền
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public LicenseStatusEnum Status { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid LicensePackageId { get; set; }
		public required LicensePackage LicensePackage { get; set; }
		
		public ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
	}
}
