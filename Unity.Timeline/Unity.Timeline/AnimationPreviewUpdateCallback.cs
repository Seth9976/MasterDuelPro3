using System;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200000F RID: 15
	internal class AnimationPreviewUpdateCallback : ITimelineEvaluateCallback
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000028CC File Offset: 0x00000ACC
		public AnimationPreviewUpdateCallback(AnimationPlayableOutput output)
		{
			this.m_Output = output;
			Playable playable = this.m_Output.GetSourcePlayable<AnimationPlayableOutput>();
			if (playable.IsValid<Playable>())
			{
				this.m_Graph = playable.GetGraph<Playable>();
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002908 File Offset: 0x00000B08
		public void Evaluate()
		{
			if (!this.m_Graph.IsValid())
			{
				return;
			}
			if (this.m_PreviewComponents == null)
			{
				this.FetchPreviewComponents();
			}
			foreach (IAnimationWindowPreview component in this.m_PreviewComponents)
			{
				if (component != null)
				{
					component.UpdatePreviewGraph(this.m_Graph);
				}
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002980 File Offset: 0x00000B80
		private void FetchPreviewComponents()
		{
			this.m_PreviewComponents = new List<IAnimationWindowPreview>();
			Animator animator = this.m_Output.GetTarget();
			if (animator == null)
			{
				return;
			}
			GameObject gameObject = animator.gameObject;
			this.m_PreviewComponents.AddRange(gameObject.GetComponents<IAnimationWindowPreview>());
		}

		// Token: 0x0400002D RID: 45
		private AnimationPlayableOutput m_Output;

		// Token: 0x0400002E RID: 46
		private PlayableGraph m_Graph;

		// Token: 0x0400002F RID: 47
		private List<IAnimationWindowPreview> m_PreviewComponents;
	}
}
