using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000384 RID: 900
	internal struct KeyStruct
	{
		// Token: 0x06001D3C RID: 7484 RVA: 0x0008B0A7 File Offset: 0x000892A7
		public KeyStruct(Major major, Minor minor, string symbol)
		{
			this.Major = major;
			this.Minor = minor;
			this.Symbol = symbol;
		}

		// Token: 0x04001864 RID: 6244
		public Major Major;

		// Token: 0x04001865 RID: 6245
		public Minor Minor;

		// Token: 0x04001866 RID: 6246
		public string Symbol;
	}
}
