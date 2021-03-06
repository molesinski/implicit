﻿using System;

namespace Implicit
{
    public class AlternatingLeastSquaresIterationCompletedEventArgs : EventArgs
    {
        public AlternatingLeastSquaresIterationCompletedEventArgs(int iteration, double loss, TimeSpan elapsed)
        {
            this.Iteration = iteration;
            this.Loss = loss;
            this.Elapsed = elapsed;
        }

        public int Iteration { get; }

        public double Loss { get; }

        public TimeSpan Elapsed { get; }
    }
}
