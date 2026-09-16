using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum PayoutBatchStatusEnum
	{
		[PgName("pending")]
		PENDING,
		[PgName("processing")]
		PROCESSING,
		[PgName("completed")]
		COMPLETED,
		[PgName("failed")]
		FAILED
	}
}
