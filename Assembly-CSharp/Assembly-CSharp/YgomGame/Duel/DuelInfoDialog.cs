using System;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D85 RID: 3461
	public class DuelInfoDialog : DuelInfoDialogBase
	{
		// Token: 0x06006553 RID: 25939 RVA: 0x0000216D File Offset: 0x0000036D
		public void ReqOpen(string message, DuelInfoDialogBase.Place place, bool cancelable, Action cancelCallback, Action closeCallback = null, bool decidable = false, Action decisionCallback = null, Action actCallback = null)
		{
		}

		// Token: 0x06006554 RID: 25940 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Create(RunEffectWorker effectWorker, Transform parent, Action<DuelInfoDialog> finishCallback)
		{
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x0000216D File Offset: 0x0000036D
		protected override void CreateUI()
		{
		}

		// Token: 0x04009F9B RID: 40859
		private const string prefabPath = "Prefabs/Duel/DuelInfoDialog";

		// Token: 0x02000D86 RID: 3462
		private class InfoDialogOperationInfo : DuelInfoDialogBase.OperationInfo
		{
			// Token: 0x06006557 RID: 25943 RVA: 0x0000216A File Offset: 0x0000036A
			public static DuelInfoDialogBase.OperationInfo OpenOperation(DuelInfoDialog dialog, string message, DuelInfoDialogBase.Place place, bool cancelable, Action cancelCallback, Action closeCallback = null, bool decidable = false, Action decisionCallback = null, Action actCallback = null)
			{
				return null;
			}
		}
	}
}
