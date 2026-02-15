using System;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000352 RID: 850
	internal sealed class LogicalExpr : ValueQuery
	{
		// Token: 0x06002605 RID: 9733 RVA: 0x000D59F9 File Offset: 0x000D3BF9
		public LogicalExpr(Operator.Op op, Query opnd1, Query opnd2)
		{
			this._op = op;
			this._opnd1 = opnd1;
			this._opnd2 = opnd2;
		}

		// Token: 0x06002606 RID: 9734 RVA: 0x000D5A16 File Offset: 0x000D3C16
		private LogicalExpr(LogicalExpr other)
			: base(other)
		{
			this._op = other._op;
			this._opnd1 = Query.Clone(other._opnd1);
			this._opnd2 = Query.Clone(other._opnd2);
		}

		// Token: 0x06002607 RID: 9735 RVA: 0x000D5A4D File Offset: 0x000D3C4D
		public override void SetXsltContext(XsltContext context)
		{
			this._opnd1.SetXsltContext(context);
			this._opnd2.SetXsltContext(context);
		}

		// Token: 0x06002608 RID: 9736 RVA: 0x000D5A68 File Offset: 0x000D3C68
		public override object Evaluate(XPathNodeIterator nodeIterator)
		{
			Operator.Op op = this._op;
			object obj = this._opnd1.Evaluate(nodeIterator);
			object obj2 = this._opnd2.Evaluate(nodeIterator);
			int num = (int)base.GetXPathType(obj);
			int num2 = (int)base.GetXPathType(obj2);
			if (num < num2)
			{
				op = Operator.InvertOperator(op);
				object obj3 = obj;
				obj = obj2;
				obj2 = obj3;
				int num3 = num;
				num = num2;
				num2 = num3;
			}
			if (op == Operator.Op.EQ || op == Operator.Op.NE)
			{
				return LogicalExpr.s_CompXsltE[num][num2](op, obj, obj2);
			}
			return LogicalExpr.s_CompXsltO[num][num2](op, obj, obj2);
		}

		// Token: 0x06002609 RID: 9737 RVA: 0x000D5AF4 File Offset: 0x000D3CF4
		private static bool cmpQueryQueryE(Operator.Op op, object val1, object val2)
		{
			bool flag = op == Operator.Op.EQ;
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			LogicalExpr.NodeSet nodeSet2 = new LogicalExpr.NodeSet(val2);
			IL_0015:
			while (nodeSet.MoveNext())
			{
				if (!nodeSet2.MoveNext())
				{
					return false;
				}
				string value = nodeSet.Value;
				while (value == nodeSet2.Value != flag)
				{
					if (!nodeSet2.MoveNext())
					{
						nodeSet2.Reset();
						goto IL_0015;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600260A RID: 9738 RVA: 0x000D5B58 File Offset: 0x000D3D58
		private static bool cmpQueryQueryO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			LogicalExpr.NodeSet nodeSet2 = new LogicalExpr.NodeSet(val2);
			IL_0010:
			while (nodeSet.MoveNext())
			{
				if (!nodeSet2.MoveNext())
				{
					return false;
				}
				double num = NumberFunctions.Number(nodeSet.Value);
				while (!LogicalExpr.cmpNumberNumber(op, num, NumberFunctions.Number(nodeSet2.Value)))
				{
					if (!nodeSet2.MoveNext())
					{
						nodeSet2.Reset();
						goto IL_0010;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x0600260B RID: 9739 RVA: 0x000D5BC0 File Offset: 0x000D3DC0
		private static bool cmpQueryNumber(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double num = (double)val2;
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumber(op, NumberFunctions.Number(nodeSet.Value), num))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600260C RID: 9740 RVA: 0x000D5C00 File Offset: 0x000D3E00
		private static bool cmpQueryStringE(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			string text = (string)val2;
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpStringStringE(op, nodeSet.Value, text))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600260D RID: 9741 RVA: 0x000D5C3C File Offset: 0x000D3E3C
		private static bool cmpQueryStringO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double num = NumberFunctions.Number((string)val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number(nodeSet.Value), num))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600260E RID: 9742 RVA: 0x000D5C80 File Offset: 0x000D3E80
		private static bool cmpRtfQueryE(Operator.Op op, object val1, object val2)
		{
			string text = LogicalExpr.Rtf(val1);
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpStringStringE(op, text, nodeSet.Value))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600260F RID: 9743 RVA: 0x000D5CBC File Offset: 0x000D3EBC
		private static bool cmpRtfQueryO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val2);
			while (nodeSet.MoveNext())
			{
				if (LogicalExpr.cmpNumberNumberO(op, num, NumberFunctions.Number(nodeSet.Value)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002610 RID: 9744 RVA: 0x000D5D00 File Offset: 0x000D3F00
		private static bool cmpQueryBoolE(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			bool flag = nodeSet.MoveNext();
			bool flag2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, flag, flag2);
		}

		// Token: 0x06002611 RID: 9745 RVA: 0x000D5D2C File Offset: 0x000D3F2C
		private static bool cmpQueryBoolO(Operator.Op op, object val1, object val2)
		{
			LogicalExpr.NodeSet nodeSet = new LogicalExpr.NodeSet(val1);
			double num = (nodeSet.MoveNext() ? 1.0 : 0.0);
			double num2 = NumberFunctions.Number((bool)val2);
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06002612 RID: 9746 RVA: 0x000D5D73 File Offset: 0x000D3F73
		private static bool cmpBoolBoolE(Operator.Op op, bool n1, bool n2)
		{
			return op == Operator.Op.EQ == (n1 == n2);
		}

		// Token: 0x06002613 RID: 9747 RVA: 0x000D5D80 File Offset: 0x000D3F80
		private static bool cmpBoolBoolE(Operator.Op op, object val1, object val2)
		{
			bool flag = (bool)val1;
			bool flag2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, flag, flag2);
		}

		// Token: 0x06002614 RID: 9748 RVA: 0x000D5DA4 File Offset: 0x000D3FA4
		private static bool cmpBoolBoolO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number((bool)val1);
			double num2 = NumberFunctions.Number((bool)val2);
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06002615 RID: 9749 RVA: 0x000D5DD4 File Offset: 0x000D3FD4
		private static bool cmpBoolNumberE(Operator.Op op, object val1, object val2)
		{
			bool flag = (bool)val1;
			bool flag2 = BooleanFunctions.toBoolean((double)val2);
			return LogicalExpr.cmpBoolBoolE(op, flag, flag2);
		}

		// Token: 0x06002616 RID: 9750 RVA: 0x000D5DFC File Offset: 0x000D3FFC
		private static bool cmpBoolNumberO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number((bool)val1);
			double num2 = (double)val2;
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06002617 RID: 9751 RVA: 0x000D5E24 File Offset: 0x000D4024
		private static bool cmpBoolStringE(Operator.Op op, object val1, object val2)
		{
			bool flag = (bool)val1;
			bool flag2 = BooleanFunctions.toBoolean((string)val2);
			return LogicalExpr.cmpBoolBoolE(op, flag, flag2);
		}

		// Token: 0x06002618 RID: 9752 RVA: 0x000D5E4C File Offset: 0x000D404C
		private static bool cmpRtfBoolE(Operator.Op op, object val1, object val2)
		{
			bool flag = BooleanFunctions.toBoolean(LogicalExpr.Rtf(val1));
			bool flag2 = (bool)val2;
			return LogicalExpr.cmpBoolBoolE(op, flag, flag2);
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x000D5E74 File Offset: 0x000D4074
		private static bool cmpBoolStringO(Operator.Op op, object val1, object val2)
		{
			return LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number((bool)val1), NumberFunctions.Number((string)val2));
		}

		// Token: 0x0600261A RID: 9754 RVA: 0x000D5E92 File Offset: 0x000D4092
		private static bool cmpRtfBoolO(Operator.Op op, object val1, object val2)
		{
			return LogicalExpr.cmpNumberNumberO(op, NumberFunctions.Number(LogicalExpr.Rtf(val1)), NumberFunctions.Number((bool)val2));
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x000D5EB0 File Offset: 0x000D40B0
		private static bool cmpNumberNumber(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.EQ:
				return n1 == n2;
			case Operator.Op.NE:
				return n1 != n2;
			case Operator.Op.LT:
				return n1 < n2;
			case Operator.Op.LE:
				return n1 <= n2;
			case Operator.Op.GT:
				return n1 > n2;
			case Operator.Op.GE:
				return n1 >= n2;
			default:
				return false;
			}
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x000D5F07 File Offset: 0x000D4107
		private static bool cmpNumberNumberO(Operator.Op op, double n1, double n2)
		{
			switch (op)
			{
			case Operator.Op.LT:
				return n1 < n2;
			case Operator.Op.LE:
				return n1 <= n2;
			case Operator.Op.GT:
				return n1 > n2;
			case Operator.Op.GE:
				return n1 >= n2;
			default:
				return false;
			}
		}

		// Token: 0x0600261D RID: 9757 RVA: 0x000D5F40 File Offset: 0x000D4140
		private static bool cmpNumberNumber(Operator.Op op, object val1, object val2)
		{
			double num = (double)val1;
			double num2 = (double)val2;
			return LogicalExpr.cmpNumberNumber(op, num, num2);
		}

		// Token: 0x0600261E RID: 9758 RVA: 0x000D5F64 File Offset: 0x000D4164
		private static bool cmpStringNumber(Operator.Op op, object val1, object val2)
		{
			double num = (double)val2;
			double num2 = NumberFunctions.Number((string)val1);
			return LogicalExpr.cmpNumberNumber(op, num2, num);
		}

		// Token: 0x0600261F RID: 9759 RVA: 0x000D5F8C File Offset: 0x000D418C
		private static bool cmpRtfNumber(Operator.Op op, object val1, object val2)
		{
			double num = (double)val2;
			double num2 = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			return LogicalExpr.cmpNumberNumber(op, num2, num);
		}

		// Token: 0x06002620 RID: 9760 RVA: 0x000D5FB4 File Offset: 0x000D41B4
		private static bool cmpStringStringE(Operator.Op op, string n1, string n2)
		{
			return op == Operator.Op.EQ == (n1 == n2);
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x000D5FC4 File Offset: 0x000D41C4
		private static bool cmpStringStringE(Operator.Op op, object val1, object val2)
		{
			string text = (string)val1;
			string text2 = (string)val2;
			return LogicalExpr.cmpStringStringE(op, text, text2);
		}

		// Token: 0x06002622 RID: 9762 RVA: 0x000D5FE8 File Offset: 0x000D41E8
		private static bool cmpRtfStringE(Operator.Op op, object val1, object val2)
		{
			string text = LogicalExpr.Rtf(val1);
			string text2 = (string)val2;
			return LogicalExpr.cmpStringStringE(op, text, text2);
		}

		// Token: 0x06002623 RID: 9763 RVA: 0x000D600C File Offset: 0x000D420C
		private static bool cmpRtfRtfE(Operator.Op op, object val1, object val2)
		{
			string text = LogicalExpr.Rtf(val1);
			string text2 = LogicalExpr.Rtf(val2);
			return LogicalExpr.cmpStringStringE(op, text, text2);
		}

		// Token: 0x06002624 RID: 9764 RVA: 0x000D6030 File Offset: 0x000D4230
		private static bool cmpStringStringO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number((string)val1);
			double num2 = NumberFunctions.Number((string)val2);
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06002625 RID: 9765 RVA: 0x000D6060 File Offset: 0x000D4260
		private static bool cmpRtfStringO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			double num2 = NumberFunctions.Number((string)val2);
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06002626 RID: 9766 RVA: 0x000D6090 File Offset: 0x000D4290
		private static bool cmpRtfRtfO(Operator.Op op, object val1, object val2)
		{
			double num = NumberFunctions.Number(LogicalExpr.Rtf(val1));
			double num2 = NumberFunctions.Number(LogicalExpr.Rtf(val2));
			return LogicalExpr.cmpNumberNumberO(op, num, num2);
		}

		// Token: 0x06002627 RID: 9767 RVA: 0x000D60BD File Offset: 0x000D42BD
		public override XPathNodeIterator Clone()
		{
			return new LogicalExpr(this);
		}

		// Token: 0x06002628 RID: 9768 RVA: 0x000D60C5 File Offset: 0x000D42C5
		private static string Rtf(object o)
		{
			return ((XPathNavigator)o).Value;
		}

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06002629 RID: 9769 RVA: 0x0003A73C File Offset: 0x0003893C
		public override XPathResultType StaticType
		{
			get
			{
				return XPathResultType.Boolean;
			}
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x000D60D4 File Offset: 0x000D42D4
		// Note: this type is marked as 'beforefieldinit'.
		static LogicalExpr()
		{
			LogicalExpr.cmpXslt[][] array = new LogicalExpr.cmpXslt[5][];
			int num = 0;
			LogicalExpr.cmpXslt[] array2 = new LogicalExpr.cmpXslt[5];
			array2[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpNumberNumber);
			array[num] = array2;
			int num2 = 1;
			LogicalExpr.cmpXslt[] array3 = new LogicalExpr.cmpXslt[5];
			array3[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringNumber);
			array3[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringStringE);
			array[num2] = array3;
			int num3 = 2;
			LogicalExpr.cmpXslt[] array4 = new LogicalExpr.cmpXslt[5];
			array4[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolNumberE);
			array4[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolStringE);
			array4[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolBoolE);
			array[num3] = array4;
			int num4 = 3;
			LogicalExpr.cmpXslt[] array5 = new LogicalExpr.cmpXslt[5];
			array5[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryNumber);
			array5[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryStringE);
			array5[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryBoolE);
			array5[3] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryQueryE);
			array[num4] = array5;
			array[4] = new LogicalExpr.cmpXslt[]
			{
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfNumber),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfStringE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfBoolE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfQueryE),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfRtfE)
			};
			LogicalExpr.s_CompXsltE = array;
			LogicalExpr.cmpXslt[][] array6 = new LogicalExpr.cmpXslt[5][];
			int num5 = 0;
			LogicalExpr.cmpXslt[] array7 = new LogicalExpr.cmpXslt[5];
			array7[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpNumberNumber);
			array6[num5] = array7;
			int num6 = 1;
			LogicalExpr.cmpXslt[] array8 = new LogicalExpr.cmpXslt[5];
			array8[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringNumber);
			array8[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpStringStringO);
			array6[num6] = array8;
			int num7 = 2;
			LogicalExpr.cmpXslt[] array9 = new LogicalExpr.cmpXslt[5];
			array9[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolNumberO);
			array9[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolStringO);
			array9[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpBoolBoolO);
			array6[num7] = array9;
			int num8 = 3;
			LogicalExpr.cmpXslt[] array10 = new LogicalExpr.cmpXslt[5];
			array10[0] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryNumber);
			array10[1] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryStringO);
			array10[2] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryBoolO);
			array10[3] = new LogicalExpr.cmpXslt(LogicalExpr.cmpQueryQueryO);
			array6[num8] = array10;
			array6[4] = new LogicalExpr.cmpXslt[]
			{
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfNumber),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfStringO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfBoolO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfQueryO),
				new LogicalExpr.cmpXslt(LogicalExpr.cmpRtfRtfO)
			};
			LogicalExpr.s_CompXsltO = array6;
		}

		// Token: 0x04001241 RID: 4673
		private Operator.Op _op;

		// Token: 0x04001242 RID: 4674
		private Query _opnd1;

		// Token: 0x04001243 RID: 4675
		private Query _opnd2;

		// Token: 0x04001244 RID: 4676
		private static readonly LogicalExpr.cmpXslt[][] s_CompXsltE;

		// Token: 0x04001245 RID: 4677
		private static readonly LogicalExpr.cmpXslt[][] s_CompXsltO;

		// Token: 0x02000353 RID: 851
		// (Invoke) Token: 0x0600262C RID: 9772
		private delegate bool cmpXslt(Operator.Op op, object val1, object val2);

		// Token: 0x02000354 RID: 852
		private struct NodeSet
		{
			// Token: 0x0600262D RID: 9773 RVA: 0x000D6313 File Offset: 0x000D4513
			public NodeSet(object opnd)
			{
				this._opnd = (Query)opnd;
				this._current = null;
			}

			// Token: 0x0600262E RID: 9774 RVA: 0x000D6328 File Offset: 0x000D4528
			public bool MoveNext()
			{
				this._current = this._opnd.Advance();
				return this._current != null;
			}

			// Token: 0x0600262F RID: 9775 RVA: 0x000D6344 File Offset: 0x000D4544
			public void Reset()
			{
				this._opnd.Reset();
			}

			// Token: 0x170008F8 RID: 2296
			// (get) Token: 0x06002630 RID: 9776 RVA: 0x000D6351 File Offset: 0x000D4551
			public string Value
			{
				get
				{
					return this._current.Value;
				}
			}

			// Token: 0x04001246 RID: 4678
			private Query _opnd;

			// Token: 0x04001247 RID: 4679
			private XPathNavigator _current;
		}
	}
}
