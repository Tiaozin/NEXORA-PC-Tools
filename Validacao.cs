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

    public static string LerTexto(string mensagem)
    {
        Console.Write(mensagem);
        string entrada = Console.ReadLine() ?? "";
        while (string.IsNullOrWhiteSpace(entrada))
        {
            Console.WriteLine("Esse campo é obrigatório!");
            Console.Write(mensagem);
            entrada = Console.ReadLine() ?? "";
        }
        return entrada;
    }
}