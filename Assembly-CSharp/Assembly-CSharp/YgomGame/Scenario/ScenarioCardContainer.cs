using System;
using System.Collections.Generic;
using YgomSystem.ElementSystem;

namespace YgomGame.Scenario
{
	// Token: 0x020009CD RID: 2509
	public class ScenarioCardContainer : ScenarioContainerBase
	{
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060048F8 RID: 18680 RVA: 0x0000216A File Offset: 0x0000036A
		public int[] allExistsIdxs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060048F9 RID: 18681 RVA: 0x0000216A File Offset: 0x0000036A
		public ScenarioCardActor Item
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060048FA RID: 18682 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool Contains(int index)
		{
			return false;
		}

		// Token: 0x060048FB RID: 18683 RVA: 0x000F4916 File Offset: 0x000F2B16
		public ScenarioCardContainer(ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x060048FC RID: 18684 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(ScenarioCardContainer.InitializeData initializeData)
		{
		}

		// Token: 0x060048FD RID: 18685 RVA: 0x0000216A File Offset: 0x0000036A
		public ScenarioCardActor CreateActor(int index)
		{
			return null;
		}

		// Token: 0x060048FE RID: 18686 RVA: 0x0000216A File Offset: 0x0000036A
		public ScenarioCardActor SearchByMrk(int mrk)
		{
			return null;
		}

		// Token: 0x060048FF RID: 18687 RVA: 0x000029CC File Offset: 0x00000BCC
		public int GetSlotByMrk(int mrk)
		{
			return 0;
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x000029CC File Offset: 0x00000BCC
		public bool IsReadyControllBehaviour(IScenarioCardActorBehaviour controllBehaviour)
		{
			return false;
		}

		// Token: 0x040086E5 RID: 34533
		public readonly int k_LocatorLen;

		// Token: 0x040086E6 RID: 34534
		private readonly string k_ELabelLocatorFormat;

		// Token: 0x040086E7 RID: 34535
		private readonly string k_ELabelCardLocator;

		// Token: 0x040086E8 RID: 34536
		private readonly string k_ELabelPopLocator;

		// Token: 0x040086E9 RID: 34537
		private readonly ElementObjectManager[] m_Locators;

		// Token: 0x040086EA RID: 34538
		private ElementObjectManager m_CardModelPref;

		// Token: 0x040086EB RID: 34539
		private ElementObjectManager m_CardPopPref;

		// Token: 0x040086EC RID: 34540
		private ScenarioCardActor.TimelineAssets m_TimelineAssets;

		// Token: 0x040086ED RID: 34541
		private Dictionary<int, ScenarioCardActor> m_CardActorMap;

		// Token: 0x040086EE RID: 34542
		public Action<ScenarioCardActor> onCreateCallback;

		// Token: 0x020009CE RID: 2510
		public enum Operations
		{
			// Token: 0x040086F0 RID: 34544
			InitMrk = 1,
			// Token: 0x040086F1 RID: 34545
			PlayAnimation
		}

		// Token: 0x020009CF RID: 2511
		public class InitializeData
		{
			// Token: 0x040086F2 RID: 34546
			public ElementObjectManager cardModelPref;

			// Token: 0x040086F3 RID: 34547
			public ElementObjectManager cardPopPref;

			// Token: 0x040086F4 RID: 34548
			public ScenarioCardActor.TimelineAssets timelineAssets;
		}
	}
}
