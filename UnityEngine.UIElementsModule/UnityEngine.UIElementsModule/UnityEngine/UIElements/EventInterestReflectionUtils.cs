using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.UIElements
{
	// Token: 0x020004D3 RID: 1235
	internal static class EventInterestReflectionUtils
	{
		// Token: 0x060022E6 RID: 8934 RVA: 0x000802CC File Offset: 0x0007E4CC
		internal static void GetDefaultEventInterests(Type elementType, out int defaultActionCategories, out int defaultActionAtTargetCategories, out int handleEventTrickleDownCategories, out int handleEventBubbleUpCategories)
		{
			EventInterestReflectionUtils.DefaultEventInterests categories;
			bool flag = !EventInterestReflectionUtils.s_DefaultEventInterests.TryGetValue(elementType, out categories);
			if (flag)
			{
				Type ancestorType = elementType.BaseType;
				bool flag2 = ancestorType != null;
				if (flag2)
				{
					EventInterestReflectionUtils.GetDefaultEventInterests(ancestorType, out categories.DefaultActionCategories, out categories.DefaultActionAtTargetCategories, out categories.HandleEventTrickleDownCategories, out categories.HandleEventBubbleUpCategories);
				}
				categories.DefaultActionCategories |= EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "ExecuteDefaultAction") | EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "ExecuteDefaultActionDisabled");
				categories.DefaultActionAtTargetCategories |= EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "ExecuteDefaultActionAtTarget") | EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "ExecuteDefaultActionDisabledAtTarget");
				categories.HandleEventTrickleDownCategories |= EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "HandleEventTrickleDown") | EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "HandleEventTrickleDownDisabled");
				categories.HandleEventBubbleUpCategories |= EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "HandleEventBubbleUp") | EventInterestReflectionUtils.ComputeDefaultEventInterests(elementType, "HandleEventBubbleUpDisabled");
				EventInterestReflectionUtils.s_DefaultEventInterests.Add(elementType, categories);
			}
			defaultActionCategories = categories.DefaultActionCategories;
			defaultActionAtTargetCategories = categories.DefaultActionAtTargetCategories;
			handleEventTrickleDownCategories = categories.HandleEventTrickleDownCategories;
			handleEventBubbleUpCategories = categories.HandleEventBubbleUpCategories;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000803E0 File Offset: 0x0007E5E0
		private static int ComputeDefaultEventInterests(Type elementType, string methodName)
		{
			MethodInfo methodInfo = elementType.GetMethod(methodName, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			bool flag = methodInfo == null;
			int num;
			if (flag)
			{
				num = 0;
			}
			else
			{
				bool found = false;
				int categories = 0;
				object[] attributes = methodInfo.GetCustomAttributes(typeof(EventInterestAttribute), false);
				foreach (EventInterestAttribute attribute in attributes)
				{
					found = true;
					bool flag2 = attribute.eventTypes != null;
					if (flag2)
					{
						foreach (Type eventType in attribute.eventTypes)
						{
							categories |= 1 << (int)EventInterestReflectionUtils.GetEventCategory(eventType);
						}
					}
					categories |= (int)attribute.categoryFlags;
				}
				num = (found ? categories : (-1));
			}
			return num;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000804A8 File Offset: 0x0007E6A8
		internal static EventCategory GetEventCategory(Type eventType)
		{
			EventCategory category;
			bool flag = EventInterestReflectionUtils.s_EventCategories.TryGetValue(eventType, out category);
			EventCategory eventCategory;
			if (flag)
			{
				eventCategory = category;
			}
			else
			{
				object[] attributes = eventType.GetCustomAttributes(typeof(EventCategoryAttribute), true);
				object[] array = attributes;
				int num = 0;
				if (num >= array.Length)
				{
					throw new ArgumentOutOfRangeException("eventType", "Type must derive from EventBase<T>");
				}
				EventCategoryAttribute attribute = (EventCategoryAttribute)array[num];
				category = attribute.category;
				EventInterestReflectionUtils.s_EventCategories.Add(eventType, category);
				eventCategory = category;
			}
			return eventCategory;
		}

		// Token: 0x04000FAE RID: 4014
		private static readonly Dictionary<Type, EventInterestReflectionUtils.DefaultEventInterests> s_DefaultEventInterests = new Dictionary<Type, EventInterestReflectionUtils.DefaultEventInterests>();

		// Token: 0x04000FAF RID: 4015
		private static readonly Dictionary<Type, EventCategory> s_EventCategories = new Dictionary<Type, EventCategory>();

		// Token: 0x020004D4 RID: 1236
		private struct DefaultEventInterests
		{
			// Token: 0x04000FB0 RID: 4016
			public int DefaultActionCategories;

			// Token: 0x04000FB1 RID: 4017
			public int DefaultActionAtTargetCategories;

			// Token: 0x04000FB2 RID: 4018
			public int HandleEventTrickleDownCategories;

			// Token: 0x04000FB3 RID: 4019
			public int HandleEventBubbleUpCategories;
		}
	}
}
