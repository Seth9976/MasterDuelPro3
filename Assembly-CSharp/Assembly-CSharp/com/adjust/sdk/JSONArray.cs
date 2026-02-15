using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace com.adjust.sdk
{
	// Token: 0x0200046E RID: 1134
	public class JSONArray : JSONNode, IEnumerable
	{
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x06002598 RID: 9624 RVA: 0x0000216D File Offset: 0x0000036D
		public override JSONNode Item
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600259A RID: 9626 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerable<JSONNode> Childs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600259B RID: 9627 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Add(string aKey, JSONNode aItem)
		{
		}

		// Token: 0x0600259C RID: 9628 RVA: 0x0000216A File Offset: 0x0000036A
		public override JSONNode Remove(int aIndex)
		{
			return null;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0000216A File Offset: 0x0000036A
		public override JSONNode Remove(JSONNode aNode)
		{
			return null;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator GetEnumerator()
		{
			return null;
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString()
		{
			return null;
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString(string aPrefix)
		{
			return null;
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Serialize(BinaryWriter aWriter)
		{
		}

		// Token: 0x0400272E RID: 10030
		private List<JSONNode> m_List;
	}
}
