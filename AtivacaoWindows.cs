using System;
using System.Management;
using System.Diagnostics;

public static class AtivacaoWindows
{

    public static void ExecutarAtivacao()
    {
        UI.Cabecalho("NEXORA > SISTEMAS > ATIVAÇÃO > STATUS");
        UI.Linha("Passos para ativar seu Windows:");
        UI.LinhaVazia();
        UI.Linha("1. Aceite o pedido de administrador");
        UI.Linha("2. Aperte 1 do seu teclado para abrir a ativação");
        UI.Linha("3. Aperte 1 novamente e espere ativar");
        UI.Linha("4. Feche o PowerShell e seja feliz :)");
        UI.LinhaVazia();
        UI.Linha("Pressione qualquer tecla para continuar a ativação...");
        UI.Rodape();

        Console.ReadKey();
        ExecutarComandos.ExecutarPowerShell("irm https://get.activated.win | iex");
    }
    public static bool VerificarStatusWindows()
    {
        try
        {
            string query = "SELECT LicenseStatus FROM SoftwareLicensingProduct " +
                            "WHERE PartialProductKey IS NOT NULL AND ApplicationID='55c92734-d682-4d71-983e-d6ec3f16059f'";

            using (var searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    int licenseStatus = Convert.ToInt32(item["LicenseStatus"]);
                    if (licenseStatus == 1)
                        return true;
                }
            }

            return false;
        }
        catch
        {
            return false;
        }
    }

    public static void MostrarStatusWindows()
    {
        UI.Cabecalho("NEXORA > SISTEMAS > ATIVAÇÃO > STATUS");

        if (VerificarStatusWindows())
        {
            UI.Linha("Status do Windows: Ativado");
            InformacoesPC.ExibirRodape();
            return;
        }
        else
        {
            UI.Linha("Status do Windows: Desativado");
            InformacoesPC.ExibirRodape();
            return;
        }
    }
}