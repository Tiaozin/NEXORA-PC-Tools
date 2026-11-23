using System;
using System.Diagnostics;
using System.Management;

class Drivers
{
    public static void VerificarDrivers(Action<string> log)
    {
        log("Verificando drivers de vídeo instalados...");

        try
        {
            using var gpus = new ManagementObjectSearcher("SELECT Name, DriverVersion, DriverDate, Status FROM Win32_VideoController");
            foreach (ManagementObject gpu in gpus.Get())
            {
                string nome = gpu["Name"]?.ToString() ?? "Não encontrado";
                string versao = gpu["DriverVersion"]?.ToString() ?? "Não encontrado";
                string status = gpu["Status"]?.ToString() ?? "Não encontrado";
                string data = "Não encontrado";

                string? dataBruta = gpu["DriverDate"]?.ToString();
                if (!string.IsNullOrEmpty(dataBruta) && dataBruta.Length >= 8)
                    data = $"{dataBruta.Substring(6, 2)}/{dataBruta.Substring(4, 2)}/{dataBruta.Substring(0, 4)}";

                log($"GPU: {nome} | Versão: {versao} | Data: {data} | Status: {status}");
            }
        }
        catch
        {
            log("⚠ Não foi possível obter os drivers de vídeo.");
        }

        log("Verificando dispositivos com problemas...");
        int problemas = 0;

        try
        {
            using var dispositivos = new ManagementObjectSearcher("SELECT Name FROM Win32_PNPEntity WHERE ConfigManagerErrorCode != 0");
            foreach (ManagementObject dispositivo in dispositivos.Get())
            {
                string nome = dispositivo["Name"]?.ToString() ?? "Desconhecido";
                log($"⚠ Dispositivo com problema: {nome}");
                problemas++;
            }

            if (problemas == 0)
                log("✓ Nenhum dispositivo com problema encontrado.");
        }
        catch
        {
            log("⚠ Não foi possível verificar os dispositivos.");
        }
    }

    public static void InstalarNvidia(Action<string> log) => AbrirUrl("https://www.nvidia.com/Download/index.aspx", log);
    public static void InstalarAmdGpu(Action<string> log) => AbrirUrl("https://www.amd.com/pt/support", log);
    public static void InstalarAmdProcessador(Action<string> log) => AbrirUrl("https://www.amd.com/pt/support/chipsets", log);

    public static void VerificarAtualizacoes(Action<string> log)
    {
        ExecutarComandos.ExecutarCMD("start ms-settings:windowsupdate-action", log);
        log("Tela do Windows Update aberta para verificar drivers opcionais.");
    }

    private static void AbrirUrl(string url, Action<string> log)
    {
        try
        {
            Process.Start(new ProcessStartInfo { FileName = url, UseShellExecute = true });
            log($"Página aberta no navegador: {url}");
        }
        catch
        {
            log($"⚠ Não foi possível abrir o navegador. Acesse: {url}");
        }
    }
}