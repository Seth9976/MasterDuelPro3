using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;

namespace YgomGame.Duel
{
	// Token: 0x02000D2C RID: 3372
	public class DuelChainManager : MonoBehaviour
	{
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x060061D3 RID: 25043 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isChainSet
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x060061D4 RID: 25044 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isChainRevolve
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x060061D5 RID: 25045 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsTimelinePlaying
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x060061D6 RID: 25046 RVA: 0x0000216A File Offset: 0x0000036A
		private DuelChainSpot m_LastChainSpot
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060061D7 RID: 25047 RVA: 0x0000216A File Offset: 0x0000036A
		public static DuelChainManager Create(Transform parent, DuelClient host)
		{
			return null;
		}

		// Token: 0x060061D8 RID: 25048 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActiveEffect(int uniqueid, int player, int position, int chainnum, Vector3 dstpos, UnityAction onAdded = null, bool mutesound = false)
		{
		}

		// Token: 0x060061D9 RID: 25049 RVA: 0x0000216D File Offset: 0x0000036D
		public void ActiveEffectMinimum(int uniqueid, int player, int position, int chainnum, Vector3 dstpos)
		{
		}

		// Token: 0x060061DA RID: 25050 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnAudienceReplayFinished()
		{
		}

		// Token: 0x060061DB RID: 25051 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResolveEffect(int chainnum, Action onRemoved = null)
		{
		}

		// Token: 0x060061DC RID: 25052 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveChainSpotMinimum()
		{
		}

		// Token: 0x060061DD RID: 25053 RVA: 0x0000216D File Offset: 0x0000036D
		public void ResolveEffectMinimum()
		{
		}

		// Token: 0x060061DE RID: 25054 RVA: 0x0000216D File Offset: 0x0000036D
		public void SkipTimeline()
		{
		}

		// Token: 0x060061DF RID: 25055 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CheckChainIdByUniqueId(int uniqueid)
		{
			return 0;
		}

		// Token: 0x060061E0 RID: 25056 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetCardidByChainId(int chainid)
		{
			return 0;
		}

		// Token: 0x060061E1 RID: 25057 RVA: 0x000029CC File Offset: 0x00000BCC
		public int CurrentChainStackCount()
		{
			return 0;
		}

		// Token: 0x060061E2 RID: 25058 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveChainSpot()
		{
		}

		// Token: 0x060061E3 RID: 25059 RVA: 0x0000216D File Offset: 0x0000036D
		private void Initialize()
		{
		}

		// Token: 0x060061E4 RID: 25060 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddChainSpot(int uniqueid, int player, int position, int chainnum, Vector3 worldpos, bool mutesound = false)
		{
		}

		// Token: 0x060061E5 RID: 25061 RVA: 0x0000216A File Offset: 0x0000036A
		private DuelChainSpot GetChainSpot()
		{
			return null;
		}

		// Token: 0x060061E6 RID: 25062 RVA: 0x0000216D File Offset: 0x0000036D
		private void RemoveSpot()
		{
		}

		// Token: 0x060061E7 RID: 25063 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetChainSpotCircle(List<DuelChainSpot> spotlist, int count, Vector3 centerpos)
		{
		}

		// Token: 0x060061E8 RID: 25064 RVA: 0x0000216D File Offset: 0x0000036D
		private void ResolveChain(Action onRemoved, bool isFirst)
		{
		}

		// Token: 0x060061E9 RID: 25065 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetChain()
		{
		}

		// Token: 0x060061EA RID: 25066 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChainTimelineEnd()
		{
		}

		// Token: 0x060061EB RID: 25067 RVA: 0x0000216D File Offset: 0x0000036D
		private void DuelChainNumSE()
		{
		}

		// Token: 0x04009CC8 RID: 40136
		public UnityAction onAdded;

		// Token: 0x04009CC9 RID: 40137
		private DuelChainManager.ChainStepMachine m_StateMachine;

		// Token: 0x04009CCA RID: 40138
		private List<DuelChainManager.ChainSpotData> m_ChainSpotDataList;

		// Token: 0x04009CCB RID: 40139
		private List<Dictionary<int, List<DuelChainSpot>>> m_PosChainSpotTable;

		// Token: 0x04009CCC RID: 40140
		private PlayableDirector m_CurrentTimeline;

		// Token: 0x04009CCD RID: 40141
		private Queue<DuelChainManager.ChainSpotData> m_ChainSpotDataQueue;

		// Token: 0x04009CCE RID: 40142
		private int m_MinimumChainEndCount;

		// Token: 0x04009CCF RID: 40143
		private int m_MinimumChainRunCount;

		// Token: 0x04009CD0 RID: 40144
		private DuelClient m_Host;

		// Token: 0x02000D2D RID: 3373
		public struct ChainSpotData
		{
			// Token: 0x04009CD1 RID: 40145
			public int uniqueid;

			// Token: 0x04009CD2 RID: 40146
			public int cardid;

			// Token: 0x04009CD3 RID: 40147
			public int styleid;

			// Token: 0x04009CD4 RID: 40148
			public int player;

			// Token: 0x04009CD5 RID: 40149
			public int position;

			// Token: 0x04009CD6 RID: 40150
			public int chainnum;

			// Token: 0x04009CD7 RID: 40151
			public Vector3 worldpos;
		}

		// Token: 0x02000D2E RID: 3374
		private enum ChainStep
		{
			// Token: 0x04009CD9 RID: 40153
			IDLE,
			// Token: 0x04009CDA RID: 40154
			ACTIVE,
			// Token: 0x04009CDB RID: 40155
			CHAINSET,
			// Token: 0x04009CDC RID: 40156
			CHAINRESOLVE1,
			// Token: 0x04009CDD RID: 40157
			CHAINRESOLVE2,
			// Token: 0x04009CDE RID: 40158
			EFFECTRESOLVE
		}

		// Token: 0x02000D2F RID: 3375
		private class ChainStepMachine
		{
			// Token: 0x17000B12 RID: 2834
			// (get) Token: 0x060061ED RID: 25069 RVA: 0x000029CC File Offset: 0x00000BCC
			// (set) Token: 0x060061EE RID: 25070 RVA: 0x0000216D File Offset: 0x0000036D
			public DuelChainManager.ChainStep step
			{
				[CompilerGenerated]
				get
				{
					return DuelChainManager.ChainStep.IDLE;
				}
				[CompilerGenerated]
				private set
				{
				}
			}

			// Token: 0x060061EF RID: 25071 RVA: 0x0000216D File Offset: 0x0000036D
			public void Increase()
			{
			}

			// Token: 0x060061F0 RID: 25072 RVA: 0x0000216D File Offset: 0x0000036D
			public void Decrease()
			{
			}

			// Token: 0x060061F1 RID: 25073 RVA: 0x0000216D File Offset: 0x0000036D
			public void Reset()
			{
			}
		}
	}
}
