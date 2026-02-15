using System;
using System.Collections;
using System.Threading;

namespace System.Windows.Forms
{
	// Token: 0x02000291 RID: 657
	internal class XEventQueue
	{
		// Token: 0x06001748 RID: 5960 RVA: 0x00074A64 File Offset: 0x00072C64
		public XEventQueue(Thread thread)
		{
			this.xqueue = new XEventQueue.XQueue(XEventQueue.InitialXEventSize);
			this.lqueue = new XEventQueue.XQueue(XEventQueue.InitialLXEventSize);
			this.paint = new XEventQueue.PaintQueue(XEventQueue.InitialPaintSize);
			this.timer_list = new ArrayList();
			this.thread = thread;
			this.dispatch_idle = true;
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001749 RID: 5961 RVA: 0x00074AC0 File Offset: 0x00072CC0
		public int Count
		{
			get
			{
				XEventQueue.XQueue xqueue = this.lqueue;
				int num;
				lock (xqueue)
				{
					num = this.xqueue.Count + this.lqueue.Count;
				}
				return num;
			}
		}

		// Token: 0x170005EE RID: 1518
		// (get) Token: 0x0600174A RID: 5962 RVA: 0x00074B14 File Offset: 0x00072D14
		public XEventQueue.PaintQueue Paint
		{
			get
			{
				return this.paint;
			}
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x00074B1C File Offset: 0x00072D1C
		public Thread Thread
		{
			get
			{
				return this.thread;
			}
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x00074B24 File Offset: 0x00072D24
		public void EnqueueLocked(XEvent xevent)
		{
			XEventQueue.XQueue xqueue = this.lqueue;
			lock (xqueue)
			{
				this.lqueue.Enqueue(xevent);
			}
		}

		// Token: 0x0600174D RID: 5965 RVA: 0x00074B6C File Offset: 0x00072D6C
		public XEvent Dequeue()
		{
			if (Thread.CurrentThread != this.thread)
			{
				Console.WriteLine("Hwnd.Queue.Dequeue called from a different thread without locking.");
				Console.WriteLine(Environment.StackTrace);
			}
			if (this.xqueue.Count == 0)
			{
				XEventQueue.XQueue xqueue = this.lqueue;
				lock (xqueue)
				{
					return this.lqueue.Dequeue();
				}
			}
			return this.xqueue.Dequeue();
		}

		// Token: 0x0600174E RID: 5966 RVA: 0x00074BF0 File Offset: 0x00072DF0
		public XEvent Peek()
		{
			if (Thread.CurrentThread != this.thread)
			{
				Console.WriteLine("Hwnd.Queue.Peek called from a different thread without locking.");
				Console.WriteLine(Environment.StackTrace);
			}
			if (this.xqueue.Count == 0)
			{
				XEventQueue.XQueue xqueue = this.lqueue;
				lock (xqueue)
				{
					return this.lqueue.Peek();
				}
			}
			return this.xqueue.Peek();
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x0600174F RID: 5967 RVA: 0x00074C74 File Offset: 0x00072E74
		// (set) Token: 0x06001750 RID: 5968 RVA: 0x00074C7C File Offset: 0x00072E7C
		public bool DispatchIdle
		{
			get
			{
				return this.dispatch_idle;
			}
			set
			{
				this.dispatch_idle = value;
			}
		}

		// Token: 0x04001214 RID: 4628
		private XEventQueue.XQueue xqueue;

		// Token: 0x04001215 RID: 4629
		private XEventQueue.XQueue lqueue;

		// Token: 0x04001216 RID: 4630
		private XEventQueue.PaintQueue paint;

		// Token: 0x04001217 RID: 4631
		internal ArrayList timer_list;

		// Token: 0x04001218 RID: 4632
		private Thread thread;

		// Token: 0x04001219 RID: 4633
		private bool dispatch_idle;

		// Token: 0x0400121A RID: 4634
		private static readonly int InitialXEventSize = 100;

		// Token: 0x0400121B RID: 4635
		private static readonly int InitialLXEventSize = 10;

		// Token: 0x0400121C RID: 4636
		private static readonly int InitialPaintSize = 50;

		// Token: 0x02000292 RID: 658
		public class PaintQueue
		{
			// Token: 0x06001752 RID: 5970 RVA: 0x00074C9C File Offset: 0x00072E9C
			public PaintQueue(int size)
			{
				this.hwnds = new ArrayList(size);
				this.xevent = default(XEvent);
				this.xevent.AnyEvent.type = XEventName.Expose;
			}

			// Token: 0x170005F1 RID: 1521
			// (get) Token: 0x06001753 RID: 5971 RVA: 0x00074CD0 File Offset: 0x00072ED0
			public int Count
			{
				get
				{
					ArrayList arrayList = this.hwnds;
					int count;
					lock (arrayList)
					{
						count = this.hwnds.Count;
					}
					return count;
				}
			}

			// Token: 0x06001754 RID: 5972 RVA: 0x00074D18 File Offset: 0x00072F18
			public void Enqueue(Hwnd hwnd)
			{
				ArrayList arrayList = this.hwnds;
				lock (arrayList)
				{
					this.hwnds.Add(hwnd);
				}
			}

			// Token: 0x06001755 RID: 5973 RVA: 0x00074D60 File Offset: 0x00072F60
			public void Remove(Hwnd hwnd)
			{
				if (!hwnd.expose_pending && !hwnd.nc_expose_pending)
				{
					ArrayList arrayList = this.hwnds;
					lock (arrayList)
					{
						this.hwnds.Remove(hwnd);
					}
				}
			}

			// Token: 0x06001756 RID: 5974 RVA: 0x00074DB8 File Offset: 0x00072FB8
			public XEvent Dequeue()
			{
				ArrayList arrayList = this.hwnds;
				XEvent xevent;
				lock (arrayList)
				{
					if (this.hwnds.Count == 0)
					{
						this.xevent.ExposeEvent.window = IntPtr.Zero;
						xevent = this.xevent;
					}
					else
					{
						IEnumerator enumerator = this.hwnds.GetEnumerator();
						enumerator.MoveNext();
						Hwnd hwnd = (Hwnd)enumerator.Current;
						if (!hwnd.nc_expose_pending || !hwnd.expose_pending)
						{
							this.hwnds.Remove(hwnd);
						}
						if (hwnd.expose_pending)
						{
							this.xevent.ExposeEvent.window = hwnd.client_window;
							xevent = this.xevent;
						}
						else
						{
							this.xevent.ExposeEvent.window = hwnd.whole_window;
							this.xevent.ExposeEvent.x = hwnd.nc_invalid.X;
							this.xevent.ExposeEvent.y = hwnd.nc_invalid.Y;
							this.xevent.ExposeEvent.width = hwnd.nc_invalid.Width;
							this.xevent.ExposeEvent.height = hwnd.nc_invalid.Height;
							xevent = this.xevent;
						}
					}
				}
				return xevent;
			}

			// Token: 0x0400121D RID: 4637
			private ArrayList hwnds;

			// Token: 0x0400121E RID: 4638
			private XEvent xevent;
		}

		// Token: 0x02000293 RID: 659
		private class XQueue
		{
			// Token: 0x06001757 RID: 5975 RVA: 0x00074F1C File Offset: 0x0007311C
			public XQueue(int size)
			{
				this.xevents = new XEvent[size];
			}

			// Token: 0x170005F2 RID: 1522
			// (get) Token: 0x06001758 RID: 5976 RVA: 0x00074F30 File Offset: 0x00073130
			public int Count
			{
				get
				{
					return this.size;
				}
			}

			// Token: 0x06001759 RID: 5977 RVA: 0x00074F38 File Offset: 0x00073138
			public void Enqueue(XEvent xevent)
			{
				if (this.size == this.xevents.Length)
				{
					this.Grow();
				}
				this.xevents[this.tail] = xevent;
				this.tail = (this.tail + 1) % this.xevents.Length;
				this.size++;
			}

			// Token: 0x0600175A RID: 5978 RVA: 0x00074F94 File Offset: 0x00073194
			public XEvent Dequeue()
			{
				if (this.size < 1)
				{
					throw new Exception("Attempt to dequeue empty queue.");
				}
				XEvent xevent = this.xevents[this.head];
				this.head = (this.head + 1) % this.xevents.Length;
				this.size--;
				return xevent;
			}

			// Token: 0x0600175B RID: 5979 RVA: 0x00074FEB File Offset: 0x000731EB
			public XEvent Peek()
			{
				if (this.size < 1)
				{
					throw new Exception("Attempt to peek at empty queue");
				}
				return this.xevents[this.head];
			}

			// Token: 0x0600175C RID: 5980 RVA: 0x00075014 File Offset: 0x00073214
			private void Grow()
			{
				XEvent[] array = new XEvent[this.xevents.Length * 2];
				this.xevents.CopyTo(array, 0);
				this.xevents = array;
				this.head = 0;
				this.tail = this.head + this.size;
			}

			// Token: 0x0400121F RID: 4639
			private XEvent[] xevents;

			// Token: 0x04001220 RID: 4640
			private int head;

			// Token: 0x04001221 RID: 4641
			private int tail;

			// Token: 0x04001222 RID: 4642
			private int size;
		}
	}
}
