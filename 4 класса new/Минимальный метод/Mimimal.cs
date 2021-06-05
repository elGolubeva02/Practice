using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_класса_new
{
    class Mimimal
    {
        public static void Основная(int[] N, int[] M, int[,] Матрица, int[,] Массив)
        {
            int max = Матрица[0, 0];
            for (int i = 0; i < Матрица.GetLength(0); i++)
            {
                for (int j = 0; j < Матрица.GetLength(1); j++)
                {
                    if (max < Матрица[i, j])
                        max = Матрица[i, j];
                }
            }
            int min = max + 1;
            int mini = 0;
            int minj = 0;
            for (int ii = 0; ii < M.Length; ii++)
                for (int jj = 0; jj < N.Length; jj++)
                    if (min > Матрица[ii, jj] && M[ii] != 0 && N[jj] != 0)
                    {
                        min = Матрица[ii, jj];
                        mini = ii;
                        minj = jj;
                    }
            if ((M[mini] - N[minj]) >= 0)
            {
                M[mini] = M[mini] - N[minj];
                Массив[mini, minj] = N[minj];
                N[minj] = N[minj] - N[minj];
            }
            if ((N[minj] - M[mini]) >= 0 && (N[minj] - M[mini]) != 0)
            {
                N[minj] = N[minj] - M[mini];
                Массив[mini, minj] = M[mini];
                M[mini] = M[mini] - M[mini];
            }
            int check = 0;
            for (int ii = 0; ii < M.Length; ii++)
            {
                if (M[ii] != 0)
                    check++;
            }
            for (int ii = 0; ii < N.Length; ii++)
            {
                if (N[ii] != 0)
                    check++;
            }
            if (check != 0)
            {
                Основная(N, M, Матрица, Массив);
            }
            else
            {
                Console.WriteLine(); Console.WriteLine();
                Console.Write("| \t ");
                foreach (int a in N)
                {
                    Console.Write("| \t" + a);
                }
                Console.WriteLine();
                for (int i = 0; i < Массив.GetLength(0); i++)
                {
                    Console.Write("| \t" + M[i]);
                    for (int j = 0; j < Массив.GetLength(1); j++)
                    {
                        Console.Write("| \t" + Массив[i, j]);
                    }
                    Console.WriteLine();
                }
                using (StreamWriter sw = new StreamWriter(@"Вывод7.csv"))
                {
                    sw.Write("| \t ");
                    foreach (int a in N)
                    {
                        sw.Write("| \t" + a);
                    }
                    sw.WriteLine();
                    for (int i = 0; i < Массив.GetLength(0); i++)
                    {
                        sw.Write("| \t" + M[i]);
                        for (int j = 0; j < Массив.GetLength(1); j++)
                        {
                            sw.Write("| \t" + Массив[i, j]);
                        }
                        sw.WriteLine();
                    }
                }
            }
        }

        public void mainMin()
        {
            int[] ms1 = { };
            string str = "";
            int[,] mas;
            int[] N, M;
            int[,] rab;
            using (StreamReader sr = new StreamReader(@"Ввод7.csv"))
            {
                str = sr.ReadToEnd();
                string[] st = str.Split('\n');
                ms1 = Array.ConvertAll(st[1].Split(';'), int.Parse);
                mas = new int[st.Length, ms1.Length];
                for (int i = 0; i < st.Length; i++)
                {
                    ms1 = Array.ConvertAll(st[i].Split(';'), int.Parse);
                    for (int j = 0; j < ms1.Length; j++)
                    {
                        mas[i, j] = ms1[j];
                    }
                }
            }
            N = new int[mas.GetLength(1) - 1];
            for (int i = 1; i < mas.GetLength(1); i++)
            {
                N[i - 1] = mas[0, i];
            }
            M = new int[mas.GetLength(0) - 1];
            for (int i = 1; i < mas.GetLength(0); i++)
            {
                M[i - 1] = mas[i, 0];
            }
            rab = new int[mas.GetLength(0) - 1, mas.GetLength(1) - 1];
            for (int i = 1; i < mas.GetLength(0); i++)
            {
                for (int j = 1; j < mas.GetLength(1); j++)
                {
                    rab[i - 1, j - 1] = mas[i, j];
                }
                Console.WriteLine();
            }
            Console.Write("| \t ");
            foreach (int a in N)
            {
                Console.Write("| \t" + a);
            }
            Console.WriteLine();
            for (int i = 0; i < rab.GetLength(0); i++)
            {
                Console.Write("| \t" + M[i]);
                for (int j = 0; j < rab.GetLength(1); j++)
                {
                    Console.Write("| \t" + rab[i, j]);
                }
                Console.WriteLine();
            }
                int[,] Массив = new int[rab.GetLength(0), rab.GetLength(1)];
            Основная(N, M, rab, Массив);
            Console.ReadKey();
        }

    }
}

