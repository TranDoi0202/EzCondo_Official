using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class PaymentTransaction : IAuditableEntity
	{
		public Guid Id { get; set; }
		public long OrderCode { get; set; }
		public TransactionFlowEnum TransactionFlow { get; set; } //Phân luồng dòng tiền Ra/Vào
		public string? PaymentLinkId { get; set; } //Link định danh
		public Decimal Amount { get; set; }
		public required string Purpose { get; set; }
		public string? CheckoutUrl { get; set; } //url trang thanh toán
		public string? PayOsReference { get; set; }
		public PaymentTransactionStatusEnum	Status { get; set; }
		public DateTime? PaidAt { get; set; } //Thời điểm ngân hàng báo tiền về
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid? InvoiceId { get; set; } 
		public Invoice? Invoice { get; set; }
		public Guid? TenantLicenseID { get; set; }
		public TenantLicense? TenantLicense { get; set; }
		public Guid InitiatedByMembershipId { get; set; }
		public required TenantMembership InitiatedByMembership { get; set; }
	}
}
