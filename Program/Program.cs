using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Utils;
using Microsoft.IdentityModel.Tokens;

namespace Sistema_academia
{
    internal class Program
    {

        private static IPagamentoService _pagamentoService = default!;

        private static IAlunoService _alunoService = default!;

        private static ITreinoAlunoService _treinoAlunoService = default!;

        private static IPlanoService _planoService = default!;

        private static IMatriculaService _matriculaService = default!;

        private static ITreinoService _treinoService = default!;

        private static IExercicioService _exercicioService = default!;

        private static ITreinoExercicioService _treinoExercicioService = default!;

        public Program(IPagamentoService pagamentoService, IAlunoService alunoService, ITreinoAlunoService treinoAlunoService, IPlanoService planoService, IMatriculaService matriculaService, ITreinoService treinoService, IExercicioService exercicioService, ITreinoExercicioService treinoExercicioService)
        {
            _pagamentoService = pagamentoService;
            _alunoService = alunoService;
            _treinoAlunoService = treinoAlunoService;
            _planoService = planoService;
            _matriculaService = matriculaService;
            _treinoService = treinoService;
            _exercicioService = exercicioService;
            _treinoExercicioService = treinoExercicioService;
        }

        static async Task Main(string[] args)
        {
            using var context = new AppDataContext();
            AlunoRepository alunoRepository = new AlunoRepository(context);
            TreinoAlunoRepository treinoRepository = new TreinoAlunoRepository(context);
            PlanoRepository planoRepository = new PlanoRepository(context);
            MatriculaRepository matriculaRepository = new MatriculaRepository(context);
            TreinoRepository treinoServiceRepository = new TreinoRepository(context);
            ExercicioRepository exercicioRepository = new ExercicioRepository(context);
            TreinoExercicioRepository treinoExercicioRepository = new TreinoExercicioRepository(context);
            PagamentoRepository pagamentoRepository = new PagamentoRepository(context);
            _alunoService = new AlunoService(alunoRepository);
            _treinoAlunoService = new TreinoAlunoService(treinoRepository);
            _planoService = new PlanoService(planoRepository);
            _matriculaService = new MatriculaService(matriculaRepository);
            _treinoService = new TreinoService(treinoServiceRepository);
            _exercicioService = new ExercicioService(exercicioRepository);
            _treinoExercicioService = new TreinoExercicioService(treinoExercicioRepository);
            _pagamentoService = new PagamentoService(pagamentoRepository);
           try {
            while (true) {
                await AtualizarPagamentos(matriculaRepository, pagamentoRepository);
                Console.WriteLine("=== SISTEMA DE ACADEMIA ===");
                Console.WriteLine("1 - Menu do cliente"); // correto
                Console.WriteLine("2 - Menu de planos"); // correto
                Console.WriteLine("3 - Menu de matrículas"); // correto
                Console.WriteLine("4 - Menu de treinos"); // correto
                Console.WriteLine("5 - Menu de exercícios"); // correto
                Console.WriteLine("6 - Menu de Treinos do aluno"); // correto
                Console.WriteLine("7 - Menu de exercicios dos treinos"); // correto
                Console.WriteLine("8 - Menu de pagamentos"); // correto
                Console.WriteLine("9 - Sair do sistema");
                Console.WriteLine("---------------------");
                Console.WriteLine("Escolha uma dessas opções para poder prosseguir");
                int opcao = int.Parse(Console.ReadLine() ?? "0");

                switch (opcao)
                {
                    case 1:
                        await menuCliente(_alunoService, _treinoAlunoService, _matriculaService);
                        break;
                    case 2:
                        await menuPlano(_planoService);
                        break;
                    case 3:
                        await menuMatricula(_matriculaService);
                        break;
                    case 4:
                        await menuTreino(_treinoService);
                        break;
                    case 5:
                        await menuExercicio(_exercicioService);
                        break;
                    case 6:
                        await menuTreinoAluno(_treinoAlunoService);
                        break; 
                    case 7: 
                        await menuExercicioTreino(_treinoExercicioService);
                        break;
                    case 8:
                        await MenuPagamentos(_pagamentoService, _matriculaService, pagamentoRepository, matriculaRepository);
                        break;
                    case 9:
                        Console.WriteLine("Saindo do sistema...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Opção inválida, por favor digite novamente");
                        break;    
                }
            }
           } catch (FormatException)
            {
                Console.WriteLine("formato inserido inválido, por favor insira novamente");
            }
            catch (System.OverflowException)
            {
                Console.WriteLine("o formato inserido é inválido por se tratar de ser muito longo ou muito pequeno para esse valor");
            }
        }

        private static async Task menuCliente(IAlunoService alunoService, ITreinoAlunoService treinoAlunoService, IMatriculaService matriculaService) {
            try {  
              while (true) {  
                Console.WriteLine("--- MENU DO CLIENTE ---");
                Console.WriteLine("1 - Cadastrar cliente"); // correto
                Console.WriteLine("2 - remover cliente"); // correto
                Console.WriteLine("3 - Atualizar dados do cliente"); // correto
                Console.WriteLine("4 - Consultar informações do cliente"); // correto
                Console.WriteLine("5 - Sair do menu do cliente");
                Console.WriteLine("---------------------");

                Console.WriteLine("Escolha uma dessas opções para poder prosseguir");
                int opcao = int.Parse(Console.ReadLine() ?? "0");

                switch (opcao)
                  {
                      case 1:
                        
                          Console.WriteLine("Qual o nome desse aluno?");
                          string nome = Console.ReadLine() ?? string.Empty;

                          Console.WriteLine("Qual o cpf desse aluno?");
                          string cpf = Console.ReadLine() ?? string.Empty;

                          Console.WriteLine("Qual o telefone desse aluno?");
                          string telefone = Console.ReadLine() ?? string.Empty;

                          Console.WriteLine("Qual o email desse aluno");
                          string email = Console.ReadLine() ?? string.Empty;

                          Console.WriteLine("Qual a data se nascimento desse aluno? insira no formato dd/mm/yyyy");
                          string data = Console.ReadLine() ?? string.Empty;

                          Aluno aluno = new Aluno()
                          {
                              NomeAluno = nome,
                              CpfAluno = cpf,
                              TelefoneAluno = telefone,
                              EmailAluno = email,
                              DataNascimento = data,
                              Ativo = true
                        };


                          alunoService.CadastrarAluno(aluno);
                          Console.WriteLine("aluno " + aluno.NomeAluno + " cadastrado com sucesso no sistema");
                          break;
                      case 2:
                          Console.WriteLine("Qual o cpf do aluno que você deseja remover do sistema?");
                          string cpfRemover = Console.ReadLine() ?? string.Empty;

                          alunoService.ExcluirAluno(cpfRemover);
                          Console.WriteLine("aluno " + alunoService.ObterAluno(cpfRemover)?.NomeAluno + " excluído com sucesso");
                          break;
                      case 3:
                           menuAtualizarCliente(alunoService);
                           break;
                      case 4:
                           await menuConsultaCliente(alunoService, treinoAlunoService, matriculaService);
                           break;
                      case 5:
                          return;
                      default: 
                          Console.WriteLine("opção inválida, por favor digite novamente");
                          break;  
                  }               
                
             }
            } catch (FormatException)
            {
                Console.WriteLine("formato inserido inválido, por favor insira novamente");
            } catch (CpfInvalidoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (TelefoneInvalidoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (EmailInvalidoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (IdadeInvalidaException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (AlunoNaoEncontradoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (AlunoJaExisteException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (StatusInvalidoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (EmailJaExisteException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (TelefoneJaExisteException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (StringInvalidaException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (System.OverflowException)
            {
                Console.WriteLine("o formato inserido é inválido por se tratar de ser muito longo ou muito pequeno para esse valor");
            } catch (MatriculaNaoEncontradaException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        
        
    

        private static void menuAtualizarCliente(IAlunoService alunoService)
        {
            while (true) {
              Console.WriteLine("1 - Editar telefone");
              Console.WriteLine("2 - Editar email");
              Console.WriteLine("3 - Mudar status para ativo");
              Console.WriteLine("4 - Mudar status para inativo");
              Console.WriteLine("5 - Sair do menu de atualização do cliente");
              Console.WriteLine("--------------");

              Console.WriteLine("Qual dessas opções você deseja seguir?");
              int opcao = int.Parse(Console.ReadLine() ?? "0");

              switch (opcao)
              {
                  case 1:
                      Console.WriteLine("Qual o cpf do aluno que você deseja editar o telefone?");
                      string cpfEditarTelefone = Console.ReadLine() ?? string.Empty;

                      Console.WriteLine("qual o novo telefone que você deseja registrar?");
                      string telefoneNovo = Console.ReadLine() ?? string.Empty;

                      alunoService.EditarTelefone(cpfEditarTelefone, telefoneNovo);

                      Console.WriteLine("telefone do aluno " + alunoService.ObterAluno(cpfEditarTelefone)?.NomeAluno + " editado com sucesso no sistema");

                      break;
                  case 2:
                      Console.WriteLine("Qual o cpf do aluno que você deseja editar o email?");
                      string cpfEditarEmail = Console.ReadLine() ?? string.Empty;    

                      Console.WriteLine("Qual o novo email que você deseja registrar?");
                      string emailNovo = Console.ReadLine() ?? string.Empty;

                      alunoService.EditarEmail(cpfEditarEmail, emailNovo);

                      Console.WriteLine("email do aluno " + alunoService.ObterAluno(cpfEditarEmail)?.NomeAluno + " editado com sucesso no sistema");
                      break;

                  case 3: 
                      Console.WriteLine("Qual o cpf do aluno que você deseja mudar o status para ativo?");
                      string cpfMudarStatusAtivo = Console.ReadLine() ?? string.Empty;

                      alunoService.EditarStatusAtivo(cpfMudarStatusAtivo);

                      Console.WriteLine("status do aluno " + alunoService.ObterAluno(cpfMudarStatusAtivo)?.NomeAluno + " editado para ativo com sucesso no sistema");
                      break;
                  case 4: 
                      Console.WriteLine("Qual o cpf do aluno que você deseja mudar o status para inativo?");
                      string cpfMudarStatusInativo = Console.ReadLine() ?? string.Empty;

                      alunoService.EditarStatusInativo(cpfMudarStatusInativo);
                      Console.WriteLine("status do aluno " + alunoService.ObterAluno(cpfMudarStatusInativo)?.NomeAluno + " editado para inativo com sucesso no sistema");
                      break;
                  case 5:
                      return;   
                  default:
                      Console.WriteLine("opção inválida, por favor digite novamente");
                      break;      
              }              
                
            }
        }

        private static async Task menuConsultaCliente(IAlunoService alunoService, ITreinoAlunoService treinoAlunoService, IMatriculaService matriculaService)
        {
            while (true) {
              Console.WriteLine("1 - Buscar aluno");  
              Console.WriteLine("2 - lisar alunos ativos");
              Console.WriteLine("3 - listar alunos inativos");
              Console.WriteLine("4 - listar os treinos de um aluno");
              Console.WriteLine("5 - Listar Alunos com matriculas proximas do vencimento");
              Console.WriteLine("6 - sair do menu de consultas do cliente");
              Console.WriteLine("---------------------------");

              Console.WriteLine("escolha uma dessas opções para prosseguir");
              int opcao = int.Parse(Console.ReadLine() ?? "0");

              switch (opcao)
              {
                  case 1:
                     Console.WriteLine("Qual o cpf do aluno que voce deseja buscar?");
                     string cpfBuscar = Console.ReadLine() ?? string.Empty;

                     alunoService.BuscarAluno(cpfBuscar);
                     break;
                  case 2:
                      var alunosAtivos = await alunoService.listarAlunosAtivos();

                      Console.WriteLine("--- LISTA DOS ALUNOS ATIVOS ---");

                      foreach (var aluno in alunosAtivos)
                      {
                          Console.WriteLine($"Nome: " +aluno.nome);
                          Console.WriteLine($"CPF: " +aluno.cpf);
                          Console.WriteLine($"Telefone: " +aluno.telefone);
                          Console.WriteLine($"Email: " +aluno.email);
                          Console.WriteLine($"Data de Nascimento: " +aluno.nascimento);
                          Console.WriteLine("-----------------------");
                      }

                      break;
                  case 3:
                      var alunosInativos = await alunoService.listarAlunosInativos();

                      Console.WriteLine("--- LISTA DOS ALUNOS INATIVOS ---");

                      foreach (var aluno in alunosInativos)
                      {
                          Console.WriteLine($"Nome: " +aluno.nome);
                          Console.WriteLine($"CPF: " +aluno.cpf);
                          Console.WriteLine($"Telefone: " +aluno.telefone);
                          Console.WriteLine($"Email: " +aluno.email);
                          Console.WriteLine($"Data de Nascimento: " +aluno.nascimento);
                          Console.WriteLine("-----------------------");
                      }
                      break;
                  case 4: 
                      Console.WriteLine("Qual o cpf do aluno que voce listar os treinos?");
                      string cpfTreino = Console.ReadLine() ?? string.Empty; 

                      var treinosDoAluno = await treinoAlunoService.listarTreinosDoAluno(cpfTreino);

                      Console.WriteLine("--- Treinos do aluno " +alunoService.ObterAluno(cpfTreino).NomeAluno+ "---");
                      foreach (var treinos in treinosDoAluno)
                      {
                          Console.WriteLine($"ID DO TREINO: [{treinos.id}]");
                          Console.WriteLine($"NOME DO TREINO: [{treinos.nome}]");
                          Console.WriteLine($"OBJETIVO DO TREINO: [{treinos.objetivo}]");
                          Console.WriteLine($"DURAÇÃO DO TREINO: [{treinos.duracao}]");
                          Console.WriteLine("----------------");
                      }      
                      break;
                  case 5:
                       var alunosProximosVencimento = await matriculaService.ObterMatriculasProximasDoVencimento();

                       Console.WriteLine("--- LISTA DE ALUNOS COM MATRICULA VENCENDO ESSA SEMANA ---");

                       foreach (var alunos in alunosProximosVencimento)
                        {
                            Console.WriteLine("ID DA MATRICULA: " +alunos.id);
                            Console.WriteLine("NOME DO ALUNO: " +alunos.nome);
                            Console.WriteLine("TIPO DE PLANO: " +alunos.plano);
                            Console.WriteLine("DATA DE INICIO: " +alunos.dataInicio);
                            Console.WriteLine("DATA DE VENCIMENTO: " +alunos.dataFim);
                            Console.WriteLine("--------------");
                        }   
                        break;
                  case 6:
                     return;
                  default:
                     Console.WriteLine("Opção inválida, por favor digite novamente");
                     break;   
              }    

            } 
        }

        private static async Task menuPlano(IPlanoService planoService)
        {
           try {
            while (true) {
                Console.WriteLine("--- MENU DE PLANOS ---");
                Console.WriteLine("1 - Cadastrar plano");
                Console.WriteLine("2 - Editar preço do plano");
                Console.WriteLine("3 - Excluir plano");
                Console.WriteLine("4 - Consultar plano");
                Console.WriteLine("5 - Listar plano mais contratado");
                Console.WriteLine("6 - Sair do menu de planos");
                Console.WriteLine("---------------------------");

                Console.WriteLine("escolha uma dessas opções para prosseguir");
                int opcao = int.Parse(Console.ReadLine() ?? "0");

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("Qual o id do plano que você deseja cadastrar?");
                        int idPlano = int.Parse(Console.ReadLine() ?? "0");

                        Console.WriteLine("Qual o nome do plano que você deseja cadastrar?");
                        string nomePlano = Console.ReadLine() ?? string.Empty;

    
                        Console.WriteLine("1 - Mensal");
                        Console.WriteLine("2 - Trimestral");
                        Console.WriteLine("3 - Semestral");
                        Console.WriteLine("4 - Anual");
                        Console.WriteLine("----------------");
                        Console.WriteLine("Qual o tipo do plano que você deseja cadastrar?");
                        int tipoPlano = int.Parse(Console.ReadLine() ?? "0");

                        TiposDePlano tipo = tipoPlano switch
                        {
                            1 => TiposDePlano.Mensal,
                            2 => TiposDePlano.Trimestral,
                            3 => TiposDePlano.Semestral,
                            4 => TiposDePlano.Anual,
                            _ => throw new ArgumentException("Opção inválida, por favor realize a operação novamente")
                        };

                        Console.WriteLine("Qual o valor do plano que você deseja cadastrar?");
                        string valorPlano = Console.ReadLine() ?? string.Empty;

                        if (decimal.TryParse(valorPlano, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out decimal valorPlanoDecimal))
                        {
                            decimal valorConvertido = valorPlanoDecimal;
                            Plano plano = new Plano()
                           {
                            IdPlano = idPlano,
                            NomePlano = nomePlano,
                            TipoPlano = tipo,
                            ValorPlano = valorConvertido
                            };
                            planoService.cadastrarPlano(plano);
                            Console.WriteLine("plano " + plano.NomePlano + " cadastrado com sucesso no sistema");
                        } else
                        {
                            throw new ArgumentException("valor do plano inserido inválido, por favor realize a operação novamente");
                        }
                        break;
                    case 2:
                        Console.WriteLine("Qual o id do plano que você deseja atualizar o preço?");
                        int idPlanoAtualizar = int.Parse(Console.ReadLine() ?? "0");

                        Console.WriteLine("Qual o novo preço do plano?");
                        string novoPrecoStr = Console.ReadLine() ?? string.Empty;

                        if (decimal.TryParse(novoPrecoStr, System.Globalization.NumberStyles.Number, System.Globalization.CultureInfo.CurrentCulture, out decimal novoPreco))
                        {
                            decimal novoPrecoConvertido = novoPreco;
                            planoService.atualizarPreco(idPlanoAtualizar, novoPrecoConvertido);
                            Console.WriteLine("preço do plano " + planoService.obterPlano(idPlanoAtualizar).NomePlano + " atualizado com sucesso para R$" + novoPrecoConvertido);
                        }
                        else
                        {
                            throw new ArgumentException("novo preço do plano inserido inválido, por favor realize a operação novamente");
                        }
                        break;
                    case 3:
                        Console.WriteLine("Qual o id do plano que você deseja remover?");
                        int idPlanoRemover = int.Parse(Console.ReadLine() ?? "0");

                        string nomeDoPlano = planoService.obterPlano(idPlanoRemover).NomePlano;

                        planoService.removerPlano(idPlanoRemover);
                        Console.WriteLine("plano " + nomeDoPlano + " removido com sucesso do sistema");
                        break;
                    case 4:
                        var planos = await planoService.listarPlanos();

                        Console.WriteLine("--- LISTA DE PLANOS DISPONÍVEIS ---");
                        foreach (var item in planos)
                        {
                            Console.WriteLine($"[ID DO PLANO: {item.IdPlano}]");
                            Console.WriteLine($"[NOME DO PLANO: {item.NomePlano}]");
                            Console.WriteLine($"[TIPO DE PLANO: {item.tipoPlano}]");
                            Console.WriteLine($"[VALOR DO PLANO: {item.ValorPlano}]");
                            Console.WriteLine("----------------");
                        }
                        break;
                    case 5: 
                        var planoMaisContratado = await _planoService.listarPlanoMaisContratado();
                        
                        if (planoMaisContratado != null)
                        {
                            Console.WriteLine("o plano mais contratado da academia é o plano " + planoMaisContratado.NomePlano + " do tipo " + planoMaisContratado.TipoPlano + " com o valor de " +planoMaisContratado.ValorPlano);
                        }
                        else
                        {
                            throw new PlanoNaoEncontradoException("Não foi possível listar o plano mais contratado pois não há planos contratados no sistema");
                        }
                        
                         break;
                    case 6:
                        return;
                    default:
                        Console.WriteLine("Opção inválida, por favor digite novamente");
                        break;
            
                }
            }
        } catch (FormatException)
        {
            Console.WriteLine("formato inserido inválido, por favor insira novamente");
        } catch (IdInvalidoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (StringInvalidaException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (ValorInvalidoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (PlanoJaExisteException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (PlanoNaoEncontradoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        
        } catch (System.OverflowException)
        {
                Console.WriteLine("o formato inserido é inválido por se tratar de ser muito longo ou muito pequeno para esse valor");
        }
        }

    private static async Task menuMatricula(IMatriculaService matriculaService)
    {
        try {
            while (true) {
                Console.WriteLine("--- MENU DE MATRÍCULAS ---");
                Console.WriteLine("1 - Cadastrar matrícula");
                Console.WriteLine("2 - Renovar matricula");
                Console.WriteLine("3 - Cancelar matricula");
                Console.WriteLine("4 - Verificar vencimento da matrícula");
                Console.WriteLine("5 - Sair do menu de matrículas");
                Console.WriteLine("---------------------------");

                Console.WriteLine("escolha uma dessas opções para prosseguir");
                int opcao = int.Parse(Console.ReadLine() ?? "0");

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("Qual o id da matrícula que você deseja cadastrar?");
                        int idMatricula = int.Parse(Console.ReadLine() ?? "0");

                        Console.WriteLine("Qual o cpf do aluno que você deseja cadastrar a matrícula?");
                        string cpfAluno = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Qual o id do plano que você deseja cadastrar a matrícula?");
                        int idPlano = int.Parse(Console.ReadLine() ?? "0");

                        Console.WriteLine("Qual a data de início da matrícula? insira no formato dd/mm/yyyy");
                        string dataInicio = Console.ReadLine() ?? string.Empty;

                        matriculaService.CadastrarMatricula(idMatricula, cpfAluno, idPlano, DateOnly.Parse(dataInicio));

                        Console.WriteLine("Matricula do aluno " +_matriculaService.ObterAluno(cpfAluno).NomeAluno+ " foi adicionada no sistema");

                        break;
                    case 2:
                        Console.WriteLine("Qual o Id da matrícula que você deseja renovar:");
                        int id = int.Parse(Console.ReadLine()?? "0");

                        Console.WriteLine("Qual o cpf do aluno que você deseja renovar a matricula?");
                        string cpf = Console.ReadLine() ?? string.Empty;
                    
                         matriculaService.RenovarMatricula(id, cpf);
                         Console.WriteLine("Matrícula do aluno " +matriculaService.ObterAluno(cpf).NomeAluno+ " foi renovada com sucesso.");
                         break;    
                    case 3:
                            Console.WriteLine("Qual o id da matrícula que você deseja cancelar?");
                            int idMatriculaCancelar = int.Parse(Console.ReadLine() ?? "0");

                            Console.WriteLine("Qual o cpf do aluno que você deseja cancelar a matrícula?");
                            string cpfAlunoCancelar = Console.ReadLine() ?? string.Empty;

                            string nomeDoAluno = _matriculaService.ObterAluno(cpfAlunoCancelar).NomeAluno;

                            matriculaService.CancelarMatricula(idMatriculaCancelar, cpfAlunoCancelar);

                            Console.WriteLine("Matriculado aluno " +nomeDoAluno+ " removida do sistema");
                        break;
                    case 4:
                            Console.WriteLine("Qual o id da matrícula que você deseja verificar o vencimento?");
                            int idMatriculaVencimento = int.Parse(Console.ReadLine() ?? "0");

                            Console.WriteLine("Qual o cpf do aluno que você deseja verificar o vencimento da matrícula?");
                            string cpfAlunoVencimento = Console.ReadLine() ?? string.Empty;

                            matriculaService.VerificarVencimento(idMatriculaVencimento, cpfAlunoVencimento);
                            break;   
                    case 5:
                        return;                
                    default:
                        Console.WriteLine("Opção inválida, por favor digite novamente");
                        break;
                }
            }
        } catch (FormatException)
        {
            Console.WriteLine("formato inserido inválido, por favor insira novamente");
        } catch (IdInvalidoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (StringInvalidaException ex)
        {
            Console.WriteLine(ex.Message);
        } 
         catch (MatriculaNaoEncontradaException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (DataInvalidaException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (AlunoNaoEncontradoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (PlanoNaoEncontradoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (MatriculaRepetidaException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (System.OverflowException)
            {
                Console.WriteLine("o formato inserido é inválido por se tratar de ser muito longo ou muito pequeno para esse valor");
            }
    }
    

    private static async Task menuTreino(ITreinoService treinoService)
    {
        try {
            while (true) {
                Console.WriteLine("--- MENU DE TREINOS ---");
                Console.WriteLine("1 - Cadastrar treino");
                Console.WriteLine("2 - Editar treino");
                Console.WriteLine("3 - Excluir treino");
                Console.WriteLine("4 - Sair do menu de treinos");
                Console.WriteLine("---------------------------");

                Console.WriteLine("escolha uma dessas opções para prosseguir");
                int opcao = int.Parse(Console.ReadLine() ?? "0");

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("Qual o id do treino que você deseja cadastrar?");
                        int idTreino = int.Parse(Console.ReadLine() ?? "0");

                        Console.WriteLine("Qual o nome do treino?");
                        string nomeTreino = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Qual o objetivo do treino?");
                        string objetivoTreino = Console.ReadLine() ?? string.Empty;

                        Console.WriteLine("Qual a duração do treino que você deseja cadastrar? insira no formato HH:mm");
                        string duracao = Console.ReadLine() ?? string.Empty;

                        Treino treino = new Treino()
                        {
                            idTreino = idTreino,
                            Nome = nomeTreino,
                            Objetivo = objetivoTreino,
                            duracaoTreino = duracao
                        };

                        treinoService.cadastrarTreino(treino);
                        Console.WriteLine("treino " + treino.Nome + " cadastrado com sucesso no sistema");
                        break;
                    case 2:
                        await menuEditarTreino(treinoService);
                        break;
                    case 3:
                        Console.WriteLine("Qual o id do treino que você deseja remover?");
                        int idTreinoRemover = int.Parse(Console.ReadLine() ?? "0");

                        string nomeDoTreino = treinoService.ObterTreino(idTreinoRemover).Nome;

                        treinoService.deletarTreino(idTreinoRemover);
                        Console.WriteLine("treino " + nomeDoTreino + " removido com sucesso do sistema");
                        break;
                    case 4:
                        return;
                    default:
                        Console.WriteLine("Opção inválida, por favor digite novamente");
                        break;
                }
            }
        } catch (FormatException)
        {
            Console.WriteLine("formato inserido inválido, por favor insira novamente");
        } catch (IdInvalidoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (StringInvalidaException ex)
        {
            Console.WriteLine(ex.Message);
        } 
         catch (TreinoNaoEncontradoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (TreinoRepetidoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (DuracaoTreinoInvalidaException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (System.OverflowException)
            {
                Console.WriteLine("o formato inserido é inválido por se tratar de ser muito longo ou muito pequeno para esse valor");
            }
    }


    private static async Task menuEditarTreino(ITreinoService treinoService)
    {
        while (true) {
            Console.WriteLine("1 - Editar nome do treino");
            Console.WriteLine("2 - Editar objetivo do treino");
            Console.WriteLine("3 - Editar duração do treino");
            Console.WriteLine("4 - Sair do menu de edição de treinos");
            Console.WriteLine("---------------------------");

            Console.WriteLine("escolha uma dessas opções para prosseguir");
            int opcao = int.Parse(Console.ReadLine() ?? "0");

            switch (opcao)
            {
                case 1:
                    Console.WriteLine("Qual o id do treino que você deseja editar o nome?");
                    int idTreinoEditarNome = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("Qual o novo nome do treino?");
                    string novoNomeTreino = Console.ReadLine() ?? string.Empty;

                    treinoService.altNomeTreino(idTreinoEditarNome, novoNomeTreino);
                    Console.WriteLine("nome do treino " + treinoService.ObterTreino(idTreinoEditarNome).Nome + " atualizado com sucesso para " + novoNomeTreino);
                    break;
                case 2:
                    Console.WriteLine("Qual o id do treino que você deseja editar o objetivo?");
                    int idTreinoEditarObjetivo = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("Qual o novo objetivo do treino?");
                    string novoObjetivoTreino = Console.ReadLine() ?? string.Empty;

                    treinoService.altObjetivoTreino(idTreinoEditarObjetivo, novoObjetivoTreino);
                    Console.WriteLine("objetivo do treino " + treinoService.ObterTreino(idTreinoEditarObjetivo).Nome + " atualizado com sucesso para " + novoObjetivoTreino);
                    break;
                case 3:
                    Console.WriteLine("Qual o id do treino que você deseja editar a duração?");
                    int idTreinoEditarDuracao = int.Parse(Console.ReadLine() ?? "0");

                    Console.WriteLine("Qual a nova duração do treino?");
                    string novaDuracaoTreino = Console.ReadLine() ?? string.Empty;

                    treinoService.altDuracaoTreino(idTreinoEditarDuracao, novaDuracaoTreino);
                    Console.WriteLine("duração do treino " + treinoService.ObterTreino(idTreinoEditarDuracao).Nome + " atualizada com sucesso para " + novaDuracaoTreino.ToString());
                    break;
                case 4:
                    return;
                default:
                    Console.WriteLine("Opção inválida, por favor digite novamente");
                    break;
            }
        }
    }

    private static async Task menuExercicio(IExercicioService exercicioService)
    {
        try {
          while (true) {
              Console.WriteLine("1 - Cadastrar exercício");
              Console.WriteLine("2 - Editar exercício");
              Console.WriteLine("3 - Excluir exercício");
              Console.WriteLine("4 - Sair do menu de exercícios");
              Console.WriteLine("---------------------------");

              Console.WriteLine("escolha uma dessas opções para prosseguir");
              int opcao = int.Parse(Console.ReadLine() ?? "0");

              switch (opcao)
              {
                  case 1:
                     Console.WriteLine("Qual o id do exercício que você deseja cadastrar?");
                     int idExercicio = int.Parse(Console.ReadLine() ?? "0");

                     Console.WriteLine("Qual o nome do exercício?");
                     string nomeExercicio = Console.ReadLine() ?? string.Empty;

                     Console.WriteLine("Quantas séries o exercício terá entre 1 até 7?");
                     int seriesExercicio = int.Parse(Console.ReadLine() ?? "0");

                     Console.WriteLine("quantas repetições o exercício terá entre 8 até 12?");
                     int repeticoesExercicio = int.Parse(Console.ReadLine() ?? "0");

                     Console.WriteLine("Qual a duração de desacnso desse treino entre 1 até 10 minutos?");
                     int descansoExercicio = int.Parse(Console.ReadLine() ?? "0");

                       Exercicio exercicio = new Exercicio()
                       {
                          IdExercicio = idExercicio,
                          NomeExercicio = nomeExercicio,
                          Series = seriesExercicio,
                          Repeticoes = repeticoesExercicio,
                          Descanso = descansoExercicio
                       };

                       exercicioService.cadastrarExercicio(exercicio);
                       Console.WriteLine("exercício " + exercicio.NomeExercicio + " cadastrado com sucesso no sistema");
                       break;
                    case 2:
                         await menuEditarExercicio(exercicioService);
                         break;      
                    case 3:
                          Console.WriteLine("Qual o id do exercício que você deseja remover?");
                            int idExercicioRemover = int.Parse(Console.ReadLine() ?? "0");

                            string nomeDoExercicio = exercicioService.obterExercicio(idExercicioRemover).NomeExercicio; 

                            exercicioService.excluirExercicio(idExercicioRemover);
                            Console.WriteLine("exercício " + nomeDoExercicio + " removido com sucesso do sistema");
                       break;
                    case 4:
                       return;
                    default:
                       Console.WriteLine("Opção inválida, por favor digite novamente");
                       break;
              }
          }
        } catch (FormatException)
        {
            Console.WriteLine("formato inserido inválido, por favor insira novamente");
        } catch (IdInvalidoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (StringInvalidaException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (SeriesInvalidaException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (RepeticoesInvalidaException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (TempoDescansoInvalidoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (ExercicioNaoEncontradoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (ExercicioRepetidoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (ExercicioJaExisteException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (System.OverflowException)
            {
                Console.WriteLine("o formato inserido é inválido por se tratar de ser muito longo ou muito pequeno para esse valor");
            }
    }


    private static async Task menuEditarExercicio(IExercicioService exercicioService)
       {
           while (true) {
              Console.WriteLine("1 - Editar número de séries do exercício");
              Console.WriteLine("2 - Editar número de repetições do exercício");
              Console.WriteLine("3 - Editar tempo de descanso do exercício");
              Console.WriteLine("4 - Sair do menu de edição de exercícios");
              Console.WriteLine("---------------------------");

              Console.WriteLine("escolha uma dessas opções para prosseguir");
              int opcao = int.Parse(Console.ReadLine() ?? "0");

              switch (opcao)
              {
                  case 1:
                      Console.WriteLine("Qual o id do exercício que você deseja editar o número de séries?");
                      int idExercicioEditarSeries = int.Parse(Console.ReadLine() ?? "0");

                      Console.WriteLine("Qual o novo número de séries do exercício entre 1 e 7?");
                      int novoNumeroSeries = int.Parse(Console.ReadLine() ?? "0");

                      exercicioService.altSeries(idExercicioEditarSeries, novoNumeroSeries);
                      Console.WriteLine("número de séries do exercício " + exercicioService.obterExercicio(idExercicioEditarSeries).NomeExercicio + " atualizado com sucesso para " + novoNumeroSeries);
                      break;
                  case 2:
                      Console.WriteLine("Qual o id do exercício que você deseja editar o número de repetições?");
                      int idExercicioEditarRepeticoes = int.Parse(Console.ReadLine() ?? "0");

                      Console.WriteLine("Qual o novo número de repetições do exercício entre 8 e 12?");
                      int novoNumeroRepeticoes = int.Parse(Console.ReadLine() ?? "0");

                      exercicioService.altRepeticoes(idExercicioEditarRepeticoes, novoNumeroRepeticoes);
                      Console.WriteLine("número de repetições do exercício " + exercicioService.obterExercicio(idExercicioEditarRepeticoes).NomeExercicio + " atualizado com sucesso para " + novoNumeroRepeticoes);
                      break;
                  case 3:
                      Console.WriteLine("Qual o id do exercício que você deseja editar o tempo de descanso?");
                      int idExercicioEditarDescanso = int.Parse(Console.ReadLine() ?? "0");

                      Console.WriteLine("Qual o novo tempo de descanso do exercício entre 1 e 10?");
                      int novoTempoDescanso = int.Parse(Console.ReadLine() ?? "0");

                      exercicioService.altDescanso(idExercicioEditarDescanso, novoTempoDescanso);
                      Console.WriteLine("tempo de descanso do exercício " + exercicioService.obterExercicio(idExercicioEditarDescanso).NomeExercicio + " atualizado com sucesso para " + novoTempoDescanso);
                      break;
                  case 4:
                      return;
                  default:
                      Console.WriteLine("Opção inválida, por favor digite novamente");
                      break;
              }
            }
       }

       private static async Task menuTreinoAluno(ITreinoAlunoService treinoAlunoService)
       {
        try {
           while (true) {
              Console.WriteLine("1 - Cadastrar treino do aluno");
              Console.WriteLine("2 - Excluir treino do aluno");
              Console.WriteLine("3 - Sair do menu de treinos do aluno");
              Console.WriteLine("---------------------------");

              Console.WriteLine("escolha uma dessas opções para prosseguir");
              int opcao = int.Parse(Console.ReadLine() ?? "0");

              switch (opcao)
              {
                  case 1:
                      Console.WriteLine("Qual o id do treino que você deseja cadastrar para o aluno?");
                      int idTreino = int.Parse(Console.ReadLine() ?? "0");

                      Console.WriteLine("Qual o cpf do aluno que você deseja cadastrar o treino?");
                      string cpfAluno = Console.ReadLine() ?? string.Empty;

                        TreinoAluno treinoAluno = new TreinoAluno()
                        {
                            IdTreino = idTreino,
                            CpfAluno = cpfAluno
                        };

                        treinoAlunoService.cadastrarTreinoAluno(treinoAluno);
                        Console.WriteLine("treino " + treinoAlunoService.obterTreino(idTreino).Nome + " cadastrado com sucesso para o aluno " + treinoAlunoService.obterAluno(cpfAluno).NomeAluno);
                      break;
                  case 2:
                      Console.WriteLine("Qual o id do treino que você deseja excluir do aluno?");
                      int idTreinoExcluir = int.Parse(Console.ReadLine() ?? "0");

                      Console.WriteLine("Qual o cpf do aluno que você deseja excluir o treino?");
                      string cpfAlunoExcluir = Console.ReadLine() ?? string.Empty;

                      string nomeDoTreino = treinoAlunoService.obterTreino(idTreinoExcluir).Nome;

                      string nomeDoAluno = treinoAlunoService.obterAluno(cpfAlunoExcluir).NomeAluno;

                      treinoAlunoService.deletarTreinoAluno(idTreinoExcluir, cpfAlunoExcluir);
                      Console.WriteLine("treino " + nomeDoTreino  + " excluído com sucesso do aluno " + nomeDoAluno);
                      break;
                  case 3:
                      return;
                  default:
                      Console.WriteLine("Opção inválida, por favor digite novamente");
                      break;
              }
            }
        } catch (FormatException)
            {
                Console.WriteLine("formato inserido inválido, por favor insira novamente");
            } catch (IdInvalidoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (StringInvalidaException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (TreinoNaoEncontradoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (AlunoNaoEncontradoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (TreinoAlunoRepetidoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (TreinoAlunoNaoEncontradoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (CpfInvalidoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (AlunoRepetidoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (System.OverflowException)
            {
                Console.WriteLine("o formato inserido é inválido por se tratar de ser muito longo ou muito pequeno para esse valor");
            } catch (AlunoInativoException ex)
            {
                Console.WriteLine(ex.Message);
            }
    
       }

       private static async Task menuExercicioTreino(ITreinoExercicioService exercicioTreinoService)
       {
        try {
           while (true) {
              Console.WriteLine("1 - Cadastrar exercício do treino");
              Console.WriteLine("2 - Excluir exercício do treino");
              Console.WriteLine("3 - Listar os exercicios de um treino");
              Console.WriteLine("4 - Sair do menu de exercícios do treino");
              Console.WriteLine("---------------------------");

              Console.WriteLine("escolha uma dessas opções para prosseguir");
              int opcao = int.Parse(Console.ReadLine() ?? "0");

              switch (opcao)
              {
                  case 1:
                      Console.WriteLine("Qual o id do treino que você deseja cadastrar o exercício?");
                      int idTreino = int.Parse(Console.ReadLine() ?? "0");

                      Console.WriteLine("Qual o id do exercício que você deseja cadastrar no treino?");
                      int idExercicio = int.Parse(Console.ReadLine() ?? "0");

                        TreinoExercicio exercicioTreino = new TreinoExercicio()
                        {
                            IdTreino = idTreino,
                            IdExercicio = idExercicio
                        };

                        exercicioTreinoService.cadastrarTreinoExercicio(exercicioTreino);
                        Console.WriteLine("exercício " + exercicioTreinoService.obterExercicio(idExercicio).NomeExercicio + " cadastrado com sucesso para o treino " + exercicioTreinoService.obterTreino(idTreino).Nome);
                      break;
                  case 2:
                      Console.WriteLine("Qual o id do treino que você deseja excluir o exercício?");
                      int idTreinoExcluir = int.Parse(Console.ReadLine() ?? "0");

                      Console.WriteLine("Qual o id do exercício que você deseja excluir do treino?");
                      int idExercicioExcluir = int.Parse(Console.ReadLine() ?? "0");

                      var nomeExercicio = exercicioTreinoService.obterExercicio(idExercicioExcluir).NomeExercicio;

                      var nometreino = exercicioTreinoService.obterTreino(idTreinoExcluir).Nome;

                        exercicioTreinoService.deletarTreinoExercicio(idTreinoExcluir, idExercicioExcluir);
                        Console.WriteLine("exercício " + nomeExercicio  + " excluído com sucesso do treino " + nometreino);
                        break;
                  case 3:
                          Console.WriteLine("Qual o id do treino que você deseja listar os exercicios?");
                          int idDoTreino = int.Parse(Console.ReadLine() ?? "0");

                          var exerciciosDoTreino = await _treinoExercicioService.listarExerciciosDoTreino(idDoTreino);

                          Console.WriteLine("---- LISTA DE EXERCICIOS DO TREINO " +_treinoExercicioService.obterTreino(idDoTreino).Nome+ " ----");
                          foreach(var exercicios in exerciciosDoTreino)
                            {
                                Console.WriteLine("ID DO EXERCICIO: " +exercicios.idExercicio);
                                Console.WriteLine("NOME DO EXERCICIO: " +exercicios.nomeExercicio);
                                Console.WriteLine("QUANTIDADE DE SERIES DO EXERCICIO: " +exercicios.series);
                                Console.WriteLine("QUANTIDADE DE REPETIÇÕES DO EXERCICIO: " +exercicios.repeticoes);
                                Console.WriteLine("QUANTIDADE DE MINUTOS DE DESCANSO DO EXERCICIO: " +exercicios.descanso);
                                Console.WriteLine("---------------------------");
                            } 
                         break;      
                  case 4:
                      return;
                  default:
                      Console.WriteLine("Opção inválida, por favor digite novamente");
                      break;
              }
            }
        } catch (FormatException)
        {
            Console.WriteLine("formato inserido inválido, por favor insira novamente");
        } catch (IdInvalidoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (TreinoNaoEncontradoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (ExercicioNaoEncontradoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (TreinoExercicioNaoEncontradoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (TreinoExercicioJaExisteExceptionException ex)
        {
            Console.WriteLine(ex.Message);
        } catch(ExercicioRepetidoException ex)
        {
            Console.WriteLine(ex.Message);
        } catch (System.OverflowException)
            {
                Console.WriteLine("o formato inserido é inválido por se tratar de ser muito longo ou muito pequeno para esse valor");
            }
    }

    private static async Task MenuPagamentos(IPagamentoService pagamentoService, IMatriculaService matriculaService, IPagamentoRepository pagamentoRepository, IMatriculaRepository matriculaRepository)
        {
            try
            {
                while (true)
                {
                    Console.WriteLine("--- MENU DE PAGAMENTOS ---");
                    Console.WriteLine("1 - Realizar pagamento");
                    Console.WriteLine("2 - Cancelar pagamento");
                    Console.WriteLine("3 - Listar pagamentos atrasados");
                    Console.WriteLine("4 - Listar pagamentos realizados");
                    Console.WriteLine("5 - Listar pagamentos pendentes");
                    Console.WriteLine("6 - Calcular o faturamento mensal de um mês específico");
                    Console.WriteLine("7 - Sair do menu de pagamentos");
                    Console.WriteLine("--------------------");

                    Console.WriteLine("Qual dessas operações você deseja realizar?");
                    int opcao = int.Parse(Console.ReadLine() ?? "0");

                    switch (opcao)
                    {
                        case 1:
                            Console.WriteLine("Qual o Id do pagamento que você deseja registrar:");
                            int id = int.Parse(Console.ReadLine() ?? "0");
                            
                            Console.WriteLine("Qual o Id da matrícula que você deseja registrar o pagamento:");
                            int idMatricula = int.Parse(Console.ReadLine() ?? "0");
                            
                            Matricula matricula = matriculaService.ObterMatricula(idMatricula);
                            pagamentoService.realizarPagamento(id, matricula);
                            await AtualizarPagamentos(matriculaRepository, pagamentoRepository);

                            Console.WriteLine("Pagamento registrado com sucesso.");

                            break;
                        case 2:
                            Console.WriteLine("qual o id do pagamento que você deseja cancelar?");
                            int idPagamento = int.Parse(Console.ReadLine() ?? "0");

                            string nomeDoAluno = pagamentoService.obterPagamento(idPagamento).Matricula.Aluno.NomeAluno;

                            DateOnly? dataPagamento = pagamentoService.obterPagamento(idPagamento).DataPagamento;

                            pagamentoService.cancelarPagamento(idPagamento);
                             await AtualizarPagamentos(matriculaRepository, pagamentoRepository);
                            Console.WriteLine("Pagamento do aluno " + nomeDoAluno + " realizado no dia " + (dataPagamento?.ToString() ?? "N/A") + " foi cancelada");
                            break;
                        case 3:
                            var matriculasVencidas = await matriculaService.listarMatriculasVencidas();

                            Console.WriteLine("--- LISTA DE PAGAMENTOS ATRASADOS ---");
                            foreach (var item in matriculasVencidas)
                           {
                            Console.WriteLine($"[NOME DO ALUNO: {item.nomeAluno}]");
                            Console.WriteLine($"[NOME DO PLANO: {item.nomePlano}]");
                            Console.WriteLine($"[DATA DE VENCIMENTO: {item.dataVencimento}]");
                            Console.WriteLine($"[SITUAÇÃO DO PAGAMENTO: {item.SituacaoPagamento}]");
                            Console.WriteLine("----------------");
                           }
                            break; 
                        case 4:
                            var matriculasAtivas = await matriculaService.listarMatriculasAtivas();

                            Console.WriteLine("--- LISTA DE PAGAMENTOS REALIZADOS ---");
                            foreach (var item in matriculasAtivas)
                           {
                            Console.WriteLine($"[NOME DO ALUNO: {item.nomeAluno}]");
                            Console.WriteLine($"[NOME DO PLANO: {item.nomePlano}]");
                            Console.WriteLine($"[DATA DE VENCIMENTO: {item.dataVencimento}]");
                            Console.WriteLine($"[SITUAÇÃO DO PAGAMENTO: {item.SituacaoPagamento}]");
                            Console.WriteLine("----------------");
                           }
                            break; 
                        case 5:
                              var matriculasPendentes = await matriculaService.listarMatriculasPendentes();

                              Console.WriteLine("--- LISTA DE PAGAMENTOS PENDENTES ---");
                              foreach (var pendente in matriculasPendentes)
                            {
                            Console.WriteLine($"[NOME DO ALUNO: {pendente.nomeAluno}]");
                            Console.WriteLine($"[NOME DO PLANO: {pendente.nomePlano}]");
                            Console.WriteLine($"[DATA DE VENCIMENTO: {pendente.dataVencimento}]");
                            Console.WriteLine($"[SITUAÇÃO DO PAGAMENTO: {pendente.SituacaoPagamento}]");
                            Console.WriteLine("----------------");
                            }    
                            break;
                        case 6:
                            Console.WriteLine("qual o mês que você deseja listar o faturamento mensal? insira entre mês 1 até o mês 12");
                            int mes = int.Parse(Console.ReadLine() ?? "0");

                            Console.WriteLine("qual o ano que você deseja listar esse faturamento mensal?"); 
                            int ano = int.Parse(Console.ReadLine() ?? "0");
                            
                            Console.WriteLine("o faturamento mensal do mês " +mes+ " do ano de " +ano+ " foi de R$:" +await pagamentoService.exibirFaturamentoMensal(mes, ano));
                            
                            break;    
                        case 7:
                            return;
                        default:
                            Console.WriteLine("opção inválida, por favor digite novamente");
                            break;       

                    }

                }
            } catch (FormatException)
            {
                Console.WriteLine("formato inserido inválido, por favor digite novamente");
            } catch (IdInvalidoException ex) {
                Console.WriteLine(ex.Message);
            } catch (DataInvalidaException ex) {
                Console.WriteLine(ex.Message);
            } catch (PagamentoInvalidoException ex) {
                Console.WriteLine(ex.Message);
            } catch (MatriculaNaoEncontradaException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (AlunoNaoEncontradoException ex)
            {
                Console.WriteLine(ex.Message);
            } catch (System.OverflowException)
            {
                Console.WriteLine("o formato inserido é inválido por se tratar de ser muito longo ou muito pequeno para esse valor");
            }
        }

      private static async Task AtualizarPagamentos(IMatriculaRepository matriculaRepository, IPagamentoRepository pagamentoRepository)
      {
        var matriculas = await matriculaRepository.ObterTodas();
        DateOnly hoje = DateOnly.FromDateTime(DateTime.Today);
        
        int mes = hoje.Month;
        int ano = hoje.Year;
        
        foreach (var matricula in matriculas)
        {
            bool pagamentoRealizado = await pagamentoRepository.ExistePagamentoMes(matricula.idMatricula, mes, ano);
            if (pagamentoRealizado)
            {
                matricula.SituacaoPagamento = SituacaoPagamento.Paga;
            }
            else if (hoje > matricula.DataVencimento)
            {
                matricula.SituacaoPagamento = SituacaoPagamento.Atrasada;
            }
            else
            {
                matricula.SituacaoPagamento = SituacaoPagamento.Pendente;
            }
        }
        matriculaRepository.atualizarSituacaoTodos();
      }
    }
}

    

    
    
    

    
    




