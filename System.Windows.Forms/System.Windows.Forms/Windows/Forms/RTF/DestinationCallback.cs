using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000382 RID: 898
	internal class DestinationCallback
	{
		// Token: 0x06001D2C RID: 7468 RVA: 0x0008AE83 File Offset: 0x00089083
		public DestinationCallback()
		{
			this.callbacks = new DestinationDelegate[Enum.GetValues(typeof(Minor)).Length];
		}

		// Token: 0x17000744 RID: 1860
		public DestinationDelegate this[Minor c]
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

		// Token: 0x0400185A RID: 6234
		private DestinationDelegate[] callbacks;
	}
}
