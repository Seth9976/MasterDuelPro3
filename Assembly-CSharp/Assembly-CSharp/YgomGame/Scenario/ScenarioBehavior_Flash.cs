using System;
using UnityEngine;

namespace YgomGame.Scenario
{
	// Token: 0x020009B0 RID: 2480
	public class ScenarioBehavior_Flash : ScenarioBehaviour, IScenarioScreenActorBehaviour, IScenarioBehaviour
	{
		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06004850 RID: 18512 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isOverrideBehaveScreen
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004851 RID: 18513 RVA: 0x000F49AE File Offset: 0x000F2BAE
		public ScenarioBehavior_Flash(object commandData)
			: base(null)
		{
		}

		// Token: 0x06004852 RID: 18514 RVA: 0x000029CC File Offset: 0x00000BCC
		public ScenarioScreenContainer.Targets GetBehaveScreenTargets()
		{
			return ScenarioScreenContainer.Targets.None;
		}

		// Token: 0x06004853 RID: 18515 RVA: 0x000029CC File Offset: 0x00000BCC
		public ScenarioScreenContainer.Operations GetBehaveScreenOperations()
		{
			return ScenarioScreenContainer.Operations.None;
		}

		// Token: 0x06004854 RID: 18516 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressInit()
		{
		}

		// Token: 0x06004855 RID: 18517 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressAction()
		{
		}

		// Token: 0x06004856 RID: 18518 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void ProgressFinish()
		{
		}

		// Token: 0x06004857 RID: 18519 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool IsOverrideBehaviour(ScenarioBehaviour target)
		{
			return false;
		}

		// Token: 0x04008680 RID: 34432
		private readonly string k_TweenFlash;

		// Token: 0x04008681 RID: 34433
		private GameObject m_TargetGo;

		// Token: 0x04008682 RID: 34434
		private int m_TotalCnt;

		// Token: 0x04008683 RID: 34435
		private int m_Cnt;
	}
}
