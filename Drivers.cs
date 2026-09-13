using System;
using System.Diagnostics;
using System.Management;

class Drivers
{
    public static void VerificarDrivers()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > DRIVERS");
        UI.Linha("Verificando drivers de vídeo instalados...");
        UI.LinhaVazia();

        try
        {
            ManagementObjectSearcher gpus = new ManagementObjectSearcher(
                "SELECT Name, DriverVersion, DriverDate, Status FROM Win32_VideoController"
            );

            foreach (ManagementObject gpu in gpus.Get())
            {
                string nome = gpu["Name"]?.ToString() ?? "Não encontrado";
                string versao = gpu["DriverVersion"]?.ToString() ?? "Não encontrado";
                string status = gpu["Status"]?.ToString() ?? "Não encontrado";
                string data = "Não encontrado";

                string? dataBruta = gpu["DriverDate"]?.ToString();

                if (!string.IsNullOrEmpty(dataBruta) && dataBruta.Length >= 8)
                {
                    string ano = dataBruta.Substring(0, 4);
                    string mes = dataBruta.Substring(4, 2);
                    string dia = dataBruta.Substring(6, 2);

                    data = $"{dia}/{mes}/{ano}";
                }

                UI.Linha($"Placa de Vídeo: {nome}");
                UI.Linha($"Versão do Driver: {versao}");
                UI.Linha($"Data do Driver: {data}");
                UI.Linha($"Status: {status}");
                UI.LinhaVazia();
            }
        }
        catch
        {
            UI.Linha("⚠ Não foi possível obter os drivers de vídeo.");
            UI.LinhaVazia();
        }

        UI.Linha("Verificando dispositivos com problemas...");
        UI.LinhaVazia();

        int problemas = 0;

        try
        {
            ManagementObjectSearcher dispositivos = new ManagementObjectSearcher(
                "SELECT Name FROM Win32_PNPEntity WHERE ConfigManagerErrorCode != 0"
            );

            foreach (ManagementObject dispositivo in dispositivos.Get())
            {
                string nome = dispositivo["Name"]?.ToString() ?? "Desconhecido";

                UI.Linha($"⚠ {nome}");
                problemas++;
            }

            if (problemas == 0)
                UI.Linha("✓ Nenhum dispositivo com problema encontrado.");
        }
        catch
        {
            UI.Linha("⚠ Não foi possível verificar os dispositivos.");
        }

        UI.Rodape();
    }

    public static void InstalarNvidia()
    {
        AbrirPaginaDrivers(
            "NVIDIA",
            "Instalar Drivers NVIDIA",
            "https://www.nvidia.com/Download/index.aspx"
        );
    }

    public static void InstalarAmdGpu()
    {
        AbrirPaginaDrivers(
            "AMD (Placa de Vídeo)",
            "Instalar Drivers AMD (GPU)",
            "https://www.amd.com/pt/support"
        );
    }

    public static void InstalarAmdProcessador()
    {
        AbrirPaginaDrivers(
            "AMD (Chipset/Processador)",
            "Instalar Drivers AMD (Processador)",
            "https://www.amd.com/pt/support/chipsets"
        );
    }

    private static void AbrirPaginaDrivers(string fabricante, string titulo, string url)
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > DRIVERS");
        UI.Linha(titulo);
        UI.LinhaVazia();
        UI.Linha("Isso abrirá o site oficial do fabricante no navegador");
        UI.Linha("para você baixar e instalar o driver mais recente.");
        UI.LinhaVazia();
        UI.Linha($"Deseja abrir a página de drivers {fabricante}? (s/n)");
        UI.Rodape();

        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");

            if (resposta == "s" || resposta == "S")
            {
                try
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true
                    });

                    Console.WriteLine("Página aberta no navegador padrão.");
                }
                catch
                {
                    Console.WriteLine(
                        $"Não foi possível abrir o navegador. Acesse manualmente: {url}"
                    );
                }

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
        UI.Cabecalho("NEXORA > FERRAMENTAS > DRIVERS");
        UI.Linha("Verificar atualizações de drivers");
        UI.LinhaVazia();
        UI.Linha("O Windows Update será aberto para que você possa");
        UI.Linha("verificar as atualizações opcionais de drivers.");
        UI.LinhaVazia();
        UI.Linha("Deseja continuar? (s/n)");
        UI.Rodape();

        while (true)
        {
            string resposta = Validacao.LerTexto("Digite uma opção: ");

            if (resposta == "s" || resposta == "S")
            {
                ExecutarComandos.ExecutarCMD(
                    "start ms-settings:windowsupdate-action"
                );

                Console.WriteLine(
                    "Verifique as atualizações opcionais de drivers na tela do Windows Update."
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
}