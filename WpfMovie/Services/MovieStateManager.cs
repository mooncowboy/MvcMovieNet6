using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfMovie.Models;

namespace WpfMovie.Services
{
    public class MovieStateManager
    {
        /// <summary>
        /// Uses System.Text.Json to serialize the object, and return a string that is base64 encoded
        /// </summary>
        /// <param name="anyMovie"></param>
        /// <returns></returns>
        public string Serialize(Movie anyMovie)
        {
            var json = JsonSerializer.Serialize(anyMovie);
            var bytes = Encoding.UTF8.GetBytes(json);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Reverses the serialize operation to recreate a movie object
        /// </summary>
        /// <param name="movieString"></param>
        /// <returns></returns>
        public Movie Deserialize(string movieString)
        {
            var bytes = Convert.FromBase64String(movieString);
            var json = Encoding.UTF8.GetString(bytes);
            return JsonSerializer.Deserialize<Movie>(json);
        }
    }
}