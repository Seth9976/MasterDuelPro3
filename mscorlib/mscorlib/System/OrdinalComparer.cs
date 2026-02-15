using System;
using System.Globalization;

namespace System
{
	// Token: 0x02000147 RID: 327
	[Serializable]
	public class OrdinalComparer : StringComparer
	{
		// Token: 0x06000B24 RID: 2852 RVA: 0x000320E9 File Offset: 0x000302E9
		internal OrdinalComparer(bool ignoreCase)
		{
			this._ignoreCase = ignoreCase;
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000320F8 File Offset: 0x000302F8
		public override int Compare(string x, string y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			if (this._ignoreCase)
			{
				return string.Compare(x, y, StringComparison.OrdinalIgnoreCase);
			}
			return string.CompareOrdinal(x, y);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00032122 File Offset: 0x00030322
		public override bool Equals(string x, string y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			if (this._ignoreCase)
			{
				return x.Length == y.Length && string.Compare(x, y, StringComparison.OrdinalIgnoreCase) == 0;
			}
			return x.Equals(y);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0003215D File Offset: 0x0003035D
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.obj);
			}
			if (this._ignoreCase)
			{
				return CompareInfo.GetIgnoreCaseHash(obj);
			}
			return obj.GetHashCode();
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00032180 File Offset: 0x00030380
		public override bool Equals(object obj)
		{
			OrdinalComparer ordinalComparer = obj as OrdinalComparer;
			return ordinalComparer != null && this._ignoreCase == ordinalComparer._ignoreCase;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000321A8 File Offset: 0x000303A8
		public override int GetHashCode()
		{
			int hashCode = "OrdinalComparer".GetHashCode();
			if (!this._ignoreCase)
			{
				return hashCode;
			}
			return ~hashCode;
		}

		// Token: 0x04000488 RID: 1160
		private readonly bool _ignoreCase;
	}
}
