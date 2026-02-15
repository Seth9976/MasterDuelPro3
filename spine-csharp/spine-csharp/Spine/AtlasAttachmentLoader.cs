using System;

namespace Spine
{
	// Token: 0x02000049 RID: 73
	public class AtlasAttachmentLoader : AttachmentLoader
	{
		// Token: 0x060001B7 RID: 439 RVA: 0x0000A154 File Offset: 0x00008354
		public AtlasAttachmentLoader(params Atlas[] atlasArray)
		{
			if (atlasArray == null)
			{
				throw new ArgumentNullException("atlas", "atlas array cannot be null.");
			}
			this.atlasArray = atlasArray;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000A178 File Offset: 0x00008378
		private void LoadSequence(string name, string basePath, Sequence sequence)
		{
			TextureRegion[] regions = sequence.Regions;
			int i = 0;
			int j = regions.Length;
			while (i < j)
			{
				string path = sequence.GetPath(basePath, i);
				regions[i] = this.FindRegion(path);
				if (regions[i] == null)
				{
					throw new ArgumentException(string.Format("Region not found in atlas: {0} (region attachment: {1})", path, name));
				}
				i++;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000A1C8 File Offset: 0x000083C8
		public RegionAttachment NewRegionAttachment(Skin skin, string name, string path, Sequence sequence)
		{
			RegionAttachment attachment = new RegionAttachment(name);
			if (sequence != null)
			{
				this.LoadSequence(name, path, sequence);
			}
			else
			{
				AtlasRegion region = this.FindRegion(path);
				if (region == null)
				{
					throw new ArgumentException(string.Format("Region not found in atlas: {0} (region attachment: {1})", path, name));
				}
				attachment.Region = region;
			}
			return attachment;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000A214 File Offset: 0x00008414
		public MeshAttachment NewMeshAttachment(Skin skin, string name, string path, Sequence sequence)
		{
			MeshAttachment attachment = new MeshAttachment(name);
			if (sequence != null)
			{
				this.LoadSequence(name, path, sequence);
			}
			else
			{
				AtlasRegion region = this.FindRegion(path);
				if (region == null)
				{
					throw new ArgumentException(string.Format("Region not found in atlas: {0} (region attachment: {1})", path, name));
				}
				attachment.Region = region;
			}
			return attachment;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000A25D File Offset: 0x0000845D
		public BoundingBoxAttachment NewBoundingBoxAttachment(Skin skin, string name)
		{
			return new BoundingBoxAttachment(name);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000A265 File Offset: 0x00008465
		public PathAttachment NewPathAttachment(Skin skin, string name)
		{
			return new PathAttachment(name);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000A26D File Offset: 0x0000846D
		public PointAttachment NewPointAttachment(Skin skin, string name)
		{
			return new PointAttachment(name);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000A275 File Offset: 0x00008475
		public ClippingAttachment NewClippingAttachment(Skin skin, string name)
		{
			return new ClippingAttachment(name);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000A280 File Offset: 0x00008480
		public AtlasRegion FindRegion(string name)
		{
			for (int i = 0; i < this.atlasArray.Length; i++)
			{
				AtlasRegion region = this.atlasArray[i].FindRegion(name);
				if (region != null)
				{
					return region;
				}
			}
			return null;
		}

		// Token: 0x0400012A RID: 298
		private Atlas[] atlasArray;
	}
}
