namespace EzCondo.BackendServer.Datas.Entities
{
	public class PayrollDetail
	{
		public Guid Id { get; set; }
		public Decimal BaseSalary { get; set; }
		public Decimal Allowances { get; set; } //Tổng các khoản phụ cấp
		public Decimal Deductions { get; set; } //Khấu trừ bảo hiểm/phạt/ứng tiền
		public Decimal NetPay {  get; set; } //Lương thực lãnh
		public required string BankBin { get; set; }
		public required string BankAccountNumber { get; set; }

		public Guid TenantId { get; set; }
		public Guid PayrollId { get; set; }
		public required Payroll Payroll { get; set; }
		public Guid TenantMembershipId { get; set; }
		public required TenantMembership TenantMembership { get; set; }

		public ICollection<PayoutItem> PayoutItems { get; set; } = new List<PayoutItem>();
	}
}
