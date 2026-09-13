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
                    if (WindowsDefender.VerificarStatus())
                        WindowsDefender.DesativarDefender();
                    else
                        WindowsDefender.AtivarDefender();
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
                    if (WindowsUpdate.VerificarStatus())
                        WindowsUpdate.DesativarUpdate();
                    else
                        WindowsUpdate.AtivarUpdate();
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
                    Otimizacao.OtimizacaoCompleta();
                    break;

                case 2:
                    Otimizacao.RestaurarServicos();
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
                    Energia.AtivarDesempenhoMaximo();
                    break;

                case 2:
                    Energia.AtivarAltoDesempenho();
                    break;

                case 3:
                    Energia.AtivarEquilibrado();
                    break;

                case 4:
                    Energia.AtivarEconomia();
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
                    Drivers.VerificarDrivers();
                    break;

                case 2:
                    Drivers.InstalarNvidia();
                    break;

                case 3:
                    Drivers.InstalarAmdGpu();
                    break;

                case 4:
                    Drivers.InstalarAmdProcessador();
                    break;

                case 5:
                    Drivers.VerificarAtualizacoes();
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
        int op;

        do
        {
            Console.Clear();
            Menu.ExibirSubMenuLimpeza();

            op = Validacao.LerNumero("Digite uma Opção: ");

            switch (op)
            {
                case 1:
                    LimpezaSistema.LimparLixeira();
                    break;

                case 2:
                    LimpezaSistema.LimparTemporarios();
                    break;

                case 3:
                    LimpezaSistema.LimparCleanmgr();
                    break;

                case 4:
                    LimpezaSistema.LimparDNS();
                    break;

                case 5:
                    LimpezaSistema.LimparDownloadsWindowsUpdate();
                    break;

                case 6:
                    LimpezaSistema.LimparMiniaturas();
                    break;

                case 7:
                    LimpezaSistema.LimparRelatoriosErros();
                    break;

                case 8:
                    LimpezaSistema.Limpar();
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