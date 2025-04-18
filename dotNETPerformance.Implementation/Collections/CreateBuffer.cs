namespace dotNETPerformance.Implementation.Collections;

public static class CreateBuffer
{
    public static void FromSpan(int size)
    {
        Span<byte> buffer = stackalloc byte[size];
        buffer.Fill(0);
    }

    public static void FromArray(int size)
    {
        var buffer = new byte[size];
        Array.Fill(buffer, (byte)0);
    }
    
    public static void FromList(int size)
    {
        var buffer = Enumerable.Repeat(0, size).ToList();
    }
}