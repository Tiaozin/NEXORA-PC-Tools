using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

class Otimizacao
{
    private static readonly string[] servicos =
    {
        "SysMain", "DiagTrack", "lfsvc", "Spooler", "Fax",
        "XblAuthManager", "XblGameSave", "XboxGipSvc", "XboxNetApiSvc", "MapsBroker"
    };

    private static readonly string caminhoBackup = Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
        "NEXORA",
        "backup_servicos.txt"
    );

    public static void OtimizacaoCompleta(bool desativarSpooler, Action<string> log)
    {
        SalvarBackup(log);
        DesativarServicos(desativarSpooler, log);

        log("Iniciando limpeza do sistema...");
        LimpezaSistema.Limpar(log);

        log("✓ Otimização completa concluída.");
    }

    private static void SalvarBackup(Action<string> log)
    {
        try
        {
            string pasta = Path.GetDirectoryName(caminhoBackup) ?? "";
            Directory.CreateDirectory(pasta);

            List<string> linhas = new List<string>();
            foreach (string servico in servicos)
            {
                string startup = ObterStartType(servico);
                string status = ObterStatus(servico);

                if (startup != "Não encontrado")
                    linhas.Add($"{servico}|{startup}|{status}");
            }

            File.WriteAllLines(caminhoBackup, linhas);
            log("Backup dos serviços salvo com sucesso.");
        }
        catch
        {
            log("⚠ Não foi possível salvar o backup dos serviços.");
        }
    }

    private static void DesativarServicos(bool desativarSpooler, Action<string> log)
    {
        foreach (string servico in servicos)
        {
            if (servico == "Spooler" && !desativarSpooler)
            {
                log("Spooler de Impressão mantido como está.");
                continue;
            }

            log($"Desativando: {servico}...");
            ExecutarComandos.ExecutarPowerShell(
                $"Stop-Service -Name \"{servico}\" -Force -ErrorAction SilentlyContinue; " +
                $"Set-Service -Name \"{servico}\" -StartupType Disabled",
                log
            );
        }
    }

    public static void RestaurarServicos(Action<string> log)
    {
        if (!File.Exists(caminhoBackup))
        {
            log("⚠ Nenhum backup de serviços foi encontrado.");
            return;
        }

        string[] linhas;
        try
        {
            linhas = File.ReadAllLines(caminhoBackup);
        }
        catch
        {
            log("⚠ Não foi possível ler o backup dos serviços.");
            return;
        }

        foreach (string linha in linhas)
        {
            string[] dados = linha.Split('|');
            if (dados.Length < 3) continue;

            string servico = dados[0];
            string startup = dados[1];
            string status = dados[2];

            log($"Restaurando: {servico}...");
            string comando = $"Set-Service -Name \"{servico}\" -StartupType {startup}; ";

            if (status == "Running")
                comando += $"Start-Service -Name \"{servico}\" -ErrorAction SilentlyContinue";
            else if (status == "Stopped")
                comando += $"Stop-Service -Name \"{servico}\" -Force -ErrorAction SilentlyContinue";

            ExecutarComandos.ExecutarPowerShell(comando, log);
        }

        try { File.Delete(caminhoBackup); } catch { }
        log("✓ Serviços restaurados com sucesso.");
    }

    private static string ObterStatus(string servico)
    {
        ProcessStartInfo processo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-NoProfile -Command \"$s = Get-Service -Name '{servico}' -ErrorAction SilentlyContinue; if ($null -eq $s) {{ 'Não encontrado' }} else {{ $s.Status }}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using Process? resultado = Process.Start(processo);
        if (resultado == null) return "Não encontrado";
        string saida = resultado.StandardOutput.ReadToEnd().Trim();
        resultado.WaitForExit();
        return string.IsNullOrEmpty(saida) ? "Não encontrado" : saida;
    }

    private static string ObterStartType(string servico)
    {
        ProcessStartInfo processo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-NoProfile -Command \"$s = Get-Service -Name '{servico}' -ErrorAction SilentlyContinue; if ($null -eq $s) {{ 'Não encontrado' }} else {{ $s.StartType }}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using Process? resultado = Process.Start(processo);
        if (resultado == null) return "Não encontrado";
        string saida = resultado.StandardOutput.ReadToEnd().Trim();
        resultado.WaitForExit();
        return string.IsNullOrEmpty(saida) ? "Não encontrado" : saida;
    }
}