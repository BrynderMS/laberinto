using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace generarNivel
{
    public partial class Form1 : Form
    {
        public class gameobject { }
        public  class habitacion { public gameobject[] prefabs = new gameobject[10]; public Boolean wall = true; public Color color = Color.Black; }
        public Dictionary<Point, habitacion> nivel = new Dictionary<Point, habitacion>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Debug.Print("");
            Graphics LIENZO = this.CreateGraphics();
            foreach (KeyValuePair<Point, habitacion> entry in nivel)
            {
                // do something with entry.Value or entry.Key
                if (entry.Value.wall == true)
                {
                    LIENZO.FillEllipse(new SolidBrush(entry.Value.color), (entry.Key.X * 20) + 30, (entry.Key.Y * 20) + 10, 19, 19);
                }
            }

            
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            var x = 0;
            var y = 0;
            for (var i = 0; i < 100; i++)
            {
                nivel.Add(new Point(x, y), new habitacion());
                if ((x+1) % 10 == 0 && x != 0)
                {
                    x -= 9;
                    y++;
                }
                else
                { 
                    x++;
                }
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            var rnd = new System.Random();
            var puntoini = new Point(rnd.Next(0, 10), rnd.Next(0, 10));
            //var puntoini = new Point(0, 0);

            //while (posible(puntoini, puntofin))
            {
                
            }
            //for (var o = 10; o < 50; o++)
            //{
            //var punto = new Point(rnd.Next(0, 10), rnd.Next(1, 11));
            //nivel[punto].wall = false;
            //}

            //for (var i = 0; i < 100; i++)
            //{
            //    nivel[nivel.ToList()[rnd.Next(0, i + 1)].Key].wall = false;
            //}

            nivel.ToList().ForEach(x => { nivel[x.Key].wall = false;nivel[x.Key].color = Color.Black; });
            var listica = new List<Point>();
            for (var i = 0; i < 60; i++)
            {
                if(nivel[puntoini].wall != true)
                {
                    nivel[puntoini].wall = true;
                    listica.Add(puntoini);
                }
                
                switch(rnd.Next(0, 4))
                {
                    case (0):
                        if(puntoini.Y != 9)puntoini.Y += 1;
                        break;
                    case (1):
                        if (puntoini.X != 9) puntoini.X += 1;
                        break;
                    case (2):
                        if (puntoini.Y != 0) puntoini.Y -= 1;
                        break;
                    case (3):
                        if (puntoini.X != 0) puntoini.X -= 1;
                        break;
                    default:
                        break;
                }
            }

            foreach(Point room in listica)
            {
                switch (vecinos(room))
                {
                    case (1):
                        nivel[room].color = Color.Red;
                        break;
                    case (2):
                        nivel[room].color = Color.Blue;
                        break;
                    case (3):
                        nivel[room].color = Color.Orange;
                        break;
                    case (4):
                        nivel[room].color = Color.DarkMagenta;
                        break;
                    default:
                        break;
                }
            }

            nivel[listica.First()].color = Color.Green;
            if (Math.Abs(listica.Last().X - listica.First().X) > 3 || Math.Abs(listica.Last().Y - listica.First().Y) > 3)
                nivel[listica.Last()].color = Color.Green;
            else
            {
                var punto = listica[rnd.Next(0, listica.Count())];
                var i = 0;
                while (Math.Abs(punto.X - listica.First().X) < 3 && Math.Abs(punto.Y - listica.First().Y) < 3 && i < 100)
                {
                    punto = listica[rnd.Next(listica.Count() / 2, listica.Count())];
                    i++;
                }// si se sale del bucle por que se ha llegado a 100 hay como maximo 1/25 posiblidades de que inicio sea igual a fin
                nivel[punto].color = Color.Green;
            }

            //sustituir multiples habitaciones por



            Invalidate();

        }

        private int vecinos(Point room)
        {
                int i = 0;
                if (room.Y + 1 != 10)
                    if (nivel[new Point(room.X, room.Y + 1)].wall) i++;
                if (room.X + 1 != 10)
                    if (nivel[new Point(room.X + 1, room.Y)].wall) i++;
                if (room.Y - 1 != -1)
                    if (nivel[new Point(room.X, room.Y - 1)].wall) i++;
                if (room.X - 1 != -1)
                    if (nivel[new Point(room.X - 1, room.Y)].wall) i++;

                return i;
        }

        //private bool posible(Point puntoini, Point puntofin)
        //{
        //    var direccion = "arriba";
        //    var puntosvis = new List<Point>();
        //    while (puntoini != puntofin)
        //    {
        //        switch (direccion)
        //        {
        //            case ("arriba"):
        //                direccion = "derecha";
        //                break;
        //            case ("derecha"):
        //                direccion = "abajo";
        //                break;
        //            case ("abajo"):
        //                direccion = "izquierda";
        //                break;
        //            case ("izquierda"):
        //                direccion = "arriba";
        //                break;
        //        }
        //    }
        //}

    }
}
