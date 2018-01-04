using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Gui
{
    class FileManager
    {
        public int[,] Matrix_2;
        public int Row_Number_2;
        public int Column_Number_2;
        public Point Car = null;
        public Point Goal = null;

        public void ReadText1()
        {
            var path = "D:\\L_Threads\\File.txt";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            string result;
            result = file.ReadLine();
            int Row_Number = Int32.Parse(result);
            result = file.ReadLine();
            int Column_Number = Int32.Parse(result);
            Row_Number = Row_Number + 2;
            Column_Number = Column_Number + 2;
            int[,] Matrix = new int[Row_Number, Column_Number];

            int Row = 0;
            int Column = -1;
            while ((result = file.ReadLine()) != null)
            {
                Row++;
                Column = 0;

                for (int i = 0; i < result.Count(); i++)
                {

                    if (result[i] != ' ')
                    {
                        Column++;
                        if (result[i] == '.')
                            Matrix[Row, Column] = 0;
                        else if (result[i] == 'c')
                        {
                            Matrix[Row, Column] = 5;
                            this.Car = new Point(Row, Column);
                        }
                        else if (result[i] == '*')
                            Matrix[Row, Column] = -3;
                        else if (result[i] == 'e')
                        {
                            Matrix[Row, Column] = 3;
                            this.Goal = new Point(Row, Column);
                        }
                    }

                }
            }
            this.Matrix_2 = Matrix;
            this.Row_Number_2 = Row_Number;
            this.Column_Number_2 = Column_Number;
            this.DrawLimits();
            //Console.WriteLine("aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa");
            file.Close();
        }


        public async void  ReadText2()
        {
            var file = "D:\\L_Threads\\File2.txt";
            String Result = "";
            try
            {
                using (StreamReader reader = File.OpenText(file))
                {
                    Result = await reader.ReadLineAsync();
                    int Row_Number = Int32.Parse(Result);
                    Result = await reader.ReadLineAsync();
                    int Column_Number = Int32.Parse(Result);
                    Row_Number = Row_Number + 2;
                    Column_Number = Column_Number + 2;
                    int[,] Matrix = new int[Row_Number, Column_Number];
                    int Row = 0;
                    int Column = -1;
                    while ((Result = await reader.ReadLineAsync()) != null)
                    {
                        Row++;
                        Column = 0;

                        for (int i = 0; i < Result.Count(); i++)
                        {

                            if (Result[i] != ' ')
                            {
                                Column++;
                                if (Result[i] == '.')
                                    Matrix[Row, Column] = 0;
                                else if (Result[i] == 'c')
                                {
                                    Matrix[Row, Column] = 5;
                                    this.Car = new Point(Row, Column);
                                }
                                else if (Result[i] == '*')
                                    Matrix[Row, Column] = -3;
                                else if (Result[i] == 'e')
                                {
                                    Matrix[Row, Column] = 3;
                                    this.Goal = new Point(Row, Column);
                                }
                            }

                        }
                    }
                    this.Matrix_2 = Matrix;
                    this.Row_Number_2 = Row_Number;
                    this.Column_Number_2 = Column_Number;
                    this.DrawLimits();

                    /* for (int i = 0; i < Row_Number;i++ )
                     {
                         Console.WriteLine();
                         for(int q=0;q<Column_Number;q++)
                         {
                             Console.Write(" "+Matrix[i,q]);
                         }
                     }*/
                    //  Console.WriteLine("aaaaaaaaa " + Matrix.GetLength(0));
                    //Console.WriteLine("bbbbbbbb "+Matrix.GetLength(1));
                }
            }
            catch (Exception E)
            {
                Console.WriteLine(E);
            }
        }
        public void DrawLimits()
        {
            for (int i = 0; i < Column_Number_2; i++)
            {
                this.Matrix_2[0, i] = -3;
            }
            int LastRow = Row_Number_2 - 1;
            for (int i = 0; i < Column_Number_2; i++)
            {
                Console.WriteLine(LastRow + " " + i);
                this.Matrix_2[LastRow, i] = -3;
            }


            for (int i = 0; i < Row_Number_2; i++)
            {
                this.Matrix_2[i, 0] = -3;
            }
            int LastCol = Column_Number_2 - 1;
            for (int i = 0; i < Row_Number_2; i++)
            {
                this.Matrix_2[i, LastCol] = -3;
            }
        }

    }
}
