using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.Cache
{
	// Token: 0x02000383 RID: 899
	internal abstract class XPathNodeHelper
	{
		// Token: 0x060027A7 RID: 10151 RVA: 0x000DB3CC File Offset: 0x000D95CC
		public static int GetLocalNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			if (pageElem[idxElem].HasNamespaceDecls)
			{
				return pageElem[idxElem].Document.LookupNamespaces(pageElem, idxElem, out pageNmsp);
			}
			pageNmsp = null;
			return 0;
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000DB3F8 File Offset: 0x000D95F8
		public static int GetInScopeNamespaces(XPathNode[] pageElem, int idxElem, out XPathNode[] pageNmsp)
		{
			if (pageElem[idxElem].NodeType == XPathNodeType.Element)
			{
				XPathDocument document = pageElem[idxElem].Document;
				while (!pageElem[idxElem].HasNamespaceDecls)
				{
					idxElem = pageElem[idxElem].GetParent(out pageElem);
					if (idxElem == 0)
					{
						return document.GetXmlNamespaceNode(out pageNmsp);
					}
				}
				return document.LookupNamespaces(pageElem, idxElem, out pageNmsp);
			}
			pageNmsp = null;
			return 0;
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x000DB45A File Offset: 0x000D965A
		public static bool GetFirstAttribute(ref XPathNode[] pageNode, ref int idxNode)
		{
			if (pageNode[idxNode].HasAttribute)
			{
				XPathNodeHelper.GetChild(ref pageNode, ref idxNode);
				return true;
			}
			return false;
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x000DB478 File Offset: 0x000D9678
		public static bool GetNextAttribute(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array;
			int sibling = pageNode[idxNode].GetSibling(out array);
			if (sibling != 0 && array[sibling].NodeType == XPathNodeType.Attribute)
			{
				pageNode = array;
				idxNode = sibling;
				return true;
			}
			return false;
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x000DB4B4 File Offset: 0x000D96B4
		public static bool GetContentChild(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasContentChild)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (array[num].NodeType == XPathNodeType.Attribute)
				{
					num = array[num].GetSibling(out array);
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x000DB508 File Offset: 0x000D9708
		public static bool GetContentSibling(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (!array[num].IsAttrNmsp)
			{
				num = array[num].GetSibling(out array);
				if (num != 0)
				{
					pageNode = array;
					idxNode = num;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060027AD RID: 10157 RVA: 0x000DB544 File Offset: 0x000D9744
		public static bool GetParent(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			num = array[num].GetParent(out array);
			if (num != 0)
			{
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x060027AE RID: 10158 RVA: 0x000DB572 File Offset: 0x000D9772
		public static int GetLocation(XPathNode[] pageNode, int idxNode)
		{
			return (pageNode[0].PageInfo.PageNumber << 16) | idxNode;
		}

		// Token: 0x060027AF RID: 10159 RVA: 0x000DB58C File Offset: 0x000D978C
		public static bool GetElementChild(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasElementChild)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (!array[num].ElementMatch(localName, namespaceName))
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x060027B0 RID: 10160 RVA: 0x000DB5E4 File Offset: 0x000D97E4
		public static bool GetElementSibling(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].NodeType != XPathNodeType.Attribute)
			{
				do
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				while (!array[num].ElementMatch(localName, namespaceName));
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x060027B1 RID: 10161 RVA: 0x000DB634 File Offset: 0x000D9834
		public static bool GetContentChild(ref XPathNode[] pageNode, ref int idxNode, XPathNodeType typ)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasContentChild)
			{
				int contentKindMask = XPathNavigator.GetContentKindMask(typ);
				XPathNodeHelper.GetChild(ref array, ref num);
				while (((1 << (int)array[num].NodeType) & contentKindMask) == 0)
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				if (typ == XPathNodeType.Attribute)
				{
					return false;
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x000DB69C File Offset: 0x000D989C
		public static bool GetContentSibling(ref XPathNode[] pageNode, ref int idxNode, XPathNodeType typ)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			int contentKindMask = XPathNavigator.GetContentKindMask(typ);
			if (array[num].NodeType != XPathNodeType.Attribute)
			{
				do
				{
					num = array[num].GetSibling(out array);
					if (num == 0)
					{
						return false;
					}
				}
				while (((1 << (int)array[num].NodeType) & contentKindMask) == 0);
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x060027B3 RID: 10163 RVA: 0x000DB6F8 File Offset: 0x000D98F8
		public static bool GetAttribute(ref XPathNode[] pageNode, ref int idxNode, string localName, string namespaceName)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			if (array[num].HasAttribute)
			{
				XPathNodeHelper.GetChild(ref array, ref num);
				while (!array[num].NameMatch(localName, namespaceName))
				{
					num = array[num].GetSibling(out array);
					if (num == 0 || array[num].NodeType != XPathNodeType.Attribute)
					{
						return false;
					}
				}
				pageNode = array;
				idxNode = num;
				return true;
			}
			return false;
		}

		// Token: 0x060027B4 RID: 10164 RVA: 0x000DB75C File Offset: 0x000D995C
		public static bool GetElementFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd, string localName, string namespaceName)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			if (array[i].NodeType != XPathNodeType.Element || array[i].LocalName != localName)
			{
				i++;
				while (array != pageEnd || i > idxEnd)
				{
					while (i < array[0].PageInfo.NodeCount)
					{
						if (array[i].ElementMatch(localName, namespaceName))
						{
							goto IL_012C;
						}
						i++;
					}
					array = array[0].PageInfo.NextPage;
					i = 1;
					if (array == null)
					{
						return false;
					}
				}
				while (i != idxEnd)
				{
					if (array[i].ElementMatch(localName, namespaceName))
					{
						goto IL_012C;
					}
					i++;
				}
				return false;
			}
			int num = 0;
			if (pageEnd != null)
			{
				num = pageEnd[0].PageInfo.PageNumber;
				int num2 = array[0].PageInfo.PageNumber;
				if (num2 > num || (num2 == num && i >= idxEnd))
				{
					pageEnd = null;
				}
			}
			do
			{
				i = array[i].GetSimilarElement(out array);
				if (i == 0)
				{
					return false;
				}
				if (pageEnd != null)
				{
					int num2 = array[0].PageInfo.PageNumber;
					if (num2 > num || (num2 == num && i >= idxEnd))
					{
						return false;
					}
				}
			}
			while (array[i].LocalName != localName || !(array[i].NamespaceUri == namespaceName));
			IL_012C:
			pageCurrent = array;
			idxCurrent = i;
			return true;
		}

		// Token: 0x060027B5 RID: 10165 RVA: 0x000DB89C File Offset: 0x000D9A9C
		public static bool GetContentFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd, XPathNodeType typ)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			int contentKindMask = XPathNavigator.GetContentKindMask(typ);
			i++;
			while (array != pageEnd || i > idxEnd)
			{
				while (i < array[0].PageInfo.NodeCount)
				{
					if (((1 << (int)array[i].NodeType) & contentKindMask) != 0)
					{
						goto IL_0081;
					}
					i++;
				}
				array = array[0].PageInfo.NextPage;
				i = 1;
				if (array == null)
				{
					return false;
				}
				continue;
				IL_0081:
				pageCurrent = array;
				idxCurrent = i;
				return true;
			}
			while (i != idxEnd)
			{
				if (((1 << (int)array[i].NodeType) & contentKindMask) != 0)
				{
					goto IL_0081;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060027B6 RID: 10166 RVA: 0x000DB934 File Offset: 0x000D9B34
		public static bool GetTextFollowing(ref XPathNode[] pageCurrent, ref int idxCurrent, XPathNode[] pageEnd, int idxEnd)
		{
			XPathNode[] array = pageCurrent;
			int i = idxCurrent;
			i++;
			while (array != pageEnd || i > idxEnd)
			{
				while (i < array[0].PageInfo.NodeCount)
				{
					if (array[i].IsText || (array[i].NodeType == XPathNodeType.Element && array[i].HasCollapsedText))
					{
						goto IL_00AB;
					}
					i++;
				}
				array = array[0].PageInfo.NextPage;
				i = 1;
				if (array == null)
				{
					return false;
				}
				continue;
				IL_00AB:
				pageCurrent = array;
				idxCurrent = i;
				return true;
			}
			while (i != idxEnd)
			{
				if (array[i].IsText || (array[i].NodeType == XPathNodeType.Element && array[i].HasCollapsedText))
				{
					goto IL_00AB;
				}
				i++;
			}
			return false;
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x000DB9F4 File Offset: 0x000D9BF4
		public static bool GetNonDescendant(ref XPathNode[] pageNode, ref int idxNode)
		{
			XPathNode[] array = pageNode;
			int num = idxNode;
			while (!array[num].HasSibling)
			{
				num = array[num].GetParent(out array);
				if (num == 0)
				{
					return false;
				}
			}
			pageNode = array;
			idxNode = array[num].GetSibling(out pageNode);
			return true;
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x000DBA3C File Offset: 0x000D9C3C
		private static void GetChild(ref XPathNode[] pageNode, ref int idxNode)
		{
			int num = idxNode + 1;
			idxNode = num;
			if (num >= pageNode.Length)
			{
				pageNode = pageNode[0].PageInfo.NextPage;
				idxNode = 1;
			}
		}
	}
}
