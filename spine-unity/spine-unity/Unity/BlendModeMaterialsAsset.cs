using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000067 RID: 103
	[CreateAssetMenu(menuName = "Spine/SkeletonData Modifiers/Blend Mode Materials", order = 200)]
	public class BlendModeMaterialsAsset : SkeletonDataModifierAsset
	{
		// Token: 0x0600032C RID: 812 RVA: 0x00012AC8 File Offset: 0x00010CC8
		public override void Apply(SkeletonData skeletonData)
		{
			BlendModeMaterialsAsset.ApplyMaterials(skeletonData, this.multiplyMaterialTemplate, this.screenMaterialTemplate, this.additiveMaterialTemplate, this.applyAdditiveMaterial);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x00012AE8 File Offset: 0x00010CE8
		public static void ApplyMaterials(SkeletonData skeletonData, Material multiplyTemplate, Material screenTemplate, Material additiveTemplate, bool includeAdditiveSlots)
		{
			if (skeletonData == null)
			{
				throw new ArgumentNullException("skeletonData");
			}
			using (BlendModeMaterialsAsset.AtlasMaterialCache materialCache = new BlendModeMaterialsAsset.AtlasMaterialCache())
			{
				List<Skin.SkinEntry> entryBuffer = new List<Skin.SkinEntry>();
				SlotData[] slotsItems = skeletonData.Slots.Items;
				int slotIndex = 0;
				int slotCount = skeletonData.Slots.Count;
				while (slotIndex < slotCount)
				{
					SlotData slot = slotsItems[slotIndex];
					if (slot.BlendMode != BlendMode.Normal && (includeAdditiveSlots || slot.BlendMode != BlendMode.Additive))
					{
						entryBuffer.Clear();
						foreach (Skin skin in skeletonData.Skins)
						{
							skin.GetAttachments(slotIndex, entryBuffer);
						}
						Material templateMaterial = null;
						switch (slot.BlendMode)
						{
						case BlendMode.Additive:
							templateMaterial = additiveTemplate;
							break;
						case BlendMode.Multiply:
							templateMaterial = multiplyTemplate;
							break;
						case BlendMode.Screen:
							templateMaterial = screenTemplate;
							break;
						}
						if (!(templateMaterial == null))
						{
							foreach (Skin.SkinEntry entry in entryBuffer)
							{
								IHasTextureRegion renderableAttachment = entry.Attachment as IHasTextureRegion;
								if (renderableAttachment != null)
								{
									renderableAttachment.Region = materialCache.CloneAtlasRegionWithMaterial((AtlasRegion)renderableAttachment.Region, templateMaterial);
								}
							}
						}
					}
					slotIndex++;
				}
			}
		}

		// Token: 0x04000210 RID: 528
		public Material multiplyMaterialTemplate;

		// Token: 0x04000211 RID: 529
		public Material screenMaterialTemplate;

		// Token: 0x04000212 RID: 530
		public Material additiveMaterialTemplate;

		// Token: 0x04000213 RID: 531
		public bool applyAdditiveMaterial = true;

		// Token: 0x02000068 RID: 104
		private class AtlasMaterialCache : IDisposable
		{
			// Token: 0x0600032F RID: 815 RVA: 0x00012C93 File Offset: 0x00010E93
			public AtlasRegion CloneAtlasRegionWithMaterial(AtlasRegion originalRegion, Material materialTemplate)
			{
				AtlasRegion atlasRegion = originalRegion.Clone();
				atlasRegion.page = this.GetAtlasPageWithMaterial(originalRegion.page, materialTemplate);
				return atlasRegion;
			}

			// Token: 0x06000330 RID: 816 RVA: 0x00012CB0 File Offset: 0x00010EB0
			private AtlasPage GetAtlasPageWithMaterial(AtlasPage originalPage, Material materialTemplate)
			{
				if (originalPage == null)
				{
					throw new ArgumentNullException("originalPage");
				}
				AtlasPage newPage = null;
				KeyValuePair<AtlasPage, Material> key = new KeyValuePair<AtlasPage, Material>(originalPage, materialTemplate);
				this.cache.TryGetValue(key, out newPage);
				if (newPage == null)
				{
					newPage = originalPage.Clone();
					Material originalMaterial = originalPage.rendererObject as Material;
					newPage.rendererObject = new Material(materialTemplate)
					{
						name = originalMaterial.name + " " + materialTemplate.name,
						mainTexture = originalMaterial.mainTexture
					};
					this.cache.Add(key, newPage);
				}
				return newPage;
			}

			// Token: 0x06000331 RID: 817 RVA: 0x00012D3D File Offset: 0x00010F3D
			public void Dispose()
			{
				this.cache.Clear();
			}

			// Token: 0x04000214 RID: 532
			private readonly Dictionary<KeyValuePair<AtlasPage, Material>, AtlasPage> cache = new Dictionary<KeyValuePair<AtlasPage, Material>, AtlasPage>();
		}
	}
}
