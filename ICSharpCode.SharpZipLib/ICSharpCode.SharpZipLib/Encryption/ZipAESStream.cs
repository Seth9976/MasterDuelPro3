using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;

namespace ICSharpCode.SharpZipLib.Encryption
{
	// Token: 0x0200009F RID: 159
	internal class ZipAESStream : CryptoStream
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x00019380 File Offset: 0x00017580
		public ZipAESStream(Stream stream, ZipAESTransform transform, CryptoStreamMode mode)
			: base(stream, transform, mode)
		{
			this._stream = stream;
			this._transform = transform;
			this._slideBuffer = new byte[1024];
			if (mode != CryptoStreamMode.Read)
			{
				throw new Exception("ZipAESStream only for read");
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x000193B7 File Offset: 0x000175B7
		private bool HasBufferedData
		{
			get
			{
				return this._transformBuffer != null && this._transformBufferStartPos < this._transformBufferFreePos;
			}
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x000193D4 File Offset: 0x000175D4
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (count == 0)
			{
				return 0;
			}
			int num = 0;
			if (this.HasBufferedData)
			{
				num = this.ReadBufferedData(buffer, offset, count);
				if (num == count)
				{
					return num;
				}
				offset += num;
				count -= num;
			}
			if (this._slideBuffer != null)
			{
				num += this.ReadAndTransform(buffer, offset, count);
			}
			return num;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0001941F File Offset: 0x0001761F
		public override Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
		{
			return Task.FromResult<int>(this.Read(buffer, offset, count));
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x00019430 File Offset: 0x00017630
		private int ReadAndTransform(byte[] buffer, int offset, int count)
		{
			int i = 0;
			while (i < count)
			{
				int num = count - i;
				int num2 = this._slideBufFreePos - this._slideBufStartPos;
				int num3 = 26 - num2;
				if (this._slideBuffer.Length - this._slideBufFreePos < num3)
				{
					int num4 = 0;
					int j = this._slideBufStartPos;
					while (j < this._slideBufFreePos)
					{
						this._slideBuffer[num4] = this._slideBuffer[j];
						j++;
						num4++;
					}
					this._slideBufFreePos -= this._slideBufStartPos;
					this._slideBufStartPos = 0;
				}
				int num5 = StreamUtils.ReadRequestedBytes(this._stream, this._slideBuffer, this._slideBufFreePos, num3);
				this._slideBufFreePos += num5;
				num2 = this._slideBufFreePos - this._slideBufStartPos;
				if (num2 < 26)
				{
					if (num2 > 10)
					{
						int num6 = num2 - 10;
						i += this.TransformAndBufferBlock(buffer, offset, num, num6);
					}
					else if (num2 < 10)
					{
						throw new ZipException("Internal error missed auth code");
					}
					byte[] authCode = this._transform.GetAuthCode();
					for (int k = 0; k < 10; k++)
					{
						if (authCode[k] != this._slideBuffer[this._slideBufStartPos + k])
						{
							throw new ZipException("AES Authentication Code does not match. This is a super-CRC check on the data in the file after compression and encryption. \r\nThe file may be damaged.");
						}
					}
					this._slideBuffer = null;
					break;
				}
				int num7 = this.TransformAndBufferBlock(buffer, offset, num, 16);
				i += num7;
				offset += num7;
			}
			return i;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x00019590 File Offset: 0x00017790
		private int ReadBufferedData(byte[] buffer, int offset, int count)
		{
			int num = Math.Min(count, this._transformBufferFreePos - this._transformBufferStartPos);
			Array.Copy(this._transformBuffer, this._transformBufferStartPos, buffer, offset, num);
			this._transformBufferStartPos += num;
			return num;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x000195D4 File Offset: 0x000177D4
		private int TransformAndBufferBlock(byte[] buffer, int offset, int count, int blockSize)
		{
			bool flag = blockSize > count;
			if (flag && this._transformBuffer == null)
			{
				this._transformBuffer = new byte[16];
			}
			byte[] array = (flag ? this._transformBuffer : buffer);
			int num = (flag ? 0 : offset);
			this._transform.TransformBlock(this._slideBuffer, this._slideBufStartPos, blockSize, array, num);
			this._slideBufStartPos += blockSize;
			if (!flag)
			{
				return blockSize;
			}
			Array.Copy(this._transformBuffer, 0, buffer, offset, count);
			this._transformBufferStartPos = count;
			this._transformBufferFreePos = blockSize;
			return count;
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0000843D File Offset: 0x0000663D
		public override void Write(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040003F1 RID: 1009
		public const int AUTH_CODE_LENGTH = 10;

		// Token: 0x040003F2 RID: 1010
		private const int CRYPTO_BLOCK_SIZE = 16;

		// Token: 0x040003F3 RID: 1011
		private const int BLOCK_AND_AUTH = 26;

		// Token: 0x040003F4 RID: 1012
		private Stream _stream;

		// Token: 0x040003F5 RID: 1013
		private ZipAESTransform _transform;

		// Token: 0x040003F6 RID: 1014
		private byte[] _slideBuffer;

		// Token: 0x040003F7 RID: 1015
		private int _slideBufStartPos;

		// Token: 0x040003F8 RID: 1016
		private int _slideBufFreePos;

		// Token: 0x040003F9 RID: 1017
		private byte[] _transformBuffer;

		// Token: 0x040003FA RID: 1018
		private int _transformBufferFreePos;

		// Token: 0x040003FB RID: 1019
		private int _transformBufferStartPos;
	}
}
