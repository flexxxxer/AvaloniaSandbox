using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

using MessageBox.Avalonia;

namespace AvaloniaSandbox
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif


            this.FindControl<Button>("OpenFileDialogButton").Click += async (sender, args) =>
            {
                try
                {
                    var fileDialog = new OpenFileDialog()
                    {
                        AllowMultiple = false
                    };

                    string[] files = await fileDialog.ShowAsync(this);
                   
                    // expected: files never is null
                    string view = string.Join(";", files.Select(s => s ?? "null"));
                    
                    await MessageBoxManager.GetMessageBoxStandardWindow("Files paths", $"{view}")
                        .ShowDialog(this);
                }
                catch (Exception e)
                {
                    await MessageBoxManager.GetMessageBoxStandardWindow("Exception", $"Message: {e.Message}")
                        .ShowDialog(this);
                }
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
