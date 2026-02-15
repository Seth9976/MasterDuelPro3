using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x02000465 RID: 1125
	public static class UQuery
	{
		// Token: 0x02000466 RID: 1126
		internal interface IVisualPredicateWrapper
		{
			// Token: 0x06002137 RID: 8503
			bool Predicate(object e);
		}

		// Token: 0x02000467 RID: 1127
		internal class IsOfType<T> : UQuery.IVisualPredicateWrapper where T : VisualElement
		{
			// Token: 0x06002138 RID: 8504 RVA: 0x0007A308 File Offset: 0x00078508
			public bool Predicate(object e)
			{
				return e is T;
			}

			// Token: 0x04000EB0 RID: 3760
			public static UQuery.IsOfType<T> s_Instance = new UQuery.IsOfType<T>();
		}

		// Token: 0x02000468 RID: 1128
		internal abstract class UQueryMatcher : HierarchyTraversal
		{
			// Token: 0x0600213C RID: 8508 RVA: 0x0007A339 File Offset: 0x00078539
			public override void Traverse(VisualElement element)
			{
				base.Traverse(element);
			}

			// Token: 0x0600213D RID: 8509 RVA: 0x0007A344 File Offset: 0x00078544
			protected virtual bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				return false;
			}

			// Token: 0x0600213E RID: 8510 RVA: 0x000020EA File Offset: 0x000002EA
			private static void NoProcessResult(VisualElement e, MatchResultInfo i)
			{
			}

			// Token: 0x0600213F RID: 8511 RVA: 0x0007A358 File Offset: 0x00078558
			public override void TraverseRecursive(VisualElement element, int depth)
			{
				int originalCount = this.m_Matchers.Count;
				int count = this.m_Matchers.Count;
				for (int j = 0; j < count; j++)
				{
					RuleMatcher matcher = this.m_Matchers[j];
					bool flag = StyleSelectorHelper.MatchRightToLeft(element, matcher.complexSelector, delegate(VisualElement e, MatchResultInfo i)
					{
						UQuery.UQueryMatcher.NoProcessResult(e, i);
					});
					if (flag)
					{
						bool flag2 = this.OnRuleMatchedElement(matcher, element);
						if (flag2)
						{
							return;
						}
					}
				}
				base.Recurse(element, depth);
				bool flag3 = this.m_Matchers.Count > originalCount;
				if (flag3)
				{
					this.m_Matchers.RemoveRange(originalCount, this.m_Matchers.Count - originalCount);
					return;
				}
			}

			// Token: 0x06002140 RID: 8512 RVA: 0x0007A41C File Offset: 0x0007861C
			public virtual void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.m_Matchers = matchers;
				this.Traverse(root);
			}

			// Token: 0x04000EB1 RID: 3761
			internal List<RuleMatcher> m_Matchers;
		}

		// Token: 0x0200046A RID: 1130
		internal abstract class SingleQueryMatcher : UQuery.UQueryMatcher
		{
			// Token: 0x170008EE RID: 2286
			// (get) Token: 0x06002144 RID: 8516 RVA: 0x0007A444 File Offset: 0x00078644
			// (set) Token: 0x06002145 RID: 8517 RVA: 0x0007A44C File Offset: 0x0007864C
			public VisualElement match { get; set; }

			// Token: 0x06002146 RID: 8518 RVA: 0x0007A455 File Offset: 0x00078655
			public override void Run(VisualElement root, List<RuleMatcher> matchers)
			{
				this.match = null;
				base.Run(root, matchers);
				this.m_Matchers = null;
			}

			// Token: 0x06002147 RID: 8519 RVA: 0x0007A470 File Offset: 0x00078670
			public bool IsInUse()
			{
				return this.m_Matchers != null;
			}

			// Token: 0x06002148 RID: 8520
			public abstract UQuery.SingleQueryMatcher CreateNew();
		}

		// Token: 0x0200046B RID: 1131
		internal class FirstQueryMatcher : UQuery.SingleQueryMatcher
		{
			// Token: 0x0600214A RID: 8522 RVA: 0x0007A494 File Offset: 0x00078694
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				bool flag = base.match == null;
				if (flag)
				{
					base.match = element;
				}
				return true;
			}

			// Token: 0x0600214B RID: 8523 RVA: 0x0007A4BC File Offset: 0x000786BC
			public override UQuery.SingleQueryMatcher CreateNew()
			{
				return new UQuery.FirstQueryMatcher();
			}

			// Token: 0x04000EB5 RID: 3765
			public static readonly UQuery.FirstQueryMatcher Instance = new UQuery.FirstQueryMatcher();
		}
	}
}
