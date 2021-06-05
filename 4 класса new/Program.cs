using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_класса_new
{
    class Program
    {
        static void Main(string[] args)
        {
            int otv = 1;
            while (otv != 0)
            {
                Console.WriteLine("Выберите номер задачи");
                Console.WriteLine(" 1 - Симплекс-метод; 2 - метод Джонсона;  \n 3 - задача Коммивояжёра; 4 - нахождение кратчайшего пути; \n 5 - нахождение критического пути; 6 - первоначальное распределение С-З; \n 7 - первоначальное распределение по минимальному элементу.");
                int nom = Convert.ToInt32(Console.ReadLine());
                if (nom == 1)
                {
                    vvodZnach vz = new vvodZnach();
                    vz.simplexBol();
                }
                else if (nom == 2)
                {
                    Jonson d = new Jonson();
                    d.textshit();
                }
                else if (nom == 3)
                {
                    Kommivoyajor k = new Kommivoyajor();
                    k.Jora();
                }
                else if (nom == 4)
                {
                    MinPth m = new MinPth(@"Ввод45.csv");
                }
                else if (nom == 5)
                {
                    CrtPth Cp = new CrtPth(@"Ввод45.csv");
                }
                else if (nom == 6)
                {
                    Severozapad sev = new Severozapad();
                    sev.mainMinSZ();
                }
                else if (nom == 7)
                {
                    Mimimal min = new Mimimal();
                    min.mainMin();
                }
                else 
                {
                    Console.WriteLine("Подумай еще раз!");
                }
                Console.WriteLine("Продолжить работу? Для завершения нажмите <0>");
                otv = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
            }
            Console.ReadKey();
        }
    }
}
