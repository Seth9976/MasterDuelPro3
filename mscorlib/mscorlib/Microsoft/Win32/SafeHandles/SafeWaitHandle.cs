using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Represents a wrapper class for a wait handle. </summary>
	// Token: 0x0200008F RID: 143
	public sealed class SafeWaitHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeWaitHandle" /> class. </summary>
		/// <param name="existingHandle">An <see cref="T:System.IntPtr" /> object that represents the pre-existing handle to use.</param>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release (not recommended).</param>
		// Token: 0x060002B0 RID: 688 RVA: 0x00010709 File Offset: 0x0000E909
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public SafeWaitHandle(IntPtr existingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x00010814 File Offset: 0x0000EA14
		protected override bool ReleaseHandle()
		{
			NativeEventCalls.CloseEvent_internal(this.handle);
			return true;
		}
	}
}
