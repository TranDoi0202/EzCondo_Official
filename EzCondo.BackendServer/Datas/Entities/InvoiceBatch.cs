using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class InvoiceBatch : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string BillingMonth { get; set; }
		public Decimal TotalAmount {  get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public DateTime? ApprovedAt { get; set; }
		public InvoiceBatchStatusEnum Status { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid PreparedByMembershipId { get; set; }
		public required TenantMembership PreparedByMembership { get; set; }
		public Guid? ApprovedByMembershipId { get; set; }
		public TenantMembership? ApprovedByMembership { get; set; }

		public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
	}
}
