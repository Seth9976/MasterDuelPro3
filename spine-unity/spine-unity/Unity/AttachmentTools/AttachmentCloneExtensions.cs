using System;
using UnityEngine;

namespace Spine.Unity.AttachmentTools
{
	// Token: 0x02000081 RID: 129
	public static class AttachmentCloneExtensions
	{
		// Token: 0x060003BB RID: 955 RVA: 0x000153F0 File Offset: 0x000135F0
		public static Attachment GetRemappedClone(this Attachment o, Sprite sprite, Material sourceMaterial, bool premultiplyAlpha = true, bool cloneMeshAsLinked = true, bool useOriginalRegionSize = false, bool pivotShiftsMeshUVCoords = true, bool useOriginalRegionScale = false, TextureFormat pmaCloneTextureFormat = TextureFormat.RGBA32, bool pmaCloneMipmaps = false)
		{
			AtlasRegion atlasRegion = (premultiplyAlpha ? sprite.ToAtlasRegionPMAClone(sourceMaterial, pmaCloneTextureFormat, pmaCloneMipmaps) : sprite.ToAtlasRegion(new Material(sourceMaterial)
			{
				mainTexture = sprite.texture
			}));
			if (!pivotShiftsMeshUVCoords && o is MeshAttachment)
			{
				atlasRegion.offsetX = 0f;
				atlasRegion.offsetY = 0f;
			}
			float scale = 1f / sprite.pixelsPerUnit;
			if (useOriginalRegionScale)
			{
				RegionAttachment regionAttachment = o as RegionAttachment;
				if (regionAttachment != null)
				{
					scale = regionAttachment.Width / (float)regionAttachment.Region.OriginalWidth;
				}
			}
			return o.GetRemappedClone(atlasRegion, cloneMeshAsLinked, useOriginalRegionSize, scale);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x00015484 File Offset: 0x00013684
		public static Attachment GetRemappedClone(this Attachment o, AtlasRegion atlasRegion, bool cloneMeshAsLinked = true, bool useOriginalRegionSize = false, float scale = 0.01f)
		{
			RegionAttachment regionAttachment = o as RegionAttachment;
			if (regionAttachment != null)
			{
				RegionAttachment newAttachment = (RegionAttachment)regionAttachment.Copy();
				newAttachment.Region = atlasRegion;
				if (!useOriginalRegionSize)
				{
					newAttachment.Width = (float)atlasRegion.width * scale;
					newAttachment.Height = (float)atlasRegion.height * scale;
				}
				newAttachment.UpdateRegion();
				return newAttachment;
			}
			MeshAttachment meshAttachment = o as MeshAttachment;
			if (meshAttachment != null)
			{
				MeshAttachment meshAttachment2 = (cloneMeshAsLinked ? meshAttachment.NewLinkedMesh() : ((MeshAttachment)meshAttachment.Copy()));
				meshAttachment2.Region = atlasRegion;
				meshAttachment2.UpdateRegion();
				return meshAttachment2;
			}
			return o.Copy();
		}
	}
}
