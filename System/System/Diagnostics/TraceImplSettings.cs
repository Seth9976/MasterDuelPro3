using System;

namespace System.Diagnostics
{
	// Token: 0x02000192 RID: 402
	internal class TraceImplSettings
	{
		// Token: 0x0600099B RID: 2459 RVA: 0x000322CF File Offset: 0x000304CF
		public TraceImplSettings()
		{
			this.Listeners.Add(new DefaultTraceListener
			{
				IndentSize = this.IndentSize
			});
		}

		// Token: 0x04000731 RID: 1841
		public bool AutoFlush;

		// Token: 0x04000732 RID: 1842
		public int IndentSize = 4;

		// Token: 0x04000733 RID: 1843
		public TraceListenerCollection Listeners = new TraceListenerCollection();
	}
}
