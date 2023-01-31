using System.ComponentModel;
using System.Windows.Controls;

namespace Scheduler
{
    public class BasePage<VM> : Page where VM : BaseViewModel, new()
    {
        private VM mViewModel;

        public double SlideSeconds { get; set; } = 0.3;

        public VM ViewModel
        {
            get { return mViewModel; }
            set
            {
                if (mViewModel == value)
                {
                    return;
                }

                mViewModel = value;
                this.DataContext = mViewModel;
            }
        }

        public BasePage()
        {
            this.ViewModel = new VM();
        }

        public BasePage(VM specificViewModel = null) : base()
        {
            if (specificViewModel != null)
            {
                ViewModel = specificViewModel;
            }
            else
            {
                if (DesignerProperties.GetIsInDesignMode(this))
                {
                    ViewModel = new VM();
                }
            }
        }
    }
}
