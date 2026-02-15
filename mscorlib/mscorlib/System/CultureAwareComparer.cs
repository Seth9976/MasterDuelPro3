using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000146 RID: 326
	[Serializable]
	public sealed class CultureAwareComparer : StringComparer, ISerializable
	{
		// Token: 0x06000B1B RID: 2843 RVA: 0x00031F31 File Offset: 0x00030131
		internal CultureAwareComparer(CultureInfo culture, CompareOptions options)
			: this(culture.CompareInfo, options)
		{
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00031F40 File Offset: 0x00030140
		internal CultureAwareComparer(CompareInfo compareInfo, CompareOptions options)
		{
			this._compareInfo = compareInfo;
			if ((options & ~(CompareOptions.IgnoreCase | CompareOptions.IgnoreNonSpace | CompareOptions.IgnoreSymbols | CompareOptions.IgnoreKanaType | CompareOptions.IgnoreWidth | CompareOptions.StringSort)) != CompareOptions.None)
			{
				throw new ArgumentException("Value of flags is invalid.", "options");
			}
			this._options = options;
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00031F70 File Offset: 0x00030170
		private CultureAwareComparer(SerializationInfo info, StreamingContext context)
		{
			this._compareInfo = (CompareInfo)info.GetValue("_compareInfo", typeof(CompareInfo));
			bool boolean = info.GetBoolean("_ignoreCase");
			object valueNoThrow = info.GetValueNoThrow("_options", typeof(CompareOptions));
			if (valueNoThrow != null)
			{
				this._options = (CompareOptions)valueNoThrow;
			}
			this._options |= (boolean ? CompareOptions.IgnoreCase : CompareOptions.None);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00031FE8 File Offset: 0x000301E8
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
			return this._compareInfo.Compare(x, y, this._options);
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x0003200D File Offset: 0x0003020D
		public override bool Equals(string x, string y)
		{
			return x == y || (x != null && y != null && this._compareInfo.Compare(x, y, this._options) == 0);
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00032033 File Offset: 0x00030233
		public override int GetHashCode(string obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return this._compareInfo.GetHashCodeOfString(obj, this._options);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00032058 File Offset: 0x00030258
		public override bool Equals(object obj)
		{
			CultureAwareComparer cultureAwareComparer = obj as CultureAwareComparer;
			return cultureAwareComparer != null && this._options == cultureAwareComparer._options && this._compareInfo.Equals(cultureAwareComparer._compareInfo);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00032090 File Offset: 0x00030290
		public override int GetHashCode()
		{
			return this._compareInfo.GetHashCode() ^ (int)(this._options & (CompareOptions)2147483647);
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000320AA File Offset: 0x000302AA
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("_compareInfo", this._compareInfo);
			info.AddValue("_options", this._options);
			info.AddValue("_ignoreCase", (this._options & CompareOptions.IgnoreCase) > CompareOptions.None);
		}

		// Token: 0x04000486 RID: 1158
		private readonly CompareInfo _compareInfo;

		// Token: 0x04000487 RID: 1159
		private CompareOptions _options;
	}
}
