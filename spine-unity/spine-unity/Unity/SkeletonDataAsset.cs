using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000013 RID: 19
	[CreateAssetMenu(fileName = "New SkeletonDataAsset", menuName = "Spine/SkeletonData Asset")]
	public class SkeletonDataAsset : ScriptableObject
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00003537 File Offset: 0x00001737
		public bool IsLoaded
		{
			get
			{
				return this.skeletonData != null;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003542 File Offset: 0x00001742
		private void Reset()
		{
			this.Clear();
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000354A File Offset: 0x0000174A
		public static SkeletonDataAsset CreateRuntimeInstance(TextAsset skeletonDataFile, AtlasAssetBase atlasAsset, bool initialize, float scale = 0.01f)
		{
			return SkeletonDataAsset.CreateRuntimeInstance(skeletonDataFile, new AtlasAssetBase[] { atlasAsset }, initialize, scale);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003560 File Offset: 0x00001760
		public static SkeletonDataAsset CreateRuntimeInstance(TextAsset skeletonDataFile, AtlasAssetBase[] atlasAssets, bool initialize, float scale = 0.01f)
		{
			SkeletonDataAsset skeletonDataAsset = ScriptableObject.CreateInstance<SkeletonDataAsset>();
			skeletonDataAsset.Clear();
			skeletonDataAsset.skeletonJSON = skeletonDataFile;
			skeletonDataAsset.atlasAssets = atlasAssets;
			skeletonDataAsset.scale = scale;
			if (initialize)
			{
				skeletonDataAsset.GetSkeletonData(true);
			}
			return skeletonDataAsset;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000359C File Offset: 0x0000179C
		public void SetupRuntimeBlendModeMaterials(bool applyAdditiveMaterial, BlendModeMaterials.TemplateMaterials templateMaterials)
		{
			this.blendModeMaterials.applyAdditiveMaterial = applyAdditiveMaterial;
			this.blendModeMaterials.UpdateBlendmodeMaterialsRequiredState(this.GetSkeletonData(true));
			bool anyMaterialsChanged = false;
			BlendModeMaterials.CreateAndAssignMaterials(this, templateMaterials, ref anyMaterialsChanged);
			this.Clear();
			this.GetSkeletonData(true);
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000035E2 File Offset: 0x000017E2
		public void Clear()
		{
			this.skeletonData = null;
			this.stateData = null;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000035F2 File Offset: 0x000017F2
		public AnimationStateData GetAnimationStateData()
		{
			if (this.stateData != null)
			{
				return this.stateData;
			}
			this.GetSkeletonData(false);
			return this.stateData;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003614 File Offset: 0x00001814
		public SkeletonData GetSkeletonData(bool quiet)
		{
			if (this.skeletonJSON == null)
			{
				if (!quiet)
				{
					Debug.LogError("Skeleton JSON file not set for SkeletonData asset: " + base.name, this);
				}
				this.Clear();
				return null;
			}
			if (this.skeletonData != null)
			{
				return this.skeletonData;
			}
			Atlas[] atlasArray = this.GetAtlasArray();
			AttachmentLoader attachmentLoader3;
			if (atlasArray.Length != 0)
			{
				AttachmentLoader attachmentLoader2 = new AtlasAttachmentLoader(atlasArray);
				attachmentLoader3 = attachmentLoader2;
			}
			else
			{
				AttachmentLoader attachmentLoader2 = new RegionlessAttachmentLoader();
				attachmentLoader3 = attachmentLoader2;
			}
			AttachmentLoader attachmentLoader = attachmentLoader3;
			float skeletonDataScale = this.scale;
			bool hasBinaryExtension = this.skeletonJSON.name.ToLower().Contains(".skel");
			SkeletonData loadedSkeletonData = null;
			try
			{
				if (hasBinaryExtension)
				{
					loadedSkeletonData = SkeletonDataAsset.ReadSkeletonData(this.skeletonJSON.bytes, attachmentLoader, skeletonDataScale);
				}
				else
				{
					loadedSkeletonData = SkeletonDataAsset.ReadSkeletonData(this.skeletonJSON.text, attachmentLoader, skeletonDataScale);
				}
			}
			catch (Exception ex)
			{
				if (!quiet)
				{
					Debug.LogError(string.Concat(new string[] { "Error reading skeleton JSON file for SkeletonData asset: ", base.name, "\n", ex.Message, "\n", ex.StackTrace }), this.skeletonJSON);
				}
			}
			if (loadedSkeletonData == null)
			{
				return null;
			}
			if (this.skeletonDataModifiers != null)
			{
				foreach (SkeletonDataModifierAsset modifier in this.skeletonDataModifiers)
				{
					if (modifier != null && (!this.isUpgradingBlendModeMaterials || !(modifier is BlendModeMaterialsAsset)))
					{
						modifier.Apply(loadedSkeletonData);
					}
				}
			}
			if (!this.isUpgradingBlendModeMaterials)
			{
				this.blendModeMaterials.ApplyMaterials(loadedSkeletonData);
			}
			this.InitializeWithData(loadedSkeletonData);
			return this.skeletonData;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000037C8 File Offset: 0x000019C8
		internal void InitializeWithData(SkeletonData sd)
		{
			this.skeletonData = sd;
			this.stateData = new AnimationStateData(this.skeletonData);
			this.FillStateData(false);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000037EC File Offset: 0x000019EC
		public void FillStateData(bool quiet = false)
		{
			if (this.stateData != null)
			{
				this.stateData.DefaultMix = this.defaultMix;
				int i = 0;
				int j = this.fromAnimation.Length;
				while (i < j)
				{
					string fromAnimationName = this.fromAnimation[i];
					string toAnimationName = this.toAnimation[i];
					if (fromAnimationName.Length != 0 && toAnimationName.Length != 0)
					{
						this.stateData.SetMix(fromAnimationName, toAnimationName, this.duration[i]);
					}
					i++;
				}
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003860 File Offset: 0x00001A60
		internal Atlas[] GetAtlasArray()
		{
			List<Atlas> returnList = new List<Atlas>(this.atlasAssets.Length);
			for (int i = 0; i < this.atlasAssets.Length; i++)
			{
				AtlasAssetBase aa = this.atlasAssets[i];
				if (!(aa == null))
				{
					Atlas a = aa.GetAtlas(false);
					if (a != null)
					{
						returnList.Add(a);
					}
				}
			}
			return returnList.ToArray();
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000038B8 File Offset: 0x00001AB8
		internal static SkeletonData ReadSkeletonData(byte[] bytes, AttachmentLoader attachmentLoader, float scale)
		{
			SkeletonData skeletonData;
			using (MemoryStream input = new MemoryStream(bytes))
			{
				skeletonData = new SkeletonBinary(attachmentLoader)
				{
					Scale = scale
				}.ReadSkeletonData(input);
			}
			return skeletonData;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003900 File Offset: 0x00001B00
		internal static SkeletonData ReadSkeletonData(string text, AttachmentLoader attachmentLoader, float scale)
		{
			StringReader input = new StringReader(text);
			return new SkeletonJson(attachmentLoader)
			{
				Scale = scale
			}.ReadSkeletonData(input);
		}

		// Token: 0x0400002F RID: 47
		public AtlasAssetBase[] atlasAssets = new AtlasAssetBase[0];

		// Token: 0x04000030 RID: 48
		public float scale = 0.01f;

		// Token: 0x04000031 RID: 49
		public TextAsset skeletonJSON;

		// Token: 0x04000032 RID: 50
		public bool isUpgradingBlendModeMaterials;

		// Token: 0x04000033 RID: 51
		public BlendModeMaterials blendModeMaterials = new BlendModeMaterials();

		// Token: 0x04000034 RID: 52
		[Tooltip("Use SkeletonDataModifierAssets to apply changes to the SkeletonData after being loaded, such as apply blend mode Materials to Attachments under slots with special blend modes.")]
		public List<SkeletonDataModifierAsset> skeletonDataModifiers = new List<SkeletonDataModifierAsset>();

		// Token: 0x04000035 RID: 53
		[SpineAnimation("", "", false, false, false)]
		public string[] fromAnimation = new string[0];

		// Token: 0x04000036 RID: 54
		[SpineAnimation("", "", false, false, false)]
		public string[] toAnimation = new string[0];

		// Token: 0x04000037 RID: 55
		public float[] duration = new float[0];

		// Token: 0x04000038 RID: 56
		public float defaultMix;

		// Token: 0x04000039 RID: 57
		public RuntimeAnimatorController controller;

		// Token: 0x0400003A RID: 58
		private SkeletonData skeletonData;

		// Token: 0x0400003B RID: 59
		private AnimationStateData stateData;
	}
}
