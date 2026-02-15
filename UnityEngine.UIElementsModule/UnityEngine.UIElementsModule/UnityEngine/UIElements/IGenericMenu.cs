using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000CB RID: 203
	internal interface IGenericMenu
	{
		// Token: 0x06000651 RID: 1617
		void AddItem(string itemName, bool isChecked, Action action);

		// Token: 0x06000652 RID: 1618
		void AddItem(string itemName, bool isChecked, Action<object> action, object data);

		// Token: 0x06000653 RID: 1619
		void DropDown(Rect position, VisualElement targetElement = null, bool anchored = false);
	}
}
