using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020004E5 RID: 1253
	internal static class VisualElementUtils
	{
		// Token: 0x0600231B RID: 8987 RVA: 0x000811E0 File Offset: 0x0007F3E0
		public static string GetUniqueName(string nameBase)
		{
			string name = nameBase;
			int counter = 2;
			while (VisualElementUtils.s_usedNames.Contains(name))
			{
				name = nameBase + counter.ToString();
				counter++;
			}
			VisualElementUtils.s_usedNames.Add(name);
			return name;
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x00081228 File Offset: 0x0007F428
		internal static int GetFoldoutDepth(this VisualElement element)
		{
			int depth = 0;
			bool flag = element.parent != null;
			if (flag)
			{
				for (VisualElement currentParent = element.parent; currentParent != null; currentParent = currentParent.parent)
				{
					bool flag2 = VisualElementUtils.s_FoldoutType.IsAssignableFrom(currentParent.GetType());
					if (flag2)
					{
						depth++;
					}
				}
			}
			return depth;
		}

		// Token: 0x0600231D RID: 8989 RVA: 0x00081284 File Offset: 0x0007F484
		internal static void AssignInspectorStyleIfNecessary(this VisualElement element, string classNameToEnable)
		{
			VisualElement inspector = element.GetFirstAncestorWhere((VisualElement i) => i.ClassListContains(VisualElementUtils.s_InspectorElementUssClassName));
			element.EnableInClassList(classNameToEnable, inspector != null);
		}

		// Token: 0x04000FE6 RID: 4070
		private static readonly HashSet<string> s_usedNames = new HashSet<string>();

		// Token: 0x04000FE7 RID: 4071
		private static readonly Type s_FoldoutType = typeof(Foldout);

		// Token: 0x04000FE8 RID: 4072
		private static readonly string s_InspectorElementUssClassName = "unity-inspector-element";
	}
}
