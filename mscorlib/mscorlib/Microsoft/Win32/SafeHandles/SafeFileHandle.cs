using System;
using System.IO;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Represents a wrapper class for a file handle. </summary>
	// Token: 0x0200008D RID: 141
	public sealed class SafeFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeFileHandle" /> class. </summary>
		/// <param name="preexistingHandle">An <see cref="T:System.IntPtr" /> object that represents the pre-existing handle to use.</param>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release (not recommended).</param>
		// Token: 0x060002AC RID: 684 RVA: 0x00010709 File Offset: 0x0000E909
		public SafeFileHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000107E8 File Offset: 0x0000E9E8
		protected override bool ReleaseHandle()
		{
			MonoIOError monoIOError;
			MonoIO.Close(this.handle, out monoIOError);
			return monoIOError == MonoIOError.ERROR_SUCCESS;
		}
	}
}
