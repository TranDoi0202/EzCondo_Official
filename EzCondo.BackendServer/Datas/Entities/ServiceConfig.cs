namespace EzCondo.BackendServer.Datas.Entities
{
	public class ServiceConfig : ISoftDeletableEntity
	{
		public Guid Id { get; set; }
		public required string ServiceCode { get; set; }
		public required string ServiceName { get; set; }
		public required string Unit { get; set; }
		public Decimal UnitPrice { get; set; }
		public Decimal VatRate { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
	}
}
