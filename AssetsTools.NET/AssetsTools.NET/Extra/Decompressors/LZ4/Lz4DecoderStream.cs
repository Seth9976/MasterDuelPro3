using System;
using System.IO;

namespace AssetsTools.NET.Extra.Decompressors.LZ4
{
	// Token: 0x0200008B RID: 139
	public class Lz4DecoderStream : Stream
	{
		// Token: 0x06000514 RID: 1300 RVA: 0x0001C242 File Offset: 0x0001A442
		public Lz4DecoderStream(Stream input, long inputLength = 9223372036854775807L)
		{
			this.Reset(input, inputLength);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x0001C268 File Offset: 0x0001A468
		public void Reset(Stream input, long inputLength = 9223372036854775807L)
		{
			this.inputLength = inputLength;
			this.input = input;
			this.phase = Lz4DecoderStream.DecodePhase.ReadToken;
			this.decodeBufferPos = 0;
			this.litLen = 0;
			this.matLen = 0;
			this.matDst = 0;
			this.inBufPos = 65536;
			this.inBufEnd = 65536;
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x0001C2BD File Offset: 0x0001A4BD
		public override void Close()
		{
			this.input = null;
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x0001C2C8 File Offset: 0x0001A4C8
		public override int Read(byte[] buffer, int offset, int count)
		{
			bool flag = buffer == null;
			if (flag)
			{
				throw new ArgumentNullException("buffer");
			}
			bool flag2 = offset < 0 || count < 0 || buffer.Length - count < offset;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException();
			}
			bool flag3 = this.input == null;
			if (flag3)
			{
				throw new InvalidOperationException();
			}
			int num = count;
			byte[] array = this.decodeBuffer;
			switch (this.phase)
			{
			case Lz4DecoderStream.DecodePhase.ReadToken:
				goto IL_009B;
			case Lz4DecoderStream.DecodePhase.ReadExLiteralLength:
				break;
			case Lz4DecoderStream.DecodePhase.CopyLiteral:
				goto IL_01A4;
			case Lz4DecoderStream.DecodePhase.ReadOffset:
				goto IL_0281;
			case Lz4DecoderStream.DecodePhase.ReadExMatchLength:
				goto IL_030B;
			case Lz4DecoderStream.DecodePhase.CopyMatch:
				goto IL_037F;
			default:
				goto IL_009B;
			}
			for (;;)
			{
				IL_0130:
				bool flag4 = this.inBufPos < this.inBufEnd;
				int num3;
				if (flag4)
				{
					byte[] array2 = array;
					int num2 = this.inBufPos;
					this.inBufPos = num2 + 1;
					num3 = array2[num2];
				}
				else
				{
					num3 = this.ReadByteCore();
					bool flag5 = num3 == -1;
					if (flag5)
					{
						break;
					}
				}
				this.litLen += num3;
				bool flag6 = num3 == 255;
				if (!flag6)
				{
					goto IL_019B;
				}
			}
			goto IL_0480;
			IL_019B:
			this.phase = Lz4DecoderStream.DecodePhase.CopyLiteral;
			int num8;
			for (;;)
			{
				IL_01A4:
				int num4 = ((this.litLen < num) ? this.litLen : num);
				bool flag7 = num4 != 0;
				if (!flag7)
				{
					goto IL_0269;
				}
				bool flag8 = this.inBufPos + num4 <= this.inBufEnd;
				if (flag8)
				{
					int num5 = offset;
					int num6 = num4;
					while (num6-- != 0)
					{
						int num7 = num5++;
						byte[] array3 = array;
						int num2 = this.inBufPos;
						this.inBufPos = num2 + 1;
						buffer[num7] = array3[num2];
					}
					num8 = num4;
				}
				else
				{
					num8 = this.ReadCore(buffer, offset, num4);
					bool flag9 = num8 == 0;
					if (flag9)
					{
						break;
					}
				}
				offset += num8;
				num -= num8;
				this.litLen -= num8;
				bool flag10 = this.litLen != 0;
				if (!flag10)
				{
					goto IL_0268;
				}
			}
			goto IL_0480;
			IL_0268:
			IL_0269:
			bool flag11 = num == 0;
			if (flag11)
			{
				goto IL_0480;
			}
			this.phase = Lz4DecoderStream.DecodePhase.ReadOffset;
			goto IL_0281;
			for (;;)
			{
				IL_030B:
				bool flag12 = this.inBufPos < this.inBufEnd;
				int num9;
				if (flag12)
				{
					byte[] array4 = array;
					int num2 = this.inBufPos;
					this.inBufPos = num2 + 1;
					num9 = array4[num2];
				}
				else
				{
					num9 = this.ReadByteCore();
					bool flag13 = num9 == -1;
					if (flag13)
					{
						break;
					}
				}
				this.matLen += num9;
				bool flag14 = num9 == 255;
				if (!flag14)
				{
					goto IL_0376;
				}
			}
			goto IL_0480;
			IL_0376:
			this.phase = Lz4DecoderStream.DecodePhase.CopyMatch;
			goto IL_037F;
			IL_009B:
			bool flag15 = this.inBufPos < this.inBufEnd;
			int num10;
			if (flag15)
			{
				byte[] array5 = array;
				int num2 = this.inBufPos;
				this.inBufPos = num2 + 1;
				num10 = array5[num2];
			}
			else
			{
				num10 = this.ReadByteCore();
				bool flag16 = num10 == -1;
				if (flag16)
				{
					goto IL_0480;
				}
			}
			this.litLen = num10 >> 4;
			this.matLen = (num10 & 15) + 4;
			int num11 = this.litLen;
			int num12 = num11;
			if (num12 != 0)
			{
				if (num12 != 15)
				{
					this.phase = Lz4DecoderStream.DecodePhase.CopyLiteral;
					goto IL_01A4;
				}
				this.phase = Lz4DecoderStream.DecodePhase.ReadExLiteralLength;
				goto IL_0130;
			}
			else
			{
				this.phase = Lz4DecoderStream.DecodePhase.ReadOffset;
			}
			IL_0281:
			bool flag17 = this.inBufPos + 1 < this.inBufEnd;
			if (flag17)
			{
				this.matDst = ((int)array[this.inBufPos + 1] << 8) | (int)array[this.inBufPos];
				this.inBufPos += 2;
			}
			else
			{
				this.matDst = this.ReadOffsetCore();
				bool flag18 = this.matDst == -1;
				if (flag18)
				{
					goto IL_0480;
				}
			}
			bool flag19 = this.matLen == 19;
			if (flag19)
			{
				this.phase = Lz4DecoderStream.DecodePhase.ReadExMatchLength;
				goto IL_030B;
			}
			this.phase = Lz4DecoderStream.DecodePhase.CopyMatch;
			IL_037F:
			int num13 = ((this.matLen < num) ? this.matLen : num);
			bool flag20 = num13 != 0;
			if (flag20)
			{
				num8 = count - num;
				int num14 = this.matDst - num8;
				bool flag21 = num14 > 0;
				if (flag21)
				{
					int num15 = this.decodeBufferPos - num14;
					bool flag22 = num15 < 0;
					if (flag22)
					{
						num15 += 65536;
					}
					int num16 = ((num14 < num13) ? num14 : num13);
					int num17 = num16;
					while (num17-- != 0)
					{
						buffer[offset++] = array[num15++ & 65535];
					}
				}
				else
				{
					num14 = 0;
				}
				int num18 = offset - this.matDst;
				for (int i = num14; i < num13; i++)
				{
					buffer[offset++] = buffer[num18++];
				}
				num -= num13;
				this.matLen -= num13;
			}
			bool flag23 = num == 0;
			if (!flag23)
			{
				this.phase = Lz4DecoderStream.DecodePhase.ReadToken;
				goto IL_009B;
			}
			IL_0480:
			num8 = count - num;
			int num19 = ((num8 < 65536) ? num8 : 65536);
			int num20 = offset - num19;
			bool flag24 = num19 == 65536;
			if (flag24)
			{
				Buffer.BlockCopy(buffer, num20, array, 0, 65536);
				this.decodeBufferPos = 0;
			}
			else
			{
				int num21 = this.decodeBufferPos;
				while (num19-- != 0)
				{
					array[num21++ & 65535] = buffer[num20++];
				}
				this.decodeBufferPos = num21 & 65535;
			}
			return num8;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		private int ReadByteCore()
		{
			byte[] array = this.decodeBuffer;
			bool flag = this.inBufPos == this.inBufEnd;
			if (flag)
			{
				int num = this.input.Read(array, 65536, (128L < this.inputLength) ? 128 : ((int)this.inputLength));
				bool flag2 = num == 0;
				if (flag2)
				{
					return -1;
				}
				this.inputLength -= (long)num;
				this.inBufPos = 65536;
				this.inBufEnd = 65536 + num;
			}
			byte[] array2 = array;
			int num2 = this.inBufPos;
			this.inBufPos = num2 + 1;
			return array2[num2];
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x0001C890 File Offset: 0x0001AA90
		private int ReadOffsetCore()
		{
			byte[] array = this.decodeBuffer;
			bool flag = this.inBufPos == this.inBufEnd;
			if (flag)
			{
				int num = this.input.Read(array, 65536, (128L < this.inputLength) ? 128 : ((int)this.inputLength));
				bool flag2 = num == 0;
				if (flag2)
				{
					return -1;
				}
				this.inputLength -= (long)num;
				this.inBufPos = 65536;
				this.inBufEnd = 65536 + num;
			}
			bool flag3 = this.inBufEnd - this.inBufPos == 1;
			if (flag3)
			{
				array[65536] = array[this.inBufPos];
				int num2 = this.input.Read(array, 65537, (127L < this.inputLength) ? 127 : ((int)this.inputLength));
				bool flag4 = num2 == 0;
				if (flag4)
				{
					this.inBufPos = 65536;
					this.inBufEnd = 65537;
					return -1;
				}
				this.inputLength -= (long)num2;
				this.inBufPos = 65536;
				this.inBufEnd = 65536 + num2 + 1;
			}
			int num3 = ((int)array[this.inBufPos + 1] << 8) | (int)array[this.inBufPos];
			this.inBufPos += 2;
			return num3;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x0001C9F0 File Offset: 0x0001ABF0
		private int ReadCore(byte[] buffer, int offset, int count)
		{
			int num = count;
			byte[] array = this.decodeBuffer;
			int num2 = this.inBufEnd - this.inBufPos;
			int num3 = ((num < num2) ? num : num2);
			bool flag = num3 != 0;
			if (flag)
			{
				int num4 = this.inBufPos;
				int num5 = num3;
				while (num5-- != 0)
				{
					buffer[offset++] = array[num4++];
				}
				this.inBufPos = num4;
				num -= num3;
			}
			bool flag2 = num != 0;
			if (flag2)
			{
				bool flag3 = num >= 128;
				int num6;
				if (flag3)
				{
					num6 = this.input.Read(buffer, offset, ((long)num < this.inputLength) ? num : ((int)this.inputLength));
					num -= num6;
				}
				else
				{
					num6 = this.input.Read(array, 65536, (128L < this.inputLength) ? 128 : ((int)this.inputLength));
					this.inBufPos = 65536;
					this.inBufEnd = 65536 + num6;
					num3 = ((num < num6) ? num : num6);
					int num7 = this.inBufPos;
					int num8 = num3;
					while (num8-- != 0)
					{
						buffer[offset++] = array[num7++];
					}
					this.inBufPos = num7;
					num -= num3;
				}
				this.inputLength -= (long)num6;
			}
			return count - num;
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x0001CB5B File Offset: 0x0001AD5B
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0001CB5E File Offset: 0x0001AD5E
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001CB5E File Offset: 0x0001AD5E
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0001174B File Offset: 0x0000F94B
		public override void Flush()
		{
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00017925 File Offset: 0x00015B25
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00017925 File Offset: 0x00015B25
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x00017925 File Offset: 0x00015B25
		public override long Position
		{
			get
			{
				throw new NotSupportedException();
			}
			set
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x00017925 File Offset: 0x00015B25
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00017925 File Offset: 0x00015B25
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x00017925 File Offset: 0x00015B25
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0400042C RID: 1068
		private long inputLength;

		// Token: 0x0400042D RID: 1069
		private Stream input;

		// Token: 0x0400042E RID: 1070
		private const int DecBufLen = 65536;

		// Token: 0x0400042F RID: 1071
		private const int DecBufMask = 65535;

		// Token: 0x04000430 RID: 1072
		private const int InBufLen = 128;

		// Token: 0x04000431 RID: 1073
		private byte[] decodeBuffer = new byte[65664];

		// Token: 0x04000432 RID: 1074
		private int decodeBufferPos;

		// Token: 0x04000433 RID: 1075
		private int inBufPos;

		// Token: 0x04000434 RID: 1076
		private int inBufEnd;

		// Token: 0x04000435 RID: 1077
		private Lz4DecoderStream.DecodePhase phase;

		// Token: 0x04000436 RID: 1078
		private int litLen;

		// Token: 0x04000437 RID: 1079
		private int matLen;

		// Token: 0x04000438 RID: 1080
		private int matDst;

		// Token: 0x0200008C RID: 140
		private enum DecodePhase
		{
			// Token: 0x0400043A RID: 1082
			ReadToken,
			// Token: 0x0400043B RID: 1083
			ReadExLiteralLength,
			// Token: 0x0400043C RID: 1084
			CopyLiteral,
			// Token: 0x0400043D RID: 1085
			ReadOffset,
			// Token: 0x0400043E RID: 1086
			ReadExMatchLength,
			// Token: 0x0400043F RID: 1087
			CopyMatch
		}
	}
}
