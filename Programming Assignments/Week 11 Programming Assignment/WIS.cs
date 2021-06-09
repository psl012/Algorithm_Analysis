using System;
using System.Collections.Generic;

class WIS
{
    public float[] Apply(float[] numArray)
    {
        float[] A = new float[numArray.Length + 1];
        A[0] = 0;
        A[1] = numArray[0];

        for(int i = 2; i < numArray.Length + 1; i++)
        {
            A[i] = MathF.Max(A[i-1], A[i-2] + numArray[i - 1]);
        }

        return A;
    }

    public List<float> Reconstruction(float[] A, float[] v)
    {
        List<float> S = new List<float>();
        List<int> temp = new List<int>();
        int i = A.Length - 1;
        bool includeFirst = true;
        while(i >= 2)
        {
            if (A[i-1] >= A[i-2] + v[i-1])
            {
                i = i - 1;
                includeFirst = true;
            }
            else
            {
                S.Add(v[i-1]);
                temp.Add(i);
                i = i - 2;
                includeFirst = false;
            }
        } 

        if (i == 0 && includeFirst)
        {
            S.Add(v[i]);
            temp.Add(i);
        }

        return S;
    }
}