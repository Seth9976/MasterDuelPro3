using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000009 RID: 9
	[Serializable]
	public class BlendModeMaterials
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000020 RID: 32 RVA: 0x000028CF File Offset: 0x00000ACF
		// (set) Token: 0x06000021 RID: 33 RVA: 0x000028D7 File Offset: 0x00000AD7
		public bool RequiresBlendModeMaterials
		{
			get
			{
				return this.requiresBlendModeMaterials;
			}
			set
			{
				this.requiresBlendModeMaterials = value;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000028E0 File Offset: 0x00000AE0
		public BlendMode BlendModeForMaterial(Material material)
		{
			using (List<BlendModeMaterials.ReplacementMaterial>.Enumerator enumerator = this.multiplyMaterials.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.material == material)
					{
						return BlendMode.Multiply;
					}
				}
			}
			using (List<BlendModeMaterials.ReplacementMaterial>.Enumerator enumerator = this.additiveMaterials.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.material == material)
					{
						return BlendMode.Additive;
					}
				}
			}
			using (List<BlendModeMaterials.ReplacementMaterial>.Enumerator enumerator = this.screenMaterials.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.material == material)
					{
						return BlendMode.Screen;
					}
				}
			}
			return BlendMode.Normal;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000029D8 File Offset: 0x00000BD8
		public bool UpdateBlendmodeMaterialsRequiredState(SkeletonData skeletonData)
		{
			this.requiresBlendModeMaterials = false;
			if (skeletonData == null)
			{
				return false;
			}
			List<Skin.SkinEntry> skinEntries = new List<Skin.SkinEntry>();
			SlotData[] slotsItems = skeletonData.Slots.Items;
			int slotIndex = 0;
			int slotCount = skeletonData.Slots.Count;
			while (slotIndex < slotCount)
			{
				SlotData slot = slotsItems[slotIndex];
				if (slot.BlendMode != BlendMode.Normal && (this.applyAdditiveMaterial || slot.BlendMode != BlendMode.Additive))
				{
					skinEntries.Clear();
					foreach (Skin skin in skeletonData.Skins)
					{
						skin.GetAttachments(slotIndex, skinEntries);
					}
					foreach (Skin.SkinEntry entry in skinEntries)
					{
						if (entry.Attachment is IHasTextureRegion)
						{
							this.requiresBlendModeMaterials = true;
							return true;
						}
					}
				}
				slotIndex++;
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002AEC File Offset: 0x00000CEC
		public static bool CreateAndAssignMaterials(SkeletonDataAsset skeletonDataAsset, BlendModeMaterials.TemplateMaterials templateMaterials, ref bool anyReplacementMaterialsChanged)
		{
			return BlendModeMaterials.CreateAndAssignMaterials(skeletonDataAsset, templateMaterials, ref anyReplacementMaterialsChanged, delegate(SkeletonDataAsset asset)
			{
				asset.Clear();
			}, null, new BlendModeMaterials.CreateForRegionDelegate(BlendModeMaterials.CreateForRegion));
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002B24 File Offset: 0x00000D24
		public static bool CreateAndAssignMaterials(SkeletonDataAsset skeletonDataAsset, BlendModeMaterials.TemplateMaterials templateMaterials, ref bool anyReplacementMaterialsChanged, Action<SkeletonDataAsset> clearSkeletonDataAssetFunc, Action<SkeletonDataAsset> afterAssetModifiedFunc, BlendModeMaterials.CreateForRegionDelegate createForRegionFunc)
		{
			bool anyCreationFailed = false;
			BlendModeMaterials blendModeMaterials = skeletonDataAsset.blendModeMaterials;
			bool applyAdditiveMaterial = blendModeMaterials.applyAdditiveMaterial;
			List<Skin.SkinEntry> skinEntries = new List<Skin.SkinEntry>();
			clearSkeletonDataAssetFunc(skeletonDataAsset);
			skeletonDataAsset.isUpgradingBlendModeMaterials = true;
			SkeletonData skeletonData = skeletonDataAsset.GetSkeletonData(true);
			SlotData[] slotsItems = skeletonData.Slots.Items;
			int slotIndex = 0;
			int slotCount = skeletonData.Slots.Count;
			while (slotIndex < slotCount)
			{
				SlotData slot = slotsItems[slotIndex];
				if (slot.BlendMode != BlendMode.Normal && (applyAdditiveMaterial || slot.BlendMode != BlendMode.Additive))
				{
					List<BlendModeMaterials.ReplacementMaterial> replacementMaterials = null;
					Material materialTemplate = null;
					string materialSuffix = null;
					switch (slot.BlendMode)
					{
					case BlendMode.Additive:
						replacementMaterials = blendModeMaterials.additiveMaterials;
						materialTemplate = templateMaterials.additiveTemplate;
						materialSuffix = "-Additive";
						break;
					case BlendMode.Multiply:
						replacementMaterials = blendModeMaterials.multiplyMaterials;
						materialTemplate = templateMaterials.multiplyTemplate;
						materialSuffix = "-Multiply";
						break;
					case BlendMode.Screen:
						replacementMaterials = blendModeMaterials.screenMaterials;
						materialTemplate = templateMaterials.screenTemplate;
						materialSuffix = "-Screen";
						break;
					}
					skinEntries.Clear();
					foreach (Skin skin in skeletonData.Skins)
					{
						skin.GetAttachments(slotIndex, skinEntries);
					}
					foreach (Skin.SkinEntry entry in skinEntries)
					{
						IHasTextureRegion renderableAttachment = entry.Attachment as IHasTextureRegion;
						if (renderableAttachment != null)
						{
							AtlasRegion originalRegion = (AtlasRegion)renderableAttachment.Region;
							if (originalRegion != null)
							{
								anyCreationFailed |= createForRegionFunc(ref replacementMaterials, ref anyReplacementMaterialsChanged, originalRegion, materialTemplate, materialSuffix, skeletonDataAsset);
							}
							else
							{
								Sequence sequence = renderableAttachment.Sequence;
								if (sequence != null && sequence.Regions != null)
								{
									int i = 0;
									int count = sequence.Regions.Length;
									while (i < count)
									{
										originalRegion = (AtlasRegion)sequence.Regions[i];
										anyCreationFailed |= createForRegionFunc(ref replacementMaterials, ref anyReplacementMaterialsChanged, originalRegion, materialTemplate, materialSuffix, skeletonDataAsset);
										i++;
									}
								}
							}
						}
					}
				}
				slotIndex++;
			}
			skeletonDataAsset.isUpgradingBlendModeMaterials = false;
			if (afterAssetModifiedFunc != null)
			{
				afterAssetModifiedFunc(skeletonDataAsset);
			}
			return !anyCreationFailed;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002D5C File Offset: 0x00000F5C
		protected static bool CreateForRegion(ref List<BlendModeMaterials.ReplacementMaterial> replacementMaterials, ref bool anyReplacementMaterialsChanged, AtlasRegion originalRegion, Material materialTemplate, string materialSuffix, SkeletonDataAsset skeletonDataAsset)
		{
			bool anyCreationFailed = false;
			if (!replacementMaterials.Exists((BlendModeMaterials.ReplacementMaterial replacement) => replacement.pageName == originalRegion.page.name))
			{
				BlendModeMaterials.ReplacementMaterial replacement2 = BlendModeMaterials.CreateReplacementMaterial(originalRegion, materialTemplate, materialSuffix);
				if (replacement2 != null)
				{
					replacementMaterials.Add(replacement2);
					anyReplacementMaterialsChanged = true;
				}
				else
				{
					Debug.LogError(string.Format("Failed creating blend mode Material for SkeletonData asset '{0}', atlas page '{1}', template '{2}'.", skeletonDataAsset.name, originalRegion.page.name, materialTemplate.name), skeletonDataAsset);
					anyCreationFailed = true;
				}
			}
			return anyCreationFailed;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002DE0 File Offset: 0x00000FE0
		protected static BlendModeMaterials.ReplacementMaterial CreateReplacementMaterial(AtlasRegion originalRegion, Material materialTemplate, string materialSuffix)
		{
			BlendModeMaterials.ReplacementMaterial newReplacement = new BlendModeMaterials.ReplacementMaterial();
			AtlasPage originalPage = originalRegion.page;
			Material originalMaterial = originalPage.rendererObject as Material;
			newReplacement.pageName = originalPage.name;
			Material blendModeMaterial = new Material(materialTemplate)
			{
				name = originalMaterial.name + " " + materialTemplate.name,
				mainTexture = originalMaterial.mainTexture
			};
			newReplacement.material = blendModeMaterial;
			if (newReplacement.material)
			{
				return newReplacement;
			}
			return null;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002E58 File Offset: 0x00001058
		public void ApplyMaterials(SkeletonData skeletonData)
		{
			if (skeletonData == null)
			{
				throw new ArgumentNullException("skeletonData");
			}
			if (!this.requiresBlendModeMaterials)
			{
				return;
			}
			List<Skin.SkinEntry> skinEntries = new List<Skin.SkinEntry>();
			SlotData[] slotsItems = skeletonData.Slots.Items;
			int slotIndex = 0;
			int slotCount = skeletonData.Slots.Count;
			while (slotIndex < slotCount)
			{
				SlotData slot = slotsItems[slotIndex];
				if (slot.BlendMode != BlendMode.Normal && (this.applyAdditiveMaterial || slot.BlendMode != BlendMode.Additive))
				{
					List<BlendModeMaterials.ReplacementMaterial> replacementMaterials = null;
					switch (slot.BlendMode)
					{
					case BlendMode.Additive:
						replacementMaterials = this.additiveMaterials;
						break;
					case BlendMode.Multiply:
						replacementMaterials = this.multiplyMaterials;
						break;
					case BlendMode.Screen:
						replacementMaterials = this.screenMaterials;
						break;
					}
					if (replacementMaterials != null)
					{
						skinEntries.Clear();
						foreach (Skin skin in skeletonData.Skins)
						{
							skin.GetAttachments(slotIndex, skinEntries);
						}
						foreach (Skin.SkinEntry entry in skinEntries)
						{
							IHasTextureRegion renderableAttachment = entry.Attachment as IHasTextureRegion;
							if (renderableAttachment != null)
							{
								if (renderableAttachment.Region != null)
								{
									renderableAttachment.Region = this.CloneAtlasRegionWithMaterial((AtlasRegion)renderableAttachment.Region, replacementMaterials);
								}
								else if (renderableAttachment.Sequence != null)
								{
									TextureRegion[] regions = renderableAttachment.Sequence.Regions;
									for (int i = 0; i < regions.Length; i++)
									{
										regions[i] = this.CloneAtlasRegionWithMaterial((AtlasRegion)regions[i], replacementMaterials);
									}
								}
							}
						}
					}
				}
				slotIndex++;
			}
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000301C File Offset: 0x0000121C
		protected AtlasRegion CloneAtlasRegionWithMaterial(AtlasRegion originalRegion, List<BlendModeMaterials.ReplacementMaterial> replacementMaterials)
		{
			AtlasRegion newRegion = originalRegion.Clone();
			Material material = null;
			foreach (BlendModeMaterials.ReplacementMaterial replacement in replacementMaterials)
			{
				if (replacement.pageName == originalRegion.page.name)
				{
					material = replacement.material;
					break;
				}
			}
			AtlasPage newPage = originalRegion.page.Clone();
			newPage.rendererObject = material;
			newRegion.page = newPage;
			return newRegion;
		}

		// Token: 0x04000015 RID: 21
		public const string MATERIAL_SUFFIX_MULTIPLY = "-Multiply";

		// Token: 0x04000016 RID: 22
		public const string MATERIAL_SUFFIX_SCREEN = "-Screen";

		// Token: 0x04000017 RID: 23
		public const string MATERIAL_SUFFIX_ADDITIVE = "-Additive";

		// Token: 0x04000018 RID: 24
		[SerializeField]
		[HideInInspector]
		protected bool requiresBlendModeMaterials;

		// Token: 0x04000019 RID: 25
		public bool applyAdditiveMaterial;

		// Token: 0x0400001A RID: 26
		public List<BlendModeMaterials.ReplacementMaterial> additiveMaterials = new List<BlendModeMaterials.ReplacementMaterial>();

		// Token: 0x0400001B RID: 27
		public List<BlendModeMaterials.ReplacementMaterial> multiplyMaterials = new List<BlendModeMaterials.ReplacementMaterial>();

		// Token: 0x0400001C RID: 28
		public List<BlendModeMaterials.ReplacementMaterial> screenMaterials = new List<BlendModeMaterials.ReplacementMaterial>();

		// Token: 0x0200000A RID: 10
		[Serializable]
		public class ReplacementMaterial
		{
			// Token: 0x0400001D RID: 29
			public string pageName;

			// Token: 0x0400001E RID: 30
			public Material material;
		}

		// Token: 0x0200000B RID: 11
		[Serializable]
		public class TemplateMaterials
		{
			// Token: 0x0400001F RID: 31
			public Material additiveTemplate;

			// Token: 0x04000020 RID: 32
			public Material multiplyTemplate;

			// Token: 0x04000021 RID: 33
			public Material screenTemplate;
		}

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x0600002E RID: 46
		public delegate bool CreateForRegionDelegate(ref List<BlendModeMaterials.ReplacementMaterial> replacementMaterials, ref bool anyReplacementMaterialsChanged, AtlasRegion originalRegion, Material materialTemplate, string materialSuffix, SkeletonDataAsset skeletonDataAsset);
	}
}
