using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueba2
{
    public partial class Form1 : Form
    {
        private Graphics lienzo;
        private int tamanolinea;
        private Point posicioni;
        private Point posicionf;
        private int numero=20;
        private int direccion;
        private struct linea { Point pi; Point pf; public Point Pi { get { return pi; } set { pi = value; } } public Point Pf { get { return pf; } set { pf = value; } } public linea(Point ppi, Point ppf) { pi = ppi; pf = ppf; } };
        private List<linea> espiral = new List<linea>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lienzo = this.CreateGraphics();
            calctamanoprimeralinea();
            crearespiral();
        }

    private void calctamanoprimeralinea()
        {
            if (Width == Height)
            {
                posicioni.X = Width/numero;
                posicioni.Y = ClientSize.Width - posicioni.X;
                tamanolinea = ClientSize.Width - ((ClientSize.Width / numero) * 2);
            }
            else this.Close();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            dibujarespiral();
        }

        private void dibujarespiral()
        {
            Pen a = new Pen(Color.Black, 1);
            espiral.ForEach(x =>
            {
                lienzo.DrawLine(a, x.Pi,x.Pf);
                System.Threading.Thread.Sleep(5);
            });
        }

        private void crearespiral()
        {
            calcposicionfinal();
            espiral.Add(new linea(posicioni,posicionf));


            posicioni = posicionf;
            cambiardireccion();
            tamanolinea -= 1;
            if (tamanolinea>1)crearespiral();
        }

        private void cambiardireccion()
        {
            direccion++;
            if (direccion > 3) direccion = 0;
        }

        private void calcposicionfinal()
        {
            switch (direccion)
            {
                case (0)://derecha
                    posicionf.X = posicioni.X + tamanolinea;
                    posicionf.Y = posicioni.Y;
                    break;
                case (1)://arriba
                    posicionf.X = posicioni.X;
                    posicionf.Y = posicioni.Y - tamanolinea;
                    break;
                case (2)://izquierda
                    posicionf.X = posicioni.X - tamanolinea;
                    posicionf.Y = posicioni.Y;
                    break;
                case (3)://abajo
                    posicionf.X = posicioni.X;
                    posicionf.Y = posicioni.Y + tamanolinea;
                    break;
            }
        }
    }
}
