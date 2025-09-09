using System;


namespace HololiveMod.Utilities
{
    public static class MiscUtils
    {
        public static T[] ShuffleArray<T>(T[] array, Random rand = null)
        {
            if (rand is null)
                rand = new Random();

            for (int i = array.Length; i > 0; --i)
            {
                int j = rand.Next(i);
                T tempElement = array[j];
                array[j] = array[i - 1];
                array[i - 1] = tempElement;
            }
            return array;
        }

        public static int SecondsToFrames(int seconds) => seconds * 60;
        public static int SecondsToFrames(float seconds) => (int)(seconds * 60);
    }
}
