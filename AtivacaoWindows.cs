using System;
using System.Management;

public static class AtivacaoWindows
{
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
            Console.WriteLine("║");
            InformacoesPC.ExibirWindows();
            InformacoesPC.ExibirRodape();
            return;
        }
        else
        {
            Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
            Console.WriteLine("║           NEXORA > SISTEMAS > ATIVAÇÃO > STATUS         ║");
            Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
            Console.WriteLine("║");
            Console.WriteLine("║ Status do Windows: Ativado");
            Console.WriteLine("║");
            InformacoesPC.ExibirWindows();
            InformacoesPC.ExibirRodape();
            return;
        }
    }
}