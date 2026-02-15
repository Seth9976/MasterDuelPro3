using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025A RID: 602
	internal class UIDocumentList
	{
		// Token: 0x0600105B RID: 4187 RVA: 0x00045D8F File Offset: 0x00043F8F
		internal void RemoveFromListAndFromVisualTree(UIDocument uiDocument)
		{
			this.m_AttachedUIDocuments.Remove(uiDocument);
			VisualElement rootVisualElement = uiDocument.rootVisualElement;
			if (rootVisualElement != null)
			{
				rootVisualElement.RemoveFromHierarchy();
			}
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x00045DB4 File Offset: 0x00043FB4
		internal void AddToListAndToVisualTree(UIDocument uiDocument, VisualElement visualTree, int firstInsertIndex = 0)
		{
			int index = 0;
			foreach (UIDocument sibling in this.m_AttachedUIDocuments)
			{
				bool flag = uiDocument.sortingOrder > sibling.sortingOrder;
				if (flag)
				{
					index++;
				}
				else
				{
					bool flag2 = uiDocument.sortingOrder < sibling.sortingOrder;
					if (flag2)
					{
						break;
					}
					bool flag3 = uiDocument.m_UIDocumentCreationIndex > sibling.m_UIDocumentCreationIndex;
					if (!flag3)
					{
						break;
					}
					index++;
				}
			}
			bool flag4 = index < this.m_AttachedUIDocuments.Count;
			if (flag4)
			{
				this.m_AttachedUIDocuments.Insert(index, uiDocument);
				bool flag5 = visualTree == null || uiDocument.rootVisualElement == null;
				if (flag5)
				{
					return;
				}
				bool flag6 = index > 0;
				if (flag6)
				{
					VisualElement previousInTree = null;
					int i = 1;
					while (previousInTree == null && index - i >= 0)
					{
						UIDocument previousUIDocument = this.m_AttachedUIDocuments[index - i++];
						previousInTree = previousUIDocument.rootVisualElement;
					}
					bool flag7 = previousInTree != null;
					if (flag7)
					{
						index = visualTree.IndexOf(previousInTree) + 1;
					}
				}
				bool flag8 = index > visualTree.childCount;
				if (flag8)
				{
					index = visualTree.childCount;
				}
			}
			else
			{
				this.m_AttachedUIDocuments.Add(uiDocument);
			}
			bool flag9 = visualTree == null || uiDocument.rootVisualElement == null;
			if (!flag9)
			{
				int insertionIndex = firstInsertIndex + index;
				bool flag10 = insertionIndex < visualTree.childCount;
				if (flag10)
				{
					visualTree.Insert(insertionIndex, uiDocument.rootVisualElement);
				}
				else
				{
					visualTree.Add(uiDocument.rootVisualElement);
				}
			}
		}

		// Token: 0x04000933 RID: 2355
		internal List<UIDocument> m_AttachedUIDocuments = new List<UIDocument>();
	}
}
