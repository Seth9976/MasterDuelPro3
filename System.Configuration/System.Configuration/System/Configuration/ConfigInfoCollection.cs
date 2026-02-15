using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Configuration
{
	// Token: 0x0200003F RID: 63
	internal class ConfigInfoCollection : NameObjectCollectionBase
	{
		// Token: 0x060001A6 RID: 422 RVA: 0x0000667E File Offset: 0x0000487E
		public ConfigInfoCollection()
			: base(StringComparer.Ordinal)
		{
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007270 File Offset: 0x00005470
		public ICollection AllKeys
		{
			get
			{
				return this.Keys;
			}
		}

		// Token: 0x17000081 RID: 129
		public ConfigInfo this[string name]
		{
			get
			{
				return (ConfigInfo)base.BaseGet(name);
			}
			set
			{
				base.BaseSet(name, value);
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00007290 File Offset: 0x00005490
		public void Add(string name, ConfigInfo config)
		{
			base.BaseAdd(name, config);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000729A File Offset: 0x0000549A
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000072A2 File Offset: 0x000054A2
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}
	}
}
