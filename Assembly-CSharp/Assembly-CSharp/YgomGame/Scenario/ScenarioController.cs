using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace YgomGame.Scenario
{
	// Token: 0x020009D3 RID: 2515
	public class ScenarioController
	{
		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06004917 RID: 18711 RVA: 0x0000216A File Offset: 0x0000036A
		public ScenarioWork work
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x06004918 RID: 18712 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x06004919 RID: 18713 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isFadeInTransitionCompleted
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

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600491A RID: 18714 RVA: 0x000029CC File Offset: 0x00000BCC
		// (set) Token: 0x0600491B RID: 18715 RVA: 0x0000216D File Offset: 0x0000036D
		public bool isFailed
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

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600491C RID: 18716 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isFinish
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x0600491D RID: 18717 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool isComplete
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x0600491E RID: 18718 RVA: 0x0000216A File Offset: 0x0000036A
		public List<ScenarioBehaviour> allBehaviours
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x0600491F RID: 18719 RVA: 0x0000216A File Offset: 0x0000036A
		public List<IScenarioLogBehavior> logBehaviors
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06004920 RID: 18720 RVA: 0x0000216A File Offset: 0x0000036A
		public StringBuilder preGenerateTextBuilder
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x00002739 File Offset: 0x00000939
		public ScenarioController(ScenarioWork work)
		{
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x0000216D File Offset: 0x0000036D
		public void Update()
		{
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool ProgressPlayBehaviours()
		{
			return false;
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x0000216D File Offset: 0x0000036D
		private void ProgressFetchBehaviours()
		{
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x0000216D File Offset: 0x0000036D
		private void OnChangeStepBehaviour(ScenarioBehaviour behabiour, ScenarioBehaviour.Step step)
		{
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x000029CC File Offset: 0x00000BCC
		public int ReadScenarioData(string scenarioName, List<object> commandList, bool loadCommand = false)
		{
			return 0;
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x0000216D File Offset: 0x0000036D
		private void AddBehavior(ScenarioBehaviour b)
		{
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x000029CC File Offset: 0x00000BCC
		public static bool IsSupportedAsyncCommand(string command)
		{
			return false;
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x0000216A File Offset: 0x0000036A
		public ScenarioBehaviour GetActiveScenarioBehavior()
		{
			return null;
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x0000216D File Offset: 0x0000036D
		private void AbortByError(ScenarioBehaviour errorBehaviour)
		{
		}

		// Token: 0x040086FE RID: 34558
		private readonly ScenarioWork m_Work;

		// Token: 0x040086FF RID: 34559
		private readonly StringBuilder m_PreGenerateTextBuilder;

		// Token: 0x04008700 RID: 34560
		private ScenarioBehaviour m_HeadBehaviour;

		// Token: 0x04008701 RID: 34561
		private readonly List<ScenarioBehaviour> m_AllBehaviours;

		// Token: 0x04008702 RID: 34562
		private readonly List<IScenarioLogBehavior> m_LogBehaviors;

		// Token: 0x04008703 RID: 34563
		private readonly Queue<ScenarioBehaviour> m_PlayQueueBehaviours;

		// Token: 0x04008704 RID: 34564
		private readonly List<ScenarioBehaviour> m_PlayBehaviours;

		// Token: 0x04008705 RID: 34565
		private readonly List<ScenarioBehaviour> m_RemoveBehavioursReserver;
	}
}
