using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum TenantStatusEnum
	{
		[PgName("active")]
		ACTIVE,
		[PgName("suspended")]
		SUSPENDED,
		[PgName("trial")]
		TRIAL,
		[PgName("cancelled")]
		CANCELLED,
		[PgName("removed")]
		REMOVED
	}
}
