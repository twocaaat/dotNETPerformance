namespace dotNETPerformance.Implementation.Collections;

public static class IterativeFillCollection
{
    public static void Span(int size)
    {
        Span<int> span = stackalloc int[size];
        for (int i = 0; i < size; i++)
        {
            span[i] = i;
        }
    }
    
    public static void Array(int size)
    {
        var array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = i;
        }
    }
    
    public static void List(int size)
    {
        var list = new List<int>(size);
        for (int i = 0; i < size; i++)
        {
            list.Add(i);
        }
    }
}