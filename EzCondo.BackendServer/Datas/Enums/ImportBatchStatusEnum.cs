using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum ImportBatchStatusEnum
	{
		[PgName("pending")]
		PENDING,
		[PgName("processing")]
		PROCESSING,
		[PgName("completed")]
		COMPLETED,
		[PgName("partially_completed")]
		PARTIALLY_COMPLETED,
		[PgName("failed")]
		FAILED
	}
}
