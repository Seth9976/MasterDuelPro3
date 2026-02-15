using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityEngine.UIElements
{
	// Token: 0x020004AF RID: 1199
	public class UxmlEnumeration : UxmlTypeRestriction
	{
		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x0007CF58 File Offset: 0x0007B158
		// (set) Token: 0x06002235 RID: 8757 RVA: 0x0007CF70 File Offset: 0x0007B170
		public IEnumerable<string> values
		{
			get
			{
				return this.m_Values;
			}
			set
			{
				this.m_Values = value.ToList<string>();
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x0007CF80 File Offset: 0x0007B180
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlEnumeration otherE = other as UxmlEnumeration;
			bool flag = otherE == null;
			return !flag && this.values.All(new Func<string, bool>(otherE.values.Contains<string>)) && this.values.Count<string>() == otherE.values.Count<string>();
		}

		// Token: 0x04000F20 RID: 3872
		private List<string> m_Values = new List<string>();
	}
}
