using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Provides a managed representation of a Win32 OVERLAPPED structure, including methods to transfer information from an <see cref="T:System.Threading.Overlapped" /> instance to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000282 RID: 642
	[ComVisible(true)]
	public class Overlapped
	{
		/// <summary>Initializes a new, empty instance of the <see cref="T:System.Threading.Overlapped" /> class.</summary>
		// Token: 0x060017D9 RID: 6105 RVA: 0x00003CE1 File Offset: 0x00001EE1
		public Overlapped()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Overlapped" /> class with the specified file position, the 32-bit integer handle to an event that is signaled when the I/O operation is complete, and an interface through which to return the results of the operation.</summary>
		/// <param name="offsetLo">The low word of the file position at which to start the transfer. </param>
		/// <param name="offsetHi">The high word of the file position at which to start the transfer. </param>
		/// <param name="hEvent">The handle to an event that is signaled when the I/O operation is complete. </param>
		/// <param name="ar">An object that implements the <see cref="T:System.IAsyncResult" /> interface and provides status information on the I/O operation. </param>
		// Token: 0x060017DA RID: 6106 RVA: 0x0005BFA5 File Offset: 0x0005A1A5
		[Obsolete("Not 64bit compatible.  Please use the constructor that takes IntPtr for the event handle")]
		public Overlapped(int offsetLo, int offsetHi, int hEvent, IAsyncResult ar)
		{
			this.offsetL = offsetLo;
			this.offsetH = offsetHi;
			this.evt = hEvent;
			this.ares = ar;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Overlapped" /> class with the specified file position, the handle to an event that is signaled when the I/O operation is complete, and an interface through which to return the results of the operation.</summary>
		/// <param name="offsetLo">The low word of the file position at which to start the transfer. </param>
		/// <param name="offsetHi">The high word of the file position at which to start the transfer. </param>
		/// <param name="hEvent">The handle to an event that is signaled when the I/O operation is complete. </param>
		/// <param name="ar">An object that implements the <see cref="T:System.IAsyncResult" /> interface and provides status information on the I/O operation. </param>
		// Token: 0x060017DB RID: 6107 RVA: 0x0005BFCA File Offset: 0x0005A1CA
		public Overlapped(int offsetLo, int offsetHi, IntPtr hEvent, IAsyncResult ar)
		{
			this.offsetL = offsetLo;
			this.offsetH = offsetHi;
			this.evt_ptr = hEvent;
			this.ares = ar;
		}

		/// <summary>Frees the unmanaged memory associated with a native overlapped structure allocated by the <see cref="Overload:System.Threading.Overlapped.Pack" /> method.</summary>
		/// <param name="nativeOverlappedPtr">A pointer to the <see cref="T:System.Threading.NativeOverlapped" /> structure to be freed.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nativeOverlappedPtr" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017DC RID: 6108 RVA: 0x0005BFEF File Offset: 0x0005A1EF
		[CLSCompliant(false)]
		public unsafe static void Free(NativeOverlapped* nativeOverlappedPtr)
		{
			if ((IntPtr)((void*)nativeOverlappedPtr) == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			Marshal.FreeHGlobal((IntPtr)((void*)nativeOverlappedPtr));
		}

		/// <summary>Unpacks the specified unmanaged <see cref="T:System.Threading.NativeOverlapped" /> structure into a managed <see cref="T:System.Threading.Overlapped" /> object. </summary>
		/// <returns>An <see cref="T:System.Threading.Overlapped" /> object containing the information unpacked from the native structure.</returns>
		/// <param name="nativeOverlappedPtr">An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="nativeOverlappedPtr" /> is null.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060017DD RID: 6109 RVA: 0x0005C01C File Offset: 0x0005A21C
		[CLSCompliant(false)]
		public unsafe static Overlapped Unpack(NativeOverlapped* nativeOverlappedPtr)
		{
			if ((IntPtr)((void*)nativeOverlappedPtr) == IntPtr.Zero)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			return new Overlapped
			{
				offsetL = nativeOverlappedPtr->OffsetLow,
				offsetH = nativeOverlappedPtr->OffsetHigh,
				evt = (int)nativeOverlappedPtr->EventHandle
			};
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure, specifying the delegate to be invoked when the asynchronous I/O operation is complete.</summary>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure. </returns>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> has already been packed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017DE RID: 6110 RVA: 0x0005C074 File Offset: 0x0005A274
		[MonoTODO("Security - we need to propagate the call stack")]
		[Obsolete("Use Pack(iocb, userData) instead")]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb)
		{
			NativeOverlapped* ptr = (NativeOverlapped*)(void*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			ptr->OffsetLow = this.offsetL;
			ptr->OffsetHigh = this.offsetH;
			ptr->EventHandle = (IntPtr)this.evt;
			return ptr;
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure, specifying a delegate that is invoked when the asynchronous I/O operation is complete and a managed object that serves as a buffer.</summary>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure. </returns>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <param name="userData">An object or array of objects representing the input or output buffer for the operation. Each object represents a buffer, for example an array of bytes.</param>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> has already been packed.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060017DF RID: 6111 RVA: 0x0005C0C8 File Offset: 0x0005A2C8
		[ComVisible(false)]
		[CLSCompliant(false)]
		[MonoTODO("handle userData")]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
		{
			NativeOverlapped* ptr = (NativeOverlapped*)(void*)Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeOverlapped)));
			ptr->OffsetLow = this.offsetL;
			ptr->OffsetHigh = this.offsetH;
			ptr->EventHandle = this.evt_ptr;
			return ptr;
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure specifying the delegate to invoke when the asynchronous I/O operation is complete. Does not propagate the calling stack.</summary>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure. </returns>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> has already been packed.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060017E0 RID: 6112 RVA: 0x0005C114 File Offset: 0x0005A314
		[Obsolete("Use UnsafePack(iocb, userData) instead")]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb)
		{
			return this.Pack(iocb);
		}

		/// <summary>Packs the current instance into a <see cref="T:System.Threading.NativeOverlapped" /> structure, specifying the delegate to invoke when the asynchronous I/O operation is complete and the managed object that serves as a buffer. Does not propagate the calling stack.</summary>
		/// <returns>An unmanaged pointer to a <see cref="T:System.Threading.NativeOverlapped" /> structure. </returns>
		/// <param name="iocb">An <see cref="T:System.Threading.IOCompletionCallback" /> delegate that represents the callback method invoked when the asynchronous I/O operation completes.</param>
		/// <param name="userData">An object or array of objects representing the input or output buffer for the operation. Each object represents a buffer, for example an array of bytes.</param>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <exception cref="T:System.InvalidOperationException">The current <see cref="T:System.Threading.Overlapped" /> is already packed.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x060017E1 RID: 6113 RVA: 0x0005C11D File Offset: 0x0005A31D
		[ComVisible(false)]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
		{
			return this.Pack(iocb, userData);
		}

		/// <summary>Gets or sets the object that provides status information on the I/O operation.</summary>
		/// <returns>An object that implements the <see cref="T:System.IAsyncResult" /> interface.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x060017E2 RID: 6114 RVA: 0x0005C127 File Offset: 0x0005A327
		// (set) Token: 0x060017E3 RID: 6115 RVA: 0x0005C12F File Offset: 0x0005A32F
		public IAsyncResult AsyncResult
		{
			get
			{
				return this.ares;
			}
			set
			{
				this.ares = value;
			}
		}

		/// <summary>Gets or sets the 32-bit integer handle to a synchronization event that is signaled when the I/O operation is complete.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the handle of the synchronization event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x060017E4 RID: 6116 RVA: 0x0005C138 File Offset: 0x0005A338
		// (set) Token: 0x060017E5 RID: 6117 RVA: 0x0005C140 File Offset: 0x0005A340
		[Obsolete("Not 64bit compatible.  Use EventHandleIntPtr instead.")]
		public int EventHandle
		{
			get
			{
				return this.evt;
			}
			set
			{
				this.evt = value;
			}
		}

		/// <summary>Gets or sets the handle to the synchronization event that is signaled when the I/O operation is complete.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> representing the handle of the event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x0005C149 File Offset: 0x0005A349
		// (set) Token: 0x060017E7 RID: 6119 RVA: 0x0005C151 File Offset: 0x0005A351
		[ComVisible(false)]
		public IntPtr EventHandleIntPtr
		{
			get
			{
				return this.evt_ptr;
			}
			set
			{
				this.evt_ptr = value;
			}
		}

		/// <summary>Gets or sets the high-order word of the file position at which to start the transfer. The file position is a byte offset from the start of the file.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the high word of the file position.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060017E8 RID: 6120 RVA: 0x0005C15A File Offset: 0x0005A35A
		// (set) Token: 0x060017E9 RID: 6121 RVA: 0x0005C162 File Offset: 0x0005A362
		public int OffsetHigh
		{
			get
			{
				return this.offsetH;
			}
			set
			{
				this.offsetH = value;
			}
		}

		/// <summary>Gets or sets the low-order word of the file position at which to start the transfer. The file position is a byte offset from the start of the file.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value representing the low word of the file position.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060017EA RID: 6122 RVA: 0x0005C16B File Offset: 0x0005A36B
		// (set) Token: 0x060017EB RID: 6123 RVA: 0x0005C173 File Offset: 0x0005A373
		public int OffsetLow
		{
			get
			{
				return this.offsetL;
			}
			set
			{
				this.offsetL = value;
			}
		}

		// Token: 0x04000B3C RID: 2876
		private IAsyncResult ares;

		// Token: 0x04000B3D RID: 2877
		private int offsetL;

		// Token: 0x04000B3E RID: 2878
		private int offsetH;

		// Token: 0x04000B3F RID: 2879
		private int evt;

		// Token: 0x04000B40 RID: 2880
		private IntPtr evt_ptr;
	}
}
