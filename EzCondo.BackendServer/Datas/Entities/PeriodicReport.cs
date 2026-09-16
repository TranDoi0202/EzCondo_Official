using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class PeriodicReport : IAuditableEntity
	{
		public Guid Id { get; set; }
		public ReportPeriodEnum ReportPeriod { get; set; }
		public ReportTypeEnum ReportType { get; set; }
		public string? AttachedFileUrl { get; set; }
		public ReportStatusEnum Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }
		public DateTime? ReviewedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid PreparedByMembershipId { get; set; }
		public required TenantMembership PreparedByMembership { get; set; }
	}
}
