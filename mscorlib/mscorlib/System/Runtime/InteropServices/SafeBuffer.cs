using System;
using Microsoft.Win32.SafeHandles;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a controlled memory buffer that can be used for reading and writing. Attempts to access memory outside the controlled buffer (underruns and overruns) raise exceptions.</summary>
	// Token: 0x02000519 RID: 1305
	public abstract class SafeBuffer : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>Obtains a pointer from a <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> object for a block of memory.</summary>
		/// <param name="pointer">A byte pointer, passed by reference, to receive the pointer from within the <see cref="T:System.Runtime.InteropServices.SafeBuffer" /> object. You must set this pointer to null before you call this method.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> method has not been called. </exception>
		// Token: 0x060028F7 RID: 10487 RVA: 0x000A7CF4 File Offset: 0x000A5EF4
		[CLSCompliant(false)]
		public unsafe void AcquirePointer(ref byte* pointer)
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			pointer = (IntPtr)((UIntPtr)0);
			bool flag = false;
			base.DangerousAddRef(ref flag);
			pointer = (void*)this.handle;
		}

		/// <summary>Releases a pointer that was obtained by the <see cref="M:System.Runtime.InteropServices.SafeBuffer.AcquirePointer(System.Byte*@)" /> method.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="Overload:System.Runtime.InteropServices.SafeBuffer.Initialize" /> method has not been called.</exception>
		// Token: 0x060028F8 RID: 10488 RVA: 0x000A7D34 File Offset: 0x000A5F34
		public void ReleasePointer()
		{
			if (this._numBytes == SafeBuffer.Uninitialized)
			{
				throw SafeBuffer.NotInitialized();
			}
			base.DangerousRelease();
		}

		// Token: 0x060028F9 RID: 10489 RVA: 0x000A7D54 File Offset: 0x000A5F54
		private static InvalidOperationException NotInitialized()
		{
			return new InvalidOperationException("You must call Initialize on this object instance before using it.");
		}

		// Token: 0x040014EC RID: 5356
		private static readonly UIntPtr Uninitialized = ((UIntPtr.Size == 4) ? ((UIntPtr)uint.MaxValue) : ((UIntPtr)ulong.MaxValue));

		// Token: 0x040014ED RID: 5357
		private UIntPtr _numBytes;
	}
}
