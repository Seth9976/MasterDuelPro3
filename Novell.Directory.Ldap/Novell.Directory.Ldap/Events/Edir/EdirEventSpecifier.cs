using System;

namespace Novell.Directory.Ldap.Events.Edir
{
	// Token: 0x020000D4 RID: 212
	public class EdirEventSpecifier
	{
		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00016512 File Offset: 0x00014712
		public EdirEventType EventType
		{
			get
			{
				return this.event_type;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001651A File Offset: 0x0001471A
		public EdirEventResultType EventResultType
		{
			get
			{
				return this.event_result_type;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x00016522 File Offset: 0x00014722
		public string EventFilter
		{
			get
			{
				return this.event_filter;
			}
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001652A File Offset: 0x0001472A
		public EdirEventSpecifier(EdirEventType eventType, EdirEventResultType eventResultType)
			: this(eventType, eventResultType, null)
		{
		}

		// Token: 0x06000538 RID: 1336 RVA: 0x00016535 File Offset: 0x00014735
		public EdirEventSpecifier(EdirEventType eventType, EdirEventResultType eventResultType, string filter)
		{
			this.event_type = eventType;
			this.event_result_type = eventResultType;
			this.event_filter = filter;
		}

		// Token: 0x04000468 RID: 1128
		private EdirEventType event_type;

		// Token: 0x04000469 RID: 1129
		private EdirEventResultType event_result_type;

		// Token: 0x0400046A RID: 1130
		private string event_filter;
	}
}
