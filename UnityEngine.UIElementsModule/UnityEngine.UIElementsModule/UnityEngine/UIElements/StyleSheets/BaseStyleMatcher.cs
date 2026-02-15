using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.UIElements.StyleSheets.Syntax;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005C0 RID: 1472
	internal abstract class BaseStyleMatcher
	{
		// Token: 0x060027D7 RID: 10199
		protected abstract bool MatchKeyword(string keyword);

		// Token: 0x060027D8 RID: 10200
		protected abstract bool MatchNumber();

		// Token: 0x060027D9 RID: 10201
		protected abstract bool MatchInteger();

		// Token: 0x060027DA RID: 10202
		protected abstract bool MatchLength();

		// Token: 0x060027DB RID: 10203
		protected abstract bool MatchPercentage();

		// Token: 0x060027DC RID: 10204
		protected abstract bool MatchColor();

		// Token: 0x060027DD RID: 10205
		protected abstract bool MatchResource();

		// Token: 0x060027DE RID: 10206
		protected abstract bool MatchUrl();

		// Token: 0x060027DF RID: 10207
		protected abstract bool MatchTime();

		// Token: 0x060027E0 RID: 10208
		protected abstract bool MatchAngle();

		// Token: 0x060027E1 RID: 10209
		protected abstract bool MatchCustomIdent();

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x060027E2 RID: 10210
		public abstract int valueCount { get; }

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x060027E3 RID: 10211
		public abstract bool isCurrentVariable { get; }

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x060027E4 RID: 10212
		public abstract bool isCurrentComma { get; }

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x060027E5 RID: 10213 RVA: 0x000A48DB File Offset: 0x000A2ADB
		public bool hasCurrent
		{
			get
			{
				return this.m_CurrentContext.valueIndex < this.valueCount;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x060027E6 RID: 10214 RVA: 0x000A48F0 File Offset: 0x000A2AF0
		// (set) Token: 0x060027E7 RID: 10215 RVA: 0x000A48FD File Offset: 0x000A2AFD
		public int currentIndex
		{
			get
			{
				return this.m_CurrentContext.valueIndex;
			}
			set
			{
				this.m_CurrentContext.valueIndex = value;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x060027E8 RID: 10216 RVA: 0x000A490B File Offset: 0x000A2B0B
		// (set) Token: 0x060027E9 RID: 10217 RVA: 0x000A4918 File Offset: 0x000A2B18
		public int matchedVariableCount
		{
			get
			{
				return this.m_CurrentContext.matchedVariableCount;
			}
			set
			{
				this.m_CurrentContext.matchedVariableCount = value;
			}
		}

		// Token: 0x060027EA RID: 10218 RVA: 0x000A4926 File Offset: 0x000A2B26
		protected void Initialize()
		{
			this.m_CurrentContext = default(BaseStyleMatcher.MatchContext);
			this.m_ContextStack.Clear();
		}

		// Token: 0x060027EB RID: 10219 RVA: 0x000A4944 File Offset: 0x000A2B44
		public void MoveNext()
		{
			bool flag = this.currentIndex + 1 <= this.valueCount;
			if (flag)
			{
				int currentIndex = this.currentIndex;
				this.currentIndex = currentIndex + 1;
			}
		}

		// Token: 0x060027EC RID: 10220 RVA: 0x000A497C File Offset: 0x000A2B7C
		public void SaveContext()
		{
			this.m_ContextStack.Push(this.m_CurrentContext);
		}

		// Token: 0x060027ED RID: 10221 RVA: 0x000A4991 File Offset: 0x000A2B91
		public void RestoreContext()
		{
			this.m_CurrentContext = this.m_ContextStack.Pop();
		}

		// Token: 0x060027EE RID: 10222 RVA: 0x000A49A5 File Offset: 0x000A2BA5
		public void DropContext()
		{
			this.m_ContextStack.Pop();
		}

		// Token: 0x060027EF RID: 10223 RVA: 0x000A49B4 File Offset: 0x000A2BB4
		protected bool Match(Expression exp)
		{
			bool flag = exp.multiplier.type == ExpressionMultiplierType.None;
			bool result;
			if (flag)
			{
				result = this.MatchExpression(exp);
			}
			else
			{
				Debug.Assert(exp.multiplier.type != ExpressionMultiplierType.GroupAtLeastOne, "'!' multiplier in syntax expression is not supported");
				result = this.MatchExpressionWithMultiplier(exp);
			}
			return result;
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x000A4A10 File Offset: 0x000A2C10
		private bool MatchExpression(Expression exp)
		{
			bool result = false;
			bool flag = exp.type == ExpressionType.Combinator;
			if (flag)
			{
				result = this.MatchCombinator(exp);
			}
			else
			{
				bool isCurrentVariable = this.isCurrentVariable;
				if (isCurrentVariable)
				{
					result = true;
					int matchedVariableCount = this.matchedVariableCount;
					this.matchedVariableCount = matchedVariableCount + 1;
				}
				else
				{
					bool flag2 = exp.type == ExpressionType.Data;
					if (flag2)
					{
						result = this.MatchDataType(exp);
					}
					else
					{
						bool flag3 = exp.type == ExpressionType.Keyword;
						if (flag3)
						{
							result = this.MatchKeyword(exp.keyword);
						}
					}
				}
				bool flag4 = result;
				if (flag4)
				{
					this.MoveNext();
				}
			}
			bool flag5 = !result && !this.hasCurrent && this.matchedVariableCount > 0;
			if (flag5)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x000A4ACC File Offset: 0x000A2CCC
		private bool MatchExpressionWithMultiplier(Expression exp)
		{
			bool isCommaSeparated = exp.multiplier.type == ExpressionMultiplierType.OneOrMoreComma;
			bool result = true;
			int min = exp.multiplier.min;
			int max = exp.multiplier.max;
			int matchCount = 0;
			int i = 0;
			while (result && this.hasCurrent && i < max)
			{
				result = this.MatchExpression(exp);
				bool flag = result;
				if (flag)
				{
					matchCount++;
					bool flag2 = isCommaSeparated;
					if (flag2)
					{
						bool flag3 = !this.isCurrentComma;
						if (flag3)
						{
							break;
						}
						this.MoveNext();
					}
				}
				i++;
			}
			result = matchCount >= min && matchCount <= max;
			bool flag4 = !result && matchCount <= max && this.matchedVariableCount > 0;
			if (flag4)
			{
				result = true;
			}
			return result;
		}

		// Token: 0x060027F2 RID: 10226 RVA: 0x000A4B98 File Offset: 0x000A2D98
		private bool MatchGroup(Expression exp)
		{
			Debug.Assert(exp.subExpressions.Length == 1, "Group has invalid number of sub expressions");
			Expression subExp = exp.subExpressions[0];
			return this.Match(subExp);
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x000A4BD0 File Offset: 0x000A2DD0
		private bool MatchCombinator(Expression exp)
		{
			this.SaveContext();
			bool result = false;
			switch (exp.combinator)
			{
			case ExpressionCombinator.Or:
				result = this.MatchOr(exp);
				break;
			case ExpressionCombinator.OrOr:
				result = this.MatchOrOr(exp);
				break;
			case ExpressionCombinator.AndAnd:
				result = this.MatchAndAnd(exp);
				break;
			case ExpressionCombinator.Juxtaposition:
				result = this.MatchJuxtaposition(exp);
				break;
			case ExpressionCombinator.Group:
				result = this.MatchGroup(exp);
				break;
			}
			bool flag = result;
			if (flag)
			{
				this.DropContext();
			}
			else
			{
				this.RestoreContext();
			}
			return result;
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x000A4C60 File Offset: 0x000A2E60
		private bool MatchOr(Expression exp)
		{
			BaseStyleMatcher.MatchContext resultContext = default(BaseStyleMatcher.MatchContext);
			int maxMatch = 0;
			for (int i = 0; i < exp.subExpressions.Length; i++)
			{
				this.SaveContext();
				int oldIndex = this.currentIndex;
				bool result = this.Match(exp.subExpressions[i]);
				int matchCount = this.currentIndex - oldIndex;
				bool flag = result && matchCount > maxMatch;
				if (flag)
				{
					maxMatch = matchCount;
					resultContext = this.m_CurrentContext;
				}
				this.RestoreContext();
			}
			bool flag2 = maxMatch > 0;
			bool flag3;
			if (flag2)
			{
				this.m_CurrentContext = resultContext;
				flag3 = true;
			}
			else
			{
				flag3 = false;
			}
			return flag3;
		}

		// Token: 0x060027F5 RID: 10229 RVA: 0x000A4D00 File Offset: 0x000A2F00
		private bool MatchOrOr(Expression exp)
		{
			int matchCount = this.MatchMany(exp);
			return matchCount > 0;
		}

		// Token: 0x060027F6 RID: 10230 RVA: 0x000A4D20 File Offset: 0x000A2F20
		private bool MatchAndAnd(Expression exp)
		{
			int matchCount = this.MatchMany(exp);
			int subExpCount = exp.subExpressions.Length;
			return matchCount == subExpCount;
		}

		// Token: 0x060027F7 RID: 10231 RVA: 0x000A4D48 File Offset: 0x000A2F48
		private unsafe int MatchMany(Expression exp)
		{
			BaseStyleMatcher.MatchContext resultContext = default(BaseStyleMatcher.MatchContext);
			int maxMatch = 0;
			int matchOrderStart = -1;
			int subExpCount = exp.subExpressions.Length;
			int* matchOrder;
			checked
			{
				matchOrder = stackalloc int[unchecked((UIntPtr)subExpCount) * 4];
			}
			do
			{
				this.SaveContext();
				matchOrderStart++;
				for (int i = 0; i < subExpCount; i++)
				{
					int matchIndex = ((matchOrderStart > 0) ? ((matchOrderStart + i) % subExpCount) : i);
					matchOrder[i] = matchIndex;
				}
				int matchCount = this.MatchManyByOrder(exp, matchOrder);
				bool flag = matchCount > maxMatch;
				if (flag)
				{
					maxMatch = matchCount;
					resultContext = this.m_CurrentContext;
				}
				this.RestoreContext();
			}
			while (maxMatch < subExpCount && matchOrderStart < subExpCount);
			bool flag2 = maxMatch > 0;
			if (flag2)
			{
				this.m_CurrentContext = resultContext;
			}
			return maxMatch;
		}

		// Token: 0x060027F8 RID: 10232 RVA: 0x000A4E08 File Offset: 0x000A3008
		private unsafe int MatchManyByOrder(Expression exp, int* matchOrder)
		{
			int subExpCount = exp.subExpressions.Length;
			int* matchedExp;
			int matchCount;
			int matchVariableCount;
			int i;
			checked
			{
				matchedExp = stackalloc int[unchecked((UIntPtr)subExpCount) * 4];
				matchCount = 0;
				matchVariableCount = 0;
				i = 0;
			}
			while (i < subExpCount && matchCount + matchVariableCount < subExpCount)
			{
				int expressionIndex = matchOrder[i];
				bool alreadyMatched = false;
				for (int j = 0; j < matchCount; j++)
				{
					bool flag = matchedExp[j] == expressionIndex;
					if (flag)
					{
						alreadyMatched = true;
						break;
					}
				}
				bool result = false;
				bool flag2 = !alreadyMatched;
				if (flag2)
				{
					result = this.Match(exp.subExpressions[expressionIndex]);
				}
				bool flag3 = result;
				if (flag3)
				{
					bool flag4 = matchVariableCount == this.matchedVariableCount;
					if (flag4)
					{
						matchedExp[matchCount] = expressionIndex;
						matchCount++;
					}
					else
					{
						matchVariableCount = this.matchedVariableCount;
					}
					i = 0;
				}
				else
				{
					i++;
				}
			}
			return matchCount + matchVariableCount;
		}

		// Token: 0x060027F9 RID: 10233 RVA: 0x000A4EF4 File Offset: 0x000A30F4
		private bool MatchJuxtaposition(Expression exp)
		{
			bool result = true;
			int i = 0;
			while (result && i < exp.subExpressions.Length)
			{
				result = this.Match(exp.subExpressions[i]);
				i++;
			}
			return result;
		}

		// Token: 0x060027FA RID: 10234 RVA: 0x000A4F38 File Offset: 0x000A3138
		private bool MatchDataType(Expression exp)
		{
			bool result = false;
			bool hasCurrent = this.hasCurrent;
			if (hasCurrent)
			{
				switch (exp.dataType)
				{
				case DataType.Number:
					result = this.MatchNumber();
					break;
				case DataType.Integer:
					result = this.MatchInteger();
					break;
				case DataType.Length:
					result = this.MatchLength();
					break;
				case DataType.Percentage:
					result = this.MatchPercentage();
					break;
				case DataType.Color:
					result = this.MatchColor();
					break;
				case DataType.Resource:
					result = this.MatchResource();
					break;
				case DataType.Url:
					result = this.MatchUrl();
					break;
				case DataType.Time:
					result = this.MatchTime();
					break;
				case DataType.Angle:
					result = this.MatchAngle();
					break;
				case DataType.CustomIdent:
					result = this.MatchCustomIdent();
					break;
				}
			}
			return result;
		}

		// Token: 0x0400151A RID: 5402
		protected static readonly Regex s_CustomIdentRegex = new Regex("^-?[_a-z][_a-z0-9-]*", RegexOptions.IgnoreCase | RegexOptions.Compiled);

		// Token: 0x0400151B RID: 5403
		private Stack<BaseStyleMatcher.MatchContext> m_ContextStack = new Stack<BaseStyleMatcher.MatchContext>();

		// Token: 0x0400151C RID: 5404
		private BaseStyleMatcher.MatchContext m_CurrentContext;

		// Token: 0x020005C1 RID: 1473
		private struct MatchContext
		{
			// Token: 0x0400151D RID: 5405
			public int valueIndex;

			// Token: 0x0400151E RID: 5406
			public int matchedVariableCount;
		}
	}
}
