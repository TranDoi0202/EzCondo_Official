using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum TransactionFlowEnum
	{
		[PgName("inbound")]
		INBOUND,
		[PgName("outbound")]
		OUTBOUND
	}
}
