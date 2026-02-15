using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000E1A RID: 3610
	public class EffectTaskRunSpecialefx : EffectTask
	{
		// Token: 0x0600684C RID: 26700 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker, int param1, int param2, int param3)
		{
		}

		// Token: 0x0600684D RID: 26701 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x0600684E RID: 26702 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskRunSpecialefx(RunEffectWorker worker, int param1, int param2, int param3)
			: base(null)
		{
		}

		// Token: 0x0600684F RID: 26703 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006850 RID: 26704 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool PlayEffectStep()
		{
			return false;
		}

		// Token: 0x06006851 RID: 26705 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayInfiniteImtermanence()
		{
		}

		// Token: 0x06006852 RID: 26706 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayEffectVeiler()
		{
		}

		// Token: 0x06006853 RID: 26707 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool WaitEffectStep()
		{
			return false;
		}

		// Token: 0x06006854 RID: 26708 RVA: 0x0000216D File Offset: 0x0000036D
		private void FinishedStep()
		{
		}

		// Token: 0x0400A352 RID: 41810
		private int cardID;

		// Token: 0x0400A353 RID: 41811
		private int param2;

		// Token: 0x0400A354 RID: 41812
		private int param3;

		// Token: 0x0400A355 RID: 41813
		private bool finished;

		// Token: 0x0400A356 RID: 41814
		private CardRunEffectSetting.CardRunEffectInfo cardRunEffectInfo;

		// Token: 0x0400A357 RID: 41815
		private EffectTaskRunSpecialefx.EffectType effectType;

		// Token: 0x0400A358 RID: 41816
		private EffectTaskRunSpecialefx.Step step;

		// Token: 0x0400A359 RID: 41817
		private static Dictionary<int, EffectTaskRunSpecialefx.EffectType> spfxList;

		// Token: 0x02000E1B RID: 3611
		private enum EffectType
		{
			// Token: 0x0400A35B RID: 41819
			None,
			// Token: 0x0400A35C RID: 41820
			InfiniteImpermanence,
			// Token: 0x0400A35D RID: 41821
			EffectVeiler
		}

		// Token: 0x02000E1C RID: 3612
		private enum Step
		{
			// Token: 0x0400A35F RID: 41823
			WaitLoad,
			// Token: 0x0400A360 RID: 41824
			PlayEffect,
			// Token: 0x0400A361 RID: 41825
			WaitEffect,
			// Token: 0x0400A362 RID: 41826
			Finished
		}
	}
}
