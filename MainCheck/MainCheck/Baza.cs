using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using System.Xml.Serialization;

namespace MainCheck
{
    [Serializable]
    public class Baza
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public long PESEL { get; set; }
        public string _imgFile { get; set; }
        public string MotherName { get; set; }
        public string FatherName { get; set; }
        public Baza()
        {
            Name = " ";
            Surname = " ";
            PESEL = 0;
            MotherName = " ";
            FatherName = " ";
            _imgFile = @"C:\Users\zwari\source\repos\Baza\MainCheck\MainCheck\bin\Debug\Pictures\Empty.png";
        }
        [XmlIgnore]
        public BitmapImage obrazek { get; set; }
        [XmlIgnore]
        public BitmapImage Obraz
        {
            get
            {
                if (obrazek == null)
                {
                    obrazek = new BitmapImage();
                    obrazek.BeginInit();
                    obrazek.UriSource = new Uri(_imgFile, UriKind.Absolute);
                    obrazek.EndInit();
                }
                return obrazek;
            }
            set
            {
                obrazek = value;
            }
        }
        [XmlIgnore]
        public Uri IconUri
        {
            get
            {
                if (obrazek != null)
                {
                    return obrazek.UriSource;
                }
                return null;
            }
            set
            {
                obrazek.UriSource = value;
            }
        }
        public Baza(string Name, string Surname, long PESEL, string _imgFile, string MotherName, string FatherName)
        {
            this.Name = Name;
            this.Surname = Surname;
            this.PESEL = PESEL;
            this._imgFile = _imgFile;
            this.MotherName = MotherName;
            this.FatherName = FatherName;
            obrazek = new BitmapImage();
            obrazek.BeginInit();
            obrazek.UriSource = new Uri(_imgFile, UriKind.Absolute);
            obrazek.EndInit();
        }
    }
}

