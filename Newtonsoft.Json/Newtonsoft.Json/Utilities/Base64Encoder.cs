using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000096 RID: 150
	[NullableContext(1)]
	[Nullable(0)]
	internal class Base64Encoder
	{
		// Token: 0x060004ED RID: 1261 RVA: 0x0001A86A File Offset: 0x00018A6A
		public Base64Encoder(TextWriter writer)
		{
			ValidationUtils.ArgumentNotNull(writer, "writer");
			this._writer = writer;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001A894 File Offset: 0x00018A94
		private void ValidateEncode(byte[] buffer, int index, int count)
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
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0001A8E0 File Offset: 0x00018AE0
		public void Encode(byte[] buffer, int index, int count)
		{
			this.ValidateEncode(buffer, index, count);
			if (this._leftOverBytesCount > 0)
			{
				if (this.FulfillFromLeftover(buffer, index, ref count))
				{
					return;
				}
				int num = Convert.ToBase64CharArray(this._leftOverBytes, 0, 3, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, num);
			}
			this.StoreLeftOverBytes(buffer, index, ref count);
			int num2 = index + count;
			int num3 = 57;
			while (index < num2)
			{
				if (index + num3 > num2)
				{
					num3 = num2 - index;
				}
				int num4 = Convert.ToBase64CharArray(buffer, index, num3, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, num4);
				index += num3;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0001A974 File Offset: 0x00018B74
		private void StoreLeftOverBytes(byte[] buffer, int index, ref int count)
		{
			int num = count % 3;
			if (num > 0)
			{
				count -= num;
				if (this._leftOverBytes == null)
				{
					this._leftOverBytes = new byte[3];
				}
				for (int i = 0; i < num; i++)
				{
					this._leftOverBytes[i] = buffer[index + count + i];
				}
			}
			this._leftOverBytesCount = num;
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0001A9C8 File Offset: 0x00018BC8
		private bool FulfillFromLeftover(byte[] buffer, int index, ref int count)
		{
			int leftOverBytesCount = this._leftOverBytesCount;
			while (leftOverBytesCount < 3 && count > 0)
			{
				this._leftOverBytes[leftOverBytesCount++] = buffer[index++];
				count--;
			}
			if (count == 0 && leftOverBytesCount < 3)
			{
				this._leftOverBytesCount = leftOverBytesCount;
				return true;
			}
			return false;
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001AA14 File Offset: 0x00018C14
		public void Flush()
		{
			if (this._leftOverBytesCount > 0)
			{
				int num = Convert.ToBase64CharArray(this._leftOverBytes, 0, this._leftOverBytesCount, this._charsLine, 0);
				this.WriteChars(this._charsLine, 0, num);
				this._leftOverBytesCount = 0;
			}
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0001AA59 File Offset: 0x00018C59
		private void WriteChars(char[] chars, int index, int count)
		{
			this._writer.Write(chars, index, count);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0001AA6C File Offset: 0x00018C6C
		public async Task EncodeAsync(byte[] buffer, int index, int count, CancellationToken cancellationToken)
		{
			this.ValidateEncode(buffer, index, count);
			if (this._leftOverBytesCount > 0)
			{
				if (this.FulfillFromLeftover(buffer, index, ref count))
				{
					return;
				}
				int num5 = Convert.ToBase64CharArray(this._leftOverBytes, 0, 3, this._charsLine, 0);
				await this.WriteCharsAsync(this._charsLine, 0, num5, cancellationToken).ConfigureAwait(false);
			}
			this.StoreLeftOverBytes(buffer, index, ref count);
			int num4 = index + count;
			int length = 57;
			while (index < num4)
			{
				if (index + length > num4)
				{
					length = num4 - index;
				}
				int num6 = Convert.ToBase64CharArray(buffer, index, length, this._charsLine, 0);
				await this.WriteCharsAsync(this._charsLine, 0, num6, cancellationToken).ConfigureAwait(false);
				index += length;
			}
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0001AAD0 File Offset: 0x00018CD0
		private Task WriteCharsAsync(char[] chars, int index, int count, CancellationToken cancellationToken)
		{
			return this._writer.WriteAsync(chars, index, count, cancellationToken);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0001AAE4 File Offset: 0x00018CE4
		public Task FlushAsync(CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return cancellationToken.FromCanceled();
			}
			if (this._leftOverBytesCount > 0)
			{
				int num = Convert.ToBase64CharArray(this._leftOverBytes, 0, this._leftOverBytesCount, this._charsLine, 0);
				this._leftOverBytesCount = 0;
				return this.WriteCharsAsync(this._charsLine, 0, num, cancellationToken);
			}
			return AsyncUtils.CompletedTask;
		}

		// Token: 0x0400036E RID: 878
		private const int Base64LineSize = 76;

		// Token: 0x0400036F RID: 879
		private const int LineSizeInBytes = 57;

		// Token: 0x04000370 RID: 880
		private readonly char[] _charsLine = new char[76];

		// Token: 0x04000371 RID: 881
		private readonly TextWriter _writer;

		// Token: 0x04000372 RID: 882
		[Nullable(2)]
		private byte[] _leftOverBytes;

		// Token: 0x04000373 RID: 883
		private int _leftOverBytesCount;
	}
}
