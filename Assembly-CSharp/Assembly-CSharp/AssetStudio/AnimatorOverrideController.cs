using System;

namespace AssetStudio
{
	// Token: 0x020000D0 RID: 208
	public sealed class AnimatorOverrideController : RuntimeAnimatorController
	{
		// Token: 0x0600030E RID: 782 RVA: 0x0000E874 File Offset: 0x0000CA74
		public AnimatorOverrideController(ObjectReader reader)
			: base(reader)
		{
			this.m_Controller = new PPtr<RuntimeAnimatorController>(reader);
			int numOverrides = reader.ReadInt32();
			this.m_Clips = new AnimationClipOverride[numOverrides];
			for (int i = 0; i < numOverrides; i++)
			{
				this.m_Clips[i] = new AnimationClipOverride(reader);
			}
		}

		// Token: 0x04000643 RID: 1603
		public PPtr<RuntimeAnimatorController> m_Controller;

		// Token: 0x04000644 RID: 1604
		public AnimationClipOverride[] m_Clips;
	}
}
