using System;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000068 RID: 104
	public class OutputWindow
	{
		// Token: 0x06000360 RID: 864 RVA: 0x000110B0 File Offset: 0x0000F2B0
		public void Write(int value)
		{
			int num = this.windowFilled;
			this.windowFilled = num + 1;
			if (num == 32768)
			{
				throw new InvalidOperationException("Window full");
			}
			byte[] array = this.window;
			num = this.windowEnd;
			this.windowEnd = num + 1;
			array[num] = (byte)value;
			this.windowEnd &= 32767;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0001110C File Offset: 0x0000F30C
		private void SlowRepeat(int repStart, int length, int distance)
		{
			while (length-- > 0)
			{
				byte[] array = this.window;
				int num = this.windowEnd;
				this.windowEnd = num + 1;
				array[num] = this.window[repStart++];
				this.windowEnd &= 32767;
				repStart &= 32767;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x00011164 File Offset: 0x0000F364
		public void Repeat(int length, int distance)
		{
			if ((this.windowFilled += length) > 32768)
			{
				throw new InvalidOperationException("Window full");
			}
			int num = (this.windowEnd - distance) & 32767;
			int num2 = 32768 - length;
			if (num > num2 || this.windowEnd >= num2)
			{
				this.SlowRepeat(num, length, distance);
				return;
			}
			if (length <= distance)
			{
				Array.Copy(this.window, num, this.window, this.windowEnd, length);
				this.windowEnd += length;
				return;
			}
			while (length-- > 0)
			{
				byte[] array = this.window;
				int num3 = this.windowEnd;
				this.windowEnd = num3 + 1;
				array[num3] = this.window[num++];
			}
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001121C File Offset: 0x0000F41C
		public int CopyStored(StreamManipulator input, int length)
		{
			length = Math.Min(Math.Min(length, 32768 - this.windowFilled), input.AvailableBytes);
			int num = 32768 - this.windowEnd;
			int num2;
			if (length > num)
			{
				num2 = input.CopyBytes(this.window, this.windowEnd, num);
				if (num2 == num)
				{
					num2 += input.CopyBytes(this.window, 0, length - num);
				}
			}
			else
			{
				num2 = input.CopyBytes(this.window, this.windowEnd, length);
			}
			this.windowEnd = (this.windowEnd + num2) & 32767;
			this.windowFilled += num2;
			return num2;
		}

		// Token: 0x06000364 RID: 868 RVA: 0x000112C0 File Offset: 0x0000F4C0
		public void CopyDict(byte[] dictionary, int offset, int length)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (this.windowFilled > 0)
			{
				throw new InvalidOperationException();
			}
			if (length > 32768)
			{
				offset += length - 32768;
				length = 32768;
			}
			Array.Copy(dictionary, offset, this.window, 0, length);
			this.windowEnd = length & 32767;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00011320 File Offset: 0x0000F520
		public int GetFreeSpace()
		{
			return 32768 - this.windowFilled;
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001132E File Offset: 0x0000F52E
		public int GetAvailable()
		{
			return this.windowFilled;
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00011338 File Offset: 0x0000F538
		public int CopyOutput(byte[] output, int offset, int len)
		{
			int num = this.windowEnd;
			if (len > this.windowFilled)
			{
				len = this.windowFilled;
			}
			else
			{
				num = (this.windowEnd - this.windowFilled + len) & 32767;
			}
			int num2 = len;
			int num3 = len - num;
			if (num3 > 0)
			{
				Array.Copy(this.window, 32768 - num3, output, offset, num3);
				offset += num3;
				len = num;
			}
			Array.Copy(this.window, num - len, output, offset, len);
			this.windowFilled -= num2;
			if (this.windowFilled < 0)
			{
				throw new InvalidOperationException();
			}
			return num2;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000113CC File Offset: 0x0000F5CC
		public void Reset()
		{
			this.windowFilled = (this.windowEnd = 0);
		}

		// Token: 0x04000278 RID: 632
		private const int WindowSize = 32768;

		// Token: 0x04000279 RID: 633
		private const int WindowMask = 32767;

		// Token: 0x0400027A RID: 634
		private byte[] window = new byte[32768];

		// Token: 0x0400027B RID: 635
		private int windowEnd;

		// Token: 0x0400027C RID: 636
		private int windowFilled;
	}
}
