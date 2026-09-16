using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class Invoice : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string Title { get; set; }
		public InvoiceTypeEnum Type { get; set; }
		public Decimal Amount { get; set; }
		public Decimal AmountPaid { get; set; }
		public Decimal? AmountRemaining { get; set; }
		public DateTime DueDate { get; set; }
		public InvoiceStatusEnum Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid? BatchId { get; set; }
		public InvoiceBatch? InvoiceBatch { get; set; }
		public Guid ApartmentId { get; set; }
		public required Apartment Apartment { get; set; }

		public ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
		public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
	}
}
