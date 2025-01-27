using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfMovie.Models;

namespace WpfMovie.ViewModels
{
    public class MainViewModel
    {
        public ObservableCollection<Movie> TodoItems { get; set; }

        public MainViewModel()
        {
            TodoItems = new ObservableCollection<Movie>();
        }

        public void AddTodoItem(string title, string description)
        {
            var newItem = new Movie { Title = title, Description = description };
            TodoItems.Add(newItem);
        }

        public void EditTodoItem(Movie item, string newTitle, string newDescription)
        {
            if (item != null)
            {
                item.Title = newTitle;
                item.Description = newDescription;
            }
        }

        public void DeleteTodoItem(Movie item)
        {
            if (item != null)
            {
                TodoItems.Remove(item);
            }
        }
    }
}
