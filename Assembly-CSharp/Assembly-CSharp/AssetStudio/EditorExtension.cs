using System;

namespace AssetStudio
{
	// Token: 0x020000E6 RID: 230
	public abstract class EditorExtension : Object
	{
		// Token: 0x06000324 RID: 804 RVA: 0x0000F1E6 File Offset: 0x0000D3E6
		protected EditorExtension(ObjectReader reader)
			: base(reader)
		{
			if (this.platform == BuildTarget.NoTarget)
			{
				new PPtr<EditorExtension>(reader);
				new PPtr<Object>(reader);
			}
		}
	}
}
