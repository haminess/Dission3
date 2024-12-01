﻿using NWaves.Filters.Base;
using NWaves.Filters.Fda;

namespace NWaves.Filters.ChebyshevI
{
    /// <summary>
    /// Represents bandstop Chebyshev-I filter.
    /// </summary>
    public class BandStopFilter : ZiFilter
    {
        /// <summary>
        /// Gets low cutoff frequency.
        /// </summary>
        public double FrequencyLow { get; private set; }

        /// <summary>
        /// Gets high cutoff frequency.
        /// </summary>
        public double FrequencyHigh { get; private set; }

        /// <summary>
        /// Gets ripple (in dB).
        /// </summary>
        public double Ripple { get; private set; }

        /// <summary>
        /// Gets filter order.
        /// </summary>
        public int Order => (_a.Length - 1) / 2;

        /// <summary>
        /// Constructs <see cref="BandStopFilter"/> of given <paramref name="order"/> 
        /// with given cutoff frequencies <paramref name="frequencyLow"/> and <paramref name="frequencyHigh"/>.
        /// </summary>
        /// <param name="frequencyLow">Normalized low cutoff frequency in range [0..0.5]</param>
        /// <param name="frequencyHigh">Normalized high cutoff frequency in range [0..0.5]</param>
        /// <param name="order">Filter order</param>
        /// <param name="ripple">Ripple (in dB)</param>
        public BandStopFilter(double frequencyLow, double frequencyHigh, int order, double ripple = 0.1) 
            : base(MakeTf(frequencyLow, frequencyHigh, order, ripple))
        {
            FrequencyLow = frequencyLow;
            FrequencyHigh = frequencyHigh;
            Ripple = ripple;
        }

        /// <summary>
        /// Generates transfer function.
        /// </summary>
        /// <param name="frequencyLow">Normalized low cutoff frequency in range [0..0.5]</param>
        /// <param name="frequencyHigh">Normalized high cutoff frequency in range [0..0.5]</param>
        /// <param name="order">Filter order</param>
        /// <param name="ripple">Ripple (in dB)</param>
        private static TransferFunction MakeTf(double frequencyLow, double frequencyHigh, int order, double ripple = 0.1)
        {
            return DesignFilter.IirBsTf(frequencyLow, frequencyHigh, PrototypeChebyshevI.Poles(order, ripple));
        }

        /// <summary>
        /// Changes filter coefficients online (preserving the state of the filter).
        /// </summary>
        /// <param name="frequencyLow">Normalized low cutoff frequency in range [0..0.5]</param>
        /// <param name="frequencyHigh">Normalized high cutoff frequency in range [0..0.5]</param>
        /// <param name="ripple">Ripple (in dB)</param>
        public void Change(double frequencyLow, double frequencyHigh, double ripple = 0.1)
        {
            FrequencyLow = frequencyLow;
            FrequencyHigh = frequencyHigh;
            Ripple = ripple;

            Change(MakeTf(frequencyLow, frequencyHigh, (_a.Length - 1) / 2, ripple));
        }
    }
}
