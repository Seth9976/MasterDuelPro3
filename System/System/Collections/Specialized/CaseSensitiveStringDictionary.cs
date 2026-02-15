using System;

namespace System.Collections.Specialized
{
	// Token: 0x0200030D RID: 781
	internal class CaseSensitiveStringDictionary : StringDictionary
	{
		// Token: 0x1700040E RID: 1038
		public override string this[string key]
		{
			get
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				return (string)this.contents[key];
			}
			set
			{
				if (key == null)
				{
					throw new ArgumentNullException("key");
				}
				this.contents[key] = value;
			}
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00054A65 File Offset: 0x00052C65
		public override void Add(string key, string value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Add(key, value);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00054A82 File Offset: 0x00052C82
		public override bool ContainsKey(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			return this.contents.ContainsKey(key);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00054A9E File Offset: 0x00052C9E
		public override void Remove(string key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			this.contents.Remove(key);
		}
	}
}
