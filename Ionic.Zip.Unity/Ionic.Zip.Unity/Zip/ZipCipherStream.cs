using System;
using System.IO;

namespace Ionic.Zip
{
	// Token: 0x0200002B RID: 43
	internal class ZipCipherStream : Stream
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000051B2 File Offset: 0x000033B2
		public ZipCipherStream(Stream s, ZipCrypto cipher, CryptoMode mode)
		{
			this._cipher = cipher;
			this._s = s;
			this._mode = mode;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000051D0 File Offset: 0x000033D0
		public override int Read(byte[] buffer, int offset, int count)
		{
			if (this._mode == CryptoMode.Encrypt)
			{
				throw new NotSupportedException("This stream does not encrypt via Read()");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			byte[] array = new byte[count];
			int num = this._s.Read(array, 0, count);
			byte[] array2 = this._cipher.DecryptMessage(array, num);
			for (int i = 0; i < num; i++)
			{
				buffer[offset + i] = array2[i];
			}
			return num;
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005238 File Offset: 0x00003438
		public override void Write(byte[] buffer, int offset, int count)
		{
			if (this._mode == CryptoMode.Decrypt)
			{
				throw new NotSupportedException("This stream does not Decrypt via Write()");
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (count == 0)
			{
				return;
			}
			byte[] array;
			if (offset != 0)
			{
				array = new byte[count];
				for (int i = 0; i < count; i++)
				{
					array[i] = buffer[offset + i];
				}
			}
			else
			{
				array = buffer;
			}
			byte[] array2 = this._cipher.EncryptMessage(array, count);
			this._s.Write(array2, 0, array2.Length);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000052AD File Offset: 0x000034AD
		public override bool CanRead
		{
			get
			{
				return this._mode == CryptoMode.Decrypt;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00003B06 File Offset: 0x00001D06
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x000052B8 File Offset: 0x000034B8
		public override bool CanWrite
		{
			get
			{
				return this._mode == CryptoMode.Encrypt;
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x000024A7 File Offset: 0x000006A7
		public override void Flush()
		{
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000052C3 File Offset: 0x000034C3
		public override long Length
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000052C3 File Offset: 0x000034C3
		// (set) Token: 0x060000FC RID: 252 RVA: 0x000052C3 File Offset: 0x000034C3
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

		// Token: 0x060000FD RID: 253 RVA: 0x000052C3 File Offset: 0x000034C3
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000052C3 File Offset: 0x000034C3
		public override void SetLength(long value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04000094 RID: 148
		private ZipCrypto _cipher;

		// Token: 0x04000095 RID: 149
		private Stream _s;

		// Token: 0x04000096 RID: 150
		private CryptoMode _mode;
	}
}
