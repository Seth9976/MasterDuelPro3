using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro
{
	// Token: 0x02000014 RID: 20
	public struct MaterialReference
	{
		// Token: 0x0600004D RID: 77 RVA: 0x0000271C File Offset: 0x0000091C
		public MaterialReference(int index, TMP_FontAsset fontAsset, TMP_SpriteAsset spriteAsset, Material material, float padding)
		{
			this.index = index;
			this.fontAsset = fontAsset;
			this.spriteAsset = spriteAsset;
			this.material = material;
			this.isDefaultMaterial = material.GetInstanceID() == fontAsset.material.GetInstanceID();
			this.isFallbackMaterial = false;
			this.fallbackMaterial = null;
			this.padding = padding;
			this.referenceCount = 0;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002780 File Offset: 0x00000980
		public static bool Contains(MaterialReference[] materialReferences, TMP_FontAsset fontAsset)
		{
			int id = fontAsset.GetInstanceID();
			int i = 0;
			while (i < materialReferences.Length && materialReferences[i].fontAsset != null)
			{
				if (materialReferences[i].fontAsset.GetInstanceID() == id)
				{
					return true;
				}
				i++;
			}
			return false;
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000027D0 File Offset: 0x000009D0
		public static int AddMaterialReference(Material material, TMP_FontAsset fontAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int materialID = material.GetInstanceID();
			int index;
			if (materialReferenceIndexLookup.TryGetValue(materialID, out index))
			{
				return index;
			}
			index = materialReferenceIndexLookup.Count;
			materialReferenceIndexLookup[materialID] = index;
			if (index >= materialReferences.Length)
			{
				Array.Resize<MaterialReference>(ref materialReferences, Mathf.NextPowerOfTwo(index + 1));
			}
			materialReferences[index].index = index;
			materialReferences[index].fontAsset = fontAsset;
			materialReferences[index].spriteAsset = null;
			materialReferences[index].material = material;
			materialReferences[index].isDefaultMaterial = materialID == fontAsset.material.GetInstanceID();
			materialReferences[index].referenceCount = 0;
			return index;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002878 File Offset: 0x00000A78
		public static int AddMaterialReference(Material material, TMP_SpriteAsset spriteAsset, ref MaterialReference[] materialReferences, Dictionary<int, int> materialReferenceIndexLookup)
		{
			int materialID = material.GetInstanceID();
			int index;
			if (materialReferenceIndexLookup.TryGetValue(materialID, out index))
			{
				return index;
			}
			index = materialReferenceIndexLookup.Count;
			materialReferenceIndexLookup[materialID] = index;
			if (index >= materialReferences.Length)
			{
				Array.Resize<MaterialReference>(ref materialReferences, Mathf.NextPowerOfTwo(index + 1));
			}
			materialReferences[index].index = index;
			materialReferences[index].fontAsset = materialReferences[0].fontAsset;
			materialReferences[index].spriteAsset = spriteAsset;
			materialReferences[index].material = material;
			materialReferences[index].isDefaultMaterial = true;
			materialReferences[index].referenceCount = 0;
			return index;
		}

		// Token: 0x0400002A RID: 42
		public int index;

		// Token: 0x0400002B RID: 43
		public TMP_FontAsset fontAsset;

		// Token: 0x0400002C RID: 44
		public TMP_SpriteAsset spriteAsset;

		// Token: 0x0400002D RID: 45
		public Material material;

		// Token: 0x0400002E RID: 46
		public bool isDefaultMaterial;

		// Token: 0x0400002F RID: 47
		public bool isFallbackMaterial;

		// Token: 0x04000030 RID: 48
		public Material fallbackMaterial;

		// Token: 0x04000031 RID: 49
		public float padding;

		// Token: 0x04000032 RID: 50
		public int referenceCount;
	}
}
