using System;
using System.Globalization;

namespace System
{
	// Token: 0x020000E4 RID: 228
	public abstract class FormattableString : IFormattable
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000791 RID: 1937
		public abstract string Format { get; }

		// Token: 0x06000792 RID: 1938
		public abstract object[] GetArguments();

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000793 RID: 1939
		public abstract int ArgumentCount { get; }

		// Token: 0x06000794 RID: 1940
		public abstract object GetArgument(int index);

		// Token: 0x06000795 RID: 1941
		public abstract string ToString(IFormatProvider formatProvider);

		// Token: 0x06000796 RID: 1942 RVA: 0x0001E7C5 File Offset: 0x0001C9C5
		string IFormattable.ToString(string ignored, IFormatProvider formatProvider)
		{
			return this.ToString(formatProvider);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0001E7CE File Offset: 0x0001C9CE
		public override string ToString()
		{
			return this.ToString(CultureInfo.CurrentCulture);
		}
	}
}
