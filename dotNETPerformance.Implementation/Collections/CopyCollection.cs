namespace dotNETPerformance.Implementation.Collections;

public static class CopyCollection
{
    public static void FromSpan(Span<byte> source, int size)
    {
        Span<byte> dest = stackalloc byte[size];
        source.CopyTo(dest);
    }
    
    public static void FromArray(byte[] source, int size)
    {
        var dest = new byte[size];
        Array.Copy(source, 0, dest, 0, size);
    }
    
    public static void FromArrayBlockCopy(byte[] source, int size)
    {
        var dest = new byte[size];
        Buffer.BlockCopy(source, 0, dest, 0, size);
    }

    public static void FromList(List<byte> source)
    {
        var dest = source.ToList();
    }
}