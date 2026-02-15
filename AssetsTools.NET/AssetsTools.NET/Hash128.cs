using System;
using System.Text;

namespace AssetsTools.NET
{
	// Token: 0x02000046 RID: 70
	public struct Hash128
	{
		// Token: 0x06000204 RID: 516 RVA: 0x00010F6C File Offset: 0x0000F16C
		public Hash128(byte[] data)
		{
			this.data = data;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00010F76 File Offset: 0x0000F176
		public Hash128(AssetsFileReader reader)
		{
			this.data = reader.ReadBytes(16);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00010F88 File Offset: 0x0000F188
		public bool IsZero()
		{
			bool flag = this.data == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				for (int i = 0; i < this.data.Length; i++)
				{
					bool flag3 = this.data[i] > 0;
					if (flag3)
					{
						return false;
					}
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00010FDC File Offset: 0x0000F1DC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(this.data.Length * 2);
			foreach (byte b in this.data)
			{
				stringBuilder.AppendFormat("{0:x2}", b);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00011034 File Offset: 0x0000F234
		public static Hash128 NewBlankHash()
		{
			return new Hash128
			{
				data = new byte[16]
			};
		}

		// Token: 0x0400019A RID: 410
		public byte[] data;
	}
}
