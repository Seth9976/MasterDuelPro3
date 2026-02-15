using System;

namespace System.Diagnostics
{
	// Token: 0x02000193 RID: 403
	internal class TraceSourceInfo
	{
		// Token: 0x0600099C RID: 2460 RVA: 0x00032306 File Offset: 0x00030506
		internal TraceSourceInfo(string name, SourceLevels levels, TraceImplSettings settings)
		{
			this.name = name;
			this.levels = levels;
			this.listeners = new TraceListenerCollection();
			this.listeners.Add(new DefaultTraceListener
			{
				IndentSize = settings.IndentSize
			});
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00032344 File Offset: 0x00030544
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x0003234C File Offset: 0x0003054C
		public TraceListenerCollection Listeners
		{
			get
			{
				return this.listeners;
			}
		}

		// Token: 0x04000734 RID: 1844
		private string name;

		// Token: 0x04000735 RID: 1845
		private SourceLevels levels;

		// Token: 0x04000736 RID: 1846
		private TraceListenerCollection listeners;
	}
}
