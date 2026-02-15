using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000030 RID: 48
	[TrackClipType(typeof(AudioPlayableAsset), false)]
	[TrackBindingType(typeof(AudioSource))]
	[ExcludeFromPreset]
	[Serializable]
	public class AudioTrack : TrackAsset
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x00006FB0 File Offset: 0x000051B0
		public TimelineClip CreateClip(AudioClip clip)
		{
			if (clip == null)
			{
				return null;
			}
			TimelineClip timelineClip = base.CreateDefaultClip();
			AudioPlayableAsset audioAsset = timelineClip.asset as AudioPlayableAsset;
			if (audioAsset != null)
			{
				audioAsset.clip = clip;
			}
			timelineClip.duration = (double)clip.length;
			timelineClip.displayName = clip.name;
			return timelineClip;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00007004 File Offset: 0x00005204
		internal override Playable CompileClips(PlayableGraph graph, GameObject go, IList<TimelineClip> timelineClips, IntervalTree<RuntimeElement> tree)
		{
			AudioMixerPlayable clipBlender = AudioMixerPlayable.Create(graph, timelineClips.Count, false);
			if (base.hasCurves)
			{
				clipBlender.GetHandle().SetScriptInstance(this.m_TrackProperties.Clone());
			}
			for (int i = 0; i < timelineClips.Count; i++)
			{
				TimelineClip c = timelineClips[i];
				PlayableAsset asset = c.asset as PlayableAsset;
				if (!(asset == null))
				{
					float buffer = 0.1f;
					AudioPlayableAsset audioAsset = c.asset as AudioPlayableAsset;
					if (audioAsset != null)
					{
						buffer = audioAsset.bufferingTime;
					}
					Playable source = asset.CreatePlayable(graph, go);
					if (source.IsValid<Playable>())
					{
						if (source.IsPlayableOfType<AudioClipPlayable>())
						{
							AudioClipPlayable audioClipPlayable = (AudioClipPlayable)source;
							AudioClipProperties audioClipProperties = audioClipPlayable.GetHandle().GetObject<AudioClipProperties>();
							audioClipPlayable.SetVolume(Mathf.Clamp01(this.m_TrackProperties.volume * audioClipProperties.volume));
							audioClipPlayable.SetStereoPan(Mathf.Clamp(this.m_TrackProperties.stereoPan, -1f, 1f));
							audioClipPlayable.SetSpatialBlend(Mathf.Clamp01(this.m_TrackProperties.spatialBlend));
						}
						tree.Add(new ScheduleRuntimeClip(c, source, clipBlender, (double)buffer, 0.1));
						graph.Connect<Playable, AudioMixerPlayable>(source, 0, clipBlender, i);
						source.SetSpeed(c.timeScale);
						source.SetDuration(c.extrapolatedDuration);
						clipBlender.SetInputWeight(source, 1f);
					}
				}
			}
			base.ConfigureTrackAnimation(tree, go, clipBlender);
			return clipBlender;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001DA RID: 474 RVA: 0x0000719C File Offset: 0x0000539C
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				yield return AudioPlayableBinding.Create(base.name, this);
				yield break;
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x000071AC File Offset: 0x000053AC
		private void OnValidate()
		{
			this.m_TrackProperties.volume = Mathf.Clamp01(this.m_TrackProperties.volume);
			this.m_TrackProperties.stereoPan = Mathf.Clamp(this.m_TrackProperties.stereoPan, -1f, 1f);
			this.m_TrackProperties.spatialBlend = Mathf.Clamp01(this.m_TrackProperties.spatialBlend);
		}

		// Token: 0x040000E0 RID: 224
		[SerializeField]
		private AudioMixerProperties m_TrackProperties = new AudioMixerProperties();
	}
}
