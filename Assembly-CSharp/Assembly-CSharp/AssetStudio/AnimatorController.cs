using System;
using System.Collections.Generic;

namespace AssetStudio
{
	// Token: 0x020000CE RID: 206
	public sealed class AnimatorController : RuntimeAnimatorController
	{
		// Token: 0x0600030C RID: 780 RVA: 0x0000E7CC File Offset: 0x0000C9CC
		public AnimatorController(ObjectReader reader)
			: base(reader)
		{
			reader.ReadUInt32();
			new ControllerConstant(reader);
			int tosSize = reader.ReadInt32();
			KeyValuePair<uint, string>[] m_TOS = new KeyValuePair<uint, string>[tosSize];
			for (int i = 0; i < tosSize; i++)
			{
				m_TOS[i] = new KeyValuePair<uint, string>(reader.ReadUInt32(), reader.ReadAlignedString());
			}
			int numClips = reader.ReadInt32();
			this.m_AnimationClips = new PPtr<AnimationClip>[numClips];
			for (int j = 0; j < numClips; j++)
			{
				this.m_AnimationClips[j] = new PPtr<AnimationClip>(reader);
			}
		}

		// Token: 0x04000640 RID: 1600
		public PPtr<AnimationClip>[] m_AnimationClips;
	}
}
