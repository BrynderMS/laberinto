using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace creadorlaberinto
{
    class creadorlaberinto
    {
        private int posactual;
        private Point posicion;
        private List<Point> posicionesvisitadas = new List<Point>();
        private Random rnd = new Random();
        private bloq controlbloqueo = new bloq();
        private class bloq
        {
            private bool top=false;
            private bool left=false;
            private bool right=false;
            private bool bottom=false;
            private bool blocked=false;

            public bool Top
            {
                get
                {
                    return top;
                }

                set
                {
                    top = value;
                    if (bottom && top && left && right)
                    {
                        this.blocked = true;
                    }
                    else blocked = false;
                }
            }

            public bool Left
            {
                get
                {
                    return left;
                }

                set
                {
                    left = value;
                    if (bottom && top && left && right)
                    {
                        this.blocked = true;
                    }
                    else blocked = false;
                }
            }

            public bool Right
            {
                get
                {
                    return right;
                }

                set
                {
                    right = value;
                    if (bottom && top && left && right)
                    {
                        this.blocked = true;
                    }
                    else blocked = false;
                }
            }

            public bool Bottom
            {
                get
                {
                    return bottom;
                }

                set
                {
                    bottom = value;
                    if (bottom && top && left && right )
                    {
                        this.blocked = true;
                    }
                    else blocked = false;
                }
            }

            public bool Blocked
            {
                get
                {
                    return blocked;
                }
            }
        }

        public creadorlaberinto(Dictionary<Point, cuadradoplus> cuadrados)
        {
            posicion = cuadrados.ToList()[cuadrados.Count()-2].Key;
        }

        public void crear(ref Dictionary<Point,cuadradoplus> laberinto)
        {
            laberinto[posicion].Bottom = false;
            laberinto[posicion].Visitado = true;
            posicionesvisitadas.Add(posicion);
            posactual = posicionesvisitadas.Count() - 1;
            var i = 0;
            do
            {
                while (!laberinto.Values.All(cuadrado => cuadrado.Visitado || controlbloqueo.Blocked))
                {

                    mover(laberinto);
                    i++;
                }
                if (controlbloqueo.Blocked && posactual > 0)
                {
                    posactual--;
                    this.posicion = posicionesvisitadas[posactual];
                    controlbloqueo.Left = false;
                    controlbloqueo.Top = false;
                    controlbloqueo.Right = false;
                    controlbloqueo.Bottom = false;
                }
                else if (posactual == 0) break;
            } while (!laberinto.Values.All(cuadrado => cuadrado.Visitado));
        }

        private void mover(Dictionary<Point, cuadradoplus> laberinto)
        {


            var a = rnd.Next(0,4);
            switch (a)
            {
                case (0):
                    if (laberinto.ContainsKey(new Point(posicion.X + 1,posicion.Y)) && laberinto[new Point(posicion.X + 1, posicion.Y)].Visitado == false)
                    {
                        laberinto[posicion].Right = false;
                        posicion.X += 1;
                        laberinto[posicion].Visitado = true;
                        laberinto[posicion].Left = false;
                        posicionesvisitadas.Add(posicion);
                        posactual = posicionesvisitadas.Count() - 1;
                        controlbloqueo.Right = false;
                    }else controlbloqueo.Right = true;
                    break;
                case (1):
                    if (laberinto.ContainsKey(new Point(posicion.X, posicion.Y - 1)) && laberinto[new Point(posicion.X, posicion.Y - 1)].Visitado == false)
                    {
                        laberinto[posicion].Top = false;
                        posicion.Y -= 1;
                        laberinto[posicion].Visitado = true;
                        laberinto[posicion].Bottom = false;
                        posicionesvisitadas.Add(posicion);
                        posactual = posicionesvisitadas.Count() - 1;
                        controlbloqueo.Top = false;
                    }else controlbloqueo.Top = true;
                    break;
                case (2):
                    if (laberinto.ContainsKey(new Point(posicion.X - 1, posicion.Y)) && laberinto[new Point(posicion.X - 1, posicion.Y)].Visitado == false)
                    {
                        laberinto[posicion].Left = false;
                        posicion.X -= 1;
                        laberinto[posicion].Visitado = true;
                        laberinto[posicion].Right = false;
                        posicionesvisitadas.Add(posicion);
                        posactual = posicionesvisitadas.Count() - 1;
                        controlbloqueo.Left = false;
                    }else controlbloqueo.Left = true;
                    break;
                case (3):
                    if (laberinto.ContainsKey(new Point(posicion.X, posicion.Y + 1)) && laberinto[new Point(posicion.X, posicion.Y + 1)].Visitado == false)
                    {
                        laberinto[posicion].Bottom = false;
                        posicion.Y += 1;
                        laberinto[posicion].Visitado = true;
                        laberinto[posicion].Top = false;
                        posicionesvisitadas.Add(posicion);
                        posactual = posicionesvisitadas.Count() - 1;
                        controlbloqueo.Bottom = false;
                    }else controlbloqueo.Bottom = true;
                    break;
                default: break;
            }
        }
    }
}
