using System;
using System.Diagnostics;

class WindowsUpdate
{
    public static bool VerificarStatus()
    {
        ProcessStartInfo processo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments =
                "-NoProfile -Command " +
                "\"$valor = (Get-ItemProperty -Path 'HKLM:\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU' -Name 'NoAutoUpdate' -ErrorAction SilentlyContinue).NoAutoUpdate; " +
                "if ($valor -eq 1) { 'False' } else { 'True' }\"",

            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using Process? resultado = Process.Start(processo);

        if (resultado == null)
            return false;

        string saida = resultado.StandardOutput.ReadToEnd().Trim();

        resultado.WaitForExit();

        return saida.Equals("True", StringComparison.OrdinalIgnoreCase);
    }

    public static void DesativarUpdate()
    {
        UI.Cabecalho("NEXORA > SISTEMAS > UPDATE");
        UI.Linha("Status do Windows Update: Ativado");
        UI.LinhaVazia();
        UI.Linha("Tem certeza que quer desativar o Windows Update? (s/n)");
        UI.Rodape();


        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");
            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarPowerShell(
        "$caminho = \"HKLM:\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\"; " +
        "if (!(Test-Path $caminho)) { New-Item -Path $caminho -Force | Out-Null }; " +
        "New-ItemProperty -Path $caminho -Name \"NoAutoUpdate\" -PropertyType DWord -Value 1 -Force | Out-Null; " +
        "gpupdate /force"
    );
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
    public static void AtivarUpdate()
    {
        UI.Cabecalho("NEXORA > SISTEMAS > UPDATE");
        UI.Linha("Status do Windows Update: Desativado");
        UI.LinhaVazia();
        UI.Linha("Tem certeza que quer ativar o Windows Update? (s/n)");
        UI.Rodape();

        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");
            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarPowerShell(
        "Remove-ItemProperty -Path \"HKLM:\\SOFTWARE\\Policies\\Microsoft\\Windows\\WindowsUpdate\\AU\" " +
        "-Name \"NoAutoUpdate\" -ErrorAction SilentlyContinue; " +
        "gpupdate /force"
    );
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