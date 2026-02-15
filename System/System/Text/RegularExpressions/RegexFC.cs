using System;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x0200013D RID: 317
	internal sealed class RegexFC
	{
		// Token: 0x060006E1 RID: 1761 RVA: 0x00024E73 File Offset: 0x00023073
		public RegexFC(bool nullable)
		{
			this._cc = new RegexCharClass();
			this._nullable = nullable;
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00024E90 File Offset: 0x00023090
		public RegexFC(char ch, bool not, bool nullable, bool caseInsensitive)
		{
			this._cc = new RegexCharClass();
			if (not)
			{
				if (ch > '\0')
				{
					this._cc.AddRange('\0', ch - '\u0001');
				}
				if (ch < '\uffff')
				{
					this._cc.AddRange(ch + '\u0001', char.MaxValue);
				}
			}
			else
			{
				this._cc.AddRange(ch, ch);
			}
			this.CaseInsensitive = caseInsensitive;
			this._nullable = nullable;
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00024EFF File Offset: 0x000230FF
		public RegexFC(string charClass, bool nullable, bool caseInsensitive)
		{
			this._cc = RegexCharClass.Parse(charClass);
			this._nullable = nullable;
			this.CaseInsensitive = caseInsensitive;
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00024F24 File Offset: 0x00023124
		public bool AddFC(RegexFC fc, bool concatenate)
		{
			if (!this._cc.CanMerge || !fc._cc.CanMerge)
			{
				return false;
			}
			if (concatenate)
			{
				if (!this._nullable)
				{
					return true;
				}
				if (!fc._nullable)
				{
					this._nullable = false;
				}
			}
			else if (fc._nullable)
			{
				this._nullable = true;
			}
			this.CaseInsensitive |= fc.CaseInsensitive;
			this._cc.AddCharClass(fc._cc);
			return true;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060006E5 RID: 1765 RVA: 0x00024F9F File Offset: 0x0002319F
		// (set) Token: 0x060006E6 RID: 1766 RVA: 0x00024FA7 File Offset: 0x000231A7
		public bool CaseInsensitive { get; private set; }

		// Token: 0x060006E7 RID: 1767 RVA: 0x00024FB0 File Offset: 0x000231B0
		public string GetFirstChars(CultureInfo culture)
		{
			if (this.CaseInsensitive)
			{
				this._cc.AddLowercase(culture);
			}
			return this._cc.ToStringClass();
		}

		// Token: 0x040005A7 RID: 1447
		private RegexCharClass _cc;

		// Token: 0x040005A8 RID: 1448
		public bool _nullable;
	}
}
