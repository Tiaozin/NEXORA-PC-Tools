using System;

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
        Console.WriteLine("║ 2. Defender                | 7. Energia                 ║");
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

}