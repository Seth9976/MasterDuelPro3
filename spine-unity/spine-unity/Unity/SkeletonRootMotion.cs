using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000026 RID: 38
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonRootMotion")]
	public class SkeletonRootMotion : SkeletonRootMotionBase
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00006144 File Offset: 0x00004344
		public override Vector2 GetRemainingRootMotion(int trackIndex)
		{
			TrackEntry track = this.animationState.GetCurrent(trackIndex);
			if (track == null)
			{
				return Vector2.zero;
			}
			Animation animation = track.Animation;
			float start = track.AnimationTime;
			float end = animation.Duration;
			return base.GetAnimationRootMotion(start, end, animation);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006188 File Offset: 0x00004388
		public override SkeletonRootMotionBase.RootMotionInfo GetRootMotionInfo(int trackIndex)
		{
			TrackEntry track = this.animationState.GetCurrent(trackIndex);
			if (track == null)
			{
				return default(SkeletonRootMotionBase.RootMotionInfo);
			}
			Animation animation = track.Animation;
			float time = track.AnimationTime;
			return base.GetAnimationRootMotionInfo(track.Animation, time);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x000061CA File Offset: 0x000043CA
		protected override float AdditionalScale
		{
			get
			{
				if (!this.skeletonGraphic)
				{
					return 1f;
				}
				return this.skeletonGraphic.MeshScale;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000061EA File Offset: 0x000043EA
		protected override void Reset()
		{
			base.Reset();
			this.animationTrackFlags = -1;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000061FC File Offset: 0x000043FC
		public override void Initialize()
		{
			base.Initialize();
			IAnimationStateComponent animstateComponent = this.skeletonComponent as IAnimationStateComponent;
			this.animationState = ((animstateComponent != null) ? animstateComponent.AnimationState : null);
			this.skeletonGraphic = base.GetComponent<SkeletonGraphic>();
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000623C File Offset: 0x0000443C
		protected override Vector2 CalculateAnimationsMovementDelta()
		{
			Vector2 localDelta = Vector2.zero;
			int trackCount = this.animationState.Tracks.Count;
			for (int trackIndex = 0; trackIndex < trackCount; trackIndex++)
			{
				if (this.animationTrackFlags == -1 || (this.animationTrackFlags & (1 << trackIndex)) != 0)
				{
					TrackEntry track = this.animationState.GetCurrent(trackIndex);
					TrackEntry next = null;
					while (track != null)
					{
						Animation animation = track.Animation;
						float start = track.AnimationLast;
						float end = track.AnimationTime;
						Vector2 currentDelta = base.GetAnimationRootMotion(start, end, animation);
						if (currentDelta != Vector2.zero)
						{
							this.ApplyMixAlphaToDelta(ref currentDelta, next, track);
							localDelta += currentDelta;
						}
						next = track;
						track = track.MixingFrom;
					}
				}
			}
			return localDelta;
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000062F4 File Offset: 0x000044F4
		protected override float CalculateAnimationsRotationDelta()
		{
			float localDelta = 0f;
			int trackCount = this.animationState.Tracks.Count;
			for (int trackIndex = 0; trackIndex < trackCount; trackIndex++)
			{
				if (this.animationTrackFlags == -1 || (this.animationTrackFlags & (1 << trackIndex)) != 0)
				{
					TrackEntry track = this.animationState.GetCurrent(trackIndex);
					TrackEntry next = null;
					while (track != null)
					{
						Animation animation = track.Animation;
						float start = track.AnimationLast;
						float end = track.AnimationTime;
						float currentDelta = base.GetAnimationRootMotionRotation(start, end, animation);
						if (currentDelta != 0f)
						{
							this.ApplyMixAlphaToDelta(ref currentDelta, next, track);
							localDelta += currentDelta;
						}
						next = track;
						track = track.MixingFrom;
					}
				}
			}
			return localDelta;
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000639C File Offset: 0x0000459C
		private void ApplyMixAlphaToDelta(ref Vector2 currentDelta, TrackEntry next, TrackEntry track)
		{
			float mixAlpha = 1f;
			this.GetMixAlpha(ref mixAlpha, next, track);
			currentDelta *= mixAlpha;
		}

		// Token: 0x060000EB RID: 235 RVA: 0x000063CC File Offset: 0x000045CC
		private void ApplyMixAlphaToDelta(ref float currentDelta, TrackEntry next, TrackEntry track)
		{
			float mixAlpha = 1f;
			this.GetMixAlpha(ref mixAlpha, next, track);
			currentDelta *= mixAlpha;
		}

		// Token: 0x060000EC RID: 236 RVA: 0x000063F0 File Offset: 0x000045F0
		private void GetMixAlpha(ref float cumulatedMixAlpha, TrackEntry next, TrackEntry track)
		{
			float mix;
			if (next != null)
			{
				if (next.MixDuration == 0f)
				{
					mix = 1f;
				}
				else
				{
					mix = next.MixTime / next.MixDuration;
					if (mix > 1f)
					{
						mix = 1f;
					}
				}
				float mixAndAlpha = track.Alpha * next.InterruptAlpha * (1f - mix);
				cumulatedMixAlpha *= mixAndAlpha;
				return;
			}
			if (track.MixDuration == 0f)
			{
				mix = 1f;
			}
			else
			{
				mix = track.Alpha * (track.MixTime / track.MixDuration);
				if (mix > 1f)
				{
					mix = 1f;
				}
			}
			cumulatedMixAlpha *= mix;
		}

		// Token: 0x0400009D RID: 157
		private const int DefaultAnimationTrackFlags = -1;

		// Token: 0x0400009E RID: 158
		public int animationTrackFlags = -1;

		// Token: 0x0400009F RID: 159
		private AnimationState animationState;

		// Token: 0x040000A0 RID: 160
		private SkeletonGraphic skeletonGraphic;
	}
}
