using System;
using System.ComponentModel.Design;
using System.Management;
class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        int op;
        do
        {
            Menu.ExibirMenuPrincipal();

            int.TryParse(Console.ReadLine(), out op);
            if (op == 9)
            {
                Menu.ExibirSubMenuInformacoes();

                int.TryParse(Console.ReadLine(), out op);
                switch (op)
                {
                    case 1:
                        InformacoesPC.ExibirInformacoesCompletas();
                        break;
                    case 2:
                        InformacoesPC.ExibirInformacoesCompletas();
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
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }
            }
            Console.ReadKey();

        } while (op != 0);
    }
}