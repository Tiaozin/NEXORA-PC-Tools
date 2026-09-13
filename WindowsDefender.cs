using System;
using System.Diagnostics;
class WindowsDefender
{
    public static bool VerificarStatus()
    {
        ProcessStartInfo processo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = "-NoProfile -Command \"(Get-MpComputerStatus).AntivirusEnabled\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using (Process? resultado = Process.Start(processo))
        {
            if (resultado == null)
                return false;

            string saida = resultado.StandardOutput.ReadToEnd().Trim();

            resultado.WaitForExit();

            return saida.Equals("True", StringComparison.OrdinalIgnoreCase);
        }
    }

    public static void DesativarDefender()
    {
        UI.Cabecalho("NEXORA > SISTEMAS > DEFENDER");
        UI.Linha("Status do Windows Defender: Ativado");
        UI.LinhaVazia();
        UI.Linha("Tem certeza que quer desativar o Windows Defender? (s/n)");
        UI.Rodape();


        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");
            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarCMD("REG ADD \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /V DisableAntiSpyware /t REG_DWORD /D 1 /F");
                break;
            }
            else if (resposta == "n" || resposta == "N")
            {
                Console.WriteLine("Voltando...");
                break;
            }
            else
                Console.WriteLine("Opção inválida!");
        }
    }
    public static void AtivarDefender()
    {
        UI.Cabecalho("NEXORA > SISTEMAS > DEFENDER");
        UI.Linha("Status do Windows Defender: Desativado");
        UI.LinhaVazia();
        UI.Linha("Tem certeza que quer ativar o Windows Defender? (s/n)");
        UI.Rodape();

        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");
            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarCMD("REG DELETE \"HKEY_LOCAL_MACHINE\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /V DisableAntiSpyware /F");
                break;
            }
            else if (resposta == "n" || resposta == "N")
            {
                Console.WriteLine("Voltando...");
                break;
            }
            else
                Console.WriteLine("Opção inválida!");
        }
    }


}