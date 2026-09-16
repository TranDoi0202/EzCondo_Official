using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum InvoiceTypeEnum
	{
		[PgName("regular")]
		REGULAR,
		[PgName("abnormal")]
		ABNORMAL
	}
}
