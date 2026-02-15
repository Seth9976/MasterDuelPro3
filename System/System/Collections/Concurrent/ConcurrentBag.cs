using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe, unordered collection of objects.</summary>
	/// <typeparam name="T">The type of the elements to be stored in the collection.</typeparam>
	// Token: 0x020002F6 RID: 758
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(IProducerConsumerCollectionDebugView<>))]
	[Serializable]
	public class ConcurrentBag<T> : IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> class.</summary>
		// Token: 0x06001233 RID: 4659 RVA: 0x00052502 File Offset: 0x00050702
		public ConcurrentBag()
		{
			this._locals = new ThreadLocal<ConcurrentBag<T>.WorkStealingQueue>();
		}

		/// <summary>Adds an object to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <param name="item">The object to be added to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x06001234 RID: 4660 RVA: 0x00052515 File Offset: 0x00050715
		public void Add(T item)
		{
			this.GetCurrentThreadWorkStealingQueue(true).LocalPush(item, ref this._emptyToNonEmptyListTransitionCount);
		}

		/// <summary>Attempts to remove and return an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>true if an object was removed successfully; otherwise, false.</returns>
		/// <param name="result">When this method returns, <paramref name="result" /> contains the object removed from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> or the default value of <paramref name="T" /> if the bag is empty.</param>
		// Token: 0x06001235 RID: 4661 RVA: 0x0005252C File Offset: 0x0005072C
		public bool TryTake(out T result)
		{
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
			return (currentThreadWorkStealingQueue != null && currentThreadWorkStealingQueue.TryLocalPop(out result)) || this.TrySteal(out result, true);
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00052557 File Offset: 0x00050757
		private ConcurrentBag<T>.WorkStealingQueue GetCurrentThreadWorkStealingQueue(bool forceCreate)
		{
			ConcurrentBag<T>.WorkStealingQueue workStealingQueue;
			if ((workStealingQueue = this._locals.Value) == null)
			{
				if (!forceCreate)
				{
					return null;
				}
				workStealingQueue = this.CreateWorkStealingQueueForCurrentThread();
			}
			return workStealingQueue;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00052574 File Offset: 0x00050774
		private ConcurrentBag<T>.WorkStealingQueue CreateWorkStealingQueueForCurrentThread()
		{
			object globalQueuesLock = this.GlobalQueuesLock;
			ConcurrentBag<T>.WorkStealingQueue workStealingQueue2;
			lock (globalQueuesLock)
			{
				ConcurrentBag<T>.WorkStealingQueue workStealingQueues = this._workStealingQueues;
				ConcurrentBag<T>.WorkStealingQueue workStealingQueue = ((workStealingQueues != null) ? this.GetUnownedWorkStealingQueue() : null);
				if (workStealingQueue == null)
				{
					workStealingQueue = (this._workStealingQueues = new ConcurrentBag<T>.WorkStealingQueue(workStealingQueues));
				}
				this._locals.Value = workStealingQueue;
				workStealingQueue2 = workStealingQueue;
			}
			return workStealingQueue2;
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000525E8 File Offset: 0x000507E8
		private ConcurrentBag<T>.WorkStealingQueue GetUnownedWorkStealingQueue()
		{
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				if (workStealingQueue._ownerThreadId == currentManagedThreadId)
				{
					return workStealingQueue;
				}
			}
			return null;
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0005261C File Offset: 0x0005081C
		private bool TrySteal(out T result, bool take)
		{
			if (take)
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryTakeSteals();
			}
			else
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryPeekSteals();
			}
			for (;;)
			{
				long num = Interlocked.Read(ref this._emptyToNonEmptyListTransitionCount);
				ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
				if ((currentThreadWorkStealingQueue == null) ? this.TryStealFromTo(this._workStealingQueues, null, out result, take) : (this.TryStealFromTo(currentThreadWorkStealingQueue._nextQueue, null, out result, take) || this.TryStealFromTo(this._workStealingQueues, currentThreadWorkStealingQueue, out result, take)))
				{
					break;
				}
				if (Interlocked.Read(ref this._emptyToNonEmptyListTransitionCount) == num)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x000526A4 File Offset: 0x000508A4
		private bool TryStealFromTo(ConcurrentBag<T>.WorkStealingQueue startInclusive, ConcurrentBag<T>.WorkStealingQueue endExclusive, out T result, bool take)
		{
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = startInclusive; workStealingQueue != endExclusive; workStealingQueue = workStealingQueue._nextQueue)
			{
				if (workStealingQueue.TrySteal(out result, take))
				{
					return true;
				}
			}
			result = default(T);
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- the number of elements in the source <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x0600123B RID: 4667 RVA: 0x000526D8 File Offset: 0x000508D8
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "The array argument is null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument must be greater than or equal zero.");
			}
			if (this._workStealingQueues == null)
			{
				return;
			}
			bool flag = false;
			try
			{
				this.FreezeBag(ref flag);
				int dangerousCount = this.DangerousCount;
				if (index > array.Length - dangerousCount)
				{
					throw new ArgumentException("The number of elements in the collection is greater than the available space from index to the end of the destination array.", "index");
				}
				try
				{
					this.CopyFromEachQueueToArray(array, index);
				}
				catch (ArrayTypeMismatchException ex)
				{
					throw new InvalidCastException(ex.Message, ex);
				}
			}
			finally
			{
				this.UnfreezeBag(flag);
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00052780 File Offset: 0x00050980
		private int CopyFromEachQueueToArray(T[] array, int index)
		{
			int num = index;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				num += workStealingQueue.DangerousCopyTo(array, num);
			}
			return num - index;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional. -or- <paramref name="array" /> does not have zero-based indexing. -or- <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. -or- The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x0600123D RID: 4669 RVA: 0x000527B4 File Offset: 0x000509B4
		void ICollection.CopyTo(Array array, int index)
		{
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException("array", "The array argument is null.");
			}
			this.ToArray().CopyTo(array, index);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> elements to a new array.</summary>
		/// <returns>A new array containing a snapshot of elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x0600123E RID: 4670 RVA: 0x000527F4 File Offset: 0x000509F4
		public T[] ToArray()
		{
			if (this._workStealingQueues != null)
			{
				bool flag = false;
				try
				{
					this.FreezeBag(ref flag);
					int dangerousCount = this.DangerousCount;
					if (dangerousCount > 0)
					{
						T[] array = new T[dangerousCount];
						this.CopyFromEachQueueToArray(array, 0);
						return array;
					}
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
			}
			return Array.Empty<T>();
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x0600123F RID: 4671 RVA: 0x00052858 File Offset: 0x00050A58
		public IEnumerator<T> GetEnumerator()
		{
			return new ConcurrentBag<T>.Enumerator(this.ToArray());
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x06001240 RID: 4672 RVA: 0x00052865 File Offset: 0x00050A65
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00052870 File Offset: 0x00050A70
		public int Count
		{
			get
			{
				if (this._workStealingQueues == null)
				{
					return 0;
				}
				bool flag = false;
				int dangerousCount;
				try
				{
					this.FreezeBag(ref flag);
					dangerousCount = this.DangerousCount;
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
				return dangerousCount;
			}
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x000528B8 File Offset: 0x00050AB8
		private int DangerousCount
		{
			get
			{
				int num = 0;
				checked
				{
					for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
					{
						num += workStealingQueue.DangerousCount;
					}
					return num;
				}
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot; otherwise, false. For <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />, this property always returns false.</returns>
		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x000028AE File Offset: 0x00000AAE
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. This property is not supported.</summary>
		/// <returns>Returns null  (Nothing in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The SyncRoot property is not supported.</exception>
		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x000528E6 File Offset: 0x00050AE6
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("The SyncRoot property may not be used for the synchronization of concurrent collections.");
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x000528F2 File Offset: 0x00050AF2
		private object GlobalQueuesLock
		{
			get
			{
				return this._locals;
			}
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x000528FC File Offset: 0x00050AFC
		private void FreezeBag(ref bool lockTaken)
		{
			Monitor.Enter(this.GlobalQueuesLock, ref lockTaken);
			ConcurrentBag<T>.WorkStealingQueue workStealingQueues = this._workStealingQueues;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				Monitor.Enter(workStealingQueue, ref workStealingQueue._frozen);
			}
			Interlocked.MemoryBarrier();
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue2 = workStealingQueues; workStealingQueue2 != null; workStealingQueue2 = workStealingQueue2._nextQueue)
			{
				if (workStealingQueue2._currentOp != 0)
				{
					SpinWait spinWait = default(SpinWait);
					do
					{
						spinWait.SpinOnce();
					}
					while (workStealingQueue2._currentOp != 0);
				}
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00052970 File Offset: 0x00050B70
		private void UnfreezeBag(bool lockTaken)
		{
			if (lockTaken)
			{
				for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
				{
					if (workStealingQueue._frozen)
					{
						workStealingQueue._frozen = false;
						Monitor.Exit(workStealingQueue);
					}
				}
				Monitor.Exit(this.GlobalQueuesLock);
			}
		}

		// Token: 0x04000B3D RID: 2877
		private readonly ThreadLocal<ConcurrentBag<T>.WorkStealingQueue> _locals;

		// Token: 0x04000B3E RID: 2878
		private volatile ConcurrentBag<T>.WorkStealingQueue _workStealingQueues;

		// Token: 0x04000B3F RID: 2879
		private long _emptyToNonEmptyListTransitionCount;

		// Token: 0x020002F7 RID: 759
		private sealed class WorkStealingQueue
		{
			// Token: 0x06001248 RID: 4680 RVA: 0x000529B5 File Offset: 0x00050BB5
			internal WorkStealingQueue(ConcurrentBag<T>.WorkStealingQueue nextQueue)
			{
				this._ownerThreadId = Environment.CurrentManagedThreadId;
				this._nextQueue = nextQueue;
			}

			// Token: 0x06001249 RID: 4681 RVA: 0x000529E8 File Offset: 0x00050BE8
			internal void LocalPush(T item, ref long emptyToNonEmptyListTransitionCount)
			{
				bool flag = false;
				try
				{
					Interlocked.Exchange(ref this._currentOp, 1);
					int num = this._tailIndex;
					if (num == 2147483647)
					{
						this._currentOp = 0;
						lock (this)
						{
							this._headIndex &= this._mask;
							num = (this._tailIndex = num & this._mask);
							Interlocked.Exchange(ref this._currentOp, 1);
						}
					}
					int num2 = this._headIndex;
					if (!this._frozen && ((num2 < num - 1) & (num < num2 + this._mask)))
					{
						this._array[num & this._mask] = item;
						this._tailIndex = num + 1;
					}
					else
					{
						this._currentOp = 0;
						Monitor.Enter(this, ref flag);
						num2 = this._headIndex;
						int num3 = num - num2;
						if (num3 >= this._mask)
						{
							T[] array = new T[this._array.Length << 1];
							int num4 = num2 & this._mask;
							if (num4 == 0)
							{
								Array.Copy(this._array, 0, array, 0, this._array.Length);
							}
							else
							{
								Array.Copy(this._array, num4, array, 0, this._array.Length - num4);
								Array.Copy(this._array, 0, array, this._array.Length - num4, num4);
							}
							this._array = array;
							this._headIndex = 0;
							num = (this._tailIndex = num3);
							this._mask = (this._mask << 1) | 1;
						}
						this._array[num & this._mask] = item;
						this._tailIndex = num + 1;
						if (num3 == 0)
						{
							Interlocked.Increment(ref emptyToNonEmptyListTransitionCount);
						}
						this._addTakeCount -= this._stealCount;
						this._stealCount = 0;
					}
					checked
					{
						this._addTakeCount++;
					}
				}
				finally
				{
					this._currentOp = 0;
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}

			// Token: 0x0600124A RID: 4682 RVA: 0x00052C40 File Offset: 0x00050E40
			internal bool TryLocalPop(out T result)
			{
				int num = this._tailIndex;
				if (this._headIndex >= num)
				{
					result = default(T);
					return false;
				}
				bool flag = false;
				bool flag2;
				try
				{
					this._currentOp = 2;
					Interlocked.Exchange(ref this._tailIndex, --num);
					if (!this._frozen && this._headIndex < num)
					{
						int num2 = num & this._mask;
						result = this._array[num2];
						this._array[num2] = default(T);
						this._addTakeCount--;
						flag2 = true;
					}
					else
					{
						this._currentOp = 0;
						Monitor.Enter(this, ref flag);
						if (this._headIndex <= num)
						{
							int num3 = num & this._mask;
							result = this._array[num3];
							this._array[num3] = default(T);
							this._addTakeCount--;
							flag2 = true;
						}
						else
						{
							this._tailIndex = num + 1;
							result = default(T);
							flag2 = false;
						}
					}
				}
				finally
				{
					this._currentOp = 0;
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
				return flag2;
			}

			// Token: 0x0600124B RID: 4683 RVA: 0x00052D8C File Offset: 0x00050F8C
			internal bool TrySteal(out T result, bool take)
			{
				lock (this)
				{
					int headIndex = this._headIndex;
					if (take)
					{
						if (headIndex < this._tailIndex - 1 && this._currentOp != 1)
						{
							SpinWait spinWait = default(SpinWait);
							do
							{
								spinWait.SpinOnce();
							}
							while (this._currentOp == 1);
						}
						Interlocked.Exchange(ref this._headIndex, headIndex + 1);
						if (headIndex < this._tailIndex)
						{
							int num = headIndex & this._mask;
							result = this._array[num];
							this._array[num] = default(T);
							this._stealCount++;
							return true;
						}
						this._headIndex = headIndex;
					}
					else if (headIndex < this._tailIndex)
					{
						result = this._array[headIndex & this._mask];
						return true;
					}
				}
				result = default(T);
				return false;
			}

			// Token: 0x0600124C RID: 4684 RVA: 0x00052EAC File Offset: 0x000510AC
			internal int DangerousCopyTo(T[] array, int arrayIndex)
			{
				int headIndex = this._headIndex;
				int dangerousCount = this.DangerousCount;
				for (int i = arrayIndex + dangerousCount - 1; i >= arrayIndex; i--)
				{
					array[i] = this._array[headIndex++ & this._mask];
				}
				return dangerousCount;
			}

			// Token: 0x170003CB RID: 971
			// (get) Token: 0x0600124D RID: 4685 RVA: 0x00052EFC File Offset: 0x000510FC
			internal int DangerousCount
			{
				get
				{
					return this._addTakeCount - this._stealCount;
				}
			}

			// Token: 0x04000B40 RID: 2880
			private volatile int _headIndex;

			// Token: 0x04000B41 RID: 2881
			private volatile int _tailIndex;

			// Token: 0x04000B42 RID: 2882
			private volatile T[] _array = new T[32];

			// Token: 0x04000B43 RID: 2883
			private volatile int _mask = 31;

			// Token: 0x04000B44 RID: 2884
			private int _addTakeCount;

			// Token: 0x04000B45 RID: 2885
			private int _stealCount;

			// Token: 0x04000B46 RID: 2886
			internal volatile int _currentOp;

			// Token: 0x04000B47 RID: 2887
			internal bool _frozen;

			// Token: 0x04000B48 RID: 2888
			internal readonly ConcurrentBag<T>.WorkStealingQueue _nextQueue;

			// Token: 0x04000B49 RID: 2889
			internal readonly int _ownerThreadId;
		}

		// Token: 0x020002F8 RID: 760
		[Serializable]
		private sealed class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600124E RID: 4686 RVA: 0x00052F0B File Offset: 0x0005110B
			public Enumerator(T[] array)
			{
				this._array = array;
			}

			// Token: 0x0600124F RID: 4687 RVA: 0x00052F1C File Offset: 0x0005111C
			public bool MoveNext()
			{
				if (this._index < this._array.Length)
				{
					T[] array = this._array;
					int index = this._index;
					this._index = index + 1;
					this._current = array[index];
					return true;
				}
				this._index = this._array.Length + 1;
				return false;
			}

			// Token: 0x170003CC RID: 972
			// (get) Token: 0x06001250 RID: 4688 RVA: 0x00052F6E File Offset: 0x0005116E
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170003CD RID: 973
			// (get) Token: 0x06001251 RID: 4689 RVA: 0x00052F76 File Offset: 0x00051176
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._array.Length + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.Current;
				}
			}

			// Token: 0x06001252 RID: 4690 RVA: 0x00052FA8 File Offset: 0x000511A8
			public void Reset()
			{
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x06001253 RID: 4691 RVA: 0x00002FA0 File Offset: 0x000011A0
			public void Dispose()
			{
			}

			// Token: 0x04000B4A RID: 2890
			private readonly T[] _array;

			// Token: 0x04000B4B RID: 2891
			private T _current;

			// Token: 0x04000B4C RID: 2892
			private int _index;
		}
	}
}
