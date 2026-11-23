using System;
using System.IO;

class LimpezaSistema
{
    public static void Limpar(Action<string> log)
    {
        ExecutarLimparTemporarios(log);
        ExecutarLimparLixeira(log);
        ExecutarLimparDNS(log);
        ExecutarLimparDownloadsWindowsUpdate(log);
        ExecutarLimparCleanmgr(log);
        ExecutarLimparComponentesWindows(log);
        ExecutarLimparDeliveryOptimization(log);
        ExecutarLimparMiniaturas(log);
        ExecutarLimparRelatoriosErros(log);

        log("✓ Limpeza completa concluída.");
    }

    public static void ExecutarLimparTemporarios(Action<string> log)
    {
        log("Limpando arquivos temporários...");
        try
        {
            LimparPasta(Path.GetTempPath());
            string tempWindows = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                "Temp"
            );
            LimparPasta(tempWindows);
            log("✓ Temporários processados.");
        }
        catch
        {
            log("⚠ Não foi possível limpar todos os temporários.");
        }
    }

    private static void LimparPasta(string caminho)
    {
        if (!Directory.Exists(caminho)) return;

        foreach (string arquivo in Directory.GetFiles(caminho))
        {
            try { File.Delete(arquivo); } catch { }
        }

        foreach (string pasta in Directory.GetDirectories(caminho))
        {
            try { Directory.Delete(pasta, true); } catch { }
        }
    }

    public static void ExecutarLimparLixeira(Action<string> log)
    {
        log("Limpando lixeira...");
        ExecutarComandos.ExecutarPowerShell("Clear-RecycleBin -Force -ErrorAction SilentlyContinue", log);
        log("✓ Lixeira processada.");
    }

    public static void ExecutarLimparDNS(Action<string> log)
    {
        log("Limpando cache DNS...");
        ExecutarComandos.ExecutarCMD("ipconfig /flushdns", log);
        log("✓ Cache DNS limpo.");
    }

    public static void ExecutarLimparDownloadsWindowsUpdate(Action<string> log)
    {
        log("Limpando atualizações baixadas...");
        ExecutarComandos.ExecutarCMD(
            "net stop wuauserv & net stop bits & rd /s /q \"%windir%\\SoftwareDistribution\\Download\" & mkdir \"%windir%\\SoftwareDistribution\\Download\" & net start bits & net start wuauserv",
            log
        );
        log("✓ Downloads do Windows Update processados.");
    }

    public static void ExecutarLimparCleanmgr(Action<string> log)
    {
        log("Executando Limpeza de Disco do Windows...");
        ExecutarComandos.ExecutarCMD("cleanmgr /verylowdisk", log);
        log("✓ Limpeza de Disco executada.");
    }

    public static void ExecutarLimparComponentesWindows(Action<string> log)
    {
        log("Limpando componentes antigos do Windows (DISM)... Pode demorar alguns minutos.");
        ExecutarComandos.ExecutarCMD("DISM /Online /Cleanup-Image /StartComponentCleanup", log);
        log("✓ Componentes processados.");
    }

    public static void ExecutarLimparDeliveryOptimization(Action<string> log)
    {
        log("Limpando cache da Otimização de Entrega...");
        ExecutarComandos.ExecutarPowerShell("Delete-DeliveryOptimizationCache -Force -ErrorAction SilentlyContinue", log);
        log("✓ Cache processado.");
    }

    public static void ExecutarLimparMiniaturas(Action<string> log)
    {
        log("Limpando cache de miniaturas...");
        ExecutarComandos.ExecutarCMD("del /f /s /q \"%LocalAppData%\\Microsoft\\Windows\\Explorer\\thumbcache_*.db\"", log);
        log("✓ Cache de miniaturas processado.");
    }

    public static void ExecutarLimparRelatoriosErros(Action<string> log)
    {
        log("Limpando relatórios de erros do Windows...");
        ExecutarComandos.ExecutarPowerShell(
            "Remove-Item \"$env:ProgramData\\Microsoft\\Windows\\WER\\ReportArchive\\*\" -Recurse -Force -ErrorAction SilentlyContinue; " +
            "Remove-Item \"$env:ProgramData\\Microsoft\\Windows\\WER\\ReportQueue\\*\" -Recurse -Force -ErrorAction SilentlyContinue; " +
            "Remove-Item \"$env:LOCALAPPDATA\\Microsoft\\Windows\\WER\\ReportArchive\\*\" -Recurse -Force -ErrorAction SilentlyContinue; " +
            "Remove-Item \"$env:LOCALAPPDATA\\Microsoft\\Windows\\WER\\ReportQueue\\*\" -Recurse -Force -ErrorAction SilentlyContinue",
            log
        );
        log("✓ Relatórios de erros processados.");
    }
}