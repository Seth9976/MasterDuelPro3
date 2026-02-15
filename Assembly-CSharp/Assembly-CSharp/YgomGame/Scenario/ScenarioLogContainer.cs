using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using YgomSystem.ElementSystem;

namespace YgomGame.Scenario
{
	// Token: 0x020009D7 RID: 2519
	public class ScenarioLogContainer : ScenarioContainerBase
	{
		// Token: 0x06004936 RID: 18742 RVA: 0x000F4916 File Offset: 0x000F2B16
		public ScenarioLogContainer(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004937 RID: 18743 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(int selectorPriority)
		{
		}

		// Token: 0x06004938 RID: 18744 RVA: 0x0000216D File Offset: 0x0000036D
		public void Show(IReadOnlyList<IScenarioLogBehavior> logBehaviors)
		{
		}

		// Token: 0x06004939 RID: 18745 RVA: 0x0000216D File Offset: 0x0000036D
		public void Hide()
		{
		}

		// Token: 0x0600493A RID: 18746 RVA: 0x0000216D File Offset: 0x0000036D
		private void CheckLogsInsert(IReadOnlyList<IScenarioLogBehavior> logBehaviors)
		{
		}

		// Token: 0x0600493B RID: 18747 RVA: 0x0000216D File Offset: 0x0000036D
		public void InsertLogText(IScenarioLogTextBehavior logText)
		{
		}

		// Token: 0x0600493C RID: 18748 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTweenOpenStart()
		{
		}

		// Token: 0x0600493D RID: 18749 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnTweenCloseFinished()
		{
		}

		// Token: 0x04008710 RID: 34576
		private readonly string k_ELabelScrollArea;

		// Token: 0x04008711 RID: 34577
		private readonly string k_ELabelTextMeshTemplate;

		// Token: 0x04008712 RID: 34578
		private readonly string k_ELabelCloseButton;

		// Token: 0x04008713 RID: 34579
		private readonly string k_TweenShow;

		// Token: 0x04008714 RID: 34580
		private readonly string k_TweenHide;

		// Token: 0x04008715 RID: 34581
		private readonly ScrollRect m_ScrollArea;

		// Token: 0x04008716 RID: 34582
		private readonly TextMeshProUGUI m_TextMeshTemplate;

		// Token: 0x04008717 RID: 34583
		private int m_CreatedLogCnt;

		// Token: 0x04008718 RID: 34584
		public Action onClickCloseCallback;
	}
}
