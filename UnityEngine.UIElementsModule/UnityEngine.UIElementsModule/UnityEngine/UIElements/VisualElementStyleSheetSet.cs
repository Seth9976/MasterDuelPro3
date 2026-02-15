using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.UIElements
{
	// Token: 0x020004E4 RID: 1252
	[DefaultMember("Item")]
	public struct VisualElementStyleSheetSet : IEquatable<VisualElementStyleSheetSet>
	{
		// Token: 0x06002315 RID: 8981 RVA: 0x00081044 File Offset: 0x0007F244
		internal VisualElementStyleSheetSet(VisualElement element)
		{
			this.m_Element = element;
		}

		// Token: 0x06002316 RID: 8982 RVA: 0x00081050 File Offset: 0x0007F250
		public void Add(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList == null;
			if (flag2)
			{
				this.m_Element.styleSheetList = new List<StyleSheet>();
			}
			else
			{
				bool flag3 = this.m_Element.styleSheetList.Contains(styleSheet);
				if (flag3)
				{
					return;
				}
			}
			this.m_Element.styleSheetList.Add(styleSheet);
			this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x000810D4 File Offset: 0x0007F2D4
		public bool Remove(StyleSheet styleSheet)
		{
			bool flag = styleSheet == null;
			if (flag)
			{
				throw new ArgumentNullException("styleSheet");
			}
			bool flag2 = this.m_Element.styleSheetList != null && this.m_Element.styleSheetList.Remove(styleSheet);
			bool flag4;
			if (flag2)
			{
				bool flag3 = this.m_Element.styleSheetList.Count == 0;
				if (flag3)
				{
					this.m_Element.styleSheetList = null;
				}
				this.m_Element.IncrementVersion(VersionChangeType.StyleSheet);
				flag4 = true;
			}
			else
			{
				flag4 = false;
			}
			return flag4;
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x0008115C File Offset: 0x0007F35C
		public bool Equals(VisualElementStyleSheetSet other)
		{
			return object.Equals(this.m_Element, other.m_Element);
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00081180 File Offset: 0x0007F380
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is VisualElementStyleSheetSet && this.Equals((VisualElementStyleSheetSet)obj);
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x000811B8 File Offset: 0x0007F3B8
		public override int GetHashCode()
		{
			return (this.m_Element != null) ? this.m_Element.GetHashCode() : 0;
		}

		// Token: 0x04000FE5 RID: 4069
		private readonly VisualElement m_Element;
	}
}
