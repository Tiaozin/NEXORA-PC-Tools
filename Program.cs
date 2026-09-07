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
            InformacoesPC.ExibirInformacoesBasicas();
        } while (op != 0);
    }
}