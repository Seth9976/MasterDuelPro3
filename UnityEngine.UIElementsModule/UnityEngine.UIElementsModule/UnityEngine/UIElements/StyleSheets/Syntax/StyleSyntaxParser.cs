using System;
using System.Collections.Generic;
using UnityEngine.Bindings;

namespace UnityEngine.UIElements.StyleSheets.Syntax
{
	// Token: 0x020005C9 RID: 1481
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class StyleSyntaxParser
	{
		// Token: 0x06002813 RID: 10259 RVA: 0x000A56D4 File Offset: 0x000A38D4
		public Expression Parse(string syntax)
		{
			bool flag = string.IsNullOrEmpty(syntax);
			Expression expression;
			if (flag)
			{
				expression = null;
			}
			else
			{
				Expression tree = null;
				bool flag2 = !this.m_ParsedExpressionCache.TryGetValue(syntax, out tree);
				if (flag2)
				{
					StyleSyntaxTokenizer tokenizer = new StyleSyntaxTokenizer();
					tokenizer.Tokenize(syntax);
					try
					{
						tree = this.ParseExpression(tokenizer);
					}
					catch (Exception e)
					{
						Debug.LogException(e);
					}
					this.m_ParsedExpressionCache[syntax] = tree;
				}
				expression = tree;
			}
			return expression;
		}

		// Token: 0x06002814 RID: 10260 RVA: 0x000A5758 File Offset: 0x000A3958
		private Expression ParseExpression(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken token = tokenizer.current;
			while (!StyleSyntaxParser.IsExpressionEnd(token))
			{
				bool flag = token.type == StyleSyntaxTokenType.String || token.type == StyleSyntaxTokenType.LessThan;
				Expression expression;
				if (flag)
				{
					expression = this.ParseTerm(tokenizer);
				}
				else
				{
					bool flag2 = token.type == StyleSyntaxTokenType.OpenBracket;
					if (!flag2)
					{
						throw new Exception(string.Format("Unexpected token '{0}' in expression", token.type));
					}
					expression = this.ParseGroup(tokenizer);
				}
				this.m_ExpressionStack.Push(expression);
				ExpressionCombinator nextCombinatorType = this.ParseCombinatorType(tokenizer);
				bool flag3 = nextCombinatorType > ExpressionCombinator.None;
				if (flag3)
				{
					bool flag4 = this.m_CombinatorStack.Count > 0;
					if (flag4)
					{
						ExpressionCombinator previousCombinator = this.m_CombinatorStack.Peek();
						int previousPrecedence = (int)previousCombinator;
						int currentPrecedence = (int)nextCombinatorType;
						while (previousPrecedence > currentPrecedence && previousCombinator != ExpressionCombinator.Group)
						{
							this.ProcessCombinatorStack();
							previousCombinator = ((this.m_CombinatorStack.Count > 0) ? this.m_CombinatorStack.Peek() : ExpressionCombinator.None);
							previousPrecedence = (int)previousCombinator;
						}
					}
					this.m_CombinatorStack.Push(nextCombinatorType);
				}
				token = tokenizer.current;
			}
			while (this.m_CombinatorStack.Count > 0)
			{
				ExpressionCombinator combinatorType = this.m_CombinatorStack.Peek();
				bool flag5 = combinatorType == ExpressionCombinator.Group;
				if (flag5)
				{
					this.m_CombinatorStack.Pop();
					break;
				}
				this.ProcessCombinatorStack();
			}
			return this.m_ExpressionStack.Pop();
		}

		// Token: 0x06002815 RID: 10261 RVA: 0x000A58E4 File Offset: 0x000A3AE4
		private void ProcessCombinatorStack()
		{
			ExpressionCombinator combinatorType = this.m_CombinatorStack.Pop();
			Expression exp2 = this.m_ExpressionStack.Pop();
			Expression exp3 = this.m_ExpressionStack.Pop();
			this.m_ProcessExpressionList.Clear();
			this.m_ProcessExpressionList.Add(exp3);
			this.m_ProcessExpressionList.Add(exp2);
			while (this.m_CombinatorStack.Count > 0 && combinatorType == this.m_CombinatorStack.Peek())
			{
				Expression e = this.m_ExpressionStack.Pop();
				this.m_ProcessExpressionList.Insert(0, e);
				this.m_CombinatorStack.Pop();
			}
			Expression c = new Expression(ExpressionType.Combinator);
			c.combinator = combinatorType;
			c.subExpressions = this.m_ProcessExpressionList.ToArray();
			this.m_ExpressionStack.Push(c);
		}

		// Token: 0x06002816 RID: 10262 RVA: 0x000A59BC File Offset: 0x000A3BBC
		private Expression ParseTerm(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken token = tokenizer.current;
			bool flag = token.type == StyleSyntaxTokenType.LessThan;
			Expression exp;
			if (flag)
			{
				exp = this.ParseDataType(tokenizer);
			}
			else
			{
				bool flag2 = token.type == StyleSyntaxTokenType.String;
				if (!flag2)
				{
					throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected term token", token.type));
				}
				exp = new Expression(ExpressionType.Keyword);
				exp.keyword = token.text.ToLower();
				tokenizer.MoveNext();
			}
			this.ParseMultiplier(tokenizer, ref exp.multiplier);
			return exp;
		}

		// Token: 0x06002817 RID: 10263 RVA: 0x000A5A50 File Offset: 0x000A3C50
		private ExpressionCombinator ParseCombinatorType(StyleSyntaxTokenizer tokenizer)
		{
			ExpressionCombinator type = ExpressionCombinator.None;
			StyleSyntaxToken token = tokenizer.current;
			while (!StyleSyntaxParser.IsExpressionEnd(token) && type == ExpressionCombinator.None)
			{
				StyleSyntaxToken next = tokenizer.PeekNext();
				switch (token.type)
				{
				case StyleSyntaxTokenType.Space:
				{
					bool flag = !StyleSyntaxParser.IsCombinator(next) && next.type != StyleSyntaxTokenType.CloseBracket;
					if (flag)
					{
						type = ExpressionCombinator.Juxtaposition;
					}
					break;
				}
				case StyleSyntaxTokenType.SingleBar:
					type = ExpressionCombinator.Or;
					break;
				case StyleSyntaxTokenType.DoubleBar:
					type = ExpressionCombinator.OrOr;
					break;
				case StyleSyntaxTokenType.DoubleAmpersand:
					type = ExpressionCombinator.AndAnd;
					break;
				default:
					throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected combinator token", token.type));
				}
				token = tokenizer.MoveNext();
			}
			StyleSyntaxParser.EatSpace(tokenizer);
			return type;
		}

		// Token: 0x06002818 RID: 10264 RVA: 0x000A5B14 File Offset: 0x000A3D14
		private Expression ParseGroup(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken token = tokenizer.current;
			bool flag = token.type != StyleSyntaxTokenType.OpenBracket;
			if (flag)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in group expression. Expected '[' token", token.type));
			}
			this.m_CombinatorStack.Push(ExpressionCombinator.Group);
			tokenizer.MoveNext();
			StyleSyntaxParser.EatSpace(tokenizer);
			Expression subExpression = this.ParseExpression(tokenizer);
			token = tokenizer.current;
			bool flag2 = token.type != StyleSyntaxTokenType.CloseBracket;
			if (flag2)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in group expression. Expected ']' token", token.type));
			}
			tokenizer.MoveNext();
			Expression group = new Expression(ExpressionType.Combinator);
			group.combinator = ExpressionCombinator.Group;
			group.subExpressions = new Expression[] { subExpression };
			this.ParseMultiplier(tokenizer, ref group.multiplier);
			return group;
		}

		// Token: 0x06002819 RID: 10265 RVA: 0x000A5BE8 File Offset: 0x000A3DE8
		private Expression ParseDataType(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken token = tokenizer.current;
			bool flag = token.type != StyleSyntaxTokenType.LessThan;
			if (flag)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in data type expression. Expected '<' token", token.type));
			}
			token = tokenizer.MoveNext();
			StyleSyntaxTokenType type = token.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			Expression exp;
			if (styleSyntaxTokenType != StyleSyntaxTokenType.String)
			{
				if (styleSyntaxTokenType != StyleSyntaxTokenType.SingleQuote)
				{
					throw new Exception(string.Format("Unexpected token '{0}' in data type expression", token.type));
				}
				exp = this.ParseProperty(tokenizer);
			}
			else
			{
				string syntaxAlias;
				bool flag2 = StylePropertyCache.TryGetNonTerminalValue(token.text, out syntaxAlias);
				if (flag2)
				{
					exp = this.ParseNonTerminalValue(syntaxAlias);
				}
				else
				{
					DataType dataType = DataType.None;
					try
					{
						object enumValue = Enum.Parse(typeof(DataType), token.text.Replace("-", ""), true);
						bool flag3 = enumValue != null;
						if (flag3)
						{
							dataType = (DataType)enumValue;
						}
					}
					catch (Exception)
					{
						throw new Exception("Unknown data type '" + token.text + "'");
					}
					exp = new Expression(ExpressionType.Data);
					exp.dataType = dataType;
				}
				tokenizer.MoveNext();
			}
			token = tokenizer.current;
			bool flag4 = token.type != StyleSyntaxTokenType.GreaterThan;
			if (flag4)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in data type expression. Expected '>' token", token.type));
			}
			tokenizer.MoveNext();
			return exp;
		}

		// Token: 0x0600281A RID: 10266 RVA: 0x000A5D64 File Offset: 0x000A3F64
		private Expression ParseNonTerminalValue(string syntax)
		{
			Expression exp = null;
			bool flag = !this.m_ParsedExpressionCache.TryGetValue(syntax, out exp);
			if (flag)
			{
				this.m_CombinatorStack.Push(ExpressionCombinator.Group);
				exp = this.Parse(syntax);
			}
			return new Expression(ExpressionType.Combinator)
			{
				combinator = ExpressionCombinator.Group,
				subExpressions = new Expression[] { exp }
			};
		}

		// Token: 0x0600281B RID: 10267 RVA: 0x000A5DC4 File Offset: 0x000A3FC4
		private Expression ParseProperty(StyleSyntaxTokenizer tokenizer)
		{
			Expression exp = null;
			StyleSyntaxToken token = tokenizer.current;
			bool flag = token.type != StyleSyntaxTokenType.SingleQuote;
			if (flag)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected ''' token", token.type));
			}
			token = tokenizer.MoveNext();
			bool flag2 = token.type != StyleSyntaxTokenType.String;
			if (flag2)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected 'string' token", token.type));
			}
			string propertyName = token.text;
			string syntax;
			bool flag3 = !StylePropertyCache.TryGetSyntax(propertyName, out syntax);
			if (flag3)
			{
				throw new Exception("Unknown property '" + propertyName + "' <''> expression.");
			}
			bool flag4 = !this.m_ParsedExpressionCache.TryGetValue(syntax, out exp);
			if (flag4)
			{
				this.m_CombinatorStack.Push(ExpressionCombinator.Group);
				exp = this.Parse(syntax);
			}
			token = tokenizer.MoveNext();
			bool flag5 = token.type != StyleSyntaxTokenType.SingleQuote;
			if (flag5)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected ''' token", token.type));
			}
			token = tokenizer.MoveNext();
			bool flag6 = token.type != StyleSyntaxTokenType.GreaterThan;
			if (flag6)
			{
				throw new Exception(string.Format("Unexpected token '{0}' in property expression. Expected '>' token", token.type));
			}
			return new Expression(ExpressionType.Combinator)
			{
				combinator = ExpressionCombinator.Group,
				subExpressions = new Expression[] { exp }
			};
		}

		// Token: 0x0600281C RID: 10268 RVA: 0x000A5F2C File Offset: 0x000A412C
		private void ParseMultiplier(StyleSyntaxTokenizer tokenizer, ref ExpressionMultiplier multiplier)
		{
			StyleSyntaxToken token = tokenizer.current;
			bool flag = StyleSyntaxParser.IsMultiplier(token);
			if (flag)
			{
				switch (token.type)
				{
				case StyleSyntaxTokenType.Asterisk:
					multiplier.type = ExpressionMultiplierType.ZeroOrMore;
					goto IL_00A1;
				case StyleSyntaxTokenType.Plus:
					multiplier.type = ExpressionMultiplierType.OneOrMore;
					goto IL_00A1;
				case StyleSyntaxTokenType.QuestionMark:
					multiplier.type = ExpressionMultiplierType.ZeroOrOne;
					goto IL_00A1;
				case StyleSyntaxTokenType.HashMark:
					multiplier.type = ExpressionMultiplierType.OneOrMoreComma;
					goto IL_00A1;
				case StyleSyntaxTokenType.ExclamationPoint:
					multiplier.type = ExpressionMultiplierType.GroupAtLeastOne;
					goto IL_00A1;
				case StyleSyntaxTokenType.OpenBrace:
					multiplier.type = ExpressionMultiplierType.Ranges;
					goto IL_00A1;
				}
				throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected multiplier token", token.type));
				IL_00A1:
				token = tokenizer.MoveNext();
			}
			bool flag2 = multiplier.type == ExpressionMultiplierType.Ranges;
			if (flag2)
			{
				this.ParseRanges(tokenizer, out multiplier.min, out multiplier.max);
			}
		}

		// Token: 0x0600281D RID: 10269 RVA: 0x000A6008 File Offset: 0x000A4208
		private void ParseRanges(StyleSyntaxTokenizer tokenizer, out int min, out int max)
		{
			min = -1;
			max = -1;
			StyleSyntaxToken token = tokenizer.current;
			bool foundComma = false;
			while (token.type != StyleSyntaxTokenType.CloseBrace)
			{
				StyleSyntaxTokenType type = token.type;
				StyleSyntaxTokenType styleSyntaxTokenType = type;
				if (styleSyntaxTokenType != StyleSyntaxTokenType.Number)
				{
					if (styleSyntaxTokenType != StyleSyntaxTokenType.Comma)
					{
						throw new Exception(string.Format("Unexpected token '{0}' in expression. Expected ranges token", token.type));
					}
					foundComma = true;
				}
				else
				{
					bool flag = !foundComma;
					if (flag)
					{
						min = token.number;
					}
					else
					{
						max = token.number;
					}
				}
				token = tokenizer.MoveNext();
			}
			bool flag2 = !foundComma;
			if (flag2)
			{
				max = min;
			}
			tokenizer.MoveNext();
		}

		// Token: 0x0600281E RID: 10270 RVA: 0x000A60B0 File Offset: 0x000A42B0
		private static void EatSpace(StyleSyntaxTokenizer tokenizer)
		{
			StyleSyntaxToken token = tokenizer.current;
			bool flag = token.type == StyleSyntaxTokenType.Space;
			if (flag)
			{
				tokenizer.MoveNext();
			}
		}

		// Token: 0x0600281F RID: 10271 RVA: 0x000A60DC File Offset: 0x000A42DC
		private static bool IsExpressionEnd(StyleSyntaxToken token)
		{
			StyleSyntaxTokenType type = token.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			return styleSyntaxTokenType == StyleSyntaxTokenType.CloseBracket || styleSyntaxTokenType == StyleSyntaxTokenType.End;
		}

		// Token: 0x06002820 RID: 10272 RVA: 0x000A610C File Offset: 0x000A430C
		private static bool IsCombinator(StyleSyntaxToken token)
		{
			StyleSyntaxTokenType type = token.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			return styleSyntaxTokenType - StyleSyntaxTokenType.Space <= 3;
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000A6134 File Offset: 0x000A4334
		private static bool IsMultiplier(StyleSyntaxToken token)
		{
			StyleSyntaxTokenType type = token.type;
			StyleSyntaxTokenType styleSyntaxTokenType = type;
			return styleSyntaxTokenType - StyleSyntaxTokenType.Asterisk <= 4 || styleSyntaxTokenType == StyleSyntaxTokenType.OpenBrace;
		}

		// Token: 0x0400154A RID: 5450
		private List<Expression> m_ProcessExpressionList = new List<Expression>();

		// Token: 0x0400154B RID: 5451
		private Stack<Expression> m_ExpressionStack = new Stack<Expression>();

		// Token: 0x0400154C RID: 5452
		private Stack<ExpressionCombinator> m_CombinatorStack = new Stack<ExpressionCombinator>();

		// Token: 0x0400154D RID: 5453
		private Dictionary<string, Expression> m_ParsedExpressionCache = new Dictionary<string, Expression>();
	}
}
