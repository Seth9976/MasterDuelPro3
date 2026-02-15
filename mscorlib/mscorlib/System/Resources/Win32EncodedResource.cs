using System;
using System.IO;

namespace System.Resources
{
	// Token: 0x020005DD RID: 1501
	internal class Win32EncodedResource : Win32Resource
	{
		// Token: 0x06002C6A RID: 11370 RVA: 0x000B0CE8 File Offset: 0x000AEEE8
		internal Win32EncodedResource(NameOrId type, NameOrId name, int language, byte[] data)
			: base(type, name, language)
		{
			this.data = data;
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000B0CFB File Offset: 0x000AEEFB
		public override void WriteTo(Stream s)
		{
			s.Write(this.data, 0, this.data.Length);
		}

		// Token: 0x040016A3 RID: 5795
		private byte[] data;
	}
}
