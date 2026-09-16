using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum ComplaintStatusEnum
	{
		[PgName("pending")]
		PENDING,
		[PgName("processing")]
		PROCESSING,
		[PgName("resolved")]
		RESOLVED,
		[PgName("rejected")]
		REJECTED
	}
}
