using System;
using UnityEngine.Playables;

namespace UnityEngine.Animations
{
	// Token: 0x02000029 RID: 41
	public static class AnimationPlayableBinding
	{
		// Token: 0x06000242 RID: 578 RVA: 0x000059E4 File Offset: 0x00003BE4
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(Animator), new PlayableBinding.CreateOutputMethod(AnimationPlayableBinding.CreateAnimationOutput));
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00005A14 File Offset: 0x00003C14
		private static PlayableOutput CreateAnimationOutput(PlayableGraph graph, string name)
		{
			return AnimationPlayableOutput.Create(graph, name, null);
		}
	}
}
