using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RuToEnToRu
{
    internal class UserIcon
    {
        public Bitmap Bitmap;

        public UserIcon()
        {
            Bitmap = new Bitmap(256, 256);
            using (Graphics g = Graphics.FromImage(Bitmap))
            {
                Pen black = new Pen(Color.Black, 12);
                Pen white = new Pen(Color.White, 13);
                g.FillPie(Brushes.White, 6, 6, 256 - 12, 256 - 12, 45, -180);
                g.DrawEllipse(black, 6, 6, 256-12, 256-12); //round
                g.FillPie(Brushes.Black,6,6, 256 - 12, 256 - 12,45,180); //1 half od round
                g.DrawString("En",new Font(System.Drawing.FontFamily.GenericSansSerif,80,FontStyle.Bold),Brushes.Black,25,30);
                g.DrawString("Ru",new Font(System.Drawing.FontFamily.GenericMonospace,80,FontStyle.Bold),Brushes.White,5,80);
            }
        }

        public Icon GetIcon()
        {
            return Icon.FromHandle(Bitmap.GetHicon());
        }
    }
}
