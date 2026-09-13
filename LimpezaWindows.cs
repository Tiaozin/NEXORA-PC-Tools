using System;
using System.IO;

class LimpezaSistema
{
    public static void Limpar()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              NEXORA > LIMPEZA DO SISTEMA               ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");

        LimparTemporarios();
        LimparLixeira();
        LimparDNS();
        LimparDownloadsWindowsUpdate();
        LimparCleanmgr();
        LimparComponentesWindows();
        LimparDeliveryOptimization();
        LimparMiniaturas();
        LimparRelatoriosErros();

        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Limpeza concluída.                                      ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
    }

    public static void LimparTemporarios()
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
            Console.WriteLine(
                "  ⚠ Não foi possível limpar todos os temporários."
            );
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

    public static void LimparLixeira()
    {
        Console.WriteLine("→ Limpando lixeira...");

        ExecutarComandos.ExecutarPowerShell(
            "Clear-RecycleBin -Force -ErrorAction SilentlyContinue"
        );

        Console.WriteLine("  ✓ Lixeira processada.");
    }

    public static void LimparDNS()
    {
        Console.WriteLine("→ Limpando cache DNS...");

        ExecutarComandos.ExecutarCMD(
            "ipconfig /flushdns"
        );

        Console.WriteLine("  ✓ Cache DNS limpo.");
    }

    public static void LimparDownloadsWindowsUpdate()
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

        Console.WriteLine(
            "  ✓ Downloads do Windows Update processados."
        );
    }

    public static void LimparCleanmgr()
    {
        Console.WriteLine("→ Executando Limpeza de Disco do Windows...");

        ExecutarComandos.ExecutarCMD(
            "cleanmgr /verylowdisk"
        );

        Console.WriteLine("  ✓ Limpeza de Disco executada.");
    }

    public static void LimparComponentesWindows()
    {
        Console.WriteLine("→ Limpando componentes antigos do Windows...");
        Console.WriteLine("  Isso pode demorar alguns minutos.");

        ExecutarComandos.ExecutarCMD(
            "DISM /Online /Cleanup-Image /StartComponentCleanup"
        );

        Console.WriteLine("  ✓ Componentes processados.");
    }

    public static void LimparDeliveryOptimization()
    {
        Console.WriteLine(
            "→ Limpando cache da Otimização de Entrega..."
        );

        ExecutarComandos.ExecutarPowerShell(
            "Delete-DeliveryOptimizationCache " +
            "-Force -ErrorAction SilentlyContinue"
        );

        Console.WriteLine("  ✓ Cache processado.");
    }

    public static void LimparMiniaturas()
    {
        Console.WriteLine("→ Limpando cache de miniaturas...");

        ExecutarComandos.ExecutarCMD(
            "del /f /s /q \"%LocalAppData%\\Microsoft\\Windows\\Explorer\\thumbcache_*.db\""
        );

        Console.WriteLine("  ✓ Cache de miniaturas processado.");
    }

    public static void LimparRelatoriosErros()
    {
        Console.WriteLine("→ Limpando relatórios de erros do Windows...");

        ExecutarComandos.ExecutarPowerShell(
            "Remove-Item \"$env:ProgramData\\Microsoft\\Windows\\WER\\ReportArchive\\*\" " +
            "-Recurse -Force -ErrorAction SilentlyContinue; " +
            "Remove-Item \"$env:ProgramData\\Microsoft\\Windows\\WER\\ReportQueue\\*\" " +
            "-Recurse -Force -ErrorAction SilentlyContinue; " +
            "Remove-Item \"$env:LOCALAPPDATA\\Microsoft\\Windows\\WER\\ReportArchive\\*\" " +
            "-Recurse -Force -ErrorAction SilentlyContinue; " +
            "Remove-Item \"$env:LOCALAPPDATA\\Microsoft\\Windows\\WER\\ReportQueue\\*\" " +
            "-Recurse -Force -ErrorAction SilentlyContinue"
        );

        Console.WriteLine("  ✓ Relatórios de erros processados.");
    }
}