using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum TicketCategoryEnum
	{
		[PgName("technical_error")]
		TECHNICAL_ERROR,
		[PgName("billing_issue")]
		BILLING_ISSUE,
		[PgName("feature_request")]
		FEATURE_REQUEST,
		[PgName("other")]
		OTHER
	}
}
