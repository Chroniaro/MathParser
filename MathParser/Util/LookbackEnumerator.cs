using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MathParser.Util
{
    public abstract class LookbackEnumerator<T> : IEnumerator<T>
    {
        protected List<T> LoadedValues { get; }
        protected int CurrentIndex { get; set; }

        public bool IsPastEnd => CurrentIndex >= LoadedValues.Count;
        public bool IsBeforeBeginning => CurrentIndex < 0;
        public bool HasCurrent => !(IsBeforeBeginning || IsPastEnd);
        public T Current => LoadedValues[CurrentIndex];
        object? IEnumerator.Current => Current;

        public LookbackEnumerator(int initialCapacity)
        {
            LoadedValues = new List<T>(initialCapacity);
            CurrentIndex = -1;
        }

        public LookbackEnumerator() :
            this(10)
        { }

        protected abstract void LoadMoreValues();

        public bool MoveNext()
        {
            if (IsPastEnd)
                return false;

            ++CurrentIndex;

            if (IsPastEnd)
            {
                LoadMoreValues();
                return !IsPastEnd;
            }
            else
                return true;
        }

        public bool StepBack()
        {
            if (CurrentIndex < 0)
                return false;

            --CurrentIndex;
            return true;
        }

        public void Reset()
        {
            CurrentIndex = -1;
        }

        public void ForgetPreceding()
        {
            if (CurrentIndex < 0)
                return;

            LoadedValues.RemoveRange(0, CurrentIndex);
            CurrentIndex = -1;
        }

        #region IDisposable Support
        protected virtual void Dispose(bool disposing)
        {}

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
