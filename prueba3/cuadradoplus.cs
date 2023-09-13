using System.Drawing;

namespace prueba3
{
    class cuadradoplus
    {
        //private int width;
        //private int height;
        private int size;
        private bool top = true;
        private bool right = true;
        private bool left = true;
        private bool bottom = true;
        private Pen pen = new Pen(Color.Black);
        private int posx;
        private int posy;
        private bool visitado;

        public bool Top
        {
            get
            {
                return top;
            }

            set
            {
                top = value;
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
            }
        }

        public bool Visitado
        {
            get
            {
                return visitado;
            }

            set
            {
                visitado = value;
            }
        }

        public cuadradoplus(/*int pwidth,int pheight,*/int psize, int pposx,int pposy)
        {
            //width = pwidth;
            //height = pheight;
            size = psize;
            posx = pposx;
            posy = pposy;
        }

        public cuadradoplus(/*int pwidth, int pheight*/int psize, int pposx, int pposy, bool ptop, bool pleft, bool pbottom, bool pright)
       {
            //width = pwidth;
            //height = pheight;
            size = psize;
            posx = pposx;
            posy = pposy;
            Top = ptop;
            Bottom = pbottom;
            Right = pright;
            Left = pleft;
        }

        public cuadradoplus(/*int pwidth, int pheight*/int psize, Point punto, bool ptop, bool pleft, bool pbottom, bool pright)
        {
            //width = pwidth;
            //height = pheight;
            size = psize;
            posx = punto.X;
            posy = punto.Y;
            Top = ptop;
            Bottom = pbottom;
            Right = pright;
            Left = pleft;
        }

        //public cuadradoplus(Rectangle rectangulo)
        //{
        //    width = rectangulo.Width;
        //    height = rectangulo.Height;
        //    posx = rectangulo.X;
        //    posy = rectangulo.Y;
        //}

        public void dibujarrect(Graphics lienzo)
        {
            if (Top) lienzo.DrawLine(pen, new Point(posx, posy), new Point(posx + size, posy));
            if (Left) lienzo.DrawLine(pen, new Point(posx, posy), new Point(posx, posy + size));
            if (Right) lienzo.DrawLine(pen, new Point(posx + size, posy), new Point(posx + size, posy + size));
            if (Bottom) lienzo.DrawLine(pen, new Point(posx, posy + size), new Point(posx + size, posy + size));
        }
    }
}
