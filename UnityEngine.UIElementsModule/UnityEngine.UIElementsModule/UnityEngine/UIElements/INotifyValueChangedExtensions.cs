using System;

namespace UnityEngine.UIElements
{
	// Token: 0x020000DE RID: 222
	public static class INotifyValueChangedExtensions
	{
		// Token: 0x060006BE RID: 1726 RVA: 0x0002057C File Offset: 0x0001E77C
		public static bool RegisterValueChangedCallback<T>(this INotifyValueChanged<T> control, EventCallback<ChangeEvent<T>> callback)
		{
			CallbackEventHandler handler = control as CallbackEventHandler;
			bool flag = handler != null;
			bool flag2;
			if (flag)
			{
				handler.RegisterCallback<ChangeEvent<T>>(callback, TrickleDown.NoTrickleDown);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x000205AC File Offset: 0x0001E7AC
		public static bool UnregisterValueChangedCallback<T>(this INotifyValueChanged<T> control, EventCallback<ChangeEvent<T>> callback)
		{
			CallbackEventHandler handler = control as CallbackEventHandler;
			bool flag = handler != null;
			bool flag2;
			if (flag)
			{
				handler.UnregisterCallback<ChangeEvent<T>>(callback, TrickleDown.NoTrickleDown);
				flag2 = true;
			}
			else
			{
				flag2 = false;
			}
			return flag2;
		}
	}
}
