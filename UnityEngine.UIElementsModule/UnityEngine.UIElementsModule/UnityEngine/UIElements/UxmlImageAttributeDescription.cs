using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A7 RID: 1191
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class UxmlImageAttributeDescription : UxmlAttributeDescription
	{
		// Token: 0x06002213 RID: 8723 RVA: 0x0007C910 File Offset: 0x0007AB10
		public UxmlImageAttributeDescription()
		{
			base.type = "string";
			base.typeNamespace = "http://www.w3.org/2001/XMLSchema";
			this.defaultValue = default(Background);
		}

		// Token: 0x1700090E RID: 2318
		// (set) Token: 0x06002214 RID: 8724 RVA: 0x0007C94D File Offset: 0x0007AB4D
		public Background defaultValue
		{
			[CompilerGenerated]
			set
			{
				this.<defaultValue>k__BackingField = value;
			}
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x0007C958 File Offset: 0x0007AB58
		public Background GetValueFromBag(IUxmlAttributes bag, CreationContext cc)
		{
			string path;
			VisualTreeAsset sourceAsset;
			bool flag = base.TryGetValueFromBagAsString(bag, cc, out path, out sourceAsset) && sourceAsset != null;
			Background background;
			if (flag)
			{
				bool flag2 = path == null;
				if (flag2)
				{
					background = default(Background);
				}
				else
				{
					bool flag3 = this.m_AssetType == null;
					if (flag3)
					{
						this.m_AssetType = sourceAsset.GetAssetType(path);
					}
					background = Background.FromObject(sourceAsset.GetAsset(path, this.m_AssetType));
				}
			}
			else
			{
				background = default(Background);
			}
			return background;
		}

		// Token: 0x04000F09 RID: 3849
		private Type m_AssetType;
	}
}
