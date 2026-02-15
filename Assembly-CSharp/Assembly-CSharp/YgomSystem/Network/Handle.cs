using System;
using System.Collections.Generic;

namespace YgomSystem.Network
{
	// Token: 0x02000713 RID: 1811
	public class Handle
	{
		// Token: 0x060038CE RID: 14542 RVA: 0x00002739 File Offset: 0x00000939
		public Handle(NetworkMain.RequestStructure request)
		{
		}

		// Token: 0x060038CF RID: 14543 RVA: 0x0000216A File Offset: 0x0000036A
		public Handle AddCompleteEvent(EventHandler e)
		{
			return null;
		}

		// Token: 0x060038D0 RID: 14544 RVA: 0x0000216A File Offset: 0x0000036A
		public Handle AddErrorEvent(EventHandler e)
		{
			return null;
		}

		// Token: 0x060038D1 RID: 14545 RVA: 0x0000216D File Offset: 0x0000036D
		public void ClearAllEvent()
		{
		}

		// Token: 0x060038D2 RID: 14546 RVA: 0x0000216A File Offset: 0x0000036A
		public Handle Chain(Handle hdl)
		{
			return null;
		}

		// Token: 0x060038D3 RID: 14547 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Retry()
		{
			return false;
		}

		// Token: 0x060038D4 RID: 14548 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsCompleted()
		{
			return false;
		}

		// Token: 0x060038D5 RID: 14549 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsError()
		{
			return false;
		}

		// Token: 0x060038D6 RID: 14550 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsFatal()
		{
			return false;
		}

		// Token: 0x060038D7 RID: 14551 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsLongPolling()
		{
			return false;
		}

		// Token: 0x060038D8 RID: 14552 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetCode()
		{
			return 0;
		}

		// Token: 0x060038D9 RID: 14553 RVA: 0x0000216D File Offset: 0x0000036D
		public void Finish()
		{
		}

		// Token: 0x060038DA RID: 14554 RVA: 0x0000216A File Offset: 0x0000036A
		public string GetCommand()
		{
			return null;
		}

		// Token: 0x060038DB RID: 14555 RVA: 0x0000216A File Offset: 0x0000036A
		public Dictionary<string, object> GetParam()
		{
			return null;
		}

		// Token: 0x060038DC RID: 14556 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetId()
		{
			return 0;
		}

		// Token: 0x060038DD RID: 14557 RVA: 0x0000216D File Offset: 0x0000036D
		public void Abort()
		{
		}

		// Token: 0x04003279 RID: 12921
		private NetworkMain.RequestStructure m_Request;
	}
}
