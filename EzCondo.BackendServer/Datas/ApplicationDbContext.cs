using System.Linq.Expressions;
using EzCondo.BackendServer.Datas.Entities;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore;

namespace EzCondo.BackendServer.Datas
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.HasPostgresEnum<ApartmentStatusEnum>(name: "apartment_status_enum");
			modelBuilder.HasPostgresEnum<AuthEventTypeEnum>(name: "auth_event_type_enum");
			modelBuilder.HasPostgresEnum<BillingCycleEnum>(name: "billing_cycle_enum");
			modelBuilder.HasPostgresEnum<ComplaintStatusEnum>(name: "complaint_status_enum");
			modelBuilder.HasPostgresEnum<ContractStatusEnum>(name: "contract_status_enum");
			modelBuilder.HasPostgresEnum<ImportBatchStatusEnum>(name: "import_batch_status_enum");
			modelBuilder.HasPostgresEnum<InvitationStatusEnum>(name: "invitation_status_enum");
			modelBuilder.HasPostgresEnum<InvoiceBatchStatusEnum>(name: "invoice_batch_status_enum");
			modelBuilder.HasPostgresEnum<InvoiceStatusEnum>(name: "invoice_status_enum");
			modelBuilder.HasPostgresEnum<InvoiceTypeEnum>(name: "invoice_type_enum");
			modelBuilder.HasPostgresEnum<IssueSeverityEnum>(name: "issue_severity_enum");
			modelBuilder.HasPostgresEnum<IssueStatusEnum>(name: "issue_status_enum");
			modelBuilder.HasPostgresEnum<LicenseStatusEnum>(name: "license_status_enum");
			modelBuilder.HasPostgresEnum<MembershipStatusEnum>(name: "membership_status_enum");
			modelBuilder.HasPostgresEnum<OtpPurposeEnum>(name: "otp_purpose_enum");
			modelBuilder.HasPostgresEnum<PaymentTransactionStatusEnum>(name: "payment_transaction_status_enum");
			modelBuilder.HasPostgresEnum<PayoutBatchStatusEnum>(name: "payout_batch_status_enum");
			modelBuilder.HasPostgresEnum<PayoutItemStatusEnum>(name: "payout_item_status_enum");
			modelBuilder.HasPostgresEnum<PayrollStatusEnum>(name: "payroll_status_enum");
			modelBuilder.HasPostgresEnum<ReportPeriodEnum>(name: "report_period_enum");
			modelBuilder.HasPostgresEnum<ReportStatusEnum>(name: "report_status_enum");
			modelBuilder.HasPostgresEnum<ReportTypeEnum>(name: "report_type_enum");
			modelBuilder.HasPostgresEnum<ResidentTypeEnum>(name: "resident_type_enum");
			modelBuilder.HasPostgresEnum<RoleNameEnum>(name: "role_name_enum");
			modelBuilder.HasPostgresEnum<RoleScopeEnum>(name: "role_scope_enum");
			modelBuilder.HasPostgresEnum<TenantStatusEnum>(name: "tenant_status_enum");
			modelBuilder.HasPostgresEnum<TicketCategoryEnum>(name: "ticket_category_enum");
			modelBuilder.HasPostgresEnum<TicketStatusEnum>(name: "ticket_status_enum");
			modelBuilder.HasPostgresEnum<TransactionFlowEnum>(name: "transaction_flow_enum");
			modelBuilder.HasPostgresEnum<UserStatusEnum>(name: "user_status_enum");

			modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
			ApplySoftDeleteQueryFilters(modelBuilder);
			ConfigureAuditProperties(modelBuilder);
		}

		public override int SaveChanges()
		{
			return SaveChanges(acceptAllChangesOnSuccess: true);
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			ApplySoftDeletes();
			ApplyAuditTimestamps();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			return SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);
		}

		public override Task<int> SaveChangesAsync(
			bool acceptAllChangesOnSuccess,
			CancellationToken cancellationToken = default)
		{
			ApplySoftDeletes();
			ApplyAuditTimestamps();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		private static void ApplySoftDeleteQueryFilters(ModelBuilder modelBuilder)
		{
			foreach (var entityType in modelBuilder.Model.GetEntityTypes()
				.Where(entityType => typeof(ISoftDeletableEntity).IsAssignableFrom(entityType.ClrType)))
			{
				var parameter = Expression.Parameter(entityType.ClrType, "entity");
				var isDeleted = Expression.Call(
					typeof(EF),
					nameof(EF.Property),
					new[] { typeof(bool) },
					parameter,
					Expression.Constant(nameof(ISoftDeletableEntity.IsDeleted)));

				modelBuilder.Entity(entityType.ClrType)
					.HasQueryFilter(Expression.Lambda(Expression.Not(isDeleted), parameter));
			}
		}

		private static void ConfigureAuditProperties(ModelBuilder modelBuilder)
		{
			foreach (var entityType in modelBuilder.Model.GetEntityTypes()
				.Where(entityType => typeof(IAuditableEntity).IsAssignableFrom(entityType.ClrType)))
			{
				var entity = modelBuilder.Entity(entityType.ClrType);
				entity.Property(nameof(IAuditableEntity.CreatedAt))
					.HasColumnName("created_at")
					.HasDefaultValueSql("now()")
					.IsRequired();
				entity.Property(nameof(IAuditableEntity.UpdatedAt))
					.HasColumnName("updated_at")
					.IsRequired(false);
			}
		}

		private void ApplySoftDeletes()
		{
			var utcNow = DateTime.UtcNow;
			foreach (var entry in ChangeTracker.Entries<ISoftDeletableEntity>()
				.Where(entry => entry.State == EntityState.Deleted))
			{
				entry.State = EntityState.Unchanged;
				entry.Entity.IsDeleted = true;
				entry.Entity.DeletedAt = utcNow;
				entry.Property(nameof(ISoftDeletableEntity.IsDeleted)).IsModified = true;
				entry.Property(nameof(ISoftDeletableEntity.DeletedAt)).IsModified = true;
			}
		}

		private void ApplyAuditTimestamps()
		{
			var utcNow = DateTime.UtcNow;
			foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
			{
				if (entry.State == EntityState.Added && entry.Entity.CreatedAt == default)
				{
					entry.Entity.CreatedAt = utcNow;
				}
				else if (entry.State == EntityState.Modified)
				{
					entry.Property(entity => entity.CreatedAt).IsModified = false;
					entry.Entity.UpdatedAt = utcNow;
				}
			}
		}

		public DbSet<AiWorkflow> AiWorkflows { get; set; }
		public DbSet<Apartment> Apartments { get; set; }
		public DbSet<AuthEvent> AuthEvents { get; set; }
		public DbSet<AuthSession> AuthSessions { get; set; }
		public DbSet<Command> Commands { get; set; }
		public DbSet<Complaint> Complaints { get; set; }
		public DbSet<Contract> Contracts { get; set; }
		public DbSet<ContractResident> ContractResidents { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Function> Functions { get; set; }
		public DbSet<Invoice> Invoices { get; set; }
		public DbSet<InvoiceBatch> InvoiceBatches { get; set; }
		public DbSet<InvoiceItem> InvoiceItems { get; set; }
		public DbSet<JobPosition> JobPositions { get; set; }
		public DbSet<LegalDocument> LegalDocuments { get; set; }
		public DbSet<LicensePackage> LicensePackages { get; set; }
		public DbSet<OtpChallenge> OtpChallenges { get; set; }
		public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
		public DbSet<PayoutBatch> PayoutBatches { get; set; }
		public DbSet<PayoutItem> PayoutItems { get; set; }
		public DbSet<Payroll> Payrolls { get; set; }
		public DbSet<PayrollDetail> PayrollDetails { get; set; }
		public DbSet<PeriodicReport> PeriodicReports { get; set; }
		public DbSet<Permission> Permissions { get; set; }
		public DbSet<PlatformUserRole> PlatformUserRoles { get; set; }
		public DbSet<QrConfig> QrConfigs { get; set; }
		public DbSet<ResidentImportBatch> ResidentImportBatches { get; set; }
		public DbSet<Role> Roles { get; set; }
		public DbSet<RolePermission> RolePermissions { get; set; }
		public DbSet<Rule> Rules { get; set; }
		public DbSet<ServiceConfig> ServiceConfigs { get; set; }
		public DbSet<StaffInvitation> StaffInvitations { get; set; }
		public DbSet<SupportTicket> SupportTickets { get; set; }
		public DbSet<SystemIssue> SystemIssues { get; set; }
		public DbSet<Tenant> Tenants { get; set; }
		public DbSet<TenantLicense> TenantLicenses { get; set; }
		public DbSet<TenantMembership> TenantMemberships { get; set; }
		public DbSet<TimeKeeping> TimeKeepings { get; set; }
		public DbSet<User> Users { get; set; }
	}
}
