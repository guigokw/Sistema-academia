using Microsoft.EntityFrameworkCore;

public interface IMatriculaService
{
    Matricula ObterMatricula(int idMatricula);

    Aluno ObterAluno(string cpfAluno);

    Plano ObterPlano(int idPlano);
    void CadastrarMatricula(int id, string cpfAluno, int idPlano, DateOnly dataInicio);
    void RenovarMatricula(int idMatricula, String cpfAluno);
    void CancelarMatricula(int idMatricula, String cpfAluno);
    void VerificarVencimento(int idMatricula, String cpfAluno);

    Task<IEnumerable<exibicaoPagamentosDTO>> listarMatriculasVencidas();

    Task<IEnumerable<exibicaoPagamentosDTO>> listarMatriculasAtivas();
}

public class MatriculaService : IMatriculaService
{

    private readonly IMatriculaRepository _matriculaRepository;

    public MatriculaService(IMatriculaRepository matriculaRepository)
    {
        _matriculaRepository = matriculaRepository;
    }

    public Matricula ObterMatricula(int idMatricula)
    {
        Matricula? matricula = _matriculaRepository.acharMatricula(idMatricula);

        if (matricula == null)
        {
            throw new MatriculaNaoEncontradaException("não foi possível encontrar a matrícula pois ela não consta como registrada no sistema");
        }

        return matricula;
    }

    public Aluno ObterAluno(string cpfAluno)
    {
        Aluno? aluno = _matriculaRepository.acharAluno(cpfAluno);

        if (aluno == null)
        {
            throw new AlunoNaoEncontradoException("não foi possível encontrar o aluno pois ele não consta como registrado no sistema");
        }

        return aluno;
    }

    public Plano ObterPlano(int idPlano)
    {
        Plano? plano = _matriculaRepository.acharPlano(idPlano);

        if (plano == null)
        {
            throw new PlanoNaoEncontradoException("não foi possível encontrar o plano pois ele não consta como registrado no sistema");
        }

        return plano;
    }
    public void CadastrarMatricula(int id, string cpfAluno, int idPlano, DateOnly dataInicio)
    {
    
        var MatriculaExiste = _matriculaRepository.acharMatricula(id);

        if (MatriculaExiste != null)
        {
            throw new MatriculaRepetidaException("não foi possível fazer esse cadastro pois esse id de matrícula já foi registrada no sistema");
        }

        var aluno = ObterAluno(cpfAluno);
        var plano = ObterPlano(idPlano);

        DateOnly dataVencimento = default;

        if (plano.TipoPlano == TiposDePlano.Mensal)
        {
            dataVencimento = dataInicio.AddMonths(1);
        }
        else if (plano.TipoPlano == TiposDePlano.Trimestral)
        {
            dataVencimento = dataInicio.AddMonths(3);
        }
        else if (plano.TipoPlano == TiposDePlano.Semestral)
        {
            dataVencimento = dataInicio.AddMonths(6);
        }
        else if (plano.TipoPlano == TiposDePlano.Anual)
        {
            dataVencimento = dataInicio.AddYears(1);
        }

        Matricula matricula = new Matricula
        {
            idMatricula = id,
            CpfAluno = cpfAluno,
            Aluno = aluno,
            IdPlano = idPlano,
            Plano = plano,
            DataInicio = dataInicio,
            DataVencimento = dataVencimento
        };

        _matriculaRepository.addMatricula(matricula);
        
    }

    public void RenovarMatricula(int idMatricula, string cpfAluno)
    {
        
        ObterAluno(cpfAluno);
        var alunoMatricula = _matriculaRepository.acharMatriculaPorAluno(cpfAluno, idMatricula);

        if (alunoMatricula == null)
        {
            throw new AlunoNaoEncontradoException("não foi possível renovar a matrícula pois o aluno não possui uma matrícula ativa no sistema ou essa matricula não está relacionada com esse aluno");
        }

        Matricula matricula = ObterMatricula(idMatricula);

        _matriculaRepository.renovaMatricula(matricula);
        
    }

    public void CancelarMatricula(int idMatricula, string cpfAluno)
    {
    
        var alunoExiste = _matriculaRepository.acharMatriculaPorAluno(cpfAluno, idMatricula);

        if (alunoExiste == null)
        {
            throw new AlunoNaoEncontradoException("não foi possível cancelar a matrícula pois o aluno não possui uma matrícula ativa no sistema ou essa matrícula não está relacionada com esse aluno");
        }

        Matricula matricula = ObterMatricula(idMatricula);

        _matriculaRepository.CancelMatricula(matricula);

    }

    public void VerificarVencimento(int idMatricula, string cpfAluno)
    {
        
        var alunoExiste = _matriculaRepository.acharMatriculaPorAluno(cpfAluno, idMatricula);

        if (alunoExiste == null)
        {
            throw new AlunoNaoEncontradoException("não foi possível verificar o vencimento da matrícula pois o aluno não possui uma matrícula ativa no sistema ou essa matricula não está relacionada com esse aluno");
        }

        var matricula = ObterMatricula(idMatricula);

        if (matricula == null)
            {
                throw new MatriculaNaoEncontradaException("não foi possível verificar o vencimento pois essa matrícula não foi encontrada");
            }

        if (matricula.DataVencimento < DateOnly.FromDateTime(DateTime.Now))
        {
            Console.WriteLine("a matrícula do aluno " + matricula.Aluno.NomeAluno + " está vencida");
        } else
        {
            Console.WriteLine("a matrícula do aluno " + matricula.Aluno.NomeAluno + " está ativa e vencerá no dia " + matricula.DataVencimento);
        }
        
    }

    public async Task<IEnumerable<exibicaoPagamentosDTO>> listarMatriculasVencidas()
    {
        var matriculasAtrasadas = await _matriculaRepository.obterMatriculasAtrasada();

        if (matriculasAtrasadas == null || !matriculasAtrasadas.Any())
        {
            throw new MatriculaNaoEncontradaException("não foi possível listar os pagamentos atrasados do sistema pois não há nenhuma pagamento atrasado registrada");
        }

        var exibirDados = matriculasAtrasadas.Select(a => new exibicaoPagamentosDTO(
            a.Aluno.NomeAluno,
            a.Plano.NomePlano,
            a.DataVencimento,
            a.SituacaoPagamento
        )).ToList();
            

        return exibirDados;
    }

    public async Task<IEnumerable<exibicaoPagamentosDTO>> listarMatriculasAtivas()
    {
        var matriculasAtivas = await _matriculaRepository.obterMatriculasPagas();

        if (matriculasAtivas == null || ! matriculasAtivas.Any())
        {
            throw new MatriculaNaoEncontradaException("não foi possível listar os pagamentos realizados pois nenhum matricula paga foi encontrada no sistema");
        }

        var exibirDados = matriculasAtivas.Select(a => new exibicaoPagamentosDTO(
            a.Aluno.NomeAluno,
            a.Plano.NomePlano,
            a.DataVencimento,
            a.SituacaoPagamento
        )).ToList();
            

        return exibirDados;
    }

    public async Task<IEnumerable<exibicaoPagamentosDTO>> listarMatriculasPendentes()
    {
        var matriculasPendentes= await _matriculaRepository.obterMatriculasPendentes();

        if (matriculasPendentes == null || ! matriculasPendentes.Any())
        {
            throw new MatriculaNaoEncontradaException("não foi possível listar os pagamentos pendentes pois nenhum pagamento pendente foi encontrada no sistema");
        }

        var exibirDados = matriculasPendentes.Select(a => new exibicaoPagamentosDTO(
            a.Aluno.NomeAluno,
            a.Plano.NomePlano,
            a.DataVencimento,
            a.SituacaoPagamento
        )).ToList();
            

        return exibirDados;
    }
}
