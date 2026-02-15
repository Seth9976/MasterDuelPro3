using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x02000195 RID: 405
	internal class ArraySliceFilter : PathFilter
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000E0D RID: 3597 RVA: 0x0003D5FB File Offset: 0x0003B7FB
		// (set) Token: 0x06000E0E RID: 3598 RVA: 0x0003D603 File Offset: 0x0003B803
		public int? Start { get; set; }

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000E0F RID: 3599 RVA: 0x0003D60C File Offset: 0x0003B80C
		// (set) Token: 0x06000E10 RID: 3600 RVA: 0x0003D614 File Offset: 0x0003B814
		public int? End { get; set; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0003D61D File Offset: 0x0003B81D
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0003D625 File Offset: 0x0003B825
		public int? Step { get; set; }

		// Token: 0x06000E13 RID: 3603 RVA: 0x0003D62E File Offset: 0x0003B82E
		[NullableContext(1)]
		public override IEnumerable<JToken> ExecuteFilter(JToken root, IEnumerable<JToken> current, [Nullable(2)] JsonSelectSettings settings)
		{
			int? num = this.Step;
			int num2 = 0;
			if ((num.GetValueOrDefault() == num2) & (num != null))
			{
				throw new JsonException("Step cannot be zero.");
			}
			foreach (JToken jtoken in current)
			{
				JArray a = jtoken as JArray;
				if (a != null)
				{
					int stepCount = this.Step ?? 1;
					int num3 = this.Start ?? ((stepCount > 0) ? 0 : (a.Count - 1));
					int stopIndex = this.End ?? ((stepCount > 0) ? a.Count : (-1));
					num = this.Start;
					num2 = 0;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						num3 = a.Count + num3;
					}
					num = this.End;
					num2 = 0;
					if ((num.GetValueOrDefault() < num2) & (num != null))
					{
						stopIndex = a.Count + stopIndex;
					}
					num3 = Math.Max(num3, (stepCount > 0) ? 0 : int.MinValue);
					num3 = Math.Min(num3, (stepCount > 0) ? a.Count : (a.Count - 1));
					stopIndex = Math.Max(stopIndex, -1);
					stopIndex = Math.Min(stopIndex, a.Count);
					bool positiveStep = stepCount > 0;
					if (this.IsValid(num3, stopIndex, positiveStep))
					{
						int i = num3;
						while (this.IsValid(i, stopIndex, positiveStep))
						{
							yield return a[i];
							i += stepCount;
						}
					}
					else if (settings != null && settings.ErrorWhenNoMatch)
					{
						throw new JsonException("Array slice of {0} to {1} returned no results.".FormatWith(CultureInfo.InvariantCulture, (this.Start != null) ? this.Start.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*", (this.End != null) ? this.End.GetValueOrDefault().ToString(CultureInfo.InvariantCulture) : "*"));
					}
				}
				else if (settings != null && settings.ErrorWhenNoMatch)
				{
					throw new JsonException("Array slice is not valid on {0}.".FormatWith(CultureInfo.InvariantCulture, jtoken.GetType().Name));
				}
				a = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x0003D64C File Offset: 0x0003B84C
		private bool IsValid(int index, int stopIndex, bool positiveStep)
		{
			if (positiveStep)
			{
				return index < stopIndex;
			}
			return index > stopIndex;
		}
	}
}
