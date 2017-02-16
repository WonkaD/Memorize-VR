using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class Utils
    {
        public static void Shuffle<T>(IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static class ThreadSafeRandom
        {
            [ThreadStatic]
            private static Random Local;

            public static Random ThisThreadsRandom
            {
                get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
            }
        }

        public static IEnumerable<float> FloatRange(float from, float to, float step)
        {
            if (step <= 0.0f) step = (step == 0.0f) ? 1.0f : -step;

            if (from <= to)
            {
                for (float f = from; f <= to; f += step) yield return f;
            }
            else
            {
                for (float f = from; f >= to; f -= step) yield return f;
            }
        }

        public static List<Vector3> PosicionInPlane(float x0, float x1, float xStep, float y0, float y1, float yStep )
        {
            List<Vector3> positionList = new List<Vector3>();
            foreach (var x in FloatRange(x0, x1, xStep))
                foreach (var y in FloatRange(y0, y1, yStep))
                    positionList.Add(new Vector3(x, y, 0));
            return positionList;
        }

        public static List<Vector3> PosicionInCircle(Vector3 centerPoint, float radius, double degreeFrom, double degreeTo, double degreeStep)
        {
            DegreesToRadianes(ref degreeFrom, ref degreeTo, ref degreeStep);
            List<Vector3> positionList = new List<Vector3>();
            foreach (var degree in DegreeRange(degreeFrom, degreeTo, degreeStep))
                positionList.Add(new Vector3(CalculateNewXPosition(centerPoint, radius, degree), centerPoint.y, CalculateNewZPosition(centerPoint, radius, degree)));
            return positionList;
        }

        private static void DegreesToRadianes(ref double degreeFrom, ref double degreeTo, ref double degreeStep)
        {
            degreeFrom = DegreesToRadianes(degreeFrom);
            degreeTo = DegreesToRadianes(degreeTo);
            degreeStep = DegreesToRadianes(degreeStep);
        }

        private static float CalculateNewZPosition(Vector3 centerPoint, float radius, double degree)
        {
            return Convert.ToSingle(radius * Math.Sin(degree) + centerPoint.z);
        }

        private static float CalculateNewXPosition(Vector3 centerPoint, float radius, double degree)
        {
            return Convert.ToSingle(radius * Math.Cos(degree) + centerPoint.x);
        }

        private static double DegreesToRadianes(double degrees)
        {
            return Math.PI* degrees / 180.0d;
        }

        private static IEnumerable<double> DegreeRange(double from, double to, double step)
        {
            if (step <= 0.0f) step = (step == 0.0f) ? 1.0f : -step;

            if (from <= to)
            {
                for (double f = from; f <= to; f += step) yield return f;
            }
            else
            {
                for (double f = from; f >= to; f -= step) yield return f;
            }
        }

        public static T PopAt<T>(List<T> list, int index)
        {
            T r = list[index];
            list.RemoveAt(index);
            return r;
        }

    }
}