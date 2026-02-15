using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000EE RID: 238
	[NullableContext(1)]
	[Nullable(0)]
	internal readonly struct StringReference
	{
		// Token: 0x170000E0 RID: 224
		public char this[int i]
		{
			get
			{
				return this._chars[i];
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x060006F9 RID: 1785 RVA: 0x00023A73 File Offset: 0x00021C73
		public char[] Chars
		{
			get
			{
				return this._chars;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x00023A7B File Offset: 0x00021C7B
		public int StartIndex
		{
			get
			{
				return this._startIndex;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00023A83 File Offset: 0x00021C83
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x060006FC RID: 1788 RVA: 0x00023A8B File Offset: 0x00021C8B
		public StringReference(char[] chars, int startIndex, int length)
		{
			this._chars = chars;
			this._startIndex = startIndex;
			this._length = length;
		}

		// Token: 0x060006FD RID: 1789 RVA: 0x00023AA2 File Offset: 0x00021CA2
		public override string ToString()
		{
			return new string(this._chars, this._startIndex, this._length);
		}

		// Token: 0x040004C7 RID: 1223
		private readonly char[] _chars;

		// Token: 0x040004C8 RID: 1224
		private readonly int _startIndex;

		// Token: 0x040004C9 RID: 1225
		private readonly int _length;
	}
}
