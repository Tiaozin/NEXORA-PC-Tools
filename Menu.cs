using System;
using System.Management;

class Menu
{
    // Monta uma linha de duas colunas (usada apenas no menu principal)
    private static string DuasColunas(string esquerda, string direita)
    {
        return esquerda.PadRight(27) + "| " + direita;
    }

    public static void ExibirMenuPrincipal()
    {
        Console.Clear();
        UI.Cabecalho("NEXORA - PC TOOLS");
        UI.Linha(DuasColunas("SISTEMAS", "FERRAMENTAS"));
        UI.LinhaVazia();
        UI.Linha(DuasColunas("1. Ativação", "6. Otimização"));
        UI.Linha(DuasColunas("2. Windows Defender", "7. Energia"));
        UI.Linha(DuasColunas("3. Windows Update", "8. Drivers"));
        UI.Linha(DuasColunas("4. Instalar Programas", "9. Informações do PC"));
        UI.Linha(DuasColunas("5. Desinstalar Programas", "10. Limpeza"));
        UI.LinhaVazia();
        UI.Item("0", "Sair");
        UI.Rodape();
        Console.WriteLine();
    }

    public static void ExibirSubMenuAtivacao()
    {
        UI.Cabecalho("NEXORA > SISTEMAS > ATIVAÇÃO");

        if (AtivacaoWindows.VerificarStatusWindows())
        {
            UI.Linha("Status do Windows: Ativado");
            UI.LinhaVazia();
            InformacoesPC.ExibirWindowsBasico();
        }
        else
        {
            UI.Linha("Status do Windows: Desativado");
            UI.LinhaVazia();
            UI.Item("1", "Ativar Windows");
        }

        UI.LinhaVazia();
        UI.Item("0", "Voltar");
        UI.Rodape();
    }

    public static void ExibirSubMenuDefender()
    {
        UI.Cabecalho("NEXORA > SISTEMAS > DEFENDER");

        if (WindowsDefender.VerificarStatus())
        {
            UI.Linha("Status do Windows Defender: Ativado");
            UI.LinhaVazia();
            UI.Item("1", "Desativar Windows Defender");
        }
        else
        {
            UI.Linha("Status do Windows Defender: Desativado");
            UI.LinhaVazia();
            UI.Item("1", "Ativar Windows Defender");
        }

        UI.LinhaVazia();
        UI.Item("0", "Voltar");
        UI.Rodape();
    }

    public static void ExibirSubMenuUpdate()
    {
        UI.Cabecalho("NEXORA > SISTEMAS > UPDATE");

        if (WindowsUpdate.VerificarStatus())
        {
            UI.Linha("Status do Windows Update: Ativado");
            UI.LinhaVazia();
            UI.Item("1", "Desativar Windows Update");
        }
        else
        {
            UI.Linha("Status do Windows Update: Desativado");
            UI.LinhaVazia();
            UI.Item("1", "Ativar Windows Update");
        }

        UI.LinhaVazia();
        UI.Item("0", "Voltar");
        UI.Rodape();
    }

    public static void ExibirSubMenuOtimizacao()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > OTIMIZAÇÃO");
        UI.Item("1", "Otimização completa");
        UI.Item("2", "Restaurar serviços");
        UI.LinhaVazia();
        UI.Item("0", "Voltar");
        UI.Rodape();
    }

    public static void ExibirSubMenuEnergia()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > ENERGIA");
        UI.Linha($"Plano atual: {Energia.ObterPlanoAtual()}");
        UI.LinhaVazia();
        UI.Item("1", "Ativar plano 'Desempenho Máximo'");
        UI.Item("2", "Ativar plano 'Alto Desempenho'");
        UI.Item("3", "Ativar plano 'Equilibrado'");
        UI.Item("4", "Ativar plano 'Economia de Energia'");
        UI.LinhaVazia();
        UI.Item("0", "Voltar");
        UI.Rodape();
    }

    public static void ExibirSubMenuDrivers()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > DRIVERS");
        UI.Item("1", "Verificar drivers");
        UI.Item("2", "Instalar Drivers NVIDIA");
        UI.Item("3", "Instalar Drivers AMD (GPU)");
        UI.Item("4", "Instalar Drivers AMD (Processador)");
        UI.Item("5", "Verificar atualizações de drivers");
        UI.LinhaVazia();
        UI.Item("0", "Voltar");
        UI.Rodape();
    }

    public static void ExibirSubMenuInformacoes()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > INFORMAÇÕES");
        UI.Item("1", "Informações Completas");
        UI.Item("2", "Informações Básicas");
        UI.LinhaVazia();
        UI.Item("3", "Processador");
        UI.Item("4", "Placa de Vídeo");
        UI.Item("5", "Memória RAM");
        UI.Item("6", "Armazenamento");
        UI.Item("7", "Windows");
        UI.LinhaVazia();
        UI.Item("0", "Voltar");
        UI.Rodape();
    }

    public static void ExibirSubMenuLimpeza()
    {
        UI.Cabecalho("NEXORA > FERRAMENTAS > LIMPEZA");
        UI.Item("1", "Esvaziar Lixeira");
        UI.Item("2", "Apagar Arquivos Temporários");
        UI.Item("3", "Limpar Cache do Windows");
        UI.Item("4", "Limpar Cache de DNS");
        UI.Item("5", "Limpar Arquivos de Atualização do Windows");
        UI.Item("6", "Limpar Miniaturas");
        UI.Item("7", "Limpar Relatórios de Erros");
        UI.LinhaVazia();
        UI.Item("8", "Limpeza Completa");
        UI.LinhaVazia();
        UI.Item("0", "Voltar");
        UI.Rodape();
    }
}