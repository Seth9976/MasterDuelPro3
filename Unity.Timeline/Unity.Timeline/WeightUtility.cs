using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200007C RID: 124
	internal static class WeightUtility
	{
		// Token: 0x06000376 RID: 886 RVA: 0x0000BBDC File Offset: 0x00009DDC
		public static float NormalizeMixer(Playable mixer)
		{
			if (!mixer.IsValid<Playable>())
			{
				return 0f;
			}
			int count = mixer.GetInputCount<Playable>();
			float weight = 0f;
			for (int c = 0; c < count; c++)
			{
				weight += mixer.GetInputWeight(c);
			}
			if (weight > Mathf.Epsilon && weight < 1f)
			{
				for (int c2 = 0; c2 < count; c2++)
				{
					mixer.SetInputWeight(c2, mixer.GetInputWeight(c2) / weight);
				}
			}
			return Mathf.Clamp01(weight);
		}
	}
}
