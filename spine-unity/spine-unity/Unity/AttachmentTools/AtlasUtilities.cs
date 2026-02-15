using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Spine.Unity.AttachmentTools
{
	// Token: 0x0200007F RID: 127
	public static class AtlasUtilities
	{
		// Token: 0x06000394 RID: 916 RVA: 0x00013FE0 File Offset: 0x000121E0
		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
		private static void Init()
		{
			AtlasUtilities.ClearCache();
		}

		// Token: 0x06000395 RID: 917 RVA: 0x00013FE7 File Offset: 0x000121E7
		public static AtlasRegion ToAtlasRegion(this Texture2D t, Material materialPropertySource, float scale = 0.01f)
		{
			return t.ToAtlasRegion(materialPropertySource.shader, scale, materialPropertySource);
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00013FF8 File Offset: 0x000121F8
		public static AtlasRegion ToAtlasRegion(this Texture2D t, Shader shader, float scale = 0.01f, Material materialPropertySource = null)
		{
			Material material = new Material(shader);
			if (materialPropertySource != null)
			{
				material.CopyPropertiesFromMaterial(materialPropertySource);
				material.shaderKeywords = materialPropertySource.shaderKeywords;
			}
			material.mainTexture = t;
			AtlasPage page = material.ToSpineAtlasPage();
			float width = (float)t.width;
			float height = (float)t.height;
			AtlasRegion atlasRegion = new AtlasRegion();
			atlasRegion.name = t.name;
			Vector2 boundsMin = Vector2.zero;
			Vector2 boundsMax = new Vector2(width, height) * scale;
			atlasRegion.width = (int)width;
			atlasRegion.originalWidth = (int)width;
			atlasRegion.height = (int)height;
			atlasRegion.originalHeight = (int)height;
			atlasRegion.offsetX = width * (0.5f - AtlasUtilities.InverseLerp(boundsMin.x, boundsMax.x, 0f));
			atlasRegion.offsetY = height * (0.5f - AtlasUtilities.InverseLerp(boundsMin.y, boundsMax.y, 0f));
			atlasRegion.u = 0f;
			atlasRegion.v = 1f;
			atlasRegion.u2 = 1f;
			atlasRegion.v2 = 0f;
			atlasRegion.x = 0;
			atlasRegion.y = 0;
			atlasRegion.page = page;
			return atlasRegion;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0001411A File Offset: 0x0001231A
		public static AtlasRegion ToAtlasRegionPMAClone(this Texture2D t, Material materialPropertySource, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false)
		{
			return t.ToAtlasRegionPMAClone(materialPropertySource.shader, textureFormat, mipmaps, materialPropertySource);
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0001412C File Offset: 0x0001232C
		public static AtlasRegion ToAtlasRegionPMAClone(this Texture2D t, Shader shader, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, Material materialPropertySource = null)
		{
			Material material = new Material(shader);
			if (materialPropertySource != null)
			{
				material.CopyPropertiesFromMaterial(materialPropertySource);
				material.shaderKeywords = materialPropertySource.shaderKeywords;
			}
			Texture2D newTexture = t.GetClone(textureFormat, mipmaps, false, true);
			newTexture.name = t.name + "-pma-";
			material.name = t.name + shader.name;
			material.mainTexture = newTexture;
			AtlasPage page = material.ToSpineAtlasPage();
			AtlasRegion atlasRegion = newTexture.ToAtlasRegion(shader, 0.01f, null);
			atlasRegion.page = page;
			return atlasRegion;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000141BC File Offset: 0x000123BC
		public static AtlasPage ToSpineAtlasPage(this Material m)
		{
			AtlasPage newPage = new AtlasPage
			{
				rendererObject = m,
				name = m.name
			};
			Texture t = m.mainTexture;
			if (t != null)
			{
				newPage.width = t.width;
				newPage.height = t.height;
			}
			return newPage;
		}

		// Token: 0x0600039A RID: 922 RVA: 0x0001420B File Offset: 0x0001240B
		public static AtlasRegion ToAtlasRegion(this Sprite s, AtlasPage page)
		{
			if (page == null)
			{
				throw new ArgumentNullException("page", "page cannot be null. AtlasPage determines which texture region belongs and how it should be rendered. You can use material.ToSpineAtlasPage() to get a shareable AtlasPage from a Material, or use the sprite.ToAtlasRegion(material) overload.");
			}
			AtlasRegion atlasRegion = s.ToAtlasRegion(false);
			atlasRegion.page = page;
			return atlasRegion;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0001422E File Offset: 0x0001242E
		public static AtlasRegion ToAtlasRegion(this Sprite s, Material material)
		{
			AtlasRegion atlasRegion = s.ToAtlasRegion(false);
			atlasRegion.page = material.ToSpineAtlasPage();
			return atlasRegion;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00014243 File Offset: 0x00012443
		public static AtlasRegion ToAtlasRegionPMAClone(this Sprite s, Material materialPropertySource, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false)
		{
			return s.ToAtlasRegionPMAClone(materialPropertySource.shader, textureFormat, mipmaps, materialPropertySource);
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00014254 File Offset: 0x00012454
		public static AtlasRegion ToAtlasRegionPMAClone(this Sprite s, Shader shader, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, Material materialPropertySource = null)
		{
			Material material = new Material(shader);
			if (materialPropertySource != null)
			{
				material.CopyPropertiesFromMaterial(materialPropertySource);
				material.shaderKeywords = materialPropertySource.shaderKeywords;
			}
			Texture2D tex = s.ToTexture(textureFormat, mipmaps, false, true);
			tex.name = s.name + "-pma-";
			material.name = tex.name + shader.name;
			material.mainTexture = tex;
			AtlasPage page = material.ToSpineAtlasPage();
			AtlasRegion atlasRegion = s.ToAtlasRegion(true);
			atlasRegion.page = page;
			return atlasRegion;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000142DC File Offset: 0x000124DC
		internal static AtlasRegion ToAtlasRegion(this Sprite s, bool isolatedTexture = false)
		{
			AtlasRegion region = new AtlasRegion();
			region.name = s.name;
			region.index = -1;
			region.degrees = ((s.packed && s.packingRotation != SpritePackingRotation.None) ? 90 : 0);
			Bounds bounds = s.bounds;
			Vector2 boundsMin = bounds.min;
			Vector2 boundsMax = bounds.max;
			Rect spineRect = s.textureRect.SpineUnityFlipRect(s.texture.height);
			Rect originalRect = s.rect;
			region.width = (int)spineRect.width;
			region.originalWidth = (int)originalRect.width;
			region.height = (int)spineRect.height;
			region.originalHeight = (int)originalRect.height;
			region.offsetX = s.textureRectOffset.x + spineRect.width * (0.5f - AtlasUtilities.InverseLerp(boundsMin.x, boundsMax.x, 0f));
			region.offsetY = s.textureRectOffset.y + spineRect.height * (0.5f - AtlasUtilities.InverseLerp(boundsMin.y, boundsMax.y, 0f));
			if (isolatedTexture)
			{
				region.u = 0f;
				region.v = 1f;
				region.u2 = 1f;
				region.v2 = 0f;
				region.x = 0;
				region.y = 0;
			}
			else
			{
				Texture2D tex = s.texture;
				Rect uvRect = AtlasUtilities.TextureRectToUVRect(s.textureRect, tex.width, tex.height);
				region.u = uvRect.xMin;
				region.v = uvRect.yMax;
				region.u2 = uvRect.xMax;
				region.v2 = uvRect.yMin;
				region.x = (int)spineRect.x;
				region.y = (int)spineRect.y;
			}
			return region;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x000144B8 File Offset: 0x000126B8
		public static void GetRepackedAttachments(List<Attachment> sourceAttachments, List<Attachment> outputAttachments, Material materialPropertySource, out Material outputMaterial, out Texture2D outputTexture, int maxAtlasSize = 1024, int padding = 2, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, string newAssetName = "Repacked Attachments", bool clearCache = false, bool useOriginalNonrenderables = true, int[] additionalTexturePropertyIDsToCopy = null, Texture2D[] additionalOutputTextures = null, TextureFormat[] additionalTextureFormats = null, bool[] additionalTextureIsLinear = null)
		{
			Shader shader = ((materialPropertySource == null) ? Shader.Find("Spine/Skeleton") : materialPropertySource.shader);
			AtlasUtilities.GetRepackedAttachments(sourceAttachments, outputAttachments, shader, out outputMaterial, out outputTexture, maxAtlasSize, padding, textureFormat, mipmaps, newAssetName, materialPropertySource, clearCache, useOriginalNonrenderables, additionalTexturePropertyIDsToCopy, additionalOutputTextures, additionalTextureFormats, additionalTextureIsLinear);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00014504 File Offset: 0x00012704
		public static void GetRepackedAttachments(List<Attachment> sourceAttachments, List<Attachment> outputAttachments, Shader shader, out Material outputMaterial, out Texture2D outputTexture, int maxAtlasSize = 1024, int padding = 2, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, string newAssetName = "Repacked Attachments", Material materialPropertySource = null, bool clearCache = false, bool useOriginalNonrenderables = true, int[] additionalTexturePropertyIDsToCopy = null, Texture2D[] additionalOutputTextures = null, TextureFormat[] additionalTextureFormats = null, bool[] additionalTextureIsLinear = null)
		{
			if (sourceAttachments == null)
			{
				throw new ArgumentNullException("sourceAttachments");
			}
			if (outputAttachments == null)
			{
				throw new ArgumentNullException("outputAttachments");
			}
			outputTexture = null;
			if (additionalTexturePropertyIDsToCopy != null && additionalTextureIsLinear == null)
			{
				additionalTextureIsLinear = new bool[additionalTexturePropertyIDsToCopy.Length];
				for (int i = 0; i < additionalTextureIsLinear.Length; i++)
				{
					additionalTextureIsLinear[i] = true;
				}
			}
			AtlasUtilities.existingRegions.Clear();
			AtlasUtilities.regionIndices.Clear();
			int numTextureParamsToRepack = 1 + ((additionalTexturePropertyIDsToCopy == null) ? 0 : additionalTexturePropertyIDsToCopy.Length);
			additionalOutputTextures = ((additionalTexturePropertyIDsToCopy == null) ? null : new Texture2D[additionalTexturePropertyIDsToCopy.Length]);
			if (AtlasUtilities.texturesToPackAtParam.Length < numTextureParamsToRepack)
			{
				Array.Resize<List<Texture2D>>(ref AtlasUtilities.texturesToPackAtParam, numTextureParamsToRepack);
			}
			for (int j = 0; j < numTextureParamsToRepack; j++)
			{
				if (AtlasUtilities.texturesToPackAtParam[j] != null)
				{
					AtlasUtilities.texturesToPackAtParam[j].Clear();
				}
				else
				{
					AtlasUtilities.texturesToPackAtParam[j] = new List<Texture2D>();
				}
			}
			AtlasUtilities.originalRegions.Clear();
			if (sourceAttachments != outputAttachments)
			{
				outputAttachments.Clear();
				outputAttachments.AddRange(sourceAttachments);
			}
			int newRegionIndex = 0;
			int attachmentIndex = 0;
			int k = sourceAttachments.Count;
			while (attachmentIndex < k)
			{
				Attachment originalAttachment = sourceAttachments[attachmentIndex];
				if (originalAttachment is IHasTextureRegion)
				{
					MeshAttachment originalMeshAttachment = originalAttachment as MeshAttachment;
					IHasTextureRegion originalTextureAttachment = (IHasTextureRegion)originalAttachment;
					Attachment newAttachment = ((originalTextureAttachment.Sequence != null) ? originalAttachment : ((originalMeshAttachment != null) ? originalMeshAttachment.NewLinkedMesh() : originalAttachment.Copy()));
					IHasTextureRegion newTextureAttachment = (IHasTextureRegion)newAttachment;
					AtlasRegion region = newTextureAttachment.Region as AtlasRegion;
					if (region == null && originalTextureAttachment.Sequence != null)
					{
						region = (AtlasRegion)originalTextureAttachment.Sequence.Regions[0];
					}
					int existingIndex;
					if (AtlasUtilities.existingRegions.TryGetValue(region, out existingIndex))
					{
						AtlasUtilities.regionIndices.Add(existingIndex);
					}
					else
					{
						AtlasUtilities.existingRegions.Add(region, newRegionIndex);
						Sequence originalSequence = originalTextureAttachment.Sequence;
						if (originalSequence != null)
						{
							newTextureAttachment.Sequence = new Sequence(originalSequence);
							int l = 0;
							int regionCount = originalSequence.Regions.Length;
							while (l < regionCount)
							{
								AtlasRegion sequenceRegion = (AtlasRegion)originalSequence.Regions[l];
								AtlasUtilities.AddRegionTexturesToPack(numTextureParamsToRepack, sequenceRegion, textureFormat, mipmaps, additionalTextureFormats, additionalTexturePropertyIDsToCopy, additionalTextureIsLinear);
								AtlasUtilities.originalRegions.Add(sequenceRegion);
								AtlasUtilities.regionIndices.Add(newRegionIndex);
								newRegionIndex++;
								l++;
							}
						}
						else
						{
							AtlasUtilities.AddRegionTexturesToPack(numTextureParamsToRepack, region, textureFormat, mipmaps, additionalTextureFormats, additionalTexturePropertyIDsToCopy, additionalTextureIsLinear);
							AtlasUtilities.originalRegions.Add(region);
							AtlasUtilities.regionIndices.Add(newRegionIndex);
							newRegionIndex++;
						}
					}
					outputAttachments[attachmentIndex] = newAttachment;
				}
				else
				{
					outputAttachments[attachmentIndex] = (useOriginalNonrenderables ? originalAttachment : originalAttachment.Copy());
					AtlasUtilities.regionIndices.Add(-1);
				}
				attachmentIndex++;
			}
			Material newMaterial = new Material(shader);
			if (materialPropertySource != null)
			{
				newMaterial.CopyPropertiesFromMaterial(materialPropertySource);
				newMaterial.shaderKeywords = materialPropertySource.shaderKeywords;
			}
			newMaterial.name = newAssetName;
			Rect[] rects = null;
			for (int m = 0; m < numTextureParamsToRepack; m++)
			{
				Texture2D newTexture = new Texture2D(maxAtlasSize, maxAtlasSize, (m > 0 && additionalTextureFormats != null && m - 1 < additionalTextureFormats.Length) ? additionalTextureFormats[m - 1] : textureFormat, mipmaps, m > 0 && additionalTextureIsLinear[m - 1]);
				newTexture.mipMapBias = -0.5f;
				List<Texture2D> texturesToPack = AtlasUtilities.texturesToPackAtParam[m];
				if (texturesToPack.Count > 0)
				{
					Texture2D sourceTexture = texturesToPack[0];
					newTexture.CopyTextureAttributesFrom(sourceTexture);
				}
				newTexture.name = newAssetName;
				Rect[] rectsForTexParam = newTexture.PackTextures(texturesToPack.ToArray(), padding, maxAtlasSize);
				if (m == 0)
				{
					rects = rectsForTexParam;
					newMaterial.mainTexture = newTexture;
					outputTexture = newTexture;
				}
				else
				{
					newMaterial.SetTexture(additionalTexturePropertyIDsToCopy[m - 1], newTexture);
					additionalOutputTextures[m - 1] = newTexture;
				}
			}
			AtlasPage page = newMaterial.ToSpineAtlasPage();
			page.name = newAssetName;
			AtlasUtilities.repackedRegions.Clear();
			int n = 0;
			int n2 = AtlasUtilities.originalRegions.Count;
			while (n < n2)
			{
				AtlasRegion oldRegion = AtlasUtilities.originalRegions[n];
				AtlasRegion newRegion = AtlasUtilities.UVRectToAtlasRegion(rects[n], oldRegion, page);
				AtlasUtilities.repackedRegions.Add(newRegion);
				n++;
			}
			int attachmentIndex2 = 0;
			int repackedIndex = 0;
			int n3 = outputAttachments.Count;
			while (attachmentIndex2 < n3)
			{
				IHasTextureRegion textureAttachment = outputAttachments[attachmentIndex2] as IHasTextureRegion;
				if (textureAttachment != null)
				{
					if (textureAttachment.Sequence != null)
					{
						TextureRegion[] regions = textureAttachment.Sequence.Regions;
						textureAttachment.Region = AtlasUtilities.repackedRegions[AtlasUtilities.regionIndices[repackedIndex]];
						int r = 0;
						int regionCount2 = regions.Length;
						while (r < regionCount2)
						{
							regions[r] = AtlasUtilities.repackedRegions[AtlasUtilities.regionIndices[repackedIndex++]];
							r++;
						}
						repackedIndex--;
					}
					else
					{
						textureAttachment.Region = AtlasUtilities.repackedRegions[AtlasUtilities.regionIndices[repackedIndex]];
					}
					textureAttachment.UpdateRegion();
				}
				attachmentIndex2++;
				repackedIndex++;
			}
			if (clearCache)
			{
				AtlasUtilities.ClearCache();
			}
			outputMaterial = newMaterial;
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x000149E0 File Offset: 0x00012BE0
		private static void AddRegionTexturesToPack(int numTextureParamsToRepack, AtlasRegion region, TextureFormat textureFormat, bool mipmaps, TextureFormat[] additionalTextureFormats, int[] additionalTexturePropertyIDsToCopy, bool[] additionalTextureIsLinear)
		{
			for (int i = 0; i < numTextureParamsToRepack; i++)
			{
				Texture2D regionTexture = ((i == 0) ? region.ToTexture(textureFormat, mipmaps, 0, false, false) : region.ToTexture((additionalTextureFormats != null && i - 1 < additionalTextureFormats.Length) ? additionalTextureFormats[i - 1] : textureFormat, mipmaps, additionalTexturePropertyIDsToCopy[i - 1], additionalTextureIsLinear[i - 1], false));
				AtlasUtilities.texturesToPackAtParam[i].Add(regionTexture);
			}
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00014A44 File Offset: 0x00012C44
		public static Skin GetRepackedSkin(this Skin o, string newName, Material materialPropertySource, out Material outputMaterial, out Texture2D outputTexture, int maxAtlasSize = 1024, int padding = 2, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, bool useOriginalNonrenderables = true, bool clearCache = false, int[] additionalTexturePropertyIDsToCopy = null, Texture2D[] additionalOutputTextures = null, TextureFormat[] additionalTextureFormats = null, bool[] additionalTextureIsLinear = null)
		{
			return o.GetRepackedSkin(newName, materialPropertySource.shader, out outputMaterial, out outputTexture, maxAtlasSize, padding, textureFormat, mipmaps, materialPropertySource, clearCache, useOriginalNonrenderables, additionalTexturePropertyIDsToCopy, additionalOutputTextures, additionalTextureFormats, additionalTextureIsLinear);
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00014A78 File Offset: 0x00012C78
		public static Skin GetRepackedSkin(this Skin o, string newName, Shader shader, out Material outputMaterial, out Texture2D outputTexture, int maxAtlasSize = 1024, int padding = 2, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, Material materialPropertySource = null, bool clearCache = false, bool useOriginalNonrenderables = true, int[] additionalTexturePropertyIDsToCopy = null, Texture2D[] additionalOutputTextures = null, TextureFormat[] additionalTextureFormats = null, bool[] additionalTextureIsLinear = null)
		{
			outputTexture = null;
			if (o == null)
			{
				throw new NullReferenceException("Skin was null");
			}
			ICollection<Skin.SkinEntry> skinAttachments = o.Attachments;
			Skin newSkin = new Skin(newName);
			newSkin.Bones.AddRange(o.Bones);
			newSkin.Constraints.AddRange(o.Constraints);
			AtlasUtilities.inoutAttachments.Clear();
			foreach (Skin.SkinEntry entry in skinAttachments)
			{
				AtlasUtilities.inoutAttachments.Add(entry.Attachment);
			}
			AtlasUtilities.GetRepackedAttachments(AtlasUtilities.inoutAttachments, AtlasUtilities.inoutAttachments, materialPropertySource, out outputMaterial, out outputTexture, maxAtlasSize, padding, textureFormat, mipmaps, newName, clearCache, useOriginalNonrenderables, additionalTexturePropertyIDsToCopy, additionalOutputTextures, additionalTextureFormats, additionalTextureIsLinear);
			int i = 0;
			foreach (Skin.SkinEntry originalSkinEntry in skinAttachments)
			{
				Attachment newAttachment = AtlasUtilities.inoutAttachments[i++];
				newSkin.SetAttachment(originalSkinEntry.SlotIndex, originalSkinEntry.Name, newAttachment);
			}
			return newSkin;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00014B9C File Offset: 0x00012D9C
		public static Sprite ToSprite(this AtlasRegion ar, float pixelsPerUnit = 100f)
		{
			return Sprite.Create(ar.GetMainTexture(), ar.GetUnityRect(), new Vector2(0.5f, 0.5f), pixelsPerUnit);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x00014BC0 File Offset: 0x00012DC0
		public static void ClearCache()
		{
			foreach (Texture2D texture2D in AtlasUtilities.CachedRegionTexturesList)
			{
				global::UnityEngine.Object.Destroy(texture2D);
			}
			AtlasUtilities.CachedRegionTextures.Clear();
			AtlasUtilities.CachedRegionTexturesList.Clear();
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x00014C24 File Offset: 0x00012E24
		public static Texture2D ToTexture(this AtlasRegion ar, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, int texturePropertyId = 0, bool linear = false, bool applyPMA = false)
		{
			AtlasUtilities.IntAndAtlasRegionKey cacheKey = new AtlasUtilities.IntAndAtlasRegionKey(texturePropertyId, ar);
			Texture2D output;
			AtlasUtilities.CachedRegionTextures.TryGetValue(cacheKey, out output);
			if (output == null)
			{
				Texture2D sourceTexture = ((texturePropertyId == 0) ? ar.GetMainTexture() : ar.GetTexture(texturePropertyId));
				Rect r = ar.GetUnityRect();
				int num = (int)r.width;
				int height = (int)r.height;
				output = new Texture2D(num, height, textureFormat, mipmaps, linear)
				{
					name = ar.name
				};
				output.CopyTextureAttributesFrom(sourceTexture);
				if (applyPMA)
				{
					AtlasUtilities.CopyTextureApplyPMA(sourceTexture, r, output);
				}
				else
				{
					AtlasUtilities.CopyTexture(sourceTexture, r, output);
				}
				AtlasUtilities.CachedRegionTextures.Add(cacheKey, output);
				AtlasUtilities.CachedRegionTexturesList.Add(output);
			}
			return output;
		}

		// Token: 0x060003A7 RID: 935 RVA: 0x00014CCC File Offset: 0x00012ECC
		private static Texture2D ToTexture(this Sprite s, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, bool linear = false, bool applyPMA = false)
		{
			Texture2D spriteTexture = s.texture;
			Rect r;
			if (!s.packed || s.packingMode == SpritePackingMode.Rectangle)
			{
				r = s.textureRect;
			}
			else
			{
				r = default(Rect);
				r.xMin = Math.Min(s.uv[0].x, s.uv[1].x) * (float)spriteTexture.width;
				r.xMax = Math.Max(s.uv[0].x, s.uv[1].x) * (float)spriteTexture.width;
				r.yMin = Math.Min(s.uv[0].y, s.uv[2].y) * (float)spriteTexture.height;
				r.yMax = Math.Max(s.uv[0].y, s.uv[2].y) * (float)spriteTexture.height;
			}
			Texture2D newTexture = new Texture2D((int)r.width, (int)r.height, textureFormat, mipmaps, linear);
			newTexture.CopyTextureAttributesFrom(spriteTexture);
			if (applyPMA)
			{
				AtlasUtilities.CopyTextureApplyPMA(spriteTexture, r, newTexture);
			}
			else
			{
				AtlasUtilities.CopyTexture(spriteTexture, r, newTexture);
			}
			return newTexture;
		}

		// Token: 0x060003A8 RID: 936 RVA: 0x00014E14 File Offset: 0x00013014
		private static Texture2D GetClone(this Texture2D t, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, bool linear = false, bool applyPMA = false)
		{
			Texture2D newTexture = new Texture2D(t.width, t.height, textureFormat, mipmaps, linear);
			newTexture.CopyTextureAttributesFrom(t);
			if (applyPMA)
			{
				AtlasUtilities.CopyTextureApplyPMA(t, new Rect(0f, 0f, (float)t.width, (float)t.height), newTexture);
			}
			else
			{
				AtlasUtilities.CopyTexture(t, new Rect(0f, 0f, (float)t.width, (float)t.height), newTexture);
			}
			return newTexture;
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x00014E8C File Offset: 0x0001308C
		private static void CopyTexture(Texture2D source, Rect sourceRect, Texture2D destination)
		{
			if (SystemInfo.copyTextureSupport == CopyTextureSupport.None)
			{
				Color[] pixelBuffer = source.GetPixels((int)sourceRect.x, (int)sourceRect.y, (int)sourceRect.width, (int)sourceRect.height);
				destination.SetPixels(pixelBuffer);
				destination.Apply();
				return;
			}
			Graphics.CopyTexture(source, 0, 0, (int)sourceRect.x, (int)sourceRect.y, (int)sourceRect.width, (int)sourceRect.height, destination, 0, 0, 0, 0);
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00014F04 File Offset: 0x00013104
		private static void CopyTextureApplyPMA(Texture2D source, Rect sourceRect, Texture2D destination)
		{
			Color[] pixelBuffer = source.GetPixels((int)sourceRect.x, (int)sourceRect.y, (int)sourceRect.width, (int)sourceRect.height);
			int i = 0;
			int j = pixelBuffer.Length;
			while (i < j)
			{
				Color p = pixelBuffer[i];
				float a = p.a;
				p.r *= a;
				p.g *= a;
				p.b *= a;
				pixelBuffer[i] = p;
				i++;
			}
			destination.SetPixels(pixelBuffer);
			destination.Apply();
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00014F9D File Offset: 0x0001319D
		private static bool IsRenderable(Attachment a)
		{
			return a is IHasTextureRegion;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00014FA8 File Offset: 0x000131A8
		private static Rect SpineUnityFlipRect(this Rect rect, int textureHeight)
		{
			rect.y = (float)textureHeight - rect.y - rect.height;
			return rect;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x00014FC4 File Offset: 0x000131C4
		private static Rect GetUnityRect(this AtlasRegion region)
		{
			return region.GetSpineAtlasRect(true).SpineUnityFlipRect(region.page.height);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00014FDD File Offset: 0x000131DD
		private static Rect GetUnityRect(this AtlasRegion region, int textureHeight)
		{
			return region.GetSpineAtlasRect(true).SpineUnityFlipRect(textureHeight);
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00014FEC File Offset: 0x000131EC
		private static Rect GetSpineAtlasRect(this AtlasRegion region, bool includeRotate = true)
		{
			float width = (float)region.packedWidth;
			float height = (float)region.packedHeight;
			if (includeRotate && region.degrees == 270)
			{
				width = (float)region.packedHeight;
				height = (float)region.packedWidth;
			}
			return new Rect((float)region.x, (float)region.y, width, height);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00015040 File Offset: 0x00013240
		private static Rect UVRectToTextureRect(Rect uvRect, int texWidth, int texHeight)
		{
			uvRect.x *= (float)texWidth;
			uvRect.width *= (float)texWidth;
			uvRect.y *= (float)texHeight;
			uvRect.height *= (float)texHeight;
			return uvRect;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00015090 File Offset: 0x00013290
		private static Rect TextureRectToUVRect(Rect textureRect, int texWidth, int texHeight)
		{
			textureRect.x = Mathf.InverseLerp(0f, (float)texWidth, textureRect.x);
			textureRect.y = Mathf.InverseLerp(0f, (float)texHeight, textureRect.y);
			textureRect.width = Mathf.InverseLerp(0f, (float)texWidth, textureRect.width);
			textureRect.height = Mathf.InverseLerp(0f, (float)texHeight, textureRect.height);
			return textureRect;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00015108 File Offset: 0x00013308
		private static AtlasRegion UVRectToAtlasRegion(Rect uvRect, AtlasRegion referenceRegion, AtlasPage page)
		{
			Rect rr = AtlasUtilities.UVRectToTextureRect(uvRect, page.width, page.height).SpineUnityFlipRect(page.height);
			int x = (int)rr.x;
			int y = (int)rr.y;
			int w = (int)rr.width;
			int h = (int)rr.height;
			if (referenceRegion.degrees == 270)
			{
				int num = w;
				w = h;
				h = num;
			}
			int originalW = Mathf.RoundToInt((float)w * ((float)referenceRegion.originalWidth / (float)referenceRegion.width));
			int originalH = Mathf.RoundToInt((float)h * ((float)referenceRegion.originalHeight / (float)referenceRegion.height));
			int offsetX = Mathf.RoundToInt(referenceRegion.offsetX * ((float)w / (float)referenceRegion.width));
			int offsetY = Mathf.RoundToInt(referenceRegion.offsetY * ((float)h / (float)referenceRegion.height));
			float u = uvRect.xMin;
			float u2 = uvRect.xMax;
			float v = uvRect.yMax;
			float v2 = uvRect.yMin;
			if (referenceRegion.degrees == 270)
			{
				float du = uvRect.width;
				float dv = uvRect.height;
				float atlasAspectRatio = (float)page.width / (float)page.height;
				u2 = u + dv / atlasAspectRatio;
				v2 = v - du * atlasAspectRatio;
			}
			return new AtlasRegion
			{
				page = page,
				name = referenceRegion.name,
				u = u,
				u2 = u2,
				v = v,
				v2 = v2,
				index = -1,
				width = w,
				originalWidth = originalW,
				height = h,
				originalHeight = originalH,
				offsetX = (float)offsetX,
				offsetY = (float)offsetY,
				x = x,
				y = y,
				rotate = referenceRegion.rotate,
				degrees = referenceRegion.degrees
			};
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000152CF File Offset: 0x000134CF
		private static Texture2D GetMainTexture(this AtlasRegion region)
		{
			return (region.page.rendererObject as Material).mainTexture as Texture2D;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000152EB File Offset: 0x000134EB
		private static Texture2D GetTexture(this AtlasRegion region, string texturePropertyName)
		{
			return (region.page.rendererObject as Material).GetTexture(texturePropertyName) as Texture2D;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x00015308 File Offset: 0x00013508
		private static Texture2D GetTexture(this AtlasRegion region, int texturePropertyId)
		{
			return (region.page.rendererObject as Material).GetTexture(texturePropertyId) as Texture2D;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x00015325 File Offset: 0x00013525
		private static void CopyTextureAttributesFrom(this Texture2D destination, Texture2D source)
		{
			destination.filterMode = source.filterMode;
			destination.anisoLevel = source.anisoLevel;
			destination.wrapModeU = source.wrapModeU;
			destination.wrapModeV = source.wrapModeV;
			destination.wrapModeW = source.wrapModeW;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00013010 File Offset: 0x00011210
		private static float InverseLerp(float a, float b, float value)
		{
			return (value - a) / (b - a);
		}

		// Token: 0x04000238 RID: 568
		internal const TextureFormat SpineTextureFormat = TextureFormat.RGBA32;

		// Token: 0x04000239 RID: 569
		internal const float DefaultMipmapBias = -0.5f;

		// Token: 0x0400023A RID: 570
		internal const bool UseMipMaps = false;

		// Token: 0x0400023B RID: 571
		internal const float DefaultScale = 0.01f;

		// Token: 0x0400023C RID: 572
		private const int NonrenderingRegion = -1;

		// Token: 0x0400023D RID: 573
		private static readonly Dictionary<AtlasRegion, int> existingRegions = new Dictionary<AtlasRegion, int>();

		// Token: 0x0400023E RID: 574
		private static readonly List<int> regionIndices = new List<int>();

		// Token: 0x0400023F RID: 575
		private static readonly List<AtlasRegion> originalRegions = new List<AtlasRegion>();

		// Token: 0x04000240 RID: 576
		private static readonly List<AtlasRegion> repackedRegions = new List<AtlasRegion>();

		// Token: 0x04000241 RID: 577
		private static List<Texture2D>[] texturesToPackAtParam = new List<Texture2D>[1];

		// Token: 0x04000242 RID: 578
		private static List<Attachment> inoutAttachments = new List<Attachment>();

		// Token: 0x04000243 RID: 579
		private static Dictionary<AtlasUtilities.IntAndAtlasRegionKey, Texture2D> CachedRegionTextures = new Dictionary<AtlasUtilities.IntAndAtlasRegionKey, Texture2D>();

		// Token: 0x04000244 RID: 580
		private static List<Texture2D> CachedRegionTexturesList = new List<Texture2D>();

		// Token: 0x02000080 RID: 128
		private struct IntAndAtlasRegionKey
		{
			// Token: 0x060003B9 RID: 953 RVA: 0x000153C2 File Offset: 0x000135C2
			public IntAndAtlasRegionKey(int i, AtlasRegion region)
			{
				this.i = i;
				this.region = region;
			}

			// Token: 0x060003BA RID: 954 RVA: 0x000153D2 File Offset: 0x000135D2
			public override int GetHashCode()
			{
				return (this.i.GetHashCode() * 23) ^ this.region.GetHashCode();
			}

			// Token: 0x04000245 RID: 581
			private int i;

			// Token: 0x04000246 RID: 582
			private AtlasRegion region;
		}
	}
}
