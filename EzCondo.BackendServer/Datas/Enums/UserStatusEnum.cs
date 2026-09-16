using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum UserStatusEnum
	{
		[PgName("active")]
		ACTIVE,
		[PgName("locked")]
		LOCKED,
		[PgName("removed")]
		REMOVED
	}
}
