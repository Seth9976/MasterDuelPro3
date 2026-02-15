using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200037F RID: 895
	internal class ClassCallback
	{
		// Token: 0x06001D1D RID: 7453 RVA: 0x0008AD14 File Offset: 0x00088F14
		public ClassCallback()
		{
			this.callbacks = new ClassDelegate[Enum.GetValues(typeof(Major)).Length];
		}

		// Token: 0x1700073F RID: 1855
		public ClassDelegate this[TokenClass c]
		{
			get
			{
				return this.callbacks[(int)c];
			}
			set
			{
				this.callbacks[(int)c] = value;
			}
		}

		// Token: 0x04001854 RID: 6228
		private ClassDelegate[] callbacks;
	}
}
