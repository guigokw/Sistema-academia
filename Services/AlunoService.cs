using System.Text.RegularExpressions;

public interface IAlunoService
{
    Aluno ObterAluno(string cpfAluno);

    void CadastrarAluno(Aluno aluno);
    void EditarTelefone(string cpfAluno, string telefone);
    void EditarEmail(string cpfAluno, string email);

    void EditarStatusInativo(string cpfAluno);

    void EditarStatusAtivo(string cpfAluno);
    void ExcluirAluno(string cpfAluno);

    void BuscarAluno(string cpfAluno);

    Task<IEnumerable<ExibicaoAlunosDTO>> listarAlunosAtivos();

    Task<IEnumerable<ExibicaoAlunosDTO>> listarAlunosInativos(); 

}

public class AlunoService : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository;

    public AlunoService(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;
    }

    public Aluno ObterAluno(string cpfAluno)
    {
        Aluno? aluno = _alunoRepository.ProcurarCpf(cpfAluno);

        if (aluno == null ) 
        {
           throw new AlunoNaoEncontradoException("não foi possível encontrar o aluno pois ele não está registrado no sistema");
        }
        return aluno;
    }



    public void CadastrarAluno(Aluno aluno)
    {

        var alunoExiste = _alunoRepository.ProcurarCpf(aluno.CpfAluno);

        if (alunoExiste != null)
        {
            throw new AlunoJaExisteException("não foi possível cadastrar esse aluno pois seu CPF já está registrado no sistema");
        }

        var emailExiste = _alunoRepository.ProcurarEmail(aluno.EmailAluno);

        if (emailExiste != null)
        {
            throw new EmailJaExisteException("não foi possível cadastrar esse aluno pois seu email já está registrado no sistema");
        }

        var TelefoneExiste = _alunoRepository.ProcurarTelefone(aluno.TelefoneAluno);

        if (TelefoneExiste != null)
        {
            throw new TelefoneJaExisteException("não foi possível cadastrar esse aluno pois seu telefone já está registrado no sistema");
        }

         _alunoRepository.AddAluno(aluno);
    }

    

    public void EditarTelefone(string cpfAluno, string telefone)
    { 
          bool numeroTelefone = Regex.IsMatch(telefone, @"^\d+$");

          if (telefone.Length != 11 || !numeroTelefone)
          {
              throw new TelefoneInvalidoException("não foi possível alterar o número de telefone pois o formato está invalido");
          }

          var telefoneExiste = _alunoRepository.ProcurarTelefone(telefone);

          if (telefoneExiste != null)
          {
              throw new TelefoneJaExisteException("não foi possível alterar o número de telefone pois ele já está registrado no sistema");
          }

          Aluno aluno = ObterAluno(cpfAluno);

        _alunoRepository.EditTelefone(aluno, telefone);
        
    }

    public void EditarEmail(string cpfAluno, string email)
    {        

        if (!email.Contains('@'))
        {
            throw new EmailInvalidoException("não foi possível cadastrar esse email pois ele contém o formato inválido");
        }

        var emailExiste = _alunoRepository.ProcurarEmail(email);

        if (emailExiste != null)
        {
            throw new EmailJaExisteException("não foi possível cadastrar esse email pois ele já está registrado no sistema");
        }

        Aluno aluno = ObterAluno(cpfAluno);

        _alunoRepository.editEmail(aluno, email);
        
    }

    public void EditarStatusAtivo(string cpfAluno)
    {

          var aluno = ObterAluno(cpfAluno);
          if (aluno.Ativo)
          {
              throw new StatusInvalidoException("erro ao editar status do aluno pois ele já está ativo no sistema");
          }
        _alunoRepository.editStatusAtivo(aluno);

    }

    public void EditarStatusInativo(string cpfAluno)
    {

          var aluno = ObterAluno(cpfAluno);

          if (!aluno.Ativo)
          {
              throw new StatusInvalidoException("erro ao editar status do aluno pois ele já está inativo no sistema");
          }

        _alunoRepository.editStatusInativo(aluno);
    
    }
    

    public void ExcluirAluno(string cpfAluno)
    {     
        Aluno aluno = ObterAluno(cpfAluno);

        _alunoRepository.DeleteAluno(aluno);
    }

    public void BuscarAluno(string cpfAluno)
    {
        Aluno aluno = ObterAluno(cpfAluno);

        _alunoRepository.SearchAluno(aluno);
    }

    public async Task<IEnumerable<ExibicaoAlunosDTO>> listarAlunosAtivos()
    {
        var alunosAtivos = await _alunoRepository.obterAlunosAtivos();

        if (alunosAtivos == null || !alunosAtivos.Any())
        {
            throw new AlunoNaoEncontradoException("não foi possível listar oas alunos ativos pois eles não há alunos ativos no sistema");
        }

        var exibirDados = alunosAtivos.Select(a => new ExibicaoAlunosDTO(
            a.NomeAluno,
            a.CpfAluno,
            a.TelefoneAluno,
            a.EmailAluno,
            a.DataNascimento
        )).ToList();
            

        return exibirDados;
    }

    public async Task<IEnumerable<ExibicaoAlunosDTO>> listarAlunosInativos()
    {
        var alunosInativos = await _alunoRepository.obterAlunosInativos();

        if (alunosInativos == null || !alunosInativos.Any())
        {
            throw new AlunoNaoEncontradoException("não foi possível listar os alunos inativos pois não há nenhum registrado");
        }

        var exibirDados = alunosInativos.Select(a => new ExibicaoAlunosDTO(
            a.NomeAluno,
            a.CpfAluno,
            a.TelefoneAluno,
            a.EmailAluno,
            a.DataNascimento
        )).ToList();
            

        return exibirDados;
    }

}
    
