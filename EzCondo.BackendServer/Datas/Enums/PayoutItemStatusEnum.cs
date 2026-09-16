using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum PayoutItemStatusEnum
	{
		[PgName("pending")]
		PENDING,
		[PgName("success")]
		SUCCESS,
		[PgName("failed")]
		FAILED,
		[PgName("cancelled")]
		CANCELLED
	}
}
