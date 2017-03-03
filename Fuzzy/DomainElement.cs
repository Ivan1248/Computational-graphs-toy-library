using System;
using System.Text;

namespace Fuzzy
{
    public class DomainElement
    {
        private readonly int[] values;

        private DomainElement(params int[] values)
        {
            this.values = values;
        }

        public int ComponentCount => values.Length;

        public int this[int i] => values[i];

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            for (var i = 0; i < values.Length; i++)
                if (((DomainElement)obj)[i] != this[i])
                    return false;
            return true;
        }

        public override int GetHashCode() => this.values.GetHashCode();

        public static bool operator ==(DomainElement a, DomainElement b)
            => ReferenceEquals(a, null) ? ReferenceEquals(b, null) : a.Equals(b);

        public static bool operator !=(DomainElement a, DomainElement b) => !(a == b);

        public override string ToString()
        {
            var sb = new StringBuilder(this.values.Length * 8).Append('(');
            foreach (var value in values) sb.Append(value.ToString()).Append(',');
            sb.Length--;
            sb.Append(')');
            return sb.ToString();
        }

        internal int[] AsArray() => values;

        public int[] ToArray()
        {
            int[] r = new int[ComponentCount];
            Array.Copy(values, r, ComponentCount);
            return r;
        }

        public static DomainElement Of(params int[] val) => new DomainElement(val);

        public static DomainElement Of(params DomainElement[] vals)
        {
            int len = 0;
            foreach (var v in vals)
                len += v.ComponentCount;
            int[] val = new int[len];
            int i = 0;
            foreach (var v in vals)
            {
                Array.Copy(v.AsArray(), 0, val, i, v.ComponentCount);
                i += v.ComponentCount;
            }
            return new DomainElement(val);
        }

        public static DomainElement Of(params int[][] vals)
        {
            int len = 0;
            foreach (var v in vals)
                len += v.Length;
            int[] val = new int[len];
            int i = 0;
            foreach (var v in vals)
            {
                Array.Copy(v, 0, val, i, v.Length);
                i += v.Length;
            }
            return new DomainElement(val);
        }
    }
}
