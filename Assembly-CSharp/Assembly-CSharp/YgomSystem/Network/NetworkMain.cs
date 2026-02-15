using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomSystem.Network
{
	// Token: 0x02000717 RID: 1815
	public class NetworkMain : MonoBehaviour
	{
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060038DE RID: 14558 RVA: 0x000029CC File Offset: 0x00000BCC
		public static NetworkReachability Reachability
		{
			get
			{
				return NetworkReachability.NotReachable;
			}
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060038DF RID: 14559 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060038E0 RID: 14560 RVA: 0x0000216D File Offset: 0x0000036D
		public static string Version
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060038E1 RID: 14561 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060038E2 RID: 14562 RVA: 0x0000216D File Offset: 0x0000036D
		public static string Session
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060038E3 RID: 14563 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060038E4 RID: 14564 RVA: 0x0000216D File Offset: 0x0000036D
		public static string Lang
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060038E5 RID: 14565 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060038E6 RID: 14566 RVA: 0x0000216D File Offset: 0x0000036D
		public static string UserAgent
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060038E7 RID: 14567 RVA: 0x000029C5 File Offset: 0x00000BC5
		// (set) Token: 0x060038E8 RID: 14568 RVA: 0x0000216D File Offset: 0x0000036D
		public static float TouchSum
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x060038E9 RID: 14569 RVA: 0x0000216D File Offset: 0x0000036D
		public static void RequestShutdown(Action callback)
		{
		}

		// Token: 0x060038EA RID: 14570 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x060038EB RID: 14571 RVA: 0x0000216D File Offset: 0x0000036D
		private void Start()
		{
		}

		// Token: 0x060038EC RID: 14572 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060038ED RID: 14573 RVA: 0x0000216A File Offset: 0x0000036A
		public static Handle Entry(string cmd, Dictionary<string, object> param, float timeout)
		{
			return null;
		}

		// Token: 0x060038EE RID: 14574 RVA: 0x0000216D File Offset: 0x0000036D
		private void IEnumRequest(NetworkMain.RequestStructure data)
		{
		}

		// Token: 0x060038EF RID: 14575 RVA: 0x0000216D File Offset: 0x0000036D
		private void IPvPEnumRequest(PvP.Event val)
		{
		}

		// Token: 0x060038F0 RID: 14576 RVA: 0x0000216D File Offset: 0x0000036D
		public static void SetDisconnectAction(Action<Action> action)
		{
		}

		// Token: 0x060038F1 RID: 14577 RVA: 0x0000216D File Offset: 0x0000036D
		public static void ResetRequest()
		{
		}

		// Token: 0x0400329A RID: 12954
		private static Queue<NetworkMain.RequestStructure> s_refRequestQue;

		// Token: 0x0400329B RID: 12955
		private static List<NetworkMain.RequestStructure> s_refExcList;

		// Token: 0x0400329C RID: 12956
		private static bool s_suspend;

		// Token: 0x0400329D RID: 12957
		private static string s_Version;

		// Token: 0x0400329E RID: 12958
		private static string s_Session;

		// Token: 0x0400329F RID: 12959
		private static string s_Lang;

		// Token: 0x040032A0 RID: 12960
		private static string s_UserAgent;

		// Token: 0x040032A1 RID: 12961
		private static float s_TouchSum;

		// Token: 0x040032A2 RID: 12962
		private Protocol m_Protocol;

		// Token: 0x040032A3 RID: 12963
		private Protocol m_ProtocolDeckExt;

		// Token: 0x040032A4 RID: 12964
		private static bool s_AbortPorotocol;

		// Token: 0x040032A5 RID: 12965
		private static Action s_ShutdownCallback;

		// Token: 0x040032A6 RID: 12966
		private static Action<Action> s_disconnectAction;

		// Token: 0x02000718 RID: 1816
		public class RequestStructure
		{
			// Token: 0x17000413 RID: 1043
			// (get) Token: 0x060038F3 RID: 14579 RVA: 0x0000216A File Offset: 0x0000036A
			public string Command
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000414 RID: 1044
			// (get) Token: 0x060038F4 RID: 14580 RVA: 0x0000216A File Offset: 0x0000036A
			public Dictionary<string, object> Param
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000415 RID: 1045
			// (get) Token: 0x060038F5 RID: 14581 RVA: 0x000029CC File Offset: 0x00000BCC
			public int Id
			{
				get
				{
					return 0;
				}
			}

			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x060038F6 RID: 14582 RVA: 0x0000216A File Offset: 0x0000036A
			public NetworkMain.RequestStructure Next
			{
				get
				{
					return null;
				}
			}

			// Token: 0x17000417 RID: 1047
			// (get) Token: 0x060038F7 RID: 14583 RVA: 0x000029CC File Offset: 0x00000BCC
			public bool IsInnerChain
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000418 RID: 1048
			// (get) Token: 0x060038F8 RID: 14584 RVA: 0x0000216A File Offset: 0x0000036A
			public byte[] Body
			{
				get
				{
					return null;
				}
			}

			// Token: 0x14000043 RID: 67
			// (add) Token: 0x060038F9 RID: 14585 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x060038FA RID: 14586 RVA: 0x0000216D File Offset: 0x0000036D
			public event EventHandler errorEvent
			{
				[CompilerGenerated]
				add
				{
				}
				[CompilerGenerated]
				remove
				{
				}
			}

			// Token: 0x14000044 RID: 68
			// (add) Token: 0x060038FB RID: 14587 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x060038FC RID: 14588 RVA: 0x0000216D File Offset: 0x0000036D
			public event EventHandler completeEvent
			{
				[CompilerGenerated]
				add
				{
				}
				[CompilerGenerated]
				remove
				{
				}
			}

			// Token: 0x14000045 RID: 69
			// (add) Token: 0x060038FD RID: 14589 RVA: 0x0000216D File Offset: 0x0000036D
			// (remove) Token: 0x060038FE RID: 14590 RVA: 0x0000216D File Offset: 0x0000036D
			public event EventHandler retryEvent
			{
				[CompilerGenerated]
				add
				{
				}
				[CompilerGenerated]
				remove
				{
				}
			}

			// Token: 0x060038FF RID: 14591 RVA: 0x00002739 File Offset: 0x00000939
			public RequestStructure(string command, Dictionary<string, object> param, Queue<NetworkMain.RequestStructure> q)
			{
			}

			// Token: 0x06003900 RID: 14592 RVA: 0x0000216D File Offset: 0x0000036D
			public void Entry()
			{
			}

			// Token: 0x06003901 RID: 14593 RVA: 0x0000216D File Offset: 0x0000036D
			public void Remove()
			{
			}

			// Token: 0x06003902 RID: 14594 RVA: 0x0000216D File Offset: 0x0000036D
			public void Chain(NetworkMain.RequestStructure req)
			{
			}

			// Token: 0x06003903 RID: 14595 RVA: 0x0000216D File Offset: 0x0000036D
			public void Complete()
			{
			}

			// Token: 0x06003904 RID: 14596 RVA: 0x0000216D File Offset: 0x0000036D
			public void Error()
			{
			}

			// Token: 0x06003905 RID: 14597 RVA: 0x0000216D File Offset: 0x0000036D
			public void Retry()
			{
			}

			// Token: 0x06003906 RID: 14598 RVA: 0x0000216D File Offset: 0x0000036D
			public void ClearAllEvent()
			{
			}

			// Token: 0x040032A7 RID: 12967
			private static int s_RequestIdCount;

			// Token: 0x040032A8 RID: 12968
			private static byte[] s_NullArray;

			// Token: 0x040032A9 RID: 12969
			private int memId;

			// Token: 0x040032AA RID: 12970
			private string memCmd;

			// Token: 0x040032AB RID: 12971
			private Dictionary<string, object> memParam;

			// Token: 0x040032AC RID: 12972
			private NetworkMain.RequestStructure memChain;

			// Token: 0x040032AD RID: 12973
			private bool memIsInnerChain;

			// Token: 0x040032AE RID: 12974
			private Queue<NetworkMain.RequestStructure> refRequestQue;

			// Token: 0x040032AF RID: 12975
			public Handle handle;

			// Token: 0x040032B0 RID: 12976
			public Status status;

			// Token: 0x040032B1 RID: 12977
			public int code;

			// Token: 0x040032B2 RID: 12978
			public float timeOut;

			// Token: 0x040032B3 RID: 12979
			public bool deckext;

			// Token: 0x040032B4 RID: 12980
			public bool abort;

			// Token: 0x040032B5 RID: 12981
			public bool finished;

			// Token: 0x040032B6 RID: 12982
			public bool nofatal;
		}
	}
}
