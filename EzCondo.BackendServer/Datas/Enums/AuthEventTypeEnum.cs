using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum AuthEventTypeEnum
	{
		[PgName("login_succeeded")]
		LOGIN_SUCCEEDED,
		[PgName("login_failed")]
		LOGIN_FAILED,
		[PgName("otp_requested")]
		OTP_REQUESTED,
		[PgName("otp_verified")]
		OTP_VERIFIED,
		[PgName("otp_failed")]
		OTP_FAILED,
		[PgName("invitation_created")]
		INVITATION_CREATED,
		[PgName("invitation_accepted")]
		INVITATION_ACCEPTED,
		[PgName("logout")]
		LOGOUT,
		[PgName("refresh_token_revoked")]
		REFRESH_TOKEN_REVOKED
	}
}
