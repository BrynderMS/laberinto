using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System;

namespace creadorlaberinto
{
    class personaje : IDisposable
    {
        private List<Point> posicicionesanteriores = new List<Point>();//<---- no es necesario (se puede sustituir por posicionanterior), se usa para pintar de DarkRed cuando se repite el camino
        private Point posicion;
        private Int16 direccion = 0;
        //private Point posicionanterior;

        public Point Posicion
        {
            get
            {
                return posicion;
            }
        }

        //public Point Posicionanterior
        //{
        //    get
        //    {
        //        return posicionanterior;
        //    }
        //}

        public List<Point> Posicicionesanteriores
        {
            get
            {
                return posicicionesanteriores;
            }
        }

        public short Direccion
        {
            get
            {
                return direccion;
            }
        }

        public personaje(Dictionary<Point, cuadradoplus> laberinto)
        {
            posicion = laberinto.ToList()[0].Key;
            Posicicionesanteriores.Add(posicion);
            //posicionanterior = posicion;
        }

        public bool Mover(Keys e, Dictionary<Point, cuadradoplus> laberinto)
        {
            switch (e)
            {
                case (Keys.Down):
                    if (laberinto.ContainsKey(new Point(posicion.X, posicion.Y + 1)) && !laberinto[posicion].Bottom){
                        //posicionanterior = posicion;
                        Posicicionesanteriores.Add(posicion);
                        posicion.Y++;
                        direccion = 0;
                        return true;
                    }
                    else return false;
                case (Keys.Up):
                    if (laberinto.ContainsKey(new Point(posicion.X, posicion.Y - 1)) && !laberinto[posicion].Top)
                    {
                        //posicionanterior = posicion;
                        Posicicionesanteriores.Add(posicion);
                        posicion.Y--;
                        direccion = 2;
                        return true;
                    }
                    else return false;
                case (Keys.Left):
                    if (laberinto.ContainsKey(new Point(posicion.X - 1, posicion.Y)) && !laberinto[posicion].Left)
                    {
                        //posicionanterior = posicion;
                        Posicicionesanteriores.Add(posicion);
                        posicion.X--;
                        direccion = 1;
                        return true;
                    }
                    else return false;
                case (Keys.Right):
                    if (laberinto.ContainsKey(new Point(posicion.X + 1, posicion.Y)) && !laberinto[posicion].Right)
                    {
                        //posicionanterior = posicion;
                        Posicicionesanteriores.Add(posicion);
                        posicion.X++;
                        direccion = 3;
                        return true;
                    }
                    else return false;
                default:return false;
            }
        }

        public void movermanoderecha(Dictionary<Point, cuadradoplus> laberinto)
        {
            if (!derecha(laberinto))
            {
                while (!avanzar(laberinto))
                {
                    girar();
                }
            }

        }

        private void girar(bool izquierda = true)
        {
            if (izquierda)
            {
                direccion--;
                if (Direccion < 0)
                {
                    direccion = 3;
                }
            }
            else
            {
                direccion++;
                if (Direccion > 3)
                {
                    direccion = 0;
                }
            }
        }

        private bool derecha(Dictionary<Point, cuadradoplus> laberinto)
        {
            switch (Direccion)
            {
                case (0):
                    return Mover(Keys.Left, laberinto);
                case (2):
                    return Mover(Keys.Right, laberinto);
                case (1):
                    return Mover(Keys.Up, laberinto);
                case (3):
                    return Mover(Keys.Down, laberinto);
                default: return false;
            }
        }

        private bool avanzar(Dictionary<Point, cuadradoplus> laberinto)
        {
            switch (Direccion)
            {
                case (0):
                    return Mover(Keys.Down,laberinto);
                case (2):
                    return Mover(Keys.Up, laberinto);
                case (1):
                    return Mover(Keys.Left, laberinto);
                case (3):
                    return Mover(Keys.Right, laberinto);
                default:return false;
            }
        }



        public void moverprimerapersona(Keys keyCode,Dictionary<Point, cuadradoplus> laberinto)
        {
            switch (keyCode)
            {
                case (Keys.W):
                    avanzar(laberinto);
                    break;
                case (Keys.A):
                    girar();
                    break;
                case (Keys.D):
                    girar(false);
                    break;

            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~personaje() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
