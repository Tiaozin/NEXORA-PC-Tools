using System;
using System.Diagnostics;

class Energia
{
    private const string GUIDDesempenhoMaximo =
        "e9a42b02-d5df-448d-aa00-03f14749eb61";

    private const string GUIDAltoDesempenho =
        "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c";

    private const string GUIDEquilibrado =
        "381b4222-f694-41f0-9685-ff5bb260df2e";

    private const string GUIDEconomia =
        "a1841308-3541-4fab-bc81-f71556f20b4a";

    public static string ObterPlanoAtual()
    {
        ProcessStartInfo processo = new ProcessStartInfo
        {
            FileName = "powercfg.exe",
            Arguments = "/getactivescheme",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using (Process? resultado = Process.Start(processo))
        {
            if (resultado == null)
                return "Desconhecido";

            string saida = resultado.StandardOutput.ReadToEnd().Trim();

            resultado.WaitForExit();

            if (string.IsNullOrWhiteSpace(saida))
                return "Desconhecido";

            if (saida.Contains(GUIDDesempenhoMaximo, StringComparison.OrdinalIgnoreCase))
                return "Desempenho Máximo";

            if (saida.Contains(GUIDAltoDesempenho, StringComparison.OrdinalIgnoreCase))
                return "Alto Desempenho";

            if (saida.Contains(GUIDEquilibrado, StringComparison.OrdinalIgnoreCase))
                return "Equilibrado";

            if (saida.Contains(GUIDEconomia, StringComparison.OrdinalIgnoreCase))
                return "Economia de Energia";

            int posicao = saida.LastIndexOf(')');

            if (posicao >= 0 && posicao < saida.Length - 1)
            {
                string nome = saida[(posicao + 1)..].Trim();

                if (!string.IsNullOrWhiteSpace(nome))
                    return nome;
            }

            return saida;
        }
    }

    public static void AtivarDesempenhoMaximo()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              NEXORA > FERRAMENTAS > ENERGIA             ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Plano: Desempenho Máximo                                ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Tem certeza que quer ativar o plano de Desempenho       ║");
        Console.WriteLine("║ Máximo? (s/n)                                           ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");

            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarCMD(
                    $"powercfg /setactive {GUIDDesempenhoMaximo}"
                );

                if (ObterPlanoAtual() == "Desempenho Máximo")
                {
                    Console.WriteLine(
                        "Plano Desempenho Máximo ativado."
                    );
                }
                else
                {
                    ExecutarComandos.ExecutarCMD(
                        $"powercfg -duplicatescheme {GUIDDesempenhoMaximo}"
                    );

                    Console.WriteLine(
                        "Plano Desempenho Máximo criado. " +
                        "Execute a operação novamente para ativá-lo."
                    );
                }

                break;
            }
            else if (resposta == "n" || resposta == "N")
            {
                Console.WriteLine("Voltando...");
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }
        }
    }

    public static void AtivarAltoDesempenho()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              NEXORA > FERRAMENTAS > ENERGIA             ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Plano: Alto Desempenho                                  ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Tem certeza que quer ativar o plano Alto Desempenho?    ║");
        Console.WriteLine("║ (s/n)                                                   ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");

            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarCMD(
                    $"powercfg /setactive {GUIDAltoDesempenho}"
                );

                Console.WriteLine(
                    "Plano Alto Desempenho ativado."
                );

                break;
            }
            else if (resposta == "n" || resposta == "N")
            {
                Console.WriteLine("Voltando...");
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }
        }
    }

    public static void AtivarEquilibrado()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              NEXORA > FERRAMENTAS > ENERGIA             ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Plano: Equilibrado                                      ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Tem certeza que quer ativar o plano Equilibrado? (s/n)  ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");

            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarCMD(
                    $"powercfg /setactive {GUIDEquilibrado}"
                );

                Console.WriteLine(
                    "Plano Equilibrado ativado."
                );

                break;
            }
            else if (resposta == "n" || resposta == "N")
            {
                Console.WriteLine("Voltando...");
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }
        }
    }

    public static void AtivarEconomia()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              NEXORA > FERRAMENTAS > ENERGIA             ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Plano: Economia de Energia                              ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Tem certeza que quer ativar Economia de Energia? (s/n)  ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");

            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarCMD(
                    $"powercfg /setactive {GUIDEconomia}"
                );

                Console.WriteLine(
                    "Plano Economia de Energia ativado."
                );

                break;
            }
            else if (resposta == "n" || resposta == "N")
            {
                Console.WriteLine("Voltando...");
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }
        }
    }
}