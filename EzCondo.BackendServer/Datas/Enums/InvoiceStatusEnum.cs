using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum InvoiceStatusEnum
	{
		[PgName("pending")]
		PENDING,
		[PgName("partial")]
		PARTIAL,
		[PgName("paid")]
		PAID,
		[PgName("cancelled")]
		CANCELLED
	}
}
