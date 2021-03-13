using System;
using System.Collections.Generic;
using System.Text;

namespace GameProject
{
    public static class Time
    {
        public static float TimeScale = 0;

        public static float ScaledTime { get; private set; }

        public static void SetScaledTime(float time)
        {
            ScaledTime = time * TimeScale;
        }
    }
}
