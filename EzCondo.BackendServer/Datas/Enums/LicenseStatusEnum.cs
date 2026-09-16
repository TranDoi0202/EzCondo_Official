using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum LicenseStatusEnum
	{
		[PgName("active")]
		ACTIVE,
		[PgName("expired")]
		EXPIRED,
		[PgName("cancelled")]
		CANCELLED,
		[PgName("removed")]
		REMOVED
	}
}
