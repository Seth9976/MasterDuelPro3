using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Internal.Runtime.Augments;

namespace System.Buffers
{
	// Token: 0x02000786 RID: 1926
	internal sealed class TlsOverPerCoreLockedStacksArrayPool<T> : ArrayPool<T>
	{
		// Token: 0x06003CF4 RID: 15604 RVA: 0x000EACA8 File Offset: 0x000E8EA8
		public TlsOverPerCoreLockedStacksArrayPool()
		{
			int[] array = new int[17];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Utilities.GetMaxSizeForBucket(i);
			}
			this._bucketArraySizes = array;
		}

		// Token: 0x06003CF5 RID: 15605 RVA: 0x000EACF0 File Offset: 0x000E8EF0
		private TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks CreatePerCoreLockedStacks(int bucketIndex)
		{
			TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks perCoreLockedStacks = new TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks();
			return Interlocked.CompareExchange<TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks>(ref this._buckets[bucketIndex], perCoreLockedStacks, null) ?? perCoreLockedStacks;
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06003CF6 RID: 15606 RVA: 0x000EA961 File Offset: 0x000E8B61
		private int Id
		{
			get
			{
				return this.GetHashCode();
			}
		}

		// Token: 0x06003CF7 RID: 15607 RVA: 0x000EAD1C File Offset: 0x000E8F1C
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
			T[] array2;
			if (num < this._buckets.Length)
			{
				T[][] array = TlsOverPerCoreLockedStacksArrayPool<T>.t_tlsBuckets;
				if (array != null)
				{
					array2 = array[num];
					if (array2 != null)
					{
						array[num] = null;
						if (log.IsEnabled())
						{
							log.BufferRented(array2.GetHashCode(), array2.Length, this.Id, num);
						}
						return array2;
					}
				}
				TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks perCoreLockedStacks = this._buckets[num];
				if (perCoreLockedStacks != null)
				{
					array2 = perCoreLockedStacks.TryPop();
					if (array2 != null)
					{
						if (log.IsEnabled())
						{
							log.BufferRented(array2.GetHashCode(), array2.Length, this.Id, num);
						}
						return array2;
					}
				}
				array2 = new T[this._bucketArraySizes[num]];
			}
			else
			{
				array2 = new T[minimumLength];
			}
			if (log.IsEnabled())
			{
				int hashCode = array2.GetHashCode();
				int num2 = -1;
				log.BufferRented(hashCode, array2.Length, this.Id, num2);
				log.BufferAllocated(hashCode, array2.Length, this.Id, num2, (num >= this._buckets.Length) ? ArrayPoolEventSource.BufferAllocatedReason.OverMaximumSize : ArrayPoolEventSource.BufferAllocatedReason.PoolExhausted);
			}
			return array2;
		}

		// Token: 0x06003CF8 RID: 15608 RVA: 0x000EAE28 File Offset: 0x000E9028
		public override void Return(T[] array, bool clearArray = false)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = Utilities.SelectBucketIndex(array.Length);
			if (num < this._buckets.Length)
			{
				if (clearArray)
				{
					Array.Clear(array, 0, array.Length);
				}
				if (array.Length != this._bucketArraySizes[num])
				{
					throw new ArgumentException("The buffer is not associated with this pool and may not be returned to it.", "array");
				}
				T[][] array2 = TlsOverPerCoreLockedStacksArrayPool<T>.t_tlsBuckets;
				if (array2 == null)
				{
					array2 = (TlsOverPerCoreLockedStacksArrayPool<T>.t_tlsBuckets = new T[17][]);
					array2[num] = array;
					if (TlsOverPerCoreLockedStacksArrayPool<T>.s_trimBuffers)
					{
						TlsOverPerCoreLockedStacksArrayPool<T>.s_allTlsBuckets.Add(array2, null);
						if (Interlocked.Exchange(ref this._callbackCreated, 1) != 1)
						{
							Gen2GcCallback.Register(new Func<object, bool>(TlsOverPerCoreLockedStacksArrayPool<T>.Gen2GcCallbackFunc), this);
						}
					}
				}
				else
				{
					T[] array3 = array2[num];
					array2[num] = array;
					if (array3 != null)
					{
						(this._buckets[num] ?? this.CreatePerCoreLockedStacks(num)).TryPush(array3);
					}
				}
			}
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferReturned(array.GetHashCode(), array.Length, this.Id);
			}
		}

		// Token: 0x06003CF9 RID: 15609 RVA: 0x000EAF1C File Offset: 0x000E911C
		public bool Trim()
		{
			int tickCount = Environment.TickCount;
			TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure memoryPressure = TlsOverPerCoreLockedStacksArrayPool<T>.GetMemoryPressure();
			ArrayPoolEventSource log = ArrayPoolEventSource.Log;
			if (log.IsEnabled())
			{
				log.BufferTrimPoll(tickCount, (int)memoryPressure);
			}
			foreach (TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks perCoreLockedStacks in this._buckets)
			{
				if (perCoreLockedStacks != null)
				{
					perCoreLockedStacks.Trim((uint)tickCount, this.Id, memoryPressure, this._bucketArraySizes);
				}
			}
			if (memoryPressure == TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.High)
			{
				if (log.IsEnabled())
				{
					using (IEnumerator<KeyValuePair<T[][], object>> enumerator = ((IEnumerable<KeyValuePair<T[][], object>>)TlsOverPerCoreLockedStacksArrayPool<T>.s_allTlsBuckets).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							KeyValuePair<T[][], object> keyValuePair = enumerator.Current;
							T[][] key = keyValuePair.Key;
							for (int j = 0; j < key.Length; j++)
							{
								T[] array = Interlocked.Exchange<T[]>(ref key[j], null);
								if (array != null)
								{
									log.BufferTrimmed(array.GetHashCode(), array.Length, this.Id);
								}
							}
						}
						return true;
					}
				}
				foreach (KeyValuePair<T[][], object> keyValuePair2 in ((IEnumerable<KeyValuePair<T[][], object>>)TlsOverPerCoreLockedStacksArrayPool<T>.s_allTlsBuckets))
				{
					T[][] key2 = keyValuePair2.Key;
					Array.Clear(key2, 0, key2.Length);
				}
			}
			return true;
		}

		// Token: 0x06003CFA RID: 15610 RVA: 0x000EB068 File Offset: 0x000E9268
		private static bool Gen2GcCallbackFunc(object target)
		{
			return ((TlsOverPerCoreLockedStacksArrayPool<T>)target).Trim();
		}

		// Token: 0x06003CFB RID: 15611 RVA: 0x000EB078 File Offset: 0x000E9278
		private static TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure GetMemoryPressure()
		{
			uint num;
			ulong num2;
			uint num3;
			UIntPtr uintPtr;
			UIntPtr uintPtr2;
			GC.GetMemoryInfo(out num, out num2, out num3, out uintPtr, out uintPtr2);
			if (num3 >= num * 0.9)
			{
				return TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.High;
			}
			if (num3 >= num * 0.7)
			{
				return TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.Medium;
			}
			return TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.Low;
		}

		// Token: 0x06003CFC RID: 15612 RVA: 0x0000C091 File Offset: 0x0000A291
		private static bool GetTrimBuffers()
		{
			return true;
		}

		// Token: 0x04001F65 RID: 8037
		private readonly int[] _bucketArraySizes;

		// Token: 0x04001F66 RID: 8038
		private readonly TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks[] _buckets = new TlsOverPerCoreLockedStacksArrayPool<T>.PerCoreLockedStacks[17];

		// Token: 0x04001F67 RID: 8039
		[ThreadStatic]
		private static T[][] t_tlsBuckets;

		// Token: 0x04001F68 RID: 8040
		private int _callbackCreated;

		// Token: 0x04001F69 RID: 8041
		private static readonly bool s_trimBuffers = TlsOverPerCoreLockedStacksArrayPool<T>.GetTrimBuffers();

		// Token: 0x04001F6A RID: 8042
		private static readonly ConditionalWeakTable<T[][], object> s_allTlsBuckets = (TlsOverPerCoreLockedStacksArrayPool<T>.s_trimBuffers ? new ConditionalWeakTable<T[][], object>() : null);

		// Token: 0x02000787 RID: 1927
		private enum MemoryPressure
		{
			// Token: 0x04001F6C RID: 8044
			Low,
			// Token: 0x04001F6D RID: 8045
			Medium,
			// Token: 0x04001F6E RID: 8046
			High
		}

		// Token: 0x02000788 RID: 1928
		private sealed class PerCoreLockedStacks
		{
			// Token: 0x06003CFE RID: 15614 RVA: 0x000EB0E0 File Offset: 0x000E92E0
			public PerCoreLockedStacks()
			{
				TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[] array = new TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[Math.Min(Environment.ProcessorCount, 64)];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = new TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack();
				}
				this._perCoreStacks = array;
			}

			// Token: 0x06003CFF RID: 15615 RVA: 0x000EB124 File Offset: 0x000E9324
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public void TryPush(T[] array)
			{
				TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[] perCoreStacks = this._perCoreStacks;
				int num = RuntimeThread.GetCurrentProcessorId() % perCoreStacks.Length;
				for (int i = 0; i < perCoreStacks.Length; i++)
				{
					if (perCoreStacks[num].TryPush(array))
					{
						return;
					}
					if (++num == perCoreStacks.Length)
					{
						num = 0;
					}
				}
			}

			// Token: 0x06003D00 RID: 15616 RVA: 0x000EB168 File Offset: 0x000E9368
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public T[] TryPop()
			{
				TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[] perCoreStacks = this._perCoreStacks;
				int num = RuntimeThread.GetCurrentProcessorId() % perCoreStacks.Length;
				for (int i = 0; i < perCoreStacks.Length; i++)
				{
					T[] array;
					if ((array = perCoreStacks[num].TryPop()) != null)
					{
						return array;
					}
					if (++num == perCoreStacks.Length)
					{
						num = 0;
					}
				}
				return null;
			}

			// Token: 0x06003D01 RID: 15617 RVA: 0x000EB1B0 File Offset: 0x000E93B0
			public bool Trim(uint tickCount, int id, TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure pressure, int[] bucketSizes)
			{
				TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[] perCoreStacks = this._perCoreStacks;
				for (int i = 0; i < perCoreStacks.Length; i++)
				{
					perCoreStacks[i].Trim(tickCount, id, pressure, bucketSizes[i]);
				}
				return true;
			}

			// Token: 0x04001F6F RID: 8047
			private readonly TlsOverPerCoreLockedStacksArrayPool<T>.LockedStack[] _perCoreStacks;
		}

		// Token: 0x02000789 RID: 1929
		private sealed class LockedStack
		{
			// Token: 0x06003D02 RID: 15618 RVA: 0x000EB1E4 File Offset: 0x000E93E4
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public bool TryPush(T[] array)
			{
				bool flag = false;
				Monitor.Enter(this);
				if (this._count < 8)
				{
					if (TlsOverPerCoreLockedStacksArrayPool<T>.s_trimBuffers && this._count == 0)
					{
						this._firstStackItemMS = (uint)Environment.TickCount;
					}
					T[][] arrays = this._arrays;
					int count = this._count;
					this._count = count + 1;
					arrays[count] = array;
					flag = true;
				}
				Monitor.Exit(this);
				return flag;
			}

			// Token: 0x06003D03 RID: 15619 RVA: 0x000EB240 File Offset: 0x000E9440
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public T[] TryPop()
			{
				T[] array = null;
				Monitor.Enter(this);
				if (this._count > 0)
				{
					T[][] arrays = this._arrays;
					int num = this._count - 1;
					this._count = num;
					array = arrays[num];
					this._arrays[this._count] = null;
				}
				Monitor.Exit(this);
				return array;
			}

			// Token: 0x06003D04 RID: 15620 RVA: 0x000EB28C File Offset: 0x000E948C
			public void Trim(uint tickCount, int id, TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure pressure, int bucketSize)
			{
				if (this._count == 0)
				{
					return;
				}
				uint num = ((pressure == TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.High) ? 10000U : 60000U);
				lock (this)
				{
					if ((this._count > 0 && this._firstStackItemMS > tickCount) || tickCount - this._firstStackItemMS > num)
					{
						ArrayPoolEventSource log = ArrayPoolEventSource.Log;
						int num2 = 1;
						if (pressure != TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.Medium)
						{
							if (pressure == TlsOverPerCoreLockedStacksArrayPool<T>.MemoryPressure.High)
							{
								num2 = 8;
								if (bucketSize > 16384)
								{
									num2++;
								}
								if (Unsafe.SizeOf<T>() > 16)
								{
									num2++;
								}
								if (Unsafe.SizeOf<T>() > 32)
								{
									num2++;
								}
							}
						}
						else
						{
							num2 = 2;
						}
						while (this._count > 0 && num2-- > 0)
						{
							T[][] arrays = this._arrays;
							int num3 = this._count - 1;
							this._count = num3;
							T[] array = arrays[num3];
							this._arrays[this._count] = null;
							if (log.IsEnabled())
							{
								log.BufferTrimmed(array.GetHashCode(), array.Length, id);
							}
						}
						if (this._count > 0 && this._firstStackItemMS < 4294952295U)
						{
							this._firstStackItemMS += 15000U;
						}
					}
				}
			}

			// Token: 0x04001F70 RID: 8048
			private readonly T[][] _arrays = new T[8][];

			// Token: 0x04001F71 RID: 8049
			private int _count;

			// Token: 0x04001F72 RID: 8050
			private uint _firstStackItemMS;
		}
	}
}
