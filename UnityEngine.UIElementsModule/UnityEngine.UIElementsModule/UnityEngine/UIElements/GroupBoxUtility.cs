using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025D RID: 605
	internal static class GroupBoxUtility
	{
		// Token: 0x06001086 RID: 4230 RVA: 0x00046DF0 File Offset: 0x00044FF0
		public static void RegisterGroupBoxOption<T>(this T option) where T : VisualElement, IGroupBoxOption
		{
			VisualElement element = option;
			IGroupBox groupInHierarchy = null;
			for (VisualElement hierarchyParent = element.hierarchy.parent; hierarchyParent != null; hierarchyParent = hierarchyParent.hierarchy.parent)
			{
				IGroupBox group = hierarchyParent as IGroupBox;
				bool flag = group != null;
				if (flag)
				{
					groupInHierarchy = group;
					break;
				}
			}
			IGroupBox groupBox = groupInHierarchy ?? element.elementPanel;
			IGroupManager groupManager = GroupBoxUtility.FindOrCreateGroupManager(groupBox);
			groupManager.RegisterOption(option);
			GroupBoxUtility.s_GroupOptionManagerCache[option] = groupManager;
		}

		// Token: 0x06001087 RID: 4231 RVA: 0x00046E84 File Offset: 0x00045084
		public static void UnregisterGroupBoxOption<T>(this T option) where T : VisualElement, IGroupBoxOption
		{
			bool flag = !GroupBoxUtility.s_GroupOptionManagerCache.ContainsKey(option);
			if (!flag)
			{
				GroupBoxUtility.s_GroupOptionManagerCache[option].UnregisterOption(option);
				GroupBoxUtility.s_GroupOptionManagerCache.Remove(option);
			}
		}

		// Token: 0x06001088 RID: 4232 RVA: 0x00046ED8 File Offset: 0x000450D8
		public static void OnOptionSelected<T>(this T selectedOption) where T : VisualElement, IGroupBoxOption
		{
			bool flag = !GroupBoxUtility.s_GroupOptionManagerCache.ContainsKey(selectedOption);
			if (!flag)
			{
				GroupBoxUtility.s_GroupOptionManagerCache[selectedOption].OnOptionSelectionChanged(selectedOption);
			}
		}

		// Token: 0x06001089 RID: 4233 RVA: 0x00046F1C File Offset: 0x0004511C
		private static IGroupManager FindOrCreateGroupManager(IGroupBox groupBox)
		{
			bool flag = GroupBoxUtility.s_GroupManagers.ContainsKey(groupBox);
			IGroupManager groupManager2;
			if (flag)
			{
				groupManager2 = GroupBoxUtility.s_GroupManagers[groupBox];
			}
			else
			{
				Type genericType = null;
				foreach (Type interfaceType in groupBox.GetType().GetInterfaces())
				{
					bool flag2 = interfaceType.IsGenericType && GroupBoxUtility.k_GenericGroupBoxType.IsAssignableFrom(interfaceType.GetGenericTypeDefinition());
					if (flag2)
					{
						genericType = interfaceType.GetGenericArguments()[0];
						break;
					}
				}
				IGroupManager groupManager4;
				if (!(genericType != null))
				{
					IGroupManager groupManager3 = new DefaultGroupManager();
					groupManager4 = groupManager3;
				}
				else
				{
					groupManager4 = (IGroupManager)Activator.CreateInstance(genericType);
				}
				IGroupManager groupManager = groupManager4;
				groupManager.Init(groupBox);
				BaseVisualElementPanel panel = groupBox as BaseVisualElementPanel;
				bool flag3 = panel != null;
				if (flag3)
				{
					panel.panelDisposed += GroupBoxUtility.OnPanelDestroyed;
				}
				else
				{
					VisualElement visualElement = groupBox as VisualElement;
					bool flag4 = visualElement != null;
					if (flag4)
					{
						visualElement.RegisterCallback<DetachFromPanelEvent>(new EventCallback<DetachFromPanelEvent>(GroupBoxUtility.OnGroupBoxDetachedFromPanel), TrickleDown.NoTrickleDown);
					}
				}
				GroupBoxUtility.s_GroupManagers[groupBox] = groupManager;
				groupManager2 = groupManager;
			}
			return groupManager2;
		}

		// Token: 0x0600108A RID: 4234 RVA: 0x00047036 File Offset: 0x00045236
		private static void OnGroupBoxDetachedFromPanel(DetachFromPanelEvent evt)
		{
			GroupBoxUtility.s_GroupManagers.Remove(evt.currentTarget as IGroupBox);
		}

		// Token: 0x0600108B RID: 4235 RVA: 0x0004704F File Offset: 0x0004524F
		private static void OnPanelDestroyed(BaseVisualElementPanel panel)
		{
			GroupBoxUtility.s_GroupManagers.Remove(panel);
			panel.panelDisposed -= GroupBoxUtility.OnPanelDestroyed;
		}

		// Token: 0x0400094A RID: 2378
		private static Dictionary<IGroupBox, IGroupManager> s_GroupManagers = new Dictionary<IGroupBox, IGroupManager>();

		// Token: 0x0400094B RID: 2379
		private static Dictionary<IGroupBoxOption, IGroupManager> s_GroupOptionManagerCache = new Dictionary<IGroupBoxOption, IGroupManager>();

		// Token: 0x0400094C RID: 2380
		private static readonly Type k_GenericGroupBoxType = typeof(IGroupBox<>);
	}
}
