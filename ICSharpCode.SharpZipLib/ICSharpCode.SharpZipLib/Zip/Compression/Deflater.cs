using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression
{
	// Token: 0x02000054 RID: 84
	public class Deflater
	{
		// Token: 0x06000299 RID: 665 RVA: 0x0000C58A File Offset: 0x0000A78A
		public Deflater()
			: this(-1, false)
		{
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000C594 File Offset: 0x0000A794
		public Deflater(int level)
			: this(level, false)
		{
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		public Deflater(int level, bool noZlibHeaderOrFooter)
		{
			if (level == -1)
			{
				level = 6;
			}
			else if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			this.pending = new DeflaterPending();
			this.engine = new DeflaterEngine(this.pending, noZlibHeaderOrFooter);
			this.noZlibHeaderOrFooter = noZlibHeaderOrFooter;
			this.SetStrategy(DeflateStrategy.Default);
			this.SetLevel(level);
			this.Reset();
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000C608 File Offset: 0x0000A808
		public void Reset()
		{
			this.state = (this.noZlibHeaderOrFooter ? 16 : 0);
			this.totalOut = 0L;
			this.pending.Reset();
			this.engine.Reset();
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000C63B File Offset: 0x0000A83B
		public int Adler
		{
			get
			{
				return this.engine.Adler;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000C648 File Offset: 0x0000A848
		public long TotalIn
		{
			get
			{
				return this.engine.TotalIn;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000C655 File Offset: 0x0000A855
		public long TotalOut
		{
			get
			{
				return this.totalOut;
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C65D File Offset: 0x0000A85D
		public void Flush()
		{
			this.state |= 4;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C66D File Offset: 0x0000A86D
		public void Finish()
		{
			this.state |= 12;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x0000C67E File Offset: 0x0000A87E
		public bool IsFinished
		{
			get
			{
				return this.state == 30 && this.pending.IsFlushed;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000C697 File Offset: 0x0000A897
		public bool IsNeedingInput
		{
			get
			{
				return this.engine.NeedsInput();
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		public void SetInput(byte[] input)
		{
			this.SetInput(input, 0, input.Length);
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C6B1 File Offset: 0x0000A8B1
		public void SetInput(byte[] input, int offset, int count)
		{
			if ((this.state & 8) != 0)
			{
				throw new InvalidOperationException("Finish() already called");
			}
			this.engine.SetInput(input, offset, count);
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000C6D6 File Offset: 0x0000A8D6
		public void SetLevel(int level)
		{
			if (level == -1)
			{
				level = 6;
			}
			else if (level < 0 || level > 9)
			{
				throw new ArgumentOutOfRangeException("level");
			}
			if (this.level != level)
			{
				this.level = level;
				this.engine.SetLevel(level);
			}
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000C711 File Offset: 0x0000A911
		public int GetLevel()
		{
			return this.level;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000C719 File Offset: 0x0000A919
		public void SetStrategy(DeflateStrategy strategy)
		{
			this.engine.Strategy = strategy;
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000C727 File Offset: 0x0000A927
		public int Deflate(byte[] output)
		{
			return this.Deflate(output, 0, output.Length);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C734 File Offset: 0x0000A934
		public int Deflate(byte[] output, int offset, int length)
		{
			int num = length;
			if (this.state == 127)
			{
				throw new InvalidOperationException("Deflater closed");
			}
			if (this.state < 16)
			{
				int num2 = 30720;
				int num3 = this.level - 1 >> 1;
				if (num3 < 0 || num3 > 3)
				{
					num3 = 3;
				}
				num2 |= num3 << 6;
				if ((this.state & 1) != 0)
				{
					num2 |= 32;
				}
				num2 += 31 - num2 % 31;
				this.pending.WriteShortMSB(num2);
				if ((this.state & 1) != 0)
				{
					int adler = this.engine.Adler;
					this.engine.ResetAdler();
					this.pending.WriteShortMSB(adler >> 16);
					this.pending.WriteShortMSB(adler & 65535);
				}
				this.state = 16 | (this.state & 12);
			}
			for (;;)
			{
				int num4 = this.pending.Flush(output, offset, length);
				offset += num4;
				this.totalOut += (long)num4;
				length -= num4;
				if (length == 0 || this.state == 30)
				{
					goto IL_01D3;
				}
				if (!this.engine.Deflate((this.state & 4) != 0, (this.state & 8) != 0))
				{
					int num5 = this.state;
					if (num5 == 16)
					{
						break;
					}
					if (num5 != 20)
					{
						if (num5 == 28)
						{
							this.pending.AlignToByte();
							if (!this.noZlibHeaderOrFooter)
							{
								int adler2 = this.engine.Adler;
								this.pending.WriteShortMSB(adler2 >> 16);
								this.pending.WriteShortMSB(adler2 & 65535);
							}
							this.state = 30;
						}
					}
					else
					{
						if (this.level != 0)
						{
							for (int i = 8 + (-this.pending.BitCount & 7); i > 0; i -= 10)
							{
								this.pending.WriteBits(2, 10);
							}
						}
						this.state = 16;
					}
				}
			}
			return num - length;
			IL_01D3:
			return num - length;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000C917 File Offset: 0x0000AB17
		public void SetDictionary(byte[] dictionary)
		{
			this.SetDictionary(dictionary, 0, dictionary.Length);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000C924 File Offset: 0x0000AB24
		public void SetDictionary(byte[] dictionary, int index, int count)
		{
			if (this.state != 0)
			{
				throw new InvalidOperationException();
			}
			this.state = 1;
			this.engine.SetDictionary(dictionary, index, count);
		}

		// Token: 0x04000199 RID: 409
		public const int BEST_COMPRESSION = 9;

		// Token: 0x0400019A RID: 410
		public const int BEST_SPEED = 1;

		// Token: 0x0400019B RID: 411
		public const int DEFAULT_COMPRESSION = -1;

		// Token: 0x0400019C RID: 412
		public const int NO_COMPRESSION = 0;

		// Token: 0x0400019D RID: 413
		public const int DEFLATED = 8;

		// Token: 0x0400019E RID: 414
		private const int IS_SETDICT = 1;

		// Token: 0x0400019F RID: 415
		private const int IS_FLUSHING = 4;

		// Token: 0x040001A0 RID: 416
		private const int IS_FINISHING = 8;

		// Token: 0x040001A1 RID: 417
		private const int INIT_STATE = 0;

		// Token: 0x040001A2 RID: 418
		private const int SETDICT_STATE = 1;

		// Token: 0x040001A3 RID: 419
		private const int BUSY_STATE = 16;

		// Token: 0x040001A4 RID: 420
		private const int FLUSHING_STATE = 20;

		// Token: 0x040001A5 RID: 421
		private const int FINISHING_STATE = 28;

		// Token: 0x040001A6 RID: 422
		private const int FINISHED_STATE = 30;

		// Token: 0x040001A7 RID: 423
		private const int CLOSED_STATE = 127;

		// Token: 0x040001A8 RID: 424
		private int level;

		// Token: 0x040001A9 RID: 425
		private bool noZlibHeaderOrFooter;

		// Token: 0x040001AA RID: 426
		private int state;

		// Token: 0x040001AB RID: 427
		private long totalOut;

		// Token: 0x040001AC RID: 428
		private DeflaterPending pending;

		// Token: 0x040001AD RID: 429
		private DeflaterEngine engine;

		// Token: 0x02000055 RID: 85
		public enum CompressionLevel
		{
			// Token: 0x040001AF RID: 431
			BEST_COMPRESSION = 9,
			// Token: 0x040001B0 RID: 432
			BEST_SPEED = 1,
			// Token: 0x040001B1 RID: 433
			DEFAULT_COMPRESSION = -1,
			// Token: 0x040001B2 RID: 434
			NO_COMPRESSION,
			// Token: 0x040001B3 RID: 435
			DEFLATED = 8
		}
	}
}
