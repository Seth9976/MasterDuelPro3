using System;

namespace Ionic.Zlib
{
	// Token: 0x0200005A RID: 90
	internal sealed class InflateManager
	{
		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x0001ABAB File Offset: 0x00018DAB
		// (set) Token: 0x0600043C RID: 1084 RVA: 0x0001ABB3 File Offset: 0x00018DB3
		internal bool HandleRfc1950HeaderBytes
		{
			get
			{
				return this._handleRfc1950HeaderBytes;
			}
			set
			{
				this._handleRfc1950HeaderBytes = value;
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x0001ABBC File Offset: 0x00018DBC
		public InflateManager()
		{
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001ABCB File Offset: 0x00018DCB
		public InflateManager(bool expectRfc1950HeaderBytes)
		{
			this._handleRfc1950HeaderBytes = expectRfc1950HeaderBytes;
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001ABE4 File Offset: 0x00018DE4
		internal int Reset()
		{
			this._codec.TotalBytesIn = (this._codec.TotalBytesOut = 0L);
			this._codec.Message = null;
			this.mode = (this.HandleRfc1950HeaderBytes ? InflateManager.InflateManagerMode.METHOD : InflateManager.InflateManagerMode.BLOCKS);
			this.blocks.Reset();
			return 0;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001AC37 File Offset: 0x00018E37
		internal int End()
		{
			if (this.blocks != null)
			{
				this.blocks.Free();
			}
			this.blocks = null;
			return 0;
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001AC54 File Offset: 0x00018E54
		internal int Initialize(ZlibCodec codec, int w)
		{
			this._codec = codec;
			this._codec.Message = null;
			this.blocks = null;
			if (w < 8 || w > 15)
			{
				this.End();
				throw new ZlibException("Bad window size.");
			}
			this.wbits = w;
			this.blocks = new InflateBlocks(codec, this.HandleRfc1950HeaderBytes ? this : null, 1 << w);
			this.Reset();
			return 0;
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x0001ACC4 File Offset: 0x00018EC4
		internal int Inflate(FlushType flush)
		{
			if (this._codec.InputBuffer == null)
			{
				throw new ZlibException("InputBuffer is null. ");
			}
			int num = 0;
			int num2 = -5;
			for (;;)
			{
				switch (this.mode)
				{
				case InflateManager.InflateManagerMode.METHOD:
					if (this._codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					if (((this.method = (int)this._codec.InputBuffer[this._codec.NextIn++]) & 15) != 8)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = string.Format("unknown compression method (0x{0:X2})", this.method);
						this.marker = 5;
						continue;
					}
					if ((this.method >> 4) + 8 > this.wbits)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = string.Format("invalid window size ({0})", (this.method >> 4) + 8);
						this.marker = 5;
						continue;
					}
					this.mode = InflateManager.InflateManagerMode.FLAG;
					continue;
				case InflateManager.InflateManagerMode.FLAG:
				{
					if (this._codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					int num3 = (int)(this._codec.InputBuffer[this._codec.NextIn++] & byte.MaxValue);
					if (((this.method << 8) + num3) % 31 != 0)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = "incorrect header check";
						this.marker = 5;
						continue;
					}
					this.mode = (((num3 & 32) == 0) ? InflateManager.InflateManagerMode.BLOCKS : InflateManager.InflateManagerMode.DICT4);
					continue;
				}
				case InflateManager.InflateManagerMode.DICT4:
					if (this._codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					this.expectedCheck = (uint)((long)((long)this._codec.InputBuffer[this._codec.NextIn++] << 24) & (long)((ulong)(-16777216)));
					this.mode = InflateManager.InflateManagerMode.DICT3;
					continue;
				case InflateManager.InflateManagerMode.DICT3:
					if (this._codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					this.expectedCheck += (uint)(((int)this._codec.InputBuffer[this._codec.NextIn++] << 16) & 16711680);
					this.mode = InflateManager.InflateManagerMode.DICT2;
					continue;
				case InflateManager.InflateManagerMode.DICT2:
					if (this._codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					this.expectedCheck += (uint)(((int)this._codec.InputBuffer[this._codec.NextIn++] << 8) & 65280);
					this.mode = InflateManager.InflateManagerMode.DICT1;
					continue;
				case InflateManager.InflateManagerMode.DICT1:
					goto IL_037E;
				case InflateManager.InflateManagerMode.DICT0:
					goto IL_0407;
				case InflateManager.InflateManagerMode.BLOCKS:
					num2 = this.blocks.Process(num2);
					if (num2 == -3)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this.marker = 0;
						continue;
					}
					if (num2 == 0)
					{
						num2 = num;
					}
					if (num2 != 1)
					{
						return num2;
					}
					num2 = num;
					this.computedCheck = this.blocks.Reset();
					if (!this.HandleRfc1950HeaderBytes)
					{
						goto Block_16;
					}
					this.mode = InflateManager.InflateManagerMode.CHECK4;
					continue;
				case InflateManager.InflateManagerMode.CHECK4:
					if (this._codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					this.expectedCheck = (uint)((long)((long)this._codec.InputBuffer[this._codec.NextIn++] << 24) & (long)((ulong)(-16777216)));
					this.mode = InflateManager.InflateManagerMode.CHECK3;
					continue;
				case InflateManager.InflateManagerMode.CHECK3:
					if (this._codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					this.expectedCheck += (uint)(((int)this._codec.InputBuffer[this._codec.NextIn++] << 16) & 16711680);
					this.mode = InflateManager.InflateManagerMode.CHECK2;
					continue;
				case InflateManager.InflateManagerMode.CHECK2:
					if (this._codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					this.expectedCheck += (uint)(((int)this._codec.InputBuffer[this._codec.NextIn++] << 8) & 65280);
					this.mode = InflateManager.InflateManagerMode.CHECK1;
					continue;
				case InflateManager.InflateManagerMode.CHECK1:
					if (this._codec.AvailableBytesIn == 0)
					{
						return num2;
					}
					num2 = num;
					this._codec.AvailableBytesIn--;
					this._codec.TotalBytesIn += 1L;
					this.expectedCheck += (uint)(this._codec.InputBuffer[this._codec.NextIn++] & byte.MaxValue);
					if (this.computedCheck != this.expectedCheck)
					{
						this.mode = InflateManager.InflateManagerMode.BAD;
						this._codec.Message = "incorrect data check";
						this.marker = 5;
						continue;
					}
					goto IL_06A4;
				case InflateManager.InflateManagerMode.DONE:
					return 1;
				case InflateManager.InflateManagerMode.BAD:
					goto IL_06B0;
				}
				break;
			}
			throw new ZlibException("Stream error.");
			IL_037E:
			if (this._codec.AvailableBytesIn == 0)
			{
				return num2;
			}
			this._codec.AvailableBytesIn--;
			this._codec.TotalBytesIn += 1L;
			this.expectedCheck += (uint)(this._codec.InputBuffer[this._codec.NextIn++] & byte.MaxValue);
			this._codec._Adler32 = this.expectedCheck;
			this.mode = InflateManager.InflateManagerMode.DICT0;
			return 2;
			IL_0407:
			this.mode = InflateManager.InflateManagerMode.BAD;
			this._codec.Message = "need dictionary";
			this.marker = 0;
			return -2;
			Block_16:
			this.mode = InflateManager.InflateManagerMode.DONE;
			return 1;
			IL_06A4:
			this.mode = InflateManager.InflateManagerMode.DONE;
			return 1;
			IL_06B0:
			throw new ZlibException(string.Format("Bad state ({0})", this._codec.Message));
		}

		// Token: 0x06000443 RID: 1091 RVA: 0x0001B3A8 File Offset: 0x000195A8
		internal int SetDictionary(byte[] dictionary)
		{
			int num = 0;
			int num2 = dictionary.Length;
			if (this.mode != InflateManager.InflateManagerMode.DICT0)
			{
				throw new ZlibException("Stream error.");
			}
			if (Adler.Adler32(1U, dictionary, 0, dictionary.Length) != this._codec._Adler32)
			{
				return -3;
			}
			this._codec._Adler32 = Adler.Adler32(0U, null, 0, 0);
			if (num2 >= 1 << this.wbits)
			{
				num2 = (1 << this.wbits) - 1;
				num = dictionary.Length - num2;
			}
			this.blocks.SetDictionary(dictionary, num, num2);
			this.mode = InflateManager.InflateManagerMode.BLOCKS;
			return 0;
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x0001B438 File Offset: 0x00019638
		internal int Sync()
		{
			if (this.mode != InflateManager.InflateManagerMode.BAD)
			{
				this.mode = InflateManager.InflateManagerMode.BAD;
				this.marker = 0;
			}
			int num;
			if ((num = this._codec.AvailableBytesIn) == 0)
			{
				return -5;
			}
			int num2 = this._codec.NextIn;
			int num3 = this.marker;
			while (num != 0 && num3 < 4)
			{
				if (this._codec.InputBuffer[num2] == InflateManager.mark[num3])
				{
					num3++;
				}
				else if (this._codec.InputBuffer[num2] != 0)
				{
					num3 = 0;
				}
				else
				{
					num3 = 4 - num3;
				}
				num2++;
				num--;
			}
			this._codec.TotalBytesIn += (long)(num2 - this._codec.NextIn);
			this._codec.NextIn = num2;
			this._codec.AvailableBytesIn = num;
			this.marker = num3;
			if (num3 != 4)
			{
				return -3;
			}
			long totalBytesIn = this._codec.TotalBytesIn;
			long totalBytesOut = this._codec.TotalBytesOut;
			this.Reset();
			this._codec.TotalBytesIn = totalBytesIn;
			this._codec.TotalBytesOut = totalBytesOut;
			this.mode = InflateManager.InflateManagerMode.BLOCKS;
			return 0;
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x0001B54E File Offset: 0x0001974E
		internal int SyncPoint(ZlibCodec z)
		{
			return this.blocks.SyncPoint();
		}

		// Token: 0x040002F7 RID: 759
		private const int PRESET_DICT = 32;

		// Token: 0x040002F8 RID: 760
		private const int Z_DEFLATED = 8;

		// Token: 0x040002F9 RID: 761
		private InflateManager.InflateManagerMode mode;

		// Token: 0x040002FA RID: 762
		internal ZlibCodec _codec;

		// Token: 0x040002FB RID: 763
		internal int method;

		// Token: 0x040002FC RID: 764
		internal uint computedCheck;

		// Token: 0x040002FD RID: 765
		internal uint expectedCheck;

		// Token: 0x040002FE RID: 766
		internal int marker;

		// Token: 0x040002FF RID: 767
		private bool _handleRfc1950HeaderBytes = true;

		// Token: 0x04000300 RID: 768
		internal int wbits;

		// Token: 0x04000301 RID: 769
		internal InflateBlocks blocks;

		// Token: 0x04000302 RID: 770
		private static readonly byte[] mark = new byte[]
		{
			default(byte),
			default(byte),
			byte.MaxValue,
			byte.MaxValue
		};

		// Token: 0x0200005B RID: 91
		private enum InflateManagerMode
		{
			// Token: 0x04000304 RID: 772
			METHOD,
			// Token: 0x04000305 RID: 773
			FLAG,
			// Token: 0x04000306 RID: 774
			DICT4,
			// Token: 0x04000307 RID: 775
			DICT3,
			// Token: 0x04000308 RID: 776
			DICT2,
			// Token: 0x04000309 RID: 777
			DICT1,
			// Token: 0x0400030A RID: 778
			DICT0,
			// Token: 0x0400030B RID: 779
			BLOCKS,
			// Token: 0x0400030C RID: 780
			CHECK4,
			// Token: 0x0400030D RID: 781
			CHECK3,
			// Token: 0x0400030E RID: 782
			CHECK2,
			// Token: 0x0400030F RID: 783
			CHECK1,
			// Token: 0x04000310 RID: 784
			DONE,
			// Token: 0x04000311 RID: 785
			BAD
		}
	}
}
