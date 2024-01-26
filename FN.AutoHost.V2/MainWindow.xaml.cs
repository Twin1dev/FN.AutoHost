using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FN.AutoHost.V2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void SetLogContent(string NewContent)
        {
            string OldContent = (string)LogLabel.Content;

            LogLabel.Content = OldContent + "\n" + $"> {NewContent}";
        }

        public void SetLogContentBlank()
        {
            LogLabel.Content = "";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetLogContent("Starting");
            new Thread(new ThreadStart(() => AutoHost.V2.Fortnite.AutoHosting.Launch("")));
          
        }
    }

}