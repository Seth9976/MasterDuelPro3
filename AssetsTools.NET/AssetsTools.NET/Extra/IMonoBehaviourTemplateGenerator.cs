using System;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000080 RID: 128
	public interface IMonoBehaviourTemplateGenerator
	{
		// Token: 0x0600049D RID: 1181
		AssetTypeTemplateField GetTemplateField(AssetTypeTemplateField baseField, string assemblyName, string nameSpace, string className, UnityVersion unityVersion);

		// Token: 0x0600049E RID: 1182
		void Dispose();
	}
}
