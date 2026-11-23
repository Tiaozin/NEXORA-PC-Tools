using System;
using System.Diagnostics;

class Energia
{
    private const string GUIDDesempenhoMaximo = "e9a42b02-d5df-448d-aa00-03f14749eb61";
    private const string GUIDAltoDesempenho = "8c5e7fda-e8bf-4a96-9a85-a6e23a8c635c";
    private const string GUIDEquilibrado = "381b4222-f694-41f0-9685-ff5bb260df2e";
    private const string GUIDEconomia = "a1841308-3541-4fab-bc81-f71556f20b4a";

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

        using Process? resultado = Process.Start(processo);
        if (resultado == null) return "Desconhecido";

        string saida = resultado.StandardOutput.ReadToEnd().Trim();
        resultado.WaitForExit();

        if (string.IsNullOrWhiteSpace(saida)) return "Desconhecido";
        if (saida.Contains(GUIDDesempenhoMaximo, StringComparison.OrdinalIgnoreCase)) return "Desempenho Máximo";
        if (saida.Contains(GUIDAltoDesempenho, StringComparison.OrdinalIgnoreCase)) return "Alto Desempenho";
        if (saida.Contains(GUIDEquilibrado, StringComparison.OrdinalIgnoreCase)) return "Equilibrado";
        if (saida.Contains(GUIDEconomia, StringComparison.OrdinalIgnoreCase)) return "Economia de Energia";

        int posicao = saida.LastIndexOf(')');
        if (posicao >= 0 && posicao < saida.Length - 1)
        {
            string nome = saida[(posicao + 1)..].Trim();
            if (!string.IsNullOrWhiteSpace(nome)) return nome;
        }
        return saida;
    }

    public static void AtivarDesempenhoMaximo(Action<string> log)
    {
        ExecutarComandos.ExecutarCMD($"powercfg /setactive {GUIDDesempenhoMaximo}", log);

        if (ObterPlanoAtual() == "Desempenho Máximo")
        {
            log("✓ Plano Desempenho Máximo ativado.");
        }
        else
        {
            ExecutarComandos.ExecutarCMD($"powercfg -duplicatescheme {GUIDDesempenhoMaximo}", log);
            ExecutarComandos.ExecutarCMD($"powercfg /setactive {GUIDDesempenhoMaximo}", log);
            log("✓ Plano Desempenho Máximo criado e ativado.");
        }
    }

    public static void AtivarAltoDesempenho(Action<string> log)
    {
        ExecutarComandos.ExecutarCMD($"powercfg /setactive {GUIDAltoDesempenho}", log);
        log("✓ Plano Alto Desempenho ativado.");
    }

    public static void AtivarEquilibrado(Action<string> log)
    {
        ExecutarComandos.ExecutarCMD($"powercfg /setactive {GUIDEquilibrado}", log);
        log("✓ Plano Equilibrado ativado.");
    }

    public static void AtivarEconomia(Action<string> log)
    {
        ExecutarComandos.ExecutarCMD($"powercfg /setactive {GUIDEconomia}", log);
        log("✓ Plano Economia de Energia ativado.");
    }
}