using System;

// Classe responsável por padronizar todo o layout visual do NEXORA:
// cabeçalhos, rodapés, linhas de conteúdo e menus.
// Nenhuma lógica de negócio deve ficar aqui, apenas exibição.
static class UI
{
    // Largura interna da caixa (não conta os cantos ╔ ╗ ╚ ╝)
    private const int Largura = 59;

    // Desenha o topo da caixa + o caminho (breadcrumb) centralizado
    public static void Cabecalho(string caminho)
    {
        Console.WriteLine("╔" + new string('═', Largura) + "╗");
        LinhaCentralizada(caminho);
        Console.WriteLine("╠" + new string('═', Largura) + "╣");
        LinhaVazia();
    }

    // Fecha a caixa (usar sempre no final de uma tela)
    public static void Rodape()
    {
        LinhaVazia();
        Console.WriteLine("╚" + new string('═', Largura) + "╝");
    }

    // Separador interno, usado para dividir seções dentro da mesma caixa
    public static void Separador()
    {
        Console.WriteLine("╠" + new string('═', Largura) + "╣");
    }

    // Linha de conteúdo alinhada à esquerda
    public static void Linha(string texto = "")
    {
        string conteudo = " " + texto;

        if (conteudo.Length > Largura)
            conteudo = conteudo.Substring(0, Largura);

        Console.WriteLine("║" + conteudo.PadRight(Largura) + "║");
    }

    // Linha em branco dentro da caixa
    public static void LinhaVazia() => Linha("");

    // Linha de texto centralizado (usada no título do cabeçalho)
    public static void LinhaCentralizada(string texto)
    {
        if (texto.Length > Largura)
            texto = texto.Substring(0, Largura);

        int espacos = Largura - texto.Length;
        int esquerda = espacos / 2;
        int direita = espacos - esquerda;

        Console.WriteLine("║" + new string(' ', esquerda) + texto + new string(' ', direita) + "║");
    }

    // Item de menu numerado, ex: "1. Otimização completa"
    public static void Item(string numero, string texto) => Linha($"{numero}. {texto}");

    // Ajusta o tamanho da janela do console para caber apenas o conteúdo,
    // em vez de abrir com o terminal inteiro/maximizado.
    public static void ConfigurarJanela()
    {
        try
        {
            int largura = Largura + 4;   // caixa + pequena margem
            int altura = 40;             // suficiente para as telas mais longas (ex: Informações Completas)

            if (OperatingSystem.IsWindows())
            {
                largura = Math.Min(largura, Console.LargestWindowWidth);
                altura = Math.Min(altura, Console.LargestWindowHeight);

                Console.SetWindowSize(largura, altura);
                Console.SetBufferSize(largura, altura);
            }

            Console.Title = "NEXORA PC Tools";
        }
        catch
        {
            // Caso o terminal não suporte redimensionamento (ex: alguns terminais
            // integrados de IDE), o programa continua normalmente no tamanho padrão.
        }
    }
}