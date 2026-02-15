using System;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x020003FF RID: 1023
	internal class ContentDecodeStream : WebReadStream
	{
		// Token: 0x06001961 RID: 6497 RVA: 0x0006CC2C File Offset: 0x0006AE2C
		public static ContentDecodeStream Create(WebOperation operation, Stream innerStream, ContentDecodeStream.Mode mode)
		{
			Stream stream;
			if (mode == ContentDecodeStream.Mode.GZip)
			{
				stream = new GZipStream(innerStream, CompressionMode.Decompress);
			}
			else
			{
				stream = new DeflateStream(innerStream, CompressionMode.Decompress);
			}
			return new ContentDecodeStream(operation, stream, innerStream);
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06001962 RID: 6498 RVA: 0x0006CC56 File Offset: 0x0006AE56
		private Stream OriginalInnerStream { get; }

		// Token: 0x06001963 RID: 6499 RVA: 0x0006CC5E File Offset: 0x0006AE5E
		private ContentDecodeStream(WebOperation operation, Stream decodeStream, Stream originalInnerStream)
			: base(operation, decodeStream)
		{
			this.OriginalInnerStream = originalInnerStream;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x0006CC6F File Offset: 0x0006AE6F
		protected override Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			return base.InnerStream.ReadAsync(buffer, offset, size, cancellationToken);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x0006CC84 File Offset: 0x0006AE84
		internal override Task FinishReading(CancellationToken cancellationToken)
		{
			WebReadStream webReadStream = this.OriginalInnerStream as WebReadStream;
			if (webReadStream != null)
			{
				return webReadStream.FinishReading(cancellationToken);
			}
			return Task.CompletedTask;
		}

		// Token: 0x02000400 RID: 1024
		internal enum Mode
		{
			// Token: 0x0400102B RID: 4139
			GZip,
			// Token: 0x0400102C RID: 4140
			Deflate
		}
	}
}
