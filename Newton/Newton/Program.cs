using System;
using static System.Math;
using System.IO;
class Program
{
    const double dx = 1E-5;
    const double dy = 1E-5;
    public static double funone(double x, double y)
    {
        return x - x + y - 2;
    }
    public static double funtwo(double x, double y)

    {
        return x * 2 + y;
    }



    static void Main(string[] args)

    {
        StreamWriter file = new StreamWriter("out.txt");
        Console.WriteLine("Введите точность:");
        double e = Convert.ToDouble(Console.ReadLine());
        double[] x = new double[] { 3.5 * 10, 2.5 * 10 };
        double[] xn = new double[] { x[0], x[1] };
        int count = 0;
        do
        {
            if (count > 0)
            {
                xn[0] = x[0];
                xn[1] = x[1];
            }
            double[] f = new double[] { funone(x[0], x[1]), funtwo(x[0], x[1]) };
            double a = funone(x[0] + dx, x[1]) - funone(x[0], x[1]);
            a /= dx;
            double b = funone(x[0], x[1] + dy) - funone(x[0], x[1]);
            b /= dy;
            double c = funtwo(x[0] + dx, x[1]) - funtwo(x[0], x[1]);
            c /= dx;
            double d = funtwo(x[0], x[1] + dy) - funtwo(x[0], x[1]);
            d /= dy;
            double[,] df = new double[,] { { a, b }, { c, d } };
            double det_df = a * d - b * c;
            double det_1 = 1.0 / det_df;
            double[,] df_inv = new double[,] { { d, -b }, { -c, a } };
            x[0] = xn[0] - det_1 * (df_inv[0, 0] * f[0] + df_inv[0, 1] * f[1]);
            x[1] = xn[1] - det_1 * (df_inv[1, 0] * f[0] + df_inv[1, 1] * f[1]);
            count++;
            file.WriteLine("{0}\t {1}\t {2}", count, x[0], x[1]);
        }
        while (Abs(x[0] - xn[0]) > e || Abs(x[1] - xn[1]) > e);
        file.WriteLine("итераций: {0}", count);
        file.WriteLine("\nрезультат");
        file.WriteLine("x={0}", x[0]);
        file.WriteLine("y={0}", x[1]);
        file.Close();
    }
}