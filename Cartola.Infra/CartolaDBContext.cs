using Cartola.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Cartola.Infra
{
    public class CartolaDBContext : DbContext, IDisposable
    {
        private const string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CartolaBackup;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        #region [Constructors]
        public CartolaDBContext() { }

        public CartolaDBContext(DbContextOptions<CartolaDBContext> options) : base(options) { }
        #endregion

        public DbSet<Clube> Clube { get; set; }
        public DbSet<Escudos> Escudos { get; set; }
        public DbSet<Posicao> Posicao { get; set; }
        public DbSet<Esquema> Esquema { get; set; }
        public DbSet<EsquemaPosicoes> PosicoesEscalacao { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Rodada> Rodada { get; set; }
        public DbSet<Partida> Partida { get; set; }
        public DbSet<Transmissao> Transmissao { get; set; }
        public DbSet<Jogador> Jogador { get; set; }
        public DbSet<JogadorHistorico> JogadorHistorico { get; set; }
        public DbSet<Scout> Scout { get; set; }
        public DbSet<PontuacaoParcial> PontuacaoParcial { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Clube>()
                .HasIndex(b => b.ClubeId)
                .IsUnique();

            modelBuilder.Entity<Clube>()
                .HasOne(b => b.Escudos)
                .WithOne(i => i.Clube)
                .HasForeignKey<Escudos>(b => b.ClubeId);


            modelBuilder.Entity<Posicao>()
                .HasIndex(b => b.PosicaoId)
                .IsUnique();

            modelBuilder.Entity<Esquema>()
                .HasIndex(b => b.EsquemaId)
                .IsUnique();

            modelBuilder.Entity<Status>()
                .HasIndex(b => b.StatusId)
                .IsUnique();

            modelBuilder.Entity<Rodada>()
                .HasIndex(b => b.RodadaId)
                .IsUnique();


            modelBuilder.Entity<Partida>()
                .HasIndex(b => b.PartidaId)
                .IsUnique();

            modelBuilder.Entity<Partida>()
                .HasOne(b => b.ClubeCasa)
                .WithMany(b => b.PartidasMandante)
                .HasForeignKey(b => b.ClubeCasaId)
                .HasPrincipalKey(c => c.ClubeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Partida>()
                .HasOne(b => b.ClubeVisitante)
                .WithMany(b => b.PartidasVisitante)
                .HasForeignKey(b => b.ClubeVisitanteId)
                .HasPrincipalKey(c => c.ClubeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Partida>()
                .HasOne(b => b.ClubeVencedor)
                .WithMany()
                .HasForeignKey(b => b.ClubeVencedorId)
                .HasPrincipalKey(c => c.ClubeId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<Jogador>()
                .HasIndex(b => b.JogadorId)
                .IsUnique();

            modelBuilder.Entity<Jogador>()
                .HasOne(b => b.Clube)
                .WithMany(b => b.Jogadores)
                .HasForeignKey(b => b.ClubeId)
                .HasPrincipalKey(c => c.ClubeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Jogador>()
                .HasOne(b => b.Rodada)
                .WithMany()
                .HasForeignKey(b => b.RodadaId)
                .HasPrincipalKey(c => c.RodadaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Jogador>()
                .HasOne(b => b.Posicao)
                .WithMany()
                .HasForeignKey(b => b.PosicaoId)
                .HasPrincipalKey(c => c.PosicaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Jogador>()
                .HasOne(b => b.Status)
                .WithMany()
                .HasForeignKey(b => b.StatusId)
                .HasPrincipalKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Jogador>()
                .HasOne(b => b.ScoutAtual)
                .WithOne(i => i.Jogador)
                .HasForeignKey<Jogador>(b => b.ScoutAtualId)
                .HasPrincipalKey<Scout>(c => c.ScoutId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Scout>()
                .HasIndex(b => b.ScoutId)
                .IsUnique();

            modelBuilder.Entity<Scout>()
               .HasOne(b => b.Rodada)
               .WithMany()
               .HasForeignKey(b => b.RodadaId)
               .HasPrincipalKey(c => c.RodadaId)
               .OnDelete(DeleteBehavior.NoAction);

            
            modelBuilder.Entity<JogadorHistorico>()
                .HasIndex(b => b.JogadorHistoricoId)
                .IsUnique();

            modelBuilder.Entity<JogadorHistorico>()
                .HasIndex(b => new { b.JogadorId, b.RodadaId })
                .IsUnique();

            modelBuilder.Entity<JogadorHistorico>()
                .HasOne(b => b.Jogador)
                .WithMany(i => i.JogadoresHistorico)
                .HasForeignKey(b => b.JogadorId)
                .HasPrincipalKey(c => c.JogadorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JogadorHistorico>()
                .HasOne(b => b.Rodada)
                .WithMany()
                .HasForeignKey(b => b.RodadaId)
                .HasPrincipalKey(c => c.RodadaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JogadorHistorico>()
                .HasOne(b => b.Clube)
                .WithMany()
                .HasForeignKey(b => b.ClubeId)
                .HasPrincipalKey(c => c.ClubeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JogadorHistorico>()
                .HasOne(b => b.Posicao)
                .WithMany()
                .HasForeignKey(b => b.PosicaoId)
                .HasPrincipalKey(c => c.PosicaoId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JogadorHistorico>()
                .HasOne(b => b.Status)
                .WithMany()
                .HasForeignKey(b => b.StatusId)
                .HasPrincipalKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<JogadorHistorico>()
                .HasOne(b => b.Scout)
                .WithOne()
                .HasForeignKey<JogadorHistorico>(b => b.ScoutId)
                .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<PontuacaoParcial>()
               .HasOne(b => b.Jogador)
               .WithMany(i => i.HistoricoParciais)
               .HasForeignKey(b => b.JogadorId)
               .HasPrincipalKey(c => c.JogadorId)
               .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PontuacaoParcial>()
                .HasOne(b => b.Rodada)
                .WithMany()
                .HasForeignKey(b => b.RodadaId)
                .HasPrincipalKey(c => c.RodadaId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PontuacaoParcial>()
                .HasOne(b => b.Scout)
                .WithOne()
                .HasForeignKey<PontuacaoParcial>(b => b.ScoutId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
