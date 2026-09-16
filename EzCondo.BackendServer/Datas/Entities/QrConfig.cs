namespace EzCondo.BackendServer.Datas.Entities
{
	public class QrConfig
	{
		public Guid Id { get; set; }
		public required string Type { get; set; }
		public required string SecretKey { get; set; }
		public int RefreshInterval { get; set; }
		public bool IsActive { get; set; }

		public Guid TenantId { get; set; }
		public required Tenant Tenant { get; set; }
	}
}
