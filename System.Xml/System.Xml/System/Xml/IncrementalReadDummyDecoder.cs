using System;

namespace System.Xml
{
	// Token: 0x02000036 RID: 54
	internal class IncrementalReadDummyDecoder : IncrementalReadDecoder
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060001BD RID: 445 RVA: 0x0000C1F2 File Offset: 0x0000A3F2
		internal override int DecodedCount
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060001BE RID: 446 RVA: 0x0000C1F5 File Offset: 0x0000A3F5
		internal override bool IsFull
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void SetNextOutputBuffer(Array array, int offset, int len)
		{
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		internal override int Decode(char[] chars, int startPos, int len)
		{
			return len;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		internal override int Decode(string str, int startPos, int len)
		{
			return len;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000A558 File Offset: 0x00008758
		internal override void Reset()
		{
		}
	}
}
