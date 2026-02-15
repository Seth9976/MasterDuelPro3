using System;
using System.Collections.Generic;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.InputForUI
{
	// Token: 0x0200001D RID: 29
	[VisibleToOtherModules(new string[] { "UnityEngine.UIElementsModule" })]
	internal static class EventProvider
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002F8C File Offset: 0x0000118C
		public static void Subscribe(EventConsumer handler, int priority = 0, int? playerId = null, params Event.Type[] type)
		{
			EventProvider.Bootstrap();
			EventProvider._registrations.Add(new EventProvider.Registration
			{
				handler = handler,
				priority = priority,
				playerId = playerId,
				_types = new HashSet<Event.Type>(type)
			});
			EventProvider._registrations.Sort((EventProvider.Registration a, EventProvider.Registration b) => a.priority.CompareTo(b.priority));
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003004 File Offset: 0x00001204
		public static void Unsubscribe(EventConsumer handler)
		{
			EventProvider._registrations.RemoveAll((EventProvider.Registration x) => x.handler == handler);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00003038 File Offset: 0x00001238
		public static void SetEnabled(bool enable)
		{
			EventProvider.m_IsEnabled = enable;
			if (enable)
			{
				EventProvider.Initialize();
			}
			else
			{
				EventProvider.Shutdown();
			}
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003060 File Offset: 0x00001260
		internal static void Dispatch(in Event ev)
		{
			bool flag = EventProvider._registrations.Count == 0;
			if (!flag)
			{
				EventProvider.s_sanitizer.Inspect(in ev);
				foreach (EventProvider.Registration registration in EventProvider._registrations)
				{
					bool flag2;
					if (registration._types.Count > 0)
					{
						HashSet<Event.Type> types = registration._types;
						Event @event = ev;
						flag2 = !types.Contains(@event.type);
					}
					else
					{
						flag2 = false;
					}
					bool flag3 = flag2;
					if (!flag3)
					{
						bool flag4 = registration.handler(in ev);
						if (flag4)
						{
							break;
						}
					}
				}
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000311C File Offset: 0x0000131C
		private static void Bootstrap()
		{
			bool isEnabled = EventProvider.m_IsEnabled;
			if (isEnabled)
			{
				EventProvider.Initialize();
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000313C File Offset: 0x0000133C
		private static void Initialize()
		{
			bool isInitialized = EventProvider.m_IsInitialized;
			if (!isInitialized)
			{
				EventProvider.s_sanitizer.Reset();
				IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
				if (eventProviderImpl != null)
				{
					eventProviderImpl.Initialize();
				}
				bool flag = !EventProvider.s_focusChangedRegistered;
				if (flag)
				{
					Application.focusChanged += EventProvider.OnFocusChanged;
					EventProvider.s_focusChangedRegistered = true;
				}
				EventProvider.m_IsInitialized = true;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000031A0 File Offset: 0x000013A0
		private static void Shutdown()
		{
			bool flag = !EventProvider.m_IsInitialized;
			if (!flag)
			{
				EventProvider.m_IsInitialized = false;
				bool flag2 = EventProvider.s_focusChangedRegistered;
				if (flag2)
				{
					EventProvider.s_focusChangedRegistered = false;
					Application.focusChanged -= EventProvider.OnFocusChanged;
				}
				IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
				if (eventProviderImpl != null)
				{
					eventProviderImpl.Shutdown();
				}
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000031F6 File Offset: 0x000013F6
		private static void OnFocusChanged(bool focus)
		{
			IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
			if (eventProviderImpl != null)
			{
				eventProviderImpl.OnFocusChanged(focus);
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000320C File Offset: 0x0000140C
		[RequiredByNativeCode]
		internal static void NotifyUpdate()
		{
			bool flag = !Application.isPlaying || EventProvider._registrations.Count == 0 || !EventProvider.m_IsInitialized;
			if (!flag)
			{
				EventProvider.s_sanitizer.BeforeProviderUpdate();
				IEventProviderImpl eventProviderImpl = EventProvider.s_impl;
				if (eventProviderImpl != null)
				{
					eventProviderImpl.Update();
				}
				EventProvider.s_sanitizer.AfterProviderUpdate();
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003268 File Offset: 0x00001468
		internal static void SetInputSystemProvider(IEventProviderImpl impl)
		{
			bool wasInitialized = EventProvider.m_IsInitialized;
			EventProvider.Shutdown();
			EventProvider.s_impl = impl;
			bool flag = wasInitialized;
			if (flag)
			{
				EventProvider.Initialize();
			}
		}

		// Token: 0x040000A2 RID: 162
		private static IEventProviderImpl s_impl = new InputManagerProvider();

		// Token: 0x040000A3 RID: 163
		private static EventSanitizer s_sanitizer;

		// Token: 0x040000A4 RID: 164
		private static IEventProviderImpl s_implMockBackup = null;

		// Token: 0x040000A5 RID: 165
		private static bool s_focusChangedRegistered;

		// Token: 0x040000A6 RID: 166
		private static bool m_IsEnabled = true;

		// Token: 0x040000A7 RID: 167
		private static bool m_IsInitialized = false;

		// Token: 0x040000A8 RID: 168
		private static List<EventProvider.Registration> _registrations = new List<EventProvider.Registration>();

		// Token: 0x0200001E RID: 30
		private struct Registration
		{
			// Token: 0x040000A9 RID: 169
			public EventConsumer handler;

			// Token: 0x040000AA RID: 170
			public int priority;

			// Token: 0x040000AB RID: 171
			public int? playerId;

			// Token: 0x040000AC RID: 172
			public HashSet<Event.Type> _types;
		}
	}
}
