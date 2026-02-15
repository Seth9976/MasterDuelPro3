using System;
using System.Threading;

namespace System.Text
{
	// Token: 0x020002DE RID: 734
	internal sealed class InternalDecoderBestFitFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06001A13 RID: 6675 RVA: 0x00063004 File Offset: 0x00061204
		private static object InternalSyncObject
		{
			get
			{
				if (InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject, obj, null);
				}
				return InternalDecoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x06001A14 RID: 6676 RVA: 0x00063030 File Offset: 0x00061230
		public InternalDecoderBestFitFallbackBuffer(InternalDecoderBestFitFallback fallback)
		{
			this._oFallback = fallback;
			if (this._oFallback._arrayBestFit == null)
			{
				object internalSyncObject = InternalDecoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this._oFallback._arrayBestFit == null)
					{
						this._oFallback._arrayBestFit = fallback._encoding.GetBestFitBytesToUnicodeData();
					}
				}
			}
		}

		// Token: 0x06001A15 RID: 6677 RVA: 0x000630B0 File Offset: 0x000612B0
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this._cBestFit = this.TryBestFit(bytesUnknown);
			if (this._cBestFit == '\0')
			{
				this._cBestFit = this._oFallback._cReplacement;
			}
			this._iCount = (this._iSize = 1);
			return true;
		}

		// Token: 0x06001A16 RID: 6678 RVA: 0x000630F4 File Offset: 0x000612F4
		public override char GetNextChar()
		{
			this._iCount--;
			if (this._iCount < 0)
			{
				return '\0';
			}
			if (this._iCount == 2147483647)
			{
				this._iCount = -1;
				return '\0';
			}
			return this._cBestFit;
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x06001A17 RID: 6679 RVA: 0x0006312B File Offset: 0x0006132B
		public override int Remaining
		{
			get
			{
				if (this._iCount <= 0)
				{
					return 0;
				}
				return this._iCount;
			}
		}

		// Token: 0x06001A18 RID: 6680 RVA: 0x0006313E File Offset: 0x0006133E
		public override void Reset()
		{
			this._iCount = -1;
			this.byteStart = null;
		}

		// Token: 0x06001A19 RID: 6681 RVA: 0x0000C091 File Offset: 0x0000A291
		internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
		{
			return 1;
		}

		// Token: 0x06001A1A RID: 6682 RVA: 0x00063150 File Offset: 0x00061350
		private char TryBestFit(byte[] bytesCheck)
		{
			int num = 0;
			int num2 = this._oFallback._arrayBestFit.Length;
			if (num2 == 0)
			{
				return '\0';
			}
			if (bytesCheck.Length == 0 || bytesCheck.Length > 2)
			{
				return '\0';
			}
			char c;
			if (bytesCheck.Length == 1)
			{
				c = (char)bytesCheck[0];
			}
			else
			{
				c = (char)(((int)bytesCheck[0] << 8) + (int)bytesCheck[1]);
			}
			if (c < this._oFallback._arrayBestFit[0] || c > this._oFallback._arrayBestFit[num2 - 2])
			{
				return '\0';
			}
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = (num3 / 2 + num) & 65534;
				char c2 = this._oFallback._arrayBestFit[i];
				if (c2 == c)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
				if (c2 < c)
				{
					num = i;
				}
				else
				{
					num2 = i;
				}
			}
			for (int i = num; i < num2; i += 2)
			{
				if (this._oFallback._arrayBestFit[i] == c)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04000C5D RID: 3165
		private char _cBestFit;

		// Token: 0x04000C5E RID: 3166
		private int _iCount = -1;

		// Token: 0x04000C5F RID: 3167
		private int _iSize;

		// Token: 0x04000C60 RID: 3168
		private InternalDecoderBestFitFallback _oFallback;

		// Token: 0x04000C61 RID: 3169
		private static object s_InternalSyncObject;
	}
}
