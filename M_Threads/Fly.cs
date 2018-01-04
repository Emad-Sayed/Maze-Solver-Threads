using M_Gui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L_Threads
{
    class Fly
    {

        private int Row;
        private int Column;
        public List<List<Point>> All_Result = new List<List<Point>>();
        public List<List<Point>> Result = new List<List<Point>>();
        public List<Point> Temporary_Follow = new List<Point>();
        public Fly(int R, int C, List<Point> Temporary,List<List<Point>> Res)
        {
            All_Result=Res;
            this.Row = R;
            this.Column = C;
            for (int j = 0; j < Temporary.Count; j++)
            {
                Temporary_Follow.Add(Temporary[j]);
            }
        }
        public void CheckDown(Object o)
        {
            List<Point> CheckDownList = new List<Point>();

            for (int i = Row + 1; i < (Seq.Max_Rows * 2); i++)
            {
                if (i == Seq.Max_Rows)
                {
                    i = -1;
                    continue;
                }
                Point Current_Point = new Point(i, Column);
                if (Seq.x[Current_Point.Row, Current_Point.Column] != -3)
                {
                    List<Point> P = CheckLeft(i, Column);
                    if (P != null)
                    {
                        CheckDownList.Add(Current_Point);
                        for (int q = 0; q < P.Count; q++)
                        {
                            CheckDownList.Add(P[q]);
                        }
                        Result.Add(CheckDownList);
                        break;
                    }
                }

                if (Current_Point.Row == Row && Current_Point.Column == Column)
                {
                    Result = null;
                    break;
                }
                CheckDownList.Add(Current_Point); // List Number 1 Up Right

                if (Seq.x[i, Column] == 3)
                {
                    Result.Add(CheckDownList);
                    break;
                }

                if (Seq.x[Current_Point.Row, Current_Point.Column] == -3)
                {
                    Result = null;
                    break;
                }

            }
            if (Result != null) //if the Step can Reach to goal
            {

                for (int s = 0; s < Result.Count; s++)
                {
                    for (int q = 0; q < Result[s].Count; q++) // add the previous step to the new list to goal
                    {
                        Temporary_Follow.Add(Result[s][q]);
                    }
                    All_Result.Add(Temporary_Follow);

                }
            }

        }

        public List<Point> CheckLeft(int Row, int Column)
        {
            List<Point> CheckLeftList = new List<Point>();
            for (int i = Column - 1; i < Seq.Max_Columns * 2; i--)
            {
                if (i == -1)
                {
                    i = Seq.Max_Columns;
                    continue;
                }
                Point Current_Point = new Point(Row, i);
                if (Seq.x[Current_Point.Row, Current_Point.Column] != -3 && Seq.x[Row, i] != 3)
                {
                    List<Point> P = CheckUp(Row, i);
                    if (P != null)
                    {
                        CheckLeftList.Add(Current_Point);
                        for (int q = 0; q < P.Count; q++)
                        {
                            CheckLeftList.Add(P[q]);
                        }
                        return CheckLeftList;
                    }
                }
                if (Current_Point.Row == Row && Current_Point.Column == Column)
                {
                    return null;
                }
                CheckLeftList.Add(Current_Point);
                if (Seq.x[Row, i] == 3)
                {
                    return CheckLeftList;
                }
                if (Seq.x[Current_Point.Row, Current_Point.Column] == -3)
                {
                    return null;
                }
            }
            return null;
        }
        public List<Point> CheckUp(int Row, int Column)
        {

            List<Point> CheckUp_List = new List<Point>();

            for (int i = Row - 1; i < (Seq.Max_Rows * 2); i--)
            {
                if (i == -1)
                {
                    i = Seq.Max_Rows;
                    continue;
                }
                Point Current_Point = new Point(i, Column);
                //Console.WriteLine(i);
                if (Seq.x[Current_Point.Row, Current_Point.Column] != -3 && Seq.x[i, Column] != 3)
                {
                    List<Point> P = Checkright(i, Column);
                    if (P != null)
                    {
                        CheckUp_List.Add(Current_Point);
                        for (int q = 0; q < P.Count; q++)
                        {
                            CheckUp_List.Add(P[q]);
                        }
                        return CheckUp_List;
                    }
                }
                if (Current_Point.Row == Row && Current_Point.Column == Column)
                {
                    return null;
                }
                CheckUp_List.Add(Current_Point); // List Number 1 Up Right

                if (Seq.x[i, Column] == 3)
                {
                    return CheckUp_List;
                }

                if (Seq.x[Current_Point.Row, Current_Point.Column] == -3)
                {
                    return null;
                }

            }
            return null;
        }
        public List<Point> Checkright(int Row, int Column)
        {
            List<Point> Checkright = new List<Point>();
            for (int i = Column + 1; i < Seq.Max_Columns * 2; i++)
            {
                if (i == Seq.Max_Columns)
                {
                    i = -1;
                    continue;
                }
                Point Current_Point = new Point(Row, i);
                if (Seq.x[Current_Point.Row, Current_Point.Column] != -3 && Seq.x[Row, i] != 3)
                {
                    List<Point> P = CheckDo(Row, i);
                    if (P != null)
                    {
                        Checkright.Add(Current_Point);
                        for (int q = 0; q < P.Count; q++)
                        {
                            Checkright.Add(P[q]);
                        }
                        return Checkright;
                    }
                }
                if (Current_Point.Row == Row && Current_Point.Column == Column)
                {
                    return null;
                }
                Checkright.Add(Current_Point);
                if (Seq.x[Row, i] == 3)
                {
                    return Checkright;
                }
                if (Seq.x[Current_Point.Row, Current_Point.Column] == -3)
                {
                    return null;
                }
            }
            return null;
        }
        public List<Point> CheckDo(int Row, int Column)
        {

            List<Point> CheckDo_List = new List<Point>();

            for (int i = Row +  1; i < (Seq.Max_Rows * 2); i++)
            {
                if (i == Seq.Max_Rows)
                {
                    i = -1;
                    continue;
                }
                Point Current_Point = new Point(i, Column);
                if (Current_Point.Row == Row && Current_Point.Column == Column)
                {
                    return null;
                }
                CheckDo_List.Add(Current_Point); // List Number 1 Up Right

                if (Seq.x[i, Column] == 3)
                {
                    return CheckDo_List;
                }

                if (Seq.x[Current_Point.Row, Current_Point.Column] == -3)
                {
                    return null;
                }

            }
            return null;
        }
    }
}
