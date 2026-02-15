using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000018 RID: 24
	public abstract class SkeletonDataModifierAsset : ScriptableObject
	{
		// Token: 0x06000077 RID: 119
		public abstract void Apply(SkeletonData skeletonData);
	}
}
