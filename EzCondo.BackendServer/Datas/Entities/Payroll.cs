using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class Payroll : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string Title { get; set; }
		public int DateMonth { get; set; } //Tháng kỳ lương
		public int DateYear { get; set; } //Năm kỳ lương
		public Decimal TotalAmount { get; set; }
		public PayrollStatusEnum Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid CreatedByMembershipId { get; set; }
		public required TenantMembership CreatedByMembership { get; set; }

		public ICollection<PayrollDetail> PayrollDetails { get; set; } = new List<PayrollDetail>();
	}
}
