using System;
using System.Globalization;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000021 RID: 33
	internal class CharEntityEncoderFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x06000127 RID: 295 RVA: 0x0000A366 File Offset: 0x00008566
		internal CharEntityEncoderFallbackBuffer(CharEntityEncoderFallback parent)
		{
			this.parent = parent;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000A388 File Offset: 0x00008588
		public override bool Fallback(char charUnknown, int index)
		{
			if (this.charEntityIndex >= 0)
			{
				new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknown, index);
			}
			if (this.parent.CanReplaceAt(index))
			{
				this.charEntity = string.Format(CultureInfo.InvariantCulture, "&#x{0:X};", new object[] { (int)charUnknown });
				this.charEntityIndex = 0;
				return true;
			}
			new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknown, index);
			return false;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000A400 File Offset: 0x00008600
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsSurrogatePair(charUnknownHigh, charUnknownLow))
			{
				throw XmlConvert.CreateInvalidSurrogatePairException(charUnknownHigh, charUnknownLow);
			}
			if (this.charEntityIndex >= 0)
			{
				new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknownHigh, charUnknownLow, index);
			}
			if (this.parent.CanReplaceAt(index))
			{
				this.charEntity = string.Format(CultureInfo.InvariantCulture, "&#x{0:X};", new object[] { this.SurrogateCharToUtf32(charUnknownHigh, charUnknownLow) });
				this.charEntityIndex = 0;
				return true;
			}
			new EncoderExceptionFallback().CreateFallbackBuffer().Fallback(charUnknownHigh, charUnknownLow, index);
			return false;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000A490 File Offset: 0x00008690
		public override char GetNextChar()
		{
			if (this.charEntityIndex == this.charEntity.Length)
			{
				this.charEntityIndex = -1;
			}
			if (this.charEntityIndex == -1)
			{
				return '\0';
			}
			string text = this.charEntity;
			int num = this.charEntityIndex;
			this.charEntityIndex = num + 1;
			return text[num];
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000A4DE File Offset: 0x000086DE
		public override bool MovePrevious()
		{
			if (this.charEntityIndex == -1)
			{
				return false;
			}
			if (this.charEntityIndex > 0)
			{
				this.charEntityIndex--;
				return true;
			}
			return false;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000A505 File Offset: 0x00008705
		public override int Remaining
		{
			get
			{
				if (this.charEntityIndex == -1)
				{
					return 0;
				}
				return this.charEntity.Length - this.charEntityIndex;
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000A524 File Offset: 0x00008724
		public override void Reset()
		{
			this.charEntityIndex = -1;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000A52D File Offset: 0x0000872D
		private int SurrogateCharToUtf32(char highSurrogate, char lowSurrogate)
		{
			return XmlCharType.CombineSurrogateChar((int)lowSurrogate, (int)highSurrogate);
		}

		// Token: 0x040000FA RID: 250
		private CharEntityEncoderFallback parent;

		// Token: 0x040000FB RID: 251
		private string charEntity = string.Empty;

		// Token: 0x040000FC RID: 252
		private int charEntityIndex = -1;
	}
}
