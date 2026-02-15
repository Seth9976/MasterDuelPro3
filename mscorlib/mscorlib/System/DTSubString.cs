using System;

namespace System
{
	// Token: 0x020000EF RID: 239
	internal ref struct DTSubString
	{
		// Token: 0x1700009C RID: 156
		internal unsafe char this[int relativeIndex]
		{
			get
			{
				return (char)(*this.s[this.index + relativeIndex]);
			}
		}

		// Token: 0x04000390 RID: 912
		internal ReadOnlySpan<char> s;

		// Token: 0x04000391 RID: 913
		internal int index;

		// Token: 0x04000392 RID: 914
		internal int length;

		// Token: 0x04000393 RID: 915
		internal DTSubStringType type;

		// Token: 0x04000394 RID: 916
		internal int value;
	}
}
