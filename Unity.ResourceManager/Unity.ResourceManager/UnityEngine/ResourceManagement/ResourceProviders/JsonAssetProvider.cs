using System;
using System.ComponentModel;

namespace UnityEngine.ResourceManagement.ResourceProviders
{
	// Token: 0x0200005D RID: 93
	[DisplayName("JSON Asset Provider")]
	public class JsonAssetProvider : TextDataProvider
	{
		// Token: 0x06000211 RID: 529 RVA: 0x00009047 File Offset: 0x00007247
		public override object Convert(Type type, string text)
		{
			return JsonUtility.FromJson(text, type);
		}
	}
}
