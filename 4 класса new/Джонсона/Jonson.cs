using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_класса_new
{
    class Jonson
    {
        public int[,] mas;
        struct MassDan
        {
            public int pervSt;
            public int vtorST;
            public override string ToString()
            {
                return pervSt + " " + vtorST;
            }
        }
        public void textshit()
        {
            List<MassDan> second = new List<MassDan>(); //считывание из файла с лист
            int[] ms1 = { };
            using (StreamReader sr = new StreamReader("Ввод2.csv"))
                while (sr.EndOfStream != true)
                {
                    string[] str = sr.ReadLine().Split(';');
                    second.Add(new MassDan() { pervSt = Convert.ToInt32(str[0]), vtorST = Convert.ToInt32(str[1]) });
                }
            int p = 0;
            Stack S = new Stack();
            Queue Q = new Queue();
            int c = 0;
            int up = second.Count;
            int c1 = second.Count - 1;
            int[,] mas = new int[10, 2];
            while (second.Count > 0)
            {
                int min = int.MaxValue;
                int max = int.MinValue;
                int n = 0;
                bool flag = true;
                foreach (MassDan u in second) //нахождение минимального значение
                {
                    if (u.pervSt < min)
                    {
                        min = u.pervSt;
                        max = u.vtorST;
                        p = n;
                        flag = true;
                    }
                    if (u.vtorST < min)
                    {
                        min = u.pervSt;
                        max = u.vtorST;
                        p = n;
                        flag = false;
                    }
                    n++;
                }


                if (flag == false) //добавление строки в массив и стек, если мин во втором столбце
                {
                    mas[c1, 0] = min;
                    mas[c1, 1] = max;
                    S.Push(second[p]);
                    c1--;
                }
                else //добавление строки в массив  очередь, если мин в первом столбце
                {
                    mas[c, 0] = min;
                    mas[c, 1] = max;
                    Q.Enqueue(second[p]);
                    c++;
                }
                second.RemoveAt(p);


            }
            for (int i = 0; i < up; i++)
            {

                Console.WriteLine(mas[i, 0] + " " + mas[i, 1]); //вывод массива
            }

            int[] vremia = new int[c1 + 1];
            int forVRE = 0;
            vremia[forVRE] = mas[0, 0];
            int sum = 0;
            for (int o = 1; o < c1 + 1; o++) //подсчет времени простоя
            {
                for (int j = 0; j < o; j++)
                {
                    sum += mas[j, 0];
                }
                for (int j = 0; j < o - 1; j++)
                {
                    sum -= mas[j, 1];
                }
                forVRE++;
                vremia[forVRE] = sum;
                sum = 0;
            }

            int maxProst = vremia.Max();
            Console.WriteLine(maxProst);
            using (StreamWriter sw = new StreamWriter("Вывод2.csv")) //запись в файл
            {
                while (Q.Count > 0)
                {
                    sw.WriteLine(Q.Dequeue());
                }
                while (S.Count > 0)
                {
                    sw.WriteLine(S.Pop());
                }
                sw.Write("Время простоя:" + maxProst);
            }

        }
    }
}