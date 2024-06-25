using System;
using System.Diagnostics;
using FlaUI.Core;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.Definitions;
using FlaUI.UIA3;

class Program
{
    static void Main(string[] args)
    {

        string processName = "inventario2"; // Exemplo com a Calculadora do Windows

        // Busca pelos processos com o nome específico
        Process[] processes = Process.GetProcessesByName(processName);
        int processId = 0;

        // Verifica se encontrou algum processo
        if (processes.Length > 0)
        {
            processId = processes[0].Id; // Assume o primeiro processo encontrado
            Console.WriteLine($"ID do processo encontrado: {processId}, Nome: {processes[0].ProcessName}");
        }
        else
        {
            Console.WriteLine($"Nenhum processo encontrado com o nome '{processName}'.");
            return;
        }


        // Usar UIA3 como a interface de automação
        using (var automation = new UIA3Automation())
        {
            // Encontra a janela da Calculadora usando o título (ou outro critério)
            var mainWindow = automation.GetDesktop().FindFirstChild(cf => cf.ByProcessId(processId))?.AsWindow();

            if (mainWindow != null)
            {
                // Foca a janela principal
                mainWindow.Focus();
                Console.WriteLine("Janela principal encontrada e focada.");
                var allButtons = mainWindow.FindAllDescendants(d => d.ByControlType(ControlType.Button));
                allButtons = mainWindow.FindAllDescendants(d => d.ByControlType(ControlType.Menu));
                allButtons = mainWindow.FindAllDescendants(d => d.ByControlType(ControlType.Unknown));
                allButtons = mainWindow.FindAllDescendants(d => d.ByControlType(ControlType.Spinner));
                allButtons = mainWindow.FindAllDescendants(d => d.ByControlType(ControlType.AppBar));
                allButtons = mainWindow.FindAllDescendants(d => d.ByControlType(ControlType.Calendar));
                // Encontra todos os elementos dentro da janela
                var allElements = mainWindow.FindAllDescendants();
                var subWindows = mainWindow.FindAllDescendants(d => d.ByControlType(ControlType.Window));

                // Encontrar todos os botões na janela da Calculadora
                var allButtons2 = mainWindow.FindAllDescendants(d => d.ByControlType(ControlType.Window));
                var button1 = allButtons.FirstOrDefault(b => b.Name.Equals("ok", StringComparison.OrdinalIgnoreCase));

                if (button1 != null)
                {
                    button1.Click();
                    Console.WriteLine("Botão '1' clicado.");
                }

                // Verifica se encontrou algum botão
                if (allButtons != null && allButtons.Length > 0)
                {
                    Console.WriteLine($"Encontrados {allButtons.Length} botões na janela.");

                    // Itera sobre os botões encontrados
                    foreach (var button in allButtons)
                    {
                        // Exibe o nome do botão
                        Console.WriteLine($"Nome do botão: {button.Properties.Name}");
                    }
                }
                else
                {
                    Console.WriteLine("Nenhum botão encontrado na janela.");
                }
            }
            else
            {
                Console.WriteLine("Janela principal da Calculadora não encontrada.");
            }
        }

        // Mantém o console aberto
        Console.WriteLine("Pressione qualquer tecla para sair...");
        Console.ReadKey();

    }
}
