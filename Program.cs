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

            Console.ReadKey();

        } while (op != 0);
    }

    static void SubMenuAtivacao()
    {
        Menu.ExibirSubMenuAtivacao();
    }

    static void SubMenuDefender()
    {
        Menu.ExibirSubMenuDefender();
    }

    static void SubMenuUpdate()
    {
        Menu.ExibirSubMenuUpdate();
    }

    static void SubMenuOtimizacao()
    {
        Menu.ExibirSubMenuOtimizacao();
    }

    static void SubMenuEnergia()
    {
        Menu.ExibirSubMenuEnergia();
    }

    static void SubMenuDrivers()
    {
        Menu.ExibirSubMenuDrivers();
    }

    static void SubMenuInformacoes()
    {
        Menu.ExibirSubMenuInformacoes();
    }

    static void SubMenuLimpeza()
    {
        Menu.ExibirSubMenuLimpeza();
    }
}