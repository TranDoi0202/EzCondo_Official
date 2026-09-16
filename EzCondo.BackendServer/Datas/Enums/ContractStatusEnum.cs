using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum ContractStatusEnum
	{
		[PgName("active")]
		ACTIVE,
		[PgName("expired")]
		EXPIRED,
		[PgName("terminated")]
		TERMINATED
	}
}
