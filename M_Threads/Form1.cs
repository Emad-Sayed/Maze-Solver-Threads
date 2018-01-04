using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace M_Gui
{
    public partial class Form1 : Form
    {
        FileManager F = null;
        int Global_index = -1;
        public int NumberOfSolutions;
        public int SolutionNumber;
        List<List<Point>> FinalResult;
        List<List<Point>> AfterFinalResult;
        public List<Label> Labels_List;
        public int MaxRows;
        public int MaxColums;
        public int[,] m;
        public Form1(int Rows,int Colums,int[,]m,int x_Pos,int y_Pos, int x_Goal,int y_Goal)
        {
            Seq S = new Seq(m,Rows,Colums, x_Pos, y_Pos, x_Goal, y_Goal);
            SolutionNumber = 0;
            this.Labels_List=new List<Label>();
            this.AfterFinalResult = new List<List<Point>>();
            this.MaxRows = Rows;
            this.MaxColums = Colums;
            this.m = m;
            InitializeComponent();
            this.FinalResult = S.Result;
                for (int j = 0; j < FinalResult.Count;j++ )
                {
                    if (FinalResult[j].Count != 0)
                    {
                        NumberOfSolutions++;
                        AfterFinalResult.Add(FinalResult[j]);
                    }
                }

                this.Solution_Number.Text = "" + SolutionNumber;
            this.All_Solutions_Number.Text = ""+NumberOfSolutions;
        }
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == M_Gui.Program.NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }
        private void ShowMe()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            //TopMost = top;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Drow();
        }
        public void Drow()
        {
                        Label l = null;

            for (int i = 0; i < MaxRows; i++)
            {
                for (int j = 0; j < MaxColums; j++)
                {
                    l = addLabel(i,j);
                    Labels_List.Add(l);

                    flowLayoutPanel1.Controls.Add(l);
                }
                flowLayoutPanel1.SetFlowBreak(l, true);
            }
        }
        public Label addLabel(int i,int j)
        {
            Label l = new Label();
            l.Name =  i+","+j;
            l.Text =  "";
            l.ForeColor = Color.White;
            if(this.m[i,j]==3)
            {
                l.Text = "GOAL";
                l.Font = new Font(l.Font, FontStyle.Bold);
                l.ForeColor =Color.Black;
                l.BackColor = Color.Green;
            }
            else if (this.m[i, j] == -3)
            {
                l.BackColor = Color.Red;
            }
            else if (this.m[i, j] == 5)
            {
                l.Text = "CAR";
                l.Font = new Font(l.Font, FontStyle.Bold);
                l.ForeColor =Color.Black;
                l.BackColor = Color.Yellow;
            }
            else
            {
                l.BackColor = Color.Black;
            }
            l.Width = 60;
            l.Height = 30;
            l.TextAlign = ContentAlignment.MiddleCenter;
            l.Margin = new Padding(4);
            return l;
        }
        public void ResetColor()
        {
            for (int i = 0; i < this.Labels_List.Count; i++)
            {
                if (Labels_List.ElementAt(i).BackColor == Color.Gray )
                {
                    Labels_List.ElementAt(i).BackColor = Color.Black;
                    Labels_List.ElementAt(i).Text = "";
                }
                if (Labels_List.ElementAt(i).Font.Bold && Labels_List.ElementAt(i).Text!="GOAL")
                {
                    Labels_List.ElementAt(i).BackColor = Color.Yellow;
                    Labels_List.ElementAt(i).Text = "CAR";
                }
            }
        }

        private void Best_Click(object sender, EventArgs e)
        {
            this.ResetColor();
            this.Global_index = -1;
            this.Solution_Number.Text = "" + 0;
            List<Point> St=new List<Point>();
            int min = 100;

                for (int j = 0; j < FinalResult.Count; j++)
                {
                    if (FinalResult[j].Count < min)
                    {
                        min = FinalResult[j].Count;
                         St = FinalResult[j];
                    }
                }
            
            for(int a=0;a<St.Count-1;a++)
            {
                for(int b=0;b<Labels_List.Count;b++)
                {
                    if(Labels_List[b].Name==St[a].Row+","+St[a].Column)
                    {
                        if (Labels_List[b].Text == "" || Labels_List[b].Text == "CAR")
                        {
                            Labels_List[b].Text = "Step " + a;
                        }
                        else
                        {
                            Labels_List[b].Text = Labels_List[b].Text + "," + a;
                        }
                        
                        Labels_List[b].BackColor = Color.Gray;
                    }
                }
            }
        }

        private void Previous_Click(object sender, EventArgs e)
        {
            this.ResetColor();
            Global_index--;
            if (Global_index == -1 || Global_index==-2)
                Global_index = NumberOfSolutions - 1;

            int Print_Global = Global_index + 1;
            this.Solution_Number.Text = "" + Print_Global;
            List<Point> St = AfterFinalResult[Global_index];
            for (int a = 0; a < St.Count - 1; a++)
            {
                for (int b = 0; b < Labels_List.Count; b++)
                {
                    if (Labels_List[b].Name == St[a].Row + "," + St[a].Column)
                    {
                        if (Labels_List[b].Text == "" || Labels_List[b].Text=="CAR")
                        {
                            Labels_List[b].Text = "Step " + a;
                        }
                        else
                        {
                            Labels_List[b].Text = Labels_List[b].Text + "," + a;
                        }
                        Labels_List[b].BackColor = Color.Gray;
                    }
                }
            }


        }

        private void Next_Click(object sender, EventArgs e)
        {
            this.ResetColor();
                Global_index++;
                if (Global_index == NumberOfSolutions)
                    Global_index = 0;
                int Print_Global = Global_index + 1;
                this.Solution_Number.Text = "" + Print_Global; this.ResetColor();

            List<Point> St = AfterFinalResult[Global_index];
            for (int a = 0; a < St.Count - 1; a++)
            {
                for (int b = 0; b < Labels_List.Count; b++)
                {
                    if (Labels_List[b].Name == St[a].Row + "," + St[a].Column)
                    {
                        if (Labels_List[b].Text == "" || Labels_List[b].Text == "CAR")
                        {
                            Labels_List[b].Text = "Step " + a;
                        }
                        else
                        {
                            Labels_List[b].Text = Labels_List[b].Text +","+ a;
                        }
                        Labels_List[b].BackColor = Color.Gray;
                    }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.F = new FileManager();
            F.ReadText2();

            MessageBox.Show("New Route Loaded Successfully");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(F!=null)
            {
                /*this.Visible = false;
                Console.WriteLine(F.Row_Number_2 + " " + F.Column_Number_2 + " " + F.Goal.Row + " " + F.Goal.Column);
                Form1 newFrame = new Form1(F.Row_Number_2, F.Column_Number_2, F.Matrix_2, F.Car.Row, F.Car.Column, F.Goal.Row, F.Goal.Column);
                newFrame.Show();*/
                Seq S = new Seq(F.Matrix_2,F.Row_Number_2,F.Column_Number_2,F.Car.Row, F.Car.Column, F.Goal.Row, F.Goal.Column);
            this.SolutionNumber = 0;
            this.NumberOfSolutions = 0;
            this.Global_index = -1;
            this.Labels_List=new List<Label>();
            this.AfterFinalResult = new List<List<Point>>();
            this.MaxRows = F.Row_Number_2;
            this.MaxColums = F.Column_Number_2;
            this.m = F.Matrix_2;
            this.flowLayoutPanel1.Controls.Clear();
            this.Drow();
            this.FinalResult = S.Result;
                for (int j = 0; j < FinalResult.Count;j++ )
                {
                    if (FinalResult[j].Count != 0)
                    {
                        NumberOfSolutions++;
                        AfterFinalResult.Add(FinalResult[j]);
                    }
                }

                this.Solution_Number.Text = "" + SolutionNumber;
            this.All_Solutions_Number.Text = ""+NumberOfSolutions;
        }
            
            else
                MessageBox.Show("Load The Route First");

        //newFrame.Show();
        }





    }
}
