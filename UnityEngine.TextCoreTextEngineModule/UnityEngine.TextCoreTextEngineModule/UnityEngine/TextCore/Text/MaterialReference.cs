using System;
using System.Collections.Generic;

namespace UnityEngine.TextCore.Text
{
	// Token: 0x02000020 RID: 32
	internal struct MaterialReference
	{
		// Token: 0x0600004C RID: 76 RVA: 0x000044DB File Offset: 0x000026DB
		public MaterialReference(int index, FontAsset fontAsset, SpriteAsset spriteAsset, Material material, float padding)
		{
			this.index = index;
			this.fontAsset = fontAsset;
			this.spriteAsset = spriteAsset;
			this.material = material;
			this.isFallbackMaterial = false;
			this.fallbackMaterial = null;
			this.padding = padding;
			this.referenceCount = 0;
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00004518 File Offset: 0x00002718
		public static int AddMaterialReference(Material material, FontAsset fontAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int materialId = material.GetHashCode();
			int index;
			bool flag = materialReferenceIndexLookup.TryGetValue(materialId, out index);
			int num;
			if (flag)
			{
				num = index;
			}
			else
			{
				index = materialReferenceIndexLookup.Count;
				materialReferenceIndexLookup[materialId] = index;
				bool flag2 = index >= materialReferences.Length;
				if (flag2)
				{
					Array.Resize<MaterialReference>(ref materialReferences, Mathf.NextPowerOfTwo(index + 1));
				}
				materialReferences[index].index = index;
				materialReferences[index].fontAsset = fontAsset;
				materialReferences[index].spriteAsset = null;
				materialReferences[index].material = material;
				materialReferences[index].referenceCount = 0;
				num = index;
			}
			return num;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000045BC File Offset: 0x000027BC
		public static int AddMaterialReference(Material material, SpriteAsset spriteAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int materialId = material.GetHashCode();
			int index;
			bool flag = materialReferenceIndexLookup.TryGetValue(materialId, out index);
			int num;
			if (flag)
			{
				num = index;
			}
			else
			{
				index = materialReferenceIndexLookup.Count;
				materialReferenceIndexLookup[materialId] = index;
				bool flag2 = index >= materialReferences.Length;
				if (flag2)
				{
					Array.Resize<MaterialReference>(ref materialReferences, Mathf.NextPowerOfTwo(index + 1));
				}
				materialReferences[index].index = index;
				materialReferences[index].fontAsset = materialReferences[0].fontAsset;
				materialReferences[index].spriteAsset = spriteAsset;
				materialReferences[index].material = material;
				materialReferences[index].referenceCount = 0;
				num = index;
			}
			return num;
		}

		// Token: 0x040000A5 RID: 165
		public int index;

		// Token: 0x040000A6 RID: 166
		public FontAsset fontAsset;

		// Token: 0x040000A7 RID: 167
		public SpriteAsset spriteAsset;

		// Token: 0x040000A8 RID: 168
		public Material material;

		// Token: 0x040000A9 RID: 169
		public bool isFallbackMaterial;

		// Token: 0x040000AA RID: 170
		public Material fallbackMaterial;

		// Token: 0x040000AB RID: 171
		public float padding;

		// Token: 0x040000AC RID: 172
		public int referenceCount;
	}
}
