using System;
using System.Collections.Generic;
using UnityEngine;

namespace TMPro.SpriteAssetUtilities
{
	// Token: 0x020000BC RID: 188
	public class TexturePacker_JsonArray
	{
		// Token: 0x020000BD RID: 189
		[Serializable]
		public struct SpriteFrame
		{
			// Token: 0x06000714 RID: 1812 RVA: 0x00043D8C File Offset: 0x00041F8C
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"x: ",
					this.x.ToString("f2"),
					" y: ",
					this.y.ToString("f2"),
					" h: ",
					this.h.ToString("f2"),
					" w: ",
					this.w.ToString("f2")
				});
			}

			// Token: 0x0400069A RID: 1690
			public float x;

			// Token: 0x0400069B RID: 1691
			public float y;

			// Token: 0x0400069C RID: 1692
			public float w;

			// Token: 0x0400069D RID: 1693
			public float h;
		}

		// Token: 0x020000BE RID: 190
		[Serializable]
		public struct SpriteSize
		{
			// Token: 0x06000715 RID: 1813 RVA: 0x00043E10 File Offset: 0x00042010
			public override string ToString()
			{
				return "w: " + this.w.ToString("f2") + " h: " + this.h.ToString("f2");
			}

			// Token: 0x0400069E RID: 1694
			public float w;

			// Token: 0x0400069F RID: 1695
			public float h;
		}

		// Token: 0x020000BF RID: 191
		[Serializable]
		public struct Frame
		{
			// Token: 0x040006A0 RID: 1696
			public string filename;

			// Token: 0x040006A1 RID: 1697
			public TexturePacker_JsonArray.SpriteFrame frame;

			// Token: 0x040006A2 RID: 1698
			public bool rotated;

			// Token: 0x040006A3 RID: 1699
			public bool trimmed;

			// Token: 0x040006A4 RID: 1700
			public TexturePacker_JsonArray.SpriteFrame spriteSourceSize;

			// Token: 0x040006A5 RID: 1701
			public TexturePacker_JsonArray.SpriteSize sourceSize;

			// Token: 0x040006A6 RID: 1702
			public Vector2 pivot;
		}

		// Token: 0x020000C0 RID: 192
		[Serializable]
		public struct Meta
		{
			// Token: 0x040006A7 RID: 1703
			public string app;

			// Token: 0x040006A8 RID: 1704
			public string version;

			// Token: 0x040006A9 RID: 1705
			public string image;

			// Token: 0x040006AA RID: 1706
			public string format;

			// Token: 0x040006AB RID: 1707
			public TexturePacker_JsonArray.SpriteSize size;

			// Token: 0x040006AC RID: 1708
			public float scale;

			// Token: 0x040006AD RID: 1709
			public string smartupdate;
		}

		// Token: 0x020000C1 RID: 193
		[Serializable]
		public class SpriteDataObject
		{
			// Token: 0x040006AE RID: 1710
			public List<TexturePacker_JsonArray.Frame> frames;

			// Token: 0x040006AF RID: 1711
			public TexturePacker_JsonArray.Meta meta;
		}
	}
}
