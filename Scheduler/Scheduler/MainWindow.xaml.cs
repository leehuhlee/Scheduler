using System.Windows;

namespace Scheduler
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new WindowViewModel(this);
            InitializeComponent();
        }
    }
}
