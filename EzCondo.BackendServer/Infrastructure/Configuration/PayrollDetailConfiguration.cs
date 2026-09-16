using EzCondo.BackendServer.Datas.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EzCondo.BackendServer.Infrastructure.Configuration
{
	public class PayrollDetailConfiguration : IEntityTypeConfiguration<PayrollDetail>
	{
		public void Configure(EntityTypeBuilder<PayrollDetail> b)
		{
			b.ToTable("Payroll_Details");
			b.HasKey(x => x.Id);
			b.HasAlternateKey(x => new { x.Id, x.TenantId })
				.HasName("AK_PayrollDetails_Id_TenantId");

			b.Property(x => x.Id)
				.HasColumnName("id")
				.HasDefaultValueSql("gen_random_uuid()");

			b.Property(x => x.BaseSalary)
				.HasColumnName("base_salary")
				.HasPrecision(18, 2)
				.HasDefaultValue(0)
				.IsRequired();

			b.Property(x => x.Allowances)
				.HasColumnName("allowances")
				.HasPrecision(18, 2)
				.HasDefaultValue(0)
				.IsRequired();

			b.Property(x => x.Deductions)
				.HasColumnName("deduction")
				.HasPrecision(18, 2)
				.HasDefaultValue(0)
				.IsRequired();

			b.Property(x => x.NetPay)
				.HasColumnName("net_pay")
				.HasPrecision(18, 2)
				.IsRequired();

			b.Property(x => x.BankBin)
				.HasColumnName("bank_bin")
				.HasMaxLength(20)
				.IsRequired();

			b.Property(x => x.BankAccountNumber)
				.HasColumnName("bank_account_number")
				.HasMaxLength(50)
				.IsRequired();

			b.Property(x => x.TenantId)
				.HasColumnName("tenant_id")
				.IsRequired();

			b.Property(x => x.TenantMembershipId)
				.HasColumnName("tenant_membership_id")
				.IsRequired();
			b.HasOne(x => x.TenantMembership)
				.WithMany(user => user.PayrollDetails)
				.HasForeignKey(x => new { x.TenantMembershipId, x.TenantId })
				.HasPrincipalKey(m => new { m.Id, m.TenantId })
				.OnDelete(DeleteBehavior.Restrict);

			b.Property(x => x.PayrollId)
				.HasColumnName("payroll_id")
				.IsRequired();
			b.HasOne(x => x.Payroll)
				.WithMany(p => p.PayrollDetails)
				.HasForeignKey(d => new { d.PayrollId, d.TenantId })
				.HasPrincipalKey(p => new { p.Id, p.TenantId })
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();
		}
	}
}
