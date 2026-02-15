using System;
using System.Text;

namespace Mono.Net.Dns
{
	// Token: 0x02000092 RID: 146
	internal static class DnsUtil
	{
		// Token: 0x0600023C RID: 572 RVA: 0x00008DDC File Offset: 0x00006FDC
		public static bool IsValidDnsName(string name)
		{
			if (name == null)
			{
				return false;
			}
			int length = name.Length;
			if (length > 255)
			{
				return false;
			}
			int num = 0;
			for (int i = 0; i < length; i++)
			{
				if (name[i] == '.')
				{
					if (i == 0 && length > 1)
					{
						return false;
					}
					if (i > 0 && num == 0)
					{
						return false;
					}
					num = 0;
				}
				else
				{
					num++;
					if (num > 63)
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00008E3C File Offset: 0x0000703C
		public static int GetEncodedLength(string name)
		{
			if (!DnsUtil.IsValidDnsName(name))
			{
				return -1;
			}
			if (name == string.Empty)
			{
				return 1;
			}
			int length = name.Length;
			if (name[length - 1] == '.')
			{
				return length + 1;
			}
			return length + 2;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00008E80 File Offset: 0x00007080
		public static string ReadName(byte[] buffer, ref int offset)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (offset < 0 || offset >= buffer.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			StringBuilder stringBuilder = new StringBuilder(32);
			bool flag = true;
			int num = offset;
			while (stringBuilder.Length < 256)
			{
				int num2 = (int)buffer[num++];
				if (flag)
				{
					offset++;
				}
				if (num2 == 0)
				{
					if (stringBuilder.Length > 0)
					{
						StringBuilder stringBuilder2 = stringBuilder;
						int length = stringBuilder2.Length;
						stringBuilder2.Length = length - 1;
					}
					return stringBuilder.ToString();
				}
				int num3 = num2 & 192;
				if (num3 == 192)
				{
					num2 = ((num3 & 63) << 8) + (int)buffer[num];
					if (flag)
					{
						offset++;
					}
					flag = false;
					num = num2;
				}
				else
				{
					if (num2 >= 64)
					{
						return null;
					}
					for (int i = 0; i < num2; i++)
					{
						stringBuilder.Append((char)buffer[num + i]);
					}
					stringBuilder.Append('.');
					num += num2;
					if (flag)
					{
						offset += num2;
					}
				}
			}
			return null;
		}
	}
}
