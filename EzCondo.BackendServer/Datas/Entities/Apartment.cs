using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class Apartment : IAuditableEntity
	{
		public Guid Id { get; set; }
		public required string RoomName { get; set; }
		public int Floor { get; set; }
		public decimal Area { get; set; } //Diện tích
		public ApartmentStatusEnum Status { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? UpdatedAt { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
		public Guid? ResidentImportBatchId { get; set; }
		public ResidentImportBatch? ResidentImportBatch { get; set; }

		public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
		public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
	}
}
