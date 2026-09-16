using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum InvoiceBatchStatusEnum
	{
		[PgName("draft")]
		DRAFT,
		[PgName("pending")]
		PENDING,
		[PgName("approved")]
		APPROVED,
		[PgName("rejected")]
		REJECTED
	}
}
