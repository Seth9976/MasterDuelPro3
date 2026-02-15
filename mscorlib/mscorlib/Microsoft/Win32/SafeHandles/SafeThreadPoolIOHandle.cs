using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200008C RID: 140
	internal class SafeThreadPoolIOHandle : SafeHandle
	{
		// Token: 0x060002A8 RID: 680 RVA: 0x000107A8 File Offset: 0x0000E9A8
		static SafeThreadPoolIOHandle()
		{
			if (!Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x000107B7 File Offset: 0x0000E9B7
		private SafeThreadPoolIOHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060002AA RID: 682 RVA: 0x000107C5 File Offset: 0x0000E9C5
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x000107D7 File Offset: 0x0000E9D7
		protected override bool ReleaseHandle()
		{
			Interop.mincore.CloseThreadpoolIo(this.handle);
			return true;
		}
	}
}
