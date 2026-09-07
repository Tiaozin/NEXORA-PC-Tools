using System;
using System.Management;
using Vortice.DXGI;

class InformacoesPC
{
    public static void ExibirInformacoesPC()
    {

        string nomeProcessador = "Não encontrado";
        double clockAtual = 0;
        int nucleos = 0;
        int threads = 0;


        // Informações do processador
        ManagementObjectSearcher processador =
            new ManagementObjectSearcher("SELECT Name, CurrentClockSpeed, MaxClockSpeed, NumberOfCores, NumberOfLogicalProcessors FROM Win32_Processor");

        foreach (ManagementObject cpu in processador.Get())
        {
            nomeProcessador = cpu["Name"]?.ToString() ?? "Não encontrado";

            clockAtual = Convert.ToDouble(cpu["CurrentClockSpeed"]) / 1000;

            nucleos = Convert.ToInt32(cpu["NumberOfCores"]);
            threads = Convert.ToInt32(cpu["NumberOfLogicalProcessors"]);
        }

        string nomeGPU = "Não encontrado";
        double vram = 0;

        // Informações da GPU
        using (IDXGIFactory1 factory = DXGI.CreateDXGIFactory1<IDXGIFactory1>())
        {
            ulong maiorMemoria = 0;

            for (uint i = 0; ; i++)
            {
                if (factory.EnumAdapters1(i, out IDXGIAdapter1 adapter).Failure)
                    break;

                AdapterDescription1 descricao = adapter.Description1;

                // Ignora adaptadores de software
                if ((descricao.Flags & AdapterFlags.Software) != 0)
                {
                    adapter.Dispose();
                    continue;
                }

                ulong memoriaBytes =
                    (ulong)(nuint)descricao.DedicatedVideoMemory;

                // Guarda a GPU com maior memória dedicada
                if (memoriaBytes > maiorMemoria)
                {
                    maiorMemoria = memoriaBytes;

                    nomeGPU = descricao.Description;

                    vram = memoriaBytes /
                           (1024.0 * 1024.0 * 1024.0);
                }

                adapter.Dispose();
            }
        }

        string driverGPU = "Não encontrado";

        ManagementObjectSearcher driver =
    new ManagementObjectSearcher(
        "SELECT Name, DriverVersion FROM Win32_VideoController");

        foreach (ManagementObject gpu in driver.Get())
        {
            if (gpu["Name"]?.ToString() == nomeGPU)
            {
                driverGPU = gpu["DriverVersion"]?.ToString() ?? "Não encontrado";
                break;
            }
        }

        double memoriaRAM = 0;
        int frequenciaRAM = 0;
        int modulosRAM = 0;

        // Informações da memória RAM
        ManagementObjectSearcher memoria =
            new ManagementObjectSearcher(
                "SELECT Capacity, Speed FROM Win32_PhysicalMemory");

        ulong memoriaTotal = 0;

        foreach (ManagementObject ram in memoria.Get())
        {
            memoriaTotal += Convert.ToUInt64(ram["Capacity"]);
            frequenciaRAM = Convert.ToInt32(ram["Speed"]);
            modulosRAM++;
        }

        memoriaRAM = memoriaTotal /
                     (1024.0 * 1024.0 * 1024.0);


        string versaoWindows = "Não encontrado";
        string ativacaoWindows = "Não encontrado";

        // Informacoes Sistema
        ManagementObjectSearcher sistema =
            new ManagementObjectSearcher(
                "SELECT Caption, Version FROM Win32_OperatingSystem");

        foreach (ManagementObject os in sistema.Get())
        {
            string nome = os["Caption"]?.ToString().Replace("Microsoft ", "") ?? "";
            string versao = os["Version"]?.ToString() ?? "";

            versaoWindows = $"{nome} ({versao})";
        }

        ManagementObjectSearcher ativacao =
    new ManagementObjectSearcher(
        "SELECT LicenseStatus FROM SoftwareLicensingProduct " +
        "WHERE PartialProductKey IS NOT NULL " +
        "AND ApplicationID = '55c92734-d682-4d71-983e-d6ec3f16059f'");

        foreach (ManagementObject produto in ativacao.Get())
        {
            int status = Convert.ToInt32(produto["LicenseStatus"]);

            if (status == 1)
            {
                ativacaoWindows = "Ativado";
                break;
            }

            ativacaoWindows = "Não ativado";
        }

        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║            NEXORA > FERRAMENTAS > INFORMAÇÕES           ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
        Console.WriteLine("║");
        Console.WriteLine("║ Informações do Computador:");
        Console.WriteLine("║");
        Console.WriteLine($"║ Nome do Dispositivo: {Environment.MachineName}");
        Console.WriteLine("║");
        Console.WriteLine($"║ Processador: {nomeProcessador}");
        Console.WriteLine($"║ Frequência: {clockAtual:F2} GHz");
        Console.WriteLine($"║ Núcleos: {nucleos}");
        Console.WriteLine($"║ Threads: {threads}");
        Console.WriteLine("║");
        Console.WriteLine($"║ Placa de Vídeo: {nomeGPU}");
        Console.WriteLine($"║ VRAM: {vram:F2} GB");
        Console.WriteLine($"║ Driver: {driverGPU}");
        Console.WriteLine("║");
        Console.WriteLine($"║ Memória RAM: {memoriaRAM:F0} GB");
        Console.WriteLine($"║ Frequência: {frequenciaRAM} MHz");
        Console.WriteLine($"║ Módulos: {modulosRAM}");
        Console.WriteLine("║");
        Console.WriteLine($"║ Sistema Operacional: {versaoWindows}");
        Console.WriteLine($"║ Status Ativação: {ativacaoWindows}");
        Console.WriteLine("║");
        Console.WriteLine("║ 0. Voltar");
        Console.WriteLine("╚══════════════════════════════════════════════════════════");
    }
}