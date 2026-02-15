using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Provides a pool of threads that can be used to execute tasks, post work items, process asynchronous I/O, wait on behalf of other threads, and process timers.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000273 RID: 627
	public static class ThreadPool
	{
		/// <summary>Sets the number of requests to the thread pool that can be active concurrently. All requests above that number remain queued until thread pool threads become available.</summary>
		/// <returns>true if the change is successful; otherwise, false.</returns>
		/// <param name="workerThreads">The maximum number of worker threads in the thread pool. </param>
		/// <param name="completionPortThreads">The maximum number of asynchronous I/O threads in the thread pool. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600171A RID: 5914 RVA: 0x0005A7C0 File Offset: 0x000589C0
		public static bool SetMaxThreads(int workerThreads, int completionPortThreads)
		{
			return ThreadPool.SetMaxThreadsNative(workerThreads, completionPortThreads);
		}

		/// <summary>Retrieves the number of requests to the thread pool that can be active concurrently. All requests above that number remain queued until thread pool threads become available.</summary>
		/// <param name="workerThreads">The maximum number of worker threads in the thread pool. </param>
		/// <param name="completionPortThreads">The maximum number of asynchronous I/O threads in the thread pool. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600171B RID: 5915 RVA: 0x0005A7C9 File Offset: 0x000589C9
		public static void GetMaxThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetMaxThreadsNative(out workerThreads, out completionPortThreads);
		}

		/// <summary>Sets the minimum number of threads the thread pool creates on demand, as new requests are made, before switching to an algorithm for managing thread creation and destruction.</summary>
		/// <returns>true if the change is successful; otherwise, false.</returns>
		/// <param name="workerThreads">The minimum number of worker threads that the thread pool creates on demand. </param>
		/// <param name="completionPortThreads">The minimum number of asynchronous I/O threads that the thread pool creates on demand. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlThread" />
		/// </PermissionSet>
		// Token: 0x0600171C RID: 5916 RVA: 0x0005A7D2 File Offset: 0x000589D2
		public static bool SetMinThreads(int workerThreads, int completionPortThreads)
		{
			return ThreadPool.SetMinThreadsNative(workerThreads, completionPortThreads);
		}

		/// <summary>Retrieves the minimum number of threads the thread pool creates on demand, as new requests are made, before switching to an algorithm for managing thread creation and destruction.</summary>
		/// <param name="workerThreads">When this method returns, contains the minimum number of worker threads that the thread pool creates on demand. </param>
		/// <param name="completionPortThreads">When this method returns, contains the minimum number of asynchronous I/O threads that the thread pool creates on demand. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600171D RID: 5917 RVA: 0x0005A7DB File Offset: 0x000589DB
		public static void GetMinThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetMinThreadsNative(out workerThreads, out completionPortThreads);
		}

		/// <summary>Retrieves the difference between the maximum number of thread pool threads returned by the <see cref="M:System.Threading.ThreadPool.GetMaxThreads(System.Int32@,System.Int32@)" /> method, and the number currently active.</summary>
		/// <param name="workerThreads">The number of available worker threads. </param>
		/// <param name="completionPortThreads">The number of available asynchronous I/O threads. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600171E RID: 5918 RVA: 0x0005A7E4 File Offset: 0x000589E4
		public static void GetAvailableThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetAvailableThreadsNative(out workerThreads, out completionPortThreads);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 32-bit unsigned integer for the time-out in milliseconds.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that can be used to cancel the registered wait operation.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600171F RID: 5919 RVA: 0x0005A7F0 File Offset: 0x000589F0
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, true);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 32-bit unsigned integer for the time-out in milliseconds. This method does not propagate the calling stack to the worker thread.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object that is passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001720 RID: 5920 RVA: 0x0005A810 File Offset: 0x00058A10
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, ref stackCrawlMark, false);
		}

		// Token: 0x06001721 RID: 5921 RVA: 0x0005A830 File Offset: 0x00058A30
		private static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce, ref StackCrawlMark stackMark, bool compressStack)
		{
			if (waitObject == null)
			{
				throw new ArgumentNullException("waitObject");
			}
			if (callBack == null)
			{
				throw new ArgumentNullException("callBack");
			}
			if (millisecondsTimeOutInterval != 4294967295U && millisecondsTimeOutInterval > 2147483647U)
			{
				throw new NotSupportedException("Timeout is too big. Maximum is Int32.MaxValue");
			}
			RegisteredWaitHandle registeredWaitHandle = new RegisteredWaitHandle(waitObject, callBack, state, new TimeSpan(0, 0, 0, 0, (int)millisecondsTimeOutInterval), executeOnlyOnce);
			if (compressStack)
			{
				ThreadPool.QueueUserWorkItem(new WaitCallback(registeredWaitHandle.Wait), null);
			}
			else
			{
				ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(registeredWaitHandle.Wait), null);
			}
			return registeredWaitHandle;
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 32-bit signed integer for the time-out in milliseconds.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that encapsulates the native handle.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object that is passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001722 RID: 5922 RVA: 0x0005A8B4 File Offset: 0x00058AB4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)((millisecondsTimeOutInterval == -1) ? (-1) : millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, true);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, using a 32-bit signed integer for the time-out in milliseconds. This method does not propagate the calling stack to the worker thread.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object that is passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001723 RID: 5923 RVA: 0x0005A8F4 File Offset: 0x00058AF4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)((millisecondsTimeOutInterval == -1) ? (-1) : millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, false);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 64-bit signed integer for the time-out in milliseconds.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that encapsulates the native handle.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001724 RID: 5924 RVA: 0x0005A934 File Offset: 0x00058B34
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (millisecondsTimeOutInterval == -1L) ? uint.MaxValue : ((uint)millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, true);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a 64-bit signed integer for the time-out in milliseconds. This method does not propagate the calling stack to the worker thread.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object that is passed to the delegate. </param>
		/// <param name="millisecondsTimeOutInterval">The time-out in milliseconds. If the <paramref name="millisecondsTimeOutInterval" /> parameter is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="millisecondsTimeOutInterval" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="millisecondsTimeOutInterval" /> parameter is less than -1. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001725 RID: 5925 RVA: 0x0005A974 File Offset: 0x00058B74
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (millisecondsTimeOutInterval == -1L) ? uint.MaxValue : ((uint)millisecondsTimeOutInterval), executeOnlyOnce, ref stackCrawlMark, false);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> that encapsulates the native handle.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The <see cref="T:System.Threading.WaitOrTimerCallback" /> delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object passed to the delegate. </param>
		/// <param name="timeout">The time-out represented by a <see cref="T:System.TimeSpan" />. If <paramref name="timeout" /> is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="timeout" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="timeout" /> parameter is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001726 RID: 5926 RVA: 0x0005A9B4 File Offset: 0x00058BB4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Argument must be less than or equal to 2^31 - 1 milliseconds."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)num, executeOnlyOnce, ref stackCrawlMark, true);
		}

		/// <summary>Registers a delegate to wait for a <see cref="T:System.Threading.WaitHandle" />, specifying a <see cref="T:System.TimeSpan" /> value for the time-out. This method does not propagate the calling stack to the worker thread.</summary>
		/// <returns>The <see cref="T:System.Threading.RegisteredWaitHandle" /> object that can be used to cancel the registered wait operation.</returns>
		/// <param name="waitObject">The <see cref="T:System.Threading.WaitHandle" /> to register. Use a <see cref="T:System.Threading.WaitHandle" /> other than <see cref="T:System.Threading.Mutex" />.</param>
		/// <param name="callBack">The delegate to call when the <paramref name="waitObject" /> parameter is signaled. </param>
		/// <param name="state">The object that is passed to the delegate. </param>
		/// <param name="timeout">The time-out represented by a <see cref="T:System.TimeSpan" />. If <paramref name="timeout" /> is 0 (zero), the function tests the object's state and returns immediately. If <paramref name="timeout" /> is -1, the function's time-out interval never elapses. </param>
		/// <param name="executeOnlyOnce">true to indicate that the thread will no longer wait on the <paramref name="waitObject" /> parameter after the delegate has been called; false to indicate that the timer is reset every time the wait operation completes until the wait is unregistered. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="timeout" /> parameter is less than -1. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="timeout" /> parameter is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06001727 RID: 5927 RVA: 0x0005AA14 File Offset: 0x00058C14
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Argument must be less than or equal to 2^31 - 1 milliseconds."));
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)num, executeOnlyOnce, ref stackCrawlMark, false);
		}

		/// <summary>Queues a method for execution, and specifies an object containing data to be used by the method. The method executes when a thread pool thread becomes available.</summary>
		/// <returns>true if the method is successfully queued; <see cref="T:System.NotSupportedException" /> is thrown if the work item could not be queued.</returns>
		/// <param name="callBack">A <see cref="T:System.Threading.WaitCallback" /> representing the method to execute. </param>
		/// <param name="state">An object containing data to be used by the method. </param>
		/// <exception cref="T:System.NotSupportedException">The common language runtime (CLR) is hosted, and the host does not support this action.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callBack" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001728 RID: 5928 RVA: 0x0005AA74 File Offset: 0x00058C74
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool QueueUserWorkItem(WaitCallback callBack, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackCrawlMark, true, true);
		}

		/// <summary>Queues a method for execution. The method executes when a thread pool thread becomes available.</summary>
		/// <returns>true if the method is successfully queued; <see cref="T:System.NotSupportedException" /> is thrown if the work item could not be queued.</returns>
		/// <param name="callBack">A <see cref="T:System.Threading.WaitCallback" /> that represents the method to be executed. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callBack" /> is null.</exception>
		/// <exception cref="T:System.NotSupportedException">The common language runtime (CLR) is hosted, and the host does not support this action.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001729 RID: 5929 RVA: 0x0005AA90 File Offset: 0x00058C90
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool QueueUserWorkItem(WaitCallback callBack)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, null, ref stackCrawlMark, true, true);
		}

		/// <summary>Queues the specified delegate to the thread pool, but does not propagate the calling stack to the worker thread.</summary>
		/// <returns>true if the method succeeds; <see cref="T:System.OutOfMemoryException" /> is thrown if the work item could not be queued.</returns>
		/// <param name="callBack">A <see cref="T:System.Threading.WaitCallback" /> that represents the delegate to invoke when a thread in the thread pool picks up the work item. </param>
		/// <param name="state">The object that is passed to the delegate when serviced from the thread pool. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.ApplicationException">An out-of-memory condition was encountered.</exception>
		/// <exception cref="T:System.OutOfMemoryException">The work item could not be queued.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="callBack" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x0600172A RID: 5930 RVA: 0x0005AAAC File Offset: 0x00058CAC
		[MethodImpl(MethodImplOptions.NoInlining)]
		public static bool UnsafeQueueUserWorkItem(WaitCallback callBack, object state)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(callBack, state, ref stackCrawlMark, false, true);
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0005AAC8 File Offset: 0x00058CC8
		public static bool QueueUserWorkItem<TState>(Action<TState> callBack, TState state, bool preferLocal)
		{
			if (callBack == null)
			{
				throw new ArgumentNullException("callBack");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(delegate(object x)
			{
				callBack((TState)((object)x));
			}, state, ref stackCrawlMark, true, !preferLocal);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0005AB14 File Offset: 0x00058D14
		public static bool UnsafeQueueUserWorkItem<TState>(Action<TState> callBack, TState state, bool preferLocal)
		{
			if (callBack == null)
			{
				throw new ArgumentNullException("callBack");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return ThreadPool.QueueUserWorkItemHelper(delegate(object x)
			{
				callBack((TState)((object)x));
			}, state, ref stackCrawlMark, false, !preferLocal);
		}

		// Token: 0x0600172D RID: 5933 RVA: 0x0005AB60 File Offset: 0x00058D60
		private static bool QueueUserWorkItemHelper(WaitCallback callBack, object state, ref StackCrawlMark stackMark, bool compressStack, bool forceGlobal = true)
		{
			bool flag = true;
			if (callBack != null)
			{
				ThreadPool.EnsureVMInitialized();
				try
				{
					return flag;
				}
				finally
				{
					QueueUserWorkItemCallback queueUserWorkItemCallback = new QueueUserWorkItemCallback(callBack, state, compressStack, ref stackMark);
					ThreadPoolGlobals.workQueue.Enqueue(queueUserWorkItemCallback, forceGlobal);
					flag = true;
				}
			}
			throw new ArgumentNullException("WaitCallback");
		}

		// Token: 0x0600172E RID: 5934 RVA: 0x0005ABB0 File Offset: 0x00058DB0
		internal static void UnsafeQueueCustomWorkItem(IThreadPoolWorkItem workItem, bool forceGlobal)
		{
			ThreadPool.EnsureVMInitialized();
			try
			{
			}
			finally
			{
				ThreadPoolGlobals.workQueue.Enqueue(workItem, forceGlobal);
			}
		}

		// Token: 0x0600172F RID: 5935 RVA: 0x0005ABE4 File Offset: 0x00058DE4
		internal static bool TryPopCustomWorkItem(IThreadPoolWorkItem workItem)
		{
			return ThreadPoolGlobals.vmTpInitialized && ThreadPoolGlobals.workQueue.LocalFindAndPop(workItem);
		}

		// Token: 0x06001730 RID: 5936 RVA: 0x0005ABFC File Offset: 0x00058DFC
		internal static IEnumerable<IThreadPoolWorkItem> GetQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(ThreadPoolWorkQueue.allThreadQueues.Current, ThreadPoolGlobals.workQueue.queueTail);
		}

		// Token: 0x06001731 RID: 5937 RVA: 0x0005AC19 File Offset: 0x00058E19
		internal static IEnumerable<IThreadPoolWorkItem> EnumerateQueuedWorkItems(ThreadPoolWorkQueue.WorkStealingQueue[] wsQueues, ThreadPoolWorkQueue.QueueSegment globalQueueTail)
		{
			if (wsQueues != null)
			{
				foreach (ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue in wsQueues)
				{
					if (workStealingQueue != null && workStealingQueue.m_array != null)
					{
						IThreadPoolWorkItem[] items = workStealingQueue.m_array;
						int num;
						for (int i = 0; i < items.Length; i = num + 1)
						{
							IThreadPoolWorkItem threadPoolWorkItem = items[i];
							if (threadPoolWorkItem != null)
							{
								yield return threadPoolWorkItem;
							}
							num = i;
						}
						items = null;
					}
				}
				ThreadPoolWorkQueue.WorkStealingQueue[] array = null;
			}
			if (globalQueueTail != null)
			{
				ThreadPoolWorkQueue.QueueSegment segment;
				for (segment = globalQueueTail; segment != null; segment = segment.Next)
				{
					IThreadPoolWorkItem[] items = segment.nodes;
					int num;
					for (int j = 0; j < items.Length; j = num + 1)
					{
						IThreadPoolWorkItem threadPoolWorkItem2 = items[j];
						if (threadPoolWorkItem2 != null)
						{
							yield return threadPoolWorkItem2;
						}
						num = j;
					}
					items = null;
				}
				segment = null;
			}
			yield break;
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x0005AC30 File Offset: 0x00058E30
		internal static IEnumerable<IThreadPoolWorkItem> GetLocallyQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(new ThreadPoolWorkQueue.WorkStealingQueue[] { ThreadPoolWorkQueueThreadLocals.threadLocals.workStealingQueue }, null);
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0005AC4B File Offset: 0x00058E4B
		internal static IEnumerable<IThreadPoolWorkItem> GetGloballyQueuedWorkItems()
		{
			return ThreadPool.EnumerateQueuedWorkItems(null, ThreadPoolGlobals.workQueue.queueTail);
		}

		// Token: 0x06001734 RID: 5940 RVA: 0x0005AC60 File Offset: 0x00058E60
		private static object[] ToObjectArray(IEnumerable<IThreadPoolWorkItem> workitems)
		{
			int num = 0;
			foreach (IThreadPoolWorkItem threadPoolWorkItem in workitems)
			{
				num++;
			}
			object[] array = new object[num];
			num = 0;
			foreach (IThreadPoolWorkItem threadPoolWorkItem2 in workitems)
			{
				if (num < array.Length)
				{
					array[num] = threadPoolWorkItem2;
				}
				num++;
			}
			return array;
		}

		// Token: 0x06001735 RID: 5941 RVA: 0x0005ACF0 File Offset: 0x00058EF0
		internal static object[] GetQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x06001736 RID: 5942 RVA: 0x0005ACFC File Offset: 0x00058EFC
		internal static object[] GetGloballyQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetGloballyQueuedWorkItems());
		}

		// Token: 0x06001737 RID: 5943 RVA: 0x0005AD08 File Offset: 0x00058F08
		internal static object[] GetLocallyQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetLocallyQueuedWorkItems());
		}

		// Token: 0x06001738 RID: 5944
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool RequestWorkerThread();

		// Token: 0x06001739 RID: 5945
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool PostQueuedCompletionStatus(NativeOverlapped* overlapped);

		/// <summary>Queues an overlapped I/O operation for execution.</summary>
		/// <returns>true if the operation was successfully queued to an I/O completion port; otherwise, false.</returns>
		/// <param name="overlapped">The <see cref="T:System.Threading.NativeOverlapped" /> structure to queue.</param>
		// Token: 0x0600173A RID: 5946 RVA: 0x0005AD14 File Offset: 0x00058F14
		[CLSCompliant(false)]
		public unsafe static bool UnsafeQueueNativeOverlapped(NativeOverlapped* overlapped)
		{
			throw new NotImplementedException("");
		}

		// Token: 0x0600173B RID: 5947 RVA: 0x0005AD20 File Offset: 0x00058F20
		private static void EnsureVMInitialized()
		{
			if (!ThreadPoolGlobals.vmTpInitialized)
			{
				ThreadPool.InitializeVMTp(ref ThreadPoolGlobals.enableWorkerTracking);
				ThreadPoolGlobals.vmTpInitialized = true;
			}
		}

		// Token: 0x0600173C RID: 5948
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetMinThreadsNative(int workerThreads, int completionPortThreads);

		// Token: 0x0600173D RID: 5949
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetMaxThreadsNative(int workerThreads, int completionPortThreads);

		// Token: 0x0600173E RID: 5950
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMinThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x0600173F RID: 5951
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMaxThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x06001740 RID: 5952
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAvailableThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x06001741 RID: 5953
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool NotifyWorkItemComplete();

		// Token: 0x06001742 RID: 5954
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportThreadStatus(bool isWorking);

		// Token: 0x06001743 RID: 5955 RVA: 0x0005AD3D File Offset: 0x00058F3D
		internal static void NotifyWorkItemProgress()
		{
			ThreadPool.EnsureVMInitialized();
			ThreadPool.NotifyWorkItemProgressNative();
		}

		// Token: 0x06001744 RID: 5956
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NotifyWorkItemProgressNative();

		// Token: 0x06001745 RID: 5957
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NotifyWorkItemQueued();

		// Token: 0x06001746 RID: 5958 RVA: 0x00033991 File Offset: 0x00031B91
		internal static bool IsThreadPoolHosted()
		{
			return false;
		}

		// Token: 0x06001747 RID: 5959
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeVMTp(ref bool enableWorkerTracking);

		/// <summary>Binds an operating system handle to the <see cref="T:System.Threading.ThreadPool" />.</summary>
		/// <returns>true if the handle is bound; otherwise, false.</returns>
		/// <param name="osHandle">An <see cref="T:System.IntPtr" /> that holds the handle. The handle must have been opened for overlapped I/O on the unmanaged side. </param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001748 RID: 5960 RVA: 0x0005AD49 File Offset: 0x00058F49
		[Obsolete("ThreadPool.BindHandle(IntPtr) has been deprecated.  Please use ThreadPool.BindHandle(SafeHandle) instead.", false)]
		public static bool BindHandle(IntPtr osHandle)
		{
			return ThreadPool.BindIOCompletionCallbackNative(osHandle);
		}

		/// <summary>Binds an operating system handle to the <see cref="T:System.Threading.ThreadPool" />.</summary>
		/// <returns>true if the handle is bound; otherwise, false.</returns>
		/// <param name="osHandle">A <see cref="T:System.Runtime.InteropServices.SafeHandle" />  that holds the operating system handle. The handle must have been opened for overlapped I/O on the unmanaged side.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="osHandle" /> is null. </exception>
		// Token: 0x06001749 RID: 5961 RVA: 0x0005AD54 File Offset: 0x00058F54
		public static bool BindHandle(SafeHandle osHandle)
		{
			if (osHandle == null)
			{
				throw new ArgumentNullException("osHandle");
			}
			bool flag = false;
			bool flag2 = false;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				osHandle.DangerousAddRef(ref flag2);
				flag = ThreadPool.BindIOCompletionCallbackNative(osHandle.DangerousGetHandle());
			}
			finally
			{
				if (flag2)
				{
					osHandle.DangerousRelease();
				}
			}
			return flag;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0000C091 File Offset: 0x0000A291
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		private static bool BindIOCompletionCallbackNative(IntPtr fileHandle)
		{
			return true;
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x0600174B RID: 5963 RVA: 0x0005ADAC File Offset: 0x00058FAC
		internal static bool IsThreadPoolThread
		{
			get
			{
				return Thread.CurrentThread.IsThreadPoolThread;
			}
		}
	}
}
