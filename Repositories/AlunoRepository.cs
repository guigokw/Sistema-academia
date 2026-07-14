using Microsoft.EntityFrameworkCore;

public interface IAlunoRepository
{

    Aluno? ProcurarCpf(string cpfAluno);

    Aluno? ProcurarEmail(string email);

    Aluno? ProcurarTelefone(string telefone);
    void AddAluno(Aluno aluno);
    void EditTelefone(Aluno aluno, string telefone);
    void editEmail(Aluno aluno, string email);

    void editStatusInativo(Aluno aluno);

    void editStatusAtivo(Aluno aluno);
    void DeleteAluno(Aluno aluno);

    void SearchAluno(Aluno aluno);

    Task<List<Aluno>> obterAlunosAtivos();

    Task<List<Aluno>> obterAlunosInativos();
}


public class AlunoRepository : IAlunoRepository
{
    private readonly AppDataContext _context;

    public AlunoRepository(AppDataContext context)
    {
        _context = context;
    }
    public Aluno? ProcurarCpf(string cpfAluno)
    {
        return _context.Alunos.FirstOrDefault(a => a.CpfAluno == cpfAluno);
    }

    public Aluno? ProcurarEmail(string email)
    {
        return _context.Alunos.FirstOrDefault(a => a.EmailAluno == email);
    }

    public Aluno? ProcurarTelefone(string telefone)
    {
        return _context.Alunos.FirstOrDefault(a => a.TelefoneAluno == telefone);
    }


    public void AddAluno(Aluno aluno)
    {  
        _context.Alunos.Add(aluno);
        _context.SaveChanges();    
    }
    

    public void EditTelefone(Aluno aluno, string telefone)
    {
        aluno.TelefoneAluno = telefone;
        _context.SaveChanges();
    }

    public void editEmail(Aluno aluno, string email)
    {     
        aluno.EmailAluno = email;
        _context.Alunos.Update(aluno);
        _context.SaveChanges();   
    }

    public void editStatusAtivo(Aluno aluno)
    {
        
        aluno.Ativo = true;
        _context.SaveChanges();
    }

    public void editStatusInativo(Aluno aluno)
    {  
        aluno.Ativo = false;
        _context.SaveChanges();  
    }

    public void DeleteAluno(Aluno aluno)
    {
        
        _context.Alunos.Remove(aluno);
        _context.SaveChanges();
    }

    public void SearchAluno(Aluno aluno)
    {
        Console.WriteLine($"Nome: {aluno.NomeAluno}");
        Console.WriteLine($"CPF: {aluno.CpfAluno}");
        Console.WriteLine($"Telefone: {aluno.TelefoneAluno}");
        Console.WriteLine($"Email: {aluno.EmailAluno}");
        Console.WriteLine($"Data de Nascimento: {aluno.DataNascimento}");
        if (aluno.Ativo)
        {
            Console.WriteLine("Status: Ativo");
        }
        else
        {
            Console.WriteLine("Status: Inativo");
        }
        Console.WriteLine("-----------------------------");
    }

    public async Task<List<Aluno>> obterAlunosAtivos()
    {
        return await _context.Alunos
        .Where(a => a.Ativo)
        .ToListAsync();

    }
    
    public async Task<List<Aluno>> obterAlunosInativos()
    {
        return await _context.Alunos
        .Where(a => ! a.Ativo)
        .ToListAsync();
    }

}
            