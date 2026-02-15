using System;
using System.Collections.Generic;

namespace MDPro3
{
	// Token: 0x02001215 RID: 4629
	public class MultiStringMaster
	{
		// Token: 0x0600893B RID: 35131 RVA: 0x00108E64 File Offset: 0x00107064
		public void Add(string str)
		{
			bool exist = false;
			for (int i = 0; i < this.strings.Count; i++)
			{
				if (this.strings[i].str == str)
				{
					exist = true;
					this.strings[i].count++;
				}
			}
			if (!exist)
			{
				MultiStringMaster.Part t = new MultiStringMaster.Part();
				t.count = 1;
				t.str = str;
				this.strings.Add(t);
			}
			this.ReCreateString();
		}

		// Token: 0x0600893C RID: 35132 RVA: 0x00108EE8 File Offset: 0x001070E8
		public void Remove(string str, bool all = false)
		{
			MultiStringMaster.Part t = null;
			for (int i = 0; i < this.strings.Count; i++)
			{
				if (this.strings[i].str.Replace(str, "miaowu") != this.strings[i].str)
				{
					t = this.strings[i];
				}
			}
			if (t != null)
			{
				if (all)
				{
					this.strings.Remove(t);
				}
				else if (t.count == 1)
				{
					this.strings.Remove(t);
				}
				else
				{
					t.count--;
				}
			}
			this.ReCreateString();
		}

		// Token: 0x0600893D RID: 35133 RVA: 0x00108F90 File Offset: 0x00107190
		private void ReCreateString()
		{
			this.managedString = "";
			for (int i = 0; i < this.strings.Count; i++)
			{
				if (this.strings[i].count == 1)
				{
					this.managedString = this.managedString + this.strings[i].str + "\n";
				}
				else
				{
					this.managedString = string.Concat(new string[]
					{
						this.managedString,
						this.strings[i].str,
						"*",
						this.strings[i].count.ToString(),
						"\n"
					});
				}
			}
		}

		// Token: 0x0600893E RID: 35134 RVA: 0x00109058 File Offset: 0x00107258
		public void Clear()
		{
			this.strings.Clear();
			this.managedString = "";
		}

		// Token: 0x0400C45F RID: 50271
		public string managedString = "";

		// Token: 0x0400C460 RID: 50272
		public List<MultiStringMaster.Part> strings = new List<MultiStringMaster.Part>();

		// Token: 0x02001216 RID: 4630
		public class Part
		{
			// Token: 0x0400C461 RID: 50273
			public int count;

			// Token: 0x0400C462 RID: 50274
			public string str;
		}
	}
}
