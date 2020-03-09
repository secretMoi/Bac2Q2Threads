using System;
using System.Threading;

namespace Bac2Q2Threads
{
    internal class Program
    {
        private static int iCommun;
        
        public static void Main(string[] args)
        {
            Thread a = new Thread(A);
            Thread b = new Thread(B);
            
            a.Start();
            b.Start();

            a.Join();
            b.Join();
            
            Console.WriteLine("FINI");
            Console.ReadLine();
        }
        
        private static void A()
        {
            int n;
            
            for (int i = 0; i < 500; i++)
                lock (typeof(Program))
                {
                    n = iCommun;
                    n++;
                    Console.Write("A{0, -5}", iCommun); // -5 => codé sur 5 caractères, remplit le reste avec des espaces

                    iCommun = n;
                }
        }

        private static void B()
        {
            int n;
            
            for (int i = 0; i < 500; i++)
                lock (typeof(Program))
                {
                    n = iCommun;
                    n++;
                    Console.Write("B{0, -5}", iCommun);
                    
                    iCommun = n;
                }
        }
    }
}