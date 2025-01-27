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
        public ObservableCollection<Movie> MovieItems { get; set; }

        public MainViewModel()
        {
            MovieItems = new ObservableCollection<Movie>();
        }

        public void AddTodoItem(string title, string description)
        {
            var newItem = new Movie { Title = title, Description = description };
            MovieItems.Add(newItem);
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
                MovieItems.Remove(item);
            }
        }
    }
}