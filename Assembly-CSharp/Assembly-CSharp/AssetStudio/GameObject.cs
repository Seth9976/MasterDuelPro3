using System;

namespace AssetStudio
{
	// Token: 0x020000E8 RID: 232
	public sealed class GameObject : EditorExtension
	{
		// Token: 0x06000326 RID: 806 RVA: 0x0000F454 File Offset: 0x0000D654
		public GameObject(ObjectReader reader)
			: base(reader)
		{
			int m_Component_size = reader.ReadInt32();
			this.m_Components = new PPtr<Component>[m_Component_size];
			for (int i = 0; i < m_Component_size; i++)
			{
				if ((this.version[0] == 5 && this.version[1] < 5) || this.version[0] < 5)
				{
					reader.ReadInt32();
				}
				this.m_Components[i] = new PPtr<Component>(reader);
			}
			reader.ReadInt32();
			this.m_Name = reader.ReadAlignedString();
		}

		// Token: 0x040006CD RID: 1741
		public PPtr<Component>[] m_Components;

		// Token: 0x040006CE RID: 1742
		public string m_Name;

		// Token: 0x040006CF RID: 1743
		public Transform m_Transform;

		// Token: 0x040006D0 RID: 1744
		public MeshRenderer m_MeshRenderer;

		// Token: 0x040006D1 RID: 1745
		public MeshFilter m_MeshFilter;

		// Token: 0x040006D2 RID: 1746
		public SkinnedMeshRenderer m_SkinnedMeshRenderer;

		// Token: 0x040006D3 RID: 1747
		public Animator m_Animator;

		// Token: 0x040006D4 RID: 1748
		public Animation m_Animation;
	}
}
