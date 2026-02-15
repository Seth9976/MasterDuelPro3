using System;
using System.IO;
using System.Text;

namespace AssetStudio
{
	// Token: 0x0200014B RID: 331
	public static class BinaryWriterExtensions
	{
		// Token: 0x060003C2 RID: 962 RVA: 0x00014EA4 File Offset: 0x000130A4
		public static void AlignStream(this BinaryWriter writer, int alignment)
		{
			long mod = writer.BaseStream.Position % (long)alignment;
			if (mod != 0L)
			{
				writer.Write(new byte[(long)alignment - mod]);
			}
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x00014ED4 File Offset: 0x000130D4
		public static void WriteAlignedString(this BinaryWriter writer, string str)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(str);
			writer.Write(bytes.Length);
			writer.Write(bytes);
			writer.AlignStream(4);
		}
	}
}
