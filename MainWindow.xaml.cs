using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace NEXUS_PC_Tools
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Log("Carregando informações do sistema...");

            var dados = await Task.Run(() => InformacoesPC.ObterDadosCompletos());
            string plano = await Task.Run(() => Energia.ObterPlanoAtual());
            bool ativado = await Task.Run(() => AtivacaoWindows.VerificarStatusWindows());

            TxtDispositivo.Text = dados.Dispositivo;
            TxtCpu.Text = dados.Cpu;
            TxtCpuDetalhes.Text = dados.CpuDetalhes;
            TxtGpu.Text = dados.Gpu;
            TxtGpuDetalhes.Text = dados.GpuDetalhes;
            TxtRam.Text = dados.Ram;
            TxtRamDetalhes.Text = dados.RamDetalhes;
            TxtPlacaMae.Text = dados.PlacaMae;
            TxtWindows.Text = $"{dados.Windows} ({dados.StatusAtivacao})";
            ListaDiscos.ItemsSource = dados.Discos;

            TxtPlanoAtual.Text = $"Plano atual: {plano}";
            TxtStatusAtivacao.Text = ativado ? "Status do Windows: Ativado ✓" : "Status do Windows: Não ativado";

            Log("Pronto.");
        }

        private void Nav_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not string tag) return;

            PanelInfo.Visibility = tag == "Info" ? Visibility.Visible : Visibility.Collapsed;
            PanelAtivacao.Visibility = tag == "Ativacao" ? Visibility.Visible : Visibility.Collapsed;
            PanelProgramas.Visibility = tag == "Programas" ? Visibility.Visible : Visibility.Collapsed;
            PanelOtimizacao.Visibility = tag == "Otimizacao" ? Visibility.Visible : Visibility.Collapsed;
            PanelEnergia.Visibility = tag == "Energia" ? Visibility.Visible : Visibility.Collapsed;
            PanelDrivers.Visibility = tag == "Drivers" ? Visibility.Visible : Visibility.Collapsed;
            PanelLimpeza.Visibility = tag == "Limpeza" ? Visibility.Visible : Visibility.Collapsed;

            var normalStyle = (Style)FindResource("MenuBtn");
            var activeStyle = (Style)FindResource("MenuBtnActive");

            foreach (var child in SidebarMenu.Children)
            {
                if (child is Button navBtn)
                    navBtn.Style = navBtn == btn ? activeStyle : normalStyle;
            }
        }

        private void Log(string msg)
        {
            Dispatcher.Invoke(() =>
            {
                TxtLog.AppendText($"[{DateTime.Now:HH:mm:ss}] {msg}{Environment.NewLine}");
                TxtLog.ScrollToEnd();
            });
        }

        private void BtnLimparLog_Click(object sender, RoutedEventArgs e) => TxtLog.Clear();

        // --- ATIVAÇÃO ---
        private async void BtnAtivarWindows_Click(object sender, RoutedEventArgs e)
        {
            Log("Iniciando módulo de ativação...");
            await Task.Run(() => AtivacaoWindows.ExecutarAtivacao());
            bool ativado = await Task.Run(() => AtivacaoWindows.VerificarStatusWindows());
            TxtStatusAtivacao.Text = ativado ? "Status do Windows: Ativado ✓" : "Status do Windows: Não ativado";
        }

        // --- PROGRAMAS (WINGET) ---
        private async void BtnInstalarSelecionados_Click(object sender, RoutedEventArgs e)
        {
            var ids = new List<string>();
            foreach (var child in WrapAppsRapidos.Children)
            {
                if (child is CheckBox cb && cb.IsChecked == true && cb.Tag is string id)
                    ids.Add(id);
            }

            if (ids.Count == 0)
            {
                Log("Marque pelo menos um programa na lista.");
                return;
            }

            foreach (string id in ids)
            {
                Log($"Instalando {id} via WinGet...");
                await Task.Run(() => ExecutarComandos.ExecutarWinget(
                    $"install -e --id {id} --silent --accept-package-agreements --accept-source-agreements", Log));
            }
            Log("✓ Instalação concluída.");
        }

        private async void BtnWingetUpgradeAll_Click(object sender, RoutedEventArgs e)
        {
            Log("Atualizando todos os programas via WinGet...");
            await Task.Run(() => ExecutarComandos.ExecutarWinget(
                "upgrade --all --silent --accept-package-agreements --accept-source-agreements", Log));
            Log("✓ Atualização concluída.");
        }

        private async void BtnWingetSearch_Click(object sender, RoutedEventArgs e)
        {
            string termo = TxtPacoteWinget.Text.Trim();
            if (string.IsNullOrEmpty(termo)) return;
            Log($"Buscando '{termo}' no WinGet...");
            await Task.Run(() => ExecutarComandos.ExecutarWinget($@"search ""{termo}""", Log));
        }

        private async void BtnWingetInstallId_Click(object sender, RoutedEventArgs e)
        {
            string pacote = TxtPacoteWinget.Text.Trim();
            if (string.IsNullOrEmpty(pacote)) return;
            Log($"Instalando '{pacote}'...");
            await Task.Run(() => ExecutarComandos.ExecutarWinget(
                $@"install ""{pacote}"" --silent --accept-package-agreements --accept-source-agreements", Log));
        }

        private async void BtnWingetUninstallId_Click(object sender, RoutedEventArgs e)
        {
            string pacote = TxtPacoteWinget.Text.Trim();
            if (string.IsNullOrEmpty(pacote)) return;
            Log($"Desinstalando '{pacote}'...");
            await Task.Run(() => ExecutarComandos.ExecutarWinget($@"uninstall ""{pacote}"" --silent", Log));
        }

        // --- OTIMIZAÇÃO ---
        private async void BtnOtimizacaoCompleta_Click(object sender, RoutedEventArgs e)
        {
            bool spooler = ChkDesativarSpooler.IsChecked == true;
            await Task.Run(() => Otimizacao.OtimizacaoCompleta(spooler, Log));
        }

        private async void BtnRestaurarServicos_Click(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => Otimizacao.RestaurarServicos(Log));
        }

        // --- ENERGIA ---
        private async void AlterarEnergia(Action<Action<string>> acao)
        {
            await Task.Run(() => acao(Log));
            string atual = await Task.Run(() => Energia.ObterPlanoAtual());
            TxtPlanoAtual.Text = $"Plano atual: {atual}";
        }

        private void BtnEnergiaMax_Click(object sender, RoutedEventArgs e) => AlterarEnergia(Energia.AtivarDesempenhoMaximo);
        private void BtnEnergiaAlto_Click(object sender, RoutedEventArgs e) => AlterarEnergia(Energia.AtivarAltoDesempenho);
        private void BtnEnergiaEquilibrado_Click(object sender, RoutedEventArgs e) => AlterarEnergia(Energia.AtivarEquilibrado);
        private void BtnEnergiaEconomia_Click(object sender, RoutedEventArgs e) => AlterarEnergia(Energia.AtivarEconomia);

        // --- DRIVERS ---
        private async void BtnVerificarDrivers_Click(object sender, RoutedEventArgs e) => await Task.Run(() => Drivers.VerificarDrivers(Log));
        private void BtnNvidia_Click(object sender, RoutedEventArgs e) => Drivers.InstalarNvidia(Log);
        private void BtnAmdGpu_Click(object sender, RoutedEventArgs e) => Drivers.InstalarAmdGpu(Log);
        private void BtnAmdCpu_Click(object sender, RoutedEventArgs e) => Drivers.InstalarAmdProcessador(Log);
        private void BtnWinUpdateDrivers_Click(object sender, RoutedEventArgs e) => Drivers.VerificarAtualizacoes(Log);

        // --- LIMPEZA ---
        private async void BtnLimparTudo_Click(object sender, RoutedEventArgs e) => await Task.Run(() => LimpezaSistema.Limpar(Log));
        private async void BtnLimparLixeira_Click(object sender, RoutedEventArgs e) => await Task.Run(() => LimpezaSistema.ExecutarLimparLixeira(Log));
        private async void BtnLimparTemp_Click(object sender, RoutedEventArgs e) => await Task.Run(() => LimpezaSistema.ExecutarLimparTemporarios(Log));
        private async void BtnLimparCleanmgr_Click(object sender, RoutedEventArgs e) => await Task.Run(() => LimpezaSistema.ExecutarLimparCleanmgr(Log));
        private async void BtnLimparDns_Click(object sender, RoutedEventArgs e) => await Task.Run(() => LimpezaSistema.ExecutarLimparDNS(Log));
        private async void BtnLimparWU_Click(object sender, RoutedEventArgs e) => await Task.Run(() => LimpezaSistema.ExecutarLimparDownloadsWindowsUpdate(Log));
        private async void BtnLimparDism_Click(object sender, RoutedEventArgs e) => await Task.Run(() => LimpezaSistema.ExecutarLimparComponentesWindows(Log));
        private async void BtnLimparDelivery_Click(object sender, RoutedEventArgs e) => await Task.Run(() => LimpezaSistema.ExecutarLimparDeliveryOptimization(Log));
        private async void BtnLimparMiniaturas_Click(object sender, RoutedEventArgs e) => await Task.Run(() => LimpezaSistema.ExecutarLimparMiniaturas(Log));
        private async void BtnLimparWer_Click(object sender, RoutedEventArgs e) => await Task.Run(() => LimpezaSistema.ExecutarLimparRelatoriosErros(Log));
    }
}
