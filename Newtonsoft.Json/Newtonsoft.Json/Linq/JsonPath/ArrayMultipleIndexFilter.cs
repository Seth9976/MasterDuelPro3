using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x02000193 RID: 403
	[NullableContext(1)]
	[Nullable(0)]
	internal class ArrayMultipleIndexFilter : PathFilter
	{
		// Token: 0x06000E01 RID: 3585 RVA: 0x0003D39B File Offset: 0x0003B59B
		public ArrayMultipleIndexFilter(List<int> indexes)
		{
			this.Indexes = indexes;
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x0003D3AA File Offset: 0x0003B5AA
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			foreach (JToken t in current)
			{
				foreach (int num in this.Indexes)
				{
					JToken tokenIndex = PathFilter.GetTokenIndex(t, settings, num);
					if (tokenIndex != null)
					{
						yield return tokenIndex;
					}
				}
				List<int>.Enumerator enumerator2 = default(List<int>.Enumerator);
				t = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x04000771 RID: 1905
		internal List<int> Indexes;
	}
}
