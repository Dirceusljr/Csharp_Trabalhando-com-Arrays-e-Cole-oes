using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.byteBank.Exception;

namespace bytebank_ATENDIMENTO.bytebank.Atendimento
{
    internal class ByteBankAtendimento
    {
#nullable disable
        private List<ContaCorrente> _listaDeContas = new List<ContaCorrente>() {
            new ContaCorrente(95, "123456-X") {Saldo=100, Titular = new Cliente{Cpf="11111",Nome="Henrique",Profissao="Dev"} },
            new ContaCorrente(95, "951258-X") {Saldo=200, Titular = new Cliente{Cpf="22222",Nome="Pedro",Profissao="Engenheiro" } },
            new ContaCorrente(94, "987321-W") {Saldo=60, Titular = new Cliente{Cpf="33333",Nome="Marisa",Profissao="Dev" } }
        };

        public void AtendimentoCliente()
        {
            try
            {
                char opcao = '0';
                while (opcao != '6')
                {
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("===       Atendimento       ===");
                    Console.WriteLine("===1 - Cadastrar Conta      ===");
                    Console.WriteLine("===2 - Listar Contas        ===");
                    Console.WriteLine("===3 - Remover Conta        ===");
                    Console.WriteLine("===4 - Ordenar Conta        ===");
                    Console.WriteLine("===5 - Pesquisar Conta      ===");
                    Console.WriteLine("===6 - Sair do Sistema      ===");
                    Console.WriteLine("===============================");
                    Console.WriteLine("\n\n");
                    Console.Write("Digite a opção desejada: ");
                    try
                    {
                        opcao = Console.ReadLine()[0];
                    }
                    catch (Exception excecao)
                    {
                        throw new ByteBankException(excecao.Message);
                    }

                    switch (opcao)
                    {
                        case '1':
                            CadastrarConta();
                            break;
                        case '2':
                            ListarContas();
                            break;
                        case '3':
                            RemoverContas();
                            break;
                        case '4':
                            OrdenarContas();
                            break;
                        case '5':
                            PesquisarContas();
                            break;
                        case '6':
                            EncerrarAplicacao();
                            break;
                        default:
                            Console.WriteLine("Opção não implementada.");
                            break;
                    }
                }

            }
            catch (ByteBankException excecao)
            {
                Console.WriteLine($"{excecao.Message}");
            };
        }

        private void EncerrarAplicacao()
        {
            Console.WriteLine("... Encerrando a aplicação ...");
            Console.ReadKey();
        }

        private void OrdenarContas()
        {
            _listaDeContas.Sort();
            Console.WriteLine("... Lista de contas ordenadas ...");
            Console.ReadKey();
        }

        private void RemoverContas()
        {
            Console.Clear();
            Console.WriteLine("==============================");
            Console.WriteLine("===     REMOVER CONTAS     ===");
            Console.WriteLine("==============================");
            Console.WriteLine("\n");
            Console.Write("Informe o número da Conta: ");
            string numeroConta = Console.ReadLine();
            ContaCorrente conta = null;
            foreach (var item in _listaDeContas)
            {
                if (item.Conta.Equals(numeroConta))
                {
                    conta = item;
                }
            }
            if (conta != null)
            {
                _listaDeContas.Remove(conta);
                Console.WriteLine("... Conta removida da lista! ...");
            }
            else
            {
                Console.WriteLine("... Conta para remoção não encontrada ...");
            }
            Console.ReadKey();

        }

        private void ListarContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===     LISTA DE CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            if (_listaDeContas.Count <= 0)
            {
                Console.WriteLine("... Não há contas cadastradas! ...");
                Console.ReadKey();
                return;
            }
            foreach (ContaCorrente item in _listaDeContas)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                Console.ReadKey();
            }
        }

        private void CadastrarConta()
        {
            Console.Clear();
            Console.WriteLine("==============================");
            Console.WriteLine("===   CADASTRO DE CONTAS   ===");
            Console.WriteLine("==============================");
            Console.WriteLine("\n");
            Console.WriteLine("=== Informe dados da conta ===");

            Console.Write("Número da agência: ");
            int numeroAgencia = int.Parse(Console.ReadLine());

            ContaCorrente conta = new ContaCorrente(numeroAgencia);
            Console.WriteLine($"Número da conta nova [NOVA] : {conta.Conta}");

            Console.Write("Informe o saldo incial: ");
            conta.Saldo = double.Parse(Console.ReadLine());

            Console.Write("Informe nome do Titular: ");
            conta.Titular.Nome = Console.ReadLine();

            Console.Write("Informe CPF do Titular: ");
            conta.Titular.Cpf = Console.ReadLine();

            Console.Write("Informe Profissão do Titular: ");
            conta.Titular.Profissao = Console.ReadLine();

            _listaDeContas.Add(conta);
            Console.WriteLine("... Conta cadastrada com sucesso! ...");
            Console.ReadKey();
        }

        private void PesquisarContas()
        {
            Console.Clear();
            Console.WriteLine("================================");
            Console.WriteLine("===     PESQUISAR CONTAS     ===");
            Console.WriteLine("================================");
            Console.WriteLine("\n");
            Console.Write("Deseja pesquisar por (1) NÚMERO DA CONTA (2) CPF TITULAR (3) NÚMERO DA AGÊNCIA? ");
            switch (int.Parse(Console.ReadLine()))
            {
                case 1:
                    {
                        Console.Write("Informe o número da Conta: ");
                        string _numeroDaConta = Console.ReadLine();
                        ContaCorrente consultaConta = ConsultaPorNumeroDaConta(_numeroDaConta);
                        Console.WriteLine(consultaConta.ToString());
                        Console.ReadKey();
                        break;
                    }
                case 2:
                    {
                        Console.Write("Informe o CPF do Titular: ");
                        string _cpf = Console.ReadLine();
                        ContaCorrente consultaConta = ConsultaPorCpfTitular(_cpf);
                        Console.WriteLine(consultaConta.ToString());
                        Console.ReadKey();
                        break;
                    }
                case 3:
                    {
                        Console.Write("Informe o número da Agência da Conta: ");
                        int _numeroDaAgencia = int.Parse(Console.ReadLine());
                        var consultaPorAgencia = ConsultaPorNumeroDaAgencia(_numeroDaAgencia);
                        ExibirListaDeContas(consultaPorAgencia);
                        Console.ReadKey();
                        break;
                    }
                default:
                    Console.WriteLine("Opção não implementada.");
                    break;

            };




        }

        private void ExibirListaDeContas(List<ContaCorrente> consultaPorAgencia)
        {
            if (consultaPorAgencia == null)
            {
                Console.WriteLine("... A consulta não retornou dados ...");
            }
            else
            {
                foreach (var item in consultaPorAgencia)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }

        private List<ContaCorrente> ConsultaPorNumeroDaAgencia(int numeroDaAgencia)
        {
            var consulta = (
                from conta in _listaDeContas
                where conta.Numero_agencia == numeroDaAgencia
                select conta).ToList();
            return consulta;
        }

        private ContaCorrente ConsultaPorCpfTitular(string? cpf)
        {
            return _listaDeContas.Where(conta => conta.Titular.Cpf == cpf).FirstOrDefault();
        }

            
        private ContaCorrente ConsultaPorNumeroDaConta(string? numeroDaConta)
        {
            return _listaDeContas.Where(conta => conta.Conta == numeroDaConta).FirstOrDefault();
        }
    }
}

    





