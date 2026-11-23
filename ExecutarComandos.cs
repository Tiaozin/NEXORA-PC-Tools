using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;

class ExecutarComandos
{
    public static void ExecutarPowerShell(string comando, Action<string>? log = null)
    {
        try
        {
            string comandoBase64 = Convert.ToBase64String(
                Encoding.Unicode.GetBytes(comando)
            );

            ProcessStartInfo processo = new ProcessStartInfo
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -EncodedCommand {comandoBase64}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using Process? proc = Process.Start(processo);
            if (proc == null) return;

            proc.OutputDataReceived += (_, e) => { if (!string.IsNullOrWhiteSpace(e.Data)) log?.Invoke(e.Data); };
            proc.ErrorDataReceived += (_, e) => { if (!string.IsNullOrWhiteSpace(e.Data)) log?.Invoke($"[ERRO] {e.Data}"); };

            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();
        }
        catch (Win32Exception)
        {
            log?.Invoke("Permissão de administrador não concedida.");
        }
    }

    public static void ExecutarCMD(string comando, Action<string>? log = null)
    {
        string arquivoTemp = Path.Combine(
            Path.GetTempPath(),
            $"NEXORA_{Guid.NewGuid()}.bat"
        );

        try
        {
            File.WriteAllText(
                arquivoTemp,
                "@echo off\r\n" + comando,
                Encoding.Default
            );

            ProcessStartInfo processo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                Arguments = $"/c \"{arquivoTemp}\"",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using Process? proc = Process.Start(processo);
            if (proc == null) return;

            proc.OutputDataReceived += (_, e) => { if (!string.IsNullOrWhiteSpace(e.Data)) log?.Invoke(e.Data); };
            proc.ErrorDataReceived += (_, e) => { if (!string.IsNullOrWhiteSpace(e.Data)) log?.Invoke($"[ERRO] {e.Data}"); };

            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();
        }
        catch (Win32Exception)
        {
            log?.Invoke("Permissão de administrador não concedida.");
        }
        finally
        {
            try
            {
                if (File.Exists(arquivoTemp))
                    File.Delete(arquivoTemp);
            }
            catch { }
        }
    }

    public static void ExecutarWinget(string argumentos, Action<string>? log = null)
    {
        try
        {
            ProcessStartInfo processo = new ProcessStartInfo
            {
                FileName = "winget.exe",
                Arguments = argumentos,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
                CreateNoWindow = true
            };

            using Process? proc = Process.Start(processo);
            if (proc == null) return;

            proc.OutputDataReceived += (_, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.Data) && e.Data.Trim().Length > 2)
                    log?.Invoke(e.Data.Trim());
            };
            proc.ErrorDataReceived += (_, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.Data))
                    log?.Invoke($"[ERRO] {e.Data.Trim()}");
            };

            proc.BeginOutputReadLine();
            proc.BeginErrorReadLine();
            proc.WaitForExit();
        }
        catch (Exception ex)
        {
            log?.Invoke($"Erro ao executar WinGet: {ex.Message}");
        }
    }
}