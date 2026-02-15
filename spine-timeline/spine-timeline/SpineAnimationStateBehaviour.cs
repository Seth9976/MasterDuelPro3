using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;
using UnityEngine.Timeline;

namespace Spine.Unity.Playables
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public class SpineAnimationStateBehaviour : PlayableBehaviour
	{
		// Token: 0x0400000C RID: 12
		[NonSerialized]
		public TimelineClip timelineClip;

		// Token: 0x0400000D RID: 13
		public AnimationReferenceAsset animationReference;

		// Token: 0x0400000E RID: 14
		public bool loop;

		// Token: 0x0400000F RID: 15
		public bool customDuration;

		// Token: 0x04000010 RID: 16
		public bool useBlendDuration = true;

		// Token: 0x04000011 RID: 17
		[SerializeField]
		private bool isInitialized;

		// Token: 0x04000012 RID: 18
		public float mixDuration = 0.1f;

		// Token: 0x04000013 RID: 19
		public bool holdPrevious;

		// Token: 0x04000014 RID: 20
		public bool dontPauseWithDirector;

		// Token: 0x04000015 RID: 21
		[FormerlySerializedAs("dontPauseOnStop")]
		public bool dontEndWithClip;

		// Token: 0x04000016 RID: 22
		public float endMixOutDuration = 0.1f;

		// Token: 0x04000017 RID: 23
		[Range(0f, 1f)]
		public float attachmentThreshold = 0.5f;

		// Token: 0x04000018 RID: 24
		[Range(0f, 1f)]
		public float eventThreshold = 0.5f;

		// Token: 0x04000019 RID: 25
		[Range(0f, 1f)]
		public float drawOrderThreshold = 0.5f;

		// Token: 0x0400001A RID: 26
		[Range(0f, 1f)]
		public float alpha = 1f;
	}
}
