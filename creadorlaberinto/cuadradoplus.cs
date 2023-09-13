using System.Drawing;

namespace creadorlaberinto
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

        public int Posx
        {
            get
            {
                return posx;
            }

            set
            {
                posx = value;
            }
        }

        public int Posy
        {
            get
            {
                return posy;
            }

            set
            {
                posy = value;
            }
        }

        public cuadradoplus(/*int pwidth,int pheight,*/int psize, int pposx,int pposy)
        {
            //width = pwidth;
            //height = pheight;
            size = psize;
            Posx = pposx;
            Posy = pposy;
        }

        public cuadradoplus(/*int pwidth, int pheight*/int psize, int pposx, int pposy, bool ptop, bool pleft, bool pbottom, bool pright)
       {
            //width = pwidth;
            //height = pheight;
            size = psize;
            Posx = pposx;
            Posy = pposy;
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
            Posx = punto.X;
            Posy = punto.Y;
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
            if (Top) lienzo.DrawLine(pen, new Point(Posx, Posy), new Point(Posx + size, Posy));
            if (Left) lienzo.DrawLine(pen, new Point(Posx, Posy), new Point(Posx, Posy + size));
            if (Right) lienzo.DrawLine(pen, new Point(Posx + size, Posy), new Point(Posx + size, Posy + size));
            if (Bottom) lienzo.DrawLine(pen, new Point(Posx, Posy + size), new Point(Posx + size, Posy + size));
        }

        public Rectangle ToRectangle()
        {
            return new Rectangle(new Point(posx, posy), new Size(size,size));
        }
    }
}
