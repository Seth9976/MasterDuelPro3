using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000021 RID: 33
	internal class MaterialReferenceManager
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004F RID: 79 RVA: 0x0000466C File Offset: 0x0000286C
		public static MaterialReferenceManager instance
		{
			get
			{
				bool flag = MaterialReferenceManager.s_Instance == null;
				if (flag)
				{
					MaterialReferenceManager.s_Instance = new MaterialReferenceManager();
				}
				return MaterialReferenceManager.s_Instance;
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004699 File Offset: 0x00002899
		public static void AddFontAsset(FontAsset fontAsset)
		{
			MaterialReferenceManager.instance.AddFontAssetInternal(fontAsset);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000046A8 File Offset: 0x000028A8
		private void AddFontAssetInternal(FontAsset fontAsset)
		{
			bool flag = this.m_FontAssetReferenceLookup.ContainsKey(fontAsset.hashCode);
			if (!flag)
			{
				this.m_FontAssetReferenceLookup.Add(fontAsset.hashCode, fontAsset);
				this.m_FontMaterialReferenceLookup.Add(fontAsset.materialHashCode, fontAsset.material);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000046F8 File Offset: 0x000028F8
		public static void AddSpriteAsset(int hashCode, SpriteAsset spriteAsset)
		{
			MaterialReferenceManager.instance.AddSpriteAssetInternal(hashCode, spriteAsset);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00004708 File Offset: 0x00002908
		private void AddSpriteAssetInternal(int hashCode, SpriteAsset spriteAsset)
		{
			bool flag = this.m_SpriteAssetReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_SpriteAssetReferenceLookup.Add(hashCode, spriteAsset);
				this.m_FontMaterialReferenceLookup.Add(hashCode, spriteAsset.material);
				bool flag2 = spriteAsset.hashCode == 0;
				if (flag2)
				{
					spriteAsset.hashCode = hashCode;
				}
			}
		}

		// Token: 0x06000054 RID: 84 RVA: 0x0000475E File Offset: 0x0000295E
		public static void AddFontMaterial(int hashCode, Material material)
		{
			MaterialReferenceManager.instance.AddFontMaterialInternal(hashCode, material);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000476E File Offset: 0x0000296E
		private void AddFontMaterialInternal(int hashCode, Material material)
		{
			this.m_FontMaterialReferenceLookup.Add(hashCode, material);
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000477F File Offset: 0x0000297F
		public static void AddColorGradientPreset(int hashCode, TextColorGradient spriteAsset)
		{
			MaterialReferenceManager.instance.AddColorGradientPreset_Internal(hashCode, spriteAsset);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00004790 File Offset: 0x00002990
		private void AddColorGradientPreset_Internal(int hashCode, TextColorGradient spriteAsset)
		{
			bool flag = this.m_ColorGradientReferenceLookup.ContainsKey(hashCode);
			if (!flag)
			{
				this.m_ColorGradientReferenceLookup.Add(hashCode, spriteAsset);
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x000047C0 File Offset: 0x000029C0
		public static bool TryGetFontAsset(int hashCode, out FontAsset fontAsset)
		{
			return MaterialReferenceManager.instance.TryGetFontAssetInternal(hashCode, out fontAsset);
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000047E0 File Offset: 0x000029E0
		private bool TryGetFontAssetInternal(int hashCode, out FontAsset fontAsset)
		{
			fontAsset = null;
			return this.m_FontAssetReferenceLookup.TryGetValue(hashCode, out fontAsset);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x00004804 File Offset: 0x00002A04
		public static bool TryGetSpriteAsset(int hashCode, out SpriteAsset spriteAsset)
		{
			return MaterialReferenceManager.instance.TryGetSpriteAssetInternal(hashCode, out spriteAsset);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00004824 File Offset: 0x00002A24
		private bool TryGetSpriteAssetInternal(int hashCode, out SpriteAsset spriteAsset)
		{
			spriteAsset = null;
			return this.m_SpriteAssetReferenceLookup.TryGetValue(hashCode, out spriteAsset);
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00004848 File Offset: 0x00002A48
		public static bool TryGetColorGradientPreset(int hashCode, out TextColorGradient gradientPreset)
		{
			return MaterialReferenceManager.instance.TryGetColorGradientPresetInternal(hashCode, out gradientPreset);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00004868 File Offset: 0x00002A68
		private bool TryGetColorGradientPresetInternal(int hashCode, out TextColorGradient gradientPreset)
		{
			gradientPreset = null;
			return this.m_ColorGradientReferenceLookup.TryGetValue(hashCode, out gradientPreset);
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000488C File Offset: 0x00002A8C
		public static bool TryGetMaterial(int hashCode, out Material material)
		{
			return MaterialReferenceManager.instance.TryGetMaterialInternal(hashCode, out material);
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000048AC File Offset: 0x00002AAC
		private bool TryGetMaterialInternal(int hashCode, out Material material)
		{
			material = null;
			return this.m_FontMaterialReferenceLookup.TryGetValue(hashCode, out material);
		}

		// Token: 0x040000AD RID: 173
		private static MaterialReferenceManager s_Instance;

		// Token: 0x040000AE RID: 174
		private Dictionary<int, Material> m_FontMaterialReferenceLookup = new Dictionary<int, Material>();

		// Token: 0x040000AF RID: 175
		private Dictionary<int, FontAsset> m_FontAssetReferenceLookup = new Dictionary<int, FontAsset>();

		// Token: 0x040000B0 RID: 176
		private Dictionary<int, SpriteAsset> m_SpriteAssetReferenceLookup = new Dictionary<int, SpriteAsset>();

		// Token: 0x040000B1 RID: 177
		private Dictionary<int, TextColorGradient> m_ColorGradientReferenceLookup = new Dictionary<int, TextColorGradient>();
	}
}
