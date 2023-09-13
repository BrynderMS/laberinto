using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace prueba3
{
    public partial class Form1 : Form
    {
        private List<cuadradoplus> cuadrados = new List<cuadradoplus>();
        private const int posx = 10;////
        private const int posy = 10;////<--------- esquina superior izquierda y tamaño de las casillas del laberinto
        private const int size = 23;////
        private Graphics lienzo;
        public Form1()
        {
            InitializeComponent();
            lienzo = this.CreateGraphics();
            var x = posx;
            var y = posy;
            for(var i = 1; i < 101; i++)
            {
                cuadrados.Add(new cuadradoplus(size, x, y));
                if (i % 10 == 0)
                {
                    y += size;
                    x = posx;
                }
                else x += size;
                
            }

            while (cuadrados.All(cuadrado => !cuadrado.Visitado))
            {
                //aqui se movera el creador del laberinto
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            cuadrados.ForEach(x => x.dibujarrect(lienzo));
        }
    }
}
