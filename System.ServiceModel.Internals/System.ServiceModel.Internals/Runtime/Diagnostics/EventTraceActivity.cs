using System;
using System.Diagnostics;

namespace System.Runtime.Diagnostics
{
	// Token: 0x02000039 RID: 57
	internal class EventTraceActivity
	{
		// Token: 0x06000148 RID: 328 RVA: 0x0000690B File Offset: 0x00004B0B
		public EventTraceActivity(Guid guid, bool setOnThread = false)
		{
			this.ActivityId = guid;
			if (setOnThread)
			{
				this.SetActivityIdOnThread();
			}
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00006924 File Offset: 0x00004B24
		public static EventTraceActivity GetFromThreadOrCreate(bool clearIdOnThread = false)
		{
			Guid guid = Trace.CorrelationManager.ActivityId;
			if (guid == Guid.Empty)
			{
				guid = Guid.NewGuid();
			}
			else if (clearIdOnThread)
			{
				Trace.CorrelationManager.ActivityId = Guid.Empty;
			}
			return new EventTraceActivity(guid, false);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000696A File Offset: 0x00004B6A
		private void SetActivityIdOnThread()
		{
			Trace.CorrelationManager.ActivityId = this.ActivityId;
		}

		// Token: 0x04000092 RID: 146
		public Guid ActivityId;
	}
}
