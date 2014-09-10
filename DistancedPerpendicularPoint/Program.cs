using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContourLibrary.Data;

namespace DistancedPerpendicularPoint
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var point1 = new DoubleVertice() {X = 100.0, Y = 100};
            var point2 = new DoubleVertice() {X = 600.0, Y = 600};
            Perpender perpender = new Perpender();
            var mediPerpend = perpender.GetMediPerpendicular(point1, point2);
            var distancedPerpendicularPoint = perpender.GetDistancedPerpendicularPoint(mediPerpend[0], mediPerpend[1], 300);
                ;
            Console.WriteLine(string.Format("X={0} ; Y= {1}", distancedPerpendicularPoint.X,
                                            distancedPerpendicularPoint.Y));
            Console.ReadLine();

        }

        internal class  Perpender
    {



        private DoubleVertice Get_qrtnDeltaVector(double x1, double y1, double x2, double y2, double distance)
        {
            var dy = y2 - y1;
            var dx = x2 - x1;
            var distance0 =Math.Sqrt( dx * dx + dy * dy);

            var qrtn = (distance  / distance0);
            return new DoubleVertice { X = dx * qrtn, Y = dy * qrtn };
        }

      

        private DoubleVertice Get_distancedPointByDeltaVector(DoubleVertice initialPoint, DoubleVertice deltaForDistance)
        {          
            return new DoubleVertice { X = initialPoint.X + deltaForDistance.X, Y = initialPoint.Y + deltaForDistance.Y };
        }


        internal DoubleVertice GetDistancedPerpendicularPoint(DoubleVertice startPoint, DoubleVertice endPoint, double distance)
        {

            var qrtn = Get_qrtnDeltaVector(startPoint.X, startPoint.Y, endPoint.X, endPoint.Y, distance);
            return Get_distancedPointByDeltaVector(startPoint, qrtn);

        }

        private double Get_k_Coefficient(double x1, double y1, double x2, double y2)
        {
            return (y2 - y1) / (x2 - x1);
        }

        private double GetPerpendicularCoefficient(double k)
        {
            if (k != 0) return -1 / k;
            else return 0;
        }

        private double Get_b_Coefficient(double k, double y, double x)
        {
            return y - k * x;
        }

        internal List<DoubleVertice> GetMediPerpendicular(DoubleVertice point0, DoubleVertice point1)
        {
            var result = new List<DoubleVertice>();
            double x, y, lx, ly, mx, my, path;
            var height = 5;
            var scale = Math.Sqrt(0.75);
            x = point1.X;
            y = point1.Y;
           
            lx = point0.X;
            ly = point0.Y;

            mx = (lx + x) / 2;
            my = (ly + y) / 2;
           // var k = Get_k_Coefficient(x, y, lx, ly);
           // var pk = GetPerpendicularCoefficient(k);
           // var b = Get_b_Coefficient(pk, my, mx);

            //path += ' ' + (mx) + ' ' + (my);
            result.Add(new DoubleVertice { X = mx, Y = my });
            //path += ' ' + (x) + ' ' + (y);
            //result.Add(new DoubleVertice { X = 0, Y = b });
            result.Add(new DoubleVertice { X = mx + scale * (y - ly), Y = my - scale * (x - lx) });
            return result;
        }

            internal void MakeOpposite(List<DoubleVertice> perpendicular)
            {
                var dx = perpendicular[1].X - perpendicular[0].X;
                var dy = perpendicular[1].Y - perpendicular[0].Y;
                perpendicular[1].X = perpendicular[0].X - dx;
                perpendicular[1].Y = perpendicular[0].Y - dy;
            }

            private double GetX1(double A, double D, double B)
        {
            return (-B + Math.Sqrt(D)) / (2 * A);
        }

        //public  DoubleVertice GetDistancedPerpendicular(DoubleVertice startP, DoubleVertice endP, double distance)
        //{
        //    var result = new DoubleVertice();
        //    var endPx = endP.X;
        //    var endPy = endP.Y;
        //    var a = GetPerpendicularCoefficient(Get_k_Coefficient(startP.X, startP.Y, endPx, endPy));
        //    var b = Get_b_Coefficient(a, endPy, endPx);
        //    var A = GetA(a, endPy);
        //    var B = GetB(b, a, endPy, endPx);
        //    var C = GetC(b, distance, endPy, endPx);
        //    var D = GetDiscriminant(A, B, C);
        //    var resultX = GetX1(A, D, B);
        //    result.X = resultX;
        //    result.Y = a * resultX + B;
        //    return result;
        //}
         }

    }
    }
