using System;

namespace AssetStudio
{
	// Token: 0x02000099 RID: 153
	public sealed class Animation : Behaviour
	{
		// Token: 0x060002CE RID: 718 RVA: 0x0000C554 File Offset: 0x0000A754
		public Animation(ObjectReader reader)
			: base(reader)
		{
			new PPtr<AnimationClip>(reader);
			int numAnimations = reader.ReadInt32();
			this.m_Animations = new PPtr<AnimationClip>[numAnimations];
			for (int i = 0; i < numAnimations; i++)
			{
				this.m_Animations[i] = new PPtr<AnimationClip>(reader);
			}
		}

		// Token: 0x04000539 RID: 1337
		public PPtr<AnimationClip>[] m_Animations;
	}
}
