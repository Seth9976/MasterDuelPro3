using System;

namespace Spine
{
	// Token: 0x0200007C RID: 124
	public abstract class SkeletonLoader
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x0001B53D File Offset: 0x0001973D
		public SkeletonLoader(params Atlas[] atlasArray)
		{
			this.attachmentLoader = new AtlasAttachmentLoader(atlasArray);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001B55C File Offset: 0x0001975C
		public SkeletonLoader(AttachmentLoader attachmentLoader)
		{
			if (attachmentLoader == null)
			{
				throw new ArgumentNullException("attachmentLoader", "attachmentLoader cannot be null.");
			}
			this.attachmentLoader = attachmentLoader;
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060004B1 RID: 1201 RVA: 0x0001B589 File Offset: 0x00019789
		// (set) Token: 0x060004B2 RID: 1202 RVA: 0x0001B591 File Offset: 0x00019791
		public float Scale
		{
			get
			{
				return this.scale;
			}
			set
			{
				if (this.scale == 0f)
				{
					throw new ArgumentNullException("scale", "scale cannot be 0.");
				}
				this.scale = value;
			}
		}

		// Token: 0x060004B3 RID: 1203
		public abstract SkeletonData ReadSkeletonData(string path);

		// Token: 0x040002B2 RID: 690
		protected readonly AttachmentLoader attachmentLoader;

		// Token: 0x040002B3 RID: 691
		protected float scale = 1f;
	}
}
