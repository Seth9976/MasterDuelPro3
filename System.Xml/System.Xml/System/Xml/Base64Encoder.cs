using System;
using System.Threading.Tasks;

namespace System.Xml
{
	// Token: 0x0200000A RID: 10
	internal abstract class Base64Encoder
	{
		// Token: 0x0600001E RID: 30 RVA: 0x0000260B File Offset: 0x0000080B
		internal Base64Encoder()
		{
			this.charsLine = new char[76];
		}

		// Token: 0x0600001F RID: 31
		internal abstract void WriteChars(char[] chars, int index, int count);

		// Token: 0x06000020 RID: 32 RVA: 0x00002620 File Offset: 0x00000820
		internal void Encode(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.leftOverBytesCount > 0)
			{
				int num = this.leftOverBytesCount;
				while (num < 3 && count > 0)
				{
					this.leftOverBytes[num++] = buffer[index++];
					count--;
				}
				if (count == 0 && num < 3)
				{
					this.leftOverBytesCount = num;
					return;
				}
				int num2 = Convert.ToBase64CharArray(this.leftOverBytes, 0, 3, this.charsLine, 0);
				this.WriteChars(this.charsLine, 0, num2);
			}
			this.leftOverBytesCount = count % 3;
			if (this.leftOverBytesCount > 0)
			{
				count -= this.leftOverBytesCount;
				if (this.leftOverBytes == null)
				{
					this.leftOverBytes = new byte[3];
				}
				for (int i = 0; i < this.leftOverBytesCount; i++)
				{
					this.leftOverBytes[i] = buffer[index + count + i];
				}
			}
			int num3 = index + count;
			int num4 = 57;
			while (index < num3)
			{
				if (index + num4 > num3)
				{
					num4 = num3 - index;
				}
				int num5 = Convert.ToBase64CharArray(buffer, index, num4, this.charsLine, 0);
				this.WriteChars(this.charsLine, 0, num5);
				index += num4;
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002764 File Offset: 0x00000964
		internal void Flush()
		{
			if (this.leftOverBytesCount > 0)
			{
				int num = Convert.ToBase64CharArray(this.leftOverBytes, 0, this.leftOverBytesCount, this.charsLine, 0);
				this.WriteChars(this.charsLine, 0, num);
				this.leftOverBytesCount = 0;
			}
		}

		// Token: 0x06000022 RID: 34
		internal abstract Task WriteCharsAsync(char[] chars, int index, int count);

		// Token: 0x06000023 RID: 35 RVA: 0x000027AC File Offset: 0x000009AC
		internal async Task EncodeAsync(byte[] buffer, int index, int count)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (count > buffer.Length - index)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (this.leftOverBytesCount > 0)
			{
				int num = this.leftOverBytesCount;
				while (num < 3 && count > 0)
				{
					byte[] array = this.leftOverBytes;
					int num2 = num++;
					int num3 = index;
					index = num3 + 1;
					array[num2] = buffer[num3];
					num3 = count;
					count = num3 - 1;
				}
				if (count == 0 && num < 3)
				{
					this.leftOverBytesCount = num;
					return;
				}
				int num4 = Convert.ToBase64CharArray(this.leftOverBytes, 0, 3, this.charsLine, 0);
				await this.WriteCharsAsync(this.charsLine, 0, num4).ConfigureAwait(false);
			}
			this.leftOverBytesCount = count % 3;
			if (this.leftOverBytesCount > 0)
			{
				count -= this.leftOverBytesCount;
				if (this.leftOverBytes == null)
				{
					this.leftOverBytes = new byte[3];
				}
				for (int i = 0; i < this.leftOverBytesCount; i++)
				{
					this.leftOverBytes[i] = buffer[index + count + i];
				}
			}
			int endIndex = index + count;
			int chunkSize = 57;
			while (index < endIndex)
			{
				if (index + chunkSize > endIndex)
				{
					chunkSize = endIndex - index;
				}
				int num5 = Convert.ToBase64CharArray(buffer, index, chunkSize, this.charsLine, 0);
				await this.WriteCharsAsync(this.charsLine, 0, num5).ConfigureAwait(false);
				index += chunkSize;
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002808 File Offset: 0x00000A08
		internal async Task FlushAsync()
		{
			if (this.leftOverBytesCount > 0)
			{
				int num = Convert.ToBase64CharArray(this.leftOverBytes, 0, this.leftOverBytesCount, this.charsLine, 0);
				await this.WriteCharsAsync(this.charsLine, 0, num).ConfigureAwait(false);
				this.leftOverBytesCount = 0;
			}
		}

		// Token: 0x04000018 RID: 24
		private byte[] leftOverBytes;

		// Token: 0x04000019 RID: 25
		private int leftOverBytesCount;

		// Token: 0x0400001A RID: 26
		private char[] charsLine;
	}
}
