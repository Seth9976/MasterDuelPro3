using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200046C RID: 1132
	public struct UQueryState<T> : IEnumerable<T>, IEnumerable, IEquatable<UQueryState<T>> where T : VisualElement
	{
		// Token: 0x0600214E RID: 8526 RVA: 0x0007A4D8 File Offset: 0x000786D8
		internal UQueryState(VisualElement element, List<RuleMatcher> matchers)
		{
			this.m_Element = element;
			this.m_Matchers = matchers;
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0007A4EC File Offset: 0x000786EC
		public UQueryState<T> RebuildOn(VisualElement element)
		{
			return new UQueryState<T>(element, this.m_Matchers);
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0007A50C File Offset: 0x0007870C
		private T Single(UQuery.SingleQueryMatcher matcher)
		{
			bool flag = matcher.IsInUse();
			if (flag)
			{
				matcher = matcher.CreateNew();
			}
			matcher.Run(this.m_Element, this.m_Matchers);
			T match = matcher.match as T;
			matcher.match = null;
			return match;
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x0007A55F File Offset: 0x0007875F
		public T First()
		{
			return this.Single(UQuery.FirstQueryMatcher.Instance);
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0007A56C File Offset: 0x0007876C
		public void ToList(List<T> results)
		{
			UQueryState<T>.s_List.matches = results;
			UQueryState<T>.s_List.Run(this.m_Element, this.m_Matchers);
			UQueryState<T>.s_List.Reset();
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0007A5A0 File Offset: 0x000787A0
		public List<T> ToList()
		{
			List<T> result = new List<T>();
			this.ToList(result);
			return result;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0007A5C1 File Offset: 0x000787C1
		public UQueryState<T>.Enumerator GetEnumerator()
		{
			return new UQueryState<T>.Enumerator(this);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0007A5CE File Offset: 0x000787CE
		IEnumerator<T> IEnumerable<T>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0007A5CE File Offset: 0x000787CE
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0007A5DC File Offset: 0x000787DC
		public bool Equals(UQueryState<T> other)
		{
			return this.m_Element == other.m_Element && EqualityComparer<List<RuleMatcher>>.Default.Equals(this.m_Matchers, other.m_Matchers);
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0007A618 File Offset: 0x00078818
		public override bool Equals(object obj)
		{
			bool flag = !(obj is UQueryState<T>);
			return !flag && this.Equals((UQueryState<T>)obj);
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0007A64C File Offset: 0x0007884C
		public override int GetHashCode()
		{
			int hashCode = 488160421;
			hashCode = hashCode * -1521134295 + EqualityComparer<VisualElement>.Default.GetHashCode(this.m_Element);
			return hashCode * -1521134295 + EqualityComparer<List<RuleMatcher>>.Default.GetHashCode(this.m_Matchers);
		}

		// Token: 0x04000EB6 RID: 3766
		private static UQueryState<T>.ActionQueryMatcher s_Action = new UQueryState<T>.ActionQueryMatcher();

		// Token: 0x04000EB7 RID: 3767
		private readonly VisualElement m_Element;

		// Token: 0x04000EB8 RID: 3768
		internal readonly List<RuleMatcher> m_Matchers;

		// Token: 0x04000EB9 RID: 3769
		private static readonly UQueryState<T>.ListQueryMatcher<T> s_List = new UQueryState<T>.ListQueryMatcher<T>();

		// Token: 0x04000EBA RID: 3770
		private static readonly UQueryState<T>.ListQueryMatcher<VisualElement> s_EnumerationList = new UQueryState<T>.ListQueryMatcher<VisualElement>();

		// Token: 0x0200046D RID: 1133
		private class ListQueryMatcher<TElement> : UQuery.UQueryMatcher where TElement : VisualElement
		{
			// Token: 0x170008EF RID: 2287
			// (get) Token: 0x0600215B RID: 8539 RVA: 0x0007A6B7 File Offset: 0x000788B7
			// (set) Token: 0x0600215C RID: 8540 RVA: 0x0007A6BF File Offset: 0x000788BF
			public List<TElement> matches { get; set; }

			// Token: 0x0600215D RID: 8541 RVA: 0x0007A6C8 File Offset: 0x000788C8
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				this.matches.Add(element as TElement);
				return false;
			}

			// Token: 0x0600215E RID: 8542 RVA: 0x0007A6F2 File Offset: 0x000788F2
			public void Reset()
			{
				this.matches = null;
			}
		}

		// Token: 0x0200046E RID: 1134
		private class ActionQueryMatcher : UQuery.UQueryMatcher
		{
			// Token: 0x170008F0 RID: 2288
			// (get) Token: 0x06002160 RID: 8544 RVA: 0x0007A6FD File Offset: 0x000788FD
			internal Action<T> callBack { get; }

			// Token: 0x06002161 RID: 8545 RVA: 0x0007A708 File Offset: 0x00078908
			protected override bool OnRuleMatchedElement(RuleMatcher matcher, VisualElement element)
			{
				T castedElement = element as T;
				bool flag = castedElement != null;
				if (flag)
				{
					this.callBack(castedElement);
				}
				return false;
			}
		}

		// Token: 0x0200046F RID: 1135
		public struct Enumerator : IEnumerator<T>, IEnumerator, IDisposable
		{
			// Token: 0x06002163 RID: 8547 RVA: 0x0007A744 File Offset: 0x00078944
			internal Enumerator(UQueryState<T> queryState)
			{
				this.iterationList = VisualElementListPool.Get(0);
				UQueryState<T>.s_EnumerationList.matches = this.iterationList;
				UQueryState<T>.s_EnumerationList.Run(queryState.m_Element, queryState.m_Matchers);
				UQueryState<T>.s_EnumerationList.Reset();
				this.currentIndex = -1;
			}

			// Token: 0x170008F1 RID: 2289
			// (get) Token: 0x06002164 RID: 8548 RVA: 0x0007A798 File Offset: 0x00078998
			public T Current
			{
				get
				{
					return (T)((object)this.iterationList[this.currentIndex]);
				}
			}

			// Token: 0x170008F2 RID: 2290
			// (get) Token: 0x06002165 RID: 8549 RVA: 0x0007A7B0 File Offset: 0x000789B0
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06002166 RID: 8550 RVA: 0x0007A7C0 File Offset: 0x000789C0
			public bool MoveNext()
			{
				int num = this.currentIndex + 1;
				this.currentIndex = num;
				return num < this.iterationList.Count;
			}

			// Token: 0x06002167 RID: 8551 RVA: 0x0007A7F0 File Offset: 0x000789F0
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x06002168 RID: 8552 RVA: 0x0007A7FA File Offset: 0x000789FA
			public void Dispose()
			{
				VisualElementListPool.Release(this.iterationList);
				this.iterationList = null;
			}

			// Token: 0x04000EBD RID: 3773
			private List<VisualElement> iterationList;

			// Token: 0x04000EBE RID: 3774
			private int currentIndex;
		}
	}
}
