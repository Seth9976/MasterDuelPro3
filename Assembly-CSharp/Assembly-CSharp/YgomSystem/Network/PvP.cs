using System;
using System.Collections;

namespace YgomSystem.Network
{
	// Token: 0x0200071F RID: 1823
	public class PvP
	{
		// Token: 0x0600391C RID: 14620 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Connect(string url, string ticket, int port = 80)
		{
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Send(PvP.Command cmd, byte[] bin, uint serial)
		{
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Send(PvP.Event ev)
		{
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x0000216A File Offset: 0x0000036A
		public static PvP.Event Recv()
		{
			return null;
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Close()
		{
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsActive()
		{
			return false;
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsQue()
		{
			return false;
		}

		// Token: 0x06003923 RID: 14627 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsPoll()
		{
			return false;
		}

		// Token: 0x06003924 RID: 14628 RVA: 0x0000216A File Offset: 0x0000036A
		public static PvP.Event Dequeue()
		{
			return null;
		}

		// Token: 0x06003925 RID: 14629 RVA: 0x0000216A File Offset: 0x0000036A
		public static IEnumerator Exec(PvP.Event val)
		{
			return null;
		}

		// Token: 0x06003926 RID: 14630 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddCompleteHandler(PvP.EventHandler handler)
		{
		}

		// Token: 0x06003927 RID: 14631 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddErrorHandler(PvP.EventHandler handler)
		{
		}

		// Token: 0x06003928 RID: 14632 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddFatalHandler(PvP.EventHandler handler)
		{
		}

		// Token: 0x06003929 RID: 14633 RVA: 0x0000216D File Offset: 0x0000036D
		public static void AddRecvHandler(PvP.EventHandler handler)
		{
		}

		// Token: 0x0600392A RID: 14634 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RemoveRecvHandler(PvP.EventHandler handler)
		{
		}

		// Token: 0x0600392B RID: 14635 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ClearHandler()
		{
		}

		// Token: 0x0600392C RID: 14636 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetPollingData(byte[] data)
		{
		}

		// Token: 0x0600392D RID: 14637 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ApplicationQuitAbort()
		{
		}

		// Token: 0x0600392E RID: 14638 RVA: 0x000029CC File Offset: 0x00000BCC
		public static int GetJobCount()
		{
			return 0;
		}

		// Token: 0x040032D3 RID: 13011
		private static PvP.Implement s_refPvP;

		// Token: 0x02000720 RID: 1824
		public enum Command
		{
			// Token: 0x040032D5 RID: 13013
			ENTRY,
			// Token: 0x040032D6 RID: 13014
			INIT,
			// Token: 0x040032D7 RID: 13015
			WAIT,
			// Token: 0x040032D8 RID: 13016
			READY,
			// Token: 0x040032D9 RID: 13017
			COMMAND,
			// Token: 0x040032DA RID: 13018
			EFFECT,
			// Token: 0x040032DB RID: 13019
			CANCEL,
			// Token: 0x040032DC RID: 13020
			RESULT,
			// Token: 0x040032DD RID: 13021
			DBGCMD,
			// Token: 0x040032DE RID: 13022
			CHEATCARD,
			// Token: 0x040032DF RID: 13023
			CHAT,
			// Token: 0x040032E0 RID: 13024
			LIST,
			// Token: 0x040032E1 RID: 13025
			PHASE,
			// Token: 0x040032E2 RID: 13026
			SKILL,
			// Token: 0x040032E3 RID: 13027
			LEAVE,
			// Token: 0x040032E4 RID: 13028
			EXIT,
			// Token: 0x040032E5 RID: 13029
			RECOVERY,
			// Token: 0x040032E6 RID: 13030
			WATCH,
			// Token: 0x040032E7 RID: 13031
			SURRENDER,
			// Token: 0x040032E8 RID: 13032
			LATENCY,
			// Token: 0x040032E9 RID: 13033
			SEND,
			// Token: 0x040032EA RID: 13034
			RECV,
			// Token: 0x040032EB RID: 13035
			TIME,
			// Token: 0x040032EC RID: 13036
			TURN,
			// Token: 0x040032ED RID: 13037
			DATA = 50,
			// Token: 0x040032EE RID: 13038
			REPLAY = 60,
			// Token: 0x040032EF RID: 13039
			TIMEUP = 97,
			// Token: 0x040032F0 RID: 13040
			FINISH,
			// Token: 0x040032F1 RID: 13041
			POLL,
			// Token: 0x040032F2 RID: 13042
			ERROR,
			// Token: 0x040032F3 RID: 13043
			FATAL = 900,
			// Token: 0x040032F4 RID: 13044
			CONNECT = 1000,
			// Token: 0x040032F5 RID: 13045
			RECONNECT,
			// Token: 0x040032F6 RID: 13046
			CLOSE = 1003,
			// Token: 0x040032F7 RID: 13047
			PING,
			// Token: 0x040032F8 RID: 13048
			PONG,
			// Token: 0x040032F9 RID: 13049
			MATCH,
			// Token: 0x040032FA RID: 13050
			DROP,
			// Token: 0x040032FB RID: 13051
			MATCH_UPDATE = 1010,
			// Token: 0x040032FC RID: 13052
			MATCH_LIST,
			// Token: 0x040032FD RID: 13053
			INFO
		}

		// Token: 0x02000721 RID: 1825
		public class KeyDef
		{
			// Token: 0x040032FE RID: 13054
			public const string CMD = "c";

			// Token: 0x040032FF RID: 13055
			public const string BODY = "b";

			// Token: 0x04003300 RID: 13056
			public const string TICKET = "t";

			// Token: 0x04003301 RID: 13057
			public const string TOKEN = "token";

			// Token: 0x04003302 RID: 13058
			public const string SERIAL = "s";

			// Token: 0x04003303 RID: 13059
			public const string USERAGENT = "ua";

			// Token: 0x04003304 RID: 13060
			public const string DATA = "d";

			// Token: 0x04003305 RID: 13061
			public const string ID = "id";

			// Token: 0x04003306 RID: 13062
			public const string INFO = "in";

			// Token: 0x04003307 RID: 13063
			public const string FROM = "f";

			// Token: 0x04003308 RID: 13064
			public const string HOST = "h";

			// Token: 0x04003309 RID: 13065
			public const string TIME = "tm";

			// Token: 0x0400330A RID: 13066
			public const string COND = "cn";

			// Token: 0x0400330B RID: 13067
			public const string MODE = "m";

			// Token: 0x0400330C RID: 13068
			public const string PROF = "p";

			// Token: 0x0400330D RID: 13069
			public const string ERROR = "e";
		}

		// Token: 0x02000722 RID: 1826
		public class Event
		{
			// Token: 0x17000419 RID: 1049
			// (get) Token: 0x06003931 RID: 14641 RVA: 0x000029CC File Offset: 0x00000BCC
			public PvP.Command Cmd
			{
				get
				{
					return PvP.Command.ENTRY;
				}
			}

			// Token: 0x1700041A RID: 1050
			// (get) Token: 0x06003932 RID: 14642 RVA: 0x0000216A File Offset: 0x0000036A
			public byte[] Body
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700041B RID: 1051
			// (get) Token: 0x06003933 RID: 14643 RVA: 0x000029CC File Offset: 0x00000BCC
			public uint Serial
			{
				get
				{
					return 0U;
				}
			}

			// Token: 0x06003934 RID: 14644 RVA: 0x00002739 File Offset: 0x00000939
			public Event(PvP.Command cmd, byte[] body, uint serial = 0U)
			{
			}

			// Token: 0x0400330E RID: 13070
			private PvP.Command memCmd;

			// Token: 0x0400330F RID: 13071
			private byte[] memBody;

			// Token: 0x04003310 RID: 13072
			private uint memSerial;
		}

		// Token: 0x02000723 RID: 1827
		public abstract class Implement
		{
			// Token: 0x06003935 RID: 14645
			public abstract void Connect(string url, string ticket, int port);

			// Token: 0x06003936 RID: 14646 RVA: 0x000029CC File Offset: 0x00000BCC
			public virtual bool IsQue()
			{
				return false;
			}

			// Token: 0x06003937 RID: 14647 RVA: 0x000029CC File Offset: 0x00000BCC
			public virtual bool IsPoll()
			{
				return false;
			}

			// Token: 0x06003938 RID: 14648
			public abstract PvP.Event Dequeue();

			// Token: 0x06003939 RID: 14649
			public abstract IEnumerator Exec(PvP.Event val);

			// Token: 0x0600393A RID: 14650 RVA: 0x0000216D File Offset: 0x0000036D
			public virtual void Close()
			{
			}

			// Token: 0x0600393B RID: 14651
			public abstract int GetConnectionID();

			// Token: 0x0600393C RID: 14652
			public abstract int[] GetMembers();

			// Token: 0x0600393D RID: 14653
			public abstract void Send(PvP.Command cmd, byte[] bin, uint serial);

			// Token: 0x0600393E RID: 14654
			public abstract void Send(PvP.Event ev);

			// Token: 0x0600393F RID: 14655
			public abstract PvP.Event Recv();

			// Token: 0x06003940 RID: 14656
			public abstract void AddCompleteHandler(PvP.EventHandler handler);

			// Token: 0x06003941 RID: 14657
			public abstract void AddErrorHandler(PvP.EventHandler handler);

			// Token: 0x06003942 RID: 14658
			public abstract void AddFatalHandler(PvP.EventHandler handler);

			// Token: 0x06003943 RID: 14659
			public abstract void AddRecvHandler(PvP.EventHandler handler);

			// Token: 0x06003944 RID: 14660
			public abstract void RemoveRecvHandler(PvP.EventHandler handler);

			// Token: 0x06003945 RID: 14661
			public abstract void ClearHandler();

			// Token: 0x06003946 RID: 14662
			public abstract void SetPollingData(byte[] data);

			// Token: 0x06003947 RID: 14663
			public abstract void ApplicationQuitAbort();

			// Token: 0x06003948 RID: 14664
			public abstract int GetJobCount();
		}

		// Token: 0x02000724 RID: 1828
		// (Invoke) Token: 0x0600394B RID: 14667
		public delegate void EventHandler(PvP.Event val, int code = 0);
	}
}
