using System;

namespace AssetStudio
{
	// Token: 0x020000FF RID: 255
	public sealed class MeshFilter : Component
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0001183E File Offset: 0x0000FA3E
		public MeshFilter(ObjectReader reader)
			: base(reader)
		{
			this.m_Mesh = new PPtr<Mesh>(reader);
		}

		// Token: 0x0400075A RID: 1882
		public PPtr<Mesh> m_Mesh;
	}
}
