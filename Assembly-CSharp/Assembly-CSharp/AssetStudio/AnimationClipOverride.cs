using System;

namespace AssetStudio
{
	// Token: 0x020000CF RID: 207
	public class AnimationClipOverride
	{
		// Token: 0x0600030D RID: 781 RVA: 0x0000E852 File Offset: 0x0000CA52
		public AnimationClipOverride(ObjectReader reader)
		{
			this.m_OriginalClip = new PPtr<AnimationClip>(reader);
			this.m_OverrideClip = new PPtr<AnimationClip>(reader);
		}

		// Token: 0x04000641 RID: 1601
		public PPtr<AnimationClip> m_OriginalClip;

		// Token: 0x04000642 RID: 1602
		public PPtr<AnimationClip> m_OverrideClip;
	}
}
