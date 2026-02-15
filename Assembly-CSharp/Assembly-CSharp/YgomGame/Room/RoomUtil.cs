using System;
using System.Collections;
using UnityEngine.Events;
using YgomSystem.Network;

namespace YgomGame.Room
{
	// Token: 0x020009FC RID: 2556
	public class RoomUtil
	{
		// Token: 0x06004A3E RID: 19006 RVA: 0x0000216A File Offset: 0x0000036A
		public static string GetSettingTextLP(RoomUtil.LPType type)
		{
			return null;
		}

		// Token: 0x06004A3F RID: 19007 RVA: 0x0000216A File Offset: 0x0000036A
		public static string MakeDuelDurationString(string name, int duration)
		{
			return null;
		}

		// Token: 0x06004A40 RID: 19008 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool HandleResultCode(Handle handle, UnityAction onSuccess = null, UnityAction<RoomCode> onFailed = null)
		{
			return false;
		}

		// Token: 0x06004A41 RID: 19009 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallGetDeckList(int deckId, Action<bool> onEnd)
		{
		}

		// Token: 0x06004A42 RID: 19010 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallReplayAPIForRoom(long did, long pcode, Action<PvPCode> onResult = null)
		{
		}

		// Token: 0x06004A43 RID: 19011 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CallSaveReplayAPI(long did, int eid, Action<PvPCode> onResult = null)
		{
		}

		// Token: 0x06004A44 RID: 19012 RVA: 0x0000216D File Offset: 0x0000036D
		public static void CloseAllDialogs()
		{
		}

		// Token: 0x06004A45 RID: 19013 RVA: 0x0000216A File Offset: 0x0000036A
		private static IEnumerator CloseAllDialogsRoutine()
		{
			return null;
		}

		// Token: 0x020009FD RID: 2557
		public enum LPType
		{
			// Token: 0x04008853 RID: 34899
			LP_8000 = 1,
			// Token: 0x04008854 RID: 34900
			LP_4000
		}
	}
}
