using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_класса_new
{
    public class vvodZnach
    {
        public double[] bufMass = { };
        public void simplexBol()
        {
            double[] ms1 = { };
            string str1 = "";
            double[,] mas;
            int raz1 = 0, d = 0;
            //Запись из csv в массив
            using (StreamReader sr = new StreamReader(@"Ввод1.csv"))
            {
                sr.ReadLine();
                str1 = sr.ReadToEnd();
                string[] st = str1.Split('\n');
                raz1 = st.Length;
                ms1 = Array.ConvertAll(st[0].Split(';'), double.Parse);
                d = ms1.Length;
                mas = new double[raz1, d];
                for (int i = 0; i < raz1; i++)
                {
                    ms1 = Array.ConvertAll(st[i].Split(';'), double.Parse);
                    for (int j = 0; j < d; j++)
                    {
                        mas[i, j] = ms1[j];

                    }
                }
                // Меняем первый и последний столбцы местами для того что бы удобно вводить в csv файл ограничения
                for (int i = 0; i < raz1; i++)
                {
                    for (int j = 0; j < d; j += d - 1)
                    {
                        double tmp = mas[i, j];
                        mas[i, j] = mas[i, d - 1];
                        mas[i, d - 1] = tmp;
                    }

                }
                // делаем строку оценок отрицательной для корректного вывода
                for (int i = 0; i < raz1; i++)
                {
                    for (int j = 0; j < d; j++)
                    {
                        if (i == raz1 - 1)
                        {
                            mas[i, j] = mas[i, j] * (-1);
                        }
                    }
                }
                Console.WriteLine("Исходная матрица");
                for (int i = 0; i < raz1; i++)
                {
                    for (int j = 0; j < d; j++)
                    {
                        Console.Write($"{mas[i, j],5}");
                    }
                    Console.WriteLine();
                }
            }
            //Объявляем массив размерностью в два раза больше, чем введенный массив для фиктивных переменных
            double[] result = new double[raz1 * 2];
            double[,] table_result;
            
            //Конструктор класса
            Simplex S = new Simplex(mas);
            //Основной метод программы
            table_result = S.Calculate(result);
           
            for (int i = 0; i < table_result.GetLength(0); i++)
            {
                for (int j = 0; j < table_result.GetLength(1); j++)
                {
                    if (i == raz1 - 1)
                    {
                        table_result[i, j] = table_result[i, j] * (-1);
                    }
                }
            } 
            //Запись в csv файл
            using (StreamWriter sw = new StreamWriter(@"Вывод1.csv"))
            {
                sw.WriteLine("reshenie:");
                for (int i = 0; i < table_result.GetLength(0); i++)
                {
                    for (int j = 0; j < table_result.GetLength(1); j++)
                        sw.Write($"{Math.Round(table_result[i, j]),5}" + ";");
                    sw.WriteLine();
                }
                int ind1 = 1;
                for (int j = d - 2; j >= 0; j--)
                {
                    sw.WriteLine("X[{0}] = {1}", ind1, result[j]);
                    ind1++;
                }
                sw.WriteLine("F = " + (table_result[table_result.GetLength(0) - 1, 0] * -1));
                sw.WriteLine("F' = " + (table_result[table_result.GetLength(0) - 1, 0]));
            } 
            //Вывод результатов
            Console.WriteLine("Решенная симплекс-таблица: ");
            for (int i = 0; i < table_result.GetLength(0); i++)
            {
                for (int j = 0; j < table_result.GetLength(1); j++)
                    Console.Write($"{Math.Round(table_result[i, j], 2),10}");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("Решение:");
           

            int ind = 1;
            for (int j = d - 2; j >= 0; j--)
            {
                Console.WriteLine("X[{0}] = {1}", ind, result[j]);
                ind++;
            }
            Console.WriteLine("F = " + (table_result[table_result.GetLength(0) - 1, 0] * -1));
            Console.WriteLine("F' = " + (table_result[table_result.GetLength(0) - 1, 0]));
        }
    }
}
