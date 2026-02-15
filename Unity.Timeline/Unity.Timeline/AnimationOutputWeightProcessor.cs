using System;
using System.Collections.Generic;
using UnityEngine.Animations;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000008 RID: 8
	internal class AnimationOutputWeightProcessor : ITimelineEvaluateCallback
	{
		// Token: 0x06000014 RID: 20 RVA: 0x000022AD File Offset: 0x000004AD
		public AnimationOutputWeightProcessor(AnimationPlayableOutput output)
		{
			this.m_Output = output;
			output.SetWeight(0f);
			this.FindMixers();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000022D8 File Offset: 0x000004D8
		private void FindMixers()
		{
			Playable playable = this.m_Output.GetSourcePlayable<AnimationPlayableOutput>();
			int outputPort = this.m_Output.GetSourceOutputPort<AnimationPlayableOutput>();
			this.m_Mixers.Clear();
			this.FindMixers(playable, outputPort, playable.GetInput(outputPort));
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002318 File Offset: 0x00000518
		private void FindMixers(Playable parent, int port, Playable node)
		{
			if (!node.IsValid<Playable>())
			{
				return;
			}
			Type type = node.GetPlayableType();
			if (type == typeof(AnimationMixerPlayable) || type == typeof(AnimationLayerMixerPlayable))
			{
				int subCount = node.GetInputCount<Playable>();
				for (int i = 0; i < subCount; i++)
				{
					this.FindMixers(node, i, node.GetInput(i));
				}
				AnimationOutputWeightProcessor.WeightInfo weightInfo = new AnimationOutputWeightProcessor.WeightInfo
				{
					parentMixer = parent,
					mixer = node,
					port = port
				};
				this.m_Mixers.Add(weightInfo);
				return;
			}
			int count = node.GetInputCount<Playable>();
			for (int j = 0; j < count; j++)
			{
				this.FindMixers(parent, port, node.GetInput(j));
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023D8 File Offset: 0x000005D8
		public void Evaluate()
		{
			float weight = 1f;
			this.m_Output.SetWeight(1f);
			for (int i = 0; i < this.m_Mixers.Count; i++)
			{
				AnimationOutputWeightProcessor.WeightInfo mixInfo = this.m_Mixers[i];
				weight = WeightUtility.NormalizeMixer(mixInfo.mixer);
				mixInfo.parentMixer.SetInputWeight(mixInfo.port, weight);
			}
			if (Application.isPlaying)
			{
				this.m_Output.SetWeight(weight);
			}
		}

		// Token: 0x04000010 RID: 16
		private AnimationPlayableOutput m_Output;

		// Token: 0x04000011 RID: 17
		private AnimationMotionXToDeltaPlayable m_MotionXPlayable;

		// Token: 0x04000012 RID: 18
		private readonly List<AnimationOutputWeightProcessor.WeightInfo> m_Mixers = new List<AnimationOutputWeightProcessor.WeightInfo>();

		// Token: 0x02000009 RID: 9
		private struct WeightInfo
		{
			// Token: 0x04000013 RID: 19
			public Playable mixer;

			// Token: 0x04000014 RID: 20
			public Playable parentMixer;

			// Token: 0x04000015 RID: 21
			public int port;
		}
	}
}
