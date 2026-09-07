using System;

class Validacao
{
    public static int LerNumero(string mensagem)
    {
        int valor;
        Console.Write(mensagem);
        while (!int.TryParse(Console.ReadLine(), out valor))
        {
            Console.WriteLine("Valor inválido! Digite apenas números.");
            Console.Write(mensagem);
        }
        return valor;
    }
}