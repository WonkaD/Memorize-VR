using System;
using System.Collections.Generic;
using System.Threading;

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
    }
}