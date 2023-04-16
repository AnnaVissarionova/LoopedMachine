using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopedMachine
{
    public class Machine
    {
        Dictionary<int[], int> comb;
        Dictionary<int, Dictionary<char, int[]>> q_table;
        int length;
        int steps_count;
        public Machine(int[] conds, string[] transf, int l)
        {
            q_table = DefineConditionsDictionary(conds, transf);
            comb = CreateCombDict(q_table);
            length = l;
            steps_count = 0;
        }
        static Dictionary<int[], int> CreateCombDict(Dictionary<int, Dictionary<char, int[]>> q_table)
        {
            var dict = new Dictionary<int[], int>();
            foreach (var i in q_table)
            {
                foreach (var d in i.Value)
                {
                    var tf = GetTransformInArr(q_table, i.Key, d.Key);
                    dict.Add(tf, 0);
                }
            }
            return dict;
        }

        public void PrintComb()
        {
            foreach (var a in comb)
            {
                foreach (var e in a.Key)
                {
                    Console.Write($"{e} ");
                }
                Console.WriteLine(a.Value);
            }
        }

        bool CheckCobminations(Dictionary<int[], int> comb)
        {
            foreach (var i in comb)
            {
                if (i.Value == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static int[] GetTransformInArr(Dictionary<int, Dictionary<char, int[]>> q_table, int q, char c)
        {
            var res = new int[5];
            res[0] = q;
            var dict = q_table.GetValueOrDefault(q);
            //res[1] = c == 'a' ? 0 : 1;
            res[1] = c;
            res[2] = dict.GetValueOrDefault(c)[0];
            res[3] = dict.GetValueOrDefault(c)[1];
            res[4] = dict.GetValueOrDefault(c)[2];
            return res;
        }

        string CheckText(string text)
        {
            while (text.Length < length)
            {
                text = text + "^";
            }
            return text;
        }

        static Dictionary<int, Dictionary<char, int[]>> DefineConditionsDictionary(int[] conds, string[] transforms)
        {
            var dict = new Dictionary<int, Dictionary<char, int[]>>();
            for (var i = 0; i < conds.Length; i++)
            {
                string[] cur_tf = transforms.Where(x => x.StartsWith($"q{i + 1}")).ToArray();
                var inner_dict = new Dictionary<char, int[]>();
                foreach (var c in cur_tf)
                {
                    var ss = c.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
                    // Console.WriteLine(ss[2]);
                    var t = "";
                    foreach (var h in ss[3..])
                    {
                        t = t + " " + h;
                    }
                    Console.WriteLine(ss[1][0]);
                    inner_dict.Add(ss[1][0], ParseTransormation(t));
                }
                dict.Add(conds[i], inner_dict);

                //dict.Add(conds[i], inner_dict[i]);
            }
            return dict;
        }

        public (int, int, string) OneStep(int q, string text, int cur_pos)
        {
            text = CheckText(text);
            (int next_cond, int next_pos) = (-1, -1);
            var cur_char = text[cur_pos];
            var dict = q_table.GetValueOrDefault(q);
            if (dict.GetValueOrDefault(cur_char) == null)
            {
                Console.WriteLine("step is not made");
                return (-1, -1, text); //сигнал об окончании работы
            }
            //text = text[..cur_pos] + (dict.GetValueOrDefault(cur_char)[1] == 0 ? 'a' : 'b') + text[(cur_pos+1)..];
            text = text[..cur_pos] + (char)dict.GetValueOrDefault(cur_char)[1] + text[(cur_pos + 1)..];
            next_cond = dict.GetValueOrDefault(cur_char)[0];
            next_pos = cur_pos + dict.GetValueOrDefault(cur_char)[2];

            Console.WriteLine("step is made");

            if (next_pos == -1) { next_pos = length - 1; }
            if (next_pos == length) { next_pos = 0; }

            steps_count++;
            return (next_cond, next_pos, text);
        }


        public (int, int, string) MakeNSteps(int q, string text, int cur_pos, int steps)
        {
            text = CheckText(text);
            for (var i = 0; i < steps; i++)
            {
                (q, cur_pos, text) = OneStep(q, text, cur_pos);
                if(q == -1)
                {
                    break;
                }
            }
            return (q, cur_pos, text);
        }

        public void RunMachine(int q, string text, int cur_pos)
        {
            text = CheckText(text);
            while (q != -1)
            {
                if (CheckCobminations(comb) && steps_count > length)
                {
                    Console.WriteLine("машина прошла все комбинации и зациклилась");
                    break;
                }
                foreach (var c in comb)
                {
                    PrintComb(c);
                }
                (int q_prev, int pos_prev, char prev) = (q, cur_pos, text[cur_pos]);
                (q, cur_pos, text) = OneStep(q, text, cur_pos);
                if (q != -1)
                {
                    UpdateComb(q_prev, prev);
                }
                Console.Write($"text : {text} ; ");
                Console.Write($"cond : {q}; ");
                Console.WriteLine($"pos : {cur_pos}");
            }
           if (q == -1)  Console.WriteLine("машина не зациклилась");
        }

       

        static void PrintComb(KeyValuePair<int[], int> t)
        {
            Console.WriteLine($"q{t.Key[0]} {t.Key[1]} -> q{t.Key[2]} {t.Key[3]} {t.Key[4]} sum = {t.Value}");
        }

         void UpdateComb(int q, char c)
        {
            Console.WriteLine("starting update");
            var arr = new int[5];
            arr[0] = q;
            arr[1] = c;
            var dict = q_table.GetValueOrDefault(q);
            arr[2] = dict.GetValueOrDefault(c)[0];
            arr[3] = dict.GetValueOrDefault(c)[1];
            arr[4] = dict.GetValueOrDefault(c)[2];
            /*foreach (var i in comb)
            {
                if (CheckArrs(i.Key, arr))
                {
                    var s = i.Value;
                    comb.Remove(i.Key);
                    Console.WriteLine("updates1");
                    comb.Add(i.Key, s + 1);
                }
            }*/
            var kvarr = comb.Keys;
            for(var i =0; i < kvarr.Count; i++)
            {
                if (CheckArrs(kvarr.ElementAt(i), arr))
                {
                    var s = comb.GetValueOrDefault(kvarr.ElementAt(i));
                    //Console.WriteLine("removeing " + comb.Remove(kvarr.ElementAt(i)));
                    Console.WriteLine("updates1");
                    comb[kvarr.ElementAt(i)] = s + 1;
                }
            }
              
            
        }

        

        static bool CheckArrs(int[] a, int[]b)
        {
            if(a.Length == b.Length)
            {
                for(var i = 0; i < a.Length; i++)
                {
                    if (a[i] != b[i])
                    {
                        return false;
                    }
                }
                return true;
            }
            return false;
        } 

        static int[] ParseTransormation(string tf)
        {
            var res = new int[3];
            // Console.WriteLine(tf);
            var ss = tf.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToArray();
            // Console.WriteLine(ss[0]);
            //Console.WriteLine(tf);
            res[0] = int.Parse(ss[0].Substring(1));
            // res[1] = ss[1].Equals("a") ? 0 : 1;
            res[1] = ss[1][0];
            res[2] = ss[2].Equals("+1") ? 1 : -1;
            return res;
        }
    }
}
