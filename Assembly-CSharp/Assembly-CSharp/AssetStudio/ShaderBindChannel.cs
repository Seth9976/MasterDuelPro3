using System;
using System.IO;

namespace AssetStudio
{
	// Token: 0x0200011C RID: 284
	public class ShaderBindChannel
	{
		// Token: 0x06000374 RID: 884 RVA: 0x000126E0 File Offset: 0x000108E0
		public ShaderBindChannel(BinaryReader reader)
		{
			this.source = reader.ReadSByte();
			this.target = reader.ReadSByte();
		}

		// Token: 0x040007C9 RID: 1993
		public sbyte source;

		// Token: 0x040007CA RID: 1994
		public sbyte target;
	}
}
