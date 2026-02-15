using System;
using System.Collections;

namespace Novell.Directory.Ldap.Utilclass
{
	// Token: 0x02000054 RID: 84
	public class AttributeQualifier
	{
		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600032E RID: 814 RVA: 0x0000D993 File Offset: 0x0000BB93
		public virtual string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600032F RID: 815 RVA: 0x0000D99C File Offset: 0x0000BB9C
		public virtual string[] Values
		{
			get
			{
				string[] array = null;
				if (this.values.Count > 0)
				{
					array = new string[this.values.Count];
					for (int i = 0; i < this.values.Count; i++)
					{
						array[i] = (string)this.values[i];
					}
				}
				return array;
			}
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		public AttributeQualifier(string name, string[] value_Renamed)
		{
			if (name == null || value_Renamed == null)
			{
				throw new ArgumentException("A null name or value was passed in for a schema definition qualifier");
			}
			this.name = name;
			this.values = new ArrayList(5);
			for (int i = 0; i < value_Renamed.Length; i++)
			{
				this.values.Add(value_Renamed[i]);
			}
		}

		// Token: 0x040001B8 RID: 440
		internal string name;

		// Token: 0x040001B9 RID: 441
		internal ArrayList values;
	}
}
