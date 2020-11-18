using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;

namespace MainCheck
{
    public class Baza
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long PESEL { get; set; }
        public Image _img { get; set; }
        public Baza()
        {
            Name = " ";
            Surname = " ";
            PESEL = 0;
        }
        public Baza(string Name, string Surname, long PESEL)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.PESEL = PESEL;
        }
        public Baza(Baza o)
        {
            Name = o.Name;
            Surname = o.Surname;
            PESEL = o.PESEL;
        }
    }
}
