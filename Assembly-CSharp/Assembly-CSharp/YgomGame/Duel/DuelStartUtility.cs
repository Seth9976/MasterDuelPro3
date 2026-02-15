using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D9A RID: 3482
	public class DuelStartUtility
	{
		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x0600665B RID: 26203 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelStartUtility Instance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x0600665C RID: 26204 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Active
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600665D RID: 26205 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isDone
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x0600665E RID: 26206 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isSettingFileReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x0600665F RID: 26207 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isGoManagerReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06006660 RID: 26208 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool isTextResReady
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06006661 RID: 26209 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(DuelEntryMode mode, bool loadExData = true)
		{
		}

		// Token: 0x06006662 RID: 26210 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate(bool formDuelClient = false)
		{
		}

		// Token: 0x06006663 RID: 26211 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadDuelResource()
		{
		}

		// Token: 0x06006664 RID: 26212 RVA: 0x0000216D File Offset: 0x0000036D
		private void ReleaseDuelResource()
		{
		}

		// Token: 0x06006665 RID: 26213 RVA: 0x0000216D File Offset: 0x0000036D
		public void MoveGOManager(Transform parent)
		{
		}

		// Token: 0x06006666 RID: 26214 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnLoadEnd()
		{
		}

		// Token: 0x06006667 RID: 26215 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnFirstMoveDecide()
		{
		}

		// Token: 0x06006668 RID: 26216 RVA: 0x0000216D File Offset: 0x0000036D
		public void PreloadDeckCardPicture()
		{
		}

		// Token: 0x06006669 RID: 26217 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetRunEffectWorkerForGoManager(RunEffectWorker runEffectWorker)
		{
		}

		// Token: 0x0600666A RID: 26218 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadTextFile()
		{
		}

		// Token: 0x0600666B RID: 26219 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnloadTextFile()
		{
		}

		// Token: 0x0600666C RID: 26220 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadSettingFile()
		{
		}

		// Token: 0x0600666D RID: 26221 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnloadSettingFile()
		{
		}

		// Token: 0x0600666E RID: 26222 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadMateModel()
		{
		}

		// Token: 0x0600666F RID: 26223 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnloadMateModel()
		{
		}

		// Token: 0x06006670 RID: 26224 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator LoadTimelineFile()
		{
			return null;
		}

		// Token: 0x06006671 RID: 26225 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnloadTimelineFile()
		{
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator LoadDuelData()
		{
			return null;
		}

		// Token: 0x06006673 RID: 26227 RVA: 0x0000216D File Offset: 0x0000036D
		private void UnloadDuelData()
		{
		}

		// Token: 0x06006674 RID: 26228 RVA: 0x000F5CA6 File Offset: 0x000F3EA6
		private int[] objectListToIntArray(List<object> src, bool rejectZero, out List<int> rejectIdxs)
		{
			rejectIdxs = null;
			return null;
		}

		// Token: 0x06006675 RID: 26229 RVA: 0x000F5CAC File Offset: 0x000F3EAC
		private int[] dicDeckToIntArray(Dictionary<string, object> dic, string key1, string key2, bool rejectZero, out List<int> rejectIdxs)
		{
			rejectIdxs = null;
			return null;
		}

		// Token: 0x0400A068 RID: 41064
		private static DuelStartUtility m_Instance;

		// Token: 0x0400A069 RID: 41065
		public int loadDefinitionCounter;

		// Token: 0x0400A06A RID: 41066
		private bool m_Active;

		// Token: 0x0400A06B RID: 41067
		private bool m_DuelClientStart;

		// Token: 0x0400A06C RID: 41068
		private bool m_LoadExData;

		// Token: 0x0400A06D RID: 41069
		private DuelEntryController duelEntryController;

		// Token: 0x0400A06E RID: 41070
		private DuelGameObjectManager goManager;

		// Token: 0x0400A06F RID: 41071
		private bool loadTextDuelLive;

		// Token: 0x0400A070 RID: 41072
		private List<string> PreLoadTimeline_Group1;

		// Token: 0x0400A071 RID: 41073
		private List<string> PreLoadTimeline_Group2;

		// Token: 0x0400A072 RID: 41074
		private List<string> PreLoadDuelData;
	}
}
