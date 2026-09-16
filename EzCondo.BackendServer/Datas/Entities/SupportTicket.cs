using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class SupportTicket : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string Description { get; set; }
		public TicketCategoryEnum IssueCategory { get; set; }
		public TicketStatusEnum Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public DateTime? ResolvedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid CreatedByMembershipId { get; set; }
		public required TenantMembership CreatedByMembership { get; set; }
	}
}
