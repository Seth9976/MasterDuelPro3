using System;
using UnityEngine;

namespace Spine.Unity.AttachmentTools
{
	// Token: 0x02000082 RID: 130
	public static class AttachmentRegionExtensions
	{
		// Token: 0x060003BD RID: 957 RVA: 0x0001550D File Offset: 0x0001370D
		public static RegionAttachment ToRegionAttachment(this Sprite sprite, Material material, float rotation = 0f)
		{
			return sprite.ToRegionAttachment(material.ToSpineAtlasPage(), rotation);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001551C File Offset: 0x0001371C
		public static RegionAttachment ToRegionAttachment(this Sprite sprite, AtlasPage page, float rotation = 0f)
		{
			if (sprite == null)
			{
				throw new ArgumentNullException("sprite");
			}
			if (page == null)
			{
				throw new ArgumentNullException("page");
			}
			AtlasRegion atlasRegion = sprite.ToAtlasRegion(page);
			float unitsPerPixel = 1f / sprite.pixelsPerUnit;
			return atlasRegion.ToRegionAttachment(sprite.name, unitsPerPixel, rotation);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0001556C File Offset: 0x0001376C
		public static RegionAttachment ToRegionAttachmentPMAClone(this Sprite sprite, Shader shader, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, Material materialPropertySource = null, float rotation = 0f)
		{
			if (sprite == null)
			{
				throw new ArgumentNullException("sprite");
			}
			if (shader == null)
			{
				throw new ArgumentNullException("shader");
			}
			AtlasRegion atlasRegion = sprite.ToAtlasRegionPMAClone(shader, textureFormat, mipmaps, materialPropertySource);
			float unitsPerPixel = 1f / sprite.pixelsPerUnit;
			return atlasRegion.ToRegionAttachment(sprite.name, unitsPerPixel, rotation);
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000155C7 File Offset: 0x000137C7
		public static RegionAttachment ToRegionAttachmentPMAClone(this Sprite sprite, Material materialPropertySource, TextureFormat textureFormat = TextureFormat.RGBA32, bool mipmaps = false, float rotation = 0f)
		{
			return sprite.ToRegionAttachmentPMAClone(materialPropertySource.shader, textureFormat, mipmaps, materialPropertySource, rotation);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x000155DC File Offset: 0x000137DC
		public static RegionAttachment ToRegionAttachment(this AtlasRegion region, string attachmentName, float scale = 0.01f, float rotation = 0f)
		{
			if (string.IsNullOrEmpty(attachmentName))
			{
				throw new ArgumentException("attachmentName can't be null or empty.", "attachmentName");
			}
			if (region == null)
			{
				throw new ArgumentNullException("region");
			}
			RegionAttachment regionAttachment = new RegionAttachment(attachmentName);
			regionAttachment.Region = region;
			regionAttachment.Path = region.name;
			regionAttachment.ScaleX = 1f;
			regionAttachment.ScaleY = 1f;
			regionAttachment.Rotation = rotation;
			regionAttachment.R = 1f;
			regionAttachment.G = 1f;
			regionAttachment.B = 1f;
			regionAttachment.A = 1f;
			TextureRegion textreRegion = regionAttachment.Region;
			AtlasRegion atlasRegion = textreRegion as AtlasRegion;
			float originalWidth = (float)((atlasRegion != null) ? atlasRegion.originalWidth : textreRegion.width);
			float originalHeight = (float)((atlasRegion != null) ? atlasRegion.originalHeight : textreRegion.height);
			regionAttachment.Width = originalWidth * scale;
			regionAttachment.Height = originalHeight * scale;
			regionAttachment.SetColor(Color.white);
			regionAttachment.UpdateRegion();
			return regionAttachment;
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000156C8 File Offset: 0x000138C8
		public static void SetScale(this RegionAttachment regionAttachment, Vector2 scale)
		{
			regionAttachment.ScaleX = scale.x;
			regionAttachment.ScaleY = scale.y;
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000156E2 File Offset: 0x000138E2
		public static void SetScale(this RegionAttachment regionAttachment, float x, float y)
		{
			regionAttachment.ScaleX = x;
			regionAttachment.ScaleY = y;
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000156F2 File Offset: 0x000138F2
		public static void SetPositionOffset(this RegionAttachment regionAttachment, Vector2 offset)
		{
			regionAttachment.X = offset.x;
			regionAttachment.Y = offset.y;
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0001570C File Offset: 0x0001390C
		public static void SetPositionOffset(this RegionAttachment regionAttachment, float x, float y)
		{
			regionAttachment.X = x;
			regionAttachment.Y = y;
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0001571C File Offset: 0x0001391C
		public static void SetRotation(this RegionAttachment regionAttachment, float rotation)
		{
			regionAttachment.Rotation = rotation;
		}
	}
}
