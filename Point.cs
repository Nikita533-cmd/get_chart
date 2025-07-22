using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace WindowsFormsApp
{
    public class Point

    {
        public List<int> X {get; set; }
        public List<int> Y { get; set; }
        public List<double> X1 { get; set; }
        public List<double> Y1 { get; set; }
        public Bitmap Bitmap { get; set; }
        public int W { get; set; }
        public int H { get; set; }

        public Point()
        {
            // Path to your PNG file
            string filePath = "D:\\IMG\\EN80-250.png";
            bool t = true;
            Dictionary<int, int> L = new Dictionary<int, int>();
            List<int> X = new List<int>();
            List<int> Y = new List<int>();
            List<double> X1 = new List<double>();
            List<double> Y1 = new List<double>();
            // Load the image
            int HI = 0;
            int HI_ = 10;
            // using (Bitmap bitmap = new Bitmap(filePath))
            //   {
                Bitmap bitmap = new Bitmap(filePath);
   
                this.Bitmap = bitmap;
                // Loop through the pixels
            
                for (int y = 0; y < bitmap.Height && t; y++)
                {
                    Color pixelColor = bitmap.GetPixel(bitmap.Width/2, y);
                    if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160) { HI++; }
                    if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160 && HI == 4) 
                    { HI_ = y - HI; break; }
                    else if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160 && HI == 3) 
                    { HI_ = y; HI++; } 
                   // else if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160) { HI++; }
                }

                for (int y = 0; y < bitmap.Height && t; y++)
                {
                    for (int x = 0; x < bitmap.Width; x = x + 20)
                    {
                        // Get the pixel color
                        Color pixelColor = bitmap.GetPixel(x, y);

                        // Output the pixel color (for example)
                        Console.WriteLine($"Pixel at ({x},{y}): R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B}");
                        if (pixelColor.R == 7 && pixelColor.G == 90 && pixelColor.B == 173) { X.Add(x); Y.Add(y); break; }
                    }
                }
        //    }

            for (int i = 0; i < X.Count; i++)
            {
                //   
               // Y[i] = Y[i] / HI_ * 4;
                Console.WriteLine($"X: {X[i]} Y: {Y[i]} X[i] = {Convert.ToDouble(X[i]-48.57) / Convert.ToDouble(48.57)*20} Y[i] = {100-Convert.ToDouble(Y[i])/ Convert.ToDouble(4)} HI_ = {HI_}");
                X1.Add(Convert.ToDouble(X[i] - 48.57) / Convert.ToDouble(48.57) * 20);
                Y1.Add(100 - Convert.ToDouble(Y[i]) / Convert.ToDouble(4));
            }
            this.X1 = X1;
            this.Y1 = Y1;
            this.X = X;
            this.Y = Y;
          //  Console.ReadLine();

        }



        public Point(string filePath, int kol_X, int kol_Y,double SH_X,double SH_Y )
        {
            // Path to your PNG file
          //  string filePath = "D:\\IMG\\EN80-250.png";
            bool t = true;
            Dictionary<int, int> L = new Dictionary<int, int>();
            List<int> X = new List<int>();
            List<int> Y = new List<int>();
            List<double> X1 = new List<double>();
            List<double> Y1 = new List<double>();
            // Load the image
            int HI = 0;
            int HI_ = 10;
            double WIDE = 0;
            double HIDE = 0;
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                this.Bitmap = bitmap;
                // Loop through the pixels
                WIDE = bitmap.Width;
                HIDE = bitmap.Height;
                W = (int) WIDE;
                H = (int) HIDE;
                for (int y = 0; y < bitmap.Height && t; y++)
                {
                    Color pixelColor = bitmap.GetPixel(bitmap.Width / 2, y);
                    if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160) { HI++; }
                    if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160 && HI == 4)
                    { HI_ = y - HI; break; }
                    else if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160 && HI == 3)
                    { HI_ = y; HI++; }
                    // else if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160) { HI++; }
                }

                for (int y = 0; y < bitmap.Height && t; y++)
                {
                    for (int x = 0; x < bitmap.Width; x = x + 20)
                    {
                        // Get the pixel color
                        Color pixelColor = bitmap.GetPixel(x, y);

                        // Output the pixel color (for example)
                        Console.WriteLine($"Pixel at ({x},{y}): R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B}");
                        if (pixelColor.R == 7 && pixelColor.G == 90 && pixelColor.B == 173) { X.Add(x); Y.Add(y); break; }
                    }
                }
            }
            double One_pix_X = WIDE / (kol_X + 1);
            double One_pix_Y = HIDE / (kol_Y + 1);
            for (int i = 0; i < X.Count; i++)
            {
                //   
                // Y[i] = Y[i] / HI_ * 4;
                Console.WriteLine($"X: {X[i]} Y: {Y[i]} X[i] = {Convert.ToDouble(X[i] - 48.57) / Convert.ToDouble(48.57) * 20} Y[i] = {100 - Convert.ToDouble(Y[i]) / Convert.ToDouble(4)} HI_ = {HI_}");
                // X1.Add(Convert.ToDouble(X[i] - 48.57) / Convert.ToDouble(48.57) * 20);
                X1.Add(Convert.ToDouble(X[i] - One_pix_X) / Convert.ToDouble(One_pix_X) * SH_X);
                Y1.Add(100 - Convert.ToDouble(Y[i]) / Convert.ToDouble(4));
            }
            this.X1 = X1;
            this.Y1 = Y1;
            this.X = X;
            this.Y = Y;
            //  Console.ReadLine();

        }
        //***
        public Point(string filePath, int kol_X, int kol_Y, double SH_X, double SH_Y,int XO,int YO, int Xmax, int Ymax, Color C)
        {
            // Path to your PNG file
            //  string filePath = "D:\\IMG\\EN80-250.png";
            bool t = true;
            Dictionary<int, int> L = new Dictionary<int, int>();
            List<int> X = new List<int>();
            List<int> Y = new List<int>();
            List<double> X1 = new List<double>();
            List<double> Y1 = new List<double>();
            // Load the image
            int HI = 0;
            int HI_ = 10;
            double WIDE = 0;
            double HIDE = 0;
            int Y_up = 0;
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                this.Bitmap = bitmap;
                // Loop through the pixels
                WIDE = bitmap.Width;
                HIDE = bitmap.Height;
                W = (int)WIDE;
                H = (int)HIDE;
                for (int y = 0; y < bitmap.Height && t; y++)
                {
                    Color pixelColor = bitmap.GetPixel(bitmap.Width / 2, y);
                    if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160) { HI++; }
                    if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160 && HI == 4)
                    { HI_ = y - HI; break; }
                    else if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160 && HI == 3)
                    { HI_ = y; HI++; }
                    // else if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160) { HI++; }
                }

                for (int y = 0; y < bitmap.Height && t; y++)
                {
                    for (int x = 0; x < bitmap.Width; x = x + 20)
                    {
                        // Get the pixel color
                        Color pixelColor = bitmap.GetPixel(x, y);

                        // Output the pixel color (for example)
                        Console.WriteLine($"Pixel at ({x},{y}): R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B}");
                        if (pixelColor==C) { X.Add(x); Y.Add(y); break; }
                    }
                }
            }
            double One_pix_X = Math.Abs(Xmax-XO) / (kol_X);
            double One_pix_Y =Math.Abs( Ymax-YO)/ (kol_Y);
            //  Y_up = (int)HIDE-Ymax;
            Y_up = Ymax;
            for (int i = 0; i < X.Count; i++)
            {

                // Console.WriteLine($"X: {X[i]} Y: {Y[i]} X[i] = {Convert.ToDouble(X[i] - 48.57) / Convert.ToDouble(48.57) * 20} Y[i] = {100 - Convert.ToDouble(Y[i]) / Convert.ToDouble(4)} HI_ = {HI_}");
               Console.WriteLine($"X: {X[i]} Y: {Y[i]} X[i] = {Convert.ToDouble(X[i] - XO) / Convert.ToDouble(One_pix_X) * SH_X} Y[i] = {(SH_Y * kol_Y) - Convert.ToDouble(Y[i]-Y_up) / Convert.ToDouble(One_pix_X) * Convert.ToDouble(SH_Y)} HI_ = {HI_}");
                X1.Add(Convert.ToDouble(X[i] - XO) / Convert.ToDouble(One_pix_X) * SH_X);
                Y1.Add((SH_Y*kol_Y) - Convert.ToDouble(Y[i]-Y_up) / Convert.ToDouble(One_pix_Y)*Convert.ToDouble(SH_Y));
            }
            this.X1 = X1;
            this.Y1 = Y1;
            this.X = X;
            this.Y = Y;
      

        }



        public Point(string filePath, int XO, int YO, int Xmax, int Ymax, List<int> X,List<int> Y,double X_MAX_real, double Y_MAX_real, double X_min_real, double Y_min_real)
        {
            bool t = true;
            Dictionary<int, int> L = new Dictionary<int, int>();
            List<double> X1 = new List<double>();
            List<double> Y1 = new List<double>();
            // Load the image
            int HI = 0;
            int HI_ = 10;
            double WIDE = 0;
            double HIDE = 0;
            int Y_up = 0;

            int X_pix = Xmax - XO;
            int Y_pix = YO - Ymax;
            double One_pix_X = (X_MAX_real-X_min_real) / X_pix;
            double One_pix_Y = (Y_MAX_real-Y_min_real) / Y_pix;
            double X1_i = 0;
            double Y1_i = 0;
            for (int i = 0; i < X.Count; i++)
            {
                X1_i = X_min_real+Convert.ToDouble(X[i] - XO) * One_pix_X;
                Y1_i = Y_min_real+(Y_pix - (Y[i] - Ymax)) * One_pix_Y;
                X1.Add(X1_i);
                Y1.Add(Y1_i);
                Console.WriteLine($"Xi = {X1_i} Yi = {Y1_i}");
            }
            this.X1 = X1;
            this.Y1 = Y1;
            this.X = X;
            this.Y = Y;


        }

        public Point(string filePath,int XO, int YO, int Xmax, int Ymax, Color C, double X_MAX_real,double Y_MAX_real,double X_min_real, double Y_min_real)
        {

            bool t = true;
            Dictionary<int, int> L = new Dictionary<int, int>();
            List<int> X = new List<int>();
            List<int> Y = new List<int>();
            List<double> X1 = new List<double>();
            List<double> Y1 = new List<double>();
            int HI = 0;
            int HI_ = 10;
            double WIDE = 0;
            double HIDE = 0;
            int Y_up = 0;
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                this.Bitmap = bitmap;
                // Loop through the pixels
                WIDE = bitmap.Width;
                HIDE = bitmap.Height;
                W = (int)WIDE;
                H = (int)HIDE;
                for (int y = 0; y < bitmap.Height && t; y++)
                {
                    Color pixelColor = bitmap.GetPixel(bitmap.Width / 2, y);
                    if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160) { HI++; }
                    if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160 && HI == 4)
                    { HI_ = y - HI; break; }
                    else if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160 && HI == 3)
                    { HI_ = y; HI++; }
                    // else if (pixelColor.R == 160 && pixelColor.G == 160 && pixelColor.B == 160) { HI++; }
                }
                int Y_H = YO-Ymax;
                for (int x = XO; x < Xmax; x = x + 1)
                {
                    for (int y = YO; y >0 && t; y--)
                    {

                        // Get the pixel color
                        Color pixelColor = bitmap.GetPixel(x, y);
                        Color PC = bitmap.GetPixel(x, y);
                        // Output the pixel color (for example)
                        Console.WriteLine($"Pixel at ({x},{y}): R={pixelColor.R}, G={pixelColor.G}, B={pixelColor.B}");
                      //  
                        if (PC.R>C.R * 0.75 && PC.R < C.R * 1.25 && PC.G > C.G * 0.75 && PC.G < C.G * 1.25 && PC.B> C.B*0.75&& PC.B < C.B * 1.25 ) 
                        {
                            if (Y.Count != 0 && (Y.Last()-20 > y|| Y.Last() + 50 < y)) { break; }
                            X.Add(x); Y.Add(y); break;
                        }
                    }
                }
            }
            int X_pix = Xmax-XO;
            int Y_pix = YO-Ymax;
            double One_pix_X = (X_MAX_real-X_min_real)/X_pix;
            double One_pix_Y = (Y_MAX_real-Y_min_real)/Y_pix;

            double X1_i = 0;
            double Y1_i = 0;

            for (int i = 0; i < X.Count; i++)
            {
                 X1_i = X_min_real+Convert.ToDouble(X[i] - XO) * One_pix_X;
                Y1_i = Y_min_real+(Y_pix - (Y[i] - Ymax))*One_pix_Y;
                X1.Add(X1_i);
                Y1.Add(Y1_i);
                Console.WriteLine($"Xi = {X1_i} Yi = {Y1_i}   X = {X[i]} Y = {Y[i]}");
                
            }
            this.X1 = X1;
            this.Y1 = Y1;
            this.X = X;
            this.Y = Y;


        }




    }
}