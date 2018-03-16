using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Reflection;
namespace InputManagerAdder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool replace;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists(Directory.GetCurrentDirectory() + "/InputManager.asset"))
            {
                using (FileStream fs = File.Open(Directory.GetCurrentDirectory() + "/InputManager.asset", FileMode.Open))
                {
                    byte[] bytes = new byte[fs.Length];
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        bytes[i] = (byte)fs.ReadByte();
                    }
                    File.WriteAllBytes(Directory.GetCurrentDirectory() + "/InputManagerBackup.asset", bytes);
				}
            }
            if (!replace)
            {
				string addText = Encoding.ASCII.GetString(global::InputManagerAdder.Properties.Resources.InputManager);
				File.AppendAllText(Directory.GetCurrentDirectory() + "/InputManager.asset", DeleteLines(addText, 7));
				MessageBoxResult result = MessageBox.Show("You have added the controller inputs.");
				if (result == MessageBoxResult.OK)
				{
					Application.Current.Shutdown();
				}
			}
            else
            {
                using (FileStream fs = File.Create(Directory.GetCurrentDirectory() + "/InputManager.asset"))
                {
                    byte[] replaceArray = global::InputManagerAdder.Properties.Resources.InputManager;
                    fs.Write(replaceArray, 0, replaceArray.Length);
                }
                MessageBoxResult result = MessageBox.Show("You have replaced all inputs with the controller inputs.");
                if (result == MessageBoxResult.OK)
                {
                    Application.Current.Shutdown();
                }

            }

        }
        public static string DeleteLines(string s, int linesToRemove)
        {
            return s.Split(Environment.NewLine.ToCharArray(),
                           linesToRemove + 1
                ).Skip(linesToRemove)
                .FirstOrDefault();
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            replace = (sender as CheckBox).IsChecked.Value;
        }
        private void CheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            replace = (sender as CheckBox).IsChecked.Value;
        }
    }
}
