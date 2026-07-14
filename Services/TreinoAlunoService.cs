public interface ITreinoAlunoService
{
    Treino obterTreino(int id);

    Aluno obterAluno(string cpf);

    TreinoAluno obterTreinoAluno(int id, string cpf);

    void cadastrarTreinoAluno(TreinoAluno treinoAluno);

    void deletarTreinoAluno(int id, string cpf);

    Task<IEnumerable<exibicaoTreinoAlunoDTO>> listarTreinosDoAluno(string cpf);
}

public class TreinoAlunoService : ITreinoAlunoService
{
    private readonly ITreinoAlunoRepository _treinoAlunoRepository;

    public TreinoAlunoService(ITreinoAlunoRepository treinoAlunoRepository)
    {
        _treinoAlunoRepository = treinoAlunoRepository;
    }

    public Treino obterTreino(int id)
    {
        Treino? treino = _treinoAlunoRepository.acharTreino(id);

        if (treino == null)
        {
            throw new TreinoNaoEncontradoException("não foi possível concluir essa operação pois o treino com esse id não foi encontrado");
        }

        return treino;
    }

    public Aluno obterAluno(string cpf)
    {
        Aluno? aluno = _treinoAlunoRepository.acharAluno(cpf);

        if (aluno == null)
        {
            throw new AlunoNaoEncontradoException("não foi possível concluir essa operação pois o aluno não foi encontrado");
        }

        return aluno;
    }

    public TreinoAluno obterTreinoAluno(int id, string cpf)
    {
        TreinoAluno? treinoAluno = _treinoAlunoRepository.acharTreinoAluno(id, cpf);

        if (treinoAluno == null)
        {
            throw new TreinoAlunoNaoEncontradoException("não foi possível concluir essa operação pois esse treino do aluno não foi encontrado");
        }

        return treinoAluno;
    }

    public void cadastrarTreinoAluno(TreinoAluno treinoAluno)
    {

        var aluno = obterAluno(treinoAluno.CpfAluno);

        if (! aluno.Ativo)
        {
            throw new AlunoInativoException("Não foi possível passar o treino para o aluno " +aluno.NomeAluno+ " pois ele consta como aluno inativo no sistema");
        }
        
         var treinoAlunoExiste = _treinoAlunoRepository.acharTreinoAluno(treinoAluno.IdTreino, treinoAluno.CpfAluno);

        if (treinoAlunoExiste != null)
        {
            throw new TreinoAlunoRepetidoException("não foi possível cadastrar esse treino pois ele já está relacionado a esse aluno");
        }

        obterAluno(treinoAluno.CpfAluno);
        obterTreino(treinoAluno.IdTreino);

        _treinoAlunoRepository.addTreinoAluno(treinoAluno);
        
    }

    public void deletarTreinoAluno(int id, string cpf)
    {

        var aluno = obterAluno(cpf);

        if (! aluno.Ativo)
        {
            throw new AlunoInativoException("Não foi possível remover o treino do aluno " +aluno.NomeAluno+ " pois ele consta como aluno inativo no sistema");
        }
        
        TreinoAluno treinoAluno = obterTreinoAluno(id, cpf);

        _treinoAlunoRepository.removeTreinoAluno(treinoAluno);
        
    }

    public async Task<IEnumerable<exibicaoTreinoAlunoDTO>> listarTreinosDoAluno(string cpf)
    {
        var TreinosDoAluno = await _treinoAlunoRepository.consultarTreinosAlunoEspecifico(cpf);

        if (TreinosDoAluno == null || !TreinosDoAluno.Any())
        {
            throw new AlunoNaoEncontradoException("não foi possível listar os treinos desse aluno pois não foi encontrado treinos relacionados a esse aluno");
        }

        var exibirDados = TreinosDoAluno.Select(a => new exibicaoTreinoAlunoDTO(
            a.Treino.idTreino,
            a.Treino.Nome,
            a.Treino.Objetivo,
            a.Treino.duracaoTreino
        )).ToList();

        return exibirDados;
    }
}