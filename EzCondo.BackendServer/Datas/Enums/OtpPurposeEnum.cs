using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum OtpPurposeEnum
	{
		[PgName("resident_activation")]
		RESIDENT_ACTIVATION,
		[PgName("login")]
		LOGIN,
		[PgName("staff_activation")]
		STAFF_ACTIVATION
	}
}
