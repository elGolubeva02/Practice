using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_класса_new
{
    public class Kommivoyajor
    {
        static int uzli;
        static int[,] time;
        static int[] f = new int[0];
        static string[] puti = new string[0];
        public void Jora()
        {
            //Читает из файлика
            using (StreamReader sr = new StreamReader(@"Ввод3.csv"))
            {
                int[] ms1 = { };
                string str = "";
                str = sr.ReadToEnd(); //Считал до конца в строку
                string[] st = str.Split('\n');//Стринговый массив по строчно по знаку \n
                ms1 = Array.ConvertAll(st[1].Split(';'), int.Parse);
                time = new int[st.Length, ms1.Length];
                for (int i = 0; i < st.Length; i++)//Цикл записи в двумерный массив из одномерных!
                {
                    ms1 = Array.ConvertAll(st[i].Split(';'), int.Parse);
                    for (int j = 0; j < ms1.Length; j++)
                    {
                        time[i, j] = ms1[j];
                    }
                }
                uzli = time.GetLength(0); //Количество узлов
            }
            for (int i = 0; i < uzli; i++) //Цикл построения путей, по количеству узлов
            {
                Array.Resize(ref puti, puti.Length + 1); // Переопределение массива путей
                Array.Resize(ref f, f.Length + 1);//Переопределение массива с фунцией путей
                puti[puti.Length - 1] = $"{i + 1}"; // Запись в последную пустую строку массива путей точки начала пути
                puti = schet(puti.Length - 1, i, 0);//Вызов метода для составления пути
            }
            //Запись в файл
            using (StreamWriter sw = new StreamWriter(@"Вывод3.txt"))
            {
                sw.Write("Все пути:\r\n");
                for (int i = 0; i < puti.Length; i++)
                {
                    sw.Write($"Путь: {puti[i]}, F = {f[i]} у.д.е.\r\n");
                }
                int mina = f[0];
                for (int i = 1; i < puti.Length; i++)
                {
                    if (mina > f[i])
                        mina = f[i];
                }
                sw.Write("\r\nОТВЕТ:\r\nПути с наименьшими затратами: \r\n");
                for (int i = 0; i < puti.Length; i++)
                {
                    if (f[i] == mina)
                        sw.Write((puti[i] + ", F = " + f[i] + "\r\n"));
                }
            }
            //Вывод на экран
            Console.WriteLine("Все пути:\r\n");
            for (int i = 0; i < puti.Length; i++)
            {
                Console.WriteLine($"Путь: {puti[i]}, F = {f[i]} у.д.е.\r\n");
            }
            int min = f[0];
            for (int i = 1; i < puti.Length; i++)
            {
                if (min > f[i])
                    min = f[i];
            }
            Console.WriteLine("\r\nОТВЕТ:\r\nПути с наименьшими затратами: \r\n");
            for (int i = 0; i < puti.Length; i++)
            {
                if (f[i] == min)
                    Console.WriteLine((puti[i] + ", F = " + f[i] + "\r\n"));
            }
        }
        public static string[] schet(int pi, int i1, int i2) //Принимает i пути в массиве путей, начальный узел и столбец
        {
            int min = time[i1, i2], mi2 = i2;//Мин началу строки,индекс минимального элемента 0
            if (puti[pi].Length != time.GetLength(0) * 2 - 1)//Сравнение длины пути на итерации с эталоном длины (11 символов.)
            {
                //Поиск первого элемента который мы не использовали в пути
                for (int j = 0; j < time.GetLength(0); j++)
                {
                    if (time[i1, j] != 0)
                    {
                        if (poisksovpad(pi, j + 1))//Вызов метода смотрящего был ли элемент у нас в пути или нет
                        {
                            min = time[i1, j];//значение
                            mi2 = j;//индекс
                            break;
                        }
                    }
                }
                //Поиск реального минимального элемента
                for (int j = 0; j < time.GetLength(0); j++)
                {
                    if (time[i1, j] != 0)
                    {
                        if (poisksovpad(pi, j + 1))//Вызов метода смотрящего был ли элемент у нас в пути или нет
                        {
                            if (min > time[i1, j] || (min == time[i1, j] && j == mi2))
                            {
                                min = time[i1, j];
                                mi2 = j;
                            }
                        }
                    }
                }
                for (int j = i2; j < time.GetLength(0); j++) //Цикл проверки на возможность ветвления
                {
                    if (poisksovpad(pi, j + 1))//Проверка есть ли одинаковые минимальные элементы по цене
                        if (min == time[i1, j] && j != mi2)//Если мы нашли такую же цену, то смотрим не нашли ли мы тот же самый элемент, который мы нашли как минимум
                        {
                            //Создание ветвления
                            string s = puti[pi];//Запомнили путь старый
                            int ff = f[pi];//Запомниили функцию старую
                            puti[pi] += $"-{mi2 + 1}";//Записали новую точку после ветления
                            f[pi] += min;//Прибавили функцию
                            puti = schet(pi, mi2, 0);//Заполняем новую ветку вызвав метод построения путей
                            //Возврат к страрой ветке
                            mi2 = j; //Возврат к строму индексу минимума
                            Array.Resize(ref puti, puti.Length + 1);//Увеличение массива путей, так как последний элемент полон
                            Array.Resize(ref f, f.Length + 1);//Увеличение массива функций, так как последний элемент полон
                            pi = f.Length - 1; //Смещение к нашему старому пути индекса
                            puti[pi] = s; //Продолжаем путь
                            f[pi] = ff; //Продолжаем функцию
                        }
                }
            }
            else
            {
                //Поиск начала пути
                foreach (string s in puti[pi].Split('-'))
                {
                    mi2 = Convert.ToInt32(s);
                    min = time[i1, mi2 - 1];
                    break;
                }
                puti[pi] += $"-{mi2}";//Запись начальной точки в конец
                f[pi] += min; //Увеление массива с функцией
                return puti;
            }
            //Запись новой точки в путь
            puti[pi] += $"-{mi2 + 1}";
            f[pi] += min;//Увеличие массива с функцией
            puti = schet(pi, mi2, 0); //Получение пути/продолжение построения пути
            return puti;
        }
        public static bool poisksovpad(int pi, int j)
        {
            bool sch = true; //совпадений нет
            foreach (string s in puti[pi].Split('-'))
            {
                if (Convert.ToInt32(s) == j)
                {
                    sch = false; //совпадение есть
                    break;
                }
            }
            return sch;
        }
    }
}
