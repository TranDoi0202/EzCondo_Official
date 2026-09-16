using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class Role : ISoftDeletableEntity
	{
		public Guid Id { get; set; }
		public RoleNameEnum RoleName { get; set; }
		public RoleScopeEnum RoleScope { get; set; }
		public int HierarchyLevel { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }

		public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
		public ICollection<TenantMembership> TenantMemberships { get; set; } = new List<TenantMembership>();
		public ICollection<PlatformUserRole> PlatformUsers { get; set; } = new List<PlatformUserRole>();
		public ICollection<StaffInvitation> StaffInvitations { get; set; } = new List<StaffInvitation>();
	}
}
