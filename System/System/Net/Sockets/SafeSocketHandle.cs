using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Net.Sockets
{
	// Token: 0x020004C1 RID: 1217
	internal sealed class SafeSocketHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06001DEE RID: 7662 RVA: 0x000823B8 File Offset: 0x000805B8
		public SafeSocketHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
			if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
			{
				this.threads_stacktraces = new Dictionary<Thread, StackTrace>();
			}
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x000823DC File Offset: 0x000805DC
		protected override bool ReleaseHandle()
		{
			int num = 0;
			Socket.Blocking_icall(this.handle, false, out num);
			if (this.blocking_threads != null)
			{
				List<Thread> list = this.blocking_threads;
				lock (list)
				{
					int num2 = 0;
					while (this.blocking_threads.Count > 0)
					{
						if (num2++ >= 10)
						{
							if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
							{
								StringBuilder stringBuilder = new StringBuilder();
								stringBuilder.AppendLine("Could not abort registered blocking threads before closing socket.");
								foreach (Thread thread in this.blocking_threads)
								{
									stringBuilder.AppendLine("Thread StackTrace:");
									stringBuilder.AppendLine(this.threads_stacktraces[thread].ToString());
								}
								stringBuilder.AppendLine();
								throw new Exception(stringBuilder.ToString());
							}
							break;
						}
						else
						{
							if (this.blocking_threads.Count == 1 && this.blocking_threads[0] == Thread.CurrentThread)
							{
								break;
							}
							foreach (Thread thread2 in this.blocking_threads)
							{
								Socket.cancel_blocking_socket_operation(thread2);
							}
							this.in_cleanup = true;
							Monitor.Wait(this.blocking_threads, 100);
						}
					}
				}
			}
			Socket.Close_icall(this.handle, out num);
			return num == 0;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0008259C File Offset: 0x0008079C
		public void RegisterForBlockingSyscall()
		{
			if (this.blocking_threads == null)
			{
				Interlocked.CompareExchange<List<Thread>>(ref this.blocking_threads, new List<Thread>(), null);
			}
			bool flag = false;
			try
			{
				base.DangerousAddRef(ref flag);
			}
			finally
			{
				List<Thread> list = this.blocking_threads;
				lock (list)
				{
					this.blocking_threads.Add(Thread.CurrentThread);
					if (SafeSocketHandle.THROW_ON_ABORT_RETRIES)
					{
						this.threads_stacktraces.Add(Thread.CurrentThread, new StackTrace(true));
					}
				}
				if (flag)
				{
					base.DangerousRelease();
				}
				if (base.IsClosed)
				{
					throw new SocketException(10004);
				}
			}
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00082654 File Offset: 0x00080854
		public void UnRegisterForBlockingSyscall()
		{
			List<Thread> list = this.blocking_threads;
			lock (list)
			{
				Thread currentThread = Thread.CurrentThread;
				this.blocking_threads.Remove(currentThread);
				if (SafeSocketHandle.THROW_ON_ABORT_RETRIES && this.blocking_threads.IndexOf(currentThread) == -1)
				{
					this.threads_stacktraces.Remove(currentThread);
				}
				if (this.in_cleanup && this.blocking_threads.Count == 0)
				{
					Monitor.Pulse(this.blocking_threads);
				}
			}
		}

		// Token: 0x04001534 RID: 5428
		private List<Thread> blocking_threads;

		// Token: 0x04001535 RID: 5429
		private Dictionary<Thread, StackTrace> threads_stacktraces;

		// Token: 0x04001536 RID: 5430
		private bool in_cleanup;

		// Token: 0x04001537 RID: 5431
		private static bool THROW_ON_ABORT_RETRIES = Environment.GetEnvironmentVariable("MONO_TESTS_IN_PROGRESS") == "yes";
	}
}
