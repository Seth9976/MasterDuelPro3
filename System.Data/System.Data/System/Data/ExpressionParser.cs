using System;
using System.Data.Common;
using System.Globalization;

namespace System.Data
{
	// Token: 0x0200006A RID: 106
	internal sealed class ExpressionParser
	{
		// Token: 0x0600060B RID: 1547 RVA: 0x0001DEA8 File Offset: 0x0001C0A8
		internal ExpressionParser(DataTable table)
		{
			this._table = table;
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0001DF04 File Offset: 0x0001C104
		internal void LoadExpression(string data)
		{
			int num;
			if (data == null)
			{
				num = 0;
				this._text = new char[num + 1];
			}
			else
			{
				num = data.Length;
				this._text = new char[num + 1];
				data.CopyTo(0, this._text, 0, num);
			}
			this._text[num] = '\0';
			if (this._expression != null)
			{
				this._expression = null;
			}
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x0001DF64 File Offset: 0x0001C164
		internal void StartScan()
		{
			this._op = 0;
			this._pos = 0;
			this._start = 0;
			this._topOperator = 0;
			OperatorInfo[] ops = this._ops;
			int topOperator = this._topOperator;
			this._topOperator = topOperator + 1;
			ops[topOperator] = new OperatorInfo(Nodes.Noop, 0, 0);
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x0001DFB0 File Offset: 0x0001C1B0
		internal ExpressionNode Parse()
		{
			this._expression = null;
			this.StartScan();
			int num = 0;
			while (this._token != Tokens.EOS)
			{
				OperatorInfo operatorInfo;
				for (;;)
				{
					this.Scan();
					int num2;
					switch (this._token)
					{
					case Tokens.Name:
					case Tokens.Numeric:
					case Tokens.Decimal:
					case Tokens.Float:
					case Tokens.StringConst:
					case Tokens.Date:
					case Tokens.Parent:
					{
						ExpressionNode expressionNode = null;
						if (this._prevOperand != 0)
						{
							goto Block_5;
						}
						if (this._topOperator > 0)
						{
							operatorInfo = this._ops[this._topOperator - 1];
							if (operatorInfo._type == Nodes.Binop && operatorInfo._op == 5 && this._token != Tokens.Parent)
							{
								goto Block_9;
							}
						}
						this._prevOperand = 1;
						Tokens token = this._token;
						switch (token)
						{
						case Tokens.Name:
							operatorInfo = this._ops[this._topOperator - 1];
							expressionNode = new NameNode(this._table, this._text, this._start, this._pos);
							break;
						case Tokens.Numeric:
						{
							string text = new string(this._text, this._start, this._pos - this._start);
							expressionNode = new ConstNode(this._table, ValueType.Numeric, text);
							break;
						}
						case Tokens.Decimal:
						{
							string text = new string(this._text, this._start, this._pos - this._start);
							expressionNode = new ConstNode(this._table, ValueType.Decimal, text);
							break;
						}
						case Tokens.Float:
						{
							string text = new string(this._text, this._start, this._pos - this._start);
							expressionNode = new ConstNode(this._table, ValueType.Float, text);
							break;
						}
						case Tokens.BinaryConst:
							break;
						case Tokens.StringConst:
						{
							string text = new string(this._text, this._start + 1, this._pos - this._start - 2);
							expressionNode = new ConstNode(this._table, ValueType.Str, text);
							break;
						}
						case Tokens.Date:
						{
							string text = new string(this._text, this._start + 1, this._pos - this._start - 2);
							expressionNode = new ConstNode(this._table, ValueType.Date, text);
							break;
						}
						default:
							if (token == Tokens.Parent)
							{
								string text2;
								try
								{
									this.Scan();
									if (this._token == Tokens.LeftParen)
									{
										this.ScanToken(Tokens.Name);
										text2 = NameNode.ParseName(this._text, this._start, this._pos);
										this.ScanToken(Tokens.RightParen);
										this.ScanToken(Tokens.Dot);
									}
									else
									{
										text2 = null;
										this.CheckToken(Tokens.Dot);
									}
								}
								catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
								{
									throw ExprException.LookupArgument();
								}
								this.ScanToken(Tokens.Name);
								string text3 = NameNode.ParseName(this._text, this._start, this._pos);
								operatorInfo = this._ops[this._topOperator - 1];
								expressionNode = new LookupNode(this._table, text3, text2);
							}
							break;
						}
						this.NodePush(expressionNode);
						continue;
					}
					case Tokens.ListSeparator:
					{
						if (this._prevOperand == 0)
						{
							goto Block_23;
						}
						this.BuildExpression(3);
						operatorInfo = this._ops[this._topOperator - 1];
						if (operatorInfo._type != Nodes.Call)
						{
							goto Block_24;
						}
						ExpressionNode expressionNode2 = this.NodePop();
						FunctionNode functionNode = (FunctionNode)this.NodePop();
						functionNode.AddArgument(expressionNode2);
						this.NodePush(functionNode);
						this._prevOperand = 0;
						continue;
					}
					case Tokens.LeftParen:
						num++;
						if (this._prevOperand == 0)
						{
							operatorInfo = this._ops[this._topOperator - 1];
							if (operatorInfo._type == Nodes.Binop && operatorInfo._op == 5)
							{
								ExpressionNode expressionNode = new FunctionNode(this._table, "In");
								this.NodePush(expressionNode);
								OperatorInfo[] ops = this._ops;
								num2 = this._topOperator;
								this._topOperator = num2 + 1;
								ops[num2] = new OperatorInfo(Nodes.Call, 0, 2);
								continue;
							}
							OperatorInfo[] ops2 = this._ops;
							num2 = this._topOperator;
							this._topOperator = num2 + 1;
							ops2[num2] = new OperatorInfo(Nodes.Paren, 0, 2);
							continue;
						}
						else
						{
							this.BuildExpression(22);
							this._prevOperand = 0;
							ExpressionNode expressionNode3 = this.NodePeek();
							if (expressionNode3 == null || expressionNode3.GetType() != typeof(NameNode))
							{
								goto IL_041A;
							}
							NameNode nameNode = (NameNode)this.NodePop();
							ExpressionNode expressionNode = new FunctionNode(this._table, nameNode._name);
							Aggregate aggregate = (Aggregate)((FunctionNode)expressionNode).Aggregate;
							if (aggregate != Aggregate.None)
							{
								expressionNode = this.ParseAggregateArgument((FunctionId)aggregate);
								this.NodePush(expressionNode);
								this._prevOperand = 2;
								continue;
							}
							this.NodePush(expressionNode);
							OperatorInfo[] ops3 = this._ops;
							num2 = this._topOperator;
							this._topOperator = num2 + 1;
							ops3[num2] = new OperatorInfo(Nodes.Call, 0, 2);
							continue;
						}
						break;
					case Tokens.RightParen:
						if (this._prevOperand != 0)
						{
							this.BuildExpression(3);
						}
						if (this._topOperator <= 1)
						{
							goto Block_18;
						}
						this._topOperator--;
						operatorInfo = this._ops[this._topOperator];
						if (this._prevOperand == 0 && operatorInfo._type != Nodes.Call)
						{
							goto Block_20;
						}
						if (operatorInfo._type == Nodes.Call)
						{
							if (this._prevOperand != 0)
							{
								ExpressionNode expressionNode4 = this.NodePop();
								FunctionNode functionNode2 = (FunctionNode)this.NodePop();
								functionNode2.AddArgument(expressionNode4);
								functionNode2.Check();
								this.NodePush(functionNode2);
							}
						}
						else
						{
							ExpressionNode expressionNode = this.NodePop();
							expressionNode = new UnaryNode(this._table, 0, expressionNode);
							this.NodePush(expressionNode);
						}
						this._prevOperand = 2;
						num--;
						continue;
					case Tokens.ZeroOp:
					{
						if (this._prevOperand != 0)
						{
							goto Block_28;
						}
						OperatorInfo[] ops4 = this._ops;
						num2 = this._topOperator;
						this._topOperator = num2 + 1;
						ops4[num2] = new OperatorInfo(Nodes.Zop, this._op, 24);
						this._prevOperand = 2;
						continue;
					}
					case Tokens.UnaryOp:
						goto IL_0654;
					case Tokens.BinaryOp:
						if (this._prevOperand != 0)
						{
							this._prevOperand = 0;
							this.BuildExpression(Operators.Priority(this._op));
							OperatorInfo[] ops5 = this._ops;
							num2 = this._topOperator;
							this._topOperator = num2 + 1;
							ops5[num2] = new OperatorInfo(Nodes.Binop, this._op, Operators.Priority(this._op));
							continue;
						}
						if (this._op == 15)
						{
							this._op = 2;
							goto IL_0654;
						}
						if (this._op == 16)
						{
							this._op = 1;
							goto IL_0654;
						}
						goto IL_05F4;
					case Tokens.Dot:
					{
						ExpressionNode expressionNode5 = this.NodePeek();
						if (expressionNode5 != null && expressionNode5.GetType() == typeof(NameNode))
						{
							this.Scan();
							if (this._token == Tokens.Name)
							{
								string text4 = ((NameNode)this.NodePop())._name + "." + NameNode.ParseName(this._text, this._start, this._pos);
								this.NodePush(new NameNode(this._table, text4));
								continue;
							}
						}
						break;
					}
					case Tokens.EOS:
						goto IL_0079;
					}
					goto Block_1;
					IL_0654:
					OperatorInfo[] ops6 = this._ops;
					num2 = this._topOperator;
					this._topOperator = num2 + 1;
					ops6[num2] = new OperatorInfo(Nodes.Unop, this._op, Operators.Priority(this._op));
				}
				IL_0079:
				if (this._prevOperand == 0)
				{
					if (this._topNode != 0)
					{
						operatorInfo = this._ops[this._topOperator - 1];
						throw ExprException.MissingOperand(operatorInfo);
					}
					continue;
				}
				else
				{
					this.BuildExpression(3);
					if (this._topOperator != 1)
					{
						throw ExprException.MissingRightParen();
					}
					continue;
				}
				Block_1:
				goto IL_076B;
				Block_5:
				throw ExprException.MissingOperator(new string(this._text, this._start, this._pos - this._start));
				Block_9:
				throw ExprException.InWithoutParentheses();
				IL_041A:
				throw ExprException.SyntaxError();
				Block_18:
				throw ExprException.TooManyRightParentheses();
				Block_20:
				throw ExprException.MissingOperand(operatorInfo);
				Block_23:
				throw ExprException.MissingOperandBefore(",");
				Block_24:
				throw ExprException.SyntaxError();
				IL_05F4:
				throw ExprException.MissingOperandBefore(Operators.ToString(this._op));
				Block_28:
				throw ExprException.MissingOperator(new string(this._text, this._start, this._pos - this._start));
				IL_076B:
				throw ExprException.UnknownToken(new string(this._text, this._start, this._pos - this._start), this._start + 1);
			}
			this._expression = this._nodeStack[0];
			return this._expression;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x0001E788 File Offset: 0x0001C988
		private ExpressionNode ParseAggregateArgument(FunctionId aggregate)
		{
			this.Scan();
			string text;
			bool flag;
			string text2;
			try
			{
				if (this._token != Tokens.Child)
				{
					if (this._token != Tokens.Name)
					{
						throw ExprException.AggregateArgument();
					}
					text = NameNode.ParseName(this._text, this._start, this._pos);
					this.ScanToken(Tokens.RightParen);
					return new AggregateNode(this._table, aggregate, text);
				}
				else
				{
					flag = this._token == Tokens.Child;
					this._prevOperand = 1;
					this.Scan();
					if (this._token == Tokens.LeftParen)
					{
						this.ScanToken(Tokens.Name);
						text2 = NameNode.ParseName(this._text, this._start, this._pos);
						this.ScanToken(Tokens.RightParen);
						this.ScanToken(Tokens.Dot);
					}
					else
					{
						text2 = null;
						this.CheckToken(Tokens.Dot);
					}
					this.ScanToken(Tokens.Name);
					text = NameNode.ParseName(this._text, this._start, this._pos);
					this.ScanToken(Tokens.RightParen);
				}
			}
			catch (Exception ex) when (ADP.IsCatchableExceptionType(ex))
			{
				throw ExprException.AggregateArgument();
			}
			return new AggregateNode(this._table, aggregate, text, !flag, text2);
		}

		// Token: 0x06000610 RID: 1552 RVA: 0x0001E8B0 File Offset: 0x0001CAB0
		private ExpressionNode NodePop()
		{
			ExpressionNode[] nodeStack = this._nodeStack;
			int num = this._topNode - 1;
			this._topNode = num;
			return nodeStack[num];
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x0001E8D5 File Offset: 0x0001CAD5
		private ExpressionNode NodePeek()
		{
			if (this._topNode <= 0)
			{
				return null;
			}
			return this._nodeStack[this._topNode - 1];
		}

		// Token: 0x06000612 RID: 1554 RVA: 0x0001E8F4 File Offset: 0x0001CAF4
		private void NodePush(ExpressionNode node)
		{
			if (this._topNode >= 98)
			{
				throw ExprException.ExpressionTooComplex();
			}
			ExpressionNode[] nodeStack = this._nodeStack;
			int topNode = this._topNode;
			this._topNode = topNode + 1;
			nodeStack[topNode] = node;
		}

		// Token: 0x06000613 RID: 1555 RVA: 0x0001E92C File Offset: 0x0001CB2C
		private void BuildExpression(int pri)
		{
			OperatorInfo operatorInfo;
			for (;;)
			{
				operatorInfo = this._ops[this._topOperator - 1];
				if (operatorInfo._priority < pri)
				{
					return;
				}
				this._topOperator--;
				ExpressionNode expressionNode2;
				switch (operatorInfo._type)
				{
				case Nodes.Unop:
				{
					ExpressionNode expressionNode = this.NodePop();
					int op = operatorInfo._op;
					if (op != 1 && op != 3 && op == 25)
					{
						goto Block_6;
					}
					expressionNode2 = new UnaryNode(this._table, operatorInfo._op, expressionNode);
					goto IL_0163;
				}
				case Nodes.UnopSpec:
				case Nodes.BinopSpec:
					return;
				case Nodes.Binop:
				{
					ExpressionNode expressionNode = this.NodePop();
					ExpressionNode expressionNode3 = this.NodePop();
					switch (operatorInfo._op)
					{
					case 4:
					case 6:
					case 22:
					case 23:
					case 24:
					case 25:
						goto IL_00D3;
					}
					if (operatorInfo._op == 14)
					{
						expressionNode2 = new LikeNode(this._table, operatorInfo._op, expressionNode3, expressionNode);
						goto IL_0163;
					}
					expressionNode2 = new BinaryNode(this._table, operatorInfo._op, expressionNode3, expressionNode);
					goto IL_0163;
				}
				case Nodes.Zop:
					expressionNode2 = new ZeroOpNode(operatorInfo._op);
					goto IL_0163;
				}
				break;
				IL_0163:
				this.NodePush(expressionNode2);
			}
			return;
			IL_00D3:
			throw ExprException.UnsupportedOperator(operatorInfo._op);
			Block_6:
			throw ExprException.UnsupportedOperator(operatorInfo._op);
		}

		// Token: 0x06000614 RID: 1556 RVA: 0x0001EAA8 File Offset: 0x0001CCA8
		internal void CheckToken(Tokens token)
		{
			if (this._token != token)
			{
				throw ExprException.UnknownToken(token, this._token, this._pos);
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x0001EAC8 File Offset: 0x0001CCC8
		internal Tokens Scan()
		{
			char[] text = this._text;
			this._token = Tokens.None;
			char c;
			for (;;)
			{
				this._start = this._pos;
				this._op = 0;
				char[] array = text;
				int pos = this._pos;
				this._pos = pos + 1;
				c = array[pos];
				if (c > '>')
				{
					goto IL_00CD;
				}
				if (c > '\r')
				{
					switch (c)
					{
					case ' ':
						goto IL_0111;
					case '!':
					case '"':
					case '$':
					case ',':
					case '.':
						goto IL_0311;
					case '#':
						goto IL_0136;
					case '%':
						goto IL_026E;
					case '&':
						goto IL_0283;
					case '\'':
						goto IL_0148;
					case '(':
						goto IL_011C;
					case ')':
						goto IL_0129;
					case '*':
						goto IL_0244;
					case '+':
						goto IL_021A;
					case '-':
						goto IL_022F;
					case '/':
						goto IL_0259;
					}
					goto Block_5;
				}
				if (c != '\0')
				{
					switch (c)
					{
					case '\t':
					case '\n':
					case '\r':
						goto IL_0111;
					}
					break;
				}
				goto IL_0104;
				IL_0111:
				this.ScanWhite();
			}
			goto IL_0311;
			Block_5:
			switch (c)
			{
			case '<':
				this._token = Tokens.BinaryOp;
				this.ScanWhite();
				if (text[this._pos] == '=')
				{
					this._pos++;
					this._op = 11;
					goto IL_03E5;
				}
				if (text[this._pos] == '>')
				{
					this._pos++;
					this._op = 12;
					goto IL_03E5;
				}
				this._op = 9;
				goto IL_03E5;
			case '=':
				this._token = Tokens.BinaryOp;
				this._op = 7;
				goto IL_03E5;
			case '>':
				this._token = Tokens.BinaryOp;
				this.ScanWhite();
				if (text[this._pos] == '=')
				{
					this._pos++;
					this._op = 10;
					goto IL_03E5;
				}
				this._op = 8;
				goto IL_03E5;
			default:
				goto IL_0311;
			}
			IL_00CD:
			if (c <= '^')
			{
				if (c == '[')
				{
					this.ScanName(']', this._escape, "]\\");
					this.CheckToken(Tokens.Name);
					goto IL_03E5;
				}
				if (c != '^')
				{
					goto IL_0311;
				}
				this._token = Tokens.BinaryOp;
				this._op = 24;
				goto IL_03E5;
			}
			else
			{
				if (c == '`')
				{
					this.ScanName('`', '`', "`");
					this.CheckToken(Tokens.Name);
					goto IL_03E5;
				}
				if (c == '|')
				{
					this._token = Tokens.BinaryOp;
					this._op = 23;
					goto IL_03E5;
				}
				if (c != '~')
				{
					goto IL_0311;
				}
				this._token = Tokens.BinaryOp;
				this._op = 25;
				goto IL_03E5;
			}
			IL_0104:
			this._token = Tokens.EOS;
			goto IL_03E5;
			IL_011C:
			this._token = Tokens.LeftParen;
			goto IL_03E5;
			IL_0129:
			this._token = Tokens.RightParen;
			goto IL_03E5;
			IL_0136:
			this.ScanDate();
			this.CheckToken(Tokens.Date);
			goto IL_03E5;
			IL_0148:
			this.ScanString('\'');
			this.CheckToken(Tokens.StringConst);
			goto IL_03E5;
			IL_021A:
			this._token = Tokens.BinaryOp;
			this._op = 15;
			goto IL_03E5;
			IL_022F:
			this._token = Tokens.BinaryOp;
			this._op = 16;
			goto IL_03E5;
			IL_0244:
			this._token = Tokens.BinaryOp;
			this._op = 17;
			goto IL_03E5;
			IL_0259:
			this._token = Tokens.BinaryOp;
			this._op = 18;
			goto IL_03E5;
			IL_026E:
			this._token = Tokens.BinaryOp;
			this._op = 20;
			goto IL_03E5;
			IL_0283:
			this._token = Tokens.BinaryOp;
			this._op = 22;
			goto IL_03E5;
			IL_0311:
			if (c == this._listSeparator)
			{
				this._token = Tokens.ListSeparator;
			}
			else if (c == '.')
			{
				if (this._prevOperand == 0)
				{
					this.ScanNumeric();
				}
				else
				{
					this._token = Tokens.Dot;
				}
			}
			else if (c == '0' && (text[this._pos] == 'x' || text[this._pos] == 'X'))
			{
				this.ScanBinaryConstant();
				this._token = Tokens.BinaryConst;
			}
			else if (this.IsDigit(c))
			{
				this.ScanNumeric();
			}
			else
			{
				this.ScanReserved();
				if (this._token == Tokens.None)
				{
					if (this.IsAlphaNumeric(c))
					{
						this.ScanName();
						if (this._token != Tokens.None)
						{
							this.CheckToken(Tokens.Name);
							goto IL_03E5;
						}
					}
					this._token = Tokens.Unknown;
					throw ExprException.UnknownToken(new string(text, this._start, this._pos - this._start), this._start + 1);
				}
			}
			IL_03E5:
			return this._token;
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0001EEC0 File Offset: 0x0001D0C0
		private void ScanNumeric()
		{
			char[] text = this._text;
			bool flag = false;
			bool flag2 = false;
			while (this.IsDigit(text[this._pos]))
			{
				this._pos++;
			}
			if (text[this._pos] == this._decimalSeparator)
			{
				flag = true;
				this._pos++;
			}
			while (this.IsDigit(text[this._pos]))
			{
				this._pos++;
			}
			if (text[this._pos] == this._exponentL || text[this._pos] == this._exponentU)
			{
				flag2 = true;
				this._pos++;
				if (text[this._pos] == '-' || text[this._pos] == '+')
				{
					this._pos++;
				}
				while (this.IsDigit(text[this._pos]))
				{
					this._pos++;
				}
			}
			if (flag2)
			{
				this._token = Tokens.Float;
				return;
			}
			if (flag)
			{
				this._token = Tokens.Decimal;
				return;
			}
			this._token = Tokens.Numeric;
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x0001EFCC File Offset: 0x0001D1CC
		private void ScanName()
		{
			char[] text = this._text;
			while (this.IsAlphaNumeric(text[this._pos]))
			{
				this._pos++;
			}
			this._token = Tokens.Name;
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x0001F008 File Offset: 0x0001D208
		private void ScanName(char chEnd, char esc, string charsToEscape)
		{
			char[] text = this._text;
			do
			{
				if (text[this._pos] == esc && this._pos + 1 < text.Length && charsToEscape.IndexOf(text[this._pos + 1]) >= 0)
				{
					this._pos++;
				}
				this._pos++;
			}
			while (this._pos < text.Length && text[this._pos] != chEnd);
			if (this._pos >= text.Length)
			{
				throw ExprException.InvalidNameBracketing(new string(text, this._start, this._pos - 1 - this._start));
			}
			this._pos++;
			this._token = Tokens.Name;
		}

		// Token: 0x06000619 RID: 1561 RVA: 0x0001F0BC File Offset: 0x0001D2BC
		private void ScanDate()
		{
			char[] text = this._text;
			do
			{
				this._pos++;
			}
			while (this._pos < text.Length && text[this._pos] != '#');
			if (this._pos < text.Length && text[this._pos] == '#')
			{
				this._token = Tokens.Date;
				this._pos++;
				return;
			}
			if (this._pos >= text.Length)
			{
				throw ExprException.InvalidDate(new string(text, this._start, this._pos - 1 - this._start));
			}
			throw ExprException.InvalidDate(new string(text, this._start, this._pos - this._start));
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0001F16C File Offset: 0x0001D36C
		private void ScanBinaryConstant()
		{
			char[] text = this._text;
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0001F178 File Offset: 0x0001D378
		private void ScanReserved()
		{
			char[] text = this._text;
			if (this.IsAlpha(text[this._pos]))
			{
				this.ScanName();
				string text2 = new string(text, this._start, this._pos - this._start);
				CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
				int num = 0;
				int num2 = ExpressionParser.s_reservedwords.Length - 1;
				int num3;
				for (;;)
				{
					num3 = (num + num2) / 2;
					int num4 = compareInfo.Compare(ExpressionParser.s_reservedwords[num3]._word, text2, CompareOptions.IgnoreCase);
					if (num4 == 0)
					{
						break;
					}
					if (num4 < 0)
					{
						num = num3 + 1;
					}
					else
					{
						num2 = num3 - 1;
					}
					if (num > num2)
					{
						return;
					}
				}
				this._token = ExpressionParser.s_reservedwords[num3]._token;
				this._op = ExpressionParser.s_reservedwords[num3]._op;
				return;
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0001F244 File Offset: 0x0001D444
		private void ScanString(char escape)
		{
			char[] text = this._text;
			while (this._pos < text.Length)
			{
				char[] array = text;
				int pos = this._pos;
				this._pos = pos + 1;
				char c = array[pos];
				if (c == escape && this._pos < text.Length && text[this._pos] == escape)
				{
					this._pos++;
				}
				else if (c == escape)
				{
					break;
				}
			}
			if (this._pos >= text.Length)
			{
				throw ExprException.InvalidString(new string(text, this._start, this._pos - 1 - this._start));
			}
			this._token = Tokens.StringConst;
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0001F2DA File Offset: 0x0001D4DA
		internal void ScanToken(Tokens token)
		{
			this.Scan();
			this.CheckToken(token);
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		private void ScanWhite()
		{
			char[] text = this._text;
			while (this._pos < text.Length && this.IsWhiteSpace(text[this._pos]))
			{
				this._pos++;
			}
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0001F32B File Offset: 0x0001D52B
		private bool IsWhiteSpace(char ch)
		{
			return ch <= ' ' && ch > '\0';
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0001F338 File Offset: 0x0001D538
		private bool IsAlphaNumeric(char ch)
		{
			switch (ch)
			{
			case '$':
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'G':
			case 'H':
			case 'I':
			case 'J':
			case 'K':
			case 'L':
			case 'M':
			case 'N':
			case 'O':
			case 'P':
			case 'Q':
			case 'R':
			case 'S':
			case 'T':
			case 'U':
			case 'V':
			case 'W':
			case 'X':
			case 'Y':
			case 'Z':
			case '_':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'g':
			case 'h':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'm':
			case 'n':
			case 'o':
			case 'p':
			case 'q':
			case 'r':
			case 's':
			case 't':
			case 'u':
			case 'v':
			case 'w':
			case 'x':
			case 'y':
			case 'z':
				return true;
			}
			return ch > '\u007f';
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0001F4B6 File Offset: 0x0001D6B6
		private bool IsDigit(char ch)
		{
			switch (ch)
			{
			case '0':
			case '1':
			case '2':
			case '3':
			case '4':
			case '5':
			case '6':
			case '7':
			case '8':
			case '9':
				return true;
			default:
				return false;
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x0001F4F0 File Offset: 0x0001D6F0
		private bool IsAlpha(char ch)
		{
			switch (ch)
			{
			case 'A':
			case 'B':
			case 'C':
			case 'D':
			case 'E':
			case 'F':
			case 'G':
			case 'H':
			case 'I':
			case 'J':
			case 'K':
			case 'L':
			case 'M':
			case 'N':
			case 'O':
			case 'P':
			case 'Q':
			case 'R':
			case 'S':
			case 'T':
			case 'U':
			case 'V':
			case 'W':
			case 'X':
			case 'Y':
			case 'Z':
			case '_':
			case 'a':
			case 'b':
			case 'c':
			case 'd':
			case 'e':
			case 'f':
			case 'g':
			case 'h':
			case 'i':
			case 'j':
			case 'k':
			case 'l':
			case 'm':
			case 'n':
			case 'o':
			case 'p':
			case 'q':
			case 'r':
			case 's':
			case 't':
			case 'u':
			case 'v':
			case 'w':
			case 'x':
			case 'y':
			case 'z':
				return true;
			}
			return false;
		}

		// Token: 0x04000242 RID: 578
		private static readonly ExpressionParser.ReservedWords[] s_reservedwords = new ExpressionParser.ReservedWords[]
		{
			new ExpressionParser.ReservedWords("And", Tokens.BinaryOp, 26),
			new ExpressionParser.ReservedWords("Between", Tokens.BinaryOp, 6),
			new ExpressionParser.ReservedWords("Child", Tokens.Child, 0),
			new ExpressionParser.ReservedWords("False", Tokens.ZeroOp, 34),
			new ExpressionParser.ReservedWords("In", Tokens.BinaryOp, 5),
			new ExpressionParser.ReservedWords("Is", Tokens.BinaryOp, 13),
			new ExpressionParser.ReservedWords("Like", Tokens.BinaryOp, 14),
			new ExpressionParser.ReservedWords("Not", Tokens.UnaryOp, 3),
			new ExpressionParser.ReservedWords("Null", Tokens.ZeroOp, 32),
			new ExpressionParser.ReservedWords("Or", Tokens.BinaryOp, 27),
			new ExpressionParser.ReservedWords("Parent", Tokens.Parent, 0),
			new ExpressionParser.ReservedWords("True", Tokens.ZeroOp, 33)
		};

		// Token: 0x04000243 RID: 579
		private char _escape = '\\';

		// Token: 0x04000244 RID: 580
		private char _decimalSeparator = '.';

		// Token: 0x04000245 RID: 581
		private char _listSeparator = ',';

		// Token: 0x04000246 RID: 582
		private char _exponentL = 'e';

		// Token: 0x04000247 RID: 583
		private char _exponentU = 'E';

		// Token: 0x04000248 RID: 584
		internal char[] _text;

		// Token: 0x04000249 RID: 585
		internal int _pos;

		// Token: 0x0400024A RID: 586
		internal int _start;

		// Token: 0x0400024B RID: 587
		internal Tokens _token;

		// Token: 0x0400024C RID: 588
		internal int _op;

		// Token: 0x0400024D RID: 589
		internal OperatorInfo[] _ops = new OperatorInfo[100];

		// Token: 0x0400024E RID: 590
		internal int _topOperator;

		// Token: 0x0400024F RID: 591
		internal int _topNode;

		// Token: 0x04000250 RID: 592
		private readonly DataTable _table;

		// Token: 0x04000251 RID: 593
		internal ExpressionNode[] _nodeStack = new ExpressionNode[100];

		// Token: 0x04000252 RID: 594
		internal int _prevOperand;

		// Token: 0x04000253 RID: 595
		internal ExpressionNode _expression;

		// Token: 0x0200006B RID: 107
		private readonly struct ReservedWords
		{
			// Token: 0x06000624 RID: 1572 RVA: 0x0001F707 File Offset: 0x0001D907
			internal ReservedWords(string word, Tokens token, int op)
			{
				this._word = word;
				this._token = token;
				this._op = op;
			}

			// Token: 0x04000254 RID: 596
			internal readonly string _word;

			// Token: 0x04000255 RID: 597
			internal readonly Tokens _token;

			// Token: 0x04000256 RID: 598
			internal readonly int _op;
		}
	}
}
