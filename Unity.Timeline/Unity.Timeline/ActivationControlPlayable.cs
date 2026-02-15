using System;
using UnityEngine.Playables;

namespace UnityEngine.Timeline
{
	// Token: 0x0200004F RID: 79
	public class ActivationControlPlayable : PlayableBehaviour
	{
		// Token: 0x060002BB RID: 699 RVA: 0x000094F4 File Offset: 0x000076F4
		public static ScriptPlayable<ActivationControlPlayable> Create(PlayableGraph graph, GameObject gameObject, ActivationControlPlayable.PostPlaybackState postPlaybackState)
		{
			if (gameObject == null)
			{
				return ScriptPlayable<ActivationControlPlayable>.Null;
			}
			ScriptPlayable<ActivationControlPlayable> handle = ScriptPlayable<ActivationControlPlayable>.Create(graph, 0);
			ActivationControlPlayable behaviour = handle.GetBehaviour();
			behaviour.gameObject = gameObject;
			behaviour.postPlayback = postPlaybackState;
			return handle;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000952D File Offset: 0x0000772D
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (this.gameObject == null)
			{
				return;
			}
			this.gameObject.SetActive(true);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000954A File Offset: 0x0000774A
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.gameObject != null && info.effectivePlayState == PlayState.Paused)
			{
				this.gameObject.SetActive(false);
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000956F File Offset: 0x0000776F
		public override void ProcessFrame(Playable playable, FrameData info, object userData)
		{
			if (this.gameObject != null)
			{
				this.gameObject.SetActive(true);
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000958B File Offset: 0x0000778B
		public override void OnGraphStart(Playable playable)
		{
			if (this.gameObject != null && this.m_InitialState == ActivationControlPlayable.InitialState.Unset)
			{
				this.m_InitialState = (this.gameObject.activeSelf ? ActivationControlPlayable.InitialState.Active : ActivationControlPlayable.InitialState.Inactive);
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000095BC File Offset: 0x000077BC
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.gameObject == null || this.m_InitialState == ActivationControlPlayable.InitialState.Unset)
			{
				return;
			}
			switch (this.postPlayback)
			{
			case ActivationControlPlayable.PostPlaybackState.Active:
				this.gameObject.SetActive(true);
				return;
			case ActivationControlPlayable.PostPlaybackState.Inactive:
				this.gameObject.SetActive(false);
				return;
			case ActivationControlPlayable.PostPlaybackState.Revert:
				this.gameObject.SetActive(this.m_InitialState == ActivationControlPlayable.InitialState.Active);
				return;
			default:
				return;
			}
		}

		// Token: 0x04000134 RID: 308
		public GameObject gameObject;

		// Token: 0x04000135 RID: 309
		public ActivationControlPlayable.PostPlaybackState postPlayback = ActivationControlPlayable.PostPlaybackState.Revert;

		// Token: 0x04000136 RID: 310
		private ActivationControlPlayable.InitialState m_InitialState;

		// Token: 0x02000050 RID: 80
		public enum PostPlaybackState
		{
			// Token: 0x04000138 RID: 312
			Active,
			// Token: 0x04000139 RID: 313
			Inactive,
			// Token: 0x0400013A RID: 314
			Revert
		}

		// Token: 0x02000051 RID: 81
		private enum InitialState
		{
			// Token: 0x0400013C RID: 316
			Unset,
			// Token: 0x0400013D RID: 317
			Active,
			// Token: 0x0400013E RID: 318
			Inactive
		}
	}
}
