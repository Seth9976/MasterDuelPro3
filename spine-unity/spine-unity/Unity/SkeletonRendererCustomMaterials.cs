using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000044 RID: 68
	[ExecuteAlways]
	[HelpURL("http://esotericsoftware.com/spine-unity#SkeletonRendererCustomMaterials")]
	public class SkeletonRendererCustomMaterials : MonoBehaviour
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000D8AC File Offset: 0x0000BAAC
		private void SetCustomSlotMaterials()
		{
			if (this.skeletonRenderer == null)
			{
				Debug.LogError("skeletonRenderer == null");
				return;
			}
			for (int i = 0; i < this.customSlotMaterials.Count; i++)
			{
				SkeletonRendererCustomMaterials.SlotMaterialOverride slotMaterialOverride = this.customSlotMaterials[i];
				if (!slotMaterialOverride.overrideDisabled && !string.IsNullOrEmpty(slotMaterialOverride.slotName))
				{
					Slot slotObject = this.skeletonRenderer.skeleton.FindSlot(slotMaterialOverride.slotName);
					if (slotObject != null)
					{
						this.skeletonRenderer.CustomSlotMaterials[slotObject] = slotMaterialOverride.material;
					}
				}
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000D93C File Offset: 0x0000BB3C
		private void RemoveCustomSlotMaterials()
		{
			if (this.skeletonRenderer == null)
			{
				Debug.LogError("skeletonRenderer == null");
				return;
			}
			for (int i = 0; i < this.customSlotMaterials.Count; i++)
			{
				SkeletonRendererCustomMaterials.SlotMaterialOverride slotMaterialOverride = this.customSlotMaterials[i];
				if (!string.IsNullOrEmpty(slotMaterialOverride.slotName))
				{
					Slot slotObject = this.skeletonRenderer.skeleton.FindSlot(slotMaterialOverride.slotName);
					Material currentMaterial;
					if (slotObject != null && this.skeletonRenderer.CustomSlotMaterials.TryGetValue(slotObject, out currentMaterial) && !(currentMaterial != slotMaterialOverride.material))
					{
						this.skeletonRenderer.CustomSlotMaterials.Remove(slotObject);
					}
				}
			}
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
		private void SetCustomMaterialOverrides()
		{
			if (this.skeletonRenderer == null)
			{
				Debug.LogError("skeletonRenderer == null");
				return;
			}
			for (int i = 0; i < this.customMaterialOverrides.Count; i++)
			{
				SkeletonRendererCustomMaterials.AtlasMaterialOverride atlasMaterialOverride = this.customMaterialOverrides[i];
				if (!atlasMaterialOverride.overrideDisabled)
				{
					this.skeletonRenderer.CustomMaterialOverride[atlasMaterialOverride.originalMaterial] = atlasMaterialOverride.replacementMaterial;
				}
			}
		}

		// Token: 0x06000284 RID: 644 RVA: 0x0000DA54 File Offset: 0x0000BC54
		private void RemoveCustomMaterialOverrides()
		{
			if (this.skeletonRenderer == null)
			{
				Debug.LogError("skeletonRenderer == null");
				return;
			}
			for (int i = 0; i < this.customMaterialOverrides.Count; i++)
			{
				SkeletonRendererCustomMaterials.AtlasMaterialOverride atlasMaterialOverride = this.customMaterialOverrides[i];
				Material currentMaterial;
				if (this.skeletonRenderer.CustomMaterialOverride.TryGetValue(atlasMaterialOverride.originalMaterial, out currentMaterial) && !(currentMaterial != atlasMaterialOverride.replacementMaterial))
				{
					this.skeletonRenderer.CustomMaterialOverride.Remove(atlasMaterialOverride.originalMaterial);
				}
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000DADC File Offset: 0x0000BCDC
		private void OnEnable()
		{
			if (this.skeletonRenderer == null)
			{
				this.skeletonRenderer = base.GetComponent<SkeletonRenderer>();
			}
			if (this.skeletonRenderer == null)
			{
				Debug.LogError("skeletonRenderer == null");
				return;
			}
			this.skeletonRenderer.Initialize(false, false);
			this.SetCustomMaterialOverrides();
			this.SetCustomSlotMaterials();
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000DB35 File Offset: 0x0000BD35
		private void OnDisable()
		{
			if (this.skeletonRenderer == null)
			{
				Debug.LogError("skeletonRenderer == null");
				return;
			}
			this.RemoveCustomMaterialOverrides();
			this.RemoveCustomSlotMaterials();
		}

		// Token: 0x04000188 RID: 392
		public SkeletonRenderer skeletonRenderer;

		// Token: 0x04000189 RID: 393
		[SerializeField]
		protected List<SkeletonRendererCustomMaterials.SlotMaterialOverride> customSlotMaterials = new List<SkeletonRendererCustomMaterials.SlotMaterialOverride>();

		// Token: 0x0400018A RID: 394
		[SerializeField]
		protected List<SkeletonRendererCustomMaterials.AtlasMaterialOverride> customMaterialOverrides = new List<SkeletonRendererCustomMaterials.AtlasMaterialOverride>();

		// Token: 0x02000045 RID: 69
		[Serializable]
		public struct SlotMaterialOverride : IEquatable<SkeletonRendererCustomMaterials.SlotMaterialOverride>
		{
			// Token: 0x06000288 RID: 648 RVA: 0x0000DB7A File Offset: 0x0000BD7A
			public bool Equals(SkeletonRendererCustomMaterials.SlotMaterialOverride other)
			{
				return this.overrideDisabled == other.overrideDisabled && this.slotName == other.slotName && this.material == other.material;
			}

			// Token: 0x0400018B RID: 395
			public bool overrideDisabled;

			// Token: 0x0400018C RID: 396
			[SpineSlot("", "", false, true, false)]
			public string slotName;

			// Token: 0x0400018D RID: 397
			public Material material;
		}

		// Token: 0x02000046 RID: 70
		[Serializable]
		public struct AtlasMaterialOverride : IEquatable<SkeletonRendererCustomMaterials.AtlasMaterialOverride>
		{
			// Token: 0x06000289 RID: 649 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
			public bool Equals(SkeletonRendererCustomMaterials.AtlasMaterialOverride other)
			{
				return this.overrideDisabled == other.overrideDisabled && this.originalMaterial == other.originalMaterial && this.replacementMaterial == other.replacementMaterial;
			}

			// Token: 0x0400018E RID: 398
			public bool overrideDisabled;

			// Token: 0x0400018F RID: 399
			public Material originalMaterial;

			// Token: 0x04000190 RID: 400
			public Material replacementMaterial;
		}
	}
}
