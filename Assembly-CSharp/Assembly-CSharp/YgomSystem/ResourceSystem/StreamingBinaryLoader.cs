using System;
using System.Collections;
using System.IO;

namespace YgomSystem.ResourceSystem
{
	// Token: 0x020006F4 RID: 1780
	public class StreamingBinaryLoader : BinaryLoader
	{
		// Token: 0x06003779 RID: 14201 RVA: 0x0000216A File Offset: 0x0000036A
		protected override byte[] LoadFromFile(string path)
		{
			return null;
		}

		// Token: 0x0600377A RID: 14202 RVA: 0x0000216A File Offset: 0x0000036A
		protected override string GetNativePath(string path)
		{
			return null;
		}

		// Token: 0x0600377B RID: 14203 RVA: 0x0000216A File Offset: 0x0000036A
		protected override IEnumerator LoadFromStreamFile(string path, Action<byte[]> callback)
		{
			return null;
		}

		// Token: 0x0600377C RID: 14204 RVA: 0x000029CC File Offset: 0x00000BCC
		private static int StreamLoad(Stream readStream, MemoryStream writeStream, int bufsize = 1048576)
		{
			return 0;
		}
	}
}
