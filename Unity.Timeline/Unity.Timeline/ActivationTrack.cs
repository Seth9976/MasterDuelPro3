using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x02000006 RID: 6
	[TrackClipType(typeof(ActivationPlayableAsset))]
	[TrackBindingType(typeof(GameObject))]
	[ExcludeFromPreset]
	[Serializable]
	public class ActivationTrack : TrackAsset
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021EB File Offset: 0x000003EB
		internal override bool CanCompileClips()
		{
			return !base.hasClips || base.CanCompileClips();
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000021FD File Offset: 0x000003FD
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002205 File Offset: 0x00000405
		public ActivationTrack.PostPlaybackState postPlaybackState
		{
			get
			{
				return this.m_PostPlaybackState;
			}
			set
			{
				this.m_PostPlaybackState = value;
				this.UpdateTrackMode();
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002214 File Offset: 0x00000414
		public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
		{
			ScriptPlayable<ActivationMixerPlayable> mixer = ActivationMixerPlayable.Create(graph, inputCount);
			this.m_ActivationMixer = mixer.GetBehaviour();
			this.UpdateTrackMode();
			return mixer;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002242 File Offset: 0x00000442
		internal void UpdateTrackMode()
		{
			if (this.m_ActivationMixer != null)
			{
				this.m_ActivationMixer.postPlaybackState = this.m_PostPlaybackState;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002260 File Offset: 0x00000460
		public override void GatherProperties(PlayableDirector director, IPropertyCollector driver)
		{
			GameObject gameObject = base.GetGameObjectBinding(director);
			if (gameObject != null)
			{
				driver.AddFromName(gameObject, "m_IsActive");
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000228A File Offset: 0x0000048A
		protected override void OnCreateClip(TimelineClip clip)
		{
			clip.displayName = "Active";
			base.OnCreateClip(clip);
		}

		// Token: 0x04000009 RID: 9
		[SerializeField]
		private ActivationTrack.PostPlaybackState m_PostPlaybackState = ActivationTrack.PostPlaybackState.LeaveAsIs;

		// Token: 0x0400000A RID: 10
		private ActivationMixerPlayable m_ActivationMixer;

		// Token: 0x02000007 RID: 7
		public enum PostPlaybackState
		{
			// Token: 0x0400000C RID: 12
			Active,
			// Token: 0x0400000D RID: 13
			Inactive,
			// Token: 0x0400000E RID: 14
			Revert,
			// Token: 0x0400000F RID: 15
			LeaveAsIs
		}
	}
}
