using System;
using System.IO;

class LimpezaSistema
{
    // Executa todas as limpezas dentro de uma única tela
    public static void Limpar()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > LIMPEZA");

        ExecutarLimparTemporarios();
        ExecutarLimparLixeira();
        ExecutarLimparDNS();
        ExecutarLimparDownloadsWindowsUpdate();
        ExecutarLimparCleanmgr();
        ExecutarLimparComponentesWindows();
        ExecutarLimparDeliveryOptimization();
        ExecutarLimparMiniaturas();
        ExecutarLimparRelatoriosErros();

        UI.Linha("Limpeza concluída.");
        UI.Rodape();
    }

    public static void LimparTemporarios()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > LIMPEZA");
        ExecutarLimparTemporarios();
        UI.Rodape();
    }

    private static void ExecutarLimparTemporarios()
    {
        UI.Linha("Limpando arquivos temporários...");

        try
        {
            LimparPasta(Path.GetTempPath());

            string tempWindows = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                "Temp"
            );

            LimparPasta(tempWindows);

            UI.Linha("✓ Temporários processados.");
        }
        catch
        {
            UI.Linha("⚠ Não foi possível limpar todos os temporários.");
        }

        UI.LinhaVazia();
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
        UI.Cabecalho("NEXORA > FERRAMENTAS > LIMPEZA");
        ExecutarLimparLixeira();
        UI.Rodape();
    }

    private static void ExecutarLimparLixeira()
    {
        UI.Linha("Limpando lixeira...");

        ExecutarComandos.ExecutarPowerShell(
            "Clear-RecycleBin -Force -ErrorAction SilentlyContinue"
        );

        UI.Linha("✓ Lixeira processada.");
        UI.LinhaVazia();
    }

    public static void LimparDNS()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > LIMPEZA");
        ExecutarLimparDNS();
        UI.Rodape();
    }

    private static void ExecutarLimparDNS()
    {
        UI.Linha("Limpando cache DNS...");

        ExecutarComandos.ExecutarCMD(
            "ipconfig /flushdns"
        );

        UI.Linha("✓ Cache DNS limpo.");
        UI.LinhaVazia();
    }

    public static void LimparDownloadsWindowsUpdate()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > LIMPEZA");
        ExecutarLimparDownloadsWindowsUpdate();
        UI.Rodape();
    }

    private static void ExecutarLimparDownloadsWindowsUpdate()
    {
        UI.Linha("Limpando atualizações baixadas...");

        ExecutarComandos.ExecutarCMD(
            "net stop wuauserv; " +
            "net stop bits; " +
            "rd /s /q \"%windir%\\SoftwareDistribution\\Download\"; " +
            "mkdir \"%windir%\\SoftwareDistribution\\Download\"; " +
            "net start bits; " +
            "net start wuauserv"
        );

        UI.Linha("✓ Downloads do Windows Update processados.");
        UI.LinhaVazia();
    }

    public static void LimparCleanmgr()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > LIMPEZA");
        ExecutarLimparCleanmgr();
        UI.Rodape();
    }

    private static void ExecutarLimparCleanmgr()
    {
        UI.Linha("Executando Limpeza de Disco do Windows...");

        ExecutarComandos.ExecutarCMD(
            "cleanmgr /verylowdisk"
        );

        UI.Linha("✓ Limpeza de Disco executada.");
        UI.LinhaVazia();
    }

    private static void ExecutarLimparComponentesWindows()
    {
        UI.Linha("Limpando componentes antigos do Windows...");
        UI.Linha("Isso pode demorar alguns minutos.");

        ExecutarComandos.ExecutarCMD(
            "DISM /Online /Cleanup-Image /StartComponentCleanup"
        );

        UI.Linha("✓ Componentes processados.");
        UI.LinhaVazia();
    }

    public static void LimparComponentesWindows()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > LIMPEZA");
        ExecutarLimparComponentesWindows();
        UI.Rodape();
    }

    private static void ExecutarLimparDeliveryOptimization()
    {
        UI.Linha("Limpando cache da Otimização de Entrega...");

        ExecutarComandos.ExecutarPowerShell(
            "Delete-DeliveryOptimizationCache " +
            "-Force -ErrorAction SilentlyContinue"
        );

        UI.Linha("✓ Cache processado.");
        UI.LinhaVazia();
    }

    public static void LimparDeliveryOptimization()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > LIMPEZA");
        ExecutarLimparDeliveryOptimization();
        UI.Rodape();
    }

    public static void LimparMiniaturas()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > LIMPEZA");
        ExecutarLimparMiniaturas();
        UI.Rodape();
    }

    private static void ExecutarLimparMiniaturas()
    {
        UI.Linha("Limpando cache de miniaturas...");

        ExecutarComandos.ExecutarCMD(
            "del /f /s /q \"%LocalAppData%\\Microsoft\\Windows\\Explorer\\thumbcache_*.db\""
        );

        UI.Linha("✓ Cache de miniaturas processado.");
        UI.LinhaVazia();
    }

    public static void LimparRelatoriosErros()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > LIMPEZA");
        ExecutarLimparRelatoriosErros();
        UI.Rodape();
    }

    private static void ExecutarLimparRelatoriosErros()
    {
        UI.Linha("Limpando relatórios de erros do Windows...");

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

        UI.Linha("✓ Relatórios de erros processados.");
        UI.LinhaVazia();
    }
}