using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

namespace Spine.Unity.Playables
{
	// Token: 0x0200000D RID: 13
	public class SpineAnimationStateMixerBehaviour : PlayableBehaviour
	{
		// Token: 0x06000022 RID: 34 RVA: 0x000023EC File Offset: 0x000005EC
		public override void OnBehaviourPause(Playable playable, FrameData info)
		{
			if (this.pauseWithDirector)
			{
				if (!this.isPaused)
				{
					this.HandlePause(playable);
				}
				this.isPaused = true;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000240C File Offset: 0x0000060C
		public override void OnGraphStop(Playable playable)
		{
			if (playable.GetGraph<Playable>().IsPlaying() && this.endAtClipEnd)
			{
				this.HandleClipEnd();
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002437 File Offset: 0x00000637
		public override void OnBehaviourPlay(Playable playable, FrameData info)
		{
			if (this.isPaused)
			{
				this.HandleResume(playable);
			}
			this.isPaused = false;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002450 File Offset: 0x00000650
		protected void HandlePause(Playable playable)
		{
			if (this.animationStateComponent.IsNullOrDestroyed())
			{
				return;
			}
			TrackEntry current = this.animationStateComponent.AnimationState.GetCurrent(this.trackIndex);
			if (current != null && current == this.timelineStartedTrackEntry)
			{
				this.previousTimeScale = current.TimeScale;
				current.TimeScale = 0f;
				this.pausedTrackEntry = current;
			}
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000024AC File Offset: 0x000006AC
		protected void HandleResume(Playable playable)
		{
			if (this.animationStateComponent.IsNullOrDestroyed())
			{
				return;
			}
			TrackEntry current = this.animationStateComponent.AnimationState.GetCurrent(this.trackIndex);
			if (current != null && current == this.pausedTrackEntry)
			{
				current.TimeScale = this.previousTimeScale;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000024F8 File Offset: 0x000006F8
		protected void HandleClipEnd()
		{
			if (this.animationStateComponent.IsNullOrDestroyed())
			{
				return;
			}
			AnimationState state = this.animationStateComponent.AnimationState;
			if (this.endAtClipEnd && this.timelineStartedTrackEntry != null && this.timelineStartedTrackEntry == state.GetCurrent(this.trackIndex))
			{
				if (this.endMixOutDuration >= 0f)
				{
					state.SetEmptyAnimation(this.trackIndex, this.endMixOutDuration);
				}
				else
				{
					this.timelineStartedTrackEntry.TimeScale = 0f;
				}
				this.timelineStartedTrackEntry = null;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x0000257C File Offset: 0x0000077C
		private void AdjustTrackEntryTimeScale(Playable playable, int input, TrackEntry currentTrackEntry)
		{
			if (currentTrackEntry == null)
			{
				return;
			}
			ScriptPlayable<SpineAnimationStateBehaviour> clipPlayable = (ScriptPlayable<T>)playable.GetInput(input);
			float clipSpeed = (float)clipPlayable.GetSpeed<ScriptPlayable<SpineAnimationStateBehaviour>>();
			SpineAnimationStateBehaviour clipData = clipPlayable.GetBehaviour();
			if (clipData != null && clipData.animationReference != null && currentTrackEntry.Animation == clipData.animationReference.Animation)
			{
				currentTrackEntry.TimeScale = clipSpeed * this.rootPlayableSpeed;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000025DC File Offset: 0x000007DC
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			SkeletonAnimation skeletonAnimation = playerData as SkeletonAnimation;
			SkeletonGraphic skeletonGraphic = playerData as SkeletonGraphic;
			this.animationStateComponent = playerData as IAnimationStateComponent;
			ISkeletonComponent skeletonComponent = playerData as ISkeletonComponent;
			if (this.animationStateComponent.IsNullOrDestroyed() || skeletonComponent == null)
			{
				return;
			}
			Skeleton skeleton = skeletonComponent.Skeleton;
			AnimationState state = this.animationStateComponent.AnimationState;
			if (!Application.isPlaying)
			{
				return;
			}
			int inputCount = playable.GetInputCount<Playable>();
			float num = this.rootPlayableSpeed;
			this.rootPlayableSpeed = this.GetRootPlayableSpeed(playable);
			bool rootSpeedChanged = num != this.rootPlayableSpeed;
			if (this.lastInputWeights == null || this.lastInputWeights.Length < inputCount)
			{
				this.lastInputWeights = new float[inputCount];
				for (int i = 0; i < inputCount; i++)
				{
					this.lastInputWeights[i] = 0f;
				}
			}
			float[] lastInputWeights = this.lastInputWeights;
			int numStartingClips = 0;
			bool anyClipPlaying = false;
			for (int j = 0; j < inputCount; j++)
			{
				float lastInputWeight = lastInputWeights[j];
				float inputWeight = playable.GetInputWeight(j);
				bool flag = inputWeight > 0f && (lastInputWeight == 0f || info.seekOccurred || info.timeLooped);
				if (inputWeight > 0f)
				{
					anyClipPlaying = true;
				}
				lastInputWeights[j] = inputWeight;
				if (flag)
				{
					if (numStartingClips < 2)
					{
						ScriptPlayable<SpineAnimationStateBehaviour> clipPlayable = (ScriptPlayable<T>)playable.GetInput(j);
						this.startingClips[numStartingClips++] = clipPlayable;
					}
				}
				else if (rootSpeedChanged)
				{
					TrackEntry currentEntry = state.GetCurrent(this.trackIndex);
					this.AdjustTrackEntryTimeScale(playable, j, currentEntry);
				}
			}
			if (numStartingClips == 2)
			{
				ScriptPlayable<SpineAnimationStateBehaviour> clipPlayable2 = this.startingClips[0];
				ScriptPlayable<SpineAnimationStateBehaviour> clipPlayable3 = this.startingClips[1];
				if (clipPlayable2.GetDuration<ScriptPlayable<SpineAnimationStateBehaviour>>() > clipPlayable3.GetDuration<ScriptPlayable<SpineAnimationStateBehaviour>>())
				{
					this.startingClips[0] = clipPlayable3;
					this.startingClips[1] = clipPlayable2;
				}
			}
			for (int k = 0; k < numStartingClips; k++)
			{
				ScriptPlayable<SpineAnimationStateBehaviour> clipPlayable4 = this.startingClips[k];
				SpineAnimationStateBehaviour clipData = clipPlayable4.GetBehaviour();
				this.pauseWithDirector = !clipData.dontPauseWithDirector;
				this.endAtClipEnd = !clipData.dontEndWithClip;
				this.endMixOutDuration = clipData.endMixOutDuration;
				if (clipData.animationReference == null)
				{
					float mixDuration = (clipData.customDuration ? this.GetCustomMixDuration(clipData) : state.Data.DefaultMix);
					state.SetEmptyAnimation(this.trackIndex, mixDuration);
				}
				else if (clipData.animationReference.Animation != null)
				{
					this.animationStateComponent.UnscaledTime = this.unscaledTime;
					bool current = state.GetCurrent(this.trackIndex) != null;
					float customMixDuration = (clipData.customDuration ? this.GetCustomMixDuration(clipData) : 0f);
					TrackEntry trackEntry;
					if (!current && customMixDuration > 0f)
					{
						state.SetEmptyAnimation(this.trackIndex, 0f);
						trackEntry = state.AddAnimation(this.trackIndex, clipData.animationReference.Animation, clipData.loop, 0f);
					}
					else
					{
						trackEntry = state.SetAnimation(this.trackIndex, clipData.animationReference.Animation, clipData.loop);
					}
					float clipSpeed = (float)clipPlayable4.GetSpeed<ScriptPlayable<SpineAnimationStateBehaviour>>();
					trackEntry.EventThreshold = clipData.eventThreshold;
					trackEntry.MixDrawOrderThreshold = clipData.drawOrderThreshold;
					trackEntry.TrackTime = (float)clipPlayable4.GetTime<ScriptPlayable<SpineAnimationStateBehaviour>>();
					trackEntry.TimeScale = clipSpeed * this.rootPlayableSpeed;
					trackEntry.MixAttachmentThreshold = clipData.attachmentThreshold;
					trackEntry.HoldPrevious = clipData.holdPrevious;
					trackEntry.Alpha = clipData.alpha;
					if (clipData.customDuration)
					{
						trackEntry.SetMixDuration(customMixDuration / this.rootPlayableSpeed, 0f);
					}
					this.timelineStartedTrackEntry = trackEntry;
				}
			}
			if (numStartingClips > 0)
			{
				if (skeletonAnimation)
				{
					skeletonAnimation.Update(0f);
					skeletonAnimation.LateUpdate();
				}
				else if (skeletonGraphic)
				{
					skeletonGraphic.Update(0f);
					skeletonGraphic.LateUpdate();
				}
			}
			this.startingClips[0] = (this.startingClips[1] = ScriptPlayable<SpineAnimationStateBehaviour>.Null);
			if (this.lastAnyClipPlaying && !anyClipPlaying)
			{
				this.HandleClipEnd();
			}
			this.lastAnyClipPlaying = anyClipPlaying;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000029FC File Offset: 0x00000BFC
		private float GetRootPlayableSpeed(Playable playable)
		{
			PlayableGraph graph = playable.GetGraph<Playable>();
			int rootPlayableCount = graph.GetRootPlayableCount();
			if (rootPlayableCount == 1)
			{
				return (float)graph.GetRootPlayable(0).GetSpeed<Playable>();
			}
			for (int rootIndex = 0; rootIndex < rootPlayableCount; rootIndex++)
			{
				Playable rootPlayable = graph.GetRootPlayable(rootIndex);
				int i = 0;
				int j = rootPlayable.GetInputCount<Playable>();
				while (i < j)
				{
					if (rootPlayable.GetInput(i).Equals(playable))
					{
						return (float)rootPlayable.GetSpeed<Playable>();
					}
					i++;
				}
			}
			return 1f;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002A7C File Offset: 0x00000C7C
		private float GetCustomMixDuration(SpineAnimationStateBehaviour clipData)
		{
			if (clipData.useBlendDuration)
			{
				TimelineClip clip = clipData.timelineClip;
				return (float)Math.Max(clip.blendInDuration, clip.easeInDuration);
			}
			return clipData.mixDuration;
		}

		// Token: 0x0400001F RID: 31
		private float[] lastInputWeights;

		// Token: 0x04000020 RID: 32
		private bool lastAnyClipPlaying;

		// Token: 0x04000021 RID: 33
		public int trackIndex;

		// Token: 0x04000022 RID: 34
		public bool unscaledTime;

		// Token: 0x04000023 RID: 35
		private ScriptPlayable<SpineAnimationStateBehaviour>[] startingClips = new ScriptPlayable<SpineAnimationStateBehaviour>[2];

		// Token: 0x04000024 RID: 36
		private IAnimationStateComponent animationStateComponent;

		// Token: 0x04000025 RID: 37
		private bool pauseWithDirector = true;

		// Token: 0x04000026 RID: 38
		private bool endAtClipEnd = true;

		// Token: 0x04000027 RID: 39
		private float endMixOutDuration = 0.1f;

		// Token: 0x04000028 RID: 40
		private bool isPaused;

		// Token: 0x04000029 RID: 41
		private TrackEntry pausedTrackEntry;

		// Token: 0x0400002A RID: 42
		private float previousTimeScale = 1f;

		// Token: 0x0400002B RID: 43
		private float rootPlayableSpeed = 1f;

		// Token: 0x0400002C RID: 44
		private TrackEntry timelineStartedTrackEntry;
	}
}
