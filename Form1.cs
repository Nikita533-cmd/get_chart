using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public string path_ = "";
        public int I = 0;
        public int X0 = 0;
        public int Y0 = 0;
        public int Xmax = 0;
        public int Ymax = 0;
        public double Ymin = 0;
        public double Xmin = 0;
        public Color C = Color.White;
        public double SHAG = 0;
        public List<int> X_K = new List<int>();
        public List<int> Y_K = new List<int>();
        public string[] STR_ = null;
        private float zoomFactor = 1.0f;
        public int COL = 20;
        public double[] Q { get; private set; }
        public double[] P_Q { get; private set; }
        public double[] Q_ { get; private set; }
        public double[] P_ { get; private set; }
        public double [] CF_ {  get; set; }
        public Form1()
        {
            InitializeComponent();
            button4.Visible = false;
            pictureBox1.MouseClick += pictureBox3_MouseClick;
            pictureBox1.MouseClick += pictureBox1_MouseClick;
            pictureBox1.MouseClick += pictureBox2_MouseClick;
            label13.Text = "Точки не выбранны";
            R.Visible = false;
            G.Visible = false;
            B.Visible = false;
            A.Visible = false;
            button1.Visible = false;
            textBox8.Text = "20";


        }
        ///*





        //**
        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            // Check if the Shift key is pressed
            if (ModifierKeys == Keys.Shift)
            {
                // Ensure the image is loaded
                if (pictureBox1.Image != null)
                {
                    // Get the pixel coordinates at the clicked position
                    Bitmap bitmap = new Bitmap(pictureBox1.Image);

                    // Check if the click is within bounds
                    if (e.X >= 0 && e.X < bitmap.Width && e.Y >= 0 && e.Y < bitmap.Height)
                    {
                        // Display the coordinates in a label or console
                        using (Graphics g = Graphics.FromImage(bitmap))
                        {
                            g.FillEllipse(Brushes.Red, e.X-3, e.Y-3, 6, 6);
                           

                            pictureBox1.Image = bitmap;
                            pictureBox1.Update();
                        }
                        X_K.Add(e.X);
                        Y_K.Add(e.Y);
                         button4.Visible = true;
                         label13.Text = $"количество точек: ({X_K.Count})";

                    }
                }
            }
        }
        private void pictureBox3_MouseClick(object sender, MouseEventArgs e)
        {
            // Check if the Shift key is pressed
            if (ModifierKeys == Keys.Control)
            {
                // Ensure the image is loaded
                if (pictureBox1.Image != null)
                {
                    // Get the pixel coordinates at the clicked position
                    Bitmap bitmap = new Bitmap(pictureBox1.Image);

                    // Check if the click is within bounds
                    if (e.X >= 0 && e.X < bitmap.Width && e.Y >= 0 && e.Y < bitmap.Height)
                    {
                        Color pixelColor = bitmap.GetPixel(e.X, e.Y);
                        label8.Text = $"Color at ({e.X}, {e.Y}): R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B} A={pixelColor.A}"; label8.ForeColor = Color.FromArgb(pixelColor.A, pixelColor.R, pixelColor.G, pixelColor.B); C = pixelColor;

                    }
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (ModifierKeys != Keys.Shift && ModifierKeys != Keys.Control)
            {
                // Ensure the image is loaded
                if (pictureBox1.Image != null)
                {
                    // Get the pixel color at the clicked position
                    Bitmap bitmap = new Bitmap(pictureBox1.Image);

                    // Check if the click is within bounds
                    if (e.X >= 0 && e.X < bitmap.Width && e.Y >= 0 && e.Y < bitmap.Height)
                    {
                        Color pixelColor = bitmap.GetPixel(e.X, e.Y);

                        // Display the color information in a label

                        if (I == 0) { label3.Text = $"Color at ({e.X}, {e.Y}): R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B}";  X0 = e.X; Y0 = e.Y; }
                        if (I == 1) { label4.Text = $"Color at ({e.X}, {e.Y}): R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B}";  Xmax = e.X; }
                        if (I == 2) { label7.Text = $"Color at ({e.X}, {e.Y}): R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B}"; Ymax = e.Y; }
                       // if (I == 3) { label8.Text = $"Color at ({e.X}, {e.Y}): R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B}"; label8.ForeColor = Color.FromArgb(pixelColor.A, pixelColor.R, pixelColor.G, pixelColor.B); C = pixelColor; }

                        I++;
                     //   if (I > 3) { I = 0; }
                        if (I > 2) { I = 0; }
                    }
                }
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
           
        }




        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файл изображения|*.png;*.jpeg;*.bmp";
            OPF.Multiselect = true;
            if (OPF.ShowDialog() == DialogResult.OK)
            {
                path_ = OPF.FileName;
       
                Bitmap B = new Bitmap(path_);
                Rectangle t = new Rectangle(0, 0, B.Width, B.Height);
                pictureBox1.Image = B;
                pictureBox1.Update();

            }
            else { path_ = "Нет"; }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            X_K.Clear();
            Y_K.Clear();
            label13.Text = "Точки не выбранны";
            chart1.Series[0].Points.Clear();
            chart1.Series[1].Points.Clear();
            textBox5.Clear();
            chart1.Series[2].Points.Clear();
            Bitmap B = new Bitmap(path_);
            pictureBox1.Image = B;
            pictureBox1.Update();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            double X1 = Doub(textBox6.Text);
            double Y1 = Doub(textBox7.Text);


            WindowsFormsApp.Point p = new WindowsFormsApp.Point(path_, X0, Y0, Xmax, Ymax, C, X1, Y1,Xmin,Ymin);
            chart1.Series[0].Points.Clear();
            chart1.Series[2].Points.Clear();
            chart1.ChartAreas[0].AxisX.Maximum = X1;
            chart1.ChartAreas[0].AxisY.Maximum = Y1 * 1.1;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Interval = Y1 / 10;
            chart1.ChartAreas[0].AxisX.Interval = X1 / 10;
            string[] STR = new string[p.X1.Count + 2];

            STR[1] = "Кривая построенная по цвету";
            STR[0] = "X_Y";

            Q =new double[p.X1.Count];
            P_Q = new double[p.X1.Count];
            for (int i = 0; i < p.X1.Count; i++)
            {
                chart1.Series[0].Points.AddXY(p.X1[i], p.Y1[i]);
                STR[i + 2] = $"{Math.Round(p.X1[i], 2)}_{Math.Round(p.Y1[i], 2)}";
                Q[i] = p.X1[i];
                P_Q[i] = p.Y1[i];
            }

            string[] STR_o = new string[p.X1.Count + 2];
            STR_o[0] = "Обработанная кривая";
            STR_o[1] = "X_Y";

            H_nasos(COL, Xmin, X1);
 //               for (int i = 0; i < p.X1.Count; i++)
           for (int i = 0; i < COL; i++)
            {
                chart1.Series[2].Points.AddXY(Q_[i], P_[i]);
                STR_o[i + 2] = $"{Math.Round(Q_[i], 2)}_{Math.Round(P_[i], 2)}";

            }
          

            chart1.Update();
            textBox5.Lines = STR;
            textBox4.Lines = STR_o;
        }
        public void H_nasos(int col, double Xmin,double Xmax)
        {
            double[] Q_ = new double[col];
            double[] P_ = new double[col];
            double[] CF = PolynomialRegression(Q, P_Q, 5);
            double a1 = CF[0];
            double a2 = CF[1];
            double a3 = CF[2];
            double a4 = CF[3];
            double a5 = CF[4];
            double a6 = CF[5];
            double SHag = (Xmax- Xmin)/col;
            double Q2 = Xmin;
            for (int i = 0; i < col; i++)
            {
                Q_[i] = Q2;
                P_[i]= (a6 * Math.Pow(Q2, 5) + a5 * Math.Pow(Q2, 4) + a4 * Math.Pow(Q2, 3) + a3 * Math.Pow(Q2, 2) + a2 * Q2 + a1);
                Q2=Q2+SHag;
            }
            this.Q_ = Q_;
            this.P_ = P_;
            CF_ = CF;
            textBox9.Text = $"{Math.Round(CF_[5],4)}*X^5+{Math.Round(CF_[4],4)}*X^4+{Math.Round(CF_[3],4)}*X^3+{Math.Round(CF_[2],4)}*X^2+{Math.Round(CF_[1],4)}*X+{Math.Round(CF_[0],4)}";

        }
        static double[] PolynomialRegression(double[] x, double[] y, int degree)
        {
            // Вычисляем матрицу X
            double[,] X = new double[x.Length, degree + 1];
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j <= degree; j++)
                {
                    X[i, j] = Math.Pow(x[i], j);
                }
            }

            // Вычисляем матрицу Y
            double[,] Y = new double[y.Length, 1];
            for (int i = 0; i < y.Length; i++)
            {
                Y[i, 0] = y[i];
            }

            // Вычисляем матрицу B
            double[,] XT = Transpose(X);
            double[,] XTX = Multiply(XT, X);
            double[,] XTY = Multiply(XT, Y);
            double[,] B = Multiply(Invert(XTX), XTY);

            // Преобразуем матрицу B в одномерный массив
            double[] coefficients = new double[degree + 1];
            for (int i = 0; i <= degree; i++)
            {
                coefficients[i] = B[i, 0];
            }

            return coefficients;
        }
        static double[,] Transpose(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int columns = matrix.GetLength(1);
            double[,] result = new double[columns, rows];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }
            return result;
        }

        static double[,] Multiply(double[,] matrix1, double[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int columns1 = matrix1.GetLength(1);
            int rows2 = matrix2.GetLength(0);
            int columns2 = matrix2.GetLength(1);
            double[,] result = new double[rows1, columns2];
            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < columns2; j++)
                {
                    for (int k = 0; k < columns1; k++)
                    {
                        result[i, j] += matrix1[i, k] * matrix2[k, j];
                    }
                }
            }
            return result;
        }

        static double[,] Invert(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            double[,] result = new double[n, n];
            double[,] temp = new double[n, 2 * n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    temp[i, j] = matrix[i, j];
                }
                temp[i, n + i] = 1;
            }
            for (int i = 0; i < n; i++)
            {
                double pivot = temp[i, i];
                for (int j = i; j < 2 * n; j++)
                {
                    temp[i, j] /= pivot;
                }
                for (int j = 0; j < n; j++)
                {
                    if (j != i)
                    {
                        double factor = temp[j, i];
                        for (int k = i; k < 2 * n; k++)
                        {
                            temp[j, k] -= factor * temp[i, k];
                        }
                    }
                }
            }
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result[i, j] = temp[i, n + j];
                }
            }
            return result;
        }
        public double Doub(string t)
        {
            double Q = 100;
            if (t != "")
            {
                if (t.Contains("."))
                { Q = Convert.ToDouble(t.Replace('.', ',')); }
                else
                { Q = Convert.ToDouble(t); }
            }
            return Q;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            double X1 = Doub(textBox6.Text);
            double Y1 = Doub(textBox7.Text);


            WindowsFormsApp.Point p = new WindowsFormsApp.Point(path_, X0, Y0, Xmax, Ymax, X_K, Y_K, X1, Y1, Xmin, Ymin);
            chart1.Series[1].Points.Clear();
            chart1.ChartAreas[0].AxisX.Maximum = X1;
            chart1.ChartAreas[0].AxisY.Maximum = Y1 * 1.1;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Minimum = 0;
            chart1.ChartAreas[0].AxisY.Interval = Y1 / 10;
            chart1.ChartAreas[0].AxisX.Interval = X1 / 10;
            string[] STR = new string[p.X1.Count + 2];
            STR[0] = "Кривая построенная по точкам";
            STR[1] = "X_Y";



            Q = new double[p.X1.Count];
            P_Q = new double[p.X1.Count];

            for (int i = 0; i < p.X1.Count; i++)
            {
                Q[i] = p.X1[i];
                P_Q[i] = p.Y1[i];
                chart1.Series[1].Points.AddXY(p.X1[i], p.Y1[i]);
                STR[i + 2] = $"{Math.Round(p.X1[i], 2)}_{Math.Round(p.Y1[i], 2)}";
            }
          //  chart1.Update();
            textBox2.Lines = STR;
            ///туц


            H_nasos(COL, Xmin, X1);




        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            // Change zoom factor based on mouse wheel movement
            if (e.Delta > 0) // Zoom in
            {
                zoomFactor += 0.1f;
            }
            else if (e.Delta < 0) // Zoom out
            {
                zoomFactor -= 0.1f;
                if (zoomFactor < 0.1f) zoomFactor = 0.1f; // Prevent negative zoom
            }

            // Redraw the image with the new zoom factor
            pictureBox1.Invalidate(); // Trigger repaint
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                // Calculate new dimensions based on zoom factor
                int newWidth = (int)(pictureBox1.Image.Width * zoomFactor);
                int newHeight = (int)(pictureBox1.Image.Height * zoomFactor);
         
                // Draw the image scaled by the zoom factor
                e.Graphics.DrawImage(pictureBox1.Image, new Rectangle(0, 0, newWidth, newHeight));
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "") { Ymin = Doub(textBox1.Text); }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int R = Convert.ToInt32(this.R.Text);
            int G = Convert.ToInt32(this.G.Text);
            int B = Convert.ToInt32(this.B.Text);
            int A = 255;
            if (this.A.Text != "") { A = Convert.ToInt32(this.A.Text); }
            Color color = Color.FromArgb(A,R, G, B);
            this.C = color; 
        }

        private void textBox3_TextChanged_1(object sender, EventArgs e)
        {
            if (textBox3.Text != "") { Xmin = Doub(textBox3.Text); }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text != "") { COL = Convert.ToInt32(textBox8.Text); }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {

                double Q2 = Doub(textBox10.Text);
                double[] CF = PolynomialRegression(Q, P_Q, 5);
                double a1 = CF[0];
                double a2 = CF[1];
                double a3 = CF[2];
                double a4 = CF[3];
                double a5 = CF[4];
                double a6 = CF[5];
                label6.Text=$"Y(X)={Math.Round((a6 * Math.Pow(Q2, 5) + a5 * Math.Pow(Q2, 4) + a4 * Math.Pow(Q2, 3) + a3 * Math.Pow(Q2, 2) + a2 * Q2 + a1),4)}";

        }
    }
}
