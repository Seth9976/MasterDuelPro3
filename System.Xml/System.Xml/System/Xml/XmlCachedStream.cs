using System;
using System.IO;

namespace System.Xml
{
	// Token: 0x0200011B RID: 283
	internal class XmlCachedStream : MemoryStream
	{
		// Token: 0x06000ECF RID: 3791 RVA: 0x0004B1A8 File Offset: 0x000493A8
		internal XmlCachedStream(Uri uri, Stream stream)
		{
			this.uri = uri;
			try
			{
				byte[] array = new byte[4096];
				int num;
				while ((num = stream.Read(array, 0, 4096)) > 0)
				{
					this.Write(array, 0, num);
				}
				base.Position = 0L;
			}
			finally
			{
				stream.Close();
			}
		}

		// Token: 0x04000743 RID: 1859
		private Uri uri;
	}
}
