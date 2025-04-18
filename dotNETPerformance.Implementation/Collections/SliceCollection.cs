using System.Runtime.InteropServices;

namespace dotNETPerformance.Implementation.Collections;

public static class SliceCollection
{
    public static Span<int> SliceSpan(Span<int> span, int start, int length)
    {
        return span.Slice(start, length);
    }

    public static ArraySegment<int> SliceArray(int[] array, int start, int length)
    {
        return new ArraySegment<int>(array, start, length);
    }

    public static List<int> SliceList(List<int> list, int start, int length)
    {
        return list.GetRange(start, length);
    }

    public static Span<int> SliceListAsSpan(List<int> list, int start, int length)
    {
        return CollectionsMarshal.AsSpan(list).Slice(start, length);
    }
}