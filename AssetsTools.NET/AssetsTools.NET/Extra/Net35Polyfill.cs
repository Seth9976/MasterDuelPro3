using System;
using System.IO;

namespace AssetsTools.NET.Extra
{
	// Token: 0x02000088 RID: 136
	public static class Net35Polyfill
	{
		// Token: 0x06000508 RID: 1288 RVA: 0x0001BB6C File Offset: 0x00019D6C
		public static void CopyToCompat(this Stream input, Stream output, long bytes = -1L, int bufferSize = 81920)
		{
			byte[] array = new byte[bufferSize];
			bool flag = bytes == -1L;
			if (flag)
			{
				bytes = long.MaxValue;
			}
			int num;
			while (bytes > 0L && (num = input.Read(array, 0, (int)Math.Min((long)array.Length, bytes))) > 0)
			{
				output.Write(array, 0, num);
				bytes -= (long)num;
			}
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x0001BBD0 File Offset: 0x00019DD0
		public static bool HasFlag(Enum variable, Enum value)
		{
			bool flag = variable == null;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				bool flag3 = value == null;
				if (flag3)
				{
					throw new ArgumentNullException("value");
				}
				bool flag4 = !Enum.IsDefined(variable.GetType(), value);
				if (flag4)
				{
					throw new ArgumentException(string.Format("Enumeration type mismatch.  The flag is of type '{0}', was expecting '{1}'.", value.GetType(), variable.GetType()));
				}
				ulong num = Convert.ToUInt64(value);
				flag2 = (Convert.ToUInt64(variable) & num) == num;
			}
			return flag2;
		}
	}
}
