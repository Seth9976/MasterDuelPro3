using System;

namespace System.Text
{
	// Token: 0x020002DD RID: 733
	[Serializable]
	internal sealed class InternalDecoderBestFitFallback : DecoderFallback
	{
		// Token: 0x06001A0E RID: 6670 RVA: 0x00062FA4 File Offset: 0x000611A4
		internal InternalDecoderBestFitFallback(Encoding encoding)
		{
			this._encoding = encoding;
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x00062FBB File Offset: 0x000611BB
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalDecoderBestFitFallbackBuffer(this);
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001A10 RID: 6672 RVA: 0x0000C091 File Offset: 0x0000A291
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001A11 RID: 6673 RVA: 0x00062FC4 File Offset: 0x000611C4
		public override bool Equals(object value)
		{
			InternalDecoderBestFitFallback internalDecoderBestFitFallback = value as InternalDecoderBestFitFallback;
			return internalDecoderBestFitFallback != null && this._encoding.CodePage == internalDecoderBestFitFallback._encoding.CodePage;
		}

		// Token: 0x06001A12 RID: 6674 RVA: 0x00062FF5 File Offset: 0x000611F5
		public override int GetHashCode()
		{
			return this._encoding.CodePage;
		}

		// Token: 0x04000C5A RID: 3162
		internal Encoding _encoding;

		// Token: 0x04000C5B RID: 3163
		internal char[] _arrayBestFit;

		// Token: 0x04000C5C RID: 3164
		internal char _cReplacement = '?';
	}
}
