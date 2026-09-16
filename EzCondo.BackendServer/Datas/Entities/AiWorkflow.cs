using EzCondo.BackendServer.Datas.JsonDataType;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class AiWorkflow : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string Title { get; set; }
		public required TaskPlannerDataList TaskPlannerData { get; set; }
		public bool IsApproved { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid? HitlApprovedByMembershipId { get; set; } //Quản lý duyệt
		public TenantMembership? ApprovedByMembership { get; set; }
	}
}
