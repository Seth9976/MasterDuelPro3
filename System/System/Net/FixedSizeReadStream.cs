using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net
{
	// Token: 0x02000408 RID: 1032
	internal class FixedSizeReadStream : WebReadStream
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060019A3 RID: 6563 RVA: 0x0006DF95 File Offset: 0x0006C195
		public long ContentLength { get; }

		// Token: 0x060019A4 RID: 6564 RVA: 0x0006DF9D File Offset: 0x0006C19D
		public FixedSizeReadStream(WebOperation operation, Stream innerStream, long contentLength)
			: base(operation, innerStream)
		{
			this.ContentLength = contentLength;
		}

		// Token: 0x060019A5 RID: 6565 RVA: 0x0006DFB0 File Offset: 0x0006C1B0
		protected override async Task<int> ProcessReadAsync(byte[] buffer, int offset, int size, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			long num = this.ContentLength - this.position;
			int num2;
			if (num == 0L)
			{
				num2 = 0;
			}
			else
			{
				int num3 = (int)Math.Min(num, (long)size);
				int num4 = await base.InnerStream.ReadAsync(buffer, offset, num3, cancellationToken).ConfigureAwait(false);
				if (num4 <= 0)
				{
					num2 = num4;
				}
				else
				{
					this.position += (long)num4;
					num2 = num4;
				}
			}
			return num2;
		}

		// Token: 0x04001046 RID: 4166
		private long position;
	}
}
