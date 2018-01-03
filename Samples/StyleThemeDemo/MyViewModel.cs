using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StyleThemeDemo
{

    public class Book
    {
        public string Title { get; set; }
        public string Publisher { get; set; }
        public DateTime PublishDate { get; set; }
    }


    public class MyViewModel
    {
        public List<Book> Books { get; }
        

        /// <summary>
        /// constructor
        /// </summary>
        public MyViewModel()
        {
            Books = new List<Book>();
            Books.Add(new Book { Title = "Pro ASP.NET MVC 5", Publisher = "Apress", PublishDate = new DateTime(2017, 1, 1)});
            Books.Add(new Book { Title = "Visual Studio Tools for Office 2007", Publisher = "Addison Wesley", PublishDate = new DateTime(2013, 5, 24) });
            Books.Add(new Book { Title = ".NET FRAMEWORK 2.0 Windows-Based Client Development", Publisher = "Microsoft", PublishDate = new DateTime(2015, 6, 1) });
            Books.Add(new Book { Title = "", Publisher = "", PublishDate = new DateTime(2017, 1, 1) });
        }
    }
}
