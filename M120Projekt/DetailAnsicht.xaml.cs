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
using System.Windows.Shapes;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für DetailAnsicht.xaml
    /// </summary>
    public partial class DetailAnsicht : Window
    {
        public DetailAnsicht()
        {
            InitializeComponent();
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledOrDisabled(gridAufgaben, true);
            Button button = sender as Button;
            button.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            btnSave.IsEnabled = false;
        }


        public void SetEnabledOrDisabled(Grid grid, bool input)
        {
            foreach (UIElement child in grid.Children)
            {
                if (child is TextBox || child is CheckBox)
                {
                    child.IsEnabled = input;                                                
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledOrDisabled(gridAufgaben, false);
            Button button = sender as Button;
            button.Visibility = Visibility.Collapsed;
            btnEdit.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Collapsed;
        }

        private void txtBox_Changed(object sender, TextChangedEventArgs e)
        {
            if(this.IsLoaded)
            {
                btnSave.IsEnabled = true;
            }    
        }
        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                btnSave.IsEnabled = true;
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledOrDisabled(gridAufgaben, false);
            btnCancel.Visibility = Visibility.Collapsed;
        }
    }
}
