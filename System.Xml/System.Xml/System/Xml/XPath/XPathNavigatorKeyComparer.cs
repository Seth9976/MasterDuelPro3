using System;
using System.Collections;
using MS.Internal.Xml.Cache;

namespace System.Xml.XPath
{
	// Token: 0x0200013E RID: 318
	internal class XPathNavigatorKeyComparer : IEqualityComparer
	{
		// Token: 0x06000FC0 RID: 4032 RVA: 0x0004DCD8 File Offset: 0x0004BED8
		bool IEqualityComparer.Equals(object obj1, object obj2)
		{
			XPathNavigator xpathNavigator = obj1 as XPathNavigator;
			XPathNavigator xpathNavigator2 = obj2 as XPathNavigator;
			return xpathNavigator != null && xpathNavigator2 != null && xpathNavigator.IsSamePosition(xpathNavigator2);
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x0004DD08 File Offset: 0x0004BF08
		int IEqualityComparer.GetHashCode(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			XPathDocumentNavigator xpathDocumentNavigator;
			int num;
			XPathNavigator xpathNavigator;
			if ((xpathDocumentNavigator = obj as XPathDocumentNavigator) != null)
			{
				num = xpathDocumentNavigator.GetPositionHashCode();
			}
			else if ((xpathNavigator = obj as XPathNavigator) != null)
			{
				object underlyingObject = xpathNavigator.UnderlyingObject;
				if (underlyingObject != null)
				{
					num = underlyingObject.GetHashCode();
				}
				else
				{
					num = (int)xpathNavigator.NodeType;
					num ^= xpathNavigator.LocalName.GetHashCode();
					num ^= xpathNavigator.Prefix.GetHashCode();
					num ^= xpathNavigator.NamespaceURI.GetHashCode();
				}
			}
			else
			{
				num = obj.GetHashCode();
			}
			return num;
		}
	}
}
