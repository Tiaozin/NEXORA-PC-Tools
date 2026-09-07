using System;
using System.Management;

class Menu
{
    public static void ExibirMenuPrincipal()
    {
        Console.Clear();
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                         NEXORA                          ║");
        Console.WriteLine("║                        PC Tools                         ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║         SISTEMAS           |        FERRAMENTAS         ║");
        Console.WriteLine("║                            |                            ║");
        Console.WriteLine("║ 1. Ativação                | 6. Otimização              ║");
        Console.WriteLine("║ 2. Windows Defender        | 7. Energia                 ║");
        Console.WriteLine("║ 3. Windows Update          | 8. Drivers                 ║");
        Console.WriteLine("║ 4. Instalar Programas      | 9. Informações do PC       ║");
        Console.WriteLine("║ 5. Desinstalar             | 10. Limpeza                ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 0. Sair                                                 ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
        Console.WriteLine();
    }

    public static void ExibirSubMenuAtivacao()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║               NEXORA > SISTEMAS > ATIVAÇÃO              ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 1. Ativar Windows                                       ║");
        Console.WriteLine("║ 2. Verificar status de ativação                         ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 0. Voltar                                               ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
    }

    public static void ExibirSubMenuDefender()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║               NEXORA > SISTEMAS > DEFENDER              ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 1. Ativar Windows Defender                              ║");
        Console.WriteLine("║ 2. Desativar Windows Defender                           ║");
        Console.WriteLine("║ 3. Verificar status Windows Defender                    ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 0. Voltar                                               ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
    }

    public static void ExibirSubMenuUpdate()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║                NEXORA > SISTEMAS > UPDATE               ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 1. Ativar Windows Update                                ║");
        Console.WriteLine("║ 2. Desativar Windows Update                             ║");
        Console.WriteLine("║ 3. Verificar status Windows Update                      ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 0. Voltar                                               ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
    }

    public static void ExibirSubMenuOtimizacao()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║             NEXORA > FERRAMENTAS > OTIMIZAÇÃO           ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 1. Otimização completa                                  ║");
        Console.WriteLine("║ 2. Desativar serviços desnecessários                    ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 0. Voltar                                               ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
    }

    public static void ExibirSubMenuEnergia()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              NEXORA > FERRAMENTAS > ENERGIA             ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 1. Ativar plano de energia 'Desempenho Máximo'          ║");
        Console.WriteLine("║ 2. Ativar 'Alto Desempenho'                             ║");
        Console.WriteLine("║ 3. Ativar 'Equilibrado'                                 ║");
        Console.WriteLine("║ 4. Verificar Plano Atual                                ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 0. Voltar                                               ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
    }

    public static void ExibirSubMenuDrivers()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              NEXORA > FERRAMENTAS > DRIVERS             ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 1. Instalar Drivers NVIDIA                              ║");
        Console.WriteLine("║ 2. Instalar Drivers AMD (Placa de Vídeo)                ║");
        Console.WriteLine("║ 3. Instalar Drivers AMD (Processador)                   ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 0. Voltar                                               ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
    }

    public static void ExibirSubMenuInformacoes()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║            NEXORA > FERRAMENTAS > INFORMAÇÕES           ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 1. Informações Completas                                ║");
        Console.WriteLine("║ 2. Informações Básicas                                  ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 3. Processador                                          ║");
        Console.WriteLine("║ 4. Placa de Vídeo                                       ║");
        Console.WriteLine("║ 5. Memória RAM                                          ║");
        Console.WriteLine("║ 6. Armazenamento                                        ║");
        Console.WriteLine("║ 7. Windows                                              ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 0. Voltar                                               ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
    }

    public static void ExibirSubMenuLimpeza()
    {
        Console.WriteLine("╔═════════════════════════════════════════════════════════╗");
        Console.WriteLine("║              NEXORA > FERRAMENTAS > LIMPEZA             ║");
        Console.WriteLine("╠═════════════════════════════════════════════════════════╣");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 1. Esvaziar Lixeira                                     ║");
        Console.WriteLine("║ 2. Apagar Arquivos Temporários                          ║");
        Console.WriteLine("║ 3. Limpar Cache do Windows                              ║");
        Console.WriteLine("║ 4. Limpar Cache de DNS                                  ║");
        Console.WriteLine("║ 5. Limpar Arquivos de Atualização do Windows            ║");
        Console.WriteLine("║ 6. Limpar Miniaturas                                    ║");
        Console.WriteLine("║ 7. Limpar Relatórios de Erros                           ║");
        Console.WriteLine("║ 8. Limpeza Completa Segura                              ║");
        Console.WriteLine("║                                                         ║");
        Console.WriteLine("║ 0. Voltar                                               ║");
        Console.WriteLine("╚═════════════════════════════════════════════════════════╝");
    }

}