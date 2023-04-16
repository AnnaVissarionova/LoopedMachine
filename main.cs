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
            var tf = new string[]
        {
            "q1 a  -> q1 b +1",
            "q1 b  -> q1 a +1",
           // "q2 a  -> q1 b +1",
            //"q2 b  -> q1 a -1",
            "q1 ^  -> q2 ^ +1",
            //"q2 ^  -> q1 ^ +1",


        };

            var mch = new Machine(new int[] { 1, 2 }, tf, 5);
            /*var dict = DefineCOnditionsDictionary(new int[] { 1, 2, 3 }, tf);
            var dict_comb = CreateCombDict(dict);
*/
            /*foreach(var k in dict)
            {
                foreach(var t in k.Value)
                {
                    Console.WriteLine($"q{k.Key} {t.Key} -> q{t.Value[0]} {(t.Value[1] == '1' ? 'b' : 'a')} {t.Value[2]}");
                }
            }*/
            var text = "aba^";
            (int nc, int np) = (1, 0);

            var length = 5;

            mch.PrintComb();
            mch.RunMachine(nc, text, np);

            /*(nc, np, text) = OneStep(nc, text, np, dict, 2);
            Console.Write($"text : {text} ; ");
            Console.Write($"cond : {nc}; ");
            Console.WriteLine($"pos : {np}");*/

            /* for(var i =0; i < 5; i++)
             {
                 ( nc, np, text) = OneStep(nc, text, np, dict, 2);
                 Console.Write($"text : {text} ; ");
                 Console.Write($"cond : {nc}; ");
                 Console.WriteLine($"pos : {np}");
             }*/

           /* foreach(var a in dict_comb)
            {
                foreach(var e in a.Key)
                {
                    Console.Write($" {e}");
                }
                Console.WriteLine(a.Value);
            }*/

        }

       

        
        

    }
}
