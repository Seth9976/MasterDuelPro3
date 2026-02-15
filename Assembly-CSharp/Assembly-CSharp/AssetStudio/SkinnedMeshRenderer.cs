using System;

namespace AssetStudio
{
	// Token: 0x02000132 RID: 306
	public sealed class SkinnedMeshRenderer : Renderer
	{
		// Token: 0x0600038C RID: 908 RVA: 0x000135E8 File Offset: 0x000117E8
		public SkinnedMeshRenderer(ObjectReader reader)
			: base(reader)
		{
			reader.ReadInt32();
			reader.ReadBoolean();
			reader.ReadBoolean();
			reader.AlignStream();
			if (this.version[0] == 2 && this.version[1] < 6)
			{
				new PPtr<Animation>(reader);
			}
			this.m_Mesh = new PPtr<Mesh>(reader);
			this.m_Bones = new PPtr<Transform>[reader.ReadInt32()];
			for (int b = 0; b < this.m_Bones.Length; b++)
			{
				this.m_Bones[b] = new PPtr<Transform>(reader);
			}
			if (this.version[0] > 4 || (this.version[0] == 4 && this.version[1] >= 3))
			{
				this.m_BlendShapeWeights = reader.ReadSingleArray();
			}
		}

		// Token: 0x04000875 RID: 2165
		public PPtr<Mesh> m_Mesh;

		// Token: 0x04000876 RID: 2166
		public PPtr<Transform>[] m_Bones;

		// Token: 0x04000877 RID: 2167
		public float[] m_BlendShapeWeights;
	}
}
