using System;

namespace System.IO.Compression
{
	// Token: 0x0200000E RID: 14
	internal sealed class FastEncoderWindow
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000051 RID: 81 RVA: 0x0000333F File Offset: 0x0000153F
		public int BytesAvailable
		{
			get
			{
				return this._bufEnd - this._bufPos;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000052 RID: 82 RVA: 0x0000334E File Offset: 0x0000154E
		public DeflateInput UnprocessedInput
		{
			get
			{
				return new DeflateInput
				{
					Buffer = this._window,
					StartIndex = this._bufPos,
					Count = this._bufEnd - this._bufPos
				};
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00003380 File Offset: 0x00001580
		public void FlushWindow()
		{
			this.ResetWindow();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00003388 File Offset: 0x00001588
		private void ResetWindow()
		{
			this._window = new byte[16646];
			this._prev = new ushort[8450];
			this._lookup = new ushort[2048];
			this._bufPos = 8192;
			this._bufEnd = this._bufPos;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000033DC File Offset: 0x000015DC
		public int FreeWindowSpace
		{
			get
			{
				return 16384 - this._bufEnd;
			}
		}

		// Token: 0x06000056 RID: 86 RVA: 0x000033EA File Offset: 0x000015EA
		public void CopyBytes(byte[] inputBuffer, int startIndex, int count)
		{
			Array.Copy(inputBuffer, startIndex, this._window, this._bufEnd, count);
			this._bufEnd += count;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003410 File Offset: 0x00001610
		public void MoveWindows()
		{
			Array.Copy(this._window, this._bufPos - 8192, this._window, 0, 8192);
			for (int i = 0; i < 2048; i++)
			{
				int num = (int)(this._lookup[i] - 8192);
				if (num <= 0)
				{
					this._lookup[i] = 0;
				}
				else
				{
					this._lookup[i] = (ushort)num;
				}
			}
			for (int i = 0; i < 8192; i++)
			{
				long num2 = (long)((ulong)this._prev[i] - 8192UL);
				if (num2 <= 0L)
				{
					this._prev[i] = 0;
				}
				else
				{
					this._prev[i] = (ushort)num2;
				}
			}
			this._bufPos = 8192;
			this._bufEnd = this._bufPos;
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000034CA File Offset: 0x000016CA
		private uint HashValue(uint hash, byte b)
		{
			return (hash << 4) ^ (uint)b;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000034D4 File Offset: 0x000016D4
		private uint InsertString(ref uint hash)
		{
			hash = this.HashValue(hash, this._window[this._bufPos + 2]);
			uint num = (uint)this._lookup[(int)(hash & 2047U)];
			this._lookup[(int)(hash & 2047U)] = (ushort)this._bufPos;
			this._prev[this._bufPos & 8191] = (ushort)num;
			return num;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00003538 File Offset: 0x00001738
		private void InsertStrings(ref uint hash, int matchLen)
		{
			if (this._bufEnd - this._bufPos <= matchLen)
			{
				this._bufPos += matchLen - 1;
				return;
			}
			while (--matchLen > 0)
			{
				this.InsertString(ref hash);
				this._bufPos++;
			}
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00003588 File Offset: 0x00001788
		internal bool GetNextSymbolOrMatch(Match match)
		{
			uint num = this.HashValue(0U, this._window[this._bufPos]);
			num = this.HashValue(num, this._window[this._bufPos + 1]);
			int num2 = 0;
			int num3;
			if (this._bufEnd - this._bufPos <= 3)
			{
				num3 = 0;
			}
			else
			{
				int num4 = (int)this.InsertString(ref num);
				if (num4 != 0)
				{
					num3 = this.FindMatch(num4, out num2, 32, 32);
					if (this._bufPos + num3 > this._bufEnd)
					{
						num3 = this._bufEnd - this._bufPos;
					}
				}
				else
				{
					num3 = 0;
				}
			}
			if (num3 < 3)
			{
				match.State = MatchState.HasSymbol;
				match.Symbol = this._window[this._bufPos];
				this._bufPos++;
			}
			else
			{
				this._bufPos++;
				if (num3 <= 6)
				{
					int num5 = 0;
					int num6 = (int)this.InsertString(ref num);
					int num7;
					if (num6 != 0)
					{
						num7 = this.FindMatch(num6, out num5, (num3 < 4) ? 32 : 8, 32);
						if (this._bufPos + num7 > this._bufEnd)
						{
							num7 = this._bufEnd - this._bufPos;
						}
					}
					else
					{
						num7 = 0;
					}
					if (num7 > num3)
					{
						match.State = MatchState.HasSymbolAndMatch;
						match.Symbol = this._window[this._bufPos - 1];
						match.Position = num5;
						match.Length = num7;
						this._bufPos++;
						num3 = num7;
						this.InsertStrings(ref num, num3);
					}
					else
					{
						match.State = MatchState.HasMatch;
						match.Position = num2;
						match.Length = num3;
						num3--;
						this._bufPos++;
						this.InsertStrings(ref num, num3);
					}
				}
				else
				{
					match.State = MatchState.HasMatch;
					match.Position = num2;
					match.Length = num3;
					this.InsertStrings(ref num, num3);
				}
			}
			if (this._bufPos == 16384)
			{
				this.MoveWindows();
			}
			return true;
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003758 File Offset: 0x00001958
		private int FindMatch(int search, out int matchPos, int searchDepth, int niceLength)
		{
			int num = 0;
			int num2 = 0;
			int num3 = this._bufPos - 8192;
			byte b = this._window[this._bufPos];
			while (search > num3)
			{
				if (this._window[search + num] == b)
				{
					int num4 = 0;
					while (num4 < 258 && this._window[this._bufPos + num4] == this._window[search + num4])
					{
						num4++;
					}
					if (num4 > num)
					{
						num = num4;
						num2 = search;
						if (num4 > 32)
						{
							break;
						}
						b = this._window[this._bufPos + num4];
					}
				}
				if (--searchDepth == 0)
				{
					break;
				}
				search = (int)this._prev[search & 8191];
			}
			matchPos = this._bufPos - num2 - 1;
			if (num == 3 && matchPos >= 16384)
			{
				return 0;
			}
			return num;
		}

		// Token: 0x0400003E RID: 62
		private byte[] _window;

		// Token: 0x0400003F RID: 63
		private int _bufPos;

		// Token: 0x04000040 RID: 64
		private int _bufEnd;

		// Token: 0x04000041 RID: 65
		private ushort[] _prev;

		// Token: 0x04000042 RID: 66
		private ushort[] _lookup;
	}
}
