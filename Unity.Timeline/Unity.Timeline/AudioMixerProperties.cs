using System;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002D RID: 45
	[Serializable]
	internal class AudioMixerProperties : PlayableBehaviour
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00006D18 File Offset: 0x00004F18
		public override void PrepareFrame(Playable playable, FrameData info)
		{
			if (!playable.IsValid<Playable>() || !playable.IsPlayableOfType<AudioMixerPlayable>())
			{
				return;
			}
			int inputCount = playable.GetInputCount<Playable>();
			for (int i = 0; i < inputCount; i++)
			{
				if (playable.GetInputWeight(i) > 0f)
				{
					Playable input = playable.GetInput(i);
					if (input.IsValid<Playable>() && input.IsPlayableOfType<AudioClipPlayable>())
					{
						AudioClipPlayable audioClipPlayable = (AudioClipPlayable)input;
						AudioClipProperties audioClipProperties = input.GetHandle().GetObject<AudioClipProperties>();
						audioClipPlayable.SetVolume(Mathf.Clamp01(this.volume * audioClipProperties.volume));
						audioClipPlayable.SetStereoPan(Mathf.Clamp(this.stereoPan, -1f, 1f));
						audioClipPlayable.SetSpatialBlend(Mathf.Clamp01(this.spatialBlend));
					}
				}
			}
		}

		// Token: 0x040000D5 RID: 213
		[Range(0f, 1f)]
		public float volume = 1f;

		// Token: 0x040000D6 RID: 214
		[Range(-1f, 1f)]
		public float stereoPan;

		// Token: 0x040000D7 RID: 215
		[Range(0f, 1f)]
		public float spatialBlend;
	}
}
