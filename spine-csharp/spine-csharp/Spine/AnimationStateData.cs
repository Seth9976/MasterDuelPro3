using System;
using System.Collections.Generic;

namespace Spine
{
	// Token: 0x0200003E RID: 62
	public class AnimationStateData
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000186 RID: 390 RVA: 0x000093FE File Offset: 0x000075FE
		public SkeletonData SkeletonData
		{
			get
			{
				return this.skeletonData;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00009406 File Offset: 0x00007606
		// (set) Token: 0x06000188 RID: 392 RVA: 0x0000940E File Offset: 0x0000760E
		public float DefaultMix
		{
			get
			{
				return this.defaultMix;
			}
			set
			{
				this.defaultMix = value;
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x00009417 File Offset: 0x00007617
		public AnimationStateData(SkeletonData skeletonData)
		{
			if (skeletonData == null)
			{
				throw new ArgumentException("skeletonData cannot be null.", "skeletonData");
			}
			this.skeletonData = skeletonData;
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000944C File Offset: 0x0000764C
		public void SetMix(string fromName, string toName, float duration)
		{
			Animation from = this.skeletonData.FindAnimation(fromName);
			if (from == null)
			{
				throw new ArgumentException("Animation not found: " + fromName, "fromName");
			}
			Animation to = this.skeletonData.FindAnimation(toName);
			if (to == null)
			{
				throw new ArgumentException("Animation not found: " + toName, "toName");
			}
			this.SetMix(from, to, duration);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x000094B0 File Offset: 0x000076B0
		public void SetMix(Animation from, Animation to, float duration)
		{
			if (from == null)
			{
				throw new ArgumentNullException("from", "from cannot be null.");
			}
			if (to == null)
			{
				throw new ArgumentNullException("to", "to cannot be null.");
			}
			AnimationStateData.AnimationPair key = new AnimationStateData.AnimationPair(from, to);
			this.animationToMixTime.Remove(key);
			this.animationToMixTime.Add(key, duration);
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00009508 File Offset: 0x00007708
		public float GetMix(Animation from, Animation to)
		{
			if (from == null)
			{
				throw new ArgumentNullException("from", "from cannot be null.");
			}
			if (to == null)
			{
				throw new ArgumentNullException("to", "to cannot be null.");
			}
			AnimationStateData.AnimationPair key = new AnimationStateData.AnimationPair(from, to);
			float duration;
			if (this.animationToMixTime.TryGetValue(key, out duration))
			{
				return duration;
			}
			return this.defaultMix;
		}

		// Token: 0x040000F3 RID: 243
		internal SkeletonData skeletonData;

		// Token: 0x040000F4 RID: 244
		private readonly Dictionary<AnimationStateData.AnimationPair, float> animationToMixTime = new Dictionary<AnimationStateData.AnimationPair, float>(AnimationStateData.AnimationPairComparer.Instance);

		// Token: 0x040000F5 RID: 245
		internal float defaultMix;

		// Token: 0x0200003F RID: 63
		public struct AnimationPair
		{
			// Token: 0x0600018D RID: 397 RVA: 0x0000955C File Offset: 0x0000775C
			public AnimationPair(Animation a1, Animation a2)
			{
				this.a1 = a1;
				this.a2 = a2;
			}

			// Token: 0x0600018E RID: 398 RVA: 0x0000956C File Offset: 0x0000776C
			public override string ToString()
			{
				return this.a1.name + "->" + this.a2.name;
			}

			// Token: 0x040000F6 RID: 246
			public readonly Animation a1;

			// Token: 0x040000F7 RID: 247
			public readonly Animation a2;
		}

		// Token: 0x02000040 RID: 64
		public class AnimationPairComparer : IEqualityComparer<AnimationStateData.AnimationPair>
		{
			// Token: 0x0600018F RID: 399 RVA: 0x0000958E File Offset: 0x0000778E
			bool IEqualityComparer<AnimationStateData.AnimationPair>.Equals(AnimationStateData.AnimationPair x, AnimationStateData.AnimationPair y)
			{
				return x.a1 == y.a1 && x.a2 == y.a2;
			}

			// Token: 0x06000190 RID: 400 RVA: 0x000095B0 File Offset: 0x000077B0
			int IEqualityComparer<AnimationStateData.AnimationPair>.GetHashCode(AnimationStateData.AnimationPair obj)
			{
				int h = obj.a1.GetHashCode();
				return ((h << 5) + h) ^ obj.a2.GetHashCode();
			}

			// Token: 0x040000F8 RID: 248
			public static readonly AnimationStateData.AnimationPairComparer Instance = new AnimationStateData.AnimationPairComparer();
		}
	}
}
