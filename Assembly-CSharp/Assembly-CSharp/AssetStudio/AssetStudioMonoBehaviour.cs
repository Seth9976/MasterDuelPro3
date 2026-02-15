using System;

namespace AssetStudio
{
	// Token: 0x020000D3 RID: 211
	public sealed class AssetStudioMonoBehaviour : Behaviour
	{
		// Token: 0x06000311 RID: 785 RVA: 0x0000E96D File Offset: 0x0000CB6D
		public AssetStudioMonoBehaviour(ObjectReader reader)
			: base(reader)
		{
			this.m_Script = new PPtr<MonoScript>(reader);
			this.m_Name = reader.ReadAlignedString();
		}

		// Token: 0x0400064A RID: 1610
		public PPtr<MonoScript> m_Script;

		// Token: 0x0400064B RID: 1611
		public string m_Name;
	}
}
