using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200034B RID: 843
	internal class Function : AstNode
	{
		// Token: 0x060025DF RID: 9695 RVA: 0x000D54E4 File Offset: 0x000D36E4
		public Function(Function.FunctionType ftype, List<AstNode> argumentList)
		{
			this._functionType = ftype;
			this._argumentList = new List<AstNode>(argumentList);
		}

		// Token: 0x060025E0 RID: 9696 RVA: 0x000D54FF File Offset: 0x000D36FF
		public Function(string prefix, string name, List<AstNode> argumentList)
		{
			this._functionType = Function.FunctionType.FuncUserDefined;
			this._prefix = prefix;
			this._name = name;
			this._argumentList = new List<AstNode>(argumentList);
		}

		// Token: 0x060025E1 RID: 9697 RVA: 0x000D5529 File Offset: 0x000D3729
		public Function(Function.FunctionType ftype, AstNode arg)
		{
			this._functionType = ftype;
			this._argumentList = new List<AstNode>();
			this._argumentList.Add(arg);
		}

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x0003B0B7 File Offset: 0x000392B7
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Function;
			}
		}

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x060025E3 RID: 9699 RVA: 0x000D554F File Offset: 0x000D374F
		public override XPathResultType ReturnType
		{
			get
			{
				return Function.ReturnTypes[(int)this._functionType];
			}
		}

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x000D555D File Offset: 0x000D375D
		public Function.FunctionType TypeOfFunction
		{
			get
			{
				return this._functionType;
			}
		}

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x060025E5 RID: 9701 RVA: 0x000D5565 File Offset: 0x000D3765
		public List<AstNode> ArgumentList
		{
			get
			{
				return this._argumentList;
			}
		}

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000D556D File Offset: 0x000D376D
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x060025E7 RID: 9703 RVA: 0x000D5575 File Offset: 0x000D3775
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04001219 RID: 4633
		private Function.FunctionType _functionType;

		// Token: 0x0400121A RID: 4634
		private List<AstNode> _argumentList;

		// Token: 0x0400121B RID: 4635
		private string _name;

		// Token: 0x0400121C RID: 4636
		private string _prefix;

		// Token: 0x0400121D RID: 4637
		internal static XPathResultType[] ReturnTypes = new XPathResultType[]
		{
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.NodeSet,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Number,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Boolean,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Number,
			XPathResultType.String,
			XPathResultType.String,
			XPathResultType.Boolean,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Number,
			XPathResultType.Any
		};

		// Token: 0x0200034C RID: 844
		public enum FunctionType
		{
			// Token: 0x0400121F RID: 4639
			FuncLast,
			// Token: 0x04001220 RID: 4640
			FuncPosition,
			// Token: 0x04001221 RID: 4641
			FuncCount,
			// Token: 0x04001222 RID: 4642
			FuncID,
			// Token: 0x04001223 RID: 4643
			FuncLocalName,
			// Token: 0x04001224 RID: 4644
			FuncNameSpaceUri,
			// Token: 0x04001225 RID: 4645
			FuncName,
			// Token: 0x04001226 RID: 4646
			FuncString,
			// Token: 0x04001227 RID: 4647
			FuncBoolean,
			// Token: 0x04001228 RID: 4648
			FuncNumber,
			// Token: 0x04001229 RID: 4649
			FuncTrue,
			// Token: 0x0400122A RID: 4650
			FuncFalse,
			// Token: 0x0400122B RID: 4651
			FuncNot,
			// Token: 0x0400122C RID: 4652
			FuncConcat,
			// Token: 0x0400122D RID: 4653
			FuncStartsWith,
			// Token: 0x0400122E RID: 4654
			FuncContains,
			// Token: 0x0400122F RID: 4655
			FuncSubstringBefore,
			// Token: 0x04001230 RID: 4656
			FuncSubstringAfter,
			// Token: 0x04001231 RID: 4657
			FuncSubstring,
			// Token: 0x04001232 RID: 4658
			FuncStringLength,
			// Token: 0x04001233 RID: 4659
			FuncNormalize,
			// Token: 0x04001234 RID: 4660
			FuncTranslate,
			// Token: 0x04001235 RID: 4661
			FuncLang,
			// Token: 0x04001236 RID: 4662
			FuncSum,
			// Token: 0x04001237 RID: 4663
			FuncFloor,
			// Token: 0x04001238 RID: 4664
			FuncCeiling,
			// Token: 0x04001239 RID: 4665
			FuncRound,
			// Token: 0x0400123A RID: 4666
			FuncUserDefined
		}
	}
}
