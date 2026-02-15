using System;
using System.Data;

namespace YGOSharp.OCGWrapper
{
	// Token: 0x020001CC RID: 460
	public class NamedCard : Card
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00026299 File Offset: 0x00024499
		// (set) Token: 0x06000824 RID: 2084 RVA: 0x000262A1 File Offset: 0x000244A1
		public string Name { get; private set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000825 RID: 2085 RVA: 0x000262AA File Offset: 0x000244AA
		// (set) Token: 0x06000826 RID: 2086 RVA: 0x000262B2 File Offset: 0x000244B2
		public string Description { get; private set; }

		// Token: 0x06000827 RID: 2087 RVA: 0x000262BB File Offset: 0x000244BB
		internal NamedCard(IDataRecord reader)
			: base(reader)
		{
			this.Name = reader.GetString(10);
			this.Description = reader.GetString(11);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000262E0 File Offset: 0x000244E0
		public new static NamedCard Get(int id)
		{
			return NamedCardsManager.GetCard(id);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000262E8 File Offset: 0x000244E8
		public bool HasSetcode(int setcode)
		{
			long num = (long)setcode;
			int num2 = setcode & 4095;
			int num3 = setcode & 61440;
			while (num > 0L)
			{
				long num4 = num & 65535L;
				num >>= 16;
				if ((num4 & 4095L) == (long)num2 && (num4 & 61440L & (long)num3) == (long)num3)
				{
					return true;
				}
			}
			return false;
		}
	}
}
