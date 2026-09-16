using System.IO;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace EzCondo.BackendServer.Datas

{
	public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
	{
		public ApplicationDbContext CreateDbContext(string[] args)
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json", optional: true)
				.AddJsonFile("appsettings.Development.json", optional: true)
				.Build();

			var connectionString = configuration.GetConnectionString("DefaultConnection")
				?? "Host=localhost; Database=EzCondoDB_official;Username=postgres";

			var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
			dataSourceBuilder.EnableDynamicJson();

			dataSourceBuilder.MapEnum<ApartmentStatusEnum>("apartment_status_enum");
			dataSourceBuilder.MapEnum<AuthEventTypeEnum>("auth_event_type_enum");
			dataSourceBuilder.MapEnum<BillingCycleEnum>("billing_cycle_enum");
			dataSourceBuilder.MapEnum<ComplaintStatusEnum>("complaint_status_enum");
			dataSourceBuilder.MapEnum<ContractStatusEnum>("contract_status_enum");
			dataSourceBuilder.MapEnum<ImportBatchStatusEnum>("import_batch_status_enum");
			dataSourceBuilder.MapEnum<InvitationStatusEnum>("invitation_status_enum");
			dataSourceBuilder.MapEnum<InvoiceBatchStatusEnum>("invoice_batch_status_enum");
			dataSourceBuilder.MapEnum<InvoiceStatusEnum>("invoice_status_enum");
			dataSourceBuilder.MapEnum<InvoiceTypeEnum>("invoice_type_enum");
			dataSourceBuilder.MapEnum<IssueSeverityEnum>("issue_severity_enum");
			dataSourceBuilder.MapEnum<IssueStatusEnum>("issue_status_enum");
			dataSourceBuilder.MapEnum<LicenseStatusEnum>("license_status_enum");
			dataSourceBuilder.MapEnum<MembershipStatusEnum>("membership_status_enum");
			dataSourceBuilder.MapEnum<OtpPurposeEnum>("otp_purpose_enum");
			dataSourceBuilder.MapEnum<PaymentTransactionStatusEnum>("payment_transaction_status_enum");
			dataSourceBuilder.MapEnum<PayoutBatchStatusEnum>("payout_batch_status_enum");
			dataSourceBuilder.MapEnum<PayoutItemStatusEnum>("payout_item_status_enum");
			dataSourceBuilder.MapEnum<PayrollStatusEnum>("payroll_status_enum");
			dataSourceBuilder.MapEnum<ReportPeriodEnum>("report_period_enum");
			dataSourceBuilder.MapEnum<ReportStatusEnum>("report_status_enum");
			dataSourceBuilder.MapEnum<ReportTypeEnum>("report_type_enum");
			dataSourceBuilder.MapEnum<ResidentTypeEnum>("resident_type_enum");
			dataSourceBuilder.MapEnum<RoleNameEnum>("role_name_enum");
			dataSourceBuilder.MapEnum<RoleScopeEnum>("role_scope_enum");
			dataSourceBuilder.MapEnum<TenantStatusEnum>("tenant_status_enum");
			dataSourceBuilder.MapEnum<TicketCategoryEnum>("ticket_category_enum");
			dataSourceBuilder.MapEnum<TicketStatusEnum>("ticket_status_enum");
			dataSourceBuilder.MapEnum<TransactionFlowEnum>("transaction_flow_enum");
			dataSourceBuilder.MapEnum<UserStatusEnum>("user_status_enum");

			var dataSource = dataSourceBuilder.Build();

			var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
			builder.UseNpgsql(dataSource);

			return new ApplicationDbContext(builder.Options);
		}
	}
}
