using System;
using System.Windows;
using System.Windows.Controls;

namespace Zadanie_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Cipher[] Ciphers;

        public MainWindow()
        {
            InitializeComponent();
            Ciphers = new Cipher[]
            {
                new RailFence(),
                new MatrixTranspositionNumbers(),
                new MatrixTranspositionWord()
            };
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "1. Rail fence\n" +
                "wysokość płotu, np. 3\n\n" +
                "2. Przestawienia macierzowe z kluczem liczbowym\n" +
                "kolejność przepisywania znaków z kolumn macierzy do szyfrogramu, np. 3-1-4-2\n\n" +
                "3. Przestawienia macierzowe z kluczem słownym\n" +
                "słowo, na podstawie którego ustalana jest kolejność przepisywania kolumn do szyfrogramu, np. CONVENIENCE");
        }

        private void PerformButton_Click(object sender, RoutedEventArgs e)
        {
            int cipherIndex = CheckedIndex((CipherGroupBox.Content as StackPanel).Children);
            int operationIndex = CheckedIndex((OperationGroupBox.Content as StackPanel).Children);
            try
            {
                if (cipherIndex < 0 || cipherIndex > 2)
                    return;
                if (operationIndex == 0)
                    OutputTextBox.Text = Ciphers[cipherIndex].Encode(InputTextBox.Text, KeyTextBox.Text);
                else if (operationIndex == 1)
                    OutputTextBox.Text = Ciphers[cipherIndex].Decode(InputTextBox.Text, KeyTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int CheckedIndex(UIElementCollection radioButtons)
        {
            int index = 0;
            foreach (var rb in radioButtons)
            {
                if ((bool)(rb as RadioButton).IsChecked)
                    break;
                ++index;
            }
            return index;
        }
    }
}
