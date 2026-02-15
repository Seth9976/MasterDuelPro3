using System;
using System.Diagnostics;
using System.Threading;

namespace System.Buffers
{
	// Token: 0x02000780 RID: 1920
	internal sealed class ConfigurableArrayPool<T> : ArrayPool<T>
	{
		// Token: 0x06003CE4 RID: 15588 RVA: 0x000EA8DC File Offset: 0x000E8ADC
		internal ConfigurableArrayPool(int maxArrayLength, int maxArraysPerBucket)
		{
			if (maxArrayLength <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArrayLength");
			}
			if (maxArraysPerBucket <= 0)
			{
				throw new ArgumentOutOfRangeException("maxArraysPerBucket");
			}
			if (maxArrayLength > 1073741824)
			{
				maxArrayLength = 1073741824;
			}
			else if (maxArrayLength < 16)
			{
				maxArrayLength = 16;
			}
			int id = this.Id;
			ConfigurableArrayPool<T>.Bucket[] array = new ConfigurableArrayPool<T>.Bucket[Utilities.SelectBucketIndex(maxArrayLength) + 1];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new ConfigurableArrayPool<T>.Bucket(Utilities.GetMaxSizeForBucket(i), maxArraysPerBucket, id);
			}
			this._buckets = array;
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06003CE5 RID: 15589 RVA: 0x000EA961 File Offset: 0x000E8B61
		private int Id
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x06003CE6 RID: 15590 RVA: 0x000EA96C File Offset: 0x000E8B6C
		public override T[] Rent(int minimumLength)
		{
			if (minimumLength < 0)
			{
				throw new ArgumentOutOfRangeException("minimumLength");
			}
			if (minimumLength == 0)
			{
				return Array.Empty<T>();
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			int num = Utilities.SelectBucketIndex(minimumLength);
			T[] array;
			if (num < this._buckets.Length)
			{
				int num2 = num;
				for (;;)
				{
					array = this._buckets[num2].Rent();
					if (array != null)
					{
						break;
					}
					if (++num2 >= this._buckets.Length || num2 == num + 2)
					{
						goto IL_0086;
					}
				}
				if (log.IsEnabled())
				{
					log.BufferRented(array.GetHashCode(), array.Length, this.Id, this._buckets[num2].Id);
				}
				return array;
				IL_0086:
				array = new T[this._buckets[num]._bufferLength];
			}
			else
			{
				array = new T[minimumLength];
			}
			if (log.IsEnabled())
			{
				int hashCode = array.GetHashCode();
				int num3 = -1;
				log.BufferRented(hashCode, array.Length, this.Id, num3);
				log.BufferAllocated(hashCode, array.Length, this.Id, num3, (num >= this._buckets.Length) ? ArrayPoolEventSource.BufferAllocatedReason.OverMaximumSize : ArrayPoolEventSource.BufferAllocatedReason.PoolExhausted);
			}
			return array;
		}

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000EAA64 File Offset: 0x000E8C64
		public override void Return(T[] array, bool clearArray = false)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (array.Length == 0)
			{
				return;
			}
			int num = Utilities.SelectBucketIndex(array.Length);
			if (num < this._buckets.Length)
			{
				if (clearArray)
				{
					Array.Clear(array, 0, array.Length);
				}
				this._buckets[num].Return(array);
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferReturned(array.GetHashCode(), array.Length, this.Id);
			}
		}

		// Token: 0x04001F5C RID: 8028
		private readonly ConfigurableArrayPool<T>.Bucket[] _buckets;

		// Token: 0x02000781 RID: 1921
		private sealed class Bucket
		{
			// Token: 0x06003CE8 RID: 15592 RVA: 0x000EAAD6 File Offset: 0x000E8CD6
			internal Bucket(int bufferLength, int numberOfBuffers, int poolId)
			{
				this._lock = new SpinLock(Debugger.IsAttached);
				this._buffers = new T[numberOfBuffers][];
				this._bufferLength = bufferLength;
				this._poolId = poolId;
			}

			// Token: 0x170009E0 RID: 2528
			// (get) Token: 0x06003CE9 RID: 15593 RVA: 0x000EA961 File Offset: 0x000E8B61
			internal int Id
			{
				get
				{
					return this.GetHashCode();
				}
			}

			// Token: 0x06003CEA RID: 15594 RVA: 0x000EAB08 File Offset: 0x000E8D08
			internal T[] Rent()
			{
				T[][] buffers = this._buffers;
				T[] array = null;
				bool flag = false;
				bool flag2 = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index < buffers.Length)
					{
						array = buffers[this._index];
						T[][] array2 = buffers;
						int index = this._index;
						this._index = index + 1;
						array2[index] = null;
						flag2 = array == null;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
				if (flag2)
				{
					array = new T[this._bufferLength];
					ArrayPoolEventSource log = ArrayPoolEventSource.Log;
					if (log.IsEnabled())
					{
						log.BufferAllocated(array.GetHashCode(), this._bufferLength, this._poolId, this.Id, ArrayPoolEventSource.BufferAllocatedReason.Pooled);
					}
				}
				return array;
			}

			// Token: 0x06003CEB RID: 15595 RVA: 0x000EABC4 File Offset: 0x000E8DC4
			internal void Return(T[] array)
			{
				if (array.Length != this._bufferLength)
				{
					throw new ArgumentException("The buffer is not associated with this pool and may not be returned to it.", "array");
				}
				bool flag = false;
				try
				{
					this._lock.Enter(ref flag);
					if (this._index != 0)
					{
						T[][] buffers = this._buffers;
						int num = this._index - 1;
						this._index = num;
						buffers[num] = array;
					}
				}
				finally
				{
					if (flag)
					{
						this._lock.Exit(false);
					}
				}
			}

			// Token: 0x04001F5D RID: 8029
			internal readonly int _bufferLength;

			// Token: 0x04001F5E RID: 8030
			private readonly T[][] _buffers;

			// Token: 0x04001F5F RID: 8031
			private readonly int _poolId;

			// Token: 0x04001F60 RID: 8032
			private SpinLock _lock;

			// Token: 0x04001F61 RID: 8033
			private int _index;
		}
	}
}
