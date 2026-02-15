using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000E8B RID: 3723
	public class FaceDownCardEffectPool : MonoBehaviour
	{
		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06006C16 RID: 27670 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06006C17 RID: 27671 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isInitialized
		{
			[CompilerGenerated]
			get
			{
				return false;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06006C18 RID: 27672 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06006C19 RID: 27673 RVA: 0x0000216D File Offset: 0x0000036D
		public DuelGameObjectManager goManager
		{
			[CompilerGenerated]
			get
			{
				return null;
			}
			[CompilerGenerated]
			private set
			{
			}
		}

		// Token: 0x06006C1A RID: 27674 RVA: 0x0000216A File Offset: 0x0000036A
		public static FaceDownCardEffectPool Create(DuelGameObjectManager goManager, GameObject root, string name)
		{
			return null;
		}

		// Token: 0x06006C1B RID: 27675 RVA: 0x0000216D File Offset: 0x0000036D
		protected void Initialize()
		{
		}

		// Token: 0x06006C1C RID: 27676 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x06006C1D RID: 27677 RVA: 0x0000216D File Offset: 0x0000036D
		protected void WaitPrefabLoad()
		{
		}

		// Token: 0x06006C1E RID: 27678 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator InstantiateProcess()
		{
			return null;
		}

		// Token: 0x06006C1F RID: 27679 RVA: 0x0000216D File Offset: 0x0000036D
		protected void WaitInstantiateStep()
		{
		}

		// Token: 0x06006C20 RID: 27680 RVA: 0x0000216D File Offset: 0x0000036D
		protected void IdleStep()
		{
		}

		// Token: 0x06006C21 RID: 27681 RVA: 0x0000216D File Offset: 0x0000036D
		protected void TerminateStep()
		{
		}

		// Token: 0x06006C22 RID: 27682 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateEffect(int player, int position)
		{
		}

		// Token: 0x06006C23 RID: 27683 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateAllEffect(bool forcehide = false)
		{
		}

		// Token: 0x06006C24 RID: 27684 RVA: 0x0000216D File Offset: 0x0000036D
		private void SetEffectVisible(int player, int position, bool visible)
		{
		}

		// Token: 0x06006C25 RID: 27685 RVA: 0x0000216D File Offset: 0x0000036D
		public void RemoveFaceDownCardEffect(int player, int position, bool temporary = false)
		{
		}

		// Token: 0x06006C26 RID: 27686 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateFaceDownCardEffect(int player, int position)
		{
		}

		// Token: 0x06006C27 RID: 27687 RVA: 0x0000216D File Offset: 0x0000036D
		public void UpdateFaceDownEffectTable(bool forcehide = false)
		{
		}

		// Token: 0x06006C28 RID: 27688 RVA: 0x0000216D File Offset: 0x0000036D
		public void Terminate()
		{
		}

		// Token: 0x0400A77B RID: 42875
		private const string PREFABPATH = "Duel/Effects/Buff/fxp_bff_disquiet/fxp_bff_disquiet_001";

		// Token: 0x0400A77C RID: 42876
		private List<Dictionary<int, GameObject>> m_EffectList;

		// Token: 0x0400A77D RID: 42877
		private List<Dictionary<int, bool>> m_EffectVisibleList;

		// Token: 0x0400A77E RID: 42878
		private FaceDownCardEffectPool.Step m_Step;

		// Token: 0x0400A77F RID: 42879
		private bool isInstantiated;

		// Token: 0x0400A780 RID: 42880
		private GameObject m_Prefab;

		// Token: 0x02000E8C RID: 3724
		private enum Step
		{
			// Token: 0x0400A782 RID: 42882
			WaitPrefabLoad,
			// Token: 0x0400A783 RID: 42883
			WaitInstantiate,
			// Token: 0x0400A784 RID: 42884
			Idle,
			// Token: 0x0400A785 RID: 42885
			Terminating
		}
	}
}
