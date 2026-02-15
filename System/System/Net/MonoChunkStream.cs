using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000424 RID: 1060
	internal class MonoChunkStream : WebReadStream
	{
		// Token: 0x170005DD RID: 1501
		// (get) Token: 0x06001AAF RID: 6831 RVA: 0x000741A5 File Offset: 0x000723A5
		protected MonoChunkParser Decoder { get; }

		// Token: 0x06001AB0 RID: 6832 RVA: 0x000741AD File Offset: 0x000723AD
		public MonoChunkStream(WebOperation operation, Stream innerStream, WebHeaderCollection headers)
			: base(operation, innerStream)
		{
			this.<Headers>k__BackingField = headers;
			this.Decoder = new MonoChunkParser(headers);
		}

		// Token: 0x06001AB1 RID: 6833 RVA: 0x000741CC File Offset: 0x000723CC
		protected override async Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			int num;
			if (this.Decoder.DataAvailable)
			{
				num = this.Decoder.Read(buffer, offset, size);
			}
			else
			{
				int num2 = 0;
				byte[] moreBytes = null;
				while (num2 == 0 && this.Decoder.WantMore)
				{
					int num3 = this.Decoder.ChunkLeft;
					if (num3 <= 0)
					{
						num3 = 1024;
					}
					else if (num3 > 16384)
					{
						num3 = 16384;
					}
					if (moreBytes == null || moreBytes.Length < num3)
					{
						moreBytes = new byte[num3];
					}
					num2 = await base.InnerStream.ReadAsync(moreBytes, 0, num3, cancellationToken).ConfigureAwait(false);
					if (num2 <= 0)
					{
						return num2;
					}
					this.Decoder.Write(moreBytes, 0, num2);
					num2 = this.Decoder.Read(buffer, offset, size);
				}
				num = num2;
			}
			return num;
		}

		// Token: 0x06001AB2 RID: 6834 RVA: 0x00074230 File Offset: 0x00072430
		internal override async Task FinishReading(CancellationToken cancellationToken)
		{
			await base.FinishReading(cancellationToken).ConfigureAwait(false);
			cancellationToken.ThrowIfCancellationRequested();
			if (this.Decoder.DataAvailable)
			{
				MonoChunkStream.ThrowExpectingChunkTrailer();
			}
			while (this.Decoder.WantMore)
			{
				byte[] buffer = new byte[256];
				int num = await base.InnerStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
				if (num <= 0)
				{
					MonoChunkStream.ThrowExpectingChunkTrailer();
				}
				this.Decoder.Write(buffer, 0, num);
				if (this.Decoder.Read(buffer, 0, 1) != 0)
				{
					MonoChunkStream.ThrowExpectingChunkTrailer();
				}
				buffer = null;
			}
		}

		// Token: 0x06001AB3 RID: 6835 RVA: 0x0007427B File Offset: 0x0007247B
		private static void ThrowExpectingChunkTrailer()
		{
			throw new WebException("Expecting chunk trailer.", null, WebExceptionStatus.ServerProtocolViolation, null);
		}
	}
}
