using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Scheduler
{
    public partial class UserMainPage : BasePage<SchedulerViewModel>
    {
        public UserMainPage() : base()
        {
            InitializeComponent();
        }

        public UserMainPage(SchedulerViewModel specificViewModel) : base()
        {
            InitializeComponent();
        }

        private void LblNote_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtNote.Focus();
        }

        private void TxtNote_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNote.Text) && txtNote.Text.Length > 0)
            {
                this.lblNote.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblNote.Visibility = Visibility.Visible;
            }
        }

        private void LblTime_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            txtTime.Focus();
        }

        private void TxtTime_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtTime.Text) && txtTime.Text.Length > 0)
            {
                lblTime.Visibility = Visibility.Collapsed;
            }
            else
            {
                lblTime.Visibility = Visibility.Visible;
            }
        }
    }
}
