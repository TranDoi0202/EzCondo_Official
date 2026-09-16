using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class PayoutBatch : IAuditableEntity
	{
		public Guid Id { get; set; }
		public int PayoutMonth { get; set; }
		public int PayoutYear { get; set; }
		public Decimal TotalAmount { get; set; }
		public PayoutBatchStatusEnum Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public required string ReferenceId { get; set; } //Mã đối soát PayOS

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid CreatedByMembershipId { get; set; }
		public required TenantMembership CreatedByMembership { get; set; }

		public ICollection<PayoutItem> PayoutItems { get; set; } = new List<PayoutItem>();
	}
}
