using System;
using System.Management;

public static class AtivacaoWindows
{
    public static void ExecutarAtivacao()
    {
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
}