using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using L_Threads;
using System.Threading.Tasks;
namespace M_Gui
{
    class Seq
    {
        public List<List<Point>> Result = new List<List<Point>>();
        public static int Max_Rows ;
        public static int Max_Columns ;
        public Point My_Real_Position = null;
        public Point My_Goal_Position = null;

        public static  int[,] x;

        public Seq(int[,] xx, int rows, int Colums, int q, int y, int a, int b)
        {
            Max_Rows=rows;
            Max_Columns = Colums;
            x = xx;
            My_Real_Position = new Point(q, y);
            My_Goal_Position = new Point(q, b);
            x[a, b] = 3;
            Stopwatch MyTime = new Stopwatch();
            MyTime.Start();
            Generator();
            MyTime.Stop();
            //Console.WriteLine("Time is " + MyTime.ElapsedMilliseconds);
        }


        public List<List<Point>> CheckDown(int Row, int Column)
        {
            List<List<Point>> Result = new List<List<Point>>();
            List<Point> CheckDownList = new List<Point>();

            for (int i = Row + 1; i < (Max_Rows * 2); i++)
            {
                if (i == Max_Rows)
                {
                    i = -1;
                    continue;
                }
                Point Current_Point = new Point(i, Column);
                if (x[Current_Point.Row, Current_Point.Column] != -3)
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
                        return Result;
                    }
                }

                if (Current_Point.Row == Row && Current_Point.Column == Column)
                {
                    return null;
                }
                CheckDownList.Add(Current_Point); // List Number 1 Up Right

                if (x[i, Column] == 3)
                {
                    Result.Add(CheckDownList);
                    return Result;
                }

                if (x[Current_Point.Row, Current_Point.Column] == -3)
                {
                    return null;
                }

            }
            return Result;
        }


        public List<Point> CheckLeft(int Row, int Column)
        {
            List<Point> CheckLeftList = new List<Point>();
            for (int i = Column - 1; i < Max_Columns * 2; i--)
            {
                if (i == -1)
                {
                    i = Max_Columns;
                    continue;
                }
                Point Current_Point = new Point(Row, i);
                if (Current_Point.Row == Row && Current_Point.Column == Column)
                {
                    return null;
                }
                CheckLeftList.Add(Current_Point);
                if (x[Row, i] == 3)
                {
                    return CheckLeftList;
                }
                if (x[Current_Point.Row, Current_Point.Column] == -3)
                {
                    return null;
                }
            }
            return null;
        }


        /*
public int x[][] = {
    {0,        0,         0,        0,        0,       0,       0},
    {0,        0,         0,        -1,        0,       0,       0},
    {0,        0,         0,       0,        0,       0,       0},
    {0,        0,         -1,        0,        0,       0,       0},
    {0,        0,         0,        -1,        0,       0,       0},
    {0,        0,         0,        0,        0,       0,       0},
};
 */



        public void Generator()
        {
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            List<Point> rowSteps = new List<Point>();
            int MaxThreads;
            int Comp1;
            int MinThreads;
            int Comp2;
            int AvailableThreads;
            int Comp3;
            int toProcess = Max_Rows;
            for (int i = My_Real_Position.Row - 1; i < Max_Rows * 2; i--)
            {
                List<Point> rowSteps_Temp_Threads = new List<Point>();// Work For threads
                if (i == -1) //Circular Handling
                {
                    i = Max_Rows;
                    continue;
                }
                if (x[i, this.My_Real_Position.Column] == -3)
                {
                    break;
                }
                rowSteps.Add(new Point(i, My_Real_Position.Column));
                if (x[i, this.My_Real_Position.Column] == 3)
                {
                    this.Result.Add(rowSteps);
                    break;
                } 
                for (int j = 0; j < rowSteps.Count; j++)
                {
                    rowSteps_Temp_Threads.Add(rowSteps[j]);

                }
                //FullCheckDownLeft(rowSteps);//0014868 //0024855
                ThreadPool.QueueUserWorkItem(new WaitCallback( FullCheckDownLeft), rowSteps_Temp_Threads);
                ThreadPool.GetMaxThreads(out MaxThreads,out Comp1);
                Console.WriteLine("The Max Number Of Threads is : {0}",MaxThreads);
                ThreadPool.GetMinThreads(out MinThreads, out Comp2);
                //ThreadPool.SetMaxThreads(10);
                Console.WriteLine("The Min Number Of Threads is : {0}", MinThreads);
                ThreadPool.GetAvailableThreads(out AvailableThreads, out Comp3);
                Console.WriteLine("The Available Number Of Threads is : {0}", AvailableThreads);

                /* ThreadPool.QueueUserWorkItem(
                  new WaitCallback(x =>
                  {
                      FullCheckDownLeft(rowSteps_Temp_Threads);
                      // Safely decrement the counter
                      if (Interlocked.Decrement(ref toProcess) == 0)
                          resetEvent.Set();
                Time is 00:00:00.0009000
Time is 00:00:00.0001000
                 * 
                 * 
                 * 
                  }), rowSteps_Temp_Threads);*/
                if (My_Real_Position.Row == i) //Circular Break;
                {
                    break;
                }

            }
            //resetEvent.WaitOne();
        }
        public void FullCheckDownLeft(Object o)
        {
            List<Point> Pos = o as List<Point>;
            List<Point> Follow = new List<Point>();           // Keep trac the Car Motion
            List<Point> Temporary_Follow = new List<Point>();

            for (int i = 0; i < Pos.Count; i++)
            {
                Follow.Add(Pos[i]);
                Temporary_Follow.Add(Pos[i]);
            }

            int NewPos = Pos[Pos.Count - 1].Row;         //Pos.get(Pos.size()-1).Row;
            for (int i = My_Real_Position.Column + 1; i < Max_Columns * 2; i++)
            {

                if (i == Max_Columns) //Circular Handling
                {
                    i = 0;
                }
                if (My_Real_Position.Column == i) //Circular Break;
                {
                    break;
                }
                if (x[NewPos, i] == -3)
                    break;
                Point Step = new Point(NewPos, i);  // One Step
                Temporary_Follow.Add(Step);                   // Save the Step in Temporary
                Follow.Add(Step);                             // Save the Step  
                if (x[NewPos, i] == 3) // l2aha w howa byt7rk bel gnb
                {
                    Result.Add(Follow);
                    break;
                }
                Fly F = new Fly(NewPos, i,Temporary_Follow,this.Result);
                //F.CheckDown("");
                new Task(F.CheckDown, "").Start();
                //ThreadPool.QueueUserWorkItem(new WaitCallback(F.CheckDown)," ");
                
            }



            //Thread.Sleep(5000);
        }


    }
}
