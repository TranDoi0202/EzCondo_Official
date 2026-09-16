namespace EzCondo.BackendServer.Datas.Entities
{
	public class InvoiceItem
	{
		public Guid Id { get; set; }
		public required string ItemName { get; set; }
		public Decimal Quantity { get; set; }
		public Decimal UnitPrice { get; set; }
		public Decimal Amount { get; set; }

		public Guid TenantId { get; set; }
		public Guid InvoiceId { get; set; }
		public required Invoice Invoice { get; set; }
		public Guid? ServiceConfigId { get; set; }
		public ServiceConfig? ServiceConfig { get; set; }
	}
}
