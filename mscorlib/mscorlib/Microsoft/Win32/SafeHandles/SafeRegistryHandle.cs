using System;

namespace Microsoft.Win32.SafeHandles
{
	/// <summary>Represents a safe handle to the Windows registry.</summary>
	// Token: 0x0200008A RID: 138
	public sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600029E RID: 670 RVA: 0x000106F9 File Offset: 0x0000E8F9
		protected override bool ReleaseHandle()
		{
			return Interop.Advapi32.RegCloseKey(this.handle) == 0;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000106E3 File Offset: 0x0000E8E3
		internal SafeRegistryHandle()
			: base(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.Win32.SafeHandles.SafeRegistryHandle" /> class. </summary>
		/// <param name="preexistingHandle">An object that represents the pre-existing handle to use.</param>
		/// <param name="ownsHandle">true to reliably release the handle during the finalization phase; false to prevent reliable release.</param>
		// Token: 0x060002A0 RID: 672 RVA: 0x00010709 File Offset: 0x0000E909
		public SafeRegistryHandle(IntPtr preexistingHandle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}
	}
}
