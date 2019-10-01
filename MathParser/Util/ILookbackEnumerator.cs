using System;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Util
{
    public interface ILookbackEnumerator<T> : IEnumerator<T>
    {
        public bool IsPastEnd { get; }
        public bool IsBeforeBeginning { get; }
        public bool HasCurrent { get; }

        public bool StepBack();

        public void ForgetPreceding();
    }
}
