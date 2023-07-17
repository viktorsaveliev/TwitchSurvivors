using UnityEngine;

public class ArrayHandler
{
    public T[] MixArray<T>(T[] mixedArray)
    {
        for (int i = 0; i < mixedArray.Length; i++)
        {
            T currentValue = mixedArray[i];
            int randomValue = Random.Range(i, mixedArray.Length);

            mixedArray[i] = mixedArray[randomValue];
            mixedArray[randomValue] = currentValue;
        }
        return mixedArray;
    }
}
