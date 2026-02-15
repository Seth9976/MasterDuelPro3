using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	/// <summary>Encapsulates operating system–specific objects that wait for exclusive access to shared resources.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000277 RID: 631
	[ComVisible(true)]
	[StructLayout(LayoutKind.Sequential)]
	public abstract class WaitHandle : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.WaitHandle" /> class.</summary>
		// Token: 0x06001758 RID: 5976 RVA: 0x0005AFFF File Offset: 0x000591FF
		protected WaitHandle()
		{
			this.Init();
		}

		// Token: 0x06001759 RID: 5977 RVA: 0x0005B00D File Offset: 0x0005920D
		private void Init()
		{
			this.safeWaitHandle = null;
			this.waitHandle = WaitHandle.InvalidHandle;
			this.hasThreadAffinity = false;
		}

		/// <summary>Gets or sets the native operating system handle.</summary>
		/// <returns>An IntPtr representing the native operating system handle. The default is the value of the <see cref="F:System.Threading.WaitHandle.InvalidHandle" /> field.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600175A RID: 5978 RVA: 0x0005B02A File Offset: 0x0005922A
		// (set) Token: 0x0600175B RID: 5979 RVA: 0x0005B04C File Offset: 0x0005924C
		[Obsolete("Use the SafeWaitHandle property instead.")]
		public virtual IntPtr Handle
		{
			get
			{
				if (this.safeWaitHandle != null)
				{
					return this.safeWaitHandle.DangerousGetHandle();
				}
				return WaitHandle.InvalidHandle;
			}
			set
			{
				if (value == WaitHandle.InvalidHandle)
				{
					if (this.safeWaitHandle != null)
					{
						this.safeWaitHandle.SetHandleAsInvalid();
						this.safeWaitHandle = null;
					}
				}
				else
				{
					this.safeWaitHandle = new SafeWaitHandle(value, true);
				}
				this.waitHandle = value;
			}
		}

		/// <summary>Gets or sets the native operating system handle.</summary>
		/// <returns>A <see cref="T:Microsoft.Win32.SafeHandles.SafeWaitHandle" /> representing the native operating system handle.</returns>
		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600175C RID: 5980 RVA: 0x0005B09E File Offset: 0x0005929E
		// (set) Token: 0x0600175D RID: 5981 RVA: 0x0005B0C8 File Offset: 0x000592C8
		public SafeWaitHandle SafeWaitHandle
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
			get
			{
				if (this.safeWaitHandle == null)
				{
					this.safeWaitHandle = new SafeWaitHandle(WaitHandle.InvalidHandle, false);
				}
				return this.safeWaitHandle;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
				}
				finally
				{
					if (value == null)
					{
						this.safeWaitHandle = null;
						this.waitHandle = WaitHandle.InvalidHandle;
					}
					else
					{
						this.safeWaitHandle = value;
						this.waitHandle = this.safeWaitHandle.DangerousGetHandle();
					}
				}
			}
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x0005B124 File Offset: 0x00059324
		internal void SetHandleInternal(SafeWaitHandle handle)
		{
			this.safeWaitHandle = handle;
			this.waitHandle = handle.DangerousGetHandle();
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.WaitHandle" /> receives a signal, using a 32-bit signed integer to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>true if the current instance receives a signal; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600175F RID: 5983 RVA: 0x0005B13B File Offset: 0x0005933B
		public virtual bool WaitOne(int millisecondsTimeout, bool exitContext)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.WaitOne((long)millisecondsTimeout, exitContext);
		}

		/// <summary>Blocks the current thread until the current instance receives a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>true if the current instance receives a signal; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.-or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001760 RID: 5984 RVA: 0x0005B160 File Offset: 0x00059360
		public virtual bool WaitOne(TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return this.WaitOne(num, exitContext);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.WaitHandle" /> receives a signal.</summary>
		/// <returns>true if the current instance receives a signal. If the current instance is never signaled, <see cref="M:System.Threading.WaitHandle.WaitOne(System.Int32,System.Boolean)" /> never returns.</returns>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001761 RID: 5985 RVA: 0x0005B1A1 File Offset: 0x000593A1
		public virtual bool WaitOne()
		{
			return this.WaitOne(-1, false);
		}

		/// <summary>Blocks the current thread until the current <see cref="T:System.Threading.WaitHandle" /> receives a signal, using a 32-bit signed integer to specify the time interval.</summary>
		/// <returns>true if the current instance receives a signal; otherwise, false.</returns>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06001762 RID: 5986 RVA: 0x0005B1AB File Offset: 0x000593AB
		public virtual bool WaitOne(int millisecondsTimeout)
		{
			return this.WaitOne(millisecondsTimeout, false);
		}

		/// <summary>Blocks the current thread until the current instance receives a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval.</summary>
		/// <returns>true if the current instance receives a signal; otherwise, false.</returns>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely. </param>
		/// <exception cref="T:System.ObjectDisposedException">The current instance has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out.-or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The current instance is a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06001763 RID: 5987 RVA: 0x0005B1B5 File Offset: 0x000593B5
		public virtual bool WaitOne(TimeSpan timeout)
		{
			return this.WaitOne(timeout, false);
		}

		// Token: 0x06001764 RID: 5988 RVA: 0x0005B1BF File Offset: 0x000593BF
		private bool WaitOne(long timeout, bool exitContext)
		{
			return WaitHandle.InternalWaitOne(this.safeWaitHandle, timeout, this.hasThreadAffinity, exitContext);
		}

		// Token: 0x06001765 RID: 5989 RVA: 0x0005B1D8 File Offset: 0x000593D8
		internal static bool InternalWaitOne(SafeHandle waitableSafeHandle, long millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
		{
			if (waitableSafeHandle == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			int num = WaitHandle.WaitOneNative(waitableSafeHandle, (uint)millisecondsTimeout, hasThreadAffinity, exitContext);
			if (num == 128)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			return num != 258 && num != int.MaxValue;
		}

		// Token: 0x06001766 RID: 5990 RVA: 0x0005B228 File Offset: 0x00059428
		internal bool WaitOneWithoutFAS()
		{
			if (this.safeWaitHandle == null)
			{
				throw new ObjectDisposedException(null, Environment.GetResourceString("Cannot access a disposed object."));
			}
			long num = -1L;
			int num2 = WaitHandle.WaitOneNative(this.safeWaitHandle, (uint)num, this.hasThreadAffinity, false);
			if (num2 == 128)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			return num2 != 258 && num2 != int.MaxValue;
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using an <see cref="T:System.Int32" /> value to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>true when every element in <paramref name="waitHandles" /> has received a signal; otherwise, false.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object (duplicates). </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later. </exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001767 RID: 5991 RVA: 0x0005B28C File Offset: 0x0005948C
		public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("The waitHandles parameter cannot be null."));
			}
			if (waitHandles.Length == 0)
			{
				throw new ArgumentNullException(Environment.GetResourceString("Waithandle array may not be empty."));
			}
			if (waitHandles.Length > 64)
			{
				throw new NotSupportedException(Environment.GetResourceString("The number of WaitHandles must be less than or equal to 64."));
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			WaitHandle[] array = new WaitHandle[waitHandles.Length];
			for (int i = 0; i < waitHandles.Length; i++)
			{
				WaitHandle waitHandle = waitHandles[i];
				if (waitHandle == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("At least one element in the specified array was null."));
				}
				if (RemotingServices.IsTransparentProxy(waitHandle))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Cannot wait on a transparent proxy."));
				}
				array[i] = waitHandle;
			}
			int num = WaitHandle.WaitMultiple(array, millisecondsTimeout, exitContext, true);
			if (128 <= num && 128 + array.Length > num)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			GC.KeepAlive(array);
			return num != 258 && num != int.MaxValue;
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> value to specify the time interval, and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>true when every element in <paramref name="waitHandles" /> has received a signal; otherwise false.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object. </param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds, to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null. -or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later. </exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001768 RID: 5992 RVA: 0x0005B378 File Offset: 0x00059578
		public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return WaitHandle.WaitAll(waitHandles, (int)num, exitContext);
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal.</summary>
		/// <returns>true when every element in <paramref name="waitHandles" /> has received a signal; otherwise the method never returns.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null. -or- One or more of the objects in the <paramref name="waitHandles" /> array are null. -or-<paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 2.0 or later.</exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001769 RID: 5993 RVA: 0x0005B3BA File Offset: 0x000595BA
		public static bool WaitAll(WaitHandle[] waitHandles)
		{
			return WaitHandle.WaitAll(waitHandles, -1, true);
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using an <see cref="T:System.Int32" /> value to specify the time interval.</summary>
		/// <returns>true when every element in <paramref name="waitHandles" /> has received a signal; otherwise, false.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object (duplicates). </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements. </exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600176A RID: 5994 RVA: 0x0005B3C4 File Offset: 0x000595C4
		public static bool WaitAll(WaitHandle[] waitHandles, int millisecondsTimeout)
		{
			return WaitHandle.WaitAll(waitHandles, millisecondsTimeout, true);
		}

		/// <summary>Waits for all the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> value to specify the time interval.</summary>
		/// <returns>true when every element in <paramref name="waitHandles" /> has received a signal; otherwise, false.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. This array cannot contain multiple references to the same object. </param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds, to wait indefinitely. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null. -or- One or more of the objects in the <paramref name="waitHandles" /> array is null. -or-<paramref name="waitHandles" /> is an array with no elements. </exception>
		/// <exception cref="T:System.DuplicateWaitObjectException">NoteIn the .NET for Windows Store apps or the Portable Class Library, catch the base class exception, <see cref="T:System.ArgumentException" />, instead.The <paramref name="waitHandles" /> array contains elements that are duplicates. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits.-or- The <see cref="T:System.STAThreadAttribute" /> attribute is applied to the thread procedure for the current thread, and <paramref name="waitHandles" /> contains more than one element. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait terminated because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600176B RID: 5995 RVA: 0x0005B3CE File Offset: 0x000595CE
		public static bool WaitAll(WaitHandle[] waitHandles, TimeSpan timeout)
		{
			return WaitHandle.WaitAll(waitHandles, timeout, true);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a 32-bit signed integer to specify the time interval, and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="millisecondsTimeout" /> has passed.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600176C RID: 5996 RVA: 0x0005B3D8 File Offset: 0x000595D8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException(Environment.GetResourceString("The waitHandles parameter cannot be null."));
			}
			if (waitHandles.Length == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Waithandle array may not be empty."));
			}
			if (64 < waitHandles.Length)
			{
				throw new NotSupportedException(Environment.GetResourceString("The number of WaitHandles must be less than or equal to 64."));
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			WaitHandle[] array = new WaitHandle[waitHandles.Length];
			for (int i = 0; i < waitHandles.Length; i++)
			{
				WaitHandle waitHandle = waitHandles[i];
				if (waitHandle == null)
				{
					throw new ArgumentNullException(Environment.GetResourceString("At least one element in the specified array was null."));
				}
				if (RemotingServices.IsTransparentProxy(waitHandle))
				{
					throw new InvalidOperationException(Environment.GetResourceString("Cannot wait on a transparent proxy."));
				}
				array[i] = waitHandle;
			}
			int num = WaitHandle.WaitMultiple(array, millisecondsTimeout, exitContext, false);
			if (128 <= num && 128 + array.Length > num)
			{
				int num2 = num - 128;
				if (0 <= num2 && num2 < array.Length)
				{
					WaitHandle.ThrowAbandonedMutexException(num2, array[num2]);
				}
				else
				{
					WaitHandle.ThrowAbandonedMutexException();
				}
			}
			GC.KeepAlive(array);
			return num;
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval and specifying whether to exit the synchronization domain before the wait.</summary>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="timeout" /> has passed.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. </param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely. </param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600176D RID: 5997 RVA: 0x0005B4D4 File Offset: 0x000596D4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return WaitHandle.WaitAny(waitHandles, (int)num, exitContext);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a <see cref="T:System.TimeSpan" /> to specify the time interval.</summary>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="timeout" /> has passed.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. </param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the number of milliseconds to wait, or a <see cref="T:System.TimeSpan" /> that represents -1 milliseconds to wait indefinitely. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> is a negative number other than -1 milliseconds, which represents an infinite time-out. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x0600176E RID: 5998 RVA: 0x0005B516 File Offset: 0x00059716
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, TimeSpan timeout)
		{
			return WaitHandle.WaitAny(waitHandles, timeout, true);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal.</summary>
		/// <returns>The array index of the object that satisfied the wait.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 1.0 or 1.1. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements, and the .NET Framework version is 2.0 or later. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600176F RID: 5999 RVA: 0x0005B520 File Offset: 0x00059720
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles)
		{
			return WaitHandle.WaitAny(waitHandles, -1, true);
		}

		/// <summary>Waits for any of the elements in the specified array to receive a signal, using a 32-bit signed integer to specify the time interval.</summary>
		/// <returns>The array index of the object that satisfied the wait, or <see cref="F:System.Threading.WaitHandle.WaitTimeout" /> if no object satisfied the wait and a time interval equivalent to <paramref name="millisecondsTimeout" /> has passed.</returns>
		/// <param name="waitHandles">A WaitHandle array containing the objects for which the current instance will wait. </param>
		/// <param name="millisecondsTimeout">The number of milliseconds to wait, or <see cref="F:System.Threading.Timeout.Infinite" /> (-1) to wait indefinitely. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="waitHandles" /> parameter is null.-or-One or more of the objects in the <paramref name="waitHandles" /> array is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The number of objects in <paramref name="waitHandles" /> is greater than the system permits. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="waitHandles" /> is an array with no elements. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="waitHandles" /> array contains a transparent proxy for a <see cref="T:System.Threading.WaitHandle" /> in another application domain.</exception>
		// Token: 0x06001770 RID: 6000 RVA: 0x0005B52A File Offset: 0x0005972A
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static int WaitAny(WaitHandle[] waitHandles, int millisecondsTimeout)
		{
			return WaitHandle.WaitAny(waitHandles, millisecondsTimeout, true);
		}

		/// <summary>Signals one <see cref="T:System.Threading.WaitHandle" /> and waits on another.</summary>
		/// <returns>true if both the signal and the wait complete successfully; if the wait does not complete, the method does not return.</returns>
		/// <param name="toSignal">The <see cref="T:System.Threading.WaitHandle" /> to signal.</param>
		/// <param name="toWaitOn">The <see cref="T:System.Threading.WaitHandle" /> to wait on.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toSignal" /> is null.-or-<paramref name="toWaitOn" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The method was called on a thread that has <see cref="T:System.STAThreadAttribute" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="toSignal" /> is a semaphore, and it already has a full count. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001771 RID: 6001 RVA: 0x0005B534 File Offset: 0x00059734
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn)
		{
			return WaitHandle.SignalAndWait(toSignal, toWaitOn, -1, false);
		}

		/// <summary>Signals one <see cref="T:System.Threading.WaitHandle" /> and waits on another, specifying the time-out interval as a <see cref="T:System.TimeSpan" /> and specifying whether to exit the synchronization domain for the context before entering the wait.</summary>
		/// <returns>true if both the signal and the wait completed successfully, or false if the signal completed but the wait timed out.</returns>
		/// <param name="toSignal">The <see cref="T:System.Threading.WaitHandle" /> to signal.</param>
		/// <param name="toWaitOn">The <see cref="T:System.Threading.WaitHandle" /> to wait on.</param>
		/// <param name="timeout">A <see cref="T:System.TimeSpan" /> that represents the interval to wait. If the value is -1, the wait is infinite.</param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toSignal" /> is null.-or-<paramref name="toWaitOn" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The method was called on a thread that has <see cref="T:System.STAThreadAttribute" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="toSignal" /> is a semaphore, and it already has a full count. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> evaluates to a negative number of milliseconds other than -1. -or-<paramref name="timeout" /> is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001772 RID: 6002 RVA: 0x0005B540 File Offset: 0x00059740
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, TimeSpan timeout, bool exitContext)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (-1L > num || 2147483647L < num)
			{
				throw new ArgumentOutOfRangeException("timeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			return WaitHandle.SignalAndWait(toSignal, toWaitOn, (int)num, exitContext);
		}

		/// <summary>Signals one <see cref="T:System.Threading.WaitHandle" /> and waits on another, specifying a time-out interval as a 32-bit signed integer and specifying whether to exit the synchronization domain for the context before entering the wait.</summary>
		/// <returns>true if both the signal and the wait completed successfully, or false if the signal completed but the wait timed out.</returns>
		/// <param name="toSignal">The <see cref="T:System.Threading.WaitHandle" /> to signal.</param>
		/// <param name="toWaitOn">The <see cref="T:System.Threading.WaitHandle" /> to wait on.</param>
		/// <param name="millisecondsTimeout">An integer that represents the interval to wait. If the value is <see cref="F:System.Threading.Timeout.Infinite" />, that is, -1, the wait is infinite.</param>
		/// <param name="exitContext">true to exit the synchronization domain for the context before the wait (if in a synchronized context), and reacquire it afterward; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="toSignal" /> is null.-or-<paramref name="toWaitOn" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">The method is called on a thread that has <see cref="T:System.STAThreadAttribute" />. </exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This method is not supported on Windows 98 or Windows Millennium Edition. </exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="toSignal" /> is a semaphore, and it already has a full count. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="millisecondsTimeout" /> is a negative number other than -1, which represents an infinite time-out. </exception>
		/// <exception cref="T:System.Threading.AbandonedMutexException">The wait completed because a thread exited without releasing a mutex. This exception is not thrown on Windows 98 or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.WaitHandle" /> cannot be signaled because it would exceed its maximum count.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001773 RID: 6003 RVA: 0x0005B584 File Offset: 0x00059784
		public static bool SignalAndWait(WaitHandle toSignal, WaitHandle toWaitOn, int millisecondsTimeout, bool exitContext)
		{
			if (toSignal == null)
			{
				throw new ArgumentNullException("toSignal");
			}
			if (toWaitOn == null)
			{
				throw new ArgumentNullException("toWaitOn");
			}
			if (-1 > millisecondsTimeout)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", Environment.GetResourceString("Number must be either non-negative and less than or equal to Int32.MaxValue or -1."));
			}
			int num = WaitHandle.SignalAndWaitOne(toSignal.safeWaitHandle, toWaitOn.safeWaitHandle, millisecondsTimeout, toWaitOn.hasThreadAffinity, exitContext);
			if (2147483647 != num && toSignal.hasThreadAffinity)
			{
				Thread.EndCriticalRegion();
				Thread.EndThreadAffinity();
			}
			if (128 == num)
			{
				WaitHandle.ThrowAbandonedMutexException();
			}
			if (298 == num)
			{
				throw new InvalidOperationException(Environment.GetResourceString("The WaitHandle cannot be signaled because it would exceed its maximum count."));
			}
			if (299 == num)
			{
				throw new ApplicationException("Attempt to release mutex not owned by caller");
			}
			return num == 0;
		}

		// Token: 0x06001774 RID: 6004 RVA: 0x0005B63C File Offset: 0x0005983C
		private static void ThrowAbandonedMutexException()
		{
			throw new AbandonedMutexException();
		}

		// Token: 0x06001775 RID: 6005 RVA: 0x0005B643 File Offset: 0x00059843
		private static void ThrowAbandonedMutexException(int location, WaitHandle handle)
		{
			throw new AbandonedMutexException(location, handle);
		}

		/// <summary>When overridden in a derived class, releases all resources held by the current <see cref="T:System.Threading.WaitHandle" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001776 RID: 6006 RVA: 0x0005B64C File Offset: 0x0005984C
		public virtual void Close()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		/// <summary>When overridden in a derived class, releases the unmanaged resources used by the <see cref="T:System.Threading.WaitHandle" />, and optionally releases the managed resources.</summary>
		/// <param name="explicitDisposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x06001777 RID: 6007 RVA: 0x0005B65B File Offset: 0x0005985B
		protected virtual void Dispose(bool explicitDisposing)
		{
			if (this.safeWaitHandle != null)
			{
				this.safeWaitHandle.Close();
			}
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.WaitHandle" /> class.</summary>
		// Token: 0x06001778 RID: 6008 RVA: 0x0005B64C File Offset: 0x0005984C
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001779 RID: 6009 RVA: 0x0005B674 File Offset: 0x00059874
		private unsafe static int WaitOneNative(SafeHandle waitableSafeHandle, uint millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
		{
			bool flag = false;
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			int num;
			try
			{
				waitableSafeHandle.DangerousAddRef(ref flag);
				if (exitContext)
				{
					SynchronizationAttribute.ExitContext();
				}
				if (synchronizationContext != null && synchronizationContext.IsWaitNotificationRequired())
				{
					num = synchronizationContext.Wait(new IntPtr[] { waitableSafeHandle.DangerousGetHandle() }, false, (int)millisecondsTimeout);
				}
				else
				{
					IntPtr intPtr = waitableSafeHandle.DangerousGetHandle();
					num = WaitHandle.Wait_internal(&intPtr, 1, false, (int)millisecondsTimeout);
				}
			}
			finally
			{
				if (flag)
				{
					waitableSafeHandle.DangerousRelease();
				}
				if (exitContext)
				{
					SynchronizationAttribute.EnterContext();
				}
			}
			return num;
		}

		// Token: 0x0600177A RID: 6010 RVA: 0x0005B6F8 File Offset: 0x000598F8
		private unsafe static int WaitMultiple(WaitHandle[] waitHandles, int millisecondsTimeout, bool exitContext, bool WaitAll)
		{
			if (waitHandles.Length > 64)
			{
				return int.MaxValue;
			}
			int num = -1;
			SynchronizationContext synchronizationContext = SynchronizationContext.Current;
			int num2;
			try
			{
				if (exitContext)
				{
					SynchronizationAttribute.ExitContext();
				}
				for (int i = 0; i < waitHandles.Length; i++)
				{
					try
					{
					}
					finally
					{
						bool flag = false;
						waitHandles[i].SafeWaitHandle.DangerousAddRef(ref flag);
						num = i;
					}
				}
				if (synchronizationContext != null && synchronizationContext.IsWaitNotificationRequired())
				{
					IntPtr[] array = new IntPtr[waitHandles.Length];
					for (int j = 0; j < waitHandles.Length; j++)
					{
						array[j] = waitHandles[j].SafeWaitHandle.DangerousGetHandle();
					}
					num2 = synchronizationContext.Wait(array, false, millisecondsTimeout);
				}
				else
				{
					IntPtr* ptr;
					checked
					{
						ptr = stackalloc IntPtr[unchecked((UIntPtr)waitHandles.Length) * (UIntPtr)sizeof(IntPtr)];
					}
					for (int k = 0; k < waitHandles.Length; k++)
					{
						ptr[k] = waitHandles[k].SafeWaitHandle.DangerousGetHandle();
					}
					num2 = WaitHandle.Wait_internal(ptr, waitHandles.Length, WaitAll, millisecondsTimeout);
				}
			}
			finally
			{
				for (int l = num; l >= 0; l--)
				{
					waitHandles[l].SafeWaitHandle.DangerousRelease();
				}
				if (exitContext)
				{
					SynchronizationAttribute.EnterContext();
				}
			}
			return num2;
		}

		// Token: 0x0600177B RID: 6011
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern int Wait_internal(IntPtr* handles, int numHandles, bool waitAll, int ms);

		// Token: 0x0600177C RID: 6012 RVA: 0x0005B824 File Offset: 0x00059A24
		private static int SignalAndWaitOne(SafeWaitHandle waitHandleToSignal, SafeWaitHandle waitHandleToWaitOn, int millisecondsTimeout, bool hasThreadAffinity, bool exitContext)
		{
			bool flag = false;
			bool flag2 = false;
			int num;
			try
			{
				waitHandleToSignal.DangerousAddRef(ref flag);
				waitHandleToWaitOn.DangerousAddRef(ref flag2);
				num = WaitHandle.SignalAndWait_Internal(waitHandleToSignal.DangerousGetHandle(), waitHandleToWaitOn.DangerousGetHandle(), millisecondsTimeout);
			}
			finally
			{
				if (flag)
				{
					waitHandleToSignal.DangerousRelease();
				}
				if (flag2)
				{
					waitHandleToWaitOn.DangerousRelease();
				}
			}
			return num;
		}

		// Token: 0x0600177D RID: 6013
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int SignalAndWait_Internal(IntPtr toSignal, IntPtr toWaitOn, int ms);

		// Token: 0x0600177E RID: 6014 RVA: 0x0005B880 File Offset: 0x00059A80
		internal static int ToTimeoutMilliseconds(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", "Number must be either non-negative and less than or equal to Int32.MaxValue or -1.");
			}
			return (int)num;
		}

		/// <summary>Indicates that a <see cref="M:System.Threading.WaitHandle.WaitAny(System.Threading.WaitHandle[],System.Int32,System.Boolean)" /> operation timed out before any of the wait handles were signaled. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000B24 RID: 2852
		public const int WaitTimeout = 258;

		// Token: 0x04000B25 RID: 2853
		private const int MAX_WAITHANDLES = 64;

		// Token: 0x04000B26 RID: 2854
		private IntPtr waitHandle;

		// Token: 0x04000B27 RID: 2855
		internal volatile SafeWaitHandle safeWaitHandle;

		// Token: 0x04000B28 RID: 2856
		internal bool hasThreadAffinity;

		// Token: 0x04000B29 RID: 2857
		private const int WAIT_OBJECT_0 = 0;

		// Token: 0x04000B2A RID: 2858
		private const int WAIT_ABANDONED = 128;

		// Token: 0x04000B2B RID: 2859
		private const int WAIT_FAILED = 2147483647;

		// Token: 0x04000B2C RID: 2860
		private const int ERROR_TOO_MANY_POSTS = 298;

		// Token: 0x04000B2D RID: 2861
		private const int ERROR_NOT_OWNED_BY_CALLER = 299;

		/// <summary>Represents an invalid native operating system handle. This field is read-only.</summary>
		// Token: 0x04000B2E RID: 2862
		protected static readonly IntPtr InvalidHandle = (IntPtr)(-1);

		// Token: 0x04000B2F RID: 2863
		internal const int MaxWaitHandles = 64;

		// Token: 0x02000278 RID: 632
		internal enum OpenExistingResult
		{
			// Token: 0x04000B31 RID: 2865
			Success,
			// Token: 0x04000B32 RID: 2866
			NameNotFound,
			// Token: 0x04000B33 RID: 2867
			PathNotFound,
			// Token: 0x04000B34 RID: 2868
			NameInvalid
		}
	}
}
