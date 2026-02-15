using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Net
{
	// Token: 0x020003D2 RID: 978
	internal static class TimerThread
	{
		// Token: 0x0600184F RID: 6223 RVA: 0x00067430 File Offset: 0x00065630
		static TimerThread()
		{
			AppDomain.CurrentDomain.DomainUnload += TimerThread.OnDomainUnload;
		}

		// Token: 0x06001850 RID: 6224 RVA: 0x000674A8 File Offset: 0x000656A8
		internal static TimerThread.Queue CreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			LinkedList<WeakReference> linkedList = TimerThread.s_NewQueues;
			TimerThread.TimerQueue timerQueue;
			lock (linkedList)
			{
				timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
				WeakReference weakReference = new WeakReference(timerQueue);
				TimerThread.s_NewQueues.AddLast(weakReference);
			}
			return timerQueue;
		}

		// Token: 0x06001851 RID: 6225 RVA: 0x00067518 File Offset: 0x00065718
		internal static TimerThread.Queue GetOrCreateQueue(int durationMilliseconds)
		{
			if (durationMilliseconds == -1)
			{
				return new TimerThread.InfiniteTimerQueue();
			}
			if (durationMilliseconds < 0)
			{
				throw new ArgumentOutOfRangeException("durationMilliseconds");
			}
			WeakReference weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
			TimerThread.TimerQueue timerQueue;
			if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
			{
				LinkedList<WeakReference> linkedList = TimerThread.s_NewQueues;
				lock (linkedList)
				{
					weakReference = (WeakReference)TimerThread.s_QueuesCache[durationMilliseconds];
					if (weakReference == null || (timerQueue = (TimerThread.TimerQueue)weakReference.Target) == null)
					{
						timerQueue = new TimerThread.TimerQueue(durationMilliseconds);
						weakReference = new WeakReference(timerQueue);
						TimerThread.s_NewQueues.AddLast(weakReference);
						TimerThread.s_QueuesCache[durationMilliseconds] = weakReference;
						if (++TimerThread.s_CacheScanIteration % 32 == 0)
						{
							List<int> list = new List<int>();
							foreach (object obj in TimerThread.s_QueuesCache)
							{
								DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
								if (((WeakReference)dictionaryEntry.Value).Target == null)
								{
									list.Add((int)dictionaryEntry.Key);
								}
							}
							for (int i = 0; i < list.Count; i++)
							{
								TimerThread.s_QueuesCache.Remove(list[i]);
							}
						}
					}
				}
			}
			return timerQueue;
		}

		// Token: 0x06001852 RID: 6226 RVA: 0x000676BC File Offset: 0x000658BC
		private static void Prod()
		{
			TimerThread.s_ThreadReadyEvent.Set();
			if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) == 0)
			{
				new Thread(new ThreadStart(TimerThread.ThreadProc)).Start();
			}
		}

		// Token: 0x06001853 RID: 6227 RVA: 0x000676F0 File Offset: 0x000658F0
		private static void ThreadProc()
		{
			Thread.CurrentThread.IsBackground = true;
			LinkedList<WeakReference> linkedList = TimerThread.s_Queues;
			lock (linkedList)
			{
				if (Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 1) == 1)
				{
					bool flag2 = true;
					while (flag2)
					{
						try
						{
							TimerThread.s_ThreadReadyEvent.Reset();
							for (;;)
							{
								if (TimerThread.s_NewQueues.Count > 0)
								{
									LinkedList<WeakReference> linkedList2 = TimerThread.s_NewQueues;
									lock (linkedList2)
									{
										for (LinkedListNode<WeakReference> linkedListNode = TimerThread.s_NewQueues.First; linkedListNode != null; linkedListNode = TimerThread.s_NewQueues.First)
										{
											TimerThread.s_NewQueues.Remove(linkedListNode);
											TimerThread.s_Queues.AddLast(linkedListNode);
										}
									}
								}
								int tickCount = Environment.TickCount;
								int num = 0;
								bool flag4 = false;
								LinkedListNode<WeakReference> linkedListNode2 = TimerThread.s_Queues.First;
								while (linkedListNode2 != null)
								{
									TimerThread.TimerQueue timerQueue = (TimerThread.TimerQueue)linkedListNode2.Value.Target;
									if (timerQueue == null)
									{
										LinkedListNode<WeakReference> next = linkedListNode2.Next;
										TimerThread.s_Queues.Remove(linkedListNode2);
										linkedListNode2 = next;
									}
									else
									{
										int num2;
										if (timerQueue.Fire(out num2) && (!flag4 || TimerThread.IsTickBetween(tickCount, num, num2)))
										{
											num = num2;
											flag4 = true;
										}
										linkedListNode2 = linkedListNode2.Next;
									}
								}
								int tickCount2 = Environment.TickCount;
								int num3 = (int)(flag4 ? (TimerThread.IsTickBetween(tickCount, num, tickCount2) ? (Math.Min((uint)(num - tickCount2), 2147483632U) + 15U) : 0U) : 30000U);
								int num4 = WaitHandle.WaitAny(TimerThread.s_ThreadEvents, num3, false);
								if (num4 == 0)
								{
									break;
								}
								if (num4 == 258 && !flag4)
								{
									Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 0, 1);
									if (!TimerThread.s_ThreadReadyEvent.WaitOne(0, false) || Interlocked.CompareExchange(ref TimerThread.s_ThreadState, 1, 0) != 0)
									{
										goto IL_01A8;
									}
								}
							}
							flag2 = false;
							continue;
							IL_01A8:
							flag2 = false;
						}
						catch (Exception ex)
						{
							if (NclUtilities.IsFatal(ex))
							{
								throw;
							}
							bool on = Logging.On;
							Thread.Sleep(1000);
						}
					}
				}
			}
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x00067924 File Offset: 0x00065B24
		private static void StopTimerThread()
		{
			Interlocked.Exchange(ref TimerThread.s_ThreadState, 2);
			TimerThread.s_ThreadShutdownEvent.Set();
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0006793D File Offset: 0x00065B3D
		private static bool IsTickBetween(int start, int end, int comparand)
		{
			return start <= comparand == end <= comparand != start <= end;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0006795C File Offset: 0x00065B5C
		private static void OnDomainUnload(object sender, EventArgs e)
		{
			try
			{
				TimerThread.StopTimerThread();
			}
			catch
			{
			}
		}

		// Token: 0x04000F66 RID: 3942
		private static LinkedList<WeakReference> s_Queues = new LinkedList<WeakReference>();

		// Token: 0x04000F67 RID: 3943
		private static LinkedList<WeakReference> s_NewQueues = new LinkedList<WeakReference>();

		// Token: 0x04000F68 RID: 3944
		private static int s_ThreadState = 0;

		// Token: 0x04000F69 RID: 3945
		private static AutoResetEvent s_ThreadReadyEvent = new AutoResetEvent(false);

		// Token: 0x04000F6A RID: 3946
		private static ManualResetEvent s_ThreadShutdownEvent = new ManualResetEvent(false);

		// Token: 0x04000F6B RID: 3947
		private static WaitHandle[] s_ThreadEvents = new WaitHandle[]
		{
			TimerThread.s_ThreadShutdownEvent,
			TimerThread.s_ThreadReadyEvent
		};

		// Token: 0x04000F6C RID: 3948
		private static int s_CacheScanIteration;

		// Token: 0x04000F6D RID: 3949
		private static Hashtable s_QueuesCache = new Hashtable();

		// Token: 0x020003D3 RID: 979
		internal abstract class Queue
		{
			// Token: 0x06001857 RID: 6231 RVA: 0x00067984 File Offset: 0x00065B84
			internal Queue(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
			}

			// Token: 0x17000531 RID: 1329
			// (get) Token: 0x06001858 RID: 6232 RVA: 0x00067993 File Offset: 0x00065B93
			internal int Duration
			{
				get
				{
					return this.m_DurationMilliseconds;
				}
			}

			// Token: 0x06001859 RID: 6233
			internal abstract TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context);

			// Token: 0x04000F6E RID: 3950
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x020003D4 RID: 980
		internal abstract class Timer : IDisposable
		{
			// Token: 0x0600185A RID: 6234 RVA: 0x0006799B File Offset: 0x00065B9B
			internal Timer(int durationMilliseconds)
			{
				this.m_DurationMilliseconds = durationMilliseconds;
				this.m_StartTimeMilliseconds = Environment.TickCount;
			}

			// Token: 0x17000532 RID: 1330
			// (get) Token: 0x0600185B RID: 6235 RVA: 0x000679B5 File Offset: 0x00065BB5
			internal int StartTime
			{
				get
				{
					return this.m_StartTimeMilliseconds;
				}
			}

			// Token: 0x17000533 RID: 1331
			// (get) Token: 0x0600185C RID: 6236 RVA: 0x000679BD File Offset: 0x00065BBD
			internal int Expiration
			{
				get
				{
					return this.m_StartTimeMilliseconds + this.m_DurationMilliseconds;
				}
			}

			// Token: 0x0600185D RID: 6237
			internal abstract bool Cancel();

			// Token: 0x17000534 RID: 1332
			// (get) Token: 0x0600185E RID: 6238
			internal abstract bool HasExpired { get; }

			// Token: 0x0600185F RID: 6239 RVA: 0x000679CC File Offset: 0x00065BCC
			public void Dispose()
			{
				this.Cancel();
			}

			// Token: 0x04000F6F RID: 3951
			private readonly int m_StartTimeMilliseconds;

			// Token: 0x04000F70 RID: 3952
			private readonly int m_DurationMilliseconds;
		}

		// Token: 0x020003D5 RID: 981
		// (Invoke) Token: 0x06001861 RID: 6241
		internal delegate void Callback(TimerThread.Timer timer, int timeNoticed, object context);

		// Token: 0x020003D6 RID: 982
		private class TimerQueue : TimerThread.Queue
		{
			// Token: 0x06001862 RID: 6242 RVA: 0x000679D5 File Offset: 0x00065BD5
			internal TimerQueue(int durationMilliseconds)
				: base(durationMilliseconds)
			{
				this.m_Timers = new TimerThread.TimerNode();
				this.m_Timers.Next = this.m_Timers;
				this.m_Timers.Prev = this.m_Timers;
			}

			// Token: 0x06001863 RID: 6243 RVA: 0x00067A0C File Offset: 0x00065C0C
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				TimerThread.TimerNode timerNode = new TimerThread.TimerNode(callback, context, base.Duration, this.m_Timers);
				bool flag = false;
				TimerThread.TimerNode timers = this.m_Timers;
				lock (timers)
				{
					if (this.m_Timers.Next == this.m_Timers)
					{
						if (this.m_ThisHandle == IntPtr.Zero)
						{
							this.m_ThisHandle = (IntPtr)GCHandle.Alloc(this);
						}
						flag = true;
					}
					timerNode.Next = this.m_Timers;
					timerNode.Prev = this.m_Timers.Prev;
					this.m_Timers.Prev.Next = timerNode;
					this.m_Timers.Prev = timerNode;
				}
				if (flag)
				{
					TimerThread.Prod();
				}
				return timerNode;
			}

			// Token: 0x06001864 RID: 6244 RVA: 0x00067AD8 File Offset: 0x00065CD8
			internal bool Fire(out int nextExpiration)
			{
				TimerThread.TimerNode timerNode;
				do
				{
					timerNode = this.m_Timers.Next;
					if (timerNode == this.m_Timers)
					{
						TimerThread.TimerNode timers = this.m_Timers;
						lock (timers)
						{
							timerNode = this.m_Timers.Next;
							if (timerNode == this.m_Timers)
							{
								if (this.m_ThisHandle != IntPtr.Zero)
								{
									((GCHandle)this.m_ThisHandle).Free();
									this.m_ThisHandle = IntPtr.Zero;
								}
								nextExpiration = 0;
								return false;
							}
						}
					}
				}
				while (timerNode.Fire());
				nextExpiration = timerNode.Expiration;
				return true;
			}

			// Token: 0x04000F71 RID: 3953
			private IntPtr m_ThisHandle;

			// Token: 0x04000F72 RID: 3954
			private readonly TimerThread.TimerNode m_Timers;
		}

		// Token: 0x020003D7 RID: 983
		private class InfiniteTimerQueue : TimerThread.Queue
		{
			// Token: 0x06001865 RID: 6245 RVA: 0x00067B8C File Offset: 0x00065D8C
			internal InfiniteTimerQueue()
				: base(-1)
			{
			}

			// Token: 0x06001866 RID: 6246 RVA: 0x00067B95 File Offset: 0x00065D95
			internal override TimerThread.Timer CreateTimer(TimerThread.Callback callback, object context)
			{
				return new TimerThread.InfiniteTimer();
			}
		}

		// Token: 0x020003D8 RID: 984
		private class TimerNode : TimerThread.Timer
		{
			// Token: 0x06001867 RID: 6247 RVA: 0x00067B9C File Offset: 0x00065D9C
			internal TimerNode(TimerThread.Callback callback, object context, int durationMilliseconds, object queueLock)
				: base(durationMilliseconds)
			{
				if (callback != null)
				{
					this.m_Callback = callback;
					this.m_Context = context;
				}
				this.m_TimerState = TimerThread.TimerNode.TimerState.Ready;
				this.m_QueueLock = queueLock;
			}

			// Token: 0x06001868 RID: 6248 RVA: 0x00067BC5 File Offset: 0x00065DC5
			internal TimerNode()
				: base(0)
			{
				this.m_TimerState = TimerThread.TimerNode.TimerState.Sentinel;
			}

			// Token: 0x17000535 RID: 1333
			// (get) Token: 0x06001869 RID: 6249 RVA: 0x00067BD5 File Offset: 0x00065DD5
			internal override bool HasExpired
			{
				get
				{
					return this.m_TimerState == TimerThread.TimerNode.TimerState.Fired;
				}
			}

			// Token: 0x17000536 RID: 1334
			// (get) Token: 0x0600186A RID: 6250 RVA: 0x00067BE0 File Offset: 0x00065DE0
			// (set) Token: 0x0600186B RID: 6251 RVA: 0x00067BE8 File Offset: 0x00065DE8
			internal TimerThread.TimerNode Next
			{
				get
				{
					return this.next;
				}
				set
				{
					this.next = value;
				}
			}

			// Token: 0x17000537 RID: 1335
			// (get) Token: 0x0600186C RID: 6252 RVA: 0x00067BF1 File Offset: 0x00065DF1
			// (set) Token: 0x0600186D RID: 6253 RVA: 0x00067BF9 File Offset: 0x00065DF9
			internal TimerThread.TimerNode Prev
			{
				get
				{
					return this.prev;
				}
				set
				{
					this.prev = value;
				}
			}

			// Token: 0x0600186E RID: 6254 RVA: 0x00067C04 File Offset: 0x00065E04
			internal override bool Cancel()
			{
				if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
				{
					object queueLock = this.m_QueueLock;
					lock (queueLock)
					{
						if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
						{
							this.Next.Prev = this.Prev;
							this.Prev.Next = this.Next;
							this.Next = null;
							this.Prev = null;
							this.m_Callback = null;
							this.m_Context = null;
							this.m_TimerState = TimerThread.TimerNode.TimerState.Cancelled;
							return true;
						}
					}
					return false;
				}
				return false;
			}

			// Token: 0x0600186F RID: 6255 RVA: 0x00067C9C File Offset: 0x00065E9C
			internal bool Fire()
			{
				if (this.m_TimerState != TimerThread.TimerNode.TimerState.Ready)
				{
					return true;
				}
				int tickCount = Environment.TickCount;
				if (TimerThread.IsTickBetween(base.StartTime, base.Expiration, tickCount))
				{
					return false;
				}
				bool flag = false;
				object queueLock = this.m_QueueLock;
				lock (queueLock)
				{
					if (this.m_TimerState == TimerThread.TimerNode.TimerState.Ready)
					{
						this.m_TimerState = TimerThread.TimerNode.TimerState.Fired;
						this.Next.Prev = this.Prev;
						this.Prev.Next = this.Next;
						this.Next = null;
						this.Prev = null;
						flag = this.m_Callback != null;
					}
				}
				if (flag)
				{
					try
					{
						TimerThread.Callback callback = this.m_Callback;
						object context = this.m_Context;
						this.m_Callback = null;
						this.m_Context = null;
						callback(this, tickCount, context);
					}
					catch (Exception ex)
					{
						if (NclUtilities.IsFatal(ex))
						{
							throw;
						}
						bool on = Logging.On;
					}
				}
				return true;
			}

			// Token: 0x04000F73 RID: 3955
			private TimerThread.TimerNode.TimerState m_TimerState;

			// Token: 0x04000F74 RID: 3956
			private TimerThread.Callback m_Callback;

			// Token: 0x04000F75 RID: 3957
			private object m_Context;

			// Token: 0x04000F76 RID: 3958
			private object m_QueueLock;

			// Token: 0x04000F77 RID: 3959
			private TimerThread.TimerNode next;

			// Token: 0x04000F78 RID: 3960
			private TimerThread.TimerNode prev;

			// Token: 0x020003D9 RID: 985
			private enum TimerState
			{
				// Token: 0x04000F7A RID: 3962
				Ready,
				// Token: 0x04000F7B RID: 3963
				Fired,
				// Token: 0x04000F7C RID: 3964
				Cancelled,
				// Token: 0x04000F7D RID: 3965
				Sentinel
			}
		}

		// Token: 0x020003DA RID: 986
		private class InfiniteTimer : TimerThread.Timer
		{
			// Token: 0x06001870 RID: 6256 RVA: 0x00067D90 File Offset: 0x00065F90
			internal InfiniteTimer()
				: base(-1)
			{
			}

			// Token: 0x17000538 RID: 1336
			// (get) Token: 0x06001871 RID: 6257 RVA: 0x000028AE File Offset: 0x00000AAE
			internal override bool HasExpired
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06001872 RID: 6258 RVA: 0x00067D99 File Offset: 0x00065F99
			internal override bool Cancel()
			{
				return Interlocked.Exchange(ref this.cancelled, 1) == 0;
			}

			// Token: 0x04000F7E RID: 3966
			private int cancelled;
		}
	}
}
