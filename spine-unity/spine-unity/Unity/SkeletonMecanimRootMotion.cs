using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000025 RID: 37
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonMecanimRootMotion")]
	public class SkeletonMecanimRootMotion : SkeletonRootMotionBase
	{
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00005F28 File Offset: 0x00004128
		public SkeletonMecanim SkeletonMecanim
		{
			get
			{
				if (!this.skeletonMecanim)
				{
					return this.skeletonMecanim = base.GetComponent<SkeletonMecanim>();
				}
				return this.skeletonMecanim;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005F58 File Offset: 0x00004158
		public override Vector2 GetRemainingRootMotion(int layerIndex)
		{
			KeyValuePair<Animation, float> pair = this.skeletonMecanim.Translator.GetActiveAnimationAndTime(layerIndex);
			Animation animation = pair.Key;
			float time = pair.Value;
			if (animation == null)
			{
				return Vector2.zero;
			}
			float start = time;
			float end = animation.Duration;
			return base.GetAnimationRootMotion(start, end, animation);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005FA4 File Offset: 0x000041A4
		public override SkeletonRootMotionBase.RootMotionInfo GetRootMotionInfo(int layerIndex)
		{
			KeyValuePair<Animation, float> pair = this.skeletonMecanim.Translator.GetActiveAnimationAndTime(layerIndex);
			Animation animation = pair.Key;
			float time = pair.Value;
			if (animation == null)
			{
				return default(SkeletonRootMotionBase.RootMotionInfo);
			}
			return base.GetAnimationRootMotionInfo(animation, time);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00005FE8 File Offset: 0x000041E8
		protected override void Reset()
		{
			base.Reset();
			this.mecanimLayerFlags = -1;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00005FF8 File Offset: 0x000041F8
		public override void Initialize()
		{
			base.Initialize();
			this.skeletonMecanim = base.GetComponent<SkeletonMecanim>();
			if (this.skeletonMecanim)
			{
				this.skeletonMecanim.Translator.OnClipApplied -= this.OnClipApplied;
				this.skeletonMecanim.Translator.OnClipApplied += this.OnClipApplied;
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000605C File Offset: 0x0000425C
		private void OnClipApplied(Animation animation, int layerIndex, float weight, float time, float lastTime, bool playsBackward)
		{
			if ((this.mecanimLayerFlags & (1 << layerIndex)) == 0 || weight == 0f)
			{
				return;
			}
			if (!playsBackward)
			{
				this.movementDelta += weight * base.GetAnimationRootMotion(lastTime, time, animation);
			}
			else
			{
				this.movementDelta -= weight * base.GetAnimationRootMotion(time, lastTime, animation);
			}
			if (this.transformRotation)
			{
				if (!playsBackward)
				{
					this.rotationDelta += weight * base.GetAnimationRootMotionRotation(lastTime, time, animation);
					return;
				}
				this.rotationDelta -= weight * base.GetAnimationRootMotionRotation(time, lastTime, animation);
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000610C File Offset: 0x0000430C
		protected override Vector2 CalculateAnimationsMovementDelta()
		{
			Vector2 vector = this.movementDelta;
			this.movementDelta = Vector2.zero;
			return vector;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000611F File Offset: 0x0000431F
		protected override float CalculateAnimationsRotationDelta()
		{
			float num = this.rotationDelta;
			this.rotationDelta = 0f;
			return num;
		}

		// Token: 0x04000098 RID: 152
		private const int DefaultMecanimLayerFlags = -1;

		// Token: 0x04000099 RID: 153
		public int mecanimLayerFlags = -1;

		// Token: 0x0400009A RID: 154
		protected Vector2 movementDelta;

		// Token: 0x0400009B RID: 155
		protected float rotationDelta;

		// Token: 0x0400009C RID: 156
		private SkeletonMecanim skeletonMecanim;
	}
}
