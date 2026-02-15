using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000297 RID: 663
	internal struct SafeHandleAccess
	{
		// Token: 0x060011E6 RID: 4582 RVA: 0x0004ADC2 File Offset: 0x00048FC2
		public SafeHandleAccess(IntPtr ptr)
		{
			this.m_Handle = ptr;
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x0004ADCC File Offset: 0x00048FCC
		public bool IsNull()
		{
			return this.m_Handle == IntPtr.Zero;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x0004ADF0 File Offset: 0x00048FF0
		public static implicit operator IntPtr(SafeHandleAccess a)
		{
			bool flag = a.m_Handle == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentNullException();
			}
			return a.m_Handle;
		}

		// Token: 0x04000A4E RID: 2638
		private IntPtr m_Handle;
	}
}
