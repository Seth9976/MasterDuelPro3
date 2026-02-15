using System;
using System.Threading;

namespace System.Text
{
	// Token: 0x020002E9 RID: 745
	internal sealed class InternalEncoderBestFitFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x00063F2C File Offset: 0x0006212C
		private static object InternalSyncObject
		{
			get
			{
				if (InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject == null)
				{
					object obj = new object();
					Interlocked.CompareExchange<object>(ref InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject, obj, null);
				}
				return InternalEncoderBestFitFallbackBuffer.s_InternalSyncObject;
			}
		}

		// Token: 0x06001A64 RID: 6756 RVA: 0x00063F58 File Offset: 0x00062158
		public InternalEncoderBestFitFallbackBuffer(InternalEncoderBestFitFallback fallback)
		{
			this._oFallback = fallback;
			if (this._oFallback._arrayBestFit == null)
			{
				object internalSyncObject = InternalEncoderBestFitFallbackBuffer.InternalSyncObject;
				lock (internalSyncObject)
				{
					if (this._oFallback._arrayBestFit == null)
					{
						this._oFallback._arrayBestFit = fallback._encoding.GetBestFitUnicodeToBytesData();
					}
				}
			}
		}

		// Token: 0x06001A65 RID: 6757 RVA: 0x00063FD8 File Offset: 0x000621D8
		public override bool Fallback(char charUnknown, int index)
		{
			this._iCount = (this._iSize = 1);
			this._cBestFit = this.TryBestFit(charUnknown);
			if (this._cBestFit == '\0')
			{
				this._cBestFit = '?';
			}
			return true;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x00064014 File Offset: 0x00062214
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", SR.Format("Valid values are between {0} and {1}, inclusive.", 55296, 56319));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("charUnknownLow", SR.Format("Valid values are between {0} and {1}, inclusive.", 56320, 57343));
			}
			this._cBestFit = '?';
			this._iCount = (this._iSize = 2);
			return true;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0006409C File Offset: 0x0006229C
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

		// Token: 0x06001A68 RID: 6760 RVA: 0x000640D3 File Offset: 0x000622D3
		public override bool MovePrevious()
		{
			if (this._iCount >= 0)
			{
				this._iCount++;
			}
			return this._iCount >= 0 && this._iCount <= this._iSize;
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x00064108 File Offset: 0x00062308
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

		// Token: 0x06001A6A RID: 6762 RVA: 0x0006411B File Offset: 0x0006231B
		public override void Reset()
		{
			this._iCount = -1;
			this.charStart = null;
			this.bFallingBack = false;
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x00064134 File Offset: 0x00062334
		private char TryBestFit(char cUnknown)
		{
			int num = 0;
			int num2 = this._oFallback._arrayBestFit.Length;
			int num3;
			while ((num3 = num2 - num) > 6)
			{
				int i = (num3 / 2 + num) & 65534;
				char c = this._oFallback._arrayBestFit[i];
				if (c == cUnknown)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
				if (c < cUnknown)
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
				if (this._oFallback._arrayBestFit[i] == cUnknown)
				{
					return this._oFallback._arrayBestFit[i + 1];
				}
			}
			return '\0';
		}

		// Token: 0x04000C74 RID: 3188
		private char _cBestFit;

		// Token: 0x04000C75 RID: 3189
		private InternalEncoderBestFitFallback _oFallback;

		// Token: 0x04000C76 RID: 3190
		private int _iCount = -1;

		// Token: 0x04000C77 RID: 3191
		private int _iSize;

		// Token: 0x04000C78 RID: 3192
		private static object s_InternalSyncObject;
	}
}
