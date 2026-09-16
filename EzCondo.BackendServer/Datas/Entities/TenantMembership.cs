using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class TenantMembership : IAuditableEntity
	{
		public Guid Id { get; set; }
		public MembershipStatusEnum Status { get; set; }
		public DateTime? InvitedAt { get; set; }
		public DateTime? ActivatedAt { get; set; }
		public DateTime? RevokedAt { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid UserId { get; set; }
		public required User User { get; set; }
		public Guid RoleId { get; set; }
		public required Role Role { get; set; }
		public Guid? JobPositionId { get; set; }
		public JobPosition? JobPosition { get; set; }
		public Guid? InvitedByMembershipId { get; set; }
		public TenantMembership? InvitedByMembership { get; set; }

		public ICollection<TenantMembership> InvitedMemberships { get; set; } = new List<TenantMembership>();
		public ICollection<StaffInvitation> SentInvitations { get; set; } = new List<StaffInvitation>();
		public ICollection<ContractResident> ContractResidents { get; set; } = new List<ContractResident>();
		public ICollection<TimeKeeping> TimeKeepings { get; set; } = new List<TimeKeeping>();
		public ICollection<Payroll> CreatedPayrolls { get; set; } = new List<Payroll>();
		public ICollection<PayrollDetail> PayrollDetails { get; set; } = new List<PayrollDetail>();
		public ICollection<PayoutBatch> PayoutBatches { get; set; } = new List<PayoutBatch>();
		public ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
		public ICollection<AiWorkflow> AiWorkflows { get; set; } = new List<AiWorkflow>();
		public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
		public ICollection<Complaint> Complaints { get; set; } = new List<Complaint>();
		public ICollection<PeriodicReport> PeriodicReports { get; set; } = new List<PeriodicReport>();
		public ICollection<InvoiceBatch> PreparedInvoiceBatches { get; set; } = new List<InvoiceBatch>();
		public ICollection<InvoiceBatch> ApprovedInvoiceBatches { get; set; } = new List<InvoiceBatch>();
		public ICollection<ResidentImportBatch> ResidentImportBatches { get; set; } = new List<ResidentImportBatch>();
		public ICollection<AuthSession> AuthSessions { get; set; } = new List<AuthSession>();
		public ICollection<AuthEvent> AuthEvents { get; set; } = new List<AuthEvent>();
	}
}
