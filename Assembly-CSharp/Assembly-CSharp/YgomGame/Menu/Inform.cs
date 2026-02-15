using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Menu
{
	// Token: 0x02000A94 RID: 2708
	public class Inform : MonoBehaviour
	{
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06004F08 RID: 20232 RVA: 0x0000216A File Offset: 0x0000036A
		public static Inform Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004F09 RID: 20233 RVA: 0x0000216A File Offset: 0x0000036A
		private InformContentBase GetPref(Inform.PrefType prefType)
		{
			return null;
		}

		// Token: 0x06004F0A RID: 20234 RVA: 0x0000216D File Offset: 0x0000036D
		private void Awake()
		{
		}

		// Token: 0x06004F0B RID: 20235 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x06004F0C RID: 20236 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Push(Inform.PrefType prefType, Dictionary<string, object> args)
		{
		}

		// Token: 0x06004F0D RID: 20237 RVA: 0x0000216A File Offset: 0x0000036A
		public static IReadOnlyList<InformContentBase> SearchContents()
		{
			return null;
		}

		// Token: 0x04008CD0 RID: 36048
		private static Inform s_Instance;

		// Token: 0x04008CD1 RID: 36049
		[SerializeField]
		private ToastMessageInform m_ToastPref;

		// Token: 0x04008CD2 RID: 36050
		private List<InformContentBase> m_SearchContentList;

		// Token: 0x02000A95 RID: 2709
		public enum PrefType
		{
			// Token: 0x04008CD4 RID: 36052
			Toast
		}
	}
}
