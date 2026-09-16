namespace EzCondo.BackendServer.Datas.Entities
{
	public class PlatformUserRole
	{
		public Guid UserId { get; set; }
		public required User User { get; set; }
		public Guid RoleId { get; set; }
		public required Role Role { get; set; }
	}
}
