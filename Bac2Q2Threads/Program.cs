using System;
using System.Threading;

namespace Bac2Q2Threads
{
    internal class Program
    {
        private static int iCommun;
        // mutex sous forme de file, le suivant à demander sera mis en file d'attente FIFO
        // permet de bloquer les threads qui se partagent ce même verrou et laisser libres les autres => moins contraignant que le lock
        private static ReaderWriterLock readerWriterLock = new ReaderWriterLock(); 
        
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
            {
                readerWriterLock.AcquireWriterLock(-1);
                n = iCommun;
                n++;
                Console.Write("A{0, -5}", iCommun); // -5 => codé sur 5 caractères, remplit le reste avec des espaces

                iCommun = n;
                readerWriterLock.ReleaseWriterLock();
            }
        }

        private static void B()
        {
            int n;
            
            for (int i = 0; i < 500; i++)
            {
                readerWriterLock.AcquireWriterLock(-1);
                n = iCommun;
                n++;
                Console.Write("B{0, -5}", iCommun);
                
                iCommun = n;
                readerWriterLock.ReleaseWriterLock();
            }
        }
    }
}