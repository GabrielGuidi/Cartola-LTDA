﻿// <auto-generated />
using System;
using Cartola.Infra;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cartola.Infra.Migrations
{
    [DbContext(typeof(CartolaDBContext))]
    partial class CartolaDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Cartola.Domain.Entities.Clube", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abreviacao")
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<int>("ClubeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("NomeFantasia")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<int?>("Posicao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClubeId")
                        .IsUnique();

                    b.ToTable("Clube");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Escudos", b =>
                {
                    b.Property<int>("EscudosId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClubeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Grande")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Medio")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("Pequeno")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("EscudosId");

                    b.HasIndex("ClubeId")
                        .IsUnique();

                    b.ToTable("Escudos");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Esquema", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("EsquemaId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.HasIndex("EsquemaId")
                        .IsUnique();

                    b.ToTable("Esquema");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.EsquemaPosicoes", b =>
                {
                    b.Property<int>("PosicoesEscalacaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Atacantes")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("EsquemaId")
                        .HasColumnType("int");

                    b.Property<int>("Goleiro")
                        .HasColumnType("int");

                    b.Property<int>("Laterais")
                        .HasColumnType("int");

                    b.Property<int>("Meias")
                        .HasColumnType("int");

                    b.Property<int>("Tecnico")
                        .HasColumnType("int");

                    b.Property<int>("Zagueiros")
                        .HasColumnType("int");

                    b.HasKey("PosicoesEscalacaoId");

                    b.HasIndex("EsquemaId")
                        .IsUnique();

                    b.ToTable("EsquemaPosicoes");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Jogador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apelido")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<int>("ClubeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Foto")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<int>("JogadorId")
                        .HasColumnType("int");

                    b.Property<int>("JogosNum")
                        .HasColumnType("int");

                    b.Property<decimal>("MediaNum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<decimal>("PontosNum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PosicaoId")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecoNum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RodadaId")
                        .HasColumnType("int");

                    b.Property<int?>("ScoutAtualId")
                        .HasColumnType("int");

                    b.Property<string>("Slug")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<decimal>("VariacaoNum")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ClubeId");

                    b.HasIndex("JogadorId")
                        .IsUnique();

                    b.HasIndex("PosicaoId");

                    b.HasIndex("RodadaId");

                    b.HasIndex("ScoutAtualId")
                        .IsUnique()
                        .HasFilter("[ScoutAtualId] IS NOT NULL");

                    b.HasIndex("StatusId");

                    b.ToTable("Jogador");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.JogadorHistorico", b =>
                {
                    b.Property<int>("JogadorHistoricoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClubeId")
                        .HasColumnType("int");

                    b.Property<bool>("Consolidado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("JogadorId")
                        .HasColumnType("int");

                    b.Property<int>("JogosNum")
                        .HasColumnType("int");

                    b.Property<decimal>("MediaNum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("PontosNum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PosicaoId")
                        .HasColumnType("int");

                    b.Property<decimal>("PrecoNum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RodadaId")
                        .HasColumnType("int");

                    b.Property<int?>("ScoutId")
                        .HasColumnType("int");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<decimal>("VariacaoNum")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("JogadorHistoricoId");

                    b.HasIndex("ClubeId");

                    b.HasIndex("JogadorHistoricoId")
                        .IsUnique();

                    b.HasIndex("PosicaoId");

                    b.HasIndex("RodadaId");

                    b.HasIndex("ScoutId")
                        .IsUnique()
                        .HasFilter("[ScoutId] IS NOT NULL");

                    b.HasIndex("StatusId");

                    b.HasIndex("JogadorId", "RodadaId")
                        .IsUnique();

                    b.ToTable("JogadorHistorico");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Partida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AproveitamentoMandante")
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<string>("AproveitamentoVisitante")
                        .HasColumnType("nvarchar(16)")
                        .HasMaxLength(16);

                    b.Property<int>("ClubeCasaId")
                        .HasColumnType("int");

                    b.Property<int>("ClubeCasaPosicao")
                        .HasColumnType("int");

                    b.Property<int?>("ClubeVencedorId")
                        .HasColumnType("int");

                    b.Property<int>("ClubeVisitanteId")
                        .HasColumnType("int");

                    b.Property<int>("ClubeVisitantePosicao")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataPartida")
                        .HasColumnType("datetime2");

                    b.Property<string>("LocalPartida")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<int>("PartidaId")
                        .HasColumnType("int");

                    b.Property<bool>("PartidaValida")
                        .HasColumnType("bit");

                    b.Property<int?>("PlacarOficialMandante")
                        .HasColumnType("int");

                    b.Property<int?>("PlacarOficialVisitante")
                        .HasColumnType("int");

                    b.Property<int>("Rodada")
                        .HasColumnType("int");

                    b.Property<string>("UrlConfronto")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("UrlTransmissao")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("ClubeCasaId");

                    b.HasIndex("ClubeVencedorId");

                    b.HasIndex("ClubeVisitanteId");

                    b.HasIndex("PartidaId")
                        .IsUnique();

                    b.ToTable("Partida");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.PontuacaoParcial", b =>
                {
                    b.Property<int>("PontuacaoParcialId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Apelido")
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<bool>("Consolidado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("JogadorId")
                        .HasColumnType("int");

                    b.Property<decimal>("Pontuacao")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("RodadaId")
                        .HasColumnType("int");

                    b.Property<int?>("ScoutId")
                        .HasColumnType("int");

                    b.HasKey("PontuacaoParcialId");

                    b.HasIndex("JogadorId");

                    b.HasIndex("RodadaId");

                    b.HasIndex("ScoutId")
                        .IsUnique()
                        .HasFilter("[ScoutId] IS NOT NULL");

                    b.ToTable("PontuacaoParcial");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Posicao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abreviacao")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PosicaoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PosicaoId")
                        .IsUnique();

                    b.ToTable("Posicao");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Rodada", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataFim")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataInicio")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("RodadaId")
                        .HasColumnType("int")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("RodadaId")
                        .IsUnique();

                    b.ToTable("Rodada");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Scout", b =>
                {
                    b.Property<int>("ScoutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Assistencia")
                        .HasColumnType("int");

                    b.Property<int>("CartaoAmarelo")
                        .HasColumnType("int");

                    b.Property<int>("CartaoVermelho")
                        .HasColumnType("int");

                    b.Property<bool>("Consolidado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<int>("DefesaDePenalti")
                        .HasColumnType("int");

                    b.Property<int>("DefesaDificil")
                        .HasColumnType("int");

                    b.Property<int>("Desarme")
                        .HasColumnType("int");

                    b.Property<int>("FaltaCometida")
                        .HasColumnType("int");

                    b.Property<int>("FaltaSofrida")
                        .HasColumnType("int");

                    b.Property<int>("FinalizacaoDefendida")
                        .HasColumnType("int");

                    b.Property<int>("FinalizacaoNaTrave")
                        .HasColumnType("int");

                    b.Property<int>("FinalizacaoParaFora")
                        .HasColumnType("int");

                    b.Property<int>("Gol")
                        .HasColumnType("int");

                    b.Property<int>("GolContra")
                        .HasColumnType("int");

                    b.Property<int>("GolSofrido")
                        .HasColumnType("int");

                    b.Property<int>("Impedimento")
                        .HasColumnType("int");

                    b.Property<int?>("JogadorId")
                        .HasColumnType("int");

                    b.Property<int>("JogoSemSofrerGols")
                        .HasColumnType("int");

                    b.Property<int>("Origem")
                        .HasColumnType("int");

                    b.Property<int>("PasseIncompleto")
                        .HasColumnType("int");

                    b.Property<int>("PenaltiPerdido")
                        .HasColumnType("int");

                    b.Property<int>("RodadaId")
                        .HasColumnType("int");

                    b.HasKey("ScoutId");

                    b.HasIndex("JogadorId");

                    b.HasIndex("RodadaId");

                    b.HasIndex("ScoutId")
                        .IsUnique();

                    b.ToTable("Scout");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StatusId")
                        .IsUnique();

                    b.ToTable("Status");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Transmissao", b =>
                {
                    b.Property<int>("TransmissaoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DataModificacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Label")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<int>("PartidaId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("TransmissaoId");

                    b.HasIndex("PartidaId")
                        .IsUnique();

                    b.ToTable("Transmissao");
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Escudos", b =>
                {
                    b.HasOne("Cartola.Domain.Entities.Clube", "Clube")
                        .WithOne("Escudos")
                        .HasForeignKey("Cartola.Domain.Entities.Escudos", "ClubeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cartola.Domain.Entities.EsquemaPosicoes", b =>
                {
                    b.HasOne("Cartola.Domain.Entities.Esquema", "Esquema")
                        .WithOne("Posicoes")
                        .HasForeignKey("Cartola.Domain.Entities.EsquemaPosicoes", "EsquemaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Jogador", b =>
                {
                    b.HasOne("Cartola.Domain.Entities.Clube", "Clube")
                        .WithMany("Jogadores")
                        .HasForeignKey("ClubeId")
                        .HasPrincipalKey("ClubeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cartola.Domain.Entities.Posicao", "Posicao")
                        .WithMany()
                        .HasForeignKey("PosicaoId")
                        .HasPrincipalKey("PosicaoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cartola.Domain.Entities.Rodada", "Rodada")
                        .WithMany()
                        .HasForeignKey("RodadaId")
                        .HasPrincipalKey("RodadaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cartola.Domain.Entities.Scout", "ScoutAtual")
                        .WithOne("Jogador")
                        .HasForeignKey("Cartola.Domain.Entities.Jogador", "ScoutAtualId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Cartola.Domain.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .HasPrincipalKey("StatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Cartola.Domain.Entities.JogadorHistorico", b =>
                {
                    b.HasOne("Cartola.Domain.Entities.Clube", "Clube")
                        .WithMany()
                        .HasForeignKey("ClubeId")
                        .HasPrincipalKey("ClubeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cartola.Domain.Entities.Jogador", "Jogador")
                        .WithMany("JogadoresHistorico")
                        .HasForeignKey("JogadorId")
                        .HasPrincipalKey("JogadorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cartola.Domain.Entities.Posicao", "Posicao")
                        .WithMany()
                        .HasForeignKey("PosicaoId")
                        .HasPrincipalKey("PosicaoId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cartola.Domain.Entities.Rodada", "Rodada")
                        .WithMany()
                        .HasForeignKey("RodadaId")
                        .HasPrincipalKey("RodadaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cartola.Domain.Entities.Scout", "Scout")
                        .WithOne()
                        .HasForeignKey("Cartola.Domain.Entities.JogadorHistorico", "ScoutId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Cartola.Domain.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId")
                        .HasPrincipalKey("StatusId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Partida", b =>
                {
                    b.HasOne("Cartola.Domain.Entities.Clube", "ClubeCasa")
                        .WithMany("PartidasMandante")
                        .HasForeignKey("ClubeCasaId")
                        .HasPrincipalKey("ClubeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cartola.Domain.Entities.Clube", "ClubeVencedor")
                        .WithMany()
                        .HasForeignKey("ClubeVencedorId")
                        .HasPrincipalKey("ClubeId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("Cartola.Domain.Entities.Clube", "ClubeVisitante")
                        .WithMany("PartidasVisitante")
                        .HasForeignKey("ClubeVisitanteId")
                        .HasPrincipalKey("ClubeId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Cartola.Domain.Entities.PontuacaoParcial", b =>
                {
                    b.HasOne("Cartola.Domain.Entities.Jogador", "Jogador")
                        .WithMany("HistoricoParciais")
                        .HasForeignKey("JogadorId")
                        .HasPrincipalKey("JogadorId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cartola.Domain.Entities.Rodada", "Rodada")
                        .WithMany()
                        .HasForeignKey("RodadaId")
                        .HasPrincipalKey("RodadaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("Cartola.Domain.Entities.Scout", "Scout")
                        .WithOne()
                        .HasForeignKey("Cartola.Domain.Entities.PontuacaoParcial", "ScoutId")
                        .OnDelete(DeleteBehavior.NoAction);
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Scout", b =>
                {
                    b.HasOne("Cartola.Domain.Entities.Jogador", null)
                        .WithMany("HistoricoScouts")
                        .HasForeignKey("JogadorId");

                    b.HasOne("Cartola.Domain.Entities.Rodada", "Rodada")
                        .WithMany()
                        .HasForeignKey("RodadaId")
                        .HasPrincipalKey("RodadaId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();
                });

            modelBuilder.Entity("Cartola.Domain.Entities.Transmissao", b =>
                {
                    b.HasOne("Cartola.Domain.Entities.Partida", "Partida")
                        .WithOne("Transmissao")
                        .HasForeignKey("Cartola.Domain.Entities.Transmissao", "PartidaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
