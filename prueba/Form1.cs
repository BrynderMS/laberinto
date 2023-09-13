using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace prueba
{
    public partial class Form1 : Form
    {
        //////////PRUEBA FUNCIONSITA
        //Func<int, bool> espar = x => x%2==0;
        /////////PRUEBA FUNCIONSITA
        private List<Tuple<Rectangle, SolidBrush,Graphics>> puntos = new List<Tuple<Rectangle, SolidBrush,Graphics>>();
        private Graphics lienzo;
        private SolidBrush[] listabrush = new SolidBrush[5] { new SolidBrush(Color.Black), new SolidBrush(Color.Green), new SolidBrush(Color.Gold), new SolidBrush(Color.Cyan), new SolidBrush(Color.OrangeRed) };
        private int brushactual;
        private int tamanopincel=10;
        private Boolean pulsado = false;
        private Boolean borrar = false;

        public Form1()
        {
            InitializeComponent();
            lienzo = this.CreateGraphics();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //////////PRUEBA FUNCIONSITA
            //var a = new Random().Next();
            //if (espar(a))
            //    lienzo.DrawString($"{a} es par", new Font("Times New Roman", 15.0f), new SolidBrush(Color.Black), 30, 10);
            //else
            //    lienzo.DrawString($"{a} no es par", new Font("Times New Roman", 15.0f), new SolidBrush(Color.Black), 30, 10);
            /////////PRUEBA FUNCIONSITA
            pintar();
        }

        private void pintar()
        {
            //dibujo
            puntos.ForEach(punto => { lienzo.FillRectangle(punto.Item2, punto.Item1);/*punto.Item3.DrawString(puntos.IndexOf(punto).ToString(), new Font("Times New Roman", 12.0f),new SolidBrush(Color.White),punto.Item1.X,punto.Item1.Y);*/ });
            //Parallel.ForEach(puntos, punto => punto.Item3.FillRectangle(punto.Item2, punto.Item1);
            //pincel
            lienzo.FillRectangle(listabrush[brushactual], 2, 2, 30, 30);
            if (listabrush[brushactual].Color != Color.Black)
            {
                lienzo.DrawRectangle(new Pen(Color.Black, 1), 2, 2, 30, 30);
                lienzo.DrawRectangle(new Pen(Color.Black, 1), 2, 2, tamanopincel, tamanopincel);
            }
            else
            {
                lienzo.DrawRectangle(new Pen(Color.DarkGray, 1), 2, 2, 30, 30);
                lienzo.DrawRectangle(new Pen(Color.DarkGray, 1), 2, 2, tamanopincel, tamanopincel);
            }
            //borrador
            lienzo.FillRectangle(new SolidBrush(Color.White), 2, 34, 30, 30);
            lienzo.DrawRectangle(new Pen(Color.Black), 2, 34, 30, 30);
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!clickcambiocolor() && !clickborrador())
            {
                if (e.Button == MouseButtons.Right)
                {
                    cambiarcolor();
                }
                else
                {
                    //puntos.Add(new Tuple<Rectangle, SolidBrush>(new Rectangle(e.X, e.Y, tamanopincel, tamanopincel), listabrush[brushactual]));
                    //this.pintar();
                    if(!borrar) puntos.Add(new Tuple<Rectangle, SolidBrush,Graphics>(new Rectangle(e.X, e.Y, tamanopincel, tamanopincel), listabrush[brushactual],this.CreateGraphics()));
                    pulsado = true;
                }
            }
        }

        private bool clickborrador()
        {
            if (this.PointToClient(MousePosition).X > 2 && this.PointToClient(MousePosition).X < 32 && this.PointToClient(MousePosition).Y > 34 && this.PointToClient(MousePosition).Y < 64)
            {
                borrar=true;
                return true;
            }
            return false;
        }

        private void Form1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                if(tamanopincel<30)tamanopincel+=e.Delta/100;
                pintar();
            }
            else
            {
                if(tamanopincel>0)tamanopincel+=e.Delta/100;
                pintar();
            }
        }

        private void cambiarcolor()
        {
            brushactual++;
            if (brushactual > listabrush.Length - 1) brushactual = 0;
            this.pintar();
        }

        private bool clickcambiocolor()
        {
                if (this.PointToClient(MousePosition).X > 2 && this.PointToClient(MousePosition).X< 32 && this.PointToClient(MousePosition).Y>2 && this.PointToClient(MousePosition).Y < 32)
                {
                if (!dejardeborrar()) cambiarcolor();
                return true;
                }
            return false;
        }

        private bool dejardeborrar()
        {
            if (borrar)
            {
                borrar = false;
                return true;
            }
            else return false;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            var punto = new Rectangle(e.X, e.Y, tamanopincel, tamanopincel);
            if (pulsado)
            {
                if (borrar)
                {
                    lienzo.Clear(Color.White);
                    lienzo.DrawRectangle(new Pen(Color.DarkGray), punto);
                    puntos.RemoveAll(x => punto.Contains(x.Item1) || punto.Equals(x.Item1));
                    //borrarpuntos(punto);
                    //puntos.Add(new Tuple<Rectangle, SolidBrush>(punto, new SolidBrush(Color.White)));
                }
                else puntos.Add(new Tuple<Rectangle, SolidBrush,Graphics>(punto, listabrush[brushactual],this.CreateGraphics()));
                this.pintar();
            }
        }

        //private void borrarpuntos(Rectangle punto)
        //{
        //    //puntos.ForEach(x =>
        //    //{
        //    //    if (x.Item1.Size.Height < punto.Size.Height && punto.X <= (x.Item1.X-(punto.Size.Height-x.Item1.Size.Height))&& punto.X >= x.Item1.X && punto.Y <= (x.Item1.Y - (punto.Size.Height - x.Item1.Size.Height)) && punto.Y >= x.Item1.Y)
        //    //    {
        //    //        puntos.Remove(x);
        //    //    }
        //    //});//<----no funciona

        //    //puntos.RemoveAll(x=> x.Item1.Size.Height <= punto.Size.Height && punto.X >= (x.Item1.X - (punto.Size.Height - x.Item1.Size.Height)) && punto.X <= x.Item1.X && punto.Y >= (x.Item1.Y - (punto.Size.Height - x.Item1.Size.Height)) && punto.Y <= x.Item1.Y);
        //    puntos.RemoveAll(x => punto.Contains(x.Item1)||punto.Equals(x.Item1));
        //}

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            pulsado = false;
            //lienzo.FillRectangle(new SolidBrush(Color.White), 0, 0, this.Size.Width, this.Size.Height);
            lienzo.Clear(Color.White);
            pintar();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            puntos.Clear();
            lienzo.Clear(Color.White);
        }
    }
}
