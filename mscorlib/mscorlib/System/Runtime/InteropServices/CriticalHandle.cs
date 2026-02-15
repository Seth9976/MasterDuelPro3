using System;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime.InteropServices
{
	/// <summary>Represents a wrapper class for handle resources.</summary>
	// Token: 0x0200053E RID: 1342
	public abstract class CriticalHandle : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> class with the specified invalid handle value.</summary>
		/// <param name="invalidHandleValue">The value of an invalid handle (usually 0 or -1).</param>
		/// <exception cref="T:System.TypeLoadException">The derived class resides in an assembly without unmanaged code access permission.</exception>
		// Token: 0x06002928 RID: 10536 RVA: 0x000A8148 File Offset: 0x000A6348
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected CriticalHandle(IntPtr invalidHandleValue)
		{
			this.handle = invalidHandleValue;
			this._isClosed = false;
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x000A8160 File Offset: 0x000A6360
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~CriticalHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x0600292A RID: 10538 RVA: 0x000A8190 File Offset: 0x000A6390
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		private void Cleanup()
		{
			if (this.IsClosed)
			{
				return;
			}
			this._isClosed = true;
			if (this.IsInvalid)
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!this.ReleaseHandle())
			{
				CriticalHandle.FireCustomerDebugProbe();
			}
			Marshal.SetLastWin32Error(lastWin32Error);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600292B RID: 10539 RVA: 0x00002C89 File Offset: 0x00000E89
		private static void FireCustomerDebugProbe()
		{
		}

		/// <summary>Sets the handle to the specified pre-existing handle.</summary>
		/// <param name="handle">The pre-existing handle to use.</param>
		// Token: 0x0600292C RID: 10540 RVA: 0x000A81C8 File Offset: 0x000A63C8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		/// <summary>Gets a value indicating whether the handle is closed.</summary>
		/// <returns>true if the handle is closed; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x000A81D1 File Offset: 0x000A63D1
		public bool IsClosed
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return this._isClosed;
			}
		}

		/// <summary>When overridden in a derived class, gets a value indicating whether the handle value is invalid.</summary>
		/// <returns>true if the handle is valid; otherwise, false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600292E RID: 10542
		public abstract bool IsInvalid
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get;
		}

		/// <summary>Marks the handle for releasing and freeing resources.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600292F RID: 10543 RVA: 0x000A81D9 File Offset: 0x000A63D9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Close()
		{
			this.Dispose(true);
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.InteropServices.CriticalHandle" />. </summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06002930 RID: 10544 RVA: 0x000A81D9 File Offset: 0x000A63D9
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Runtime.InteropServices.CriticalHandle" /> class specifying whether to perform a normal dispose operation.</summary>
		/// <param name="disposing">true for a normal dispose operation; false to finalize the handle.</param>
		// Token: 0x06002931 RID: 10545 RVA: 0x000A81E2 File Offset: 0x000A63E2
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected virtual void Dispose(bool disposing)
		{
			this.Cleanup();
		}

		/// <summary>When overridden in a derived class, executes the code required to free the handle.</summary>
		/// <returns>true if the handle is released successfully; otherwise, in the event of a catastrophic failure, false. In this case, it generates a releaseHandleFailed MDA Managed Debugging Assistant.</returns>
		// Token: 0x06002932 RID: 10546
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		protected abstract bool ReleaseHandle();

		/// <summary>Specifies the handle to be wrapped.</summary>
		// Token: 0x04001584 RID: 5508
		protected IntPtr handle;

		// Token: 0x04001585 RID: 5509
		private bool _isClosed;
	}
}
