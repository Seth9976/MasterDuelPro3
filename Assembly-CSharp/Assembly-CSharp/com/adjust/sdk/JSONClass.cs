using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace com.adjust.sdk
{
	// Token: 0x02000470 RID: 1136
	public class JSONClass : JSONNode, IEnumerable
	{
		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x0000216A File Offset: 0x0000036A
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x0000216D File Offset: 0x0000036D
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

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060025A5 RID: 9637 RVA: 0x000029CC File Offset: 0x00000BCC
		public override int Count
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060025A6 RID: 9638 RVA: 0x0000216A File Offset: 0x0000036A
		public override IEnumerable<JSONNode> Childs
		{
			get
			{
				return null;
			}
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Add(string aKey, JSONNode aItem)
		{
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x0000216A File Offset: 0x0000036A
		public override JSONNode Remove(string aKey)
		{
			return null;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x0000216A File Offset: 0x0000036A
		public override JSONNode Remove(int aIndex)
		{
			return null;
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x0000216A File Offset: 0x0000036A
		public override JSONNode Remove(JSONNode aNode)
		{
			return null;
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0000216A File Offset: 0x0000036A
		public IEnumerator GetEnumerator()
		{
			return null;
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString()
		{
			return null;
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x0000216A File Offset: 0x0000036A
		public override string ToString(string aPrefix)
		{
			return null;
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Serialize(BinaryWriter aWriter)
		{
		}

		// Token: 0x04002737 RID: 10039
		private Dictionary<string, JSONNode> m_Dict;
	}
}
