using System;
using System.Runtime.ConstrainedExecution;

namespace System.Threading
{
	// Token: 0x0200026B RID: 619
	internal sealed class ThreadPoolWorkQueue
	{
		// Token: 0x060016F1 RID: 5873 RVA: 0x00059A14 File Offset: 0x00057C14
		public ThreadPoolWorkQueue()
		{
			this.queueTail = (this.queueHead = new ThreadPoolWorkQueue.QueueSegment());
		}

		// Token: 0x060016F2 RID: 5874 RVA: 0x00059A3F File Offset: 0x00057C3F
		public ThreadPoolWorkQueueThreadLocals EnsureCurrentThreadHasQueue()
		{
			if (ThreadPoolWorkQueueThreadLocals.threadLocals == null)
			{
				ThreadPoolWorkQueueThreadLocals.threadLocals = new ThreadPoolWorkQueueThreadLocals(this);
			}
			return ThreadPoolWorkQueueThreadLocals.threadLocals;
		}

		// Token: 0x060016F3 RID: 5875 RVA: 0x00059A58 File Offset: 0x00057C58
		internal void EnsureThreadRequested()
		{
			int num;
			for (int i = this.numOutstandingThreadRequests; i < ThreadPoolGlobals.processorCount; i = num)
			{
				num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, i + 1, i);
				if (num == i)
				{
					ThreadPool.RequestWorkerThread();
					return;
				}
			}
		}

		// Token: 0x060016F4 RID: 5876 RVA: 0x00059A98 File Offset: 0x00057C98
		internal void MarkThreadRequestSatisfied()
		{
			int num;
			for (int i = this.numOutstandingThreadRequests; i > 0; i = num)
			{
				num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, i - 1, i);
				if (num == i)
				{
					break;
				}
			}
		}

		// Token: 0x060016F5 RID: 5877 RVA: 0x00059ACC File Offset: 0x00057CCC
		public void Enqueue(IThreadPoolWorkItem callback, bool forceGlobal)
		{
			ThreadPoolWorkQueueThreadLocals threadPoolWorkQueueThreadLocals = null;
			if (!forceGlobal)
			{
				threadPoolWorkQueueThreadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
			}
			if (threadPoolWorkQueueThreadLocals != null)
			{
				threadPoolWorkQueueThreadLocals.workStealingQueue.LocalPush(callback);
			}
			else
			{
				ThreadPoolWorkQueue.QueueSegment queueSegment = this.queueHead;
				while (!queueSegment.TryEnqueue(callback))
				{
					Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref queueSegment.Next, new ThreadPoolWorkQueue.QueueSegment(), null);
					while (queueSegment.Next != null)
					{
						Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref this.queueHead, queueSegment.Next, queueSegment);
						queueSegment = this.queueHead;
					}
				}
			}
			ThreadPool.NotifyWorkItemQueued();
			this.EnsureThreadRequested();
		}

		// Token: 0x060016F6 RID: 5878 RVA: 0x00059B50 File Offset: 0x00057D50
		internal bool LocalFindAndPop(IThreadPoolWorkItem callback)
		{
			ThreadPoolWorkQueueThreadLocals threadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
			return threadLocals != null && threadLocals.workStealingQueue.LocalFindAndPop(callback);
		}

		// Token: 0x060016F7 RID: 5879 RVA: 0x00059B74 File Offset: 0x00057D74
		public void Dequeue(ThreadPoolWorkQueueThreadLocals tl, out IThreadPoolWorkItem callback, out bool missedSteal)
		{
			callback = null;
			missedSteal = false;
			ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue = tl.workStealingQueue;
			workStealingQueue.LocalPop(out callback);
			if (callback == null)
			{
				ThreadPoolWorkQueue.QueueSegment queueSegment = this.queueTail;
				while (!queueSegment.TryDequeue(out callback) && queueSegment.Next != null && queueSegment.IsUsedUp())
				{
					Interlocked.CompareExchange<ThreadPoolWorkQueue.QueueSegment>(ref this.queueTail, queueSegment.Next, queueSegment);
					queueSegment = this.queueTail;
				}
			}
			if (callback == null)
			{
				ThreadPoolWorkQueue.WorkStealingQueue[] array = ThreadPoolWorkQueue.allThreadQueues.Current;
				int num = tl.random.Next(array.Length);
				for (int i = array.Length; i > 0; i--)
				{
					ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue2 = Volatile.Read<ThreadPoolWorkQueue.WorkStealingQueue>(ref array[num % array.Length]);
					if (workStealingQueue2 != null && workStealingQueue2 != workStealingQueue && workStealingQueue2.TrySteal(out callback, ref missedSteal))
					{
						break;
					}
					num++;
				}
			}
		}

		// Token: 0x060016F8 RID: 5880 RVA: 0x00059C38 File Offset: 0x00057E38
		internal static bool Dispatch()
		{
			ThreadPoolWorkQueue workQueue = ThreadPoolGlobals.workQueue;
			int tickCount = Environment.TickCount;
			workQueue.MarkThreadRequestSatisfied();
			bool flag = true;
			IThreadPoolWorkItem threadPoolWorkItem = null;
			try
			{
				ThreadPoolWorkQueueThreadLocals threadPoolWorkQueueThreadLocals = workQueue.EnsureCurrentThreadHasQueue();
				while ((long)(Environment.TickCount - tickCount) < 30L)
				{
					try
					{
					}
					finally
					{
						bool flag2 = false;
						workQueue.Dequeue(threadPoolWorkQueueThreadLocals, out threadPoolWorkItem, out flag2);
						if (threadPoolWorkItem == null)
						{
							flag = flag2;
						}
						else
						{
							workQueue.EnsureThreadRequested();
						}
					}
					if (threadPoolWorkItem == null)
					{
						return true;
					}
					if (ThreadPoolGlobals.enableWorkerTracking)
					{
						bool flag3 = false;
						try
						{
							try
							{
							}
							finally
							{
								ThreadPool.ReportThreadStatus(true);
								flag3 = true;
							}
							threadPoolWorkItem.ExecuteWorkItem();
							threadPoolWorkItem = null;
							goto IL_007C;
						}
						finally
						{
							if (flag3)
							{
								ThreadPool.ReportThreadStatus(false);
							}
						}
						goto IL_0074;
					}
					goto IL_0074;
					IL_007C:
					if (!ThreadPool.NotifyWorkItemComplete())
					{
						return false;
					}
					continue;
					IL_0074:
					threadPoolWorkItem.ExecuteWorkItem();
					threadPoolWorkItem = null;
					goto IL_007C;
				}
				return true;
			}
			catch (ThreadAbortException ex)
			{
				if (threadPoolWorkItem != null)
				{
					threadPoolWorkItem.MarkAborted(ex);
				}
				flag = false;
			}
			finally
			{
				if (flag)
				{
					workQueue.EnsureThreadRequested();
				}
			}
			return true;
		}

		// Token: 0x04000AF8 RID: 2808
		internal volatile ThreadPoolWorkQueue.QueueSegment queueHead;

		// Token: 0x04000AF9 RID: 2809
		internal volatile ThreadPoolWorkQueue.QueueSegment queueTail;

		// Token: 0x04000AFA RID: 2810
		internal static ThreadPoolWorkQueue.SparseArray<ThreadPoolWorkQueue.WorkStealingQueue> allThreadQueues = new ThreadPoolWorkQueue.SparseArray<ThreadPoolWorkQueue.WorkStealingQueue>(16);

		// Token: 0x04000AFB RID: 2811
		private volatile int numOutstandingThreadRequests;

		// Token: 0x0200026C RID: 620
		internal class SparseArray<T> where T : class
		{
			// Token: 0x060016FA RID: 5882 RVA: 0x00059D4E File Offset: 0x00057F4E
			internal SparseArray(int initialSize)
			{
				this.m_array = new T[initialSize];
			}

			// Token: 0x17000276 RID: 630
			// (get) Token: 0x060016FB RID: 5883 RVA: 0x00059D64 File Offset: 0x00057F64
			internal T[] Current
			{
				get
				{
					return this.m_array;
				}
			}

			// Token: 0x060016FC RID: 5884 RVA: 0x00059D70 File Offset: 0x00057F70
			internal int Add(T e)
			{
				for (;;)
				{
					T[] array = this.m_array;
					T[] array2 = array;
					lock (array2)
					{
						for (int i = 0; i < array.Length; i++)
						{
							if (array[i] == null)
							{
								Volatile.Write<T>(ref array[i], e);
								return i;
							}
							if (i == array.Length - 1 && array == this.m_array)
							{
								T[] array3 = new T[array.Length * 2];
								Array.Copy(array, array3, i + 1);
								array3[i + 1] = e;
								this.m_array = array3;
								return i + 1;
							}
						}
						continue;
					}
					break;
				}
				int num;
				return num;
			}

			// Token: 0x060016FD RID: 5885 RVA: 0x00059E28 File Offset: 0x00058028
			internal void Remove(T e)
			{
				T[] array = this.m_array;
				lock (array)
				{
					for (int i = 0; i < this.m_array.Length; i++)
					{
						if (this.m_array[i] == e)
						{
							Volatile.Write<T>(ref this.m_array[i], default(T));
							break;
						}
					}
				}
			}

			// Token: 0x04000AFC RID: 2812
			private volatile T[] m_array;
		}

		// Token: 0x0200026D RID: 621
		internal class WorkStealingQueue
		{
			// Token: 0x060016FE RID: 5886 RVA: 0x00059EB4 File Offset: 0x000580B4
			public void LocalPush(IThreadPoolWorkItem obj)
			{
				int num = this.m_tailIndex;
				if (num == 2147483647)
				{
					bool flag = false;
					try
					{
						this.m_foreignLock.Enter(ref flag);
						if (this.m_tailIndex == 2147483647)
						{
							this.m_headIndex &= this.m_mask;
							num = (this.m_tailIndex &= this.m_mask);
						}
					}
					finally
					{
						if (flag)
						{
							this.m_foreignLock.Exit(true);
						}
					}
				}
				if (num < this.m_headIndex + this.m_mask)
				{
					Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[num & this.m_mask], obj);
					this.m_tailIndex = num + 1;
					return;
				}
				bool flag2 = false;
				try
				{
					this.m_foreignLock.Enter(ref flag2);
					int headIndex = this.m_headIndex;
					int num2 = this.m_tailIndex - this.m_headIndex;
					if (num2 >= this.m_mask)
					{
						IThreadPoolWorkItem[] array = new IThreadPoolWorkItem[this.m_array.Length << 1];
						for (int i = 0; i < this.m_array.Length; i++)
						{
							array[i] = this.m_array[(i + headIndex) & this.m_mask];
						}
						this.m_array = array;
						this.m_headIndex = 0;
						num = (this.m_tailIndex = num2);
						this.m_mask = (this.m_mask << 1) | 1;
					}
					Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[num & this.m_mask], obj);
					this.m_tailIndex = num + 1;
				}
				finally
				{
					if (flag2)
					{
						this.m_foreignLock.Exit(false);
					}
				}
			}

			// Token: 0x060016FF RID: 5887 RVA: 0x0005A07C File Offset: 0x0005827C
			public bool LocalFindAndPop(IThreadPoolWorkItem obj)
			{
				if (this.m_array[(this.m_tailIndex - 1) & this.m_mask] == obj)
				{
					IThreadPoolWorkItem threadPoolWorkItem;
					return this.LocalPop(out threadPoolWorkItem);
				}
				for (int i = this.m_tailIndex - 2; i >= this.m_headIndex; i--)
				{
					if (this.m_array[i & this.m_mask] == obj)
					{
						bool flag = false;
						try
						{
							this.m_foreignLock.Enter(ref flag);
							if (this.m_array[i & this.m_mask] == null)
							{
								return false;
							}
							Volatile.Write<IThreadPoolWorkItem>(ref this.m_array[i & this.m_mask], null);
							if (i == this.m_tailIndex)
							{
								this.m_tailIndex--;
							}
							else if (i == this.m_headIndex)
							{
								this.m_headIndex++;
							}
							return true;
						}
						finally
						{
							if (flag)
							{
								this.m_foreignLock.Exit(false);
							}
						}
					}
				}
				return false;
			}

			// Token: 0x06001700 RID: 5888 RVA: 0x0005A19C File Offset: 0x0005839C
			public bool LocalPop(out IThreadPoolWorkItem obj)
			{
				int num3;
				for (;;)
				{
					int num = this.m_tailIndex;
					if (this.m_headIndex >= num)
					{
						break;
					}
					num--;
					Interlocked.Exchange(ref this.m_tailIndex, num);
					if (this.m_headIndex > num)
					{
						bool flag = false;
						bool flag2;
						try
						{
							this.m_foreignLock.Enter(ref flag);
							if (this.m_headIndex <= num)
							{
								int num2 = num & this.m_mask;
								obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num2]);
								if (obj == null)
								{
									continue;
								}
								this.m_array[num2] = null;
								flag2 = true;
							}
							else
							{
								this.m_tailIndex = num + 1;
								obj = null;
								flag2 = false;
							}
						}
						finally
						{
							if (flag)
							{
								this.m_foreignLock.Exit(false);
							}
						}
						return flag2;
					}
					num3 = num & this.m_mask;
					obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num3]);
					if (obj != null)
					{
						goto Block_2;
					}
				}
				obj = null;
				return false;
				Block_2:
				this.m_array[num3] = null;
				return true;
			}

			// Token: 0x06001701 RID: 5889 RVA: 0x0005A298 File Offset: 0x00058498
			public bool TrySteal(out IThreadPoolWorkItem obj, ref bool missedSteal)
			{
				return this.TrySteal(out obj, ref missedSteal, 0);
			}

			// Token: 0x06001702 RID: 5890 RVA: 0x0005A2A4 File Offset: 0x000584A4
			private bool TrySteal(out IThreadPoolWorkItem obj, ref bool missedSteal, int millisecondsTimeout)
			{
				obj = null;
				while (this.m_headIndex < this.m_tailIndex)
				{
					bool flag = false;
					try
					{
						this.m_foreignLock.TryEnter(millisecondsTimeout, ref flag);
						if (flag)
						{
							int headIndex = this.m_headIndex;
							Interlocked.Exchange(ref this.m_headIndex, headIndex + 1);
							if (headIndex < this.m_tailIndex)
							{
								int num = headIndex & this.m_mask;
								obj = Volatile.Read<IThreadPoolWorkItem>(ref this.m_array[num]);
								if (obj == null)
								{
									continue;
								}
								this.m_array[num] = null;
								return true;
							}
							else
							{
								this.m_headIndex = headIndex;
								obj = null;
								missedSteal = true;
							}
						}
						else
						{
							missedSteal = true;
						}
					}
					finally
					{
						if (flag)
						{
							this.m_foreignLock.Exit(false);
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x04000AFD RID: 2813
			private const int INITIAL_SIZE = 32;

			// Token: 0x04000AFE RID: 2814
			internal volatile IThreadPoolWorkItem[] m_array = new IThreadPoolWorkItem[32];

			// Token: 0x04000AFF RID: 2815
			private volatile int m_mask = 31;

			// Token: 0x04000B00 RID: 2816
			private const int START_INDEX = 0;

			// Token: 0x04000B01 RID: 2817
			private volatile int m_headIndex;

			// Token: 0x04000B02 RID: 2818
			private volatile int m_tailIndex;

			// Token: 0x04000B03 RID: 2819
			private SpinLock m_foreignLock = new SpinLock(false);
		}

		// Token: 0x0200026E RID: 622
		internal class QueueSegment
		{
			// Token: 0x06001704 RID: 5892 RVA: 0x0005A39C File Offset: 0x0005859C
			private void GetIndexes(out int upper, out int lower)
			{
				int num = this.indexes;
				upper = (num >> 16) & 65535;
				lower = num & 65535;
			}

			// Token: 0x06001705 RID: 5893 RVA: 0x0005A3C8 File Offset: 0x000585C8
			private bool CompareExchangeIndexes(ref int prevUpper, int newUpper, ref int prevLower, int newLower)
			{
				int num = (prevUpper << 16) | (prevLower & 65535);
				int num2 = (newUpper << 16) | (newLower & 65535);
				int num3 = Interlocked.CompareExchange(ref this.indexes, num2, num);
				prevUpper = (num3 >> 16) & 65535;
				prevLower = num3 & 65535;
				return num3 == num;
			}

			// Token: 0x06001706 RID: 5894 RVA: 0x0005A419 File Offset: 0x00058619
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			public QueueSegment()
			{
				this.nodes = new IThreadPoolWorkItem[256];
			}

			// Token: 0x06001707 RID: 5895 RVA: 0x0005A434 File Offset: 0x00058634
			public bool IsUsedUp()
			{
				int num;
				int num2;
				this.GetIndexes(out num, out num2);
				return num == this.nodes.Length && num2 == this.nodes.Length;
			}

			// Token: 0x06001708 RID: 5896 RVA: 0x0005A464 File Offset: 0x00058664
			public bool TryEnqueue(IThreadPoolWorkItem node)
			{
				int num;
				int num2;
				this.GetIndexes(out num, out num2);
				while (num != this.nodes.Length)
				{
					if (this.CompareExchangeIndexes(ref num, num + 1, ref num2, num2))
					{
						Volatile.Write<IThreadPoolWorkItem>(ref this.nodes[num], node);
						return true;
					}
				}
				return false;
			}

			// Token: 0x06001709 RID: 5897 RVA: 0x0005A4AC File Offset: 0x000586AC
			public bool TryDequeue(out IThreadPoolWorkItem node)
			{
				int num;
				int num2;
				this.GetIndexes(out num, out num2);
				while (num2 != num)
				{
					if (this.CompareExchangeIndexes(ref num, num, ref num2, num2 + 1))
					{
						SpinWait spinWait = default(SpinWait);
						for (;;)
						{
							IThreadPoolWorkItem threadPoolWorkItem;
							node = (threadPoolWorkItem = Volatile.Read<IThreadPoolWorkItem>(ref this.nodes[num2]));
							if (threadPoolWorkItem != null)
							{
								break;
							}
							spinWait.SpinOnce();
						}
						this.nodes[num2] = null;
						return true;
					}
				}
				node = null;
				return false;
			}

			// Token: 0x04000B04 RID: 2820
			internal readonly IThreadPoolWorkItem[] nodes;

			// Token: 0x04000B05 RID: 2821
			private const int QueueSegmentLength = 256;

			// Token: 0x04000B06 RID: 2822
			private volatile int indexes;

			// Token: 0x04000B07 RID: 2823
			public volatile ThreadPoolWorkQueue.QueueSegment Next;

			// Token: 0x04000B08 RID: 2824
			private const int SixteenBits = 65535;
		}
	}
}
