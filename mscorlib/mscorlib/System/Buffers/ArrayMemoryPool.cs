using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x0200078B RID: 1931
	internal sealed class ArrayMemoryPool<T> : MemoryPool<T>
	{
		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06003D08 RID: 15624 RVA: 0x000EB43E File Offset: 0x000E963E
		public sealed override int MaxBufferSize
		{
			get
			{
				return int.MaxValue;
			}
		}

		// Token: 0x06003D09 RID: 15625 RVA: 0x000EB445 File Offset: 0x000E9645
		public sealed override IMemoryOwner<T> Rent(int minimumBufferSize = -1)
		{
			if (minimumBufferSize == -1)
			{
				minimumBufferSize = 1 + 4095 / Unsafe.SizeOf<T>();
			}
			else if (minimumBufferSize > 2147483647)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.minimumBufferSize);
			}
			return new ArrayMemoryPool<T>.ArrayMemoryPoolBuffer(minimumBufferSize);
		}

		// Token: 0x06003D0A RID: 15626 RVA: 0x00002C89 File Offset: 0x00000E89
		protected sealed override void Dispose(bool disposing)
		{
		}

		// Token: 0x0200078C RID: 1932
		private sealed class ArrayMemoryPoolBuffer : IMemoryOwner<T>, IDisposable
		{
			// Token: 0x06003D0C RID: 15628 RVA: 0x000EB479 File Offset: 0x000E9679
			public ArrayMemoryPoolBuffer(int size)
			{
				this._array = ArrayPool<T>.Shared.Rent(size);
			}

			// Token: 0x170009E5 RID: 2533
			// (get) Token: 0x06003D0D RID: 15629 RVA: 0x000EB492 File Offset: 0x000E9692
			public Memory<T> Memory
			{
				get
				{
					T[] array = this._array;
					if (array == null)
					{
						ThrowHelper.ThrowObjectDisposedException_ArrayMemoryPoolBuffer();
					}
					return new Memory<T>(array);
				}
			}

			// Token: 0x06003D0E RID: 15630 RVA: 0x000EB4A8 File Offset: 0x000E96A8
			public void Dispose()
			{
				T[] array = this._array;
				if (array != null)
				{
					this._array = null;
					ArrayPool<T>.Shared.Return(array, false);
				}
			}

			// Token: 0x04001F73 RID: 8051
			private T[] _array;
		}
	}
}
