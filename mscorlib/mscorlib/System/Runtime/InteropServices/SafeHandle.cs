using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents a wrapper class for operating system handles. This class must be inherited.</summary>
	// Token: 0x02000542 RID: 1346
	[StructLayout(LayoutKind.Sequential)]
	public abstract class SafeHandle : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class with the specified invalid handle value.</summary>
		/// <param name="invalidHandleValue">The value of an invalid handle (usually 0 or -1).  Your implementation of <see cref="P:System.Runtime.InteropServices.SafeHandle.IsInvalid" /> should return true for this value.</param>
		/// <param name="ownsHandle">true to reliably let <see cref="T:System.Runtime.InteropServices.SafeHandle" /> release the handle during the finalization phase; otherwise, false (not recommended). </param>
		/// <exception cref="T:System.TypeLoadException">The derived class resides in an assembly without unmanaged code access permission. </exception>
		// Token: 0x06002937 RID: 10551 RVA: 0x000A81F9 File Offset: 0x000A63F9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandle(IntPtr invalidHandleValue, bool ownsHandle)
		{
			this.handle = invalidHandleValue;
			this._state = 4;
			this._ownsHandle = ownsHandle;
			if (!ownsHandle)
			{
				GC.SuppressFinalize(this);
			}
			this._fullyInitialized = true;
		}

		// Token: 0x06002938 RID: 10552 RVA: 0x000A8228 File Offset: 0x000A6428
		~SafeHandle()
		{
			this.Dispose(false);
		}

		/// <summary>Sets the handle to the specified pre-existing handle.</summary>
		/// <param name="handle">The pre-existing handle to use. </param>
		// Token: 0x06002939 RID: 10553 RVA: 0x000A8258 File Offset: 0x000A6458
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		/// <summary>Returns the value of the <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" /> field.</summary>
		/// <returns>An IntPtr representing the value of the <see cref="F:System.Runtime.InteropServices.SafeHandle.handle" /> field. If the handle has been marked invalid with <see cref="M:System.Runtime.InteropServices.SafeHandle.SetHandleAsInvalid" />, this method still returns the original handle value, which can be a stale value.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600293A RID: 10554 RVA: 0x000A8261 File Offset: 0x000A6461
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		/// <summary>Gets a value indicating whether the handle is closed.</summary>
		/// <returns>true if the handle is closed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x0600293B RID: 10555 RVA: 0x000A8269 File Offset: 0x000A6469
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return (this._state & 1) == 1;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the handle value is invalid.</summary>
		/// <returns>true if the handle value is invalid; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x0600293C RID: 10556
		public abstract bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get;
		}

		/// <summary>Marks the handle for releasing and freeing resources.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600293D RID: 10557 RVA: 0x000A8276 File Offset: 0x000A6476
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600293E RID: 10558 RVA: 0x000A8276 File Offset: 0x000A6476
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Runtime.InteropServices.SafeHandle" /> class specifying whether to perform a normal dispose operation.</summary>
		/// <param name="disposing">true for a normal dispose operation; false to finalize the handle.</param>
		// Token: 0x0600293F RID: 10559 RVA: 0x000A827F File Offset: 0x000A647F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.InternalDispose();
				return;
			}
			this.InternalFinalize();
		}

		/// <summary>When overridden in a derived class, executes the code required to free the handle.</summary>
		/// <returns>true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.</returns>
		// Token: 0x06002940 RID: 10560
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseHandle();

		/// <summary>Marks a handle as no longer used.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002941 RID: 10561 RVA: 0x000A8294 File Offset: 0x000A6494
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void SetHandleAsInvalid()
		{
			try
			{
			}
			finally
			{
				int state;
				int num;
				do
				{
					state = this._state;
					num = state | 1;
				}
				while (Interlocked.CompareExchange(ref this._state, num, state) != state);
				GC.SuppressFinalize(this);
			}
		}

		/// <summary>Manually increments the reference counter on <see cref="T:System.Runtime.InteropServices.SafeHandle" /> instances.</summary>
		/// <param name="success">true if the reference counter was successfully incremented; otherwise, false.</param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002942 RID: 10562 RVA: 0x000A82D8 File Offset: 0x000A64D8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public void DangerousAddRef(ref bool success)
		{
			try
			{
			}
			finally
			{
				if (!this._fullyInitialized)
				{
					throw new InvalidOperationException();
				}
				for (;;)
				{
					int state = this._state;
					if ((state & 1) != 0)
					{
						break;
					}
					int num = state + 4;
					if (Interlocked.CompareExchange(ref this._state, num, state) == state)
					{
						goto Block_5;
					}
				}
				throw new ObjectDisposedException(null, "Safe handle has been closed");
				Block_5:
				success = true;
			}
		}

		/// <summary>Manually decrements the reference counter on a <see cref="T:System.Runtime.InteropServices.SafeHandle" /> instance.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002943 RID: 10563 RVA: 0x000A8338 File Offset: 0x000A6538
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void DangerousRelease()
		{
			this.DangerousReleaseInternal(false);
		}

		// Token: 0x06002944 RID: 10564 RVA: 0x000A8341 File Offset: 0x000A6541
		private void InternalDispose()
		{
			if (!this._fullyInitialized)
			{
				throw new InvalidOperationException();
			}
			this.DangerousReleaseInternal(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002945 RID: 10565 RVA: 0x000A835E File Offset: 0x000A655E
		private void InternalFinalize()
		{
			if (this._fullyInitialized)
			{
				this.DangerousReleaseInternal(true);
			}
		}

		// Token: 0x06002946 RID: 10566 RVA: 0x000A8370 File Offset: 0x000A6570
		private void DangerousReleaseInternal(bool dispose)
		{
			try
			{
			}
			finally
			{
				if (!this._fullyInitialized)
				{
					throw new InvalidOperationException();
				}
				bool flag;
				for (;;)
				{
					int state = this._state;
					if (dispose && (state & 2) != 0)
					{
						break;
					}
					if ((state & 2147483644) == 0)
					{
						goto Block_6;
					}
					flag = (state & 2147483644) == 4 && (state & 1) == 0 && this._ownsHandle && !this.IsInvalid;
					int num = state - 4;
					if ((state & 2147483644) == 4)
					{
						num |= 1;
					}
					if (dispose)
					{
						num |= 2;
					}
					if (Interlocked.CompareExchange(ref this._state, num, state) == state)
					{
						goto IL_009A;
					}
				}
				flag = false;
				goto IL_009A;
				Block_6:
				throw new ObjectDisposedException(null, "Safe handle has been closed");
				IL_009A:
				if (flag)
				{
					this.ReleaseHandle();
				}
			}
		}

		/// <summary>Specifies the handle to be wrapped.</summary>
		// Token: 0x04001587 RID: 5511
		protected IntPtr handle;

		// Token: 0x04001588 RID: 5512
		private int _state;

		// Token: 0x04001589 RID: 5513
		private bool _ownsHandle;

		// Token: 0x0400158A RID: 5514
		private bool _fullyInitialized;

		// Token: 0x0400158B RID: 5515
		private const int RefCount_Mask = 2147483644;

		// Token: 0x0400158C RID: 5516
		private const int RefCount_One = 4;
	}
}
