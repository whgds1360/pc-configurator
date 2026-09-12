using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using PcConfigurator.services.WindowsManager;
using PcConfigurator.windows.Configurator;
using PcConfigurator.shared.DataBase;
using Dapper;
using PcConfigurator.helpers.TableStructures;
using System.Collections.Generic;
using System;
using Avalonia.Media;

namespace PcConfigurator.windows.Main;

internal partial class MainWindow : Window
{
    private Button? currentConfiguration = null;
    private List<Config> configs = new();

    public MainWindow()
    {
        InitializeComponent();
        Loaded += getConfigs;
    }

    private void getConfigs(object? sender, RoutedEventArgs e)
    {
        Configuration.Children.Clear();

        using(var connect = DataBaseManager.GetConnection())
        {
            configs = connect.Query<Config>("SELECT * FROM `configs`").ToList();
        }

        foreach (var config in configs)
        {
            Button btn = new();
            btn.Content = config.Name;
            btn.Classes.Add("Configuration");
            btn.Click += selectHandler;
            
            Configuration.Children.Add(btn);
        }   
    }

    private void selectHandler(object? sender, RoutedEventArgs e)
    {   
        if (sender is Button)
        {   
            currentConfiguration?.Background = Brush.Parse("#3F4347");

            currentConfiguration = (Button)sender;
            currentConfiguration.Background = Brush.Parse("#2b0179");
        }
    }

    private void CreateHandler(object? sender, RoutedEventArgs e)
    {
        var nextWindow = new ConfiguratorWindow();

        MainWindowsManager.changeWindow(closingWindow: this, newWindow: nextWindow);
    }

    private void DeleteHandler(object? sender, RoutedEventArgs e)
    {
        using(var connect = DataBaseManager.GetConnection())
        {   
            try 
            {
                if (currentConfiguration is null) return;

                var affected = connect.Execute(
                    "DELETE FROM `configs` WHERE Name = @Name",
                    new { Name = currentConfiguration.Content });

                if (affected == 0)
                {
                    System.Diagnostics.Debug.WriteLine("Запись не найдена");
                    return;
                }

                Configuration.Children.Remove(currentConfiguration);
                currentConfiguration = null;
            }
            catch (Exception error)
            {
                System.Diagnostics.Debug.WriteLine($"Ошибка удаления: {error}");
            }
        }
    }
}