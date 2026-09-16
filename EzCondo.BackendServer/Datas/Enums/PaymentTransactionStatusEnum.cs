using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum PaymentTransactionStatusEnum
	{
		[PgName("pending")]
		PENDING,
		[PgName("paid")]
		PAID,
		[PgName("cancelled")]
		CANCELLED,
		[PgName("expired")]
		EXPIRED
	}
}
