using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using M120Projekt.Ressources;

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für DetailAnsicht.xaml
    /// </summary>
    public partial class EinzelAnsicht : UserControl
    {
        private MainWindow mainWindow;
        public EinzelAnsicht(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledOrDisabled(gridAufgaben, true);
            Button button = sender as Button;
            button.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            btnSave.IsEnabled = false;
            lblStatus.Content = strings.btnEdit;
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
            bool allValid = true;
            foreach (UIElement child in gridAufgaben.Children)
            {
                if (child is TextBox)
                {
                    
                    TextBox textBox = child as TextBox;
                    if(ValidateAufgabe(textBox.Text))
                    { }
                    else
                    {
                        allValid = false;
                        textBox.BorderBrush = Brushes.Red;
                        lblWarning.Content = strings.msgError;
                    }                    
                }
            }
            if (allValid == true)
            {
                SetEnabledOrDisabled(gridAufgaben, false);
                Button button = sender as Button;
                button.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Visible;
                btnCancel.Visibility = Visibility.Collapsed;
                lblStatus.Content = strings.stsSaved;
            }
        }

        private void txtBox_Changed(object sender, TextChangedEventArgs e)
        {
            if(this.IsLoaded)
            {
                btnSave.IsEnabled = true;
                lblStatus.Content = strings.stsEdit;
            }          
        }
        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                btnSave.IsEnabled = true;
                lblStatus.Content = strings.stsEdit;
            }           
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledOrDisabled(gridAufgaben, false);
            btnCancel.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnEdit.Visibility = Visibility.Visible;
            lblStatus.Content = strings.stsCanceled;
            lblWarning.Content = "";
            foreach (UIElement child in gridAufgaben.Children)
            {
                if (child is TextBox)
                {
                    TextBox textBox = child as TextBox;
                    textBox.BorderBrush = Brushes.LightGray;
                }
            }
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            // Create new Row
            gridAufgaben.RowDefinitions.Add(new RowDefinition());

            // Create new TextBox
            TextBox textBox = new TextBox();
            textBox.BorderBrush = Brushes.LightGray;
            textBox.BorderThickness = new Thickness(1);
            textBox.FontSize = 24;
            textBox.Text = strings.btnContentNewItem;
            textBox.TextChanged += new TextChangedEventHandler(txtBox_Changed);
            // Create new CheckBox
            CheckBox checkBox = new CheckBox();
            checkBox.Checked += new RoutedEventHandler(CheckBoxChanged);
            checkBox.HorizontalAlignment = HorizontalAlignment.Center;
            checkBox.VerticalAlignment = VerticalAlignment.Center;
            // Create new DeleteButton
            Button button = new Button();
            button.BorderBrush = Brushes.LightGray;
            button.BorderThickness = new Thickness(1);
            button.Click += new RoutedEventHandler(btnDelete_Click);
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"Images/delete_forever.png", UriKind.Relative));
            image.Width = 30;
            button.Content = image;

            if (btnEdit.IsVisible == true)
            {
                textBox.IsEnabled = false;
                checkBox.IsEnabled = false;
            }
            else
            {
                textBox.IsEnabled = true;
                checkBox.IsEnabled = true;
            }          

            // Add Objects to grid
            gridAufgaben.Children.Add(textBox);
            gridAufgaben.Children.Add(checkBox);
            gridAufgaben.Children.Add(button);
            Grid.SetRow(textBox, gridAufgaben.RowDefinitions.Count - 1);
            Grid.SetRow(checkBox, gridAufgaben.RowDefinitions.Count - 1);
            Grid.SetRow(button, gridAufgaben.RowDefinitions.Count - 1);
            Grid.SetColumn(textBox, 0);
            Grid.SetColumn(checkBox, 1);
            Grid.SetColumn(button, 2);
            lblStatus.Content = strings.stsNew;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {         
            if (MessageBox.Show(strings.msgSureToProceedContent, strings.msgSureToProceedTitle, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Button button = sender as Button;
                List<UIElement> elementsToRemove = new List<UIElement>();
                foreach (UIElement child in gridAufgaben.Children)
                {
                    if (Grid.GetRow(child) == Grid.GetRow(button))
                    {
                        elementsToRemove.Add(child);
                    }
                }
                foreach (UIElement child in elementsToRemove)
                {
                    gridAufgaben.Children.Remove(child);
                }
                lblStatus.Content = strings.stsDeleted;
            }
        }

        private bool ValidateAufgabe(string aufgabenInhalt)
        {
            string pattern = @"^([A-Za-z0-9äöüÄÖÜ\s?]+)$";
            Match match = Regex.Match(aufgabenInhalt, pattern);
            if(match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Main.Content = new UebersichtAufgabensammlung(mainWindow);
        }
    }
}
