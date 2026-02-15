using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace MiniMessagePack
{
	// Token: 0x02001195 RID: 4501
	public class MiniMessagePacker
	{
		// Token: 0x060086A6 RID: 34470 RVA: 0x0000216A File Offset: 0x0000036A
		public byte[] Pack(object o)
		{
			return null;
		}

		// Token: 0x060086A7 RID: 34471 RVA: 0x0000216D File Offset: 0x0000036D
		public void Pack(Stream s, object o)
		{
		}

		// Token: 0x060086A8 RID: 34472 RVA: 0x0000216D File Offset: 0x0000036D
		private void PackNull(Stream s)
		{
		}

		// Token: 0x060086A9 RID: 34473 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, IList list)
		{
		}

		// Token: 0x060086AA RID: 34474 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, IDictionary dict)
		{
		}

		// Token: 0x060086AB RID: 34475 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, bool val)
		{
		}

		// Token: 0x060086AC RID: 34476 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, sbyte val)
		{
		}

		// Token: 0x060086AD RID: 34477 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, byte val)
		{
		}

		// Token: 0x060086AE RID: 34478 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, short val)
		{
		}

		// Token: 0x060086AF RID: 34479 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, ushort val)
		{
		}

		// Token: 0x060086B0 RID: 34480 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, int val)
		{
		}

		// Token: 0x060086B1 RID: 34481 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, uint val)
		{
		}

		// Token: 0x060086B2 RID: 34482 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, long val)
		{
		}

		// Token: 0x060086B3 RID: 34483 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, ulong val)
		{
		}

		// Token: 0x060086B4 RID: 34484 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, float val)
		{
		}

		// Token: 0x060086B5 RID: 34485 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, double val)
		{
		}

		// Token: 0x060086B6 RID: 34486 RVA: 0x0000216D File Offset: 0x0000036D
		private void Pack(Stream s, string val)
		{
		}

		// Token: 0x060086B7 RID: 34487 RVA: 0x0000216D File Offset: 0x0000036D
		private void Write(Stream s, ushort val)
		{
		}

		// Token: 0x060086B8 RID: 34488 RVA: 0x0000216D File Offset: 0x0000036D
		private void Write(Stream s, uint val)
		{
		}

		// Token: 0x060086B9 RID: 34489 RVA: 0x0000216D File Offset: 0x0000036D
		private void Write(Stream s, ulong val)
		{
		}

		// Token: 0x060086BA RID: 34490 RVA: 0x0000216A File Offset: 0x0000036A
		public object Unpack(byte[] buf, int offset, int size)
		{
			return null;
		}

		// Token: 0x060086BB RID: 34491 RVA: 0x0000216A File Offset: 0x0000036A
		public object Unpack(byte[] buf)
		{
			return null;
		}

		// Token: 0x060086BC RID: 34492 RVA: 0x0000216A File Offset: 0x0000036A
		public object Unpack(Stream s)
		{
			return null;
		}

		// Token: 0x060086BD RID: 34493 RVA: 0x000F1669 File Offset: 0x000EF869
		private long UnpackUint16(Stream s)
		{
			return 0L;
		}

		// Token: 0x060086BE RID: 34494 RVA: 0x000F1669 File Offset: 0x000EF869
		private long UnpackUint32(Stream s)
		{
			return 0L;
		}

		// Token: 0x060086BF RID: 34495 RVA: 0x0000216A File Offset: 0x0000036A
		private string UnpackString(Stream s, long len)
		{
			return null;
		}

		// Token: 0x060086C0 RID: 34496 RVA: 0x0000216A File Offset: 0x0000036A
		private byte[] UnpackBinary(Stream s, long len)
		{
			return null;
		}

		// Token: 0x060086C1 RID: 34497 RVA: 0x0000216A File Offset: 0x0000036A
		private List<object> UnpackArray(Stream s, long len)
		{
			return null;
		}

		// Token: 0x060086C2 RID: 34498 RVA: 0x0000216A File Offset: 0x0000036A
		private Dictionary<string, object> UnpackMap(Stream s, long len)
		{
			return null;
		}

		// Token: 0x0400C149 RID: 49481
		private byte[] tmp0;

		// Token: 0x0400C14A RID: 49482
		private byte[] tmp1;

		// Token: 0x0400C14B RID: 49483
		private byte[] string_buf;

		// Token: 0x0400C14C RID: 49484
		private Encoding encoder;
	}
}
