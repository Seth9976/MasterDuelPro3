using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200013A RID: 314
	public class Rectf
	{
		// Token: 0x06000391 RID: 913 RVA: 0x00013952 File Offset: 0x00011B52
		public Rectf(BinaryReader reader)
		{
			this.x = reader.ReadSingle();
			this.y = reader.ReadSingle();
			this.width = reader.ReadSingle();
			this.height = reader.ReadSingle();
		}

		// Token: 0x0400089D RID: 2205
		public float x;

		// Token: 0x0400089E RID: 2206
		public float y;

		// Token: 0x0400089F RID: 2207
		public float width;

		// Token: 0x040008A0 RID: 2208
		public float height;
	}
}
