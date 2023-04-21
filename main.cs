using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopedMachine
{
    internal class main
    {

        
        static void Main(string[] args)
        {

            var q = 0;
            var m = 0;
            var tf = new string[0];
            var text = "";
            var length = 0;

            var inp = "";

            Console.Write("Введите количество состояний машины (exit для выхода) : ");
            inp = Console.ReadLine();
            while (!inp.Equals("exit"))
            {
                
                q = int.Parse(inp);
                Console.Write("Введите количество строк с описанием машины : ");
                m = int.Parse(Console.ReadLine());
                Console.WriteLine("Введите строки с описанием машины : ");

                for (var i = 0; i < m; i++ )
                {
                    tf = tf.Append(Console.ReadLine()).ToArray();
                }

                Console.WriteLine("Введите входные данные на ленте (без пробелов) : ");
                text = Console.ReadLine();

                Console.Write("Введите длину ленты : ");
                length = int.Parse(Console.ReadLine());


                Console.WriteLine("~~~~~~~ Запускаем проверку машины");
                var mch = new Machine(q, tf, length);

                (int nc, int np) = (1, 0);

                mch.RunMachine(nc, text, np);

                Console.WriteLine();

                Console.Write("Введите количество состояний машины (exit для выхода) : ");
                inp = Console.ReadLine();
            }

            tf = new string[]
        {
            "q1 a  -> q1 b +1",
            "q1 b  -> q1 a +1",
           // "q2 a  -> q1 b +1",
            //"q2 b  -> q1 a -1",
            "q1 ^  -> q2 ^ +1",
            //"q2 ^  -> q1 ^ +1",


        };

        }

       
        
    }
}
