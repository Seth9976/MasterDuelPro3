using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200037B RID: 891
	internal class Charset
	{
		// Token: 0x06001D16 RID: 7446 RVA: 0x0008AC5E File Offset: 0x00088E5E
		public Charset()
		{
			this.flags = CharsetFlags.Read | CharsetFlags.Switch;
			this.id = CharsetType.General;
			this.file = string.Empty;
			this.ReadMap();
		}

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x0008AC86 File Offset: 0x00088E86
		public CharsetFlags Flags
		{
			get
			{
				return this.flags;
			}
		}

		// Token: 0x1700073D RID: 1853
		// (set) Token: 0x06001D18 RID: 7448 RVA: 0x0008AC8E File Offset: 0x00088E8E
		public CharsetType ID
		{
			set
			{
				if (value != CharsetType.General && value == CharsetType.Symbol)
				{
					this.id = CharsetType.Symbol;
					return;
				}
				this.id = CharsetType.General;
			}
		}

		// Token: 0x1700073E RID: 1854
		public StandardCharCode this[int c]
		{
			get
			{
				return this.code[c];
			}
		}

		// Token: 0x06001D1A RID: 7450 RVA: 0x0008ACB4 File Offset: 0x00088EB4
		public bool ReadMap()
		{
			CharsetType charsetType = this.id;
			if (charsetType != CharsetType.General)
			{
				if (charsetType != CharsetType.Symbol)
				{
					return false;
				}
				if (this.file == string.Empty)
				{
					this.code = Charcode.AnsiSymbol;
					return true;
				}
				return true;
			}
			else
			{
				if (this.file == string.Empty)
				{
					this.code = Charcode.AnsiGeneric;
					return true;
				}
				return true;
			}
		}

		// Token: 0x04001849 RID: 6217
		private CharsetType id;

		// Token: 0x0400184A RID: 6218
		private CharsetFlags flags;

		// Token: 0x0400184B RID: 6219
		private Charcode code;

		// Token: 0x0400184C RID: 6220
		private string file;
	}
}
