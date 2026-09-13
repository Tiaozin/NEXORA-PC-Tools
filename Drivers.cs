using System;
using System.Diagnostics;

class Drivers
{
    public static void VerificarDrivers()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              NEXORA > FERRAMENTAS > DRIVERS            ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Detectando drivers instalados...                        ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

        ExecutarComandos.ExecutarPowerShell(
            "Get-CimInstance Win32_PnPSignedDriver | " +
            "Where-Object {$_.DeviceName -ne $null} | " +
            "Select-Object DeviceName, Manufacturer, DriverVersion | " +
            "Sort-Object DeviceName | " +
            "Format-Table -AutoSize"
        );
    }

    public static void InstalarNvidia()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              NEXORA > DRIVERS > NVIDIA                 ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Será instalado o aplicativo da NVIDIA para             ║");
        Console.WriteLine("║ gerenciamento e atualização dos drivers.               ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Tem certeza que deseja continuar? (s/n)                ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");

            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarCMD(
                    "winget install Nvidia.NVIDIAApp " +
                    "--exact --silent " +
                    "--accept-package-agreements " +
                    "--accept-source-agreements"
                );

                break;
            }
            else if (resposta == "n" || resposta == "N")
            {
                Console.WriteLine("Voltando...");
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }
        }
    }

    public static void InstalarAmdGpu()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║               NEXORA > DRIVERS > AMD GPU               ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Será instalado o AMD Software Adrenalin.               ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Tem certeza que deseja continuar? (s/n)                ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");

            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarCMD(
                    "winget install " +
                    "AdvancedMicroDevices.AMDSoftwareAdrenalinEdition " +
                    "--exact --silent " +
                    "--accept-package-agreements " +
                    "--accept-source-agreements"
                );

                break;
            }
            else if (resposta == "n" || resposta == "N")
            {
                Console.WriteLine("Voltando...");
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }
        }
    }

    public static void InstalarAmdProcessador()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║             NEXORA > DRIVERS > AMD CPU                 ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Esta opção procura o pacote de chipset AMD disponível. ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Tem certeza que deseja continuar? (s/n)                ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");

            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarCMD(
                    "winget search \"AMD Chipset Software\""
                );

                break;
            }
            else if (resposta == "n" || resposta == "N")
            {
                Console.WriteLine("Voltando...");
                break;
            }
            else
            {
                Console.WriteLine("Opção inválida!");
            }
        }
    }

    public static void VerificarAtualizacoes()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║          NEXORA > DRIVERS > ATUALIZAÇÕES               ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Procurando atualizações disponíveis...                  ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

        ExecutarComandos.ExecutarCMD(
            "winget upgrade"
        );
    }
}