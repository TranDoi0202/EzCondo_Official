using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum MembershipStatusEnum
	{
		[PgName("invited")]
		INVITED,
		[PgName("active")]
		ACTIVE,
		[PgName("suspended")]
		SUSPENDED,
		[PgName("revoked")]
		REVOKED
	}
}
