using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4_класса_new
{
    class Poten
    {


        public void Potenz(string path_in, string path_out)
        {
            List<List<int[]>> data = new List<List<int[]>>();
            List<int> M = new List<int>() { };//M предложение
            List<int> N = new List<int>() { };//N спрос
            using (StreamReader sr = new StreamReader(path_in))
            {
                while (sr.EndOfStream != true)
                {
                    var z = sr.ReadLine();
                    if (z[0] == '|')
                    {
                        string z_M = z.Replace('|', ' ');
                        var data_M = z_M.Split(';');
                        for (int a = 0; a < data_M.Length; a++)
                        {
                            M.Add(Convert.ToInt32(data_M[a]));

                        }

                        z = sr.ReadLine();
                        string z_N = z.Replace('|', ' ');
                        var data_N = z_N.Split(';');
                        for (int a = 0; a < data_N.Length; a++)
                        {
                            N.Add(Convert.ToInt32(data_N[a]));
                        }



                    }
                    else
                    {
                        var data_z = z.Split(';');
                        data.Add(new List<int[]>());
                        for (int q = 0; q < data_z.Length; q++)
                        {
                            if (Convert.ToInt32(data_z[q]) == 0)
                            {
                                data[data.Count - 1].Add(new int[] { 1000, 0 });
                            }
                            else
                            {
                                data[data.Count - 1].Add(new int[] { Convert.ToInt32(data_z[q]), 0 });
                            }
                        }
                    }
                }
            }






            int iN = 0, iM = 0, i = 0, j = 0;
            while (iN != 3 && iM != 3)
            {
                try
                {

                    if (N[iN] > M[iM])
                    {
                        data[i][j][1] = M[iM];
                        N[iN] = N[iN] - M[iM];
                        iM++;
                        i++;
                    }

                    if (N[iN] < M[iM])
                    {
                        data[i][j][1] = N[iN];
                        M[iM] = M[iM] - N[iN];
                        iN++;
                        j++;
                    }
                    if (N[iN] == M[iM])
                    {
                        data[i][j][1] = N[iN];
                        M[iM] = 0;
                        N[iN] = 0;
                        iM++;
                        iN++;
                        i++;
                        j++;
                    }

                }
                catch
                {
                    break;
                }
            }

            while (true)
            {

                int v = 0; //Количество заполенных клеток
                int _vdata = data.Count + data[0].Count - 1;

                for (int q = 0; q < data.Count; q++)
                {
                    foreach (int[] a in data[q])
                    {
                        if (a[0] == 1000)
                        {
                            a[0] = 0;
                        }
                        Console.Write("{0}[{1}]; ", a[0], a[1]);
                        if (a[0] == 0)
                        {
                            a[0] = 1000;
                        }
                        if (a[1] != 0)
                        {
                            v++;
                        }
                    }
                    Console.WriteLine();
                }


                if (v == _vdata)
                {
                    Console.WriteLine("Таблица невырожденная!");
                }

                List<int> U = new List<int>() { 0, 0, 0 };
                List<int> V = new List<int>() { 0, 0, 0 };



                //Расставление потенциалов
                for (int q = 0; q < data.Count; q++)
                {
                    for (int k = 0; k < data[q].Count; k++)
                    {
                        if (data[q][k][1] != 0)
                        {
                            if (U[k] == 0 && q != 0 && k != 0)
                            {
                                U[k] = data[q][k][0] - V[q];
                            }
                            if (V[q] == 0)
                            {
                                V[q] = data[q][k][0] - U[k];
                            }

                        }
                    }

                }




                //Проверка оптимальности распределения
                List<int[]> dnk = new List<int[]>();

                for (int q = 0; q < data.Count; q++)
                {

                    for (int k = 0; k < data[q].Count; k++)
                    {

                        if (data[q][k][1] == 0 && data[q][k][0] != 0)
                        {
                            int[] temp = new int[3];
                            temp[0] = V[q] + U[k] - data[q][k][0];
                            temp[1] = q;
                            temp[2] = k;
                            dnk.Add(temp);
                        }

                    }

                    Console.WriteLine();
                }





                //Поиск максимального элемента ДНК
                int max = dnk[0][0];
                int _imax = dnk[0][1];
                int _jmax = dnk[0][2];
                for (int q = 0; q < dnk.Count; q++)
                {
                    if (max < dnk[q][0])
                    {
                        max = dnk[q][0];
                        _imax = dnk[q][1];
                        _jmax = dnk[q][2];
                    }
                }



                //Цикл перераспределения
                if (max > 0)
                {

                    int min = 10000;
                    int selecti = _imax;
                    int selectj = _jmax;
                    bool move = true;

                    while (true)
                    {
                        for (int l = 1; l < data.Count; l++)
                        {
                            if (selecti + l < data.Count && data[selecti + l][selectj][1] != 0 && move == true)
                            {
                                selecti = selecti + l;
                                if (min > data[selecti][_jmax][1])
                                {
                                    min = data[selecti][_jmax][1];
                                }
                                move = false;

                                break;



                            }

                            if (selecti - l < data.Count && selecti - l >= 0 && data[selecti - l][selectj][1] != 0 && move == true)
                            {
                                selecti = selecti - l;
                                if (min > data[selecti][selectj][1])
                                {
                                    min = data[selecti][selectj][1];
                                }
                                move = false;
                                break;

                            }
                        }

                        if (selecti == _imax)
                        {
                            break;
                        }


                        for (int l = 1; l < data.Count; l++)
                        {
                            if (selectj + l < data.Count && data[selecti][selectj + l][1] != 0 && move == false)
                            {
                                selectj = selectj + l;
                                move = true;
                                break;



                            }

                            if (selectj - l < data.Count && selecti - l >= 0 && data[selecti][selectj - l][1] != 0 && move == false)
                            {
                                selectj = selectj - l;
                                move = true;
                                break;


                            }
                        }
                        if (selectj == _jmax)
                        {
                            break;
                        }

                    }






                    //Повторный проход по циклу распределения с распределением элементов

                    data[_imax][_jmax][1] = min;

                    selecti = _imax;
                    selectj = _jmax;
                    move = true;

                    while (true)
                    {
                        for (int l = 1; l < data.Count; l++)
                        {
                            if (selecti + l < data.Count && data[selecti + l][selectj][1] != 0 && move == true)
                            {
                                selecti = selecti + l;
                                data[selecti][selectj][1] = data[selecti][selectj][1] - min;
                                move = false;

                                break;



                            }

                            if (selecti - l < data.Count && selecti - l >= 0 && data[selecti - l][selectj][1] != 0 && move == true)
                            {
                                selecti = selecti - l;
                                data[selecti][selectj][1] = data[selecti][selectj][1] - min;
                                move = false;
                                break;

                            }
                        }

                        if (selecti == _imax)
                        {
                            break;
                        }


                        for (int l = 1; l < data.Count; l++)
                        {
                            if (selectj + l < data.Count && data[selecti][selectj + l][1] != 0 && move == false)
                            {
                                selectj = selectj + l;
                                data[selecti][selectj][1] = data[selecti][selectj][1] + min;
                                move = true;
                                break;



                            }

                            if (selectj - l < data.Count && selecti - l >= 0 && data[selecti][selectj - l][1] != 0 && move == false)
                            {
                                selectj = selectj - l;
                                data[selecti][selectj][1] = data[selecti][selectj][1] + min;
                                move = true;
                                break;


                            }
                        }
                        if (selectj == _jmax)
                        {
                            break;
                        }

                    }




                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(path_out))
                    {

                        for (int q = 0; q < data.Count; q++)
                        {
                            string line = "";
                            foreach (int[] a in data[q])
                            {
                                if (a[0] == 1000)
                                {
                                    a[0] = 0;
                                }
                                line = line + a[0] + "[" + a[1] + "];";

                            }
                            sw.WriteLine(line);
                            Console.WriteLine();
                        }
                        sw.Close();
                    }
                    break;
                }
            }
        }
    }
}
