using System;
using System.Management;

class InformacoesPC
{
    public static void ExibirInformacoesPC()
    {

        string nomeProcessador = "Não encontrado";
        double clockAtual = 0;
        double clockMaximo = 0;
        int nucleos = 0;
        int threads = 0;

        ManagementObjectSearcher processador =
            new ManagementObjectSearcher("SELECT Name, CurrentClockSpeed, MaxClockSpeed, NumberOfCores, NumberOfLogicalProcessors FROM Win32_Processor");

        foreach (ManagementObject cpu in processador.Get())
        {
            foreach (PropertyData propriedade in cpu.Properties)
            {
                nomeProcessador = cpu["Name"]?.ToString() ?? "Não encontrado";

                clockAtual = Convert.ToDouble(cpu["CurrentClockSpeed"]) / 1000;
                clockMaximo = Convert.ToDouble(cpu["MaxClockSpeed"]) / 1000;

                nucleos = Convert.ToInt32(cpu["NumberOfCores"]);
                threads = Convert.ToInt32(cpu["NumberOfLogicalProcessors"]);
            }
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
        Console.WriteLine("║ Placa de Vídeo:");
        Console.WriteLine("║ VRAM:");
        Console.WriteLine("║");
        Console.WriteLine("║ Memória RAM:");
        Console.WriteLine("║ Frequência:");
        Console.WriteLine("║");
        Console.WriteLine("║ Sistema Operacional:");
        Console.WriteLine("║ Status Ativação:");
        Console.WriteLine("║");
        Console.WriteLine("║ 0. Voltar");
        Console.WriteLine("╚══════════════════════════════════════════════════════════");
    }
}