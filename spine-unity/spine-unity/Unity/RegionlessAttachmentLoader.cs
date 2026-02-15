using System;
using UnityEngine;

namespace Spine.Unity
{
	// Token: 0x02000012 RID: 18
	public class RegionlessAttachmentLoader : AttachmentLoader
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00003478 File Offset: 0x00001678
		private static AtlasRegion EmptyRegion
		{
			get
			{
				if (RegionlessAttachmentLoader.emptyRegion == null)
				{
					Shader hiddenShader = Shader.Find("Spine/Special/HiddenPass");
					if (hiddenShader == null)
					{
						Debug.LogError("Shader \"Spine/Special/HiddenPass\" not found while loading SkeletonDataAsset with 0 Atlas Assets. Please add this shader to Project Settings - Graphics - Always Included Shaders, or make sure your SkeletonDataAssets all have an AtlasAsset assigned.");
					}
					RegionlessAttachmentLoader.emptyRegion = new AtlasRegion
					{
						name = "Empty AtlasRegion",
						page = new AtlasPage
						{
							name = "Empty AtlasPage",
							rendererObject = new Material(hiddenShader)
							{
								name = "NoRender Material"
							}
						}
					};
				}
				return RegionlessAttachmentLoader.emptyRegion;
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x000034F1 File Offset: 0x000016F1
		public RegionAttachment NewRegionAttachment(Skin skin, string name, string path, Sequence sequence)
		{
			return new RegionAttachment(name)
			{
				Region = RegionlessAttachmentLoader.EmptyRegion
			};
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00003504 File Offset: 0x00001704
		public MeshAttachment NewMeshAttachment(Skin skin, string name, string path, Sequence sequence)
		{
			return new MeshAttachment(name)
			{
				Region = RegionlessAttachmentLoader.EmptyRegion
			};
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00003517 File Offset: 0x00001717
		public BoundingBoxAttachment NewBoundingBoxAttachment(Skin skin, string name)
		{
			return new BoundingBoxAttachment(name);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000351F File Offset: 0x0000171F
		public PathAttachment NewPathAttachment(Skin skin, string name)
		{
			return new PathAttachment(name);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x00003527 File Offset: 0x00001727
		public PointAttachment NewPointAttachment(Skin skin, string name)
		{
			return new PointAttachment(name);
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000352F File Offset: 0x0000172F
		public ClippingAttachment NewClippingAttachment(Skin skin, string name)
		{
			return new ClippingAttachment(name);
		}

		// Token: 0x0400002E RID: 46
		private static AtlasRegion emptyRegion;
	}
}
