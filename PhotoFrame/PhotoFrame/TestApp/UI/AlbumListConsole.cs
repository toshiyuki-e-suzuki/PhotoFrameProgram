using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoFrame.Domain.Model;
using static System.Console;

namespace TestApp.UI
{
    public class AlbumListConsole
    {
        public void PrintList()
        {
            Album album1 = Album.Create("album1");
            WriteLine(album1.Id);
            
        }
    }
}
