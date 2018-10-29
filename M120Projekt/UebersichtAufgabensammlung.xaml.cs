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

namespace M120Projekt
{
    /// <summary>
    /// Interaktionslogik für UebersichtAufgabensammlung.xaml
    /// </summary>
    public partial class UebersichtAufgabensammlung : UserControl
    {
        private MainWindow mainWindow;
        public UebersichtAufgabensammlung(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledOrDisabled(gridAufgabensammlung, true);
            Button button = sender as Button;
            button.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
            btnSave.IsEnabled = false;
            lblStatus.Content = "In Bearbeitung";
        }


        private void SetEnabledOrDisabled(Grid grid, bool input)
        {
            List<UIElement> elementsToRemove = new List<UIElement>();
            List<UIElement> elementsToAdd = new List<UIElement>();
            foreach (UIElement child in grid.Children)
            {
                if (child is DatePicker)
                {
                    child.IsEnabled = input;
                }
                if (child is Button && input == true && Grid.GetColumn(child) == 0)
                {
                    Button button = child as Button;
                    string content = button.Content.ToString();
                    int row = Grid.GetRow(button);
                    int column = Grid.GetColumn(button);

                    TextBox textBox = new TextBox();
                    textBox.Text = content;
                    textBox.FontSize = 24;
                    textBox.TextChanged += new TextChangedEventHandler(txtBox_Changed);

                    elementsToRemove.Add(button);
                    elementsToAdd.Add(textBox);
                    Grid.SetColumn(textBox, 0);
                    Grid.SetRow(textBox, row);
                }
                else if (child is TextBox && input == false && Grid.GetColumn(child) == 0)
                {
                    TextBox textBox = child as TextBox;
                    string content = textBox.Text;
                    int row = Grid.GetRow(textBox);
                    int column = Grid.GetColumn(textBox);

                    Button button = new Button();
                    button.Content = content;
                    button.FontSize = 24;
                    button.HorizontalContentAlignment = HorizontalAlignment.Left;
                    button.Click += new RoutedEventHandler(btnAufgabensammlung_Click);

                    elementsToRemove.Add(textBox);
                    elementsToAdd.Add(button);
                    Grid.SetColumn(button, 0);
                    Grid.SetRow(button, row);
                }
            }
            foreach(UIElement control in elementsToRemove)
            {
                gridAufgabensammlung.Children.Remove(control);
            }
            foreach (UIElement control in elementsToAdd)
            {
                gridAufgabensammlung.Children.Add(control);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            bool allValid = true;
            foreach (UIElement child in gridAufgabensammlung.Children)
            {
                if (child is TextBox)
                {

                    TextBox textBox = child as TextBox;
                    if (ValidateAufgabe(textBox.Text))
                    { }
                    else
                    {
                        allValid = false;
                        textBox.BorderBrush = Brushes.Red;
                        lblWarning.Content = "Es dürfen nur Buchstaben, Zahlen und Leerzeichen eingegeben werden.";
                    }
                }
            }
            if (allValid == true)
            {
                SetEnabledOrDisabled(gridAufgabensammlung, false);
                Button button = sender as Button;
                button.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Visible;
                btnCancel.Visibility = Visibility.Collapsed;
                lblStatus.Content = "Gespeichert";
            }
        }

        private void txtBox_Changed(object sender, TextChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                btnSave.IsEnabled = true;
                lblStatus.Content = "Verändert";
            }
        }
        private void datePicker_Changed(object sender, RoutedEventArgs e)
        {
            if (this.IsLoaded)
            {
                btnSave.IsEnabled = true;
                lblStatus.Content = "Verändert";
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            SetEnabledOrDisabled(gridAufgabensammlung, false);
            btnCancel.Visibility = Visibility.Collapsed;
            btnSave.Visibility = Visibility.Collapsed;
            btnEdit.Visibility = Visibility.Visible;
            lblStatus.Content = "Abgebrochen";
            lblWarning.Content = "";
            foreach (UIElement child in gridAufgabensammlung.Children)
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
            gridAufgabensammlung.RowDefinitions.Add(new RowDefinition());

            
            // Create new CheckBox
            DatePicker datePicker = new DatePicker();
            datePicker.SelectedDateChanged += new EventHandler<SelectionChangedEventArgs>(datePicker_Changed);
            datePicker.HorizontalAlignment = HorizontalAlignment.Left;
            datePicker.VerticalAlignment = VerticalAlignment.Center;
            datePicker.Margin = new Thickness(6, 0, 0, 0);
            // Create new DeleteButton
            Button btnDelete = new Button();
            btnDelete.BorderBrush = Brushes.LightGray;
            btnDelete.BorderThickness = new Thickness(1);
            btnDelete.Click += new RoutedEventHandler(btnDelete_Click);
            Image image = new Image();
            image.Source = new BitmapImage(new Uri(@"Images/delete_forever.png", UriKind.Relative));
            image.Width = 30;
            btnDelete.Content = image;

            if (btnEdit.IsVisible == true)
            {
                // Create new Button
                Button button = new Button();
                button.FontSize = 24;
                button.Content = "Neue Aufgabensammlung";
                button.Click += new RoutedEventHandler(btnAufgabensammlung_Click);
                button.HorizontalContentAlignment = HorizontalAlignment.Left;
                datePicker.IsEnabled = false;
                gridAufgabensammlung.Children.Add(button);
                Grid.SetRow(button, gridAufgabensammlung.RowDefinitions.Count - 1);
                Grid.SetColumn(button, 0);
            }
            else
            {
                TextBox textBox = new TextBox();
                textBox.Text = "Neue Aufgabensammlung";
                textBox.FontSize = 24;
                textBox.TextChanged += new TextChangedEventHandler(txtBox_Changed);
                datePicker.IsEnabled = true;
                gridAufgabensammlung.Children.Add(textBox);
                Grid.SetRow(textBox, gridAufgabensammlung.RowDefinitions.Count - 1);
                Grid.SetColumn(textBox, 0);
            }

            // Add Objects to grid
            
            gridAufgabensammlung.Children.Add(datePicker);
            gridAufgabensammlung.Children.Add(btnDelete);
            
            Grid.SetRow(datePicker, gridAufgabensammlung.RowDefinitions.Count - 1);
            Grid.SetRow(btnDelete, gridAufgabensammlung.RowDefinitions.Count - 1);
            
            Grid.SetColumn(datePicker, 1);
            Grid.SetColumn(btnDelete, 2);
            lblStatus.Content = "Neu";
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Sind sie sicher, dass Sie diese Aufgabensammlung löschen möchten?", "Aufgabe wird gelöscht", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Button button = sender as Button;
                List<UIElement> elementsToRemove = new List<UIElement>();
                foreach (UIElement child in gridAufgabensammlung.Children)
                {
                    if (Grid.GetRow(child) == Grid.GetRow(button))
                    {
                        elementsToRemove.Add(child);
                    }
                }
                foreach (UIElement child in elementsToRemove)
                {
                    gridAufgabensammlung.Children.Remove(child);
                }
                lblStatus.Content = "Gelöscht";
            }
        }

        private bool ValidateAufgabe(string aufgabenInhalt)
        {
            string pattern = @"^([A-Za-z0-9äöüÄÖÜ\s?]+)$";
            Match match = Regex.Match(aufgabenInhalt, pattern);
            if (match.Success)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnAufgabensammlung_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Main.Content = new EinzelAnsicht(mainWindow);
        }
    }
}
