using System;
class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        int op;
        do
        {
            Menu.ExibirMenuPrincipal();
            op = Validacao.LerNumero("Digite uma Opção: ");
            switch (op)
            {
                case 0:
                    Console.Write("Tem certeza que quer sair? (s/n): ");
                    string resposta = Console.ReadLine() ?? "";
                    if (resposta == "s")
                    {
                        Console.WriteLine("Fechando o programa...");
                    }
                    else
                    {
                        op = -1;
                    }
                    break;
                case 1:
                    SubMenuAtivacao();
                    break;
                case 2:
                    SubMenuDefender();
                    break;
                case 3:
                    SubMenuUpdate();
                    break;
                case 4:

                    break;
                case 5:

                    break;
                case 6:
                    SubMenuOtimizacao();
                    break;
                case 7:
                    SubMenuEnergia();
                    break;
                case 8:
                    SubMenuDrivers();
                    break;
                case 9:
                    SubMenuInformacoes();
                    break;
                case 10:
                    SubMenuLimpeza();
                    break;

                default:
                    Console.WriteLine("Opção inválida");
                    break;
            }

        } while (op != 0);
    }

    static void SubMenuAtivacao()
    {
        int op;
        do
        {
            Console.Clear();
            Menu.ExibirSubMenuAtivacao();
            op = Validacao.LerNumero("Digite uma Opção: ");

            switch (op)
            {
                case 1:
                    AtivacaoWindows.ExecutarAtivacao();
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (op != 0)
                Console.ReadKey();

        } while (op != 0);
    }

    static void SubMenuDefender()
    {
        int op;
        do
        {
            Console.Clear();
            Menu.ExibirSubMenuDefender();
            op = Validacao.LerNumero("Digite uma Opção: ");

            switch (op)
            {
                case 1:
                    // Funcao pra ativar o Defender
                    break;

                case 2:
                    // Funcao pra desativar Defender
                    break;

                case 3:
                    // Funcao pra verificar Defender
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (op != 0)
                Console.ReadKey();

        } while (op != 0);
    }

    static void SubMenuUpdate()
    {
        int op;
        do
        {
            Console.Clear();
            Menu.ExibirSubMenuUpdate();
            op = Validacao.LerNumero("Digite uma Opção: ");

            switch (op)
            {
                case 1:
                    // Funcao pra ativar o Update
                    break;

                case 2:
                    // Funcao pra desativar Update
                    break;

                case 3:
                    // Funcao pra verificar Update
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (op != 0)
                Console.ReadKey();

        } while (op != 0);
    }

    static void SubMenuOtimizacao()
    {
        int op;
        do
        {
            Console.Clear();
            Menu.ExibirSubMenuOtimizacao();
            op = Validacao.LerNumero("Digite uma Opção: ");

            switch (op)
            {
                case 1:
                    // Funcao pra otimizar
                    break;

                case 2:
                    // Funcao pra desativar programas desnecessarios
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (op != 0)
                Console.ReadKey();

        } while (op != 0);
    }

    static void SubMenuEnergia()
    {
        int op;
        do
        {
            Console.Clear();
            Menu.ExibirSubMenuEnergia();
            op = Validacao.LerNumero("Digite uma Opção: ");

            switch (op)
            {
                case 1:
                    // Funcao pra ativar Desempenho Maximo
                    break;

                case 2:
                    // Funcao pra ativar Alto Desempenho
                    break;

                case 3:
                    // Funcao pra ativar Equilibrado
                    break;

                case 4:
                    // Funcao pra ativar Economia
                    break;

                case 5:
                    // Funcao pra verificar plano
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (op != 0)
                Console.ReadKey();

        } while (op != 0);
    }

    static void SubMenuDrivers()
    {
        int op;
        do
        {
            Console.Clear();
            Menu.ExibirSubMenuDrivers();
            op = Validacao.LerNumero("Digite uma Opção: ");

            switch (op)
            {
                case 1:
                    // Funcao pra instalar driver nvidia
                    break;

                case 2:
                    // Funcao pra instalar driver amd gpu
                    break;

                case 3:
                    // Funcao pra instalar driver amd cpu
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (op != 0)
                Console.ReadKey();
        } while (op != 0);
    }

    static void SubMenuInformacoes()
    {
        int op;
        do
        {
            Console.Clear();
            Menu.ExibirSubMenuInformacoes();
            op = Validacao.LerNumero("Digite uma Opção: ");

            switch (op)
            {
                case 1:
                    InformacoesPC.ExibirInformacoesCompletas();
                    break;

                case 2:
                    InformacoesPC.ExibirInformacoesBasicas();
                    break;

                case 3:
                    InformacoesPC.ExibirInformacoesCPU();
                    break;

                case 4:
                    InformacoesPC.ExibirInformacoesGPU();
                    break;

                case 5:
                    InformacoesPC.ExibirInformacoesRAM();
                    break;

                case 6:
                    InformacoesPC.ExibirInformacoesArmazenamento();
                    break;
                
                case 7:
                    InformacoesPC.ExibirInformacoesWindows();
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (op != 0)
                Console.ReadKey();

        } while (op != 0);
    }

    static void SubMenuLimpeza()
    {
        Menu.ExibirSubMenuLimpeza();
        int op;
        do
        {
            Console.Clear();
            Menu.ExibirSubMenuEnergia();
            op = Validacao.LerNumero("Digite uma Opção: ");

            switch (op)
            {
                case 1:
                    // Funcao pra Esvaziar Lixeira 
                    break;

                case 2:
                    // Funcao pra Apagar Arquivos Temporários
                    break;

                case 3:
                    // Funcao pra Limpar Cache do Windows
                    break;

                case 4:
                    // Funcao pra Limpar Cache de DNS
                    break;

                case 5:
                    // Funcao pra Limpar Arquivos de Atualização do Windows
                    break;

                case 6:
                    // Funcao pra Limpar Miniaturas
                    break;

                case 7:
                    // Funcao pra Limpar Relatórios de Erros
                    break;

                case 8:
                    // Funcao pra Limpeza Completa Segura
                    break;

                case 0:
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (op != 0)
                Console.ReadKey();

        } while (op != 0);
    }
}