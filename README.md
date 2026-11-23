# NEXORA PC Tools

> 🛠️ Ferramenta para manutenção, otimização, limpeza, energia e diagnóstico do Windows com interface gráfica.

**Status:** 🚀 Interface gráfica ativa (WPF / .NET 8)

O **NEXORA PC Tools** é um utilitário desenvolvido em **C#** com **WPF** sobre **.NET 8** para reunir rotinas de manutenção, otimização, limpeza do sistema, gerenciamento de energia, drivers, gerenciamento de programas via **WinGet** e informações de hardware em um único aplicativo.

## 📌 Funcionalidades

- [x] **Informações do PC:** exibição de processador e memória via WMI, placa de vídeo via Vortice.DXGI, placa-mãe, sistema operacional e armazenamento.
- [x] **Ativação do Windows:** verificação do status atual de ativação e acesso ao assistente.
- [ ] **Programas (WinGet):** instalação rápida em lote, atualização geral (`upgrade --all`), busca de pacotes, instalação e desinstalação por ID. (Está com problemas)
- [x] **Otimização do Sistema:** desativação de serviços selecionados, backup automático e restauração de serviços.
- [x] **Planos de Energia:** alternância rápida entre Desempenho Máximo, Alto Desempenho, Equilibrado e Economia de Energia.
- [x] **Drivers e Dispositivos:** verificação de dispositivos e atalhos para portais de drivers e Windows Update.
- [x] **Limpeza do Windows:** remoção de arquivos temporários, esvaziamento de lixeira, limpeza de cache DNS, downloads do Windows Update, componentes DISM, cache de miniaturas, relatórios de erro e limpeza completa.

## 💻 Tecnologias e Dependências

- **Linguagem / Framework:** C# e .NET 8 (`net8.0-windows` com WPF)
- **Pacotes NuGet:** `System.Management` e `Vortice.DXGI`
- **Privilégios:** execução em Modo Administrador configurada via `app.manifest`
- **Gerenciador de pacotes:** WinGet (Windows Package Manager)

## ▶️ Como rodar no PC (Agora tem o zip em release para baixar diretamente)

### 1. Pré-requisitos

- **Sistema operacional:** Windows 10 ou Windows 11 (64 bits)
- **.NET 8 SDK:** baixe e instale o [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) para Windows
- **WinGet (opcional para a aba de programas):** disponível na Microsoft Store (*App Installer*) ou já incluído nas versões mais recentes do Windows

### 2. Baixar ou clonar o projeto

Se estiver usando Git, abra o terminal (PowerShell) e clone o repositório:

```powershell
git clone https://github.com/Tiaozin/NEXORA-PC-Tools.git
cd NEXORA-PC-Tools
```

> Caso tenha baixado o projeto em arquivo `.zip`, extraia a pasta no seu PC e abra o PowerShell dentro da pasta onde está o arquivo `NEXORA PC Tools.csproj`.

### 3. Restaurar pacotes e executar

No PowerShell, dentro da pasta do projeto, rode:

```powershell
dotnet restore
dotnet run
```

O .NET vai baixar as dependências (`System.Management` e `Vortice.DXGI`), compilar o projeto WPF e abrir a janela do **NEXORA PC Tools** (aceite o aviso de Administrador do Windows se o UAC solicitar).

### 4. Gerar executável avulso (opcional)

Se quiser compilar uma versão *Release* ou gerar um executável pronto fora da pasta do código-fonte:

```powershell
dotnet build -c Release
```

Ou publique uma pasta autocontida:

```powershell
dotnet publish -c Release -r win-x64 --self-contained false
```

Os arquivos compilados ficam dentro da pasta `bin\Release\net8.0-windows\` (ou na pasta `publish` indicada no terminal).

> **Observação:** as pastas `bin` e `obj` são criadas automaticamente pelo .NET durante o build e costumam ficar ignoradas no Git.

---

**NEXORA PC Tools**
