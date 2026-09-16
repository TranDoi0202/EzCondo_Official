using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum RoleNameEnum
	{
		[PgName("admin")]
		ADMIN,
		[PgName("apartment_owner")]
		APARTMENT_OWNER,
		[PgName("staff")]
		STAFF,
		[PgName("resident")]
		RESIDENT
	}
}
