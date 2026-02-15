using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering.Universal
{
	// Token: 0x0200006F RID: 111
	internal class UniversalRenderPipelineDebugDisplayStats : DebugDisplayStats<URPProfileId>
	{
		// Token: 0x06000284 RID: 644 RVA: 0x000086FC File Offset: 0x000068FC
		public override void EnableProfilingRecorders()
		{
			this.m_RecordedSamplers = base.GetProfilerIdsToDisplay();
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000870C File Offset: 0x0000690C
		public override void DisableProfilingRecorders()
		{
			foreach (URPProfileId urpprofileId in this.m_RecordedSamplers)
			{
				ProfilingSampler.Get<URPProfileId>(urpprofileId).enableRecording = false;
			}
			this.m_RecordedSamplers.Clear();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00008770 File Offset: 0x00006970
		public override void RegisterDebugUI(List<DebugUI.Widget> list)
		{
			this.m_DebugFrameTiming.RegisterDebugUI(list);
			DebugUI.Foldout detailedStatsFoldout = new DebugUI.Foldout
			{
				displayName = "Detailed Stats",
				isHeader = true,
				opened = false,
				children = 
				{
					new DebugUI.BoolField
					{
						displayName = "Update every second with average",
						getter = () => this.averageProfilerTimingsOverASecond,
						setter = delegate(bool value)
						{
							this.averageProfilerTimingsOverASecond = value;
						}
					},
					new DebugUI.BoolField
					{
						displayName = "Hide empty scopes",
						tooltip = "Hide profiling scopes where elapsed time in each category is zero",
						getter = () => this.hideEmptyScopes,
						setter = delegate(bool value)
						{
							this.hideEmptyScopes = value;
						}
					}
				}
			};
			detailedStatsFoldout.children.Add(base.BuildDetailedStatsList("Profiling Scopes", this.m_RecordedSamplers));
			list.Add(detailedStatsFoldout);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00008854 File Offset: 0x00006A54
		public override void Update()
		{
			this.m_DebugFrameTiming.UpdateFrameTiming();
			base.UpdateDetailedStats(this.m_RecordedSamplers);
		}

		// Token: 0x0400020B RID: 523
		private DebugFrameTiming m_DebugFrameTiming = new DebugFrameTiming();

		// Token: 0x0400020C RID: 524
		private List<URPProfileId> m_RecordedSamplers = new List<URPProfileId>();
	}
}
