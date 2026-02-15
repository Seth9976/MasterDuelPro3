using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000184 RID: 388
	internal class QNameComparer : IComparer
	{
		// Token: 0x0600124D RID: 4685 RVA: 0x000573D4 File Offset: 0x000555D4
		public int Compare(object o1, object o2)
		{
			XmlQualifiedName xmlQualifiedName = (XmlQualifiedName)o1;
			XmlQualifiedName xmlQualifiedName2 = (XmlQualifiedName)o2;
			int num = string.Compare(xmlQualifiedName.Namespace, xmlQualifiedName2.Namespace, StringComparison.Ordinal);
			if (num == 0)
			{
				return string.Compare(xmlQualifiedName.Name, xmlQualifiedName2.Name, StringComparison.Ordinal);
			}
			return num;
		}
	}
}
