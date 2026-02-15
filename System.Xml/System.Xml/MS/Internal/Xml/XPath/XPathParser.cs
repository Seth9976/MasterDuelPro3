using System;
using System.Collections.Generic;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000374 RID: 884
	internal class XPathParser
	{
		// Token: 0x060026FB RID: 9979 RVA: 0x000D8940 File Offset: 0x000D6B40
		private XPathParser(XPathScanner scanner)
		{
			this._scanner = scanner;
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000D8950 File Offset: 0x000D6B50
		public static AstNode ParseXPathExpression(string xpathExpression)
		{
			XPathScanner xpathScanner = new XPathScanner(xpathExpression);
			AstNode astNode = new XPathParser(xpathScanner).ParseExpression(null);
			if (xpathScanner.Kind != XPathScanner.LexKind.Eof)
			{
				throw XPathException.Create("'{0}' has an invalid token.", xpathScanner.SourceText);
			}
			return astNode;
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000D898C File Offset: 0x000D6B8C
		private AstNode ParseExpression(AstNode qyInput)
		{
			int num = this._parseDepth + 1;
			this._parseDepth = num;
			if (num > 200)
			{
				throw XPathException.Create("The xpath query is too complex.");
			}
			AstNode astNode = this.ParseOrExpr(qyInput);
			this._parseDepth--;
			return astNode;
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000D89D4 File Offset: 0x000D6BD4
		private AstNode ParseOrExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseAndExpr(qyInput);
			while (this.TestOp("or"))
			{
				this.NextLex();
				astNode = new Operator(Operator.Op.OR, astNode, this.ParseAndExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000D8A10 File Offset: 0x000D6C10
		private AstNode ParseAndExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseEqualityExpr(qyInput);
			while (this.TestOp("and"))
			{
				this.NextLex();
				astNode = new Operator(Operator.Op.AND, astNode, this.ParseEqualityExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000D8A4C File Offset: 0x000D6C4C
		private AstNode ParseEqualityExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseRelationalExpr(qyInput);
			for (;;)
			{
				Operator.Op op = ((this._scanner.Kind == XPathScanner.LexKind.Eq) ? Operator.Op.EQ : ((this._scanner.Kind == XPathScanner.LexKind.Ne) ? Operator.Op.NE : Operator.Op.INVALID));
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseRelationalExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000D8AA4 File Offset: 0x000D6CA4
		private AstNode ParseRelationalExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseAdditiveExpr(qyInput);
			for (;;)
			{
				Operator.Op op = ((this._scanner.Kind == XPathScanner.LexKind.Lt) ? Operator.Op.LT : ((this._scanner.Kind == XPathScanner.LexKind.Le) ? Operator.Op.LE : ((this._scanner.Kind == XPathScanner.LexKind.Gt) ? Operator.Op.GT : ((this._scanner.Kind == XPathScanner.LexKind.Ge) ? Operator.Op.GE : Operator.Op.INVALID))));
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseAdditiveExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000D8B20 File Offset: 0x000D6D20
		private AstNode ParseAdditiveExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseMultiplicativeExpr(qyInput);
			for (;;)
			{
				Operator.Op op = ((this._scanner.Kind == XPathScanner.LexKind.Plus) ? Operator.Op.PLUS : ((this._scanner.Kind == XPathScanner.LexKind.Minus) ? Operator.Op.MINUS : Operator.Op.INVALID));
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseMultiplicativeExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000D8B78 File Offset: 0x000D6D78
		private AstNode ParseMultiplicativeExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParseUnaryExpr(qyInput);
			for (;;)
			{
				Operator.Op op = ((this._scanner.Kind == XPathScanner.LexKind.Star) ? Operator.Op.MUL : (this.TestOp("div") ? Operator.Op.DIV : (this.TestOp("mod") ? Operator.Op.MOD : Operator.Op.INVALID)));
				if (op == Operator.Op.INVALID)
				{
					break;
				}
				this.NextLex();
				astNode = new Operator(op, astNode, this.ParseUnaryExpr(qyInput));
			}
			return astNode;
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000D8BE0 File Offset: 0x000D6DE0
		private AstNode ParseUnaryExpr(AstNode qyInput)
		{
			bool flag = false;
			while (this._scanner.Kind == XPathScanner.LexKind.Minus)
			{
				this.NextLex();
				flag = !flag;
			}
			if (flag)
			{
				return new Operator(Operator.Op.MUL, this.ParseUnionExpr(qyInput), new Operand(-1.0));
			}
			return this.ParseUnionExpr(qyInput);
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000D8C34 File Offset: 0x000D6E34
		private AstNode ParseUnionExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParsePathExpr(qyInput);
			while (this._scanner.Kind == XPathScanner.LexKind.Union)
			{
				this.NextLex();
				AstNode astNode2 = this.ParsePathExpr(qyInput);
				this.CheckNodeSet(astNode.ReturnType);
				this.CheckNodeSet(astNode2.ReturnType);
				astNode = new Operator(Operator.Op.UNION, astNode, astNode2);
			}
			return astNode;
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000D8C8C File Offset: 0x000D6E8C
		private static bool IsNodeType(XPathScanner scaner)
		{
			return scaner.Prefix.Length == 0 && (scaner.Name == "node" || scaner.Name == "text" || scaner.Name == "processing-instruction" || scaner.Name == "comment");
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000D8CF0 File Offset: 0x000D6EF0
		private AstNode ParsePathExpr(AstNode qyInput)
		{
			AstNode astNode;
			if (XPathParser.IsPrimaryExpr(this._scanner))
			{
				astNode = this.ParseFilterExpr(qyInput);
				if (this._scanner.Kind == XPathScanner.LexKind.Slash)
				{
					this.NextLex();
					astNode = this.ParseRelativeLocationPath(astNode);
				}
				else if (this._scanner.Kind == XPathScanner.LexKind.SlashSlash)
				{
					this.NextLex();
					astNode = this.ParseRelativeLocationPath(new Axis(Axis.AxisType.DescendantOrSelf, astNode));
				}
			}
			else
			{
				astNode = this.ParseLocationPath(null);
			}
			return astNode;
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000D8D60 File Offset: 0x000D6F60
		private AstNode ParseFilterExpr(AstNode qyInput)
		{
			AstNode astNode = this.ParsePrimaryExpr(qyInput);
			while (this._scanner.Kind == XPathScanner.LexKind.LBracket)
			{
				astNode = new Filter(astNode, this.ParsePredicate(astNode));
			}
			return astNode;
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000D8D95 File Offset: 0x000D6F95
		private AstNode ParsePredicate(AstNode qyInput)
		{
			this.CheckNodeSet(qyInput.ReturnType);
			this.PassToken(XPathScanner.LexKind.LBracket);
			AstNode astNode = this.ParseExpression(qyInput);
			this.PassToken(XPathScanner.LexKind.RBracket);
			return astNode;
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000D8DBC File Offset: 0x000D6FBC
		private AstNode ParseLocationPath(AstNode qyInput)
		{
			if (this._scanner.Kind == XPathScanner.LexKind.Slash)
			{
				this.NextLex();
				AstNode astNode = new Root();
				if (XPathParser.IsStep(this._scanner.Kind))
				{
					astNode = this.ParseRelativeLocationPath(astNode);
				}
				return astNode;
			}
			if (this._scanner.Kind == XPathScanner.LexKind.SlashSlash)
			{
				this.NextLex();
				return this.ParseRelativeLocationPath(new Axis(Axis.AxisType.DescendantOrSelf, new Root()));
			}
			return this.ParseRelativeLocationPath(qyInput);
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000D8E30 File Offset: 0x000D7030
		private AstNode ParseRelativeLocationPath(AstNode qyInput)
		{
			AstNode astNode = qyInput;
			for (;;)
			{
				astNode = this.ParseStep(astNode);
				if (XPathScanner.LexKind.SlashSlash == this._scanner.Kind)
				{
					this.NextLex();
					astNode = new Axis(Axis.AxisType.DescendantOrSelf, astNode);
				}
				else
				{
					if (XPathScanner.LexKind.Slash != this._scanner.Kind)
					{
						break;
					}
					this.NextLex();
				}
			}
			return astNode;
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000D8E7E File Offset: 0x000D707E
		private static bool IsStep(XPathScanner.LexKind lexKind)
		{
			return lexKind == XPathScanner.LexKind.Dot || lexKind == XPathScanner.LexKind.DotDot || lexKind == XPathScanner.LexKind.At || lexKind == XPathScanner.LexKind.Axe || lexKind == XPathScanner.LexKind.Star || lexKind == XPathScanner.LexKind.Name;
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x000D8EA0 File Offset: 0x000D70A0
		private AstNode ParseStep(AstNode qyInput)
		{
			AstNode astNode;
			if (XPathScanner.LexKind.Dot == this._scanner.Kind)
			{
				this.NextLex();
				astNode = new Axis(Axis.AxisType.Self, qyInput);
			}
			else if (XPathScanner.LexKind.DotDot == this._scanner.Kind)
			{
				this.NextLex();
				astNode = new Axis(Axis.AxisType.Parent, qyInput);
			}
			else
			{
				Axis.AxisType axisType = Axis.AxisType.Child;
				XPathScanner.LexKind kind = this._scanner.Kind;
				if (kind != XPathScanner.LexKind.At)
				{
					if (kind == XPathScanner.LexKind.Axe)
					{
						axisType = this.GetAxis();
						this.NextLex();
					}
				}
				else
				{
					axisType = Axis.AxisType.Attribute;
					this.NextLex();
				}
				XPathNodeType xpathNodeType = ((axisType == Axis.AxisType.Attribute) ? XPathNodeType.Attribute : XPathNodeType.Element);
				astNode = this.ParseNodeTest(qyInput, axisType, xpathNodeType);
				while (XPathScanner.LexKind.LBracket == this._scanner.Kind)
				{
					astNode = new Filter(astNode, this.ParsePredicate(astNode));
				}
			}
			return astNode;
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000D8F54 File Offset: 0x000D7154
		private AstNode ParseNodeTest(AstNode qyInput, Axis.AxisType axisType, XPathNodeType nodeType)
		{
			XPathScanner.LexKind kind = this._scanner.Kind;
			string text;
			string text2;
			if (kind != XPathScanner.LexKind.Star)
			{
				if (kind != XPathScanner.LexKind.Name)
				{
					throw XPathException.Create("Expression must evaluate to a node-set.", this._scanner.SourceText);
				}
				if (this._scanner.CanBeFunction && XPathParser.IsNodeType(this._scanner))
				{
					text = string.Empty;
					text2 = string.Empty;
					nodeType = ((this._scanner.Name == "comment") ? XPathNodeType.Comment : ((this._scanner.Name == "text") ? XPathNodeType.Text : ((this._scanner.Name == "node") ? XPathNodeType.All : ((this._scanner.Name == "processing-instruction") ? XPathNodeType.ProcessingInstruction : XPathNodeType.Root))));
					this.NextLex();
					this.PassToken(XPathScanner.LexKind.LParens);
					if (nodeType == XPathNodeType.ProcessingInstruction && this._scanner.Kind != XPathScanner.LexKind.RParens)
					{
						this.CheckToken(XPathScanner.LexKind.String);
						text2 = this._scanner.StringValue;
						this.NextLex();
					}
					this.PassToken(XPathScanner.LexKind.RParens);
				}
				else
				{
					text = this._scanner.Prefix;
					text2 = this._scanner.Name;
					this.NextLex();
					if (text2 == "*")
					{
						text2 = string.Empty;
					}
				}
			}
			else
			{
				text = string.Empty;
				text2 = string.Empty;
				this.NextLex();
			}
			return new Axis(axisType, qyInput, text, text2, nodeType);
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x000D90C4 File Offset: 0x000D72C4
		private static bool IsPrimaryExpr(XPathScanner scanner)
		{
			return scanner.Kind == XPathScanner.LexKind.String || scanner.Kind == XPathScanner.LexKind.Number || scanner.Kind == XPathScanner.LexKind.Dollar || scanner.Kind == XPathScanner.LexKind.LParens || (scanner.Kind == XPathScanner.LexKind.Name && scanner.CanBeFunction && !XPathParser.IsNodeType(scanner));
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000D9118 File Offset: 0x000D7318
		private AstNode ParsePrimaryExpr(AstNode qyInput)
		{
			AstNode astNode = null;
			XPathScanner.LexKind kind = this._scanner.Kind;
			if (kind <= XPathScanner.LexKind.LParens)
			{
				if (kind != XPathScanner.LexKind.Dollar)
				{
					if (kind == XPathScanner.LexKind.LParens)
					{
						this.NextLex();
						astNode = this.ParseExpression(qyInput);
						if (astNode.Type != AstNode.AstType.ConstantOperand)
						{
							astNode = new Group(astNode);
						}
						this.PassToken(XPathScanner.LexKind.RParens);
					}
				}
				else
				{
					this.NextLex();
					this.CheckToken(XPathScanner.LexKind.Name);
					astNode = new Variable(this._scanner.Name, this._scanner.Prefix);
					this.NextLex();
				}
			}
			else if (kind != XPathScanner.LexKind.Number)
			{
				if (kind != XPathScanner.LexKind.Name)
				{
					if (kind == XPathScanner.LexKind.String)
					{
						astNode = new Operand(this._scanner.StringValue);
						this.NextLex();
					}
				}
				else if (this._scanner.CanBeFunction && !XPathParser.IsNodeType(this._scanner))
				{
					astNode = this.ParseMethod(null);
				}
			}
			else
			{
				astNode = new Operand(this._scanner.NumberValue);
				this.NextLex();
			}
			return astNode;
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x000D9214 File Offset: 0x000D7414
		private AstNode ParseMethod(AstNode qyInput)
		{
			List<AstNode> list = new List<AstNode>();
			string name = this._scanner.Name;
			string prefix = this._scanner.Prefix;
			this.PassToken(XPathScanner.LexKind.Name);
			this.PassToken(XPathScanner.LexKind.LParens);
			if (this._scanner.Kind != XPathScanner.LexKind.RParens)
			{
				for (;;)
				{
					list.Add(this.ParseExpression(qyInput));
					if (this._scanner.Kind == XPathScanner.LexKind.RParens)
					{
						break;
					}
					this.PassToken(XPathScanner.LexKind.Comma);
				}
			}
			this.PassToken(XPathScanner.LexKind.RParens);
			XPathParser.ParamInfo paramInfo;
			if (prefix.Length != 0 || !XPathParser.s_functionTable.TryGetValue(name, out paramInfo))
			{
				return new Function(prefix, name, list);
			}
			int num = list.Count;
			if (num < paramInfo.Minargs)
			{
				throw XPathException.Create("Function '{0}' in '{1}' has an invalid number of arguments.", name, this._scanner.SourceText);
			}
			if (paramInfo.FType == Function.FunctionType.FuncConcat)
			{
				for (int i = 0; i < num; i++)
				{
					AstNode astNode = list[i];
					if (astNode.ReturnType != XPathResultType.String)
					{
						astNode = new Function(Function.FunctionType.FuncString, astNode);
					}
					list[i] = astNode;
				}
			}
			else
			{
				if (paramInfo.Maxargs < num)
				{
					throw XPathException.Create("Function '{0}' in '{1}' has an invalid number of arguments.", name, this._scanner.SourceText);
				}
				if (paramInfo.ArgTypes.Length < num)
				{
					num = paramInfo.ArgTypes.Length;
				}
				for (int j = 0; j < num; j++)
				{
					AstNode astNode2 = list[j];
					if (paramInfo.ArgTypes[j] != XPathResultType.Any && paramInfo.ArgTypes[j] != astNode2.ReturnType)
					{
						switch (paramInfo.ArgTypes[j])
						{
						case XPathResultType.Number:
							astNode2 = new Function(Function.FunctionType.FuncNumber, astNode2);
							break;
						case XPathResultType.String:
							astNode2 = new Function(Function.FunctionType.FuncString, astNode2);
							break;
						case XPathResultType.Boolean:
							astNode2 = new Function(Function.FunctionType.FuncBoolean, astNode2);
							break;
						case XPathResultType.NodeSet:
							if (!(astNode2 is Variable) && (!(astNode2 is Function) || astNode2.ReturnType != XPathResultType.Any))
							{
								throw XPathException.Create("The argument to function '{0}' in '{1}' cannot be converted to a node-set.", name, this._scanner.SourceText);
							}
							break;
						}
						list[j] = astNode2;
					}
				}
			}
			return new Function(paramInfo.FType, list);
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x000D942F File Offset: 0x000D762F
		private void CheckToken(XPathScanner.LexKind t)
		{
			if (this._scanner.Kind != t)
			{
				throw XPathException.Create("'{0}' has an invalid token.", this._scanner.SourceText);
			}
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000D9455 File Offset: 0x000D7655
		private void PassToken(XPathScanner.LexKind t)
		{
			this.CheckToken(t);
			this.NextLex();
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x000D9464 File Offset: 0x000D7664
		private void NextLex()
		{
			this._scanner.NextLex();
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000D9472 File Offset: 0x000D7672
		private bool TestOp(string op)
		{
			return this._scanner.Kind == XPathScanner.LexKind.Name && this._scanner.Prefix.Length == 0 && this._scanner.Name.Equals(op);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x000D94A8 File Offset: 0x000D76A8
		private void CheckNodeSet(XPathResultType t)
		{
			if (t != XPathResultType.NodeSet && t != XPathResultType.Any)
			{
				throw XPathException.Create("Expression must evaluate to a node-set.", this._scanner.SourceText);
			}
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x000D94C8 File Offset: 0x000D76C8
		private static Dictionary<string, XPathParser.ParamInfo> CreateFunctionTable()
		{
			return new Dictionary<string, XPathParser.ParamInfo>(36)
			{
				{
					"last",
					new XPathParser.ParamInfo(Function.FunctionType.FuncLast, 0, 0, XPathParser.s_temparray1)
				},
				{
					"position",
					new XPathParser.ParamInfo(Function.FunctionType.FuncPosition, 0, 0, XPathParser.s_temparray1)
				},
				{
					"name",
					new XPathParser.ParamInfo(Function.FunctionType.FuncName, 0, 1, XPathParser.s_temparray2)
				},
				{
					"namespace-uri",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNameSpaceUri, 0, 1, XPathParser.s_temparray2)
				},
				{
					"local-name",
					new XPathParser.ParamInfo(Function.FunctionType.FuncLocalName, 0, 1, XPathParser.s_temparray2)
				},
				{
					"count",
					new XPathParser.ParamInfo(Function.FunctionType.FuncCount, 1, 1, XPathParser.s_temparray2)
				},
				{
					"id",
					new XPathParser.ParamInfo(Function.FunctionType.FuncID, 1, 1, XPathParser.s_temparray3)
				},
				{
					"string",
					new XPathParser.ParamInfo(Function.FunctionType.FuncString, 0, 1, XPathParser.s_temparray3)
				},
				{
					"concat",
					new XPathParser.ParamInfo(Function.FunctionType.FuncConcat, 2, 100, XPathParser.s_temparray4)
				},
				{
					"starts-with",
					new XPathParser.ParamInfo(Function.FunctionType.FuncStartsWith, 2, 2, XPathParser.s_temparray5)
				},
				{
					"contains",
					new XPathParser.ParamInfo(Function.FunctionType.FuncContains, 2, 2, XPathParser.s_temparray5)
				},
				{
					"substring-before",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSubstringBefore, 2, 2, XPathParser.s_temparray5)
				},
				{
					"substring-after",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSubstringAfter, 2, 2, XPathParser.s_temparray5)
				},
				{
					"substring",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSubstring, 2, 3, XPathParser.s_temparray6)
				},
				{
					"string-length",
					new XPathParser.ParamInfo(Function.FunctionType.FuncStringLength, 0, 1, XPathParser.s_temparray4)
				},
				{
					"normalize-space",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNormalize, 0, 1, XPathParser.s_temparray4)
				},
				{
					"translate",
					new XPathParser.ParamInfo(Function.FunctionType.FuncTranslate, 3, 3, XPathParser.s_temparray7)
				},
				{
					"boolean",
					new XPathParser.ParamInfo(Function.FunctionType.FuncBoolean, 1, 1, XPathParser.s_temparray3)
				},
				{
					"not",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNot, 1, 1, XPathParser.s_temparray8)
				},
				{
					"true",
					new XPathParser.ParamInfo(Function.FunctionType.FuncTrue, 0, 0, XPathParser.s_temparray8)
				},
				{
					"false",
					new XPathParser.ParamInfo(Function.FunctionType.FuncFalse, 0, 0, XPathParser.s_temparray8)
				},
				{
					"lang",
					new XPathParser.ParamInfo(Function.FunctionType.FuncLang, 1, 1, XPathParser.s_temparray4)
				},
				{
					"number",
					new XPathParser.ParamInfo(Function.FunctionType.FuncNumber, 0, 1, XPathParser.s_temparray3)
				},
				{
					"sum",
					new XPathParser.ParamInfo(Function.FunctionType.FuncSum, 1, 1, XPathParser.s_temparray2)
				},
				{
					"floor",
					new XPathParser.ParamInfo(Function.FunctionType.FuncFloor, 1, 1, XPathParser.s_temparray9)
				},
				{
					"ceiling",
					new XPathParser.ParamInfo(Function.FunctionType.FuncCeiling, 1, 1, XPathParser.s_temparray9)
				},
				{
					"round",
					new XPathParser.ParamInfo(Function.FunctionType.FuncRound, 1, 1, XPathParser.s_temparray9)
				}
			};
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x000D9778 File Offset: 0x000D7978
		private static Dictionary<string, Axis.AxisType> CreateAxesTable()
		{
			return new Dictionary<string, Axis.AxisType>(13)
			{
				{
					"ancestor",
					Axis.AxisType.Ancestor
				},
				{
					"ancestor-or-self",
					Axis.AxisType.AncestorOrSelf
				},
				{
					"attribute",
					Axis.AxisType.Attribute
				},
				{
					"child",
					Axis.AxisType.Child
				},
				{
					"descendant",
					Axis.AxisType.Descendant
				},
				{
					"descendant-or-self",
					Axis.AxisType.DescendantOrSelf
				},
				{
					"following",
					Axis.AxisType.Following
				},
				{
					"following-sibling",
					Axis.AxisType.FollowingSibling
				},
				{
					"namespace",
					Axis.AxisType.Namespace
				},
				{
					"parent",
					Axis.AxisType.Parent
				},
				{
					"preceding",
					Axis.AxisType.Preceding
				},
				{
					"preceding-sibling",
					Axis.AxisType.PrecedingSibling
				},
				{
					"self",
					Axis.AxisType.Self
				}
			};
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x000D982C File Offset: 0x000D7A2C
		private Axis.AxisType GetAxis()
		{
			Axis.AxisType axisType;
			if (!XPathParser.s_AxesTable.TryGetValue(this._scanner.Name, out axisType))
			{
				throw XPathException.Create("'{0}' has an invalid token.", this._scanner.SourceText);
			}
			return axisType;
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x000D986C File Offset: 0x000D7A6C
		// Note: this type is marked as 'beforefieldinit'.
		static XPathParser()
		{
			XPathResultType[] array = new XPathResultType[3];
			array[0] = XPathResultType.String;
			XPathParser.s_temparray6 = array;
			XPathParser.s_temparray7 = new XPathResultType[]
			{
				XPathResultType.String,
				XPathResultType.String,
				XPathResultType.String
			};
			XPathParser.s_temparray8 = new XPathResultType[] { XPathResultType.Boolean };
			XPathParser.s_temparray9 = new XPathResultType[1];
			XPathParser.s_functionTable = XPathParser.CreateFunctionTable();
			XPathParser.s_AxesTable = XPathParser.CreateAxesTable();
		}

		// Token: 0x0400129C RID: 4764
		private XPathScanner _scanner;

		// Token: 0x0400129D RID: 4765
		private int _parseDepth;

		// Token: 0x0400129E RID: 4766
		private static readonly XPathResultType[] s_temparray1 = Array.Empty<XPathResultType>();

		// Token: 0x0400129F RID: 4767
		private static readonly XPathResultType[] s_temparray2 = new XPathResultType[] { XPathResultType.NodeSet };

		// Token: 0x040012A0 RID: 4768
		private static readonly XPathResultType[] s_temparray3 = new XPathResultType[] { XPathResultType.Any };

		// Token: 0x040012A1 RID: 4769
		private static readonly XPathResultType[] s_temparray4 = new XPathResultType[] { XPathResultType.String };

		// Token: 0x040012A2 RID: 4770
		private static readonly XPathResultType[] s_temparray5 = new XPathResultType[]
		{
			XPathResultType.String,
			XPathResultType.String
		};

		// Token: 0x040012A3 RID: 4771
		private static readonly XPathResultType[] s_temparray6;

		// Token: 0x040012A4 RID: 4772
		private static readonly XPathResultType[] s_temparray7;

		// Token: 0x040012A5 RID: 4773
		private static readonly XPathResultType[] s_temparray8;

		// Token: 0x040012A6 RID: 4774
		private static readonly XPathResultType[] s_temparray9;

		// Token: 0x040012A7 RID: 4775
		private static Dictionary<string, XPathParser.ParamInfo> s_functionTable;

		// Token: 0x040012A8 RID: 4776
		private static Dictionary<string, Axis.AxisType> s_AxesTable;

		// Token: 0x02000375 RID: 885
		private class ParamInfo
		{
			// Token: 0x17000926 RID: 2342
			// (get) Token: 0x0600271B RID: 10011 RVA: 0x000D9916 File Offset: 0x000D7B16
			public Function.FunctionType FType
			{
				get
				{
					return this._ftype;
				}
			}

			// Token: 0x17000927 RID: 2343
			// (get) Token: 0x0600271C RID: 10012 RVA: 0x000D991E File Offset: 0x000D7B1E
			public int Minargs
			{
				get
				{
					return this._minargs;
				}
			}

			// Token: 0x17000928 RID: 2344
			// (get) Token: 0x0600271D RID: 10013 RVA: 0x000D9926 File Offset: 0x000D7B26
			public int Maxargs
			{
				get
				{
					return this._maxargs;
				}
			}

			// Token: 0x17000929 RID: 2345
			// (get) Token: 0x0600271E RID: 10014 RVA: 0x000D992E File Offset: 0x000D7B2E
			public XPathResultType[] ArgTypes
			{
				get
				{
					return this._argTypes;
				}
			}

			// Token: 0x0600271F RID: 10015 RVA: 0x000D9936 File Offset: 0x000D7B36
			internal ParamInfo(Function.FunctionType ftype, int minargs, int maxargs, XPathResultType[] argTypes)
			{
				this._ftype = ftype;
				this._minargs = minargs;
				this._maxargs = maxargs;
				this._argTypes = argTypes;
			}

			// Token: 0x040012A9 RID: 4777
			private Function.FunctionType _ftype;

			// Token: 0x040012AA RID: 4778
			private int _minargs;

			// Token: 0x040012AB RID: 4779
			private int _maxargs;

			// Token: 0x040012AC RID: 4780
			private XPathResultType[] _argTypes;
		}
	}
}
