using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_класса_new
{
    class Severozapad
    {
       
            struct Element
            {

                public int Delivery { get; set; }
                public int Value { get; set; }

                public static int MinEl(int a, int b)
                {
                    if (a > b) return b;
                    if (a == b)
                    {
                        return a;
                    }
                    else return a;
                }

            }
            public void mainMinSZ()
            {
                int[] a = { }, b = { }, c;
                // int[,] C = { };
                string a1, b1, c1;
                int n = 0, m = 0;
                int i = 0;
                int j = 0;
                Element[,] C = new Element[n, m];
                using (StreamReader sr = new StreamReader("Ввод6.csv"))
                {
                    while (sr.EndOfStream != true)
                    {
                        n = int.Parse(sr.ReadLine());
                        m = int.Parse(sr.ReadLine());
                        C = new Element[n, m];
                        a1 = sr.ReadLine();
                        b1 = sr.ReadLine();
                        c1 = sr.ReadToEnd();
                        string[] st = c1.Split('\n');
                        a = Array.ConvertAll(a1.Split(';'), int.Parse);
                        b = Array.ConvertAll(b1.Split(';'), int.Parse);
                        int k = 0;
                        foreach (string o in st)
                        {
                            c = Array.ConvertAll(o.Split(';'), int.Parse);

                            for (j = 0; j < m; j++)
                            {
                                C[k, j].Value = Convert.ToInt32(c[j]);
                            }
                            k++;
                        }
                    }
                }

                i = j = 0;

                // действуем по алгоритму
                // идём с северо-западного элемента

                while (i < n && j < m)
                {
                    try
                    {
                        if (a[i] == 0) i++;
                        if (b[j] == 0) j++;
                        if (a[i] == 0 && b[j] == 0) { i++; j++; }
                        C[i, j].Delivery = Element.MinEl(a[i], b[j]);
                        a[i] -= C[i, j].Delivery;
                        b[j] -= C[i, j].Delivery;
                    }
                    catch { }
                }
                int ResultFunction = 0;
                //считаем целевую функцию
                for (i = 0; i < n; i++)
                {
                    for (j = 0; j < m; j++) { ResultFunction += (C[i, j].Value * C[i, j].Delivery); }
                }
                using (StreamWriter sw = new StreamWriter("Вывод6.csv"))
                {
                    //выводим массив на экран
                    for (i = 0; i < n; i++)
                    {
                        for (j = 0; j < m; j++)
                        {
                            if (C[i, j].Delivery != 0)
                            {
                                sw.Write("{0} ", C[i, j].Value);
                                sw.Write("({0}) | ", C[i, j].Delivery);
                            }
                            else
                                sw.Write("{0}({1}) | ", C[i, j].Value, C[i, j].Delivery);
                        }
                        sw.WriteLine();
                    }
                    sw.WriteLine(" Целевая функция = {0}", ResultFunction);
                
                }
            Console.ReadKey();
            }

        }
    }

