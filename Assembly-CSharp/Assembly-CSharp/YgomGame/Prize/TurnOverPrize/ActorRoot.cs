using System;
using UnityEngine;
using UnityEngine.Playables;
using YgomSystem.ElementSystem;
using YgomSystem.Timeline;
using YgomSystem.UI.ElementWidget;

namespace YgomGame.Prize.TurnOverPrize
{
	// Token: 0x02000A17 RID: 2583
	public class ActorRoot : ElementWidgetBase
	{
		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06004AF6 RID: 19190 RVA: 0x0000216A File Offset: 0x0000036A
		public LabeledPlayableController playableController
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06004AF7 RID: 19191 RVA: 0x0000216A File Offset: 0x0000036A
		public PackGroupActor packGroup
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06004AF8 RID: 19192 RVA: 0x000F2C76 File Offset: 0x000F0E76
		public ActorRoot(GameObject root3D, GameObject rootUI, ElementObjectManager eom)
			: base(null)
		{
		}

		// Token: 0x06004AF9 RID: 19193 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPackTrackOverrideEnableAll(bool enable)
		{
		}

		// Token: 0x06004AFA RID: 19194 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetPackTrackOverrideEnableAt(int idx, bool enable)
		{
		}

		// Token: 0x06004AFB RID: 19195 RVA: 0x0000216D File Offset: 0x0000036D
		public void TMRebuildGraph()
		{
		}

		// Token: 0x04008917 RID: 35095
		private const string k_ELabelRoot3D = "Root3D";

		// Token: 0x04008918 RID: 35096
		private const string k_ELabelRootUI = "RootUI";

		// Token: 0x04008919 RID: 35097
		private const string k_ELabel3D_PackGroup = "PackGroup";

		// Token: 0x0400891A RID: 35098
		internal const string k_TLabel_Shuffle = "shuffle";

		// Token: 0x0400891B RID: 35099
		internal const string k_TLabel_SelectBegin = "select_begin";

		// Token: 0x0400891C RID: 35100
		internal const string k_TLabel_SelectEnd = "select_end";

		// Token: 0x0400891D RID: 35101
		internal const string k_TLabel_Result = "result";

		// Token: 0x0400891E RID: 35102
		private readonly PlayableDirector m_Director;

		// Token: 0x0400891F RID: 35103
		private readonly LabeledPlayableController m_PlayableController;

		// Token: 0x04008920 RID: 35104
		private readonly PackGroupActor m_PackGroup;
	}
}
