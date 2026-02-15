using System;

namespace AssetStudio
{
	// Token: 0x020000E5 RID: 229
	public abstract class Component : EditorExtension
	{
		// Token: 0x06000323 RID: 803 RVA: 0x0000F1D1 File Offset: 0x0000D3D1
		protected Component(ObjectReader reader)
			: base(reader)
		{
			this.m_GameObject = new PPtr<GameObject>(reader);
		}

		// Token: 0x040006CB RID: 1739
		public PPtr<GameObject> m_GameObject;
	}
}
