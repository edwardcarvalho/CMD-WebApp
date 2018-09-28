using CMD.Model.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace CMD.Data.EntityContext
{
    public class EfContext : IdentityDbContext<Usuarios>
    {

        public EfContext() : base("CMD", throwIfV1Schema: false)
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public static EfContext Create()
        {
            return new EfContext();
        }

        public DbSet<Medidas> Medida { get; set; }
        public DbSet<Funcionario> Funcionario { get; set; }
        public DbSet<Filial> Filial { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
        public DbSet<Operacao> Operacao { get; set; }
        public DbSet<Motivos> Motivo { get; set; }
        public DbSet<Advertencias> Advertencia { get; set; }
        //public DbSet<Usuarios> Usuario { get; set; }
        public DbSet<StatusMedida> StatusMedida { get; set; }
        public DbSet<Perfil> Perfil { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<System.Data.Entity.ModelConfiguration.Conventions.PluralizingTableNameConvention>();

            modelBuilder.Entity<Medidas>()
            .HasOptional(acc => acc.Funcionario)
            .WithMany()
            .HasForeignKey(acc => acc.FuncionarioId);

            modelBuilder.Entity<Medidas>()
            .HasOptional(acc => acc.FuncSolicitante)
            .WithMany()
            .HasForeignKey(acc => acc.FuncSolicitanteId);

            modelBuilder.Entity<Medidas>()
            .HasOptional(acc => acc.FuncAprovador)
            .WithMany()
            .HasForeignKey(acc => acc.FuncAprovadorId);

            modelBuilder.Entity<Operacao>()
            .HasOptional(acc => acc.Gerente)
            .WithMany()
            .HasForeignKey(acc => acc.GerenteId);

            modelBuilder.Entity<Operacao>()
            .HasOptional(acc => acc.Supervisor)
            .WithMany()
            .HasForeignKey(acc => acc.SupervisorId);

            modelBuilder.Entity<Funcionario>()
            .HasOptional(acc => acc.Operacao)
            .WithMany()
            .HasForeignKey(acc => acc.OperacaoId);
        }
    }
}