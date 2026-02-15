using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x02000470 RID: 1136
	public struct UQueryBuilder<T> : IEquatable<UQueryBuilder<T>> where T : VisualElement
	{
		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06002169 RID: 8553 RVA: 0x0007A810 File Offset: 0x00078A10
		private List<StyleSelector> styleSelectors
		{
			get
			{
				List<StyleSelector> list;
				if ((list = this.m_StyleSelectors) == null)
				{
					list = (this.m_StyleSelectors = new List<StyleSelector>());
				}
				return list;
			}
		}

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x0007A83C File Offset: 0x00078A3C
		private List<StyleSelectorPart> parts
		{
			get
			{
				List<StyleSelectorPart> list;
				if ((list = this.m_Parts) == null)
				{
					list = (this.m_Parts = new List<StyleSelectorPart>());
				}
				return list;
			}
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x0007A868 File Offset: 0x00078A68
		public UQueryBuilder(VisualElement visualElement)
		{
			this = default(UQueryBuilder<T>);
			this.m_Element = visualElement;
			this.m_Parts = null;
			this.m_StyleSelectors = null;
			this.m_Relationship = StyleSelectorRelationship.None;
			this.m_Matchers = new List<RuleMatcher>();
			this.pseudoStatesMask = (this.negatedPseudoStatesMask = 0);
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0007A8B4 File Offset: 0x00078AB4
		public UQueryBuilder<T> Class(string classname)
		{
			this.AddClass(classname);
			return this;
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0007A8D4 File Offset: 0x00078AD4
		public UQueryBuilder<T> Name(string id)
		{
			this.AddName(id);
			return this;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0007A8F4 File Offset: 0x00078AF4
		public UQueryBuilder<T2> OfType<T2>(string name = null, string className = null) where T2 : VisualElement
		{
			this.AddType<T2>();
			this.AddName(name);
			this.AddClass(className);
			return this.AddRelationship<T2>(StyleSelectorRelationship.None);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0007A924 File Offset: 0x00078B24
		internal UQueryBuilder<T> SingleBaseType()
		{
			this.parts.Add(StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance));
			return this;
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0007A954 File Offset: 0x00078B54
		private void AddClass(string c)
		{
			bool flag = c != null;
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreateClass(c));
			}
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0007A97C File Offset: 0x00078B7C
		private void AddName(string id)
		{
			bool flag = id != null;
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreateId(id));
			}
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0007A9A4 File Offset: 0x00078BA4
		private void AddType<T2>() where T2 : VisualElement
		{
			bool flag = typeof(T2) != typeof(VisualElement);
			if (flag)
			{
				this.parts.Add(StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T2>.s_Instance));
			}
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0007A9E8 File Offset: 0x00078BE8
		private UQueryBuilder<T2> AddRelationship<T2>(StyleSelectorRelationship relationship) where T2 : VisualElement
		{
			return new UQueryBuilder<T2>(this.m_Element)
			{
				m_Matchers = this.m_Matchers,
				m_Parts = this.m_Parts,
				m_StyleSelectors = this.m_StyleSelectors,
				m_Relationship = ((relationship == StyleSelectorRelationship.None) ? this.m_Relationship : relationship),
				pseudoStatesMask = this.pseudoStatesMask,
				negatedPseudoStatesMask = this.negatedPseudoStatesMask
			};
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0007AA5C File Offset: 0x00078C5C
		private void AddPseudoStatesRuleIfNecessasy()
		{
			bool flag = this.pseudoStatesMask != 0 || this.negatedPseudoStatesMask != 0;
			if (flag)
			{
				this.parts.Add(new StyleSelectorPart
				{
					type = StyleSelectorType.PseudoClass
				});
			}
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0007AAA4 File Offset: 0x00078CA4
		private void FinishSelector()
		{
			this.FinishCurrentSelector();
			bool flag = this.styleSelectors.Count > 0;
			if (flag)
			{
				StyleComplexSelector selector = new StyleComplexSelector();
				selector.selectors = this.styleSelectors.ToArray();
				this.styleSelectors.Clear();
				this.m_Matchers.Add(new RuleMatcher
				{
					complexSelector = selector
				});
			}
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0007AB10 File Offset: 0x00078D10
		private bool CurrentSelectorEmpty()
		{
			return this.parts.Count == 0 && this.m_Relationship == StyleSelectorRelationship.None && this.pseudoStatesMask == 0 && this.negatedPseudoStatesMask == 0;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0007AB4C File Offset: 0x00078D4C
		private void FinishCurrentSelector()
		{
			bool flag = !this.CurrentSelectorEmpty();
			if (flag)
			{
				StyleSelector sel = new StyleSelector();
				sel.previousRelationship = this.m_Relationship;
				this.AddPseudoStatesRuleIfNecessasy();
				sel.parts = this.m_Parts.ToArray();
				sel.pseudoStateMask = this.pseudoStatesMask;
				sel.negatedPseudoStateMask = this.negatedPseudoStatesMask;
				this.styleSelectors.Add(sel);
				this.m_Parts.Clear();
				this.pseudoStatesMask = (this.negatedPseudoStatesMask = 0);
			}
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0007ABD8 File Offset: 0x00078DD8
		public UQueryState<T> Build()
		{
			this.FinishSelector();
			bool flag = this.m_Matchers.Count == 0;
			if (flag)
			{
				this.parts.Add(new StyleSelectorPart
				{
					type = StyleSelectorType.Wildcard
				});
				this.FinishSelector();
			}
			return new UQueryState<T>(this.m_Element, this.m_Matchers);
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0007AC3C File Offset: 0x00078E3C
		public List<T> ToList()
		{
			return this.Build().ToList();
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0007AC5C File Offset: 0x00078E5C
		public bool Equals(UQueryBuilder<T> other)
		{
			return EqualityComparer<List<StyleSelector>>.Default.Equals(this.m_StyleSelectors, other.m_StyleSelectors) && EqualityComparer<List<StyleSelector>>.Default.Equals(this.styleSelectors, other.styleSelectors) && EqualityComparer<List<StyleSelectorPart>>.Default.Equals(this.m_Parts, other.m_Parts) && EqualityComparer<List<StyleSelectorPart>>.Default.Equals(this.parts, other.parts) && this.m_Element == other.m_Element && EqualityComparer<List<RuleMatcher>>.Default.Equals(this.m_Matchers, other.m_Matchers) && this.m_Relationship == other.m_Relationship && this.pseudoStatesMask == other.pseudoStatesMask && this.negatedPseudoStatesMask == other.negatedPseudoStatesMask;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0007AD2C File Offset: 0x00078F2C
		public override bool Equals(object obj)
		{
			bool flag = !(obj is UQueryBuilder<T>);
			return !flag && this.Equals((UQueryBuilder<T>)obj);
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0007AD60 File Offset: 0x00078F60
		public override int GetHashCode()
		{
			int hashCode = -949812380;
			hashCode = hashCode * -1521134295 + EqualityComparer<List<StyleSelector>>.Default.GetHashCode(this.m_StyleSelectors);
			hashCode = hashCode * -1521134295 + EqualityComparer<List<StyleSelector>>.Default.GetHashCode(this.styleSelectors);
			hashCode = hashCode * -1521134295 + EqualityComparer<List<StyleSelectorPart>>.Default.GetHashCode(this.m_Parts);
			hashCode = hashCode * -1521134295 + EqualityComparer<List<StyleSelectorPart>>.Default.GetHashCode(this.parts);
			hashCode = hashCode * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.m_Element);
			hashCode = hashCode * -1521134295 + EqualityComparer<List<RuleMatcher>>.Default.GetHashCode(this.m_Matchers);
			hashCode = hashCode * -1521134295 + this.m_Relationship.GetHashCode();
			hashCode = hashCode * -1521134295 + this.pseudoStatesMask.GetHashCode();
			return hashCode * -1521134295 + this.negatedPseudoStatesMask.GetHashCode();
		}

		// Token: 0x04000EBF RID: 3775
		private List<StyleSelector> m_StyleSelectors;

		// Token: 0x04000EC0 RID: 3776
		private List<StyleSelectorPart> m_Parts;

		// Token: 0x04000EC1 RID: 3777
		private VisualElement m_Element;

		// Token: 0x04000EC2 RID: 3778
		private List<RuleMatcher> m_Matchers;

		// Token: 0x04000EC3 RID: 3779
		private StyleSelectorRelationship m_Relationship;

		// Token: 0x04000EC4 RID: 3780
		private int pseudoStatesMask;

		// Token: 0x04000EC5 RID: 3781
		private int negatedPseudoStatesMask;
	}
}
