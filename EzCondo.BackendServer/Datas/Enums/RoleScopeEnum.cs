using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum RoleScopeEnum
	{
		[PgName("platform")]
		PLATFORM,
		[PgName("tenant")]
		TENANT
	}
}
