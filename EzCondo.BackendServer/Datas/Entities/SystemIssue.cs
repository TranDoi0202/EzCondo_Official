using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class SystemIssue
	{
		public Guid Id { get; set; }
		public required string Title {  get; set; }
		public required string Description { get; set; }
		public IssueSeverityEnum Severity { get; set; }
		public IssueStatusEnum Status { get; set; }
		public DateTime ReportedAt { get; set; }
		public DateTime? ResolvedAt { get; set; }

		public Guid? TenantId { get; set; }
		public Tenant? Tenant { get; set; }
	}
}
