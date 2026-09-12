using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Text;

class ExecutarComandos
{
    public static void ExecutarPowerShell(string comando)
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
                UseShellExecute = true,
                Verb = "runas"
            };

            Process? processoPowerShell = Process.Start(processo);

            processoPowerShell?.WaitForExit();
        }
        catch (Win32Exception)
        {
            Console.WriteLine("Permissão de administrador não concedida.");
        }
    }

    public static void ExecutarCMD(string comando)
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
                UseShellExecute = true,
                Verb = "runas"
            };

            Process? processoCMD = Process.Start(processo);

            processoCMD?.WaitForExit();
        }
        catch (Win32Exception)
        {
            Console.WriteLine("Permissão de administrador não concedida.");
        }
        finally
        {
            try
            {
                if (File.Exists(arquivoTemp))
                    File.Delete(arquivoTemp);
            }
            catch
            {
                // Ignora caso o arquivo temporário ainda esteja em uso.
            }
        }
    }
}