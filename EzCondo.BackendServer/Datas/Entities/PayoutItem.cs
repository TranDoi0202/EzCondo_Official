using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class PayoutItem
	{
		public Guid Id { get; set; }
		public required string ToBin { get; set; }
		public required string ToAccountNumber { get; set; }
		public Decimal Amount { get; set; }
		public required string ReferenceId { get; set; } //Mã đối soát từng lệnh con
		public PayoutItemStatusEnum Status { get; set; }

		public Guid TenantId { get; set; }
		public Guid BatchId { get; set; }
		public required PayoutBatch PayoutBatch { get; set; }
		public Guid PayrollDetailId { get; set; }
		public required PayrollDetail PayrollDetail { get; set; }
	}
}
