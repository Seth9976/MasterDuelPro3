using System;
using UnityEngine;

namespace Spine.Unity.AnimationTools
{
	// Token: 0x0200007E RID: 126
	public static class TimelineExtensions
	{
		// Token: 0x0600038C RID: 908 RVA: 0x00013CD4 File Offset: 0x00011ED4
		public static Vector2 Evaluate(this TranslateTimeline timeline, float time, SkeletonData skeletonData = null)
		{
			if (time < timeline.Frames[0])
			{
				return Vector2.zero;
			}
			float x;
			float y;
			timeline.GetCurveValue(out x, out y, time);
			if (skeletonData == null)
			{
				return new Vector2(x, y);
			}
			BoneData boneData = skeletonData.Bones.Items[timeline.BoneIndex];
			return new Vector2(boneData.X + x, boneData.Y + y);
		}

		// Token: 0x0600038D RID: 909 RVA: 0x00013D30 File Offset: 0x00011F30
		public static Vector2 Evaluate(TranslateXTimeline xTimeline, TranslateYTimeline yTimeline, float time, SkeletonData skeletonData = null)
		{
			float x = 0f;
			float y = 0f;
			if (xTimeline != null && time > xTimeline.Frames[0])
			{
				x = xTimeline.GetCurveValue(time);
			}
			if (yTimeline != null && time > yTimeline.Frames[0])
			{
				y = yTimeline.GetCurveValue(time);
			}
			if (skeletonData == null)
			{
				return new Vector2(x, y);
			}
			BoneData[] items = skeletonData.Bones.Items;
			BoneData boneDataX = items[xTimeline.BoneIndex];
			BoneData boneDataY = items[yTimeline.BoneIndex];
			return new Vector2(boneDataX.X + x, boneDataY.Y + y);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x00013DB4 File Offset: 0x00011FB4
		public static float Evaluate(this RotateTimeline timeline, float time, SkeletonData skeletonData = null)
		{
			if (time < timeline.Frames[0])
			{
				return 0f;
			}
			float rotation = timeline.GetCurveValue(time);
			if (skeletonData == null)
			{
				return rotation;
			}
			return skeletonData.Bones.Items[timeline.BoneIndex].Rotation + rotation;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x00013DF8 File Offset: 0x00011FF8
		public static Vector2 EvaluateTranslateXYMix(this TransformConstraintTimeline timeline, float time)
		{
			if (time < timeline.Frames[0])
			{
				return Vector2.zero;
			}
			float rotate;
			float mixX;
			float mixY;
			float scaleX;
			float scaleY;
			float shearY;
			timeline.GetCurveValue(out rotate, out mixX, out mixY, out scaleX, out scaleY, out shearY, time);
			return new Vector2(mixX, mixY);
		}

		// Token: 0x06000390 RID: 912 RVA: 0x00013E30 File Offset: 0x00012030
		public static float EvaluateRotateMix(this TransformConstraintTimeline timeline, float time)
		{
			if (time < timeline.Frames[0])
			{
				return 0f;
			}
			float rotate;
			float mixX;
			float mixY;
			float scaleX;
			float scaleY;
			float shearY;
			timeline.GetCurveValue(out rotate, out mixX, out mixY, out scaleX, out scaleY, out shearY, time);
			return rotate;
		}

		// Token: 0x06000391 RID: 913 RVA: 0x00013E64 File Offset: 0x00012064
		public static TranslateTimeline FindTranslateTimelineForBone(this Animation a, int boneIndex)
		{
			foreach (Timeline timeline in a.Timelines)
			{
				if (!timeline.GetType().IsSubclassOf(typeof(TranslateTimeline)))
				{
					TranslateTimeline translateTimeline = timeline as TranslateTimeline;
					if (translateTimeline != null && translateTimeline.BoneIndex == boneIndex)
					{
						return translateTimeline;
					}
				}
			}
			return null;
		}

		// Token: 0x06000392 RID: 914 RVA: 0x00013EE4 File Offset: 0x000120E4
		public static T FindTimelineForBone<T>(this Animation a, int boneIndex) where T : class, IBoneTimeline
		{
			foreach (Timeline timeline in a.Timelines)
			{
				T translateTimeline = timeline as T;
				if (translateTimeline != null && translateTimeline.BoneIndex == boneIndex)
				{
					return translateTimeline;
				}
			}
			return default(T);
		}

		// Token: 0x06000393 RID: 915 RVA: 0x00013F60 File Offset: 0x00012160
		public static TransformConstraintTimeline FindTransformConstraintTimeline(this Animation a, int transformConstraintIndex)
		{
			foreach (Timeline timeline in a.Timelines)
			{
				if (!timeline.GetType().IsSubclassOf(typeof(TransformConstraintTimeline)))
				{
					TransformConstraintTimeline transformConstraintTimeline = timeline as TransformConstraintTimeline;
					if (transformConstraintTimeline != null && transformConstraintTimeline.TransformConstraintIndex == transformConstraintIndex)
					{
						return transformConstraintTimeline;
					}
				}
			}
			return null;
		}
	}
}
