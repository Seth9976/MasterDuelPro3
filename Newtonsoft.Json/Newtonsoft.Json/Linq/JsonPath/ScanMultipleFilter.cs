using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001A9 RID: 425
	[NullableContext(1)]
	[Nullable(0)]
	internal class ScanMultipleFilter : PathFilter
	{
		// Token: 0x06000E89 RID: 3721 RVA: 0x000400FB File Offset: 0x0003E2FB
		public ScanMultipleFilter(List<string> names)
		{
			this._names = names;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x0004010A File Offset: 0x0003E30A
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			foreach (JToken c in current)
			{
				JToken value = c;
				for (;;)
				{
					JContainer jcontainer = value as JContainer;
					value = PathFilter.GetNextScanValue(c, jcontainer, value);
					if (value == null)
					{
						break;
					}
					JProperty property = value as JProperty;
					if (property != null)
					{
						foreach (string text in this._names)
						{
							if (property.Name == text)
							{
								yield return property.Value;
							}
						}
						List<string>.Enumerator enumerator2 = default(List<string>.Enumerator);
					}
					property = null;
				}
				value = null;
				c = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040007E3 RID: 2019
		private List<string> _names;
	}
}
