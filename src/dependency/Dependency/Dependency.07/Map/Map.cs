#nullable enable

using System;

namespace PrimeFuncPack
{
    partial class Dependency<T1, T2, T3, T4, T5, T6, T7>
    {
        public Dependency<TR1, TR2, TR3, TR4, TR5, TR6, TR7> Map<TR1, TR2, TR3, TR4, TR5, TR6, TR7>(
            Func<T1, TR1> mapFirst,
            Func<T2, TR2> mapSecond,
            Func<T3, TR3> mapThird,
            Func<T4, TR4> mapFourth,
            Func<T5, TR5> mapFifth,
            Func<T6, TR6> mapSixth,
            Func<T7, TR7> mapSeventh)
            =>
            InternalMap(
                mapFirst ?? throw new ArgumentNullException(nameof(mapFirst)),
                mapSecond ?? throw new ArgumentNullException(nameof(mapSecond)),
                mapThird ?? throw new ArgumentNullException(nameof(mapThird)),
                mapFourth ?? throw new ArgumentNullException(nameof(mapFourth)),
                mapFifth ?? throw new ArgumentNullException(nameof(mapFifth)),
                mapSixth ?? throw new ArgumentNullException(nameof(mapSixth)),
                mapSeventh ?? throw new ArgumentNullException(nameof(mapSeventh)));

        private Dependency<TR1, TR2, TR3, TR4, TR5, TR6, TR7> InternalMap<TR1, TR2, TR3, TR4, TR5, TR6, TR7>(
            Func<T1, TR1> mapFirst,
            Func<T2, TR2> mapSecond,
            Func<T3, TR3> mapThird,
            Func<T4, TR4> mapFourth,
            Func<T5, TR5> mapFifth,
            Func<T6, TR6> mapSixth,
            Func<T7, TR7> mapSeventh)
            =>
            new(
                sp => sp.Pipe(firstResolver).Pipe(mapFirst),
                sp => sp.Pipe(secondResolver).Pipe(mapSecond),
                sp => sp.Pipe(thirdResolver).Pipe(mapThird),
                sp => sp.Pipe(fourthResolver).Pipe(mapFourth),
                sp => sp.Pipe(fifthResolver).Pipe(mapFifth),
                sp => sp.Pipe(sixthResolver).Pipe(mapSixth),
                sp => sp.Pipe(seventhResolver).Pipe(mapSeventh));
    }
}