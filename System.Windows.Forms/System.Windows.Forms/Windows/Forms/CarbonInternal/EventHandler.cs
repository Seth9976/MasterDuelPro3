using System;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.CarbonInternal
{
	// Token: 0x020003A2 RID: 930
	internal class EventHandler
	{
		// Token: 0x06001DEF RID: 7663 RVA: 0x000947FC File Offset: 0x000929FC
		internal static int EventCallback(IntPtr callref, IntPtr eventref, IntPtr handle)
		{
			uint eventClass = EventHandler.GetEventClass(eventref);
			uint eventKind = EventHandler.GetEventKind(eventref);
			MSG msg = default(MSG);
			IEventHandler eventHandler;
			if (eventClass > 1751740258U)
			{
				if (eventClass <= 1836021107U)
				{
					if (eventClass != 1801812322U)
					{
						if (eventClass != 1836021107U)
						{
							return 0;
						}
						eventHandler = EventHandler.Driver.MouseHandler;
						goto IL_00B6;
					}
				}
				else if (eventClass != 1952807028U)
				{
					if (eventClass != 2003398244U)
					{
						return 0;
					}
					eventHandler = EventHandler.Driver.WindowHandler;
					goto IL_00B6;
				}
				eventHandler = EventHandler.Driver.KeyboardHandler;
				goto IL_00B6;
			}
			if (eventClass == 1634758764U)
			{
				eventHandler = EventHandler.Driver.ApplicationHandler;
				goto IL_00B6;
			}
			if (eventClass == 1668183148U)
			{
				eventHandler = EventHandler.Driver.ControlHandler;
				goto IL_00B6;
			}
			if (eventClass == 1751740258U)
			{
				eventHandler = EventHandler.Driver.HIObjectHandler;
				goto IL_00B6;
			}
			return 0;
			IL_00B6:
			if (eventHandler.ProcessEvent(callref, eventref, handle, eventKind, ref msg))
			{
				EventHandler.Driver.EnqueueMessage(msg);
				return -9874;
			}
			return 0;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x000948E0 File Offset: 0x00092AE0
		internal static bool TranslateMessage(ref MSG msg)
		{
			bool flag = false;
			if (!flag)
			{
				flag = EventHandler.Driver.KeyboardHandler.TranslateMessage(ref msg);
			}
			if (!flag)
			{
				flag = EventHandler.Driver.MouseHandler.TranslateMessage(ref msg);
			}
			return flag;
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x00094918 File Offset: 0x00092B18
		internal static void InstallApplicationHandler()
		{
			EventHandler.InstallEventHandler(EventHandler.GetApplicationEventTarget(), EventHandler.EventHandlerDelegate, (uint)EventHandler.ApplicationEvents.Length, EventHandler.ApplicationEvents, IntPtr.Zero, IntPtr.Zero);
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00094940 File Offset: 0x00092B40
		internal static void InstallControlHandler(IntPtr control)
		{
			EventHandler.InstallEventHandler(EventHandler.GetControlEventTarget(control), EventHandler.EventHandlerDelegate, (uint)EventHandler.ControlEvents.Length, EventHandler.ControlEvents, control, IntPtr.Zero);
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00094965 File Offset: 0x00092B65
		internal static void InstallWindowHandler(IntPtr window)
		{
			EventHandler.InstallEventHandler(EventHandler.GetWindowEventTarget(window), EventHandler.EventHandlerDelegate, (uint)EventHandler.WindowEvents.Length, EventHandler.WindowEvents, window, IntPtr.Zero);
		}

		// Token: 0x06001DF4 RID: 7668
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern IntPtr GetApplicationEventTarget();

		// Token: 0x06001DF5 RID: 7669
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetControlEventTarget(IntPtr control);

		// Token: 0x06001DF6 RID: 7670
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern IntPtr GetWindowEventTarget(IntPtr window);

		// Token: 0x06001DF7 RID: 7671
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		internal static extern uint GetEventClass(IntPtr eventref);

		// Token: 0x06001DF8 RID: 7672
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern uint GetEventKind(IntPtr eventref);

		// Token: 0x06001DF9 RID: 7673
		[DllImport("/System/Library/Frameworks/Carbon.framework/Versions/Current/Carbon")]
		private static extern int InstallEventHandler(IntPtr window, EventDelegate event_handler, uint count, EventTypeSpec[] types, IntPtr user_data, IntPtr handlerref);

		// Token: 0x04001D32 RID: 7474
		internal static EventDelegate EventHandlerDelegate = new EventDelegate(EventHandler.EventCallback);

		// Token: 0x04001D33 RID: 7475
		internal static XplatUICarbon Driver;

		// Token: 0x04001D34 RID: 7476
		internal static EventTypeSpec[] HIObjectEvents = new EventTypeSpec[]
		{
			new EventTypeSpec(1751740258U, 1U),
			new EventTypeSpec(1751740258U, 2U),
			new EventTypeSpec(1751740258U, 3U)
		};

		// Token: 0x04001D35 RID: 7477
		internal static EventTypeSpec[] ControlEvents = new EventTypeSpec[]
		{
			new EventTypeSpec(1668183148U, 154U),
			new EventTypeSpec(1668183148U, 4U),
			new EventTypeSpec(1668183148U, 18U),
			new EventTypeSpec(1668183148U, 19U),
			new EventTypeSpec(1668183148U, 20U),
			new EventTypeSpec(1668183148U, 21U),
			new EventTypeSpec(1668183148U, 8U),
			new EventTypeSpec(1668183148U, 1000U),
			new EventTypeSpec(1668183148U, 157U)
		};

		// Token: 0x04001D36 RID: 7478
		internal static EventTypeSpec[] ApplicationEvents = new EventTypeSpec[]
		{
			new EventTypeSpec(1634758764U, 1U),
			new EventTypeSpec(1634758764U, 2U)
		};

		// Token: 0x04001D37 RID: 7479
		private static EventTypeSpec[] WindowEvents = new EventTypeSpec[]
		{
			new EventTypeSpec(1836021107U, 5U),
			new EventTypeSpec(1836021107U, 6U),
			new EventTypeSpec(1836021107U, 1U),
			new EventTypeSpec(1836021107U, 2U),
			new EventTypeSpec(1836021107U, 10U),
			new EventTypeSpec(1836021107U, 11U),
			new EventTypeSpec(2003398244U, 6U),
			new EventTypeSpec(2003398244U, 5U),
			new EventTypeSpec(2003398244U, 6U),
			new EventTypeSpec(2003398244U, 67U),
			new EventTypeSpec(2003398244U, 86U),
			new EventTypeSpec(2003398244U, 70U),
			new EventTypeSpec(2003398244U, 87U),
			new EventTypeSpec(2003398244U, 27U),
			new EventTypeSpec(2003398244U, 28U),
			new EventTypeSpec(2003398244U, 29U),
			new EventTypeSpec(2003398244U, 72U),
			new EventTypeSpec(2003398244U, 24U),
			new EventTypeSpec(1801812322U, 4U),
			new EventTypeSpec(1801812322U, 1U),
			new EventTypeSpec(1801812322U, 2U),
			new EventTypeSpec(1801812322U, 3U),
			new EventTypeSpec(1952807028U, 2U)
		};
	}
}
