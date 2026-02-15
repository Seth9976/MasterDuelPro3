using System;

namespace System.Text
{
	// Token: 0x020002E8 RID: 744
	[Serializable]
	internal class InternalEncoderBestFitFallback : EncoderFallback
	{
		// Token: 0x06001A5E RID: 6750 RVA: 0x00063ED4 File Offset: 0x000620D4
		internal InternalEncoderBestFitFallback(Encoding encoding)
		{
			this._encoding = encoding;
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x00063EE3 File Offset: 0x000620E3
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new InternalEncoderBestFitFallbackBuffer(this);
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x0000C091 File Offset: 0x0000A291
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x00063EEC File Offset: 0x000620EC
		public override bool Equals(object value)
		{
			InternalEncoderBestFitFallback internalEncoderBestFitFallback = value as InternalEncoderBestFitFallback;
			return internalEncoderBestFitFallback != null && this._encoding.CodePage == internalEncoderBestFitFallback._encoding.CodePage;
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x00063F1D File Offset: 0x0006211D
		public override int GetHashCode()
		{
			return this._encoding.CodePage;
		}

		// Token: 0x04000C72 RID: 3186
		internal Encoding _encoding;

		// Token: 0x04000C73 RID: 3187
		internal char[] _arrayBestFit;
	}
}
