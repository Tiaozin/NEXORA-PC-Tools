using System;
using System.IO;

class LimpezaSistema
{
    public static void Limpar()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              NEXORA > LIMPEZA DO SISTEMA                ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");

        LimparTemporarios();
        LimparLixeira();
        LimparDNS();
        LimparDownloadsWindowsUpdate();
        LimparCleanmgr();
        LimparComponentesWindows();
        LimparDeliveryOptimization();

        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Limpeza concluída.                                      ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
    }

    private static void LimparTemporarios()
    {
        Console.WriteLine("→ Limpando arquivos temporários...");

        try
        {
            LimparPasta(Path.GetTempPath());

            string tempWindows = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                "Temp"
            );

            LimparPasta(tempWindows);

            Console.WriteLine("  ✓ Temporários processados.");
        }
        catch
        {
            Console.WriteLine("  ⚠ Não foi possível limpar todos os temporários.");
        }
    }

    private static void LimparPasta(string caminho)
    {
        if (!Directory.Exists(caminho))
            return;

        foreach (string arquivo in Directory.GetFiles(caminho))
        {
            try
            {
                File.Delete(arquivo);
            }
            catch
            {
                // Arquivo em uso ou sem permissão.
            }
        }

        foreach (string pasta in Directory.GetDirectories(caminho))
        {
            try
            {
                Directory.Delete(pasta, true);
            }
            catch
            {
                // Pasta em uso ou sem permissão.
            }
        }
    }

    private static void LimparLixeira()
    {
        Console.WriteLine("→ Limpando lixeira...");

        ExecutarComandos.ExecutarPowerShell(
            "Clear-RecycleBin -Force -ErrorAction SilentlyContinue"
        );

        Console.WriteLine("  ✓ Lixeira processada.");
    }

    private static void LimparDNS()
    {
        Console.WriteLine("→ Limpando cache DNS...");

        ExecutarComandos.ExecutarCMD(
            "ipconfig /flushdns"
        );

        Console.WriteLine("  ✓ Cache DNS limpo.");
    }

    private static void LimparDownloadsWindowsUpdate()
    {
        Console.WriteLine("→ Limpando atualizações baixadas...");

        ExecutarComandos.ExecutarCMD(
            "net stop wuauserv; " +
            "net stop bits; " +
            "rd /s /q \"%windir%\\SoftwareDistribution\\Download\"; " +
            "mkdir \"%windir%\\SoftwareDistribution\\Download\"; " +
            "net start bits; " +
            "net start wuauserv"
        );

        Console.WriteLine("  ✓ Downloads do Windows Update processados.");
    }

    private static void LimparCleanmgr()
    {
        Console.WriteLine("→ Executando Limpeza de Disco do Windows...");

        ExecutarComandos.ExecutarCMD(
            "cleanmgr /verylowdisk"
        );

        Console.WriteLine("  ✓ Limpeza de Disco executada.");
    }

    private static void LimparComponentesWindows()
    {
        Console.WriteLine("→ Limpando componentes antigos do Windows...");
        Console.WriteLine("  Isso pode demorar alguns minutos.");

        ExecutarComandos.ExecutarCMD(
            "DISM /Online /Cleanup-Image /StartComponentCleanup"
        );

        Console.WriteLine("  ✓ Componentes processados.");
    }

    private static void LimparDeliveryOptimization()
    {
        Console.WriteLine("→ Limpando cache da Otimização de Entrega...");

        ExecutarComandos.ExecutarPowerShell(
            "Delete-DeliveryOptimizationCache -Force -ErrorAction SilentlyContinue"
        );

        Console.WriteLine("  ✓ Cache processado.");
    }
}