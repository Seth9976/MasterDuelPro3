using System;

namespace Spine
{
	// Token: 0x0200004B RID: 75
	public interface AttachmentLoader
	{
		// Token: 0x060001C5 RID: 453
		RegionAttachment NewRegionAttachment(Skin skin, string name, string path, Sequence sequence);

		// Token: 0x060001C6 RID: 454
		MeshAttachment NewMeshAttachment(Skin skin, string name, string path, Sequence sequence);

		// Token: 0x060001C7 RID: 455
		BoundingBoxAttachment NewBoundingBoxAttachment(Skin skin, string name);

		// Token: 0x060001C8 RID: 456
		PathAttachment NewPathAttachment(Skin skin, string name);

		// Token: 0x060001C9 RID: 457
		PointAttachment NewPointAttachment(Skin skin, string name);

		// Token: 0x060001CA RID: 458
		ClippingAttachment NewClippingAttachment(Skin skin, string name);
	}
}
