using System;
using System.Collections.Generic;
using System.IO;
using System.Management;
using Vortice.DXGI;

public class DiscoInfo
{
    public string Resumo { get; set; } = "";
}

public class DadosPC
{
    public string Dispositivo { get; set; } = Environment.MachineName;
    public string PlacaMae { get; set; } = "Não encontrado";
    public string Cpu { get; set; } = "Não encontrado";
    public string CpuDetalhes { get; set; } = "";
    public string Gpu { get; set; } = "Não encontrado";
    public string GpuDetalhes { get; set; } = "";
    public string Ram { get; set; } = "Não encontrado";
    public string RamDetalhes { get; set; } = "";
    public string Windows { get; set; } = "Não encontrado";
    public string StatusAtivacao { get; set; } = "Não encontrado";
    public List<DiscoInfo> Discos { get; set; } = new();
}

class InformacoesPC
{
    public static DadosPC ObterDadosCompletos()
    {
        var dados = new DadosPC();

        // Placa-mãe
        using (var placaMae = new ManagementObjectSearcher("SELECT Manufacturer, Product FROM Win32_BaseBoard"))
        {
            foreach (ManagementObject placa in placaMae.Get())
            {
                string fab = placa["Manufacturer"]?.ToString() ?? "Não encontrado";
                string mod = placa["Product"]?.ToString() ?? "Não encontrado";
                dados.PlacaMae = $"{fab} {mod}".Trim();
            }
        }

        // Processador
        using (var processador = new ManagementObjectSearcher("SELECT Name, CurrentClockSpeed, NumberOfCores, NumberOfLogicalProcessors FROM Win32_Processor"))
        {
            foreach (ManagementObject cpu in processador.Get())
            {
                dados.Cpu = cpu["Name"]?.ToString()?.Trim() ?? "Não encontrado";
                double clock = Convert.ToDouble(cpu["CurrentClockSpeed"]) / 1000.0;
                int nucleos = Convert.ToInt32(cpu["NumberOfCores"]);
                int threads = Convert.ToInt32(cpu["NumberOfLogicalProcessors"]);
                dados.CpuDetalhes = $"{clock:F2} GHz • {nucleos} Núcleos / {threads} Threads";
            }
        }

        // GPU via Vortice.DXGI
        string nomeGPU = "Não encontrado";
        double vram = 0;

        using (IDXGIFactory1 factory = DXGI.CreateDXGIFactory1<IDXGIFactory1>())
        {
            ulong maiorMemoria = 0;
            for (uint i = 0; ; i++)
            {
                if (factory.EnumAdapters1(i, out IDXGIAdapter1 adapter).Failure)
                    break;

                AdapterDescription1 descricao = adapter.Description1;
                if ((descricao.Flags & AdapterFlags.Software) != 0)
                {
                    adapter.Dispose();
                    continue;
                }

                ulong memoriaBytes = (ulong)(nuint)descricao.DedicatedVideoMemory;
                if (memoriaBytes > maiorMemoria)
                {
                    maiorMemoria = memoriaBytes;
                    nomeGPU = descricao.Description.Trim();
                    vram = memoriaBytes / (1024.0 * 1024.0 * 1024.0);
                }
                adapter.Dispose();
            }
        }

        string driverGPU = "Não encontrado";
        using (var driver = new ManagementObjectSearcher("SELECT Name, DriverVersion FROM Win32_VideoController"))
        {
            foreach (ManagementObject gpu in driver.Get())
            {
                if (gpu["Name"]?.ToString()?.Trim() == nomeGPU)
                {
                    driverGPU = gpu["DriverVersion"]?.ToString() ?? "Não encontrado";
                    break;
                }
            }
        }
        dados.Gpu = nomeGPU;
        dados.GpuDetalhes = $"VRAM: {vram:F2} GB • Driver: {driverGPU}";

        // Memória RAM
        using (var memoria = new ManagementObjectSearcher("SELECT Capacity, Speed FROM Win32_PhysicalMemory"))
        {
            ulong memoriaTotal = 0;
            int freq = 0;
            int modulos = 0;

            foreach (ManagementObject ram in memoria.Get())
            {
                memoriaTotal += Convert.ToUInt64(ram["Capacity"]);
                freq = Convert.ToInt32(ram["Speed"]);
                modulos++;
            }

            double gb = memoriaTotal / (1024.0 * 1024.0 * 1024.0);
            dados.Ram = $"{gb:F0} GB";
            dados.RamDetalhes = $"{freq} MHz • {modulos} módulo(s)";
        }

        // Sistema Operacional e Ativação
        using (var sistema = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem"))
        {
            foreach (ManagementObject os in sistema.Get())
            {
                dados.Windows = os["Caption"]?.ToString()?.Replace("Microsoft ", "").Trim() ?? "Não encontrado";
            }
        }

        using (var ativacao = new ManagementObjectSearcher(
            "SELECT LicenseStatus FROM SoftwareLicensingProduct " +
            "WHERE PartialProductKey IS NOT NULL AND ApplicationID = '55c92734-d682-4d71-983e-d6ec3f16059f'"))
        {
            foreach (ManagementObject produto in ativacao.Get())
            {
                int status = Convert.ToInt32(produto["LicenseStatus"]);
                if (status == 1)
                {
                    dados.StatusAtivacao = "Ativado";
                    break;
                }
                dados.StatusAtivacao = "Não ativado";
            }
        }

        // Armazenamento
        foreach (DriveInfo disco in DriveInfo.GetDrives())
        {
            if (!disco.IsReady) continue;

            double cap = disco.TotalSize / (1024.0 * 1024.0 * 1024.0);
            double livre = disco.AvailableFreeSpace / (1024.0 * 1024.0 * 1024.0);
            double usado = cap - livre;

            dados.Discos.Add(new DiscoInfo
            {
                Resumo = $"{disco.Name} ({disco.DriveType}) — Total: {cap:F2} GB | Usado: {usado:F2} GB | Livre: {livre:F2} GB"
            });
        }

        return dados;
    }
}