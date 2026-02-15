using System;
using UnityEngine.Playables;

namespace UnityEngine.Audio
{
	// Token: 0x02000019 RID: 25
	public static class AudioPlayableBinding
	{
		// Token: 0x06000121 RID: 289 RVA: 0x00003D58 File Offset: 0x00001F58
		public static PlayableBinding Create(string name, Object key)
		{
			return PlayableBinding.CreateInternal(name, key, typeof(AudioSource), new PlayableBinding.CreateOutputMethod(AudioPlayableBinding.CreateAudioOutput));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00003D88 File Offset: 0x00001F88
		private static PlayableOutput CreateAudioOutput(PlayableGraph graph, string name)
		{
			return AudioPlayableOutput.Create(graph, name, null);
		}
	}
}
