using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000269 RID: 617
	internal struct TypeTable
	{
		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x0600166C RID: 5740 RVA: 0x000652A4 File Offset: 0x000634A4
		public IEnumerable<string> names
		{
			get
			{
				return this.table.Keys.Select((InternedString x) => x.ToString());
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x0600166D RID: 5741 RVA: 0x000652D5 File Offset: 0x000634D5
		public IEnumerable<InternedString> internedNames
		{
			get
			{
				return this.table.Keys;
			}
		}

		// Token: 0x0600166E RID: 5742 RVA: 0x000652E2 File Offset: 0x000634E2
		public void Initialize()
		{
			this.table = new Dictionary<InternedString, Type>();
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x000652F0 File Offset: 0x000634F0
		public InternedString FindNameForType(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			foreach (KeyValuePair<InternedString, Type> pair in this.table)
			{
				if (pair.Value == type)
				{
					return pair.Key;
				}
			}
			return default(InternedString);
		}

		// Token: 0x06001670 RID: 5744 RVA: 0x00065374 File Offset: 0x00063574
		public void AddTypeRegistration(string name, Type type)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("Name cannot be null or empty", "name");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			InternedString internedName = new InternedString(name);
			this.table[internedName] = type;
		}

		// Token: 0x06001671 RID: 5745 RVA: 0x000653C4 File Offset: 0x000635C4
		public Type LookupTypeRegistration(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				return null;
			}
			if (this.table == null)
			{
				throw new InvalidOperationException("Input System not yet initialized");
			}
			InternedString internedName = new InternedString(name);
			Type type;
			if (this.table.TryGetValue(internedName, out type))
			{
				return type;
			}
			return null;
		}

		// Token: 0x04000CC2 RID: 3266
		public Dictionary<InternedString, Type> table;
	}
}
