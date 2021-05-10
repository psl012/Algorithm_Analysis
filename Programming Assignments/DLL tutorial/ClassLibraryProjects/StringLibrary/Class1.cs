using System;

namespace UtilityLibraries
{
    public static class StringLibrary
    {
        public static bool StartsWithUpper(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            char ch = str[0];
            return char.IsUpper(ch);
        }
    }

    public class Job
    {
        public float _weight {get; private set;}
        public float _length {get; private set;}
        public float _jobValue;

        public Job(int weight, int length, int jobValue = -9999)
        {
            _weight = weight;
            _length = length;
            _jobValue = jobValue;
        }
    }
}