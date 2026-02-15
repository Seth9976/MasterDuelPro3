using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x02000197 RID: 407
	[NullableContext(2)]
	[Nullable(0)]
	internal class FieldFilter : PathFilter
	{
		// Token: 0x06000E1F RID: 3615 RVA: 0x0003DADB File Offset: 0x0003BCDB
		public FieldFilter(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000E20 RID: 3616 RVA: 0x0003DAEA File Offset: 0x0003BCEA
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			foreach (JToken jtoken in current)
			{
				JObject jobject = jtoken as JObject;
				if (jobject != null)
				{
					if (this.Name != null)
					{
						JToken jtoken2 = jobject[this.Name];
						if (jtoken2 != null)
						{
							yield return jtoken2;
						}
						else if (settings != null && settings.ErrorWhenNoMatch)
						{
							throw new JsonException("Property '{0}' does not exist on JObject.".FormatWith(CultureInfo.InvariantCulture, this.Name));
						}
					}
					else
					{
						foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
						{
							yield return keyValuePair.Value;
						}
						IEnumerator<KeyValuePair<string, JToken>> enumerator2 = null;
					}
				}
				else if (settings != null && settings.ErrorWhenNoMatch)
				{
					throw new JsonException("Property '{0}' not valid on {1}.".FormatWith(CultureInfo.InvariantCulture, this.Name ?? "*", jtoken.GetType().Name));
				}
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0400078E RID: 1934
		internal string Name;
	}
}
