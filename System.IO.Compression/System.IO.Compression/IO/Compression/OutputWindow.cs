using System;

namespace System.IO.Compression
{
	// Token: 0x02000019 RID: 25
	internal sealed class OutputWindow
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00004CC0 File Offset: 0x00002EC0
		public void Write(byte b)
		{
			byte[] window = this._window;
			int end = this._end;
			this._end = end + 1;
			window[end] = b;
			this._end &= 262143;
			this._bytesUsed++;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004D08 File Offset: 0x00002F08
		public void WriteLengthDistance(int length, int distance)
		{
			this._bytesUsed += length;
			int num = (this._end - distance) & 262143;
			int num2 = 262144 - length;
			if (num > num2 || this._end >= num2)
			{
				while (length-- > 0)
				{
					byte[] window = this._window;
					int num3 = this._end;
					this._end = num3 + 1;
					window[num3] = this._window[num++];
					this._end &= 262143;
					num &= 262143;
				}
				return;
			}
			if (length <= distance)
			{
				Array.Copy(this._window, num, this._window, this._end, length);
				this._end += length;
				return;
			}
			while (length-- > 0)
			{
				byte[] window2 = this._window;
				int num3 = this._end;
				this._end = num3 + 1;
				window2[num3] = this._window[num++];
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004DF0 File Offset: 0x00002FF0
		public int CopyFrom(InputBuffer input, int length)
		{
			length = Math.Min(Math.Min(length, 262144 - this._bytesUsed), input.AvailableBytes);
			int num = 262144 - this._end;
			int num2;
			if (length > num)
			{
				num2 = input.CopyTo(this._window, this._end, num);
				if (num2 == num)
				{
					num2 += input.CopyTo(this._window, 0, length - num);
				}
			}
			else
			{
				num2 = input.CopyTo(this._window, this._end, length);
			}
			this._end = (this._end + num2) & 262143;
			this._bytesUsed += num2;
			return num2;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00004E91 File Offset: 0x00003091
		public int FreeBytes
		{
			get
			{
				return 262144 - this._bytesUsed;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00004E9F File Offset: 0x0000309F
		public int AvailableBytes
		{
			get
			{
				return this._bytesUsed;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004EA8 File Offset: 0x000030A8
		public int CopyTo(byte[] output, int offset, int length)
		{
			int num;
			if (length > this._bytesUsed)
			{
				num = this._end;
				length = this._bytesUsed;
			}
			else
			{
				num = (this._end - this._bytesUsed + length) & 262143;
			}
			int num2 = length;
			int num3 = length - num;
			if (num3 > 0)
			{
				Array.Copy(this._window, 262144 - num3, output, offset, num3);
				offset += num3;
				length = num;
			}
			Array.Copy(this._window, num - length, output, offset, length);
			this._bytesUsed -= num2;
			return num2;
		}

		// Token: 0x04000094 RID: 148
		private readonly byte[] _window = new byte[262144];

		// Token: 0x04000095 RID: 149
		private int _end;

		// Token: 0x04000096 RID: 150
		private int _bytesUsed;
	}
}
