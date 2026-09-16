using EzCondo.BackendServer.Datas.Enums;

namespace EzCondo.BackendServer.Datas.Entities
{
	public class Permission : ISoftDeletableEntity
	{
		public Guid Id { get; set; }
		public RoleScopeEnum Scope { get; set; }
		public string? Description { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime? DeletedAt { get; set; }

		public required string FunctionId { get; set; }
		public required Function Function { get; set; }//FK has data type string, so should get reference method?
		public required string CommandId { get; set; }
		public required Command Command { get; set; }//

		public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
	}
}
