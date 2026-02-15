using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YgomGame.Menu;
using YgomSystem.UI;

namespace YgomGame.Scenario
{
	// Token: 0x020009E6 RID: 2534
	public class ScenarioViewController : BaseMenuViewController, IDynamicChangeDispHeaderSupported
	{
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060049A0 RID: 18848 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int selectorPriorityAddRange
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060049A1 RID: 18849 RVA: 0x0000216A File Offset: 0x0000036A
		protected override Type[] textIds
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x0000216D File Offset: 0x0000036D
		public static void Open(string scenarioName, Action<bool> onCompleteCallback = null)
		{
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x0000216D File Offset: 0x0000036D
		public static void OpenDemo(string scenarioName, Action<bool> onCompleteCallback = null)
		{
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x000029CC File Offset: 0x00000BCC
		public HeaderViewController.IsDispHeader IsDispContents()
		{
			return (HeaderViewController.IsDispHeader)0;
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackEntry()
		{
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float Progress()
		{
			return 0f;
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool OnLoadCompleted()
		{
			return false;
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x0000216D File Offset: 0x0000036D
		public override void TransitionStart(ViewController.TransitionType type)
		{
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool TransitionUpdate(ViewController.TransitionType type)
		{
			return false;
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x0000216D File Offset: 0x0000036D
		private void Update()
		{
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x0000216D File Offset: 0x0000036D
		public override void NotificationStackRemove()
		{
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x0000216D File Offset: 0x0000036D
		private void Release()
		{
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnDestroy()
		{
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x0000216D File Offset: 0x0000036D
		public void Suspend()
		{
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x0000216D File Offset: 0x0000036D
		public void Resume()
		{
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateAutoHideCount()
		{
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x0000216D File Offset: 0x0000036D
		public void HideUI()
		{
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x0000216D File Offset: 0x0000036D
		public void ShowUI()
		{
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x0000216D File Offset: 0x0000036D
		public void OpenLog()
		{
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x0000216D File Offset: 0x0000036D
		public void CloseLog()
		{
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x0000216D File Offset: 0x0000036D
		private void PlayCloseFadeOut()
		{
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x0000216A File Offset: 0x0000036A
		private IEnumerator yPlayCloseFadeOut()
		{
			return null;
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangedAutoActive(bool isAuto)
		{
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x0000216D File Offset: 0x0000036D
		public void OnClick()
		{
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickInputBlocker()
		{
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickMenuAnyButton()
		{
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickAuto()
		{
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickLog()
		{
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickSkip()
		{
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickView()
		{
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnClickLogClose()
		{
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool OnBack()
		{
			return false;
		}

		// Token: 0x04008783 RID: 34691
		public const string k_PrefabPath = "Scenario/Scenario";

		// Token: 0x04008784 RID: 34692
		public const string k_ScenarioAssetPathFormat = "Scenarios/Gates/ScenarioAseets/{0}";

		// Token: 0x04008785 RID: 34693
		public const string k_ELabelBackKey = "BackKeyShortcutButton";

		// Token: 0x04008786 RID: 34694
		public const string k_ELabelLogBackKey = "LogScreen/LogBackKeyShortcutButton";

		// Token: 0x04008787 RID: 34695
		public const string k_ELabelBlockerBackKey = "BlockerBackKeyShortcutButton";

		// Token: 0x04008788 RID: 34696
		private readonly string k_TweenLabelPopSkip;

		// Token: 0x04008789 RID: 34697
		public const string k_ArgKeyName = "name";

		// Token: 0x0400878A RID: 34698
		private const string k_ArgKeyData = "data";

		// Token: 0x0400878B RID: 34699
		public const string k_ArgKeyOnComplete = "oncomplete";

		// Token: 0x0400878C RID: 34700
		private const string k_ArgKeyDemoMode = "demomode";

		// Token: 0x0400878D RID: 34701
		private string m_ScenarioName;

		// Token: 0x0400878E RID: 34702
		private List<object> m_ScenarioData;

		// Token: 0x0400878F RID: 34703
		private ScenarioWork m_Work;

		// Token: 0x04008790 RID: 34704
		private ScenarioLoadGroupContainer m_LoadGroupContainer;

		// Token: 0x04008791 RID: 34705
		private ScenarioObjectContainer m_ObjectContainer;

		// Token: 0x04008792 RID: 34706
		private ScenarioController m_ScenarioController;

		// Token: 0x04008793 RID: 34707
		private string m_BeforeBgm;

		// Token: 0x04008794 RID: 34708
		private int m_TransitionStep;

		// Token: 0x04008795 RID: 34709
		private bool m_IsUnlockedInput;

		// Token: 0x04008796 RID: 34710
		private float m_CowntdownAutoHideSec;

		// Token: 0x04008797 RID: 34711
		private bool m_ClickedInputBlocker;

		// Token: 0x04008798 RID: 34712
		private bool m_DemoSkipEnable;

		// Token: 0x04008799 RID: 34713
		private bool m_DemoSkipRequest;

		// Token: 0x0400879A RID: 34714
		private bool m_IsSkipped;

		// Token: 0x0400879B RID: 34715
		private bool m_Ready;

		// Token: 0x0400879C RID: 34716
		private bool m_IsReleased;

		// Token: 0x0400879D RID: 34717
		private Coroutine m_yCloseFadeOut;

		// Token: 0x0400879E RID: 34718
		private bool m_ClickedBlockInputFlame;

		// Token: 0x0400879F RID: 34719
		private bool m_ToFullScreenFrame;
	}
}
