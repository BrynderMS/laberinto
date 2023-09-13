using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static byte info = 0;
        static void Main(string[] args)
        {
            //int[] a = new int[10] { 1, 5, 2, 4, 3, 5, 6, 3, 1, 7 };
            //a.ToList().ForEach(x => Console.WriteLine(x));
            //for(var i = 0; i < a.Length - 1; i++)
            //{
            //    Console.WriteLine(a[i]);
            //}
            
            const byte arriba = 1;
            const byte derecha = 1 << 1;
            const byte abajo = 1 << 2;
            const byte izquierda = 1 << 3;

            info += arriba;
            info += abajo;


            if ((info == (arriba | abajo))) Console.Write("si");
            else Console.Write("no");
        }
    }
}
