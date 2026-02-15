using System;
using System.IO;
using System.Text;

namespace YGOSharp.Network.Utils
{
	// Token: 0x020001DE RID: 478
	public static class BinaryExtensions
	{
		// Token: 0x06000887 RID: 2183 RVA: 0x000279D4 File Offset: 0x00025BD4
		public static void WriteUnicode(this BinaryWriter writer, string text, int len)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(text);
			byte[] result = new byte[len * 2];
			int copy = bytes.Length;
			if (bytes.Length > len * 2 - 2)
			{
				copy = len * 2 - 2;
			}
			Array.Copy(bytes, result, copy);
			writer.Write(result);
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00027A18 File Offset: 0x00025C18
		public static void WriteUnicodeAutoLength(this BinaryWriter writer, string text, int maxlen)
		{
			byte[] result = Encoding.Unicode.GetBytes(text + "\0");
			int len = result.Length / 2;
			if (len > maxlen)
			{
				len = maxlen;
				result[len * 2 - 2] = 0;
				result[len * 2 - 1] = 0;
			}
			writer.Write(result, 0, len * 2);
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00027A64 File Offset: 0x00025C64
		public static string ReadUnicode(this BinaryReader reader, int len)
		{
			byte[] unicode = reader.ReadBytes(len * 2);
			string text = Encoding.Unicode.GetString(unicode);
			int index = text.IndexOf('\0');
			if (index > 0)
			{
				text = text.Substring(0, index);
			}
			return text;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00027A9D File Offset: 0x00025C9D
		public static byte[] ReadToEnd(this BinaryReader reader)
		{
			return reader.ReadBytes((int)(reader.BaseStream.Length - reader.BaseStream.Position));
		}
	}
}
