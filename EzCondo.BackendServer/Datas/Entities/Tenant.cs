using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class Tenant : IAuditableEntity, ISoftDeletableEntity
	{
		public Guid Id { get; set; }
		public required string Code { get; set; }
		public required string Slug { get; set; }
		public required string TenantName { get; set; } //Tên chung cư
		public required string ContactName { get; set; } //đai diện pháp lý
		public required string ContactPhone { get; set; }
		public required string ContactEmail { get; set; }
		public required string Address { get; set; }
		public TenantStatusEnum Status { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public ICollection<TenantLicense> TenantLicenses {  get; set; } = new List<TenantLicense>();
		public ICollection<Department> Departments { get; set; } = new List<Department>();
		public ICollection<TenantMembership> TenantMemberships { get; set; } = new List<TenantMembership>();
		public ICollection<StaffInvitation> StaffInvitations { get; set; } = new List<StaffInvitation>();
		public ICollection<ContractResident> ContractResidents { get; set; } = new List<ContractResident>();
		public ICollection<OtpChallenge> OtpChallenges { get; set; } = new List<OtpChallenge>();
		public ICollection<AuthEvent> AuthEvents { get; set; } = new List<AuthEvent>();
		public ICollection<ResidentImportBatch> ResidentImportBatches { get; set; } = new List<ResidentImportBatch>();
		public ICollection<Apartment> Apartments { get; set; } = new List<Apartment>();
		public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
		public ICollection<Rule> Rules { get; set; } = new List<Rule>();
		public ICollection<LegalDocument> LegalDocuments { get; set; } = new List<LegalDocument>();
		public ICollection<ServiceConfig> ServiceConfigs { get; set; } = new List<ServiceConfig>();
		public ICollection<QrConfig> QrConfigs { get; set; } = new List<QrConfig>();
		public ICollection<TimeKeeping> TimeKeepings { get; set; } = new List<TimeKeeping>();
		public ICollection<Payroll> Payrolls { get; set; } = new List<Payroll>();
		public ICollection<PayoutBatch> PayoutBatches { get; set; } = new List<PayoutBatch>();
		public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
		public ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
		public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
		public ICollection<AiWorkflow> AiWorkflows { get; set; } = new List<AiWorkflow>();
		public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
		public ICollection<SystemIssue> SystemIssues { get; set; } = new List<SystemIssue>();
		public ICollection<PeriodicReport> PeriodicReports { get; set; } = new List<PeriodicReport>();
		public ICollection<InvoiceBatch> InvoicesBatches { get; set; } = new List<InvoiceBatch>();
	}
}
