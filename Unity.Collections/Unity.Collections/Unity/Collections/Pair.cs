using System;

namespace Unity.Collections
{
	// Token: 0x02000049 RID: 73
	internal struct Pair<Key, Value>
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x000068D6 File Offset: 0x00004AD6
		public Pair(Key k, Value v)
		{
			this.key = k;
			this.value = v;
		}

		// Token: 0x060001E8 RID: 488 RVA: 0x000068E6 File Offset: 0x00004AE6
		public override string ToString()
		{
			return string.Format("{0} = {1}", this.key, this.value);
		}

		// Token: 0x040000B7 RID: 183
		public Key key;

		// Token: 0x040000B8 RID: 184
		public Value value;
	}
}
