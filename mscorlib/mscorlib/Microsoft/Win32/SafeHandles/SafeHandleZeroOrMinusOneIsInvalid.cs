using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Provides a base class for Win32 safe handle implementations in which the value of either 0 or -1 indicates an invalid handle.</summary>
	// Token: 0x02000090 RID: 144
	public abstract class SafeHandleZeroOrMinusOneIsInvalid : SafeHandle
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeHandleZeroOrMinusOneIsInvalid" /> class, specifying whether the handle is to be reliably released. </summary>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release (not recommended).</param>
		// Token: 0x060002B2 RID: 690 RVA: 0x00010822 File Offset: 0x0000EA22
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		protected SafeHandleZeroOrMinusOneIsInvalid(bool ownsHandle)
			: base(IntPtr.Zero, ownsHandle)
		{
		}

		/// <summary>Gets a value that indicates whether the handle is invalid.</summary>
		/// <returns>true if the handle is not valid; otherwise, false.</returns>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00010830 File Offset: 0x0000EA30
		public override bool IsInvalid
		{
			get
			{
				return this.handle.IsNull() || this.handle == new IntPtr(-1);
			}
		}
	}
}
