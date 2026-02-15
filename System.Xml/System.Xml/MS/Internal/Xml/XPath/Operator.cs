using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200035C RID: 860
	internal class Operator : AstNode
	{
		// Token: 0x0600265F RID: 9823 RVA: 0x000D6A25 File Offset: 0x000D4C25
		public static Operator.Op InvertOperator(Operator.Op op)
		{
			return Operator.s_invertOp[(int)op];
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000D6A2E File Offset: 0x000D4C2E
		public Operator(Operator.Op op, AstNode opnd1, AstNode opnd2)
		{
			this._opType = op;
			this._opnd1 = opnd1;
			this._opnd2 = opnd2;
		}

		// Token: 0x17000900 RID: 2304
		// (get) Token: 0x06002661 RID: 9825 RVA: 0x0000EFDF File Offset: 0x0000D1DF
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Operator;
			}
		}

		// Token: 0x17000901 RID: 2305
		// (get) Token: 0x06002662 RID: 9826 RVA: 0x000D6A4B File Offset: 0x000D4C4B
		public override XPathResultType ReturnType
		{
			get
			{
				if (this._opType <= Operator.Op.GE)
				{
					return XPathResultType.Boolean;
				}
				if (this._opType <= Operator.Op.MOD)
				{
					return XPathResultType.Number;
				}
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x000D6A65 File Offset: 0x000D4C65
		public Operator.Op OperatorType
		{
			get
			{
				return this._opType;
			}
		}

		// Token: 0x17000903 RID: 2307
		// (get) Token: 0x06002664 RID: 9828 RVA: 0x000D6A6D File Offset: 0x000D4C6D
		public AstNode Operand1
		{
			get
			{
				return this._opnd1;
			}
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000D6A75 File Offset: 0x000D4C75
		public AstNode Operand2
		{
			get
			{
				return this._opnd2;
			}
		}

		// Token: 0x04001255 RID: 4693
		private static Operator.Op[] s_invertOp = new Operator.Op[]
		{
			Operator.Op.INVALID,
			Operator.Op.INVALID,
			Operator.Op.INVALID,
			Operator.Op.EQ,
			Operator.Op.NE,
			Operator.Op.GT,
			Operator.Op.GE,
			Operator.Op.LT,
			Operator.Op.LE
		};

		// Token: 0x04001256 RID: 4694
		private Operator.Op _opType;

		// Token: 0x04001257 RID: 4695
		private AstNode _opnd1;

		// Token: 0x04001258 RID: 4696
		private AstNode _opnd2;

		// Token: 0x0200035D RID: 861
		public enum Op
		{
			// Token: 0x0400125A RID: 4698
			INVALID,
			// Token: 0x0400125B RID: 4699
			OR,
			// Token: 0x0400125C RID: 4700
			AND,
			// Token: 0x0400125D RID: 4701
			EQ,
			// Token: 0x0400125E RID: 4702
			NE,
			// Token: 0x0400125F RID: 4703
			LT,
			// Token: 0x04001260 RID: 4704
			LE,
			// Token: 0x04001261 RID: 4705
			GT,
			// Token: 0x04001262 RID: 4706
			GE,
			// Token: 0x04001263 RID: 4707
			PLUS,
			// Token: 0x04001264 RID: 4708
			MINUS,
			// Token: 0x04001265 RID: 4709
			MUL,
			// Token: 0x04001266 RID: 4710
			DIV,
			// Token: 0x04001267 RID: 4711
			MOD,
			// Token: 0x04001268 RID: 4712
			UNION
		}
	}
}
