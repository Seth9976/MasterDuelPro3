using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Spine.Unity.Playables
{
	// Token: 0x0200000F RID: 15
	public class SpineSkeletonFlipMixerBehaviour : PlayableBehaviour
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002B94 File Offset: 0x00000D94
		public override void OnPlayableCreate(Playable playable)
		{
			this.director = playable.GetGraph<Playable>().GetResolver() as PlayableDirector;
			if (this.director)
			{
				this.director.stopped += this.OnDirectorStopped;
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002BDE File Offset: 0x00000DDE
		public override void OnPlayableDestroy(Playable playable)
		{
			if (this.director)
			{
				this.director.stopped -= this.OnDirectorStopped;
			}
			base.OnPlayableDestroy(playable);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002C0B File Offset: 0x00000E0B
		private void OnDirectorStopped(PlayableDirector obj)
		{
			this.OnStop();
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002C14 File Offset: 0x00000E14
		public override void ProcessFrame(Playable playable, FrameData info, object playerData)
		{
			this.playableHandle = playerData as SpinePlayableHandleBase;
			if (this.playableHandle == null)
			{
				return;
			}
			Skeleton skeleton = this.playableHandle.Skeleton;
			if (!this.m_FirstFrameHappened)
			{
				this.originalScaleX = skeleton.ScaleX;
				this.originalScaleY = skeleton.ScaleY;
				this.baseScaleX = Mathf.Abs(this.originalScaleX);
				this.baseScaleY = Mathf.Abs(this.originalScaleY);
				this.m_FirstFrameHappened = true;
			}
			int inputCount = playable.GetInputCount<Playable>();
			float totalWeight = 0f;
			float greatestWeight = 0f;
			int currentInputs = 0;
			for (int i = 0; i < inputCount; i++)
			{
				float inputWeight = playable.GetInputWeight(i);
				SpineSkeletonFlipBehaviour input = ((ScriptPlayable<T>)playable.GetInput(i)).GetBehaviour();
				totalWeight += inputWeight;
				if (inputWeight > greatestWeight)
				{
					this.SetSkeletonScaleFromFlip(skeleton, input.flipX, input.flipY);
					greatestWeight = inputWeight;
				}
				if (!Mathf.Approximately(inputWeight, 0f))
				{
					currentInputs++;
				}
			}
			if (currentInputs != 1 && 1f - totalWeight > greatestWeight)
			{
				skeleton.ScaleX = this.originalScaleX;
				skeleton.ScaleY = this.originalScaleY;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002D36 File Offset: 0x00000F36
		public void SetSkeletonScaleFromFlip(Skeleton skeleton, bool flipX, bool flipY)
		{
			skeleton.ScaleX = (flipX ? (-this.baseScaleX) : this.baseScaleX);
			skeleton.ScaleY = (flipY ? (-this.baseScaleY) : this.baseScaleY);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002D68 File Offset: 0x00000F68
		public void OnStop()
		{
			this.m_FirstFrameHappened = false;
			if (this.playableHandle == null)
			{
				return;
			}
			Skeleton skeleton = this.playableHandle.Skeleton;
			skeleton.ScaleX = this.originalScaleX;
			skeleton.ScaleY = this.originalScaleY;
		}

		// Token: 0x0400002F RID: 47
		private float originalScaleX;

		// Token: 0x04000030 RID: 48
		private float originalScaleY;

		// Token: 0x04000031 RID: 49
		private float baseScaleX;

		// Token: 0x04000032 RID: 50
		private float baseScaleY;

		// Token: 0x04000033 RID: 51
		private SpinePlayableHandleBase playableHandle;

		// Token: 0x04000034 RID: 52
		private bool m_FirstFrameHappened;

		// Token: 0x04000035 RID: 53
		private PlayableDirector director;
	}
}
