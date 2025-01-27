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
using WpfMovie.Models;
using WpfMovie.ViewModels;

namespace WpfMovie;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private MainViewModel viewModel;

    public MainWindow()
    {
        InitializeComponent();
        viewModel = new MainViewModel();
        DataContext = viewModel;
    }

    private void AddButton_Click(object sender, RoutedEventArgs e)
    {
        viewModel.AddTodoItem(TitleTextBox.Text, DescriptionTextBox.Text);
        TitleTextBox.Clear();
        DescriptionTextBox.Clear();
    }

    private void EditButton_Click(object sender, RoutedEventArgs e)
    {
        if (MovieListBox.SelectedItem is Movie selectedItem)
        {
            viewModel.EditTodoItem(selectedItem, TitleTextBox.Text, DescriptionTextBox.Text);
            TitleTextBox.Clear();
            DescriptionTextBox.Clear();
        }
    }

    private void DeleteButton_Click(object sender, RoutedEventArgs e)
    {
        if (MovieListBox.SelectedItem is Movie selectedItem)
        {
            viewModel.DeleteTodoItem(selectedItem);
        }
    }
}