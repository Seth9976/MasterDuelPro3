using System;
using System.Collections;
using System.Xml.XPath;
using MS.Internal.Xml.XPath;

namespace System.Xml.Schema
{
	// Token: 0x0200020B RID: 523
	internal class Asttree
	{
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001A08 RID: 6664 RVA: 0x000985BA File Offset: 0x000967BA
		internal ArrayList SubtreeArray
		{
			get
			{
				return this._fAxisArray;
			}
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000985C2 File Offset: 0x000967C2
		public Asttree(string xPath, bool isField, XmlNamespaceManager nsmgr)
		{
			this._xpathexpr = xPath;
			this._isField = isField;
			this._nsmgr = nsmgr;
			this.CompileXPath(xPath, isField, nsmgr);
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x000985E8 File Offset: 0x000967E8
		private static bool IsNameTest(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Child && ast.NodeType == XPathNodeType.Element;
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x000985FE File Offset: 0x000967FE
		internal static bool IsAttribute(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Attribute && ast.NodeType == XPathNodeType.Attribute;
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00098614 File Offset: 0x00096814
		private static bool IsDescendantOrSelf(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.DescendantOrSelf && ast.NodeType == XPathNodeType.All && ast.AbbrAxis;
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00098631 File Offset: 0x00096831
		internal static bool IsSelf(Axis ast)
		{
			return ast.TypeOfAxis == Axis.AxisType.Self && ast.NodeType == XPathNodeType.All && ast.AbbrAxis;
		}

		// Token: 0x06001A0E RID: 6670 RVA: 0x00098650 File Offset: 0x00096850
		public void CompileXPath(string xPath, bool isField, XmlNamespaceManager nsmgr)
		{
			if (xPath == null || xPath.Length == 0)
			{
				throw new XmlSchemaException("The XPath for selector or field cannot be empty.", string.Empty);
			}
			string[] array = xPath.Split('|', StringSplitOptions.None);
			ArrayList arrayList = new ArrayList(array.Length);
			this._fAxisArray = new ArrayList(array.Length);
			try
			{
				for (int i = 0; i < array.Length; i++)
				{
					Axis axis = (Axis)XPathParser.ParseXPathExpression(array[i]);
					arrayList.Add(axis);
				}
			}
			catch
			{
				throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
			}
			int j = 0;
			while (j < arrayList.Count)
			{
				Axis axis2 = (Axis)arrayList[j];
				Axis axis3;
				if ((axis3 = axis2) == null)
				{
					throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
				}
				Axis axis4 = axis3;
				if (Asttree.IsAttribute(axis3))
				{
					if (!isField)
					{
						throw new XmlSchemaException("'{0}' is an invalid XPath for selector. Selector cannot have an XPath selection with an attribute node.", xPath);
					}
					this.SetURN(axis3, nsmgr);
					try
					{
						axis3 = (Axis)axis3.Input;
						goto IL_0122;
					}
					catch
					{
						throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
					}
					goto IL_00D7;
				}
				IL_0122:
				if (axis3 == null || (!Asttree.IsNameTest(axis3) && !Asttree.IsSelf(axis3)))
				{
					axis4.Input = null;
					if (axis3 == null)
					{
						if (Asttree.IsSelf(axis2) && axis2.Input != null)
						{
							this._fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree((Axis)axis2.Input), false));
						}
						else
						{
							this._fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree(axis2), false));
						}
					}
					else
					{
						if (!Asttree.IsDescendantOrSelf(axis3))
						{
							throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
						}
						try
						{
							axis3 = (Axis)axis3.Input;
						}
						catch
						{
							throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
						}
						if (axis3 == null || !Asttree.IsSelf(axis3) || axis3.Input != null)
						{
							throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
						}
						if (Asttree.IsSelf(axis2) && axis2.Input != null)
						{
							this._fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree((Axis)axis2.Input), true));
						}
						else
						{
							this._fAxisArray.Add(new ForwardAxis(DoubleLinkAxis.ConvertTree(axis2), true));
						}
					}
					j++;
					continue;
				}
				IL_00D7:
				if (Asttree.IsSelf(axis3) && axis2 != axis3)
				{
					axis4.Input = axis3.Input;
				}
				else
				{
					axis4 = axis3;
					if (Asttree.IsNameTest(axis3))
					{
						this.SetURN(axis3, nsmgr);
					}
				}
				try
				{
					axis3 = (Axis)axis3.Input;
				}
				catch
				{
					throw new XmlSchemaException("'{0}' is an invalid XPath for selector or field.", xPath);
				}
				goto IL_0122;
			}
		}

		// Token: 0x06001A0F RID: 6671 RVA: 0x000988DC File Offset: 0x00096ADC
		private void SetURN(Axis axis, XmlNamespaceManager nsmgr)
		{
			if (axis.Prefix.Length != 0)
			{
				axis.Urn = nsmgr.LookupNamespace(axis.Prefix);
				if (axis.Urn == null)
				{
					throw new XmlSchemaException("The prefix '{0}' in XPath cannot be resolved.", axis.Prefix);
				}
			}
			else
			{
				if (axis.Name.Length != 0)
				{
					axis.Urn = null;
					return;
				}
				axis.Urn = "";
			}
		}

		// Token: 0x04000B17 RID: 2839
		private ArrayList _fAxisArray;

		// Token: 0x04000B18 RID: 2840
		private string _xpathexpr;

		// Token: 0x04000B19 RID: 2841
		private bool _isField;

		// Token: 0x04000B1A RID: 2842
		private XmlNamespaceManager _nsmgr;
	}
}
