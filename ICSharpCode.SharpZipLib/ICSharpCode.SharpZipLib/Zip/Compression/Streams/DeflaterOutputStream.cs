using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ICSharpCode.SharpZipLib.Encryption;

namespace ICSharpCode.SharpZipLib.Zip.Compression.Streams
{
	// Token: 0x02000061 RID: 97
	public class DeflaterOutputStream : Stream
	{
		// Token: 0x06000312 RID: 786 RVA: 0x0000FFDF File Offset: 0x0000E1DF
		public DeflaterOutputStream(Stream baseOutputStream)
			: this(baseOutputStream, new Deflater(), 512)
		{
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000FFF2 File Offset: 0x0000E1F2
		public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater)
			: this(baseOutputStream, deflater, 512)
		{
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00010004 File Offset: 0x0000E204
		public DeflaterOutputStream(Stream baseOutputStream, Deflater deflater, int bufferSize)
		{
			if (baseOutputStream == null)
			{
				throw new ArgumentNullException("baseOutputStream");
			}
			if (!baseOutputStream.CanWrite)
			{
				throw new ArgumentException("Must support writing", "baseOutputStream");
			}
			if (bufferSize < 512)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			this.baseOutputStream_ = baseOutputStream;
			this.buffer_ = new byte[bufferSize];
			if (deflater == null)
			{
				throw new ArgumentNullException("deflater");
			}
			this.deflater_ = deflater;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001008C File Offset: 0x0000E28C
		public virtual void Finish()
		{
			this.deflater_.Finish();
			while (!this.deflater_.IsFinished)
			{
				int num = this.deflater_.Deflate(this.buffer_, 0, this.buffer_.Length);
				if (num <= 0)
				{
					break;
				}
				this.EncryptBlock(this.buffer_, 0, num);
				this.baseOutputStream_.Write(this.buffer_, 0, num);
			}
			if (!this.deflater_.IsFinished)
			{
				throw new SharpZipBaseException("Can't deflate all input?");
			}
			this.baseOutputStream_.Flush();
			if (this.cryptoTransform_ != null)
			{
				if (this.cryptoTransform_ is ZipAESTransform)
				{
					this.AESAuthCode = ((ZipAESTransform)this.cryptoTransform_).GetAuthCode();
				}
				this.cryptoTransform_.Dispose();
				this.cryptoTransform_ = null;
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00010154 File Offset: 0x0000E354
		public virtual async Task FinishAsync(CancellationToken ct)
		{
			this.deflater_.Finish();
			while (!this.deflater_.IsFinished)
			{
				int num = this.deflater_.Deflate(this.buffer_, 0, this.buffer_.Length);
				if (num <= 0)
				{
					break;
				}
				this.EncryptBlock(this.buffer_, 0, num);
				await this.baseOutputStream_.WriteAsync(this.buffer_, 0, num, ct).ConfigureAwait(false);
			}
			if (!this.deflater_.IsFinished)
			{
				throw new SharpZipBaseException("Can't deflate all input?");
			}
			await this.baseOutputStream_.FlushAsync(ct).ConfigureAwait(false);
			if (this.cryptoTransform_ != null)
			{
				if (this.cryptoTransform_ is ZipAESTransform)
				{
					this.AESAuthCode = ((ZipAESTransform)this.cryptoTransform_).GetAuthCode();
				}
				this.cryptoTransform_.Dispose();
				this.cryptoTransform_ = null;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0001019F File Offset: 0x0000E39F
		// (set) Token: 0x06000318 RID: 792 RVA: 0x000101A7 File Offset: 0x0000E3A7
		public bool IsStreamOwner { get; set; } = true;

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000319 RID: 793 RVA: 0x000101B0 File Offset: 0x0000E3B0
		public bool CanPatchEntries
		{
			get
			{
				return this.baseOutputStream_.CanSeek;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600031A RID: 794 RVA: 0x000101BD File Offset: 0x0000E3BD
		// (set) Token: 0x0600031B RID: 795 RVA: 0x000101CA File Offset: 0x0000E3CA
		public Encoding ZipCryptoEncoding
		{
			get
			{
				return this._stringCodec.ZipCryptoEncoding;
			}
			set
			{
				this._stringCodec = this._stringCodec.WithZipCryptoEncoding(value);
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000101DE File Offset: 0x0000E3DE
		protected void EncryptBlock(byte[] buffer, int offset, int length)
		{
			if (this.cryptoTransform_ == null)
			{
				return;
			}
			this.cryptoTransform_.TransformBlock(buffer, 0, length, buffer, 0);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000101FC File Offset: 0x0000E3FC
		protected void Deflate()
		{
			this.DeflateSyncOrAsync(false, null).GetAwaiter().GetResult();
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00010228 File Offset: 0x0000E428
		private async Task DeflateSyncOrAsync(bool flushing, CancellationToken? ct)
		{
			while (flushing || !this.deflater_.IsNeedingInput)
			{
				int num = this.deflater_.Deflate(this.buffer_, 0, this.buffer_.Length);
				if (num <= 0)
				{
					break;
				}
				this.EncryptBlock(this.buffer_, 0, num);
				if (ct != null)
				{
					await this.baseOutputStream_.WriteAsync(this.buffer_, 0, num, ct.Value).ConfigureAwait(false);
				}
				else
				{
					this.baseOutputStream_.Write(this.buffer_, 0, num);
				}
			}
			if (!this.deflater_.IsNeedingInput)
			{
				throw new SharpZipBaseException("DeflaterOutputStream can't deflate all input?");
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600031F RID: 799 RVA: 0x0000840F File Offset: 0x0000660F
		public override bool CanRead
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000840F File Offset: 0x0000660F
		public override bool CanSeek
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000321 RID: 801 RVA: 0x0001027B File Offset: 0x0000E47B
		public override bool CanWrite
		{
			get
			{
				return this.baseOutputStream_.CanWrite;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00010288 File Offset: 0x0000E488
		public override long Length
		{
			get
			{
				return this.baseOutputStream_.Length;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00010295 File Offset: 0x0000E495
		// (set) Token: 0x06000324 RID: 804 RVA: 0x000102A2 File Offset: 0x0000E4A2
		public override long Position
		{
			get
			{
				return this.baseOutputStream_.Position;
			}
			set
			{
				throw new NotSupportedException("Position property not supported");
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000102AE File Offset: 0x0000E4AE
		public override long Seek(long offset, SeekOrigin origin)
		{
			throw new NotSupportedException("DeflaterOutputStream Seek not supported");
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000102BA File Offset: 0x0000E4BA
		public override void SetLength(long value)
		{
			throw new NotSupportedException("DeflaterOutputStream SetLength not supported");
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000102C6 File Offset: 0x0000E4C6
		public override int ReadByte()
		{
			throw new NotSupportedException("DeflaterOutputStream ReadByte not supported");
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000102D2 File Offset: 0x0000E4D2
		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotSupportedException("DeflaterOutputStream Read not supported");
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000102E0 File Offset: 0x0000E4E0
		public override void Flush()
		{
			this.deflater_.Flush();
			this.DeflateSyncOrAsync(true, null).GetAwaiter().GetResult();
			this.baseOutputStream_.Flush();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00010320 File Offset: 0x0000E520
		public override async Task FlushAsync(CancellationToken cancellationToken)
		{
			this.deflater_.Flush();
			await this.DeflateSyncOrAsync(true, new CancellationToken?(cancellationToken)).ConfigureAwait(false);
			await this.baseOutputStream_.FlushAsync(cancellationToken).ConfigureAwait(false);
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001036C File Offset: 0x0000E56C
		protected override void Dispose(bool disposing)
		{
			if (!this.isClosed_)
			{
				this.isClosed_ = true;
				try
				{
					this.Finish();
					if (this.cryptoTransform_ != null)
					{
						this.GetAuthCodeIfAES();
						this.cryptoTransform_.Dispose();
						this.cryptoTransform_ = null;
					}
				}
				finally
				{
					if (this.IsStreamOwner)
					{
						this.baseOutputStream_.Dispose();
					}
				}
			}
		}

		// Token: 0x0600032C RID: 812 RVA: 0x000103D4 File Offset: 0x0000E5D4
		protected void GetAuthCodeIfAES()
		{
			if (this.cryptoTransform_ is ZipAESTransform)
			{
				this.AESAuthCode = ((ZipAESTransform)this.cryptoTransform_).GetAuthCode();
			}
		}

		// Token: 0x0600032D RID: 813 RVA: 0x000103FC File Offset: 0x0000E5FC
		public override void WriteByte(byte value)
		{
			this.Write(new byte[] { value }, 0, 1);
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001041D File Offset: 0x0000E61D
		public override void Write(byte[] buffer, int offset, int count)
		{
			this.deflater_.SetInput(buffer, offset, count);
			this.Deflate();
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00010434 File Offset: 0x0000E634
		public override async Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken ct)
		{
			this.deflater_.SetInput(buffer, offset, count);
			await this.DeflateSyncOrAsync(false, new CancellationToken?(ct)).ConfigureAwait(false);
		}

		// Token: 0x0400024B RID: 587
		protected ICryptoTransform cryptoTransform_;

		// Token: 0x0400024C RID: 588
		protected byte[] AESAuthCode;

		// Token: 0x0400024D RID: 589
		private byte[] buffer_;

		// Token: 0x0400024E RID: 590
		protected Deflater deflater_;

		// Token: 0x0400024F RID: 591
		protected Stream baseOutputStream_;

		// Token: 0x04000250 RID: 592
		private bool isClosed_;

		// Token: 0x04000251 RID: 593
		protected StringCodec _stringCodec = ZipStrings.GetStringCodec();
	}
}
