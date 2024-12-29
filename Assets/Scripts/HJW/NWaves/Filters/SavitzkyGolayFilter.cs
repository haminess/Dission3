﻿using NWaves.Filters.Base;
using NWaves.Utils;
using System;

namespace NWaves.Filters
{
    /// <summary>
    /// Represents Savitzky-Golay filter.
    /// </summary>
    public class SavitzkyGolayFilter : FirFilter
    {
        /// <summary>
        /// Gets size of the filter.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Constructs <see cref="SavitzkyGolayFilter"/> of given <paramref name="size"/>.
        /// </summary>
        /// <param name="size">Size of the filter (must be odd number in range [5..31])</param>
        /// <param name="deriv">Derivative (must be 0, 1 or 2)</param>
        public SavitzkyGolayFilter(int size, int deriv = 0) : base(MakeKernel(size, deriv))
        {
            Size = size;
        }

        /// <summary>
        /// Generates filter kernel. Kernels are precomputed.
        /// </summary>
        /// <param name="size">Size of the filter (must be odd number in range [5..31])</param>
        /// <param name="deriv">Derivative (must be 0, 1 or 2)</param>
        private static double[] MakeKernel(int size, int deriv = 0)
        {
            Guard.AgainstEvenNumber(size, "The size of the filter");

            double[] kernel;

            if (deriv == 0)
            {
                switch (size)
                {
                    case 5:
                        kernel = new double[5] { -0.08571429, 0.34285714, 0.48571429, 0.34285714, -0.08571429 };
                        break;
                    case 7:
                        kernel = new double[7] { -0.0952381 ,  0.14285714,  0.28571429,  0.33333333,  0.28571429,
                                                  0.14285714, -0.0952381 };
                        break;
                    case 9:
                        kernel = new double[9] { -0.09090909,  0.06060606,  0.16883117,  0.23376623,  0.25541126,
                                                  0.23376623,  0.16883117,  0.06060606, -0.09090909 };
                        break;
                    case 11:
                        kernel = new double[11] { -0.08391608,  0.02097902,  0.1025641 ,  0.16083916,  0.1958042 ,
                                                   0.20745921,  0.1958042 ,  0.16083916,  0.1025641 ,  0.02097902, -0.08391608 };
                        break;
                    case 13:
                        kernel = new double[13] { -7.69230769e-02,  5.55111512e-17,  6.29370629e-02,  1.11888112e-01,
                                                   1.46853147e-01,  1.67832168e-01,  1.74825175e-01,  1.67832168e-01,
                                                   1.46853147e-01,  1.11888112e-01,  6.29370629e-02,  1.38777878e-17, -7.69230769e-02 };
                        break;
                    case 15:
                        kernel = new double[15] { -0.07058824, -0.01176471,  0.03800905,  0.07873303,  0.11040724,
                                                   0.13303167,  0.14660633,  0.15113122,  0.14660633,  0.13303167,
                                                   0.11040724,  0.07873303,  0.03800905, -0.01176471, -0.07058824 };
                        break;
                    case 17:
                        kernel = new double[17] { -0.06501548, -0.01857585,  0.02167183,  0.05572755,  0.08359133,
                                                   0.10526316,  0.12074303,  0.13003096,  0.13312693,  0.13003096,
                                                   0.12074303,  0.10526316,  0.08359133,  0.05572755,  0.02167183,
                                                  -0.01857585, -0.06501548 };
                        break;
                    case 19:
                        kernel = new double[19] { -0.06015038, -0.02255639,  0.01061477,  0.03936311,  0.06368863,
                                                   0.08359133,  0.09907121,  0.11012826,  0.11676249,  0.11897391,
                                                   0.11676249,  0.11012826,  0.09907121,  0.08359133,  0.06368863,
                                                   0.03936311,  0.01061477, -0.02255639, -0.06015038 };
                        break;
                    case 21:
                        kernel = new double[21] { -0.05590062, -0.02484472,  0.00294214,  0.02745995,  0.04870873,
                                                   0.06668846,  0.08139915,  0.0928408 ,  0.1010134 ,  0.10591697,
                                                   0.10755149,  0.10591697,  0.1010134 ,  0.0928408 ,  0.08139915,
                                                   0.06668846,  0.04870873,  0.02745995,  0.00294214, -0.02484472, -0.05590062 };
                        break;
                    case 23:
                        kernel = new double[23] { -0.05217391, -0.02608696, -0.00248447,  0.01863354,  0.03726708,
                                                   0.05341615,  0.06708075,  0.07826087,  0.08695652,  0.0931677 ,
                                                   0.09689441,  0.09813665,  0.09689441,  0.0931677 ,  0.08695652,
                                                   0.07826087,  0.06708075,  0.05341615,  0.03726708,  0.01863354,
                                                  -0.00248447, -0.02608696, -0.05217391 };
                        break;
                    case 25:
                        kernel = new double[25] { -0.04888889, -0.02666667, -0.00637681,  0.01198068,  0.0284058 ,
                                                   0.04289855,  0.05545894,  0.06608696,  0.07478261,  0.08154589,
                                                   0.08637681,  0.08927536,  0.09024155,  0.08927536,  0.08637681,
                                                   0.08154589,  0.07478261,  0.06608696,  0.05545894,  0.04289855,
                                                   0.0284058 ,  0.01198068, -0.00637681, -0.02666667, -0.04888889 };
                        break;
                    case 27:
                        kernel = new double[27] { -0.04597701, -0.02681992, -0.0091954 ,  0.00689655,  0.02145594,
                                                   0.03448276,  0.04597701,  0.0559387 ,  0.06436782,  0.07126437,
                                                   0.07662835,  0.08045977,  0.08275862,  0.0835249 ,  0.08275862,
                                                   0.08045977,  0.07662835,  0.07126437,  0.06436782,  0.0559387 ,
                                                   0.04597701,  0.03448276,  0.02145594,  0.00689655, -0.0091954 ,
                                                  -0.02681992, -0.04597701 };
                        break;
                    case 29:
                        kernel = new double[29] { -0.04338154, -0.02669633, -0.01124706,  0.00296626,  0.01594364,
                                                   0.02768508,  0.03819058,  0.04746014,  0.05549376,  0.06229143,
                                                   0.06785317,  0.07217896,  0.07526882,  0.07712273,  0.0777407 ,
                                                   0.07712273,  0.07526882,  0.07217896,  0.06785317,  0.06229143,
                                                   0.05549376,  0.04746014,  0.03819058,  0.02768508,  0.01594364,
                                                   0.00296626, -0.01124706, -0.02669633, -0.04338154 };
                        break;
                    case 31:
                        kernel = new double[31] { -0.04105572, -0.02639296, -0.01274143, -0.00010112,  0.01152796,
                                                   0.02214582,  0.03175245,  0.04034786,  0.04793205,  0.05450501,
                                                   0.06006674,  0.06461725,  0.06815654,  0.0706846 ,  0.07220144,
                                                   0.07270705,  0.07220144,  0.0706846 ,  0.06815654,  0.06461725,
                                                   0.06006674,  0.05450501,  0.04793205,  0.04034786,  0.03175245,
                                                   0.02214582,  0.01152796, -0.00010112, -0.01274143, -0.02639296, -0.04105572 };
                        break;
                    default:
                        throw new ArgumentException("Size of the filter must be in range [5, 31]!");
                }
            }
            else if (deriv == 1)
            {
                switch (size)
                {
                    case 5:
                        kernel = new double[5] { 0.2, 0.1, 0, -0.1, -0.2 };
                        break;
                    case 7:
                        kernel = new double[7] { 1.07142857e-01,  7.14285714e-02,  3.57142857e-02, 0,
                                                -3.57142857e-02, -7.14285714e-02, -1.07142857e-01 };
                        break;
                    case 9:
                        kernel = new double[9] { 6.66666667e-02,  5.00000000e-02,  3.33333333e-02,  1.66666667e-02, 0,
                                                -1.66666667e-02, -3.33333333e-02, -5.00000000e-02, -6.66666667e-02 };
                        break;
                    case 11:
                        kernel = new double[11] { 4.54545455e-02,  3.63636364e-02,  2.72727273e-02,  1.81818182e-02,
                                                  9.09090909e-03,  0,              -9.09090909e-03, -1.81818182e-02,
                                                 -2.72727273e-02, -3.63636364e-02, -4.54545455e-02 };
                        break;
                    case 13:
                        kernel = new double[13] { 3.29670330e-02,  2.74725275e-02,  2.19780220e-02,  1.64835165e-02,
                                                  1.09890110e-02,  5.49450549e-03,  0             , -5.49450549e-03,
                                                 -1.09890110e-02, -1.64835165e-02, -2.19780220e-02, -2.74725275e-02, -3.29670330e-02 };
                        break;
                    case 15:
                        kernel = new double[15] { 2.50000000e-02,  2.14285714e-02,  1.78571429e-02,  1.42857143e-02,
                                                  1.07142857e-02,  7.14285714e-03,  3.57142857e-03,  0             ,
                                                 -3.57142857e-03, -7.14285714e-03, -1.07142857e-02, -1.42857143e-02,
                                                 -1.78571429e-02, -2.14285714e-02, -2.50000000e-02 };
                        break;
                    case 17:
                        kernel = new double[17] { 1.96078431e-02,  1.71568627e-02,  1.47058824e-02,  1.22549020e-02,
                                                  9.80392157e-03,  7.35294118e-03,  4.90196078e-03,  2.45098039e-03,
                                                  0             , -2.45098039e-03, -4.90196078e-03, -7.35294118e-03,
                                                 -9.80392157e-03, -1.22549020e-02, -1.47058824e-02, -1.71568627e-02, -1.96078431e-02 };
                        break;
                    case 19:
                        kernel = new double[19] { 1.57894737e-02,  1.40350877e-02,  1.22807018e-02,  1.05263158e-02,
                                                  8.77192982e-03,  7.01754386e-03,  5.26315789e-03,  3.50877193e-03,
                                                  1.75438596e-03,  0             , -1.75438596e-03, -3.50877193e-03,
                                                 -5.26315789e-03, -7.01754386e-03, -8.77192982e-03, -1.05263158e-02,
                                                 -1.22807018e-02, -1.40350877e-02, -1.57894737e-02 };
                        break;
                    case 21:
                        kernel = new double[21] { 1.29870130e-02,  1.16883117e-02,  1.03896104e-02,  9.09090909e-03,
                                                  7.79220779e-03,  6.49350649e-03,  5.19480519e-03,  3.89610390e-03,
                                                  2.59740260e-03,  1.29870130e-03,  0             , -1.29870130e-03,
                                                 -2.59740260e-03, -3.89610390e-03, -5.19480519e-03, -6.49350649e-03,
                                                 -7.79220779e-03, -9.09090909e-03, -1.03896104e-02, -1.16883117e-02, -1.29870130e-02 };
                        break;
                    case 23:
                        kernel = new double[23] { 0.01086957,  0.00988142,  0.00889328,  0.00790514,  0.006917  ,
                                                  0.00592885,  0.00494071,  0.00395257,  0.00296443,  0.00197628,
                                                  0.00098814,  0         , -0.00098814, -0.00197628, -0.00296443,
                                                 -0.00395257, -0.00494071, -0.00592885, -0.006917  , -0.00790514,
                                                 -0.00889328, -0.00988142, -0.01086957 };
                        break;
                    case 25:
                        kernel = new double[25] { 9.23076923e-03,  8.46153846e-03,  7.69230769e-03,  6.92307692e-03,
                                                  6.15384615e-03,  5.38461538e-03,  4.61538462e-03,  3.84615385e-03,
                                                  3.07692308e-03,  2.30769231e-03,  1.53846154e-03,  7.69230769e-04,
                                                  0             , -7.69230769e-04, -1.53846154e-03, -2.30769231e-03,
                                                 -3.07692308e-03, -3.84615385e-03, -4.61538462e-03, -5.38461538e-03,
                                                 -6.15384615e-03, -6.92307692e-03, -7.69230769e-03, -8.46153846e-03, -9.23076923e-03 };
                        break;
                    case 27:
                        kernel = new double[27] { 7.93650794e-03,  7.32600733e-03,  6.71550672e-03,  6.10500611e-03,
                                                  5.49450549e-03,  4.88400488e-03,  4.27350427e-03,  3.66300366e-03,
                                                  3.05250305e-03,  2.44200244e-03,  1.83150183e-03,  1.22100122e-03,
                                                  6.10500611e-04,  0             , -6.10500611e-04, -1.22100122e-03,
                                                 -1.83150183e-03, -2.44200244e-03, -3.05250305e-03, -3.66300366e-03,
                                                 -4.27350427e-03, -4.88400488e-03, -5.49450549e-03, -6.10500611e-03,
                                                 -6.71550672e-03, -7.32600733e-03, -7.93650794e-03 };
                        break;
                    case 29:
                        kernel = new double[29] { 6.89655172e-03,  6.40394089e-03,  5.91133005e-03,  5.41871921e-03,
                                                  4.92610837e-03,  4.43349754e-03,  3.94088670e-03,  3.44827586e-03,
                                                  2.95566502e-03,  2.46305419e-03,  1.97044335e-03,  1.47783251e-03,
                                                  9.85221675e-04,  4.92610837e-04,  0             , -4.92610837e-04,
                                                 -9.85221675e-04, -1.47783251e-03, -1.97044335e-03, -2.46305419e-03,
                                                 -2.95566502e-03, -3.44827586e-03, -3.94088670e-03, -4.43349754e-03,
                                                 -4.92610837e-03, -5.41871921e-03, -5.91133005e-03, -6.40394089e-03, -6.89655172e-03 };
                        break;
                    case 31:
                        kernel = new double[31] { 0.00604839,  0.00564516,  0.00524194,  0.00483871,  0.00443548,
                                                  0.00403226,  0.00362903,  0.00322581,  0.00282258,  0.00241935,
                                                  0.00201613,  0.0016129 ,  0.00120968,  0.00080645,  0.00040323,
                                                  0         , -0.00040323, -0.00080645, -0.00120968, -0.0016129 ,
                                                 -0.00201613, -0.00241935, -0.00282258, -0.00322581, -0.00362903,
                                                 -0.00403226, -0.00443548, -0.00483871, -0.00524194, -0.00564516, -0.00604839 };
                        break;
                    default:
                        throw new ArgumentException("Size of the filter must be in range [5, 31]!");
                }
            }
            else if (deriv == 2)
            {
                switch (size)
                {
                    case 5:
                        kernel = new double[5] { 0.28571429, -0.14285714, -0.28571429, -0.14285714, 0.28571429 };
                        break;
                    case 7:
                        kernel = new double[7] { 1.19047619e-01,  0.00000000e+00, -7.14285714e-02, -9.52380952e-02,
                                                -7.14285714e-02,  1.38777878e-17,  1.19047619e-01 };
                        break;
                    case 9:
                        kernel = new double[9] { 0.06060606,  0.01515152, -0.01731602, -0.03679654, -0.04329004,
                                                -0.03679654, -0.01731602,  0.01515152,  0.06060606 };
                        break;
                    case 11:
                        kernel = new double[11] { 0.03496503,  0.01398601, -0.002331  , -0.01398601, -0.02097902,
                                                 -0.02331002, -0.02097902, -0.01398601, -0.002331  ,  0.01398601,  0.03496503 };
                        break;
                    case 13:
                        kernel = new double[13] { 0.02197802,  0.01098901,  0.001998  , -0.004995  , -0.00999001,
                                                 -0.01298701, -0.01398601, -0.01298701, -0.00999001, -0.004995  ,
                                                  0.001998  ,  0.01098901,  0.02197802 };
                        break;
                    case 15:
                        kernel = new double[15] { 0.01470588,  0.00840336,  0.00307046, -0.00129282, -0.00468649,
                                                 -0.00711054, -0.00856496, -0.00904977, -0.00856496, -0.00711054,
                                                 -0.00468649, -0.00129282,  0.00307046,  0.00840336,  0.01470588 };
                        break;
                    case 17:
                        kernel = new double[17] { 0.01031992,  0.00644995,  0.00309598,  0.000258  , -0.00206398,
                                                 -0.00386997, -0.00515996, -0.00593395, -0.00619195, -0.00593395,
                                                 -0.00515996, -0.00386997, -0.00206398,  0.000258  ,  0.00309598,
                                                  0.00644995,  0.01031992 };
                        break;
                    case 19:
                        kernel = new double[19] { 0.0075188 ,  0.00501253,  0.00280112,  0.00088456, -0.00073714,
                                                 -0.00206398, -0.00309598, -0.00383311, -0.00427539, -0.00442282,
                                                 -0.00427539, -0.00383311, -0.00309598, -0.00206398, -0.00073714,
                                                  0.00088456,  0.00280112,  0.00501253,  0.0075188 };
                        break;
                    case 21:
                        kernel = new double[21] { 5.64652739e-03,  3.95256917e-03,  2.43692235e-03,  1.09958691e-03,
                                                 -5.94371304e-05, -1.04014978e-03, -1.84255104e-03, -2.46664091e-03,
                                                 -2.91241939e-03, -3.17988648e-03, -3.26904217e-03, -3.17988648e-03,
                                                 -2.91241939e-03, -2.46664091e-03, -1.84255104e-03, -1.04014978e-03,
                                                 -5.94371304e-05,  1.09958691e-03,  2.43692235e-03,  3.95256917e-03,  5.64652739e-03 };
                        break;
                    case 23:
                        kernel = new double[23] { 0.00434783,  0.00316206,  0.00208922,  0.00112931,  0.00028233,
                                                 -0.00045172, -0.00107284, -0.00158103, -0.00197628, -0.00225861,
                                                 -0.00242801, -0.00248447, -0.00242801, -0.00225861, -0.00197628,
                                                 -0.00158103, -0.00107284, -0.00045172,  0.00028233,  0.00112931,
                                                  0.00208922,  0.00316206,  0.00434783 };
                        break;
                    case 25:
                        kernel = new double[25] { 0.0034188 ,  0.0025641 ,  0.00178372,  0.00107767,  0.00044593,
                                                 -0.00011148, -0.00059457, -0.00100334, -0.00133779, -0.00159792,
                                                 -0.00178372, -0.00189521, -0.00193237, -0.00189521, -0.00178372,
                                                 -0.00159792, -0.00133779, -0.00100334, -0.00059457, -0.00011148,
                                                  0.00044593,  0.00107767,  0.00178372,  0.0025641 ,  0.0034188 };
                        break;
                    case 27:
                        kernel = new double[27] { 2.73672687e-03,  2.10517452e-03,  1.52414635e-03,  9.93642373e-04,
                                                  5.13662583e-04,  8.42069808e-05, -2.94724433e-04, -6.23131658e-04,
                                                 -9.01014694e-04, -1.12837354e-03, -1.30520820e-03, -1.43151867e-03,
                                                 -1.50730496e-03, -1.53256705e-03, -1.50730496e-03, -1.43151867e-03,
                                                 -1.30520820e-03, -1.12837354e-03, -9.01014694e-04, -6.23131658e-04,
                                                 -2.94724433e-04,  8.42069808e-05,  5.13662583e-04,  9.93642373e-04,
                                                  1.52414635e-03,  2.10517452e-03,  2.73672687e-03 };
                        break;
                    case 29:
                        kernel = new double[29] { 0.00222469,  0.00174797,  0.00130657,  0.00090047,  0.00052969,
                                                  0.00019422, -0.00010594, -0.00037078, -0.00060031, -0.00079453,
                                                 -0.00095344, -0.00107703, -0.00116532, -0.00121828, -0.00123594,
                                                 -0.00121828, -0.00116532, -0.00107703, -0.00095344, -0.00079453,
                                                 -0.00060031, -0.00037078, -0.00010594,  0.00019422,  0.00052969,
                                                  0.00090047,  0.00130657,  0.00174797,  0.00222469 };
                        break;
                    case 31:
                        kernel = new double[31] { 1.83284457e-03,  1.46627566e-03,  1.12498736e-03,  8.08979674e-04,
                                                  5.18252604e-04,  2.52806148e-04,  1.26403074e-05, -2.02244919e-04,
                                                 -3.91849530e-04, -5.56173526e-04, -6.95216908e-04, -8.08979674e-04,
                                                 -8.97461826e-04, -9.60663363e-04, -9.98584286e-04, -1.01122459e-03,
                                                 -9.98584286e-04, -9.60663363e-04, -8.97461826e-04, -8.08979674e-04,
                                                 -6.95216908e-04, -5.56173526e-04, -3.91849530e-04, -2.02244919e-04,
                                                  1.26403074e-05,  2.52806148e-04,  5.18252604e-04,  8.08979674e-04,
                                                  1.12498736e-03,  1.46627566e-03,  1.83284457e-03 };
                        break;
                    default:
                        throw new ArgumentException("Size of the filter must be in range [5, 31]!");
                }
            }
            else
            {
                throw new ArgumentException("Parameter deriv must be 0, 1 or 2!");
            }

            return kernel;
        }
    }
}
