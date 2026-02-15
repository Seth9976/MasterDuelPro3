using System;
using System.Collections;

namespace Unity.Collections
{
	// Token: 0x0200004A RID: 74
	internal struct ListPair<Key, Value> where Value : IList
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00006908 File Offset: 0x00004B08
		public ListPair(Key k, Value v)
		{
			this.key = k;
			this.value = v;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006918 File Offset: 0x00004B18
		public override string ToString()
		{
			string result = string.Format("{0} = [", this.key);
			for (int v = 0; v < this.value.Count; v++)
			{
				string text = result;
				object obj = this.value[v];
				result = text + ((obj != null) ? obj.ToString() : null);
				if (v < this.value.Count - 1)
				{
					result += ", ";
				}
			}
			return result + "]";
		}

		// Token: 0x040000B9 RID: 185
		public Key key;

		// Token: 0x040000BA RID: 186
		public Value value;
	}
}
