using EzCondo.BackendServer.Datas;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration
	.GetConnectionString("DefaultConnection")
	?? throw new InvalidOperationException("The 'DefaultConnection' connection string is not found.");

// Configure PostgreSQL enums
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

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(dataSource));

// Add services to the container.
builder.Services.AddScoped<DbInitializer>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
	var initializer = scope.ServiceProvider.GetRequiredService<DbInitializer>();
	await initializer.SeedAsync();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
