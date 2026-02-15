using System;
using System.Collections;
using System.Collections.Generic;

namespace YgomSystem.Network
{
	// Token: 0x02000712 RID: 1810
	public class HTTP : PvP.Implement
	{
		// Token: 0x060038B5 RID: 14517 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Connect(string url, string ticket, int port)
		{
		}

		// Token: 0x060038B6 RID: 14518 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetConnectionID()
		{
			return 0;
		}

		// Token: 0x060038B7 RID: 14519 RVA: 0x0000216A File Offset: 0x0000036A
		public override int[] GetMembers()
		{
			return null;
		}

		// Token: 0x060038B8 RID: 14520 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Send(PvP.Command cmd, byte[] bin, uint serial)
		{
		}

		// Token: 0x060038B9 RID: 14521 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Send(PvP.Event ev)
		{
		}

		// Token: 0x060038BA RID: 14522 RVA: 0x0000216A File Offset: 0x0000036A
		public override PvP.Event Recv()
		{
			return null;
		}

		// Token: 0x060038BB RID: 14523 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Close()
		{
		}

		// Token: 0x060038BC RID: 14524 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsQue()
		{
			return false;
		}

		// Token: 0x060038BD RID: 14525 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsPoll()
		{
			return false;
		}

		// Token: 0x060038BE RID: 14526 RVA: 0x0000216A File Offset: 0x0000036A
		public override PvP.Event Dequeue()
		{
			return null;
		}

		// Token: 0x060038BF RID: 14527 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerator Exec(PvP.Event val)
		{
			return null;
		}

		// Token: 0x060038C0 RID: 14528 RVA: 0x0000216D File Offset: 0x0000036D
		public override void AddCompleteHandler(PvP.EventHandler handler)
		{
		}

		// Token: 0x060038C1 RID: 14529 RVA: 0x0000216D File Offset: 0x0000036D
		public override void AddErrorHandler(PvP.EventHandler handler)
		{
		}

		// Token: 0x060038C2 RID: 14530 RVA: 0x0000216D File Offset: 0x0000036D
		public override void AddFatalHandler(PvP.EventHandler handler)
		{
		}

		// Token: 0x060038C3 RID: 14531 RVA: 0x0000216D File Offset: 0x0000036D
		public override void AddRecvHandler(PvP.EventHandler handler)
		{
		}

		// Token: 0x060038C4 RID: 14532 RVA: 0x0000216D File Offset: 0x0000036D
		public override void RemoveRecvHandler(PvP.EventHandler handler)
		{
		}

		// Token: 0x060038C5 RID: 14533 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ClearHandler()
		{
		}

		// Token: 0x060038C6 RID: 14534 RVA: 0x0000216D File Offset: 0x0000036D
		public override void SetPollingData(byte[] data)
		{
		}

		// Token: 0x060038C7 RID: 14535 RVA: 0x0000216D File Offset: 0x0000036D
		public override void ApplicationQuitAbort()
		{
		}

		// Token: 0x060038C8 RID: 14536 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int GetJobCount()
		{
			return 0;
		}

		// Token: 0x060038C9 RID: 14537 RVA: 0x0000216D File Offset: 0x0000036D
		private void Complete(PvP.Event ev)
		{
		}

		// Token: 0x060038CA RID: 14538 RVA: 0x0000216D File Offset: 0x0000036D
		private void Error(PvP.Event ev, PvPCode code = PvPCode.ERROR)
		{
		}

		// Token: 0x060038CB RID: 14539 RVA: 0x0000216D File Offset: 0x0000036D
		private void Fatal(PvP.Event ev, PvPCode code = PvPCode.FATAL)
		{
		}

		// Token: 0x060038CC RID: 14540 RVA: 0x0000216D File Offset: 0x0000036D
		private void Received(PvP.Event ev, PvPCode code = PvPCode.NONE)
		{
		}

		// Token: 0x04003267 RID: 12903
		private Queue<PvP.Event> send_queue;

		// Token: 0x04003268 RID: 12904
		private Queue<PvP.Event> app_queue;

		// Token: 0x04003269 RID: 12905
		private int connection_id;

		// Token: 0x0400326A RID: 12906
		private int[] members;

		// Token: 0x0400326B RID: 12907
		private string entry_url;

		// Token: 0x0400326C RID: 12908
		private string entry_ticket;

		// Token: 0x0400326D RID: 12909
		private bool is_poll;

		// Token: 0x0400326E RID: 12910
		private PvP.EventHandler completeHandler;

		// Token: 0x0400326F RID: 12911
		private PvP.EventHandler errorHandler;

		// Token: 0x04003270 RID: 12912
		private PvP.EventHandler fatalHandler;

		// Token: 0x04003271 RID: 12913
		private PvP.EventHandler recvHandler;

		// Token: 0x04003272 RID: 12914
		private uint received_serial;

		// Token: 0x04003273 RID: 12915
		private List<PvP.Event> evlist;

		// Token: 0x04003274 RID: 12916
		private bool closed;

		// Token: 0x04003275 RID: 12917
		private byte[] pollData;

		// Token: 0x04003276 RID: 12918
		private bool appQuitAbort;

		// Token: 0x04003277 RID: 12919
		private int jobCount;

		// Token: 0x04003278 RID: 12920
		private static readonly byte[] dummy_bytes;
	}
}
