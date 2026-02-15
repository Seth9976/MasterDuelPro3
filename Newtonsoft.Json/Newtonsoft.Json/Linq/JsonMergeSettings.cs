using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200017E RID: 382
	public class JsonMergeSettings
	{
		// Token: 0x06000C9D RID: 3229 RVA: 0x00038062 File Offset: 0x00036262
		public JsonMergeSettings()
		{
			this._propertyNameComparison = StringComparison.Ordinal;
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000C9E RID: 3230 RVA: 0x00038071 File Offset: 0x00036271
		// (set) Token: 0x06000C9F RID: 3231 RVA: 0x00038079 File Offset: 0x00036279
		public MergeArrayHandling MergeArrayHandling
		{
			get
			{
				return this._mergeArrayHandling;
			}
			set
			{
				if (value < MergeArrayHandling.Concat || value > MergeArrayHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeArrayHandling = value;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000CA0 RID: 3232 RVA: 0x00038095 File Offset: 0x00036295
		// (set) Token: 0x06000CA1 RID: 3233 RVA: 0x0003809D File Offset: 0x0003629D
		public MergeNullValueHandling MergeNullValueHandling
		{
			get
			{
				return this._mergeNullValueHandling;
			}
			set
			{
				if (value < MergeNullValueHandling.Ignore || value > MergeNullValueHandling.Merge)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._mergeNullValueHandling = value;
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000CA2 RID: 3234 RVA: 0x000380B9 File Offset: 0x000362B9
		// (set) Token: 0x06000CA3 RID: 3235 RVA: 0x000380C1 File Offset: 0x000362C1
		public StringComparison PropertyNameComparison
		{
			get
			{
				return this._propertyNameComparison;
			}
			set
			{
				if (value < StringComparison.CurrentCulture || value > StringComparison.OrdinalIgnoreCase)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._propertyNameComparison = value;
			}
		}

		// Token: 0x040006FF RID: 1791
		private MergeArrayHandling _mergeArrayHandling;

		// Token: 0x04000700 RID: 1792
		private MergeNullValueHandling _mergeNullValueHandling;

		// Token: 0x04000701 RID: 1793
		private StringComparison _propertyNameComparison;
	}
}
