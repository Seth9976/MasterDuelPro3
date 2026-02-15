using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000E0F RID: 3599
	public class EffectTaskRunFusion : EffectTask
	{
		// Token: 0x06006819 RID: 26649 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600681A RID: 26650 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x0600681B RID: 26651 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskRunFusion(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x0600681C RID: 26652 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartLoad()
		{
		}

		// Token: 0x0600681D RID: 26653 RVA: 0x0000216D File Offset: 0x0000036D
		private void Loading()
		{
		}

		// Token: 0x0600681E RID: 26654 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool WaitCardEffect()
		{
			return false;
		}

		// Token: 0x0600681F RID: 26655 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartSummonEffect()
		{
		}

		// Token: 0x06006820 RID: 26656 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadLinkSummon(int dstCardId, int matNum, int[] materialUniqueID)
		{
		}

		// Token: 0x06006821 RID: 26657 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadWaitLinkSummon()
		{
		}

		// Token: 0x06006822 RID: 26658 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartLinkSummon(int dstCardId, int matNum, int[] materialUniqueID)
		{
		}

		// Token: 0x06006823 RID: 26659 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadFusionSummon(int dstCardId, int matNum, int[] materialUniqueID)
		{
		}

		// Token: 0x06006824 RID: 26660 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadWaitFusionSummon()
		{
		}

		// Token: 0x06006825 RID: 26661 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartFusionSummon(int dstCardId, int matNum, int[] materialUniqueID)
		{
		}

		// Token: 0x06006826 RID: 26662 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadSPFusionSummon(int dstCardId, int matNum, int[] materialUniqueID)
		{
		}

		// Token: 0x06006827 RID: 26663 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadWaitSPFusionSummon()
		{
		}

		// Token: 0x06006828 RID: 26664 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartSPFusionSummon(int dstCardId, int matNum, int[] materialUniqueID)
		{
		}

		// Token: 0x06006829 RID: 26665 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadRitualSummon(int dstCardId, int matNum, int[] materialUniqueID)
		{
		}

		// Token: 0x0600682A RID: 26666 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadWaitRitualSummon()
		{
		}

		// Token: 0x0600682B RID: 26667 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartRitualSummon(int dstCardId, int matNum, int[] materialUniqueID)
		{
		}

		// Token: 0x0600682C RID: 26668 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadSyncSummon(int dstCardId, int matNum, int[] materialUniqueID)
		{
		}

		// Token: 0x0600682D RID: 26669 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadWaitSyncSummon()
		{
		}

		// Token: 0x0600682E RID: 26670 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartSyncSummon(int dstCardId, int matNum, int[] materialUniqueID)
		{
		}

		// Token: 0x0600682F RID: 26671 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadXyzSummon(int dstCardId, int matNum, int[] materialUniqueID)
		{
		}

		// Token: 0x06006830 RID: 26672 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadWaitXyzSummon()
		{
		}

		// Token: 0x06006831 RID: 26673 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartXyzSummon(int dstCardId, int matNum, int[] matList)
		{
		}

		// Token: 0x06006832 RID: 26674 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadPendSummon(int dstCardId, int matNum, int[] matList)
		{
		}

		// Token: 0x06006833 RID: 26675 RVA: 0x0000216D File Offset: 0x0000036D
		private void LoadWaitPendSummon()
		{
		}

		// Token: 0x06006834 RID: 26676 RVA: 0x0000216D File Offset: 0x0000036D
		private void StartPendSummon(int dstCardId, int matNum, int[] matList)
		{
		}

		// Token: 0x06006835 RID: 26677 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006836 RID: 26678 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishStep()
		{
		}

		// Token: 0x0400A31B RID: 41755
		private bool finished;

		// Token: 0x0400A31C RID: 41756
		private EffectTaskRunFusion.Step step;

		// Token: 0x0400A31D RID: 41757
		private int[] materialUniqueID;

		// Token: 0x0400A31E RID: 41758
		private int[] materialCardID;

		// Token: 0x0400A31F RID: 41759
		private int dstCardId;

		// Token: 0x0400A320 RID: 41760
		private int dstUniqueID;

		// Token: 0x0400A321 RID: 41761
		private int matNum;

		// Token: 0x0400A322 RID: 41762
		private Engine.SpSummonType summonType;

		// Token: 0x0400A323 RID: 41763
		private int player;

		// Token: 0x0400A324 RID: 41764
		private bool isMyself;

		// Token: 0x0400A325 RID: 41765
		private bool loadingSummonCard;

		// Token: 0x0400A326 RID: 41766
		private int leftCardID;

		// Token: 0x0400A327 RID: 41767
		private int leftUniqueID;

		// Token: 0x0400A328 RID: 41768
		private int leftScale;

		// Token: 0x0400A329 RID: 41769
		private int rightCardID;

		// Token: 0x0400A32A RID: 41770
		private int rightUniqueID;

		// Token: 0x0400A32B RID: 41771
		private int rightScale;

		// Token: 0x02000E10 RID: 3600
		private enum Step
		{
			// Token: 0x0400A32D RID: 41773
			Loading,
			// Token: 0x0400A32E RID: 41774
			WaitCardEffect,
			// Token: 0x0400A32F RID: 41775
			Start,
			// Token: 0x0400A330 RID: 41776
			Wait,
			// Token: 0x0400A331 RID: 41777
			Finish
		}
	}
}
