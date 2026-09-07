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
        } while (op != 0);
    }
}