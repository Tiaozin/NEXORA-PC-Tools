using System;
using System.Management;
using Vortice.DXGI;

class InformacoesPC
{
    public static void ExibirInformacoesCompletas()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║            NEXORA > FERRAMENTAS > INFORMAÇÕES           ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
        Console.WriteLine("║");
        Console.WriteLine("║ Informações do Computador:");
        Console.WriteLine("║");
        Console.WriteLine($"║ Nome do Dispositivo: {Environment.MachineName}");
        Console.WriteLine("║");
        ExibirPlacaMae();
        Console.WriteLine("║");
        ExibirCPU();
        Console.WriteLine("║");
        ExibirGPU();
        Console.WriteLine("║");
        ExibirRAM();
        Console.WriteLine("║");
        ExibirArmazenamento();
        Console.WriteLine("║");
        ExibirWindows();
        ExibirRodape();
    }

    public static void ExibirInformacoesBasicas()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║       NEXORA > FERRAMENTAS > INFORMAÇÕES > BÁSICAS      ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
        Console.WriteLine("║");
        Console.WriteLine("║ Informações Básicas:");
        Console.WriteLine("║");
        Console.WriteLine($"║ Nome do Dispositivo: {Environment.MachineName}");
        ExibirCPUBasico();
        ExibirGPUBasico();
        ExibirRAMBasico();
        ExibirArmazenamentoBasico();
        ExibirWindowsBasico();
        ExibirRodape();
    }

    public static void ExibirInformacoesCPU()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║         NEXORA > FERRAMENTAS > INFORMAÇÕES > CPU        ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
        Console.WriteLine("║");
        ExibirCPU();
        ExibirRodape();
    }

    public static void ExibirInformacoesGPU()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║         NEXORA > FERRAMENTAS > INFORMAÇÕES > GPU        ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
        Console.WriteLine("║");
        ExibirGPU();
        ExibirRodape();
    }

    public static void ExibirInformacoesRAM()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║         NEXORA > FERRAMENTAS > INFORMAÇÕES > RAM        ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
        Console.WriteLine("║");
        ExibirRAM();
        ExibirRodape();
    }

    public static void ExibirInformacoesArmazenamento()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║    NEXORA > FERRAMENTAS > INFORMAÇÕES > ARMAZENAMENTO   ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
        Console.WriteLine("║");
        ExibirArmazenamento();
        ExibirRodape();
    }

    public static void ExibirInformacoesWindows()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║       NEXORA > FERRAMENTAS > INFORMAÇÕES > WINDOWS      ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╝");
        Console.WriteLine("║");
        ExibirWindows();
        ExibirRodape();
    }

    public static void ExibirPlacaMae()
    {
        string fabricante = "Não encontrado";
        string modelo = "Não encontrado";

        // Informações da placa-mãe
        ManagementObjectSearcher placaMae =
            new ManagementObjectSearcher(
                "SELECT Manufacturer, Product FROM Win32_BaseBoard");

        foreach (ManagementObject placa in placaMae.Get())
        {
            fabricante = placa["Manufacturer"]?.ToString() ?? "Não encontrado";
            modelo = placa["Product"]?.ToString() ?? "Não encontrado";
        }

        Console.WriteLine($"║ Placa-mãe: {fabricante} {modelo}");
    }

    public static void ExibirCPU()
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

        Console.WriteLine($"║ Processador: {nomeProcessador}");
        Console.WriteLine($"║ Frequência: {clockAtual:F2} GHz");
        Console.WriteLine($"║ Núcleos: {nucleos}");
        Console.WriteLine($"║ Threads: {threads}");

    }

    public static void ExibirCPUBasico()
    {
        string nomeProcessador = "Não encontrado";


        // Informações do processador
        ManagementObjectSearcher processador =
            new ManagementObjectSearcher("SELECT Name FROM Win32_Processor");

        foreach (ManagementObject cpu in processador.Get())
        {
            nomeProcessador = cpu["Name"]?.ToString() ?? "Não encontrado";
        }

        Console.WriteLine($"║ Processador: {nomeProcessador}");
    }

    public static void ExibirGPU()
    {
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

        Console.WriteLine($"║ Placa de Vídeo: {nomeGPU}");
        Console.WriteLine($"║ VRAM: {vram:F2} GB");
        Console.WriteLine($"║ Driver: {driverGPU}");

    }

    public static void ExibirGPUBasico()
    {
        string nomeGPU = "Não encontrado";

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
                }

                adapter.Dispose();
            }
        }

        Console.WriteLine($"║ Placa de Vídeo: {nomeGPU}");

    }

    public static void ExibirRAM()
    {
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

        Console.WriteLine($"║ Memória RAM: {memoriaRAM:F0} GB");
        Console.WriteLine($"║ Frequência: {frequenciaRAM} MHz");
        Console.WriteLine($"║ Módulos: {modulosRAM}");

    }

    public static void ExibirRAMBasico()
    {
        double memoriaRAM = 0;

        // Informações da memória RAM
        ManagementObjectSearcher memoria =
            new ManagementObjectSearcher(
                "SELECT Capacity, Speed FROM Win32_PhysicalMemory");

        ulong memoriaTotal = 0;

        foreach (ManagementObject ram in memoria.Get())
        {
            memoriaTotal += Convert.ToUInt64(ram["Capacity"]);
        }

        memoriaRAM = memoriaTotal /
                     (1024.0 * 1024.0 * 1024.0);

        Console.WriteLine($"║ Memória RAM: {memoriaRAM:F0} GB");

    }

    public static void ExibirWindows()
    {
        string versaoWindows = "Não encontrado";
        string ativacaoWindows = "Não encontrado";

        // Informacoes Sistema
        ManagementObjectSearcher sistema =
            new ManagementObjectSearcher(
                "SELECT Caption, Version FROM Win32_OperatingSystem");

        foreach (ManagementObject os in sistema.Get())
        {
            string nome = os["Caption"]?.ToString().Replace("Microsoft ", "") ?? "";

            versaoWindows = $"{nome}";
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

        Console.WriteLine($"║ Sistema Operacional: {versaoWindows}");
        Console.WriteLine($"║ Status Ativação: {ativacaoWindows}");
    }

    public static void ExibirWindowsBasico()
    {
        string versaoWindows = "Não encontrado";

        // Informacoes Sistema
        ManagementObjectSearcher sistema =
            new ManagementObjectSearcher(
                "SELECT Caption, Version FROM Win32_OperatingSystem");

        foreach (ManagementObject os in sistema.Get())
        {
            string nome = os["Caption"]?.ToString().Replace("Microsoft ", "") ?? "";

            versaoWindows = $"{nome}";
        }

        Console.WriteLine($"║ Sistema Operacional: {versaoWindows}");
    }

public static void ExibirArmazenamento()
{
    DriveInfo[] discos = DriveInfo.GetDrives();

    foreach (DriveInfo disco in discos)
    {
        if (!disco.IsReady)
            continue;

        double capacidade = disco.TotalSize /
                            (1024.0 * 1024.0 * 1024.0);

        double livre = disco.AvailableFreeSpace /
                       (1024.0 * 1024.0 * 1024.0);

        double usado = capacidade - livre;

        Console.WriteLine($"║ Unidade: {disco.Name}");
        Console.WriteLine($"║ Tipo: {disco.DriveType}");
        Console.WriteLine($"║ Capacidade: {capacidade:F0} GB");
        Console.WriteLine($"║ Usado: {usado:F0} GB");
        Console.WriteLine($"║ Livre: {livre:F0} GB");
        Console.WriteLine("║");
    }
}

public static void ExibirArmazenamentoBasico()
{
    DriveInfo[] discos = DriveInfo.GetDrives();

    foreach (DriveInfo disco in discos)
    {
        if (!disco.IsReady)
            continue;

        double capacidade = disco.TotalSize /
                            (1024.0 * 1024.0 * 1024.0);

        double livre = disco.AvailableFreeSpace /
                       (1024.0 * 1024.0 * 1024.0);

        Console.WriteLine($"║ Armazenamento {disco.Name}: {capacidade:F0} GB ({livre:F0} GB livres)");
    }
}
    public static void ExibirRodape()
    {
        Console.WriteLine("║                                                          ");
        Console.WriteLine("║ 0. Voltar                                                ");
        Console.WriteLine("╚══════════════════════════════════════════════════════════");
    }
}