using System;
using System.Collections.Generic;

namespace YgomGame.Duel
{
	// Token: 0x02000E26 RID: 3622
	public class EffectTaskTurnChange : EffectTask
	{
		// Token: 0x06006871 RID: 26737 RVA: 0x0000216D File Offset: 0x0000036D
		public static void MinimumEffect(RunEffectWorker worker)
		{
		}

		// Token: 0x06006872 RID: 26738 RVA: 0x0000216A File Offset: 0x0000036A
		public static Dictionary<string, object> PreCreate(RunEffectWorker worker, int param1, int param2, int param3)
		{
			return null;
		}

		// Token: 0x06006873 RID: 26739 RVA: 0x0000216A File Offset: 0x0000036A
		public static EffectTask Create(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
		{
			return null;
		}

		// Token: 0x06006874 RID: 26740 RVA: 0x000F5CE2 File Offset: 0x000F3EE2
		public EffectTaskTurnChange(RunEffectWorker worker, int param1, int param2, int param3, Dictionary<string, object> immediateWork)
			: base(null)
		{
		}

		// Token: 0x06006875 RID: 26741 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool Update()
		{
			return false;
		}

		// Token: 0x06006876 RID: 26742 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitCardMoveStep()
		{
		}

		// Token: 0x06006877 RID: 26743 RVA: 0x0000216D File Offset: 0x0000036D
		private void WaitEffectsStep()
		{
		}

		// Token: 0x0400A370 RID: 41840
		private EffectTaskTurnChange.Step step;

		// Token: 0x0400A371 RID: 41841
		private int team;

		// Token: 0x0400A372 RID: 41842
		private int player;

		// Token: 0x0400A373 RID: 41843
		private bool finished;

		// Token: 0x0400A374 RID: 41844
		private bool finishedAnime;

		// Token: 0x0400A375 RID: 41845
		private DuelStatusViewer.DuelStatusInfo statusInfo;

		// Token: 0x02000E27 RID: 3623
		private enum Step
		{
			// Token: 0x0400A377 RID: 41847
			WaitCardMove,
			// Token: 0x0400A378 RID: 41848
			WaitEffects,
			// Token: 0x0400A379 RID: 41849
			WaitFinish
		}
	}
}
