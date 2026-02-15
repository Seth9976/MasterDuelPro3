using System;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x020005A6 RID: 1446
	internal abstract class HierarchyTraversal
	{
		// Token: 0x06002709 RID: 9993 RVA: 0x0009B857 File Offset: 0x00099A57
		public virtual void Traverse(VisualElement element)
		{
			this.TraverseRecursive(element, 0);
		}

		// Token: 0x0600270A RID: 9994
		public abstract void TraverseRecursive(VisualElement element, int depth);

		// Token: 0x0600270B RID: 9995 RVA: 0x0009B864 File Offset: 0x00099A64
		protected void Recurse(VisualElement element, int depth)
		{
			int i = 0;
			while (i < element.hierarchy.childCount)
			{
				VisualElement child = element.hierarchy[i];
				this.TraverseRecursive(child, depth + 1);
				bool flag = child.hierarchy.parent != element;
				if (!flag)
				{
					i++;
				}
			}
		}
	}
}
