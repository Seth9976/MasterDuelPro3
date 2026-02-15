using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000471 RID: 1137
	public static class UQueryExtensions
	{
		// Token: 0x0600217D RID: 8573 RVA: 0x0007AE54 File Offset: 0x00079054
		public static T Q<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			bool flag2 = typeof(T) == typeof(VisualElement);
			T t;
			if (flag2)
			{
				t = e.Q(name, className) as T;
			}
			else
			{
				bool flag3 = name == null;
				if (flag3)
				{
					bool flag4 = className == null;
					if (flag4)
					{
						UQueryState<VisualElement> query = UQueryExtensions.SingleElementTypeQuery.RebuildOn(e);
						query.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						t = query.First() as T;
					}
					else
					{
						UQueryState<VisualElement> query = UQueryExtensions.SingleElementTypeAndClassQuery.RebuildOn(e);
						query.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						query.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateClass(className);
						t = query.First() as T;
					}
				}
				else
				{
					bool flag5 = className == null;
					if (flag5)
					{
						UQueryState<VisualElement> query = UQueryExtensions.SingleElementTypeAndNameQuery.RebuildOn(e);
						query.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						query.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateId(name);
						t = query.First() as T;
					}
					else
					{
						UQueryState<VisualElement> query = UQueryExtensions.SingleElementTypeAndNameAndClassQuery.RebuildOn(e);
						query.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreatePredicate(UQuery.IsOfType<T>.s_Instance);
						query.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateId(name);
						query.m_Matchers[0].complexSelector.selectors[0].parts[2] = StyleSelectorPart.CreateClass(className);
						t = query.First() as T;
					}
				}
			}
			return t;
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0007B0B4 File Offset: 0x000792B4
		public static VisualElement Q(this VisualElement e, string name = null, string className = null)
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			bool flag2 = name == null;
			VisualElement visualElement;
			if (flag2)
			{
				bool flag3 = className == null;
				if (flag3)
				{
					visualElement = UQueryExtensions.SingleElementEmptyQuery.RebuildOn(e).First();
				}
				else
				{
					UQueryState<VisualElement> query = UQueryExtensions.SingleElementClassQuery.RebuildOn(e);
					query.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateClass(className);
					visualElement = query.First();
				}
			}
			else
			{
				bool flag4 = className == null;
				if (flag4)
				{
					UQueryState<VisualElement> query = UQueryExtensions.SingleElementNameQuery.RebuildOn(e);
					query.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateId(name);
					visualElement = query.First();
				}
				else
				{
					UQueryState<VisualElement> query = UQueryExtensions.SingleElementNameAndClassQuery.RebuildOn(e);
					query.m_Matchers[0].complexSelector.selectors[0].parts[0] = StyleSelectorPart.CreateId(name);
					query.m_Matchers[0].complexSelector.selectors[0].parts[1] = StyleSelectorPart.CreateClass(className);
					visualElement = query.First();
				}
			}
			return visualElement;
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0007B1FC File Offset: 0x000793FC
		public static UQueryBuilder<T> Query<T>(this VisualElement e, string name = null, string className = null) where T : VisualElement
		{
			bool flag = e == null;
			if (flag)
			{
				throw new ArgumentNullException("e");
			}
			return new UQueryBuilder<VisualElement>(e).OfType<T>(name, className);
		}

		// Token: 0x04000EC6 RID: 3782
		private static UQueryState<VisualElement> SingleElementEmptyQuery = new UQueryBuilder<VisualElement>(null).Build();

		// Token: 0x04000EC7 RID: 3783
		private static UQueryState<VisualElement> SingleElementNameQuery = new UQueryBuilder<VisualElement>(null).Name(string.Empty).Build();

		// Token: 0x04000EC8 RID: 3784
		private static UQueryState<VisualElement> SingleElementClassQuery = new UQueryBuilder<VisualElement>(null).Class(string.Empty).Build();

		// Token: 0x04000EC9 RID: 3785
		private static UQueryState<VisualElement> SingleElementNameAndClassQuery = new UQueryBuilder<VisualElement>(null).Name(string.Empty).Class(string.Empty).Build();

		// Token: 0x04000ECA RID: 3786
		private static UQueryState<VisualElement> SingleElementTypeQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Build();

		// Token: 0x04000ECB RID: 3787
		private static UQueryState<VisualElement> SingleElementTypeAndNameQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Name(string.Empty).Build();

		// Token: 0x04000ECC RID: 3788
		private static UQueryState<VisualElement> SingleElementTypeAndClassQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Class(string.Empty).Build();

		// Token: 0x04000ECD RID: 3789
		private static UQueryState<VisualElement> SingleElementTypeAndNameAndClassQuery = new UQueryBuilder<VisualElement>(null).SingleBaseType().Name(string.Empty).Class(string.Empty)
			.Build();
	}
}
