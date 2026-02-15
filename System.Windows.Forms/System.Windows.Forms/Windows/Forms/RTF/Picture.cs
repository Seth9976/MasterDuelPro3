using System;
using System.Drawing;
using System.IO;

namespace System.Windows.Forms.RTF
{
	// Token: 0x02000388 RID: 904
	internal class Picture
	{
		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x0008F83D File Offset: 0x0008DA3D
		// (set) Token: 0x06001D41 RID: 7489 RVA: 0x0008F845 File Offset: 0x0008DA45
		public Minor ImageType
		{
			get
			{
				return this.image_type;
			}
			set
			{
				this.image_type = value;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x0008F84E File Offset: 0x0008DA4E
		public MemoryStream Data
		{
			get
			{
				if (this.data == null)
				{
					this.data = new MemoryStream();
				}
				return this.data;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x06001D43 RID: 7491 RVA: 0x0008F86C File Offset: 0x0008DA6C
		public float Width
		{
			get
			{
				float num = this.width;
				if (num == -1f)
				{
					if (this.image == null)
					{
						this.image = this.ToImage();
					}
					num = (float)this.image.Width;
				}
				return num;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0008F8AC File Offset: 0x0008DAAC
		public float Height
		{
			get
			{
				float num = this.height;
				if (num == -1f)
				{
					if (this.image == null)
					{
						this.image = this.ToImage();
					}
					num = (float)this.image.Height;
				}
				return num;
			}
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x06001D45 RID: 7493 RVA: 0x0008F8EA File Offset: 0x0008DAEA
		public SizeF Size
		{
			get
			{
				return new SizeF(this.Width, this.Height);
			}
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0008F8FD File Offset: 0x0008DAFD
		public void SetWidthFromTwips(int twips)
		{
			this.width = (float)((int)((float)twips / 1440f * Picture.dpix + 0.5f));
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x0008F91B File Offset: 0x0008DB1B
		public void SetHeightFromTwips(int twips)
		{
			this.height = (float)((int)((float)twips / 1440f * Picture.dpix + 0.5f));
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x0008F93C File Offset: 0x0008DB3C
		public bool IsValid()
		{
			if (this.data == null)
			{
				return false;
			}
			Minor minor = this.image_type;
			return minor == Minor.WinMetafile || minor == Minor.PngBlip;
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x0008F96D File Offset: 0x0008DB6D
		public Image ToImage()
		{
			this.data.Position = 0L;
			return Image.FromStream(this.data);
		}

		// Token: 0x04001B2F RID: 6959
		private Minor image_type;

		// Token: 0x04001B30 RID: 6960
		private Image image;

		// Token: 0x04001B31 RID: 6961
		private MemoryStream data;

		// Token: 0x04001B32 RID: 6962
		private float width = -1f;

		// Token: 0x04001B33 RID: 6963
		private float height = -1f;

		// Token: 0x04001B34 RID: 6964
		private static readonly float dpix = TextRenderer.GetDpi().Width;
	}
}
