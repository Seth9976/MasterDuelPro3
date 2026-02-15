using System;

namespace System
{
	// Token: 0x0200010B RID: 267
	internal interface ISpanFormattable
	{
		// Token: 0x060008A1 RID: 2209
		bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider);
	}
}
