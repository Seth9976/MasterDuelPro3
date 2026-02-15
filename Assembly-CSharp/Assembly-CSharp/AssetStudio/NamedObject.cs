using System;

namespace AssetStudio
{
	// Token: 0x02000103 RID: 259
	public class NamedObject : EditorExtension
	{
		// Token: 0x06000351 RID: 849 RVA: 0x00011954 File Offset: 0x0000FB54
		protected NamedObject(ObjectReader reader)
			: base(reader)
		{
			this.m_Name = reader.ReadAlignedString();
		}

		// Token: 0x04000760 RID: 1888
		public string m_Name;
	}
}
