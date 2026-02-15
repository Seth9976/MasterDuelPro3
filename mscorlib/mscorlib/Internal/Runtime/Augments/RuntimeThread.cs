using System;
using System.Threading;

namespace Internal.Runtime.Augments
{
	// Token: 0x02000096 RID: 150
	internal sealed class RuntimeThread
	{
		// Token: 0x060002C3 RID: 707 RVA: 0x00010922 File Offset: 0x0000EB22
		private RuntimeThread(Thread t)
		{
			this.thread = t;
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00002C89 File Offset: 0x00000E89
		public void ResetThreadPoolThread()
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00010931 File Offset: 0x0000EB31
		public static RuntimeThread InitializeThreadPoolThread()
		{
			return new RuntimeThread(null);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00010939 File Offset: 0x0000EB39
		public static RuntimeThread Create(ParameterizedThreadStart start, int maxStackSize)
		{
			return new RuntimeThread(new Thread(start, maxStackSize));
		}

		// Token: 0x1700004A RID: 74
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00010947 File Offset: 0x0000EB47
		public bool IsBackground
		{
			set
			{
				this.thread.IsBackground = value;
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00010955 File Offset: 0x0000EB55
		public void Start(object state)
		{
			this.thread.Start(state);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00010963 File Offset: 0x0000EB63
		public static void Sleep(int millisecondsTimeout)
		{
			Thread.Sleep(millisecondsTimeout);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001096B File Offset: 0x0000EB6B
		public static bool Yield()
		{
			return Thread.Yield();
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00010972 File Offset: 0x0000EB72
		public static bool SpinWait(int iterations)
		{
			Thread.SpinWait(iterations);
			return true;
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000C091 File Offset: 0x0000A291
		public static int GetCurrentProcessorId()
		{
			return 1;
		}

		// Token: 0x04000278 RID: 632
		internal static readonly int OptimalMaxSpinWaitsPerSpinIteration = 64;

		// Token: 0x04000279 RID: 633
		private readonly Thread thread;
	}
}
