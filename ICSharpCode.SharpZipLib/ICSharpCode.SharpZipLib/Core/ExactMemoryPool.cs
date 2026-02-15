using System;
using System.Buffers;

namespace ICSharpCode.SharpZipLib.Core
{
	// Token: 0x020000A9 RID: 169
	internal sealed class ExactMemoryPool<T> : MemoryPool<T>
	{
		// Token: 0x0600055F RID: 1375 RVA: 0x0001A225 File Offset: 0x00018425
		public override IMemoryOwner<T> Rent(int bufferSize = -1)
		{
			if (bufferSize > 2147483647 || bufferSize < 0)
			{
				throw new ArgumentOutOfRangeException("bufferSize");
			}
			return new ExactMemoryPool<T>.ExactMemoryPoolBuffer(bufferSize);
		}

		// Token: 0x06000560 RID: 1376 RVA: 0x00008444 File Offset: 0x00006644
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001A244 File Offset: 0x00018444
		public override int MaxBufferSize
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x0400042C RID: 1068
		public new static readonly MemoryPool<T> Shared = new ExactMemoryPool<T>();

		// Token: 0x020000AA RID: 170
		private sealed class ExactMemoryPoolBuffer : IMemoryOwner<T>, IDisposable
		{
			// Token: 0x06000564 RID: 1380 RVA: 0x0001A25F File Offset: 0x0001845F
			public ExactMemoryPoolBuffer(int size)
			{
				this.size = size;
				this.array = ArrayPool<T>.Shared.Rent(size);
			}

			// Token: 0x1700011E RID: 286
			// (get) Token: 0x06000565 RID: 1381 RVA: 0x0001A280 File Offset: 0x00018480
			public Memory<T> Memory
			{
				get
				{
					T[] array = this.array;
					if (array == null)
					{
						throw new ObjectDisposedException("ExactMemoryPoolBuffer");
					}
					return new Memory<T>(array).Slice(0, this.size);
				}
			}

			// Token: 0x06000566 RID: 1382 RVA: 0x0001A2B8 File Offset: 0x000184B8
			public void Dispose()
			{
				T[] array = this.array;
				if (array == null)
				{
					return;
				}
				this.array = null;
				ArrayPool<T>.Shared.Return(array, false);
			}

			// Token: 0x0400042D RID: 1069
			private T[] array;

			// Token: 0x0400042E RID: 1070
			private readonly int size;
		}
	}
}
