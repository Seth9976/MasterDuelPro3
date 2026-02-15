using System;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200002E RID: 46
	[Serializable]
	public class AudioPlayableAsset : PlayableAsset, ITimelineClipAsset
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00006DEB File Offset: 0x00004FEB
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00006DF3 File Offset: 0x00004FF3
		internal float bufferingTime
		{
			get
			{
				return this.m_bufferingTime;
			}
			set
			{
				this.m_bufferingTime = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00006DFC File Offset: 0x00004FFC
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00006E04 File Offset: 0x00005004
		public AudioClip clip
		{
			get
			{
				return this.m_Clip;
			}
			set
			{
				this.m_Clip = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00006E0D File Offset: 0x0000500D
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00006E15 File Offset: 0x00005015
		public bool loop
		{
			get
			{
				return this.m_Loop;
			}
			set
			{
				this.m_Loop = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00006E1E File Offset: 0x0000501E
		public override double duration
		{
			get
			{
				if (this.m_Clip == null)
				{
					return base.duration;
				}
				return (double)this.m_Clip.samples / (double)this.m_Clip.frequency;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00006E4E File Offset: 0x0000504E
		public override IEnumerable<PlayableBinding> outputs
		{
			get
			{
				yield return AudioPlayableBinding.Create(base.name, this);
				yield break;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00006E60 File Offset: 0x00005060
		public override Playable CreatePlayable(PlayableGraph graph, GameObject go)
		{
			if (this.m_Clip == null)
			{
				return Playable.Null;
			}
			AudioClipPlayable audioClipPlayable = AudioClipPlayable.Create(graph, this.m_Clip, this.m_Loop);
			audioClipPlayable.GetHandle().SetScriptInstance(this.m_ClipProperties.Clone());
			return audioClipPlayable;
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00006EB4 File Offset: 0x000050B4
		public ClipCaps clipCaps
		{
			get
			{
				return ClipCaps.ClipIn | ClipCaps.SpeedMultiplier | ClipCaps.Blending | (this.m_Loop ? ClipCaps.Looping : ClipCaps.None);
			}
		}

		// Token: 0x040000D8 RID: 216
		[SerializeField]
		private AudioClip m_Clip;

		// Token: 0x040000D9 RID: 217
		[SerializeField]
		private bool m_Loop;

		// Token: 0x040000DA RID: 218
		[SerializeField]
		[HideInInspector]
		private float m_bufferingTime = 0.1f;

		// Token: 0x040000DB RID: 219
		[SerializeField]
		private AudioClipProperties m_ClipProperties = new AudioClipProperties();
	}
}
