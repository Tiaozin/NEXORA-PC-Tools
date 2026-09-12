using System;
using System.Management;
using System.Diagnostics;

public static class AtivacaoWindows
{

    public static void ExecutarAtivacao()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║           NEXORA > SISTEMAS > ATIVAÇÃO > STATUS         ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
        Console.WriteLine("║");
        Console.WriteLine("║ Passos para ativar seu Windows:");
        Console.WriteLine("║");
        Console.WriteLine("║ 1. Aceite o pedido de administrador");
        Console.WriteLine("║ 2. Aperte 1 do seu teclado para abrir a ativação");
        Console.WriteLine("║ 3. Aperte 1 novamente e espere ativar");
        Console.WriteLine("║ 4. Feche o PowerShell e seja feliz :)");
        Console.WriteLine("║");
        Console.WriteLine("║ Pressione qualquer tecla para continuar a ativação...                                                ");
        Console.WriteLine("╚══════════════════════════════════════════════════════════");
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
        if (VerificarStatusWindows())
        {
            Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           NEXORA > SISTEMAS > ATIVAÇÃO > STATUS         ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
            Console.WriteLine("║");
            Console.WriteLine("║ Status do Windows: Ativado");
            InformacoesPC.ExibirRodape();
            return;
        }
        else
        {
            Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           NEXORA > SISTEMAS > ATIVAÇÃO > STATUS         ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
            Console.WriteLine("║");
            Console.WriteLine("║ Status do Windows: Desativado");
            InformacoesPC.ExibirRodape();
            return;
        }
    }
}