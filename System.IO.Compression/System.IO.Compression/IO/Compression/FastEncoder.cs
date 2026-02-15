using System;

namespace System.IO.Compression
{
	// Token: 0x0200000C RID: 12
	internal sealed class FastEncoder
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002EAF File Offset: 0x000010AF
		internal int BytesInHistory
		{
			get
			{
				return this._inputWindow.BytesAvailable;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002EBC File Offset: 0x000010BC
		internal DeflateInput UnprocessedInput
		{
			get
			{
				return this._inputWindow.UnprocessedInput;
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002EC9 File Offset: 0x000010C9
		internal void FlushInput()
		{
			this._inputWindow.FlushWindow();
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002ED6 File Offset: 0x000010D6
		internal double LastCompressionRatio
		{
			get
			{
				return this._lastCompressionRatio;
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002EDE File Offset: 0x000010DE
		internal void GetBlock(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			FastEncoder.WriteDeflatePreamble(output);
			this.GetCompressedOutput(input, output, maxBytesToCopy);
			this.WriteEndOfBlock(output);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002EF6 File Offset: 0x000010F6
		internal void GetCompressedData(DeflateInput input, OutputBuffer output)
		{
			this.GetCompressedOutput(input, output, -1);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002F01 File Offset: 0x00001101
		internal void GetBlockHeader(OutputBuffer output)
		{
			FastEncoder.WriteDeflatePreamble(output);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002F09 File Offset: 0x00001109
		internal void GetBlockFooter(OutputBuffer output)
		{
			this.WriteEndOfBlock(output);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002F14 File Offset: 0x00001114
		private void GetCompressedOutput(DeflateInput input, OutputBuffer output, int maxBytesToCopy)
		{
			int bytesWritten = output.BytesWritten;
			int num = 0;
			int num2 = this.BytesInHistory + input.Count;
			do
			{
				int num3 = ((input.Count < this._inputWindow.FreeWindowSpace) ? input.Count : this._inputWindow.FreeWindowSpace);
				if (maxBytesToCopy >= 1)
				{
					num3 = Math.Min(num3, maxBytesToCopy - num);
				}
				if (num3 > 0)
				{
					this._inputWindow.CopyBytes(input.Buffer, input.StartIndex, num3);
					input.ConsumeBytes(num3);
					num += num3;
				}
				this.GetCompressedOutput(output);
			}
			while (this.SafeToWriteTo(output) && this.InputAvailable(input) && (maxBytesToCopy < 1 || num < maxBytesToCopy));
			int num4 = output.BytesWritten - bytesWritten;
			int num5 = this.BytesInHistory + input.Count;
			int num6 = num2 - num5;
			if (num4 != 0)
			{
				this._lastCompressionRatio = (double)num4 / (double)num6;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002FEC File Offset: 0x000011EC
		private void GetCompressedOutput(OutputBuffer output)
		{
			while (this._inputWindow.BytesAvailable > 0 && this.SafeToWriteTo(output))
			{
				this._inputWindow.GetNextSymbolOrMatch(this._currentMatch);
				if (this._currentMatch.State == MatchState.HasSymbol)
				{
					FastEncoder.WriteChar(this._currentMatch.Symbol, output);
				}
				else if (this._currentMatch.State == MatchState.HasMatch)
				{
					FastEncoder.WriteMatch(this._currentMatch.Length, this._currentMatch.Position, output);
				}
				else
				{
					FastEncoder.WriteChar(this._currentMatch.Symbol, output);
					FastEncoder.WriteMatch(this._currentMatch.Length, this._currentMatch.Position, output);
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000030A4 File Offset: 0x000012A4
		private bool InputAvailable(DeflateInput input)
		{
			return input.Count > 0 || this.BytesInHistory > 0;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000030BA File Offset: 0x000012BA
		private bool SafeToWriteTo(OutputBuffer output)
		{
			return output.FreeBytes > 16;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000030C8 File Offset: 0x000012C8
		private void WriteEndOfBlock(OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[256];
			int num2 = (int)(num & 31U);
			output.WriteBits(num2, num >> 5);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000030F0 File Offset: 0x000012F0
		internal static void WriteMatch(int matchLen, int matchPos, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[254 + matchLen];
			int num2 = (int)(num & 31U);
			if (num2 <= 16)
			{
				output.WriteBits(num2, num >> 5);
			}
			else
			{
				output.WriteBits(16, (num >> 5) & 65535U);
				output.WriteBits(num2 - 16, num >> 21);
			}
			num = FastEncoderStatics.FastEncoderDistanceCodeInfo[FastEncoderStatics.GetSlot(matchPos)];
			output.WriteBits((int)(num & 15U), num >> 8);
			int num3 = (int)((num >> 4) & 15U);
			if (num3 != 0)
			{
				output.WriteBits(num3, (uint)(matchPos & (int)FastEncoderStatics.BitMask[num3]));
			}
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003174 File Offset: 0x00001374
		internal static void WriteChar(byte b, OutputBuffer output)
		{
			uint num = FastEncoderStatics.FastEncoderLiteralCodeInfo[(int)b];
			output.WriteBits((int)(num & 31U), num >> 5);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003196 File Offset: 0x00001396
		internal static void WriteDeflatePreamble(OutputBuffer output)
		{
			output.WriteBytes(FastEncoderStatics.FastEncoderTreeStructureData, 0, FastEncoderStatics.FastEncoderTreeStructureData.Length);
			output.WriteBits(9, 34U);
		}

		// Token: 0x04000033 RID: 51
		private readonly FastEncoderWindow _inputWindow;

		// Token: 0x04000034 RID: 52
		private readonly Match _currentMatch;

		// Token: 0x04000035 RID: 53
		private double _lastCompressionRatio;
	}
}
