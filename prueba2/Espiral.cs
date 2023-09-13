using System.Collections.Generic;
using System.Drawing;

namespace prueba2
{
    internal class Espiral : System.Collections.CollectionBase
    {
        private List<linea> lineas = new List<linea>();
        private struct linea { Point pi; Point pf; public linea(Point ppi, Point ppf) { pi = ppi;pf = ppf; } };


        public void anadir(Point inicial, Point final)
        {
            lineas.Add(new linea(inicial, final));
        }
    }
}