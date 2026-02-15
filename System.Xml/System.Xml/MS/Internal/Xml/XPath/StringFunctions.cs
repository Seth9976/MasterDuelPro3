using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000369 RID: 873
	internal sealed class StringFunctions : ValueQuery
	{
		// Token: 0x060026A1 RID: 9889 RVA: 0x000D7A39 File Offset: 0x000D5C39
		public StringFunctions(Function.FunctionType funcType, IList<Query> argList)
		{
			this._funcType = funcType;
			this._argList = argList;
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x000D7A50 File Offset: 0x000D5C50
		private StringFunctions(StringFunctions other)
			: base(other)
		{
			this._funcType = other._funcType;
			Query[] array = new Query[other._argList.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = Query.Clone(other._argList[i]);
			}
			this._argList = array;
		}

		// Token: 0x060026A3 RID: 9891 RVA: 0x000D7AAC File Offset: 0x000D5CAC
		public override void SetXsltContext(XsltContext context)
		{
			for (int i = 0; i < this._argList.Count; i++)
			{
				this._argList[i].SetXsltContext(context);
			}
		}

		// Token: 0x060026A4 RID: 9892 RVA: 0x000D7AE4 File Offset: 0x000D5CE4
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			switch (this._funcType)
			{
			case Function.FunctionType.FuncString:
				return this.toString(nodeIterator);
			case Function.FunctionType.FuncConcat:
				return this.Concat(nodeIterator);
			case Function.FunctionType.FuncStartsWith:
				return this.StartsWith(nodeIterator);
			case Function.FunctionType.FuncContains:
				return this.Contains(nodeIterator);
			case Function.FunctionType.FuncSubstringBefore:
				return this.SubstringBefore(nodeIterator);
			case Function.FunctionType.FuncSubstringAfter:
				return this.SubstringAfter(nodeIterator);
			case Function.FunctionType.FuncSubstring:
				return this.Substring(nodeIterator);
			case Function.FunctionType.FuncStringLength:
				return this.StringLength(nodeIterator);
			case Function.FunctionType.FuncNormalize:
				return this.Normalize(nodeIterator);
			case Function.FunctionType.FuncTranslate:
				return this.Translate(nodeIterator);
			}
			return string.Empty;
		}

		// Token: 0x060026A5 RID: 9893 RVA: 0x000D7BA2 File Offset: 0x000D5DA2
		internal static string toString(double num)
		{
			return num.ToString("R", NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x060026A6 RID: 9894 RVA: 0x000499B2 File Offset: 0x00047BB2
		internal static string toString(bool b)
		{
			if (!b)
			{
				return "false";
			}
			return "true";
		}

		// Token: 0x060026A7 RID: 9895 RVA: 0x000D7BB8 File Offset: 0x000D5DB8
		private string toString(XPathNodeIterator nodeIterator)
		{
			if (this._argList.Count <= 0)
			{
				return nodeIterator.Current.Value;
			}
			object obj = this._argList[0].Evaluate(nodeIterator);
			switch (base.GetXPathType(obj))
			{
			case XPathResultType.String:
				return (string)obj;
			case XPathResultType.Boolean:
				if (!(bool)obj)
				{
					return "false";
				}
				return "true";
			case XPathResultType.NodeSet:
			{
				XPathNavigator xpathNavigator = this._argList[0].Advance();
				if (xpathNavigator == null)
				{
					return string.Empty;
				}
				return xpathNavigator.Value;
			}
			case (XPathResultType)4:
				return ((XPathNavigator)obj).Value;
			default:
				return StringFunctions.toString((double)obj);
			}
		}

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x060026A8 RID: 9896 RVA: 0x000D7C6B File Offset: 0x000D5E6B
		public override XPathResultType StaticType
		{
			get
			{
				if (this._funcType == Function.FunctionType.FuncStringLength)
				{
					return XPathResultType.Number;
				}
				if (this._funcType == Function.FunctionType.FuncStartsWith || this._funcType == Function.FunctionType.FuncContains)
				{
					return XPathResultType.Boolean;
				}
				return XPathResultType.String;
			}
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x000D7C90 File Offset: 0x000D5E90
		private string Concat(XPathNodeIterator nodeIterator)
		{
			int i = 0;
			StringBuilder stringBuilder = new StringBuilder();
			while (i < this._argList.Count)
			{
				stringBuilder.Append(this._argList[i++].Evaluate(nodeIterator).ToString());
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x000D7CE0 File Offset: 0x000D5EE0
		private bool StartsWith(XPathNodeIterator nodeIterator)
		{
			string text = this._argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this._argList[1].Evaluate(nodeIterator).ToString();
			return text.Length >= text2.Length && string.CompareOrdinal(text, 0, text2, 0, text2.Length) == 0;
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x000D7D40 File Offset: 0x000D5F40
		private bool Contains(XPathNodeIterator nodeIterator)
		{
			string text = this._argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this._argList[1].Evaluate(nodeIterator).ToString();
			return StringFunctions.s_compareInfo.IndexOf(text, text2, CompareOptions.Ordinal) >= 0;
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x000D7D94 File Offset: 0x000D5F94
		private string SubstringBefore(XPathNodeIterator nodeIterator)
		{
			string text = this._argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this._argList[1].Evaluate(nodeIterator).ToString();
			if (text2.Length == 0)
			{
				return text2;
			}
			int num = StringFunctions.s_compareInfo.IndexOf(text, text2, CompareOptions.Ordinal);
			if (num >= 1)
			{
				return text.Substring(0, num);
			}
			return string.Empty;
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x000D7E00 File Offset: 0x000D6000
		private string SubstringAfter(XPathNodeIterator nodeIterator)
		{
			string text = this._argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this._argList[1].Evaluate(nodeIterator).ToString();
			if (text2.Length == 0)
			{
				return text;
			}
			int num = StringFunctions.s_compareInfo.IndexOf(text, text2, CompareOptions.Ordinal);
			if (num >= 0)
			{
				return text.Substring(num + text2.Length);
			}
			return string.Empty;
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x000D7E74 File Offset: 0x000D6074
		private string Substring(XPathNodeIterator nodeIterator)
		{
			string text = this._argList[0].Evaluate(nodeIterator).ToString();
			double num = XmlConvert.XPathRound(XmlConvert.ToXPathDouble(this._argList[1].Evaluate(nodeIterator))) - 1.0;
			if (double.IsNaN(num) || (double)text.Length <= num)
			{
				return string.Empty;
			}
			if (this._argList.Count != 3)
			{
				if (num < 0.0)
				{
					num = 0.0;
				}
				return text.Substring((int)num);
			}
			double num2 = XmlConvert.XPathRound(XmlConvert.ToXPathDouble(this._argList[2].Evaluate(nodeIterator)));
			if (double.IsNaN(num2))
			{
				return string.Empty;
			}
			if (num < 0.0 || num2 < 0.0)
			{
				num2 = num + num2;
				if (num2 <= 0.0)
				{
					return string.Empty;
				}
				num = 0.0;
			}
			double num3 = (double)text.Length - num;
			if (num2 > num3)
			{
				num2 = num3;
			}
			return text.Substring((int)num, (int)num2);
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x000D7F82 File Offset: 0x000D6182
		private double StringLength(XPathNodeIterator nodeIterator)
		{
			if (this._argList.Count > 0)
			{
				return (double)this._argList[0].Evaluate(nodeIterator).ToString().Length;
			}
			return (double)nodeIterator.Current.Value.Length;
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x000D7FC4 File Offset: 0x000D61C4
		private string Normalize(XPathNodeIterator nodeIterator)
		{
			string text;
			if (this._argList.Count > 0)
			{
				text = this._argList[0].Evaluate(nodeIterator).ToString();
			}
			else
			{
				text = nodeIterator.Current.Value;
			}
			int num = -1;
			char[] array = text.ToCharArray();
			bool flag = false;
			XmlCharType instance = XmlCharType.Instance;
			for (int i = 0; i < array.Length; i++)
			{
				if (!instance.IsWhiteSpace(array[i]))
				{
					flag = true;
					num++;
					array[num] = array[i];
				}
				else if (flag)
				{
					flag = false;
					num++;
					array[num] = ' ';
				}
			}
			if (num > -1 && array[num] == ' ')
			{
				num--;
			}
			return new string(array, 0, num + 1);
		}

		// Token: 0x060026B1 RID: 9905 RVA: 0x000D806C File Offset: 0x000D626C
		private string Translate(XPathNodeIterator nodeIterator)
		{
			string text = this._argList[0].Evaluate(nodeIterator).ToString();
			string text2 = this._argList[1].Evaluate(nodeIterator).ToString();
			string text3 = this._argList[2].Evaluate(nodeIterator).ToString();
			int num = -1;
			char[] array = text.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				int num2 = text2.IndexOf(array[i]);
				if (num2 != -1)
				{
					if (num2 < text3.Length)
					{
						num++;
						array[num] = text3[num2];
					}
				}
				else
				{
					num++;
					array[num] = array[i];
				}
			}
			return new string(array, 0, num + 1);
		}

		// Token: 0x060026B2 RID: 9906 RVA: 0x000D811B File Offset: 0x000D631B
		public override XPathNodeIterator Clone()
		{
			return new StringFunctions(this);
		}

		// Token: 0x04001284 RID: 4740
		private Function.FunctionType _funcType;

		// Token: 0x04001285 RID: 4741
		private IList<Query> _argList;

		// Token: 0x04001286 RID: 4742
		private static readonly CompareInfo s_compareInfo = CultureInfo.InvariantCulture.CompareInfo;
	}
}
