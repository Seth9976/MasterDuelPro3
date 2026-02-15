using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Serialization
{
	// Token: 0x0200014F RID: 335
	internal class CaseInsensitiveKeyComparer : CaseInsensitiveComparer, IEqualityComparer
	{
		// Token: 0x06001096 RID: 4246 RVA: 0x0005110B File Offset: 0x0004F30B
		public CaseInsensitiveKeyComparer()
			: base(CultureInfo.CurrentCulture)
		{
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x00051118 File Offset: 0x0004F318
		bool IEqualityComparer.Equals(object x, object y)
		{
			return base.Compare(x, y) == 0;
		}

		// Token: 0x06001098 RID: 4248 RVA: 0x00051125 File Offset: 0x0004F325
		int IEqualityComparer.GetHashCode(object obj)
		{
			string text = obj as string;
			if (text == null)
			{
				throw new ArgumentException(null, "obj");
			}
			return text.ToUpper(CultureInfo.CurrentCulture).GetHashCode();
		}
	}
}
