using System;
using System.ComponentModel.Design;

class Program
{
    static void Main()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        int op = 0;
        do
        {
            Menu.ExibirMenuPrincipal();
            Menu.ExibirSubMenuAtivacao();
            Menu.ExibirSubMenuDefender();
            Menu.ExibirSubMenuUpdate();
            // Menu.ExibirSubMenuOtimizacao();
        } while (op != 0);
    }
}