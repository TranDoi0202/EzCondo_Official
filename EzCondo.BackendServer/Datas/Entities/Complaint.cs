using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class Complaint : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string Title { get; set; }
		public required string Content { get; set; }
		public string? AiRoutedTo { get; set; } //AI agent tự động định tuyến
		public string? AiSentiment { get; set; } //AI phân loại mức độ khẩn cấp/cảm xúc
		public ComplaintStatusEnum Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid ResidentMembershipId { get; set; }
		public required TenantMembership ResidentMembership { get; set; }
	}
}
