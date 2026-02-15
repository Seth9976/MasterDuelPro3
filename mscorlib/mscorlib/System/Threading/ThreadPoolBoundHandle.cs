using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Unity;

namespace System.Threading
{
	// Token: 0x02000242 RID: 578
	public sealed class ThreadPoolBoundHandle : IDisposable, IDeferredDisposable
	{
		// Token: 0x06001538 RID: 5432 RVA: 0x000107A8 File Offset: 0x0000E9A8
		static ThreadPoolBoundHandle()
		{
			if (!Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06001539 RID: 5433 RVA: 0x00055536 File Offset: 0x00053736
		private ThreadPoolBoundHandle(SafeHandle handle, SafeThreadPoolIOHandle threadPoolHandle)
		{
			this._threadPoolHandle = threadPoolHandle;
			this._handle = handle;
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x0005554C File Offset: 0x0005374C
		public SafeHandle Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x0600153B RID: 5435 RVA: 0x00055554 File Offset: 0x00053754
		public static ThreadPoolBoundHandle BindHandle(SafeHandle handle)
		{
			if (handle == null)
			{
				throw new ArgumentNullException("handle");
			}
			if (handle.IsClosed || handle.IsInvalid)
			{
				throw new ArgumentException("'handle' has been disposed or is an invalid handle.", "handle");
			}
			IntPtr intPtr = AddrofIntrinsics.AddrOf<Interop.NativeIoCompletionCallback>(new Interop.NativeIoCompletionCallback(ThreadPoolBoundHandle.OnNativeIOCompleted));
			SafeThreadPoolIOHandle safeThreadPoolIOHandle = Interop.mincore.CreateThreadpoolIo(handle, intPtr, IntPtr.Zero, IntPtr.Zero);
			if (!safeThreadPoolIOHandle.IsInvalid)
			{
				return new ThreadPoolBoundHandle(handle, safeThreadPoolIOHandle);
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (lastWin32Error == 6)
			{
				throw new ArgumentException("'handle' has been disposed or is an invalid handle.", "handle");
			}
			if (lastWin32Error == 87)
			{
				throw new ArgumentException("'handle' has already been bound to the thread pool, or was not opened for asynchronous I/O.", "handle");
			}
			throw Win32Marshal.GetExceptionForWin32Error(lastWin32Error, "");
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000555FC File Offset: 0x000537FC
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* AllocateNativeOverlapped(IOCompletionCallback callback, object state, object pinData)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.AddRef();
			NativeOverlapped* ptr2;
			try
			{
				Win32ThreadPoolNativeOverlapped* ptr = Win32ThreadPoolNativeOverlapped.Allocate(callback, state, pinData, null);
				ptr->Data._boundHandle = this;
				Interop.mincore.StartThreadpoolIo(this._threadPoolHandle);
				ptr2 = Win32ThreadPoolNativeOverlapped.ToNativeOverlapped(ptr);
			}
			catch
			{
				this.Release();
				throw;
			}
			return ptr2;
		}

		// Token: 0x0600153D RID: 5437 RVA: 0x00055660 File Offset: 0x00053860
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* AllocateNativeOverlapped(PreAllocatedOverlapped preAllocated)
		{
			if (preAllocated == null)
			{
				throw new ArgumentNullException("preAllocated");
			}
			bool flag = false;
			bool flag2 = false;
			NativeOverlapped* ptr;
			try
			{
				flag = this.AddRef();
				flag2 = preAllocated.AddRef();
				Win32ThreadPoolNativeOverlapped.OverlappedData data = preAllocated._overlapped->Data;
				if (data._boundHandle != null)
				{
					throw new ArgumentException("'preAllocated' is already in use.", "preAllocated");
				}
				data._boundHandle = this;
				Interop.mincore.StartThreadpoolIo(this._threadPoolHandle);
				ptr = Win32ThreadPoolNativeOverlapped.ToNativeOverlapped(preAllocated._overlapped);
			}
			catch
			{
				if (flag2)
				{
					preAllocated.Release();
				}
				if (flag)
				{
					this.Release();
				}
				throw;
			}
			return ptr;
		}

		// Token: 0x0600153E RID: 5438 RVA: 0x000556F8 File Offset: 0x000538F8
		[CLSCompliant(false)]
		public unsafe void FreeNativeOverlapped(NativeOverlapped* overlapped)
		{
			if (overlapped == null)
			{
				throw new ArgumentNullException("overlapped");
			}
			Win32ThreadPoolNativeOverlapped* ptr = Win32ThreadPoolNativeOverlapped.FromNativeOverlapped(overlapped);
			Win32ThreadPoolNativeOverlapped.OverlappedData overlappedData = ThreadPoolBoundHandle.GetOverlappedData(ptr, this);
			if (!overlappedData._completed)
			{
				Interop.mincore.CancelThreadpoolIo(this._threadPoolHandle);
				this.Release();
			}
			overlappedData._boundHandle = null;
			overlappedData._completed = false;
			if (overlappedData._preAllocated != null)
			{
				overlappedData._preAllocated.Release();
				return;
			}
			Win32ThreadPoolNativeOverlapped.Free(ptr);
		}

		// Token: 0x0600153F RID: 5439 RVA: 0x00055765 File Offset: 0x00053965
		[CLSCompliant(false)]
		public unsafe static object GetNativeOverlappedState(NativeOverlapped* overlapped)
		{
			if (overlapped == null)
			{
				throw new ArgumentNullException("overlapped");
			}
			return ThreadPoolBoundHandle.GetOverlappedData(Win32ThreadPoolNativeOverlapped.FromNativeOverlapped(overlapped), null)._state;
		}

		// Token: 0x06001540 RID: 5440 RVA: 0x00055788 File Offset: 0x00053988
		private unsafe static Win32ThreadPoolNativeOverlapped.OverlappedData GetOverlappedData(Win32ThreadPoolNativeOverlapped* overlapped, ThreadPoolBoundHandle expectedBoundHandle)
		{
			Win32ThreadPoolNativeOverlapped.OverlappedData data = overlapped->Data;
			if (data._boundHandle == null)
			{
				throw new ArgumentException("'overlapped' has already been freed.", "overlapped");
			}
			if (expectedBoundHandle != null && data._boundHandle != expectedBoundHandle)
			{
				throw new ArgumentException("'overlapped' was not allocated by this ThreadPoolBoundHandle instance.", "overlapped");
			}
			return data;
		}

		// Token: 0x06001541 RID: 5441 RVA: 0x000557D4 File Offset: 0x000539D4
		[NativeCallable(CallingConvention = CallingConvention.StdCall)]
		private unsafe static void OnNativeIOCompleted(IntPtr instance, IntPtr context, IntPtr overlappedPtr, uint ioResult, UIntPtr numberOfBytesTransferred, IntPtr ioPtr)
		{
			ThreadPoolCallbackWrapper threadPoolCallbackWrapper = ThreadPoolCallbackWrapper.Enter();
			Win32ThreadPoolNativeOverlapped* ptr = (Win32ThreadPoolNativeOverlapped*)(void*)overlappedPtr;
			ThreadPoolBoundHandle boundHandle = ptr->Data._boundHandle;
			if (boundHandle == null)
			{
				throw new InvalidOperationException("'overlapped' has already been freed.");
			}
			boundHandle.Release();
			Win32ThreadPoolNativeOverlapped.CompleteWithCallback(ioResult, (uint)numberOfBytesTransferred, ptr);
			threadPoolCallbackWrapper.Exit(true);
		}

		// Token: 0x06001542 RID: 5442 RVA: 0x00055822 File Offset: 0x00053A22
		private bool AddRef()
		{
			return this._lifetime.AddRef(this);
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00055830 File Offset: 0x00053A30
		private void Release()
		{
			this._lifetime.Release(this);
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x0005583E File Offset: 0x00053A3E
		public void Dispose()
		{
			this._lifetime.Dispose(this);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00055854 File Offset: 0x00053A54
		~ThreadPoolBoundHandle()
		{
			if (!Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
			if (!Environment.HasShutdownStarted)
			{
				this.Dispose();
			}
		}

		// Token: 0x06001546 RID: 5446 RVA: 0x00055894 File Offset: 0x00053A94
		void IDeferredDisposable.OnFinalRelease(bool disposed)
		{
			if (disposed)
			{
				this._threadPoolHandle.Dispose();
			}
		}

		// Token: 0x06001547 RID: 5447 RVA: 0x000176B9 File Offset: 0x000158B9
		internal ThreadPoolBoundHandle()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000A6A RID: 2666
		private readonly SafeHandle _handle;

		// Token: 0x04000A6B RID: 2667
		private readonly SafeThreadPoolIOHandle _threadPoolHandle;

		// Token: 0x04000A6C RID: 2668
		private DeferredDisposableLifetime<ThreadPoolBoundHandle> _lifetime;
	}
}
