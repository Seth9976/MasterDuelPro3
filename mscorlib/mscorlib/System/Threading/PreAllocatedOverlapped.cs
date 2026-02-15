using System;

namespace System.Threading
{
	// Token: 0x02000246 RID: 582
	public sealed class PreAllocatedOverlapped : IDisposable, IDeferredDisposable
	{
		// Token: 0x06001555 RID: 5461 RVA: 0x000107A8 File Offset: 0x0000E9A8
		static PreAllocatedOverlapped()
		{
			if (!Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00055CD7 File Offset: 0x00053ED7
		[CLSCompliant(false)]
		public PreAllocatedOverlapped(IOCompletionCallback callback, object state, object pinData)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this._overlapped = Win32ThreadPoolNativeOverlapped.Allocate(callback, state, pinData, this);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00055CFC File Offset: 0x00053EFC
		internal bool AddRef()
		{
			return this._lifetime.AddRef(this);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00055D0A File Offset: 0x00053F0A
		internal void Release()
		{
			this._lifetime.Release(this);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00055D18 File Offset: 0x00053F18
		public void Dispose()
		{
			this._lifetime.Dispose(this);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x00055D2C File Offset: 0x00053F2C
		~PreAllocatedOverlapped()
		{
			if (!Environment.HasShutdownStarted)
			{
				this.Dispose();
			}
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00055D60 File Offset: 0x00053F60
		unsafe void IDeferredDisposable.OnFinalRelease(bool disposed)
		{
			if (this._overlapped != null)
			{
				if (disposed)
				{
					Win32ThreadPoolNativeOverlapped.Free(this._overlapped);
					return;
				}
				*Win32ThreadPoolNativeOverlapped.ToNativeOverlapped(this._overlapped) = default(NativeOverlapped);
			}
		}

		// Token: 0x04000A80 RID: 2688
		internal unsafe readonly Win32ThreadPoolNativeOverlapped* _overlapped;

		// Token: 0x04000A81 RID: 2689
		private DeferredDisposableLifetime<PreAllocatedOverlapped> _lifetime;
	}
}
