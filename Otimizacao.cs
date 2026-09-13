using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;

class Otimizacao
{
    private static readonly string[] servicos =
    {
        "SysMain",
        "DiagTrack",
        "lfsvc",
        "Spooler",
        "Fax",
        "XblAuthManager",
        "XblGameSave",
        "XboxGipSvc",
        "XboxNetApiSvc",
        "MapsBroker"
    };

    private static readonly string caminhoBackup =
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "NEXORA",
            "backup_servicos.txt"
        );

    public static void OtimizacaoCompleta()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║             NEXORA > OTIMIZAÇÃO COMPLETA                ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Esta função irá executar:                               ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ • Desativação de serviços selecionados                  ║");
        Console.WriteLine("║ • Limpeza de arquivos temporários                       ║");
        Console.WriteLine("║ • Limpeza da lixeira                                    ║");
        Console.WriteLine("║ • Limpeza do cache DNS                                  ║");
        Console.WriteLine("║ • Limpeza de arquivos do Windows Update                 ║");
        Console.WriteLine("║ • Limpeza de componentes do Windows                     ║");
        Console.WriteLine("║ • Limpeza da Otimização de Entrega                      ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ O estado original dos serviços será salvo para          ║");
        Console.WriteLine("║ permitir a restauração posteriormente.                  ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

        while (true)
        {
            string resposta = Validacao.LerTexto(
                "Deseja continuar? (s/n): "
            );

            if (resposta == "s" || resposta == "S")
            {
                SalvarBackup();
                DesativarServicos();

                Console.WriteLine();
                Console.WriteLine("Iniciando limpeza do sistema...");
                Console.WriteLine();

                LimpezaSistema.Limpar();

                Console.WriteLine();
                Console.WriteLine("Otimização completa concluída.");
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

    private static void SalvarBackup()
    {
        try
        {
            string pasta = Path.GetDirectoryName(caminhoBackup) ?? "";

            Directory.CreateDirectory(pasta);

            List<string> linhas = new List<string>();

            foreach (string servico in servicos)
            {
                string startup = ObterStartType(servico);
                string status = ObterStatus(servico);

                if (startup != "Não encontrado")
                {
                    linhas.Add(
                        $"{servico}|{startup}|{status}"
                    );
                }
            }

            File.WriteAllLines(
                caminhoBackup,
                linhas
            );
        }
        catch
        {
            Console.WriteLine(
                "Não foi possível salvar o backup dos serviços."
            );
        }
    }

    private static void DesativarServicos()
    {
        foreach (string servico in servicos)
        {
            if (servico == "Spooler")
            {
                Console.WriteLine();
                Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                     ATENÇÃO                            ║");
                Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
                Console.WriteLine("║ O Spooler de Impressão é necessário para o             ║");
                Console.WriteLine("║ funcionamento das impressoras do Windows.              ║");
                Console.WriteLine("║                                                         ║");
                Console.WriteLine("║ Se ele for desativado, as impressoras poderão          ║");
                Console.WriteLine("║ deixar de funcionar.                                   ║");
                Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

                while (true)
                {
                    string resposta = Validacao.LerTexto(
                        "Deseja desativar o Spooler? (s/n): "
                    );

                    if (resposta == "s" || resposta == "S")
                    {
                        DesativarServico(servico);
                        break;
                    }
                    else if (resposta == "n" || resposta == "N")
                    {
                        Console.WriteLine(
                            "Spooler mantido como está."
                        );
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Opção inválida!");
                    }
                }
            }
            else
            {
                DesativarServico(servico);
            }
        }
    }

    private static void DesativarServico(string servico)
    {
        Console.WriteLine(
            $"Desativando: {servico}..."
        );

        ExecutarComandos.ExecutarPowerShell(
            $"Stop-Service -Name \"{servico}\" " +
            "-Force -ErrorAction SilentlyContinue; " +
            $"Set-Service -Name \"{servico}\" " +
            "-StartupType Disabled"
        );
    }

    public static void RestaurarServicos()
    {
        if (!File.Exists(caminhoBackup))
        {
            Console.WriteLine(
                "Nenhum backup de serviços foi encontrado."
            );
            return;
        }

        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║             NEXORA > RESTAURAR SERVIÇOS                 ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ Os serviços serão restaurados para o estado anterior    ║");
        Console.WriteLine("║ à última otimização.                                    ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");

        while (true)
        {
            string resposta = Validacao.LerTexto(
                "Deseja continuar? (s/n): "
            );

            if (resposta == "s" || resposta == "S")
            {
                Restaurar();
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

    private static void Restaurar()
    {
        string[] linhas;

        try
        {
            linhas = File.ReadAllLines(caminhoBackup);
        }
        catch
        {
            Console.WriteLine(
                "Não foi possível ler o backup dos serviços."
            );
            return;
        }

        foreach (string linha in linhas)
        {
            string[] dados = linha.Split('|');

            if (dados.Length < 3)
                continue;

            string servico = dados[0];
            string startup = dados[1];
            string status = dados[2];

            Console.WriteLine(
                $"Restaurando: {servico}..."
            );

            string comando =
                $"Set-Service -Name \"{servico}\" " +
                $"-StartupType {startup}; ";

            if (status == "Running")
            {
                comando +=
                    $"Start-Service -Name \"{servico}\" " +
                    "-ErrorAction SilentlyContinue";
            }
            else if (status == "Stopped")
            {
                comando +=
                    $"Stop-Service -Name \"{servico}\" " +
                    "-Force -ErrorAction SilentlyContinue";
            }

            ExecutarComandos.ExecutarPowerShell(
                comando
            );
        }

        try
        {
            File.Delete(caminhoBackup);
        }
        catch
        {
            // Ignora erro ao apagar o backup.
        }

        Console.WriteLine();
        Console.WriteLine(
            "Serviços restaurados com sucesso."
        );
    }

    private static string ObterStatus(string servico)
    {
        ProcessStartInfo processo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments =
                "-NoProfile -Command " +
                $"\"$servico = Get-Service -Name '{servico}' " +
                "-ErrorAction SilentlyContinue; " +
                "if ($null -eq $servico) { " +
                "'Não encontrado' } else { $servico.Status }\"",

            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using (Process? resultado = Process.Start(processo))
        {
            if (resultado == null)
                return "Não encontrado";

            string saida =
                resultado.StandardOutput
                .ReadToEnd()
                .Trim();

            resultado.WaitForExit();

            return string.IsNullOrEmpty(saida)
                ? "Não encontrado"
                : saida;
        }
    }

    private static string ObterStartType(string servico)
    {
        ProcessStartInfo processo = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments =
                "-NoProfile -Command " +
                $"\"$servico = Get-Service -Name '{servico}' " +
                "-ErrorAction SilentlyContinue; " +
                "if ($null -eq $servico) { " +
                "'Não encontrado' } else { $servico.StartType }\"",

            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        using (Process? resultado = Process.Start(processo))
        {
            if (resultado == null)
                return "Não encontrado";

            string saida =
                resultado.StandardOutput
                .ReadToEnd()
                .Trim();

            resultado.WaitForExit();

            return string.IsNullOrEmpty(saida)
                ? "Não encontrado"
                : saida;
        }
    }
}