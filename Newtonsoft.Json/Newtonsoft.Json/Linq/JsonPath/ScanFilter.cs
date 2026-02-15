using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001A7 RID: 423
	[NullableContext(2)]
	[Nullable(0)]
	internal class ScanFilter : PathFilter
	{
		// Token: 0x06000E7E RID: 3710 RVA: 0x0003FE6B File Offset: 0x0003E06B
		public ScanFilter(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x0003FE7A File Offset: 0x0003E07A
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			foreach (JToken c in current)
			{
				if (this.Name == null)
				{
					yield return c;
				}
				JToken value = c;
				for (;;)
				{
					JContainer jcontainer = value as JContainer;
					value = PathFilter.GetNextScanValue(c, jcontainer, value);
					if (value == null)
					{
						break;
					}
					JProperty jproperty = value as JProperty;
					if (jproperty != null)
					{
						if (jproperty.Name == this.Name)
						{
							yield return jproperty.Value;
						}
					}
					else if (this.Name == null)
					{
						yield return value;
					}
				}
				value = null;
				c = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x040007D9 RID: 2009
		internal string Name;
	}
}
