using System;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x0200010A RID: 266
	internal sealed class UnsafeBitArrayDebugView
	{
		// Token: 0x06000B3A RID: 2874 RVA: 0x00022772 File Offset: 0x00020972
		public UnsafeBitArrayDebugView(UnsafeBitArray data)
		{
			this.Data = data;
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000B3B RID: 2875 RVA: 0x00022784 File Offset: 0x00020984
		public bool[] Bits
		{
			get
			{
				bool[] array = new bool[this.Data.Length];
				for (int i = 0; i < this.Data.Length; i++)
				{
					array[i] = this.Data.IsSet(i);
				}
				return array;
			}
		}

		// Token: 0x040004AD RID: 1197
		private UnsafeBitArray Data;
	}
}
