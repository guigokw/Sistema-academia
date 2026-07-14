using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;


public class AppDataContext : DbContext
{
    // cria a tabela para alunos no banco de dados
    public DbSet<Aluno> Alunos { get; set; }

    // cria a tabela para planos no banco de dados
    public DbSet<Plano> Planos { get; set; }
 
    // cria a tabela para matrículas no banco de dados
    public DbSet<Matricula> Matriculas { get; set; }

    public DbSet<Treino> Treinos {get; set;}

    public DbSet<Exercicio> Exercicios {get; set;}

    public DbSet<TreinoExercicio> TreinoExercicio {get; set;}

    public DbSet<TreinoAluno> TreinoAluno {get; set;}

    public DbSet<Pagamento> Pagamentos {get; set;}

    // configura conexão com o mysql
   // configura conexão com o mysql
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Força a leitura do diretório base do projeto (subindo de bin/Debug/net10.0 para a raiz)
        string pastaRaiz = Path.Combine(AppContext.BaseDirectory, "..", "..", "..");
        
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(pastaRaiz)
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        var serverVersion = new MySqlServerVersion(new Version(8, 3, 0));

        optionsBuilder.UseMySql(connectionString, serverVersion);
    }



protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       base.OnModelCreating(modelBuilder);

       // Garante que o CPF seja a chave de aluno no banco de dados
        modelBuilder.Entity<Aluno>()
            .HasKey(u => u.CpfAluno);

        // Garante que o Email seja único no banco de dados
        modelBuilder.Entity<Aluno>()
            .HasIndex(u => u.EmailAluno)
            .IsUnique();

        modelBuilder.Entity<Matricula>()
            .HasKey(u => u.idMatricula);

        modelBuilder.Entity<Matricula>()
        .HasOne(m => m.Aluno)
        .WithOne()
        .HasForeignKey<Matricula>(m => m.CpfAluno); 

        modelBuilder.Entity<Matricula>()
        .HasOne(m => m.Plano)
        .WithMany()
        .HasForeignKey(m => m.IdPlano);   

        modelBuilder.Entity<Treino>()
            .HasKey(u => u.idTreino); 

        modelBuilder.Entity<Exercicio>()
            .HasKey(u => u.IdExercicio);  
            
        modelBuilder.Entity<Plano>()
        .HasKey(p => p.IdPlano);  

        modelBuilder.Entity<Plano>()
        .Property(p => p.TipoPlano)
        .HasConversion(
        v => v.ToString(),
        v => Enum.Parse<TiposDePlano>(v))
        .HasColumnType("varchar(30)");

        modelBuilder.Entity<Pagamento>()
        .HasKey(k => k.IdPagamento);  
        
        modelBuilder.Entity<Pagamento>()
        .HasOne(p => p.Matricula)
        .WithMany()
        .HasForeignKey(p => p.IdMatricula);

         modelBuilder.Entity<Matricula>()
        .Property(m => m.SituacaoPagamento)
        .HasConversion(
        v => v.ToString(),
        v => Enum.Parse<SituacaoPagamento>(v))
        .HasColumnType("varchar(30)");

        modelBuilder.Entity<TreinoExercicio>()
        .HasKey(te => new {te.IdTreino, te.IdExercicio});

        modelBuilder.Entity<TreinoExercicio>()
        .HasOne(te => te.Exercicio)
        .WithMany()
        .HasForeignKey(te => te.IdExercicio);

        modelBuilder.Entity<TreinoExercicio>()
       .HasOne(ta => ta.Treino)
       .WithMany()
       .HasForeignKey(ta => ta.IdTreino);


        modelBuilder.Entity<TreinoAluno>()
        .HasKey(ta => new {ta.CpfAluno, ta.IdTreino});

        modelBuilder.Entity<TreinoAluno>()
        .HasOne(ta => ta.Aluno)
        .WithMany()
        .HasForeignKey(ta => ta.CpfAluno);

        modelBuilder.Entity<TreinoAluno>()
       .HasOne(ta => ta.Treino)
       .WithMany()
       .HasForeignKey(ta => ta.IdTreino);
                 
    }
}
    