using System;
using EzCondo.BackendServer.Datas.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EzCondo.BackendServer.Datas.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseInfrastructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:apartment_status_enum", "available,occupied,maintenance")
                .Annotation("Npgsql:Enum:auth_event_type_enum", "login_succeeded,login_failed,otp_requested,otp_verified,otp_failed,invitation_created,invitation_accepted,logout,refresh_token_revoked")
                .Annotation("Npgsql:Enum:billing_cycle_enum", "monthly,yearly")
                .Annotation("Npgsql:Enum:complaint_status_enum", "pending,processing,resolved,rejected")
                .Annotation("Npgsql:Enum:contract_status_enum", "active,expired,terminated")
                .Annotation("Npgsql:Enum:import_batch_status_enum", "pending,processing,completed,partially_completed,failed")
                .Annotation("Npgsql:Enum:invitation_status_enum", "pending,accepted,expired,revoked")
                .Annotation("Npgsql:Enum:invoice_batch_status_enum", "draft,pending,approved,rejected")
                .Annotation("Npgsql:Enum:invoice_status_enum", "pending,partial,paid,cancelled")
                .Annotation("Npgsql:Enum:invoice_type_enum", "regular,abnormal")
                .Annotation("Npgsql:Enum:issue_severity_enum", "low,medium,high,critical")
                .Annotation("Npgsql:Enum:issue_status_enum", "open,in_progress,resolved")
                .Annotation("Npgsql:Enum:license_status_enum", "active,expired,cancelled,removed")
                .Annotation("Npgsql:Enum:membership_status_enum", "invited,active,suspended,revoked")
                .Annotation("Npgsql:Enum:otp_purpose_enum", "resident_activation,login,staff_activation")
                .Annotation("Npgsql:Enum:payment_transaction_status_enum", "pending,paid,cancelled,expired")
                .Annotation("Npgsql:Enum:payout_batch_status_enum", "pending,processing,completed,failed")
                .Annotation("Npgsql:Enum:payout_item_status_enum", "pending,success,failed,cancelled")
                .Annotation("Npgsql:Enum:payroll_status_enum", "draft,pending_approval,processing,completed")
                .Annotation("Npgsql:Enum:report_period_enum", "daily,weekly,monthly,quarterly,yearly")
                .Annotation("Npgsql:Enum:report_status_enum", "submitted,reviewed,rejected")
                .Annotation("Npgsql:Enum:report_type_enum", "profit,demo_graphic,infrastructure,employee")
                .Annotation("Npgsql:Enum:resident_type_enum", "owner,tenant,family_member")
                .Annotation("Npgsql:Enum:role_name_enum", "admin,apartment_owner,staff,resident")
                .Annotation("Npgsql:Enum:role_scope_enum", "platform,tenant")
                .Annotation("Npgsql:Enum:tenant_status_enum", "active,suspended,trial,cancelled,removed")
                .Annotation("Npgsql:Enum:ticket_category_enum", "technical_error,billing_issue,feature_request,other")
                .Annotation("Npgsql:Enum:ticket_status_enum", "open,in_progress,closed")
                .Annotation("Npgsql:Enum:transaction_flow_enum", "inbound,outbound")
                .Annotation("Npgsql:Enum:user_status_enum", "active,locked,removed");

            migrationBuilder.CreateTable(
                name: "Commands",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commands", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Functions",
                columns: table => new
                {
                    id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    parent_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    url = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Functions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Functions_Functions_parent_id",
                        column: x => x.parent_id,
                        principalTable: "Functions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "License_Packages",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    package_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    billing_cycle = table.Column<BillingCycleEnum>(type: "billing_cycle_enum", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    features = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License_Packages", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    role_name = table.Column<RoleNameEnum>(type: "role_name_enum", nullable: false),
                    role_scope = table.Column<RoleScopeEnum>(type: "role_scope_enum", nullable: false),
                    hierarchy_level = table.Column<int>(type: "integer", nullable: false),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    code = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tenant_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    contact_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    contact_phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    contact_email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<TenantStatusEnum>(type: "tenant_status_enum", nullable: false, defaultValue: TenantStatusEnum.ACTIVE),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    normalized_phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    full_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    avatar_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    status = table.Column<UserStatusEnum>(type: "user_status_enum", nullable: false, defaultValue: UserStatusEnum.ACTIVE),
                    email_verified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    phone_verified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.CheckConstraint("CK_Users_EmailOrPhone", "\"normalized_email\" IS NOT NULL OR \"normalized_phone_number\" IS NOT NULL");
                });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    scope = table.Column<RoleScopeEnum>(type: "role_scope_enum", nullable: false),
                    description = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    function_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    command_id = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Permissions_Commands_command_id",
                        column: x => x.command_id,
                        principalTable: "Commands",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permissions_Functions_function_id",
                        column: x => x.function_id,
                        principalTable: "Functions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.id);
                    table.UniqueConstraint("AK_Departments_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "FK_Departments_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Legal_Documents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    doc_category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    file_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    issue_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legal_Documents", x => x.id);
                    table.ForeignKey(
                        name: "FK_Legal_Documents_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qr_Configs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    secret_key = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    refresh_interval = table.Column<int>(type: "integer", nullable: false, defaultValue: 30),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qr_Configs", x => x.id);
                    table.ForeignKey(
                        name: "FK_Qr_Configs_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rules",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rules", x => x.id);
                    table.ForeignKey(
                        name: "FK_Rules_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Service_Configs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    service_code = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    service_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    unit = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    vat_rate = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false, defaultValue: 0m),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service_Configs", x => x.id);
                    table.UniqueConstraint("AK_ServiceConfigs_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "FK_Service_Configs_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "System_Issues",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    severity = table.Column<IssueSeverityEnum>(type: "issue_severity_enum", nullable: false),
                    status = table.Column<IssueStatusEnum>(type: "issue_status_enum", nullable: false, defaultValue: IssueStatusEnum.OPEN),
                    reported_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    resolved_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_System_Issues", x => x.id);
                    table.ForeignKey(
                        name: "FK_System_Issues_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenant_Licenses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    contract_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<LicenseStatusEnum>(type: "license_status_enum", nullable: false, defaultValue: LicenseStatusEnum.ACTIVE),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    license_package_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant_Licenses", x => x.id);
                    table.UniqueConstraint("AK_TenantLicenses_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "FK_Tenant_Licenses_License_Packages_license_package_id",
                        column: x => x.license_package_id,
                        principalTable: "License_Packages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tenant_Licenses_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Platform_User_Roles",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Platform_User_Roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_Platform_User_Roles_Roles_role_id",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Platform_User_Roles_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Role_Permission",
                columns: table => new
                {
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    permission_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role_Permission", x => new { x.role_id, x.permission_id });
                    table.ForeignKey(
                        name: "FK_Role_Permission_Permissions_permission_id",
                        column: x => x.permission_id,
                        principalTable: "Permissions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Role_Permission_Roles_role_id",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Job_Positions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    is_deleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    deleted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    department_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Job_Positions", x => x.id);
                    table.UniqueConstraint("AK_JobPositions_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "FK_Job_Positions_Departments_department_id_tenant_id",
                        columns: x => new { x.department_id, x.tenant_id },
                        principalTable: "Departments",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenant_Memberships",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    status = table.Column<MembershipStatusEnum>(type: "membership_status_enum", nullable: false, defaultValue: MembershipStatusEnum.INVITED),
                    invited_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    activated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    revoked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    job_position_id = table.Column<Guid>(type: "uuid", nullable: true),
                    invited_by_membership_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenant_Memberships", x => x.id);
                    table.UniqueConstraint("AK_TenantMemberships_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.UniqueConstraint("AK_TenantMemberships_Id_TenantId_UserId", x => new { x.id, x.tenant_id, x.user_id });
                    table.CheckConstraint("CK_TenantMemberships_Lifecycle", "(\"status\" = 'invited' AND \"invited_at\" IS NOT NULL AND \"activated_at\" IS NULL AND \"revoked_at\" IS NULL) OR (\"status\" = 'active' AND \"activated_at\" IS NOT NULL AND \"revoked_at\" IS NULL) OR (\"status\" = 'suspended' AND \"revoked_at\" IS NULL) OR (\"status\" = 'revoked' AND \"revoked_at\" IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_Tenant_Memberships_Job_Positions_job_position_id_tenant_id",
                        columns: x => new { x.job_position_id, x.tenant_id },
                        principalTable: "Job_Positions",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tenant_Memberships_Roles_role_id",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tenant_Memberships_Tenant_Memberships_invited_by_membership~",
                        columns: x => new { x.invited_by_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tenant_Memberships_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tenant_Memberships_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AI_Work_Flow",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    is_approved = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    hitl_approved_by_membership_id = table.Column<Guid>(type: "uuid", nullable: true),
                    task_planner_data = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AI_Work_Flow", x => x.id);
                    table.ForeignKey(
                        name: "FK_AI_Work_Flow_Tenant_Memberships_hitl_approved_by_membership~",
                        columns: x => new { x.hitl_approved_by_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AI_Work_Flow_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Events",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    event_type = table.Column<AuthEventTypeEnum>(type: "auth_event_type_enum", nullable: false),
                    is_successful = table.Column<bool>(type: "boolean", nullable: false),
                    failure_reason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ip_address = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    user_agent = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    occurred_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_membership_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Events", x => x.id);
                    table.CheckConstraint("CK_AuthEvents_MembershipContext", "\"tenant_membership_id\" IS NULL OR (\"tenant_id\" IS NOT NULL AND \"user_id\" IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_Auth_Events_Tenant_Memberships_tenant_membership_id_tenant_~",
                        columns: x => new { x.tenant_membership_id, x.tenant_id, x.user_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id", "user_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auth_Events_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auth_Events_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Auth_Sessions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    refresh_token_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    device_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    ip_address = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    user_agent = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    revoked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_membership_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Auth_Sessions", x => x.id);
                    table.CheckConstraint("CK_AuthSessions_Expiry", "\"expires_at\" > \"created_at\"");
                    table.CheckConstraint("CK_AuthSessions_TenantContext", "(\"tenant_membership_id\" IS NULL AND \"tenant_id\" IS NULL) OR (\"tenant_membership_id\" IS NOT NULL AND \"tenant_id\" IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_Auth_Sessions_Tenant_Memberships_tenant_membership_id_tenan~",
                        columns: x => new { x.tenant_membership_id, x.tenant_id, x.user_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id", "user_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Auth_Sessions_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    content = table.Column<string>(type: "text", nullable: false),
                    AI_routed_to = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AI_sentiment = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    status = table.Column<ComplaintStatusEnum>(type: "complaint_status_enum", nullable: false, defaultValue: ComplaintStatusEnum.PENDING),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    resident_membership_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.id);
                    table.ForeignKey(
                        name: "FK_Complaints_Tenant_Memberships_resident_membership_id_tenant~",
                        columns: x => new { x.resident_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Complaints_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoice_Batches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    billing_month = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    approved_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<InvoiceBatchStatusEnum>(type: "invoice_batch_status_enum", nullable: false, defaultValue: InvoiceBatchStatusEnum.DRAFT),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    prepared_by_membership_id = table.Column<Guid>(type: "uuid", nullable: false),
                    approved_by_membership_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice_Batches", x => x.id);
                    table.UniqueConstraint("AK_InvoiceBatches_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "FK_Invoice_Batches_Tenant_Memberships_approved_by_membership_i~",
                        columns: x => new { x.approved_by_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Batches_Tenant_Memberships_prepared_by_membership_i~",
                        columns: x => new { x.prepared_by_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoice_Batches_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payout_Batches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    payout_month = table.Column<int>(type: "integer", nullable: false),
                    payout_year = table.Column<int>(type: "integer", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    status = table.Column<PayoutBatchStatusEnum>(type: "payout_batch_status_enum", nullable: false, defaultValue: PayoutBatchStatusEnum.PENDING),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    reference_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by_membership_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payout_Batches", x => x.id);
                    table.UniqueConstraint("AK_PayoutBatches_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "FK_Payout_Batches_Tenant_Memberships_created_by_membership_id_~",
                        columns: x => new { x.created_by_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payout_Batches_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payrolls",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    date_month = table.Column<int>(type: "integer", nullable: false),
                    date_year = table.Column<int>(type: "integer", nullable: false),
                    total_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    status = table.Column<PayrollStatusEnum>(type: "payroll_status_enum", nullable: false, defaultValue: PayrollStatusEnum.DRAFT),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by_membership_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payrolls", x => x.id);
                    table.UniqueConstraint("AK_Payrolls_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.CheckConstraint("CK_Payrolls_DateMonth_Range", "\"date_month\" >= 1 AND \"date_month\" <= 12");
                    table.CheckConstraint("CK_Payrolls_TotalAmount_NonNegative", "\"total_amount\" >= 0");
                    table.ForeignKey(
                        name: "FK_Payrolls_Tenant_Memberships_created_by_membership_id_tenant~",
                        columns: x => new { x.created_by_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payrolls_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Periodic_Reports",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    report_period = table.Column<ReportPeriodEnum>(type: "report_period_enum", nullable: false),
                    report_type = table.Column<ReportTypeEnum>(type: "report_type_enum", nullable: false),
                    attached_file_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    status = table.Column<ReportStatusEnum>(type: "report_status_enum", nullable: false, defaultValue: ReportStatusEnum.SUBMITTED),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    reviewed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    prepared_by_membership_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodic_Reports", x => x.id);
                    table.ForeignKey(
                        name: "FK_Periodic_Reports_Tenant_Memberships_prepared_by_membership_~",
                        columns: x => new { x.prepared_by_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Periodic_Reports_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Resident_Import_Batches",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    original_file_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    file_url = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    file_hash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    status = table.Column<ImportBatchStatusEnum>(type: "import_batch_status_enum", nullable: false, defaultValue: ImportBatchStatusEnum.PENDING),
                    total_rows = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    successful_rows = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    failed_rows = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    uploaded_by_membership_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resident_Import_Batches", x => x.id);
                    table.UniqueConstraint("AK_ResidentImportBatches_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.CheckConstraint("CK_ResidentImportBatches_Counts", "\"total_rows\" >= 0 AND \"successful_rows\" >= 0 AND \"failed_rows\" >= 0 AND \"successful_rows\" + \"failed_rows\" <= \"total_rows\"");
                    table.ForeignKey(
                        name: "FK_Resident_Import_Batches_Tenant_Memberships_uploaded_by_memb~",
                        columns: x => new { x.uploaded_by_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resident_Import_Batches_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Staff_Invitations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    full_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    normalized_phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    token_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    status = table.Column<InvitationStatusEnum>(type: "invitation_status_enum", nullable: false, defaultValue: InvitationStatusEnum.PENDING),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    accepted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    revoked_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    role_id = table.Column<Guid>(type: "uuid", nullable: false),
                    job_position_id = table.Column<Guid>(type: "uuid", nullable: true),
                    invited_by_membership_id = table.Column<Guid>(type: "uuid", nullable: true),
                    invited_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    accepted_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    accepted_membership_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff_Invitations", x => x.id);
                    table.UniqueConstraint("AK_StaffInvitations_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.CheckConstraint("CK_StaffInvitations_EmailOrPhone", "\"normalized_email\" IS NOT NULL OR \"normalized_phone_number\" IS NOT NULL");
                    table.CheckConstraint("CK_StaffInvitations_Expiry", "\"expires_at\" > \"created_at\"");
                    table.CheckConstraint("CK_StaffInvitations_Lifecycle", "(\"status\" = 'pending' AND \"accepted_at\" IS NULL AND \"revoked_at\" IS NULL AND \"accepted_membership_id\" IS NULL) OR (\"status\" = 'accepted' AND \"accepted_at\" IS NOT NULL AND \"accepted_by_user_id\" IS NOT NULL AND \"accepted_membership_id\" IS NOT NULL AND \"revoked_at\" IS NULL) OR (\"status\" = 'expired' AND \"accepted_at\" IS NULL AND \"accepted_membership_id\" IS NULL) OR (\"status\" = 'revoked' AND \"revoked_at\" IS NOT NULL AND \"accepted_at\" IS NULL AND \"accepted_membership_id\" IS NULL)");
                    table.ForeignKey(
                        name: "FK_Staff_Invitations_Job_Positions_job_position_id_tenant_id",
                        columns: x => new { x.job_position_id, x.tenant_id },
                        principalTable: "Job_Positions",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_Invitations_Roles_role_id",
                        column: x => x.role_id,
                        principalTable: "Roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_Invitations_Tenant_Memberships_accepted_membership_id~",
                        columns: x => new { x.accepted_membership_id, x.tenant_id, x.accepted_by_user_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id", "user_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_Invitations_Tenant_Memberships_invited_by_membership_~",
                        columns: x => new { x.invited_by_membership_id, x.tenant_id, x.invited_by_user_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id", "user_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_Invitations_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_Invitations_Users_accepted_by_user_id",
                        column: x => x.accepted_by_user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Staff_Invitations_Users_invited_by_user_id",
                        column: x => x.invited_by_user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Support_Tickets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    description = table.Column<string>(type: "text", nullable: false),
                    issue_category = table.Column<TicketCategoryEnum>(type: "ticket_category_enum", nullable: false),
                    status = table.Column<TicketStatusEnum>(type: "ticket_status_enum", nullable: false, defaultValue: TicketStatusEnum.OPEN),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    resolved_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_by_membership_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Support_Tickets", x => x.id);
                    table.ForeignKey(
                        name: "FK_Support_Tickets_Tenant_Memberships_created_by_membership_id~",
                        columns: x => new { x.created_by_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Support_Tickets_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Time_Keepings",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    check_in = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    check_out = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    is_AI_verified = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_membership_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Time_Keepings", x => x.id);
                    table.ForeignKey(
                        name: "FK_Time_Keepings_Tenant_Memberships_tenant_membership_id_tenan~",
                        columns: x => new { x.tenant_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Time_Keepings_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payroll_Details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    base_salary = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    allowances = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    deduction = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    net_pay = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    bank_bin = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    bank_account_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payroll_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_membership_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payroll_Details", x => x.id);
                    table.UniqueConstraint("AK_PayrollDetails_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "FK_Payroll_Details_Payrolls_payroll_id_tenant_id",
                        columns: x => new { x.payroll_id, x.tenant_id },
                        principalTable: "Payrolls",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payroll_Details_Tenant_Memberships_tenant_membership_id_ten~",
                        columns: x => new { x.tenant_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Apartments",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    room_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    floor = table.Column<int>(type: "integer", nullable: false),
                    area = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    status = table.Column<ApartmentStatusEnum>(type: "apartment_status_enum", nullable: false, defaultValue: ApartmentStatusEnum.AVAILABLE),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    resident_import_batch_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apartments", x => x.id);
                    table.UniqueConstraint("AK_Apartments_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.ForeignKey(
                        name: "FK_Apartments_Resident_Import_Batches_resident_import_batch_id~",
                        columns: x => new { x.resident_import_batch_id, x.tenant_id },
                        principalTable: "Resident_Import_Batches",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Apartments_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payout_Items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    to_bin = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    to_account_number = table.Column<string>(type: "text", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    reference_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    status = table.Column<PayoutItemStatusEnum>(type: "payout_item_status_enum", nullable: false, defaultValue: PayoutItemStatusEnum.PENDING),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_id = table.Column<Guid>(type: "uuid", nullable: false),
                    payroll_detail_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payout_Items", x => x.id);
                    table.ForeignKey(
                        name: "FK_Payout_Items_Payout_Batches_batch_id_tenant_id",
                        columns: x => new { x.batch_id, x.tenant_id },
                        principalTable: "Payout_Batches",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Payout_Items_Payroll_Details_payroll_detail_id_tenant_id",
                        columns: x => new { x.payroll_detail_id, x.tenant_id },
                        principalTable: "Payroll_Details",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    contract_number = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    start_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    month_rent = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    deposit_amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    status = table.Column<ContractStatusEnum>(type: "contract_status_enum", nullable: false, defaultValue: ContractStatusEnum.ACTIVE),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    apartment_id = table.Column<Guid>(type: "uuid", nullable: false),
                    resident_import_batch_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.id);
                    table.UniqueConstraint("AK_Contracts_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.CheckConstraint("CK_Contracts_DateRange", "\"end_date\" > \"start_date\"");
                    table.ForeignKey(
                        name: "FK_Contracts_Apartments_apartment_id_tenant_id",
                        columns: x => new { x.apartment_id, x.tenant_id },
                        principalTable: "Apartments",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contracts_Resident_Import_Batches_resident_import_batch_id_~",
                        columns: x => new { x.resident_import_batch_id, x.tenant_id },
                        principalTable: "Resident_Import_Batches",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    title = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    type = table.Column<InvoiceTypeEnum>(type: "invoice_type_enum", nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    amount_paid = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    amount_remaining = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    due_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    status = table.Column<InvoiceStatusEnum>(type: "invoice_status_enum", nullable: false, defaultValue: InvoiceStatusEnum.PENDING),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    batch_id = table.Column<Guid>(type: "uuid", nullable: true),
                    apartment_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.id);
                    table.UniqueConstraint("AK_Invoices_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.CheckConstraint("CK_Invoice_Amount", "\"amount\" > 0");
                    table.ForeignKey(
                        name: "FK_Invoices_Apartments_apartment_id_tenant_id",
                        columns: x => new { x.apartment_id, x.tenant_id },
                        principalTable: "Apartments",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Invoice_Batches_batch_id_tenant_id",
                        columns: x => new { x.batch_id, x.tenant_id },
                        principalTable: "Invoice_Batches",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Invoices_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contract_Residents",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    full_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    normalized_phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    normalized_email = table.Column<string>(type: "character varying(254)", maxLength: 254, nullable: true),
                    resident_type = table.Column<ResidentTypeEnum>(type: "resident_type_enum", nullable: false),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    activated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    contract_id = table.Column<Guid>(type: "uuid", nullable: false),
                    tenant_membership_id = table.Column<Guid>(type: "uuid", nullable: true),
                    resident_import_batch_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contract_Residents", x => x.id);
                    table.UniqueConstraint("AK_ContractResidents_Id_TenantId", x => new { x.id, x.tenant_id });
                    table.CheckConstraint("CK_ContractResidents_Activation", "(\"tenant_membership_id\" IS NULL AND \"activated_at\" IS NULL) OR (\"tenant_membership_id\" IS NOT NULL AND \"activated_at\" IS NOT NULL)");
                    table.ForeignKey(
                        name: "FK_Contract_Residents_Contracts_contract_id_tenant_id",
                        columns: x => new { x.contract_id, x.tenant_id },
                        principalTable: "Contracts",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contract_Residents_Resident_Import_Batches_resident_import_~",
                        columns: x => new { x.resident_import_batch_id, x.tenant_id },
                        principalTable: "Resident_Import_Batches",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Residents_Tenant_Memberships_tenant_membership_id_~",
                        columns: x => new { x.tenant_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contract_Residents_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Invoice_Items",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    item_name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    quantity = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    unit_price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    invoice_id = table.Column<Guid>(type: "uuid", nullable: false),
                    service_config_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice_Items", x => x.id);
                    table.CheckConstraint("CK_InvoiceItem_Quantity", "\"quantity\" > 0");
                    table.ForeignKey(
                        name: "FK_Invoice_Items_Invoices_invoice_id_tenant_id",
                        columns: x => new { x.invoice_id, x.tenant_id },
                        principalTable: "Invoices",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice_Items_Service_Configs_service_config_id_tenant_id",
                        columns: x => new { x.service_config_id, x.tenant_id },
                        principalTable: "Service_Configs",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Payment_Transactions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    order_code = table.Column<long>(type: "bigint", nullable: false),
                    transaction_flow = table.Column<TransactionFlowEnum>(type: "transaction_flow_enum", nullable: false),
                    payment_link_id = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    amount = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    purpose = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    checkout_url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    payos_reference = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    status = table.Column<PaymentTransactionStatusEnum>(type: "payment_transaction_status_enum", nullable: false, defaultValue: PaymentTransactionStatusEnum.PENDING),
                    paid_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: false),
                    invoice_id = table.Column<Guid>(type: "uuid", nullable: true),
                    tenant_license_id = table.Column<Guid>(type: "uuid", nullable: true),
                    initiated_by_membership_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment_Transactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Payment_Transactions_Invoices_invoice_id_tenant_id",
                        columns: x => new { x.invoice_id, x.tenant_id },
                        principalTable: "Invoices",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Transactions_Tenant_Licenses_tenant_license_id_tena~",
                        columns: x => new { x.tenant_license_id, x.tenant_id },
                        principalTable: "Tenant_Licenses",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Transactions_Tenant_Memberships_initiated_by_member~",
                        columns: x => new { x.initiated_by_membership_id, x.tenant_id },
                        principalTable: "Tenant_Memberships",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Payment_Transactions_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Otp_Challenges",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    normalized_phone_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    code_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    purpose = table.Column<OtpPurposeEnum>(type: "otp_purpose_enum", nullable: false),
                    attempt_count = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    max_attempts = table.Column<int>(type: "integer", nullable: false, defaultValue: 5),
                    expires_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    consumed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_sent_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    requested_ip = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    tenant_id = table.Column<Guid>(type: "uuid", nullable: true),
                    contract_resident_id = table.Column<Guid>(type: "uuid", nullable: true),
                    staff_invitation_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otp_Challenges", x => x.id);
                    table.CheckConstraint("CK_OtpChallenges_AttemptRange", "\"attempt_count\" >= 0 AND \"max_attempts\" > 0 AND \"attempt_count\" <= \"max_attempts\"");
                    table.CheckConstraint("CK_OtpChallenges_Expiry", "\"expires_at\" > \"created_at\"");
                    table.CheckConstraint("CK_OtpChallenges_Target", "(\"purpose\" = 'resident_activation' AND \"contract_resident_id\" IS NOT NULL AND \"staff_invitation_id\" IS NULL AND \"tenant_id\" IS NOT NULL) OR (\"purpose\" = 'staff_activation' AND \"staff_invitation_id\" IS NOT NULL AND \"contract_resident_id\" IS NULL AND \"tenant_id\" IS NOT NULL) OR (\"purpose\" = 'login' AND \"contract_resident_id\" IS NULL AND \"staff_invitation_id\" IS NULL)");
                    table.ForeignKey(
                        name: "FK_Otp_Challenges_Contract_Residents_contract_resident_id_tena~",
                        columns: x => new { x.contract_resident_id, x.tenant_id },
                        principalTable: "Contract_Residents",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Otp_Challenges_Staff_Invitations_staff_invitation_id_tenant~",
                        columns: x => new { x.staff_invitation_id, x.tenant_id },
                        principalTable: "Staff_Invitations",
                        principalColumns: new[] { "id", "tenant_id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Otp_Challenges_Tenants_tenant_id",
                        column: x => x.tenant_id,
                        principalTable: "Tenants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AI_Work_Flow_hitl_approved_by_membership_id_tenant_id",
                table: "AI_Work_Flow",
                columns: new[] { "hitl_approved_by_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_AI_Work_Flow_tenant_id",
                table: "AI_Work_Flow",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Apartment_TenantId_RoomName",
                table: "Apartments",
                columns: new[] { "tenant_id", "room_name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Apartments_resident_import_batch_id_tenant_id",
                table: "Apartments",
                columns: new[] { "resident_import_batch_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Auth_Events_occurred_at",
                table: "Auth_Events",
                column: "occurred_at",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_Auth_Events_tenant_id_occurred_at",
                table: "Auth_Events",
                columns: new[] { "tenant_id", "occurred_at" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_Auth_Events_tenant_membership_id_tenant_id_user_id",
                table: "Auth_Events",
                columns: new[] { "tenant_membership_id", "tenant_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Auth_Events_user_id_occurred_at",
                table: "Auth_Events",
                columns: new[] { "user_id", "occurred_at" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_Auth_Sessions_expires_at",
                table: "Auth_Sessions",
                column: "expires_at");

            migrationBuilder.CreateIndex(
                name: "IX_Auth_Sessions_refresh_token_hash",
                table: "Auth_Sessions",
                column: "refresh_token_hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auth_Sessions_tenant_membership_id_tenant_id_user_id",
                table: "Auth_Sessions",
                columns: new[] { "tenant_membership_id", "tenant_id", "user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Auth_Sessions_user_id",
                table: "Auth_Sessions",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_resident_membership_id_tenant_id",
                table: "Complaints",
                columns: new[] { "resident_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_tenant_id",
                table: "Complaints",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Contract_Residents_contract_id_normalized_phone_number",
                table: "Contract_Residents",
                columns: new[] { "contract_id", "normalized_phone_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contract_Residents_contract_id_tenant_id",
                table: "Contract_Residents",
                columns: new[] { "contract_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_Residents_resident_import_batch_id_tenant_id",
                table: "Contract_Residents",
                columns: new[] { "resident_import_batch_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_Residents_tenant_id_normalized_phone_number",
                table: "Contract_Residents",
                columns: new[] { "tenant_id", "normalized_phone_number" });

            migrationBuilder.CreateIndex(
                name: "IX_Contract_Residents_tenant_membership_id_tenant_id",
                table: "Contract_Residents",
                columns: new[] { "tenant_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_apartment_id_tenant_id",
                table: "Contracts",
                columns: new[] { "apartment_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_resident_import_batch_id_tenant_id",
                table: "Contracts",
                columns: new[] { "resident_import_batch_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_TenantId_ContractNumber",
                table: "Contracts",
                columns: new[] { "tenant_id", "contract_number" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_tenant_id",
                table: "Departments",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Functions_parent_id",
                table: "Functions",
                column: "parent_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Batches_approved_by_membership_id_tenant_id",
                table: "Invoice_Batches",
                columns: new[] { "approved_by_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Batches_prepared_by_membership_id_tenant_id",
                table: "Invoice_Batches",
                columns: new[] { "prepared_by_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Batches_tenant_id",
                table: "Invoice_Batches",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Items_invoice_id_tenant_id",
                table: "Invoice_Items",
                columns: new[] { "invoice_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Items_service_config_id_tenant_id",
                table: "Invoice_Items",
                columns: new[] { "service_config_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_apartment_id_tenant_id",
                table: "Invoices",
                columns: new[] { "apartment_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_batch_id_tenant_id",
                table: "Invoices",
                columns: new[] { "batch_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_tenant_id",
                table: "Invoices",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Job_Positions_department_id_tenant_id",
                table: "Job_Positions",
                columns: new[] { "department_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Legal_Documents_tenant_id",
                table: "Legal_Documents",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_License_Packages_package_name",
                table: "License_Packages",
                column: "package_name",
                unique: true,
                filter: "\"is_deleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_Otp_Challenges_contract_resident_id_tenant_id",
                table: "Otp_Challenges",
                columns: new[] { "contract_resident_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Otp_Challenges_expires_at",
                table: "Otp_Challenges",
                column: "expires_at");

            migrationBuilder.CreateIndex(
                name: "IX_Otp_Challenges_normalized_phone_number_created_at",
                table: "Otp_Challenges",
                columns: new[] { "normalized_phone_number", "created_at" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_Otp_Challenges_staff_invitation_id_tenant_id",
                table: "Otp_Challenges",
                columns: new[] { "staff_invitation_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Otp_Challenges_tenant_id",
                table: "Otp_Challenges",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Transactions_initiated_by_membership_id_tenant_id",
                table: "Payment_Transactions",
                columns: new[] { "initiated_by_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Transactions_invoice_id_tenant_id",
                table: "Payment_Transactions",
                columns: new[] { "invoice_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Transactions_order_code",
                table: "Payment_Transactions",
                column: "order_code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Transactions_tenant_id",
                table: "Payment_Transactions",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_Transactions_tenant_license_id_tenant_id",
                table: "Payment_Transactions",
                columns: new[] { "tenant_license_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Payout_Batches_created_by_membership_id_tenant_id",
                table: "Payout_Batches",
                columns: new[] { "created_by_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Payout_Batches_reference_id",
                table: "Payout_Batches",
                column: "reference_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payout_Batches_tenant_id",
                table: "Payout_Batches",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Payout_Items_batch_id_tenant_id",
                table: "Payout_Items",
                columns: new[] { "batch_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Payout_Items_payroll_detail_id_tenant_id",
                table: "Payout_Items",
                columns: new[] { "payroll_detail_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_Details_payroll_id_tenant_id",
                table: "Payroll_Details",
                columns: new[] { "payroll_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Payroll_Details_tenant_membership_id_tenant_id",
                table: "Payroll_Details",
                columns: new[] { "tenant_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_created_by_membership_id_tenant_id",
                table: "Payrolls",
                columns: new[] { "created_by_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Payrolls_tenant_id",
                table: "Payrolls",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Periodic_Reports_prepared_by_membership_id_tenant_id",
                table: "Periodic_Reports",
                columns: new[] { "prepared_by_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Periodic_Reports_tenant_id",
                table: "Periodic_Reports",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_command_id",
                table: "Permissions",
                column: "command_id");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_FunctionId_CommandId",
                table: "Permissions",
                columns: new[] { "function_id", "command_id" },
                unique: true,
                filter: "\"is_deleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_Platform_User_Roles_role_id",
                table: "Platform_User_Roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Qr_Configs_tenant_id",
                table: "Qr_Configs",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Resident_Import_Batches_tenant_id_file_hash",
                table: "Resident_Import_Batches",
                columns: new[] { "tenant_id", "file_hash" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Resident_Import_Batches_uploaded_by_membership_id_tenant_id",
                table: "Resident_Import_Batches",
                columns: new[] { "uploaded_by_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Role_Permission_permission_id",
                table: "Role_Permission",
                column: "permission_id");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_role_name_role_scope",
                table: "Roles",
                columns: new[] { "role_name", "role_scope" },
                unique: true,
                filter: "\"is_deleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_tenant_id",
                table: "Rules",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Service_Configs_tenant_id",
                table: "Service_Configs",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Invitations_accepted_by_user_id",
                table: "Staff_Invitations",
                column: "accepted_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Invitations_accepted_membership_id_tenant_id_accepted~",
                table: "Staff_Invitations",
                columns: new[] { "accepted_membership_id", "tenant_id", "accepted_by_user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Invitations_expires_at",
                table: "Staff_Invitations",
                column: "expires_at");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Invitations_invited_by_membership_id_tenant_id_invite~",
                table: "Staff_Invitations",
                columns: new[] { "invited_by_membership_id", "tenant_id", "invited_by_user_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Invitations_invited_by_user_id",
                table: "Staff_Invitations",
                column: "invited_by_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Invitations_job_position_id_tenant_id",
                table: "Staff_Invitations",
                columns: new[] { "job_position_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Invitations_role_id",
                table: "Staff_Invitations",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Invitations_tenant_id_normalized_email",
                table: "Staff_Invitations",
                columns: new[] { "tenant_id", "normalized_email" },
                unique: true,
                filter: "\"status\" = 'pending' AND \"normalized_email\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Invitations_tenant_id_normalized_phone_number",
                table: "Staff_Invitations",
                columns: new[] { "tenant_id", "normalized_phone_number" },
                unique: true,
                filter: "\"status\" = 'pending' AND \"normalized_phone_number\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Staff_Invitations_token_hash",
                table: "Staff_Invitations",
                column: "token_hash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Support_Tickets_created_by_membership_id_tenant_id",
                table: "Support_Tickets",
                columns: new[] { "created_by_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Support_Tickets_tenant_id",
                table: "Support_Tickets",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_System_Issues_tenant_id",
                table: "System_Issues",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Licenses_contract_number",
                table: "Tenant_Licenses",
                column: "contract_number",
                unique: true,
                filter: "\"is_deleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Licenses_license_package_id",
                table: "Tenant_Licenses",
                column: "license_package_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Licenses_tenant_id",
                table: "Tenant_Licenses",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Memberships_invited_by_membership_id_tenant_id",
                table: "Tenant_Memberships",
                columns: new[] { "invited_by_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Memberships_job_position_id_tenant_id",
                table: "Tenant_Memberships",
                columns: new[] { "job_position_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Memberships_role_id",
                table: "Tenant_Memberships",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tenant_Memberships_user_id",
                table: "Tenant_Memberships",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_TenantMemberships_TenantId_UserId",
                table: "Tenant_Memberships",
                columns: new[] { "tenant_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_code",
                table: "Tenants",
                column: "code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_slug",
                table: "Tenants",
                column: "slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Time_Keepings_tenant_id",
                table: "Time_Keepings",
                column: "tenant_id");

            migrationBuilder.CreateIndex(
                name: "IX_Time_Keepings_tenant_membership_id_tenant_id",
                table: "Time_Keepings",
                columns: new[] { "tenant_membership_id", "tenant_id" });

            migrationBuilder.CreateIndex(
                name: "IX_Users_normalized_email",
                table: "Users",
                column: "normalized_email",
                unique: true,
                filter: "\"is_deleted\" = false AND \"normalized_email\" IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_normalized_phone_number",
                table: "Users",
                column: "normalized_phone_number",
                unique: true,
                filter: "\"is_deleted\" = false AND \"normalized_phone_number\" IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AI_Work_Flow");

            migrationBuilder.DropTable(
                name: "Auth_Events");

            migrationBuilder.DropTable(
                name: "Auth_Sessions");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "Invoice_Items");

            migrationBuilder.DropTable(
                name: "Legal_Documents");

            migrationBuilder.DropTable(
                name: "Otp_Challenges");

            migrationBuilder.DropTable(
                name: "Payment_Transactions");

            migrationBuilder.DropTable(
                name: "Payout_Items");

            migrationBuilder.DropTable(
                name: "Periodic_Reports");

            migrationBuilder.DropTable(
                name: "Platform_User_Roles");

            migrationBuilder.DropTable(
                name: "Qr_Configs");

            migrationBuilder.DropTable(
                name: "Role_Permission");

            migrationBuilder.DropTable(
                name: "Rules");

            migrationBuilder.DropTable(
                name: "Support_Tickets");

            migrationBuilder.DropTable(
                name: "System_Issues");

            migrationBuilder.DropTable(
                name: "Time_Keepings");

            migrationBuilder.DropTable(
                name: "Service_Configs");

            migrationBuilder.DropTable(
                name: "Contract_Residents");

            migrationBuilder.DropTable(
                name: "Staff_Invitations");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Tenant_Licenses");

            migrationBuilder.DropTable(
                name: "Payout_Batches");

            migrationBuilder.DropTable(
                name: "Payroll_Details");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "Invoice_Batches");

            migrationBuilder.DropTable(
                name: "License_Packages");

            migrationBuilder.DropTable(
                name: "Payrolls");

            migrationBuilder.DropTable(
                name: "Commands");

            migrationBuilder.DropTable(
                name: "Functions");

            migrationBuilder.DropTable(
                name: "Apartments");

            migrationBuilder.DropTable(
                name: "Resident_Import_Batches");

            migrationBuilder.DropTable(
                name: "Tenant_Memberships");

            migrationBuilder.DropTable(
                name: "Job_Positions");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
