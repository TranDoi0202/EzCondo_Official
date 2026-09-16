using EzCondo.BackendServer.Datas.Enums;
using EzCondo.BackendServer.Datas.JsonDataType;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class LicensePackage : IAuditableEntity, ISoftDeletableEntity
	{
		public Guid Id { get; set; }
		public required string PackageName { get; set; }
		public Decimal Price { get; set; }
		public BillingCycleEnum BillingCycle { get; set; }
		public required FeatureList Features {  get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
	}
}
