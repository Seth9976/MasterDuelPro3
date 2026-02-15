using System;
using System.Collections;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Net
{
	// Token: 0x0200037E RID: 894
	internal sealed class NetEventSource : EventSource
	{
		// Token: 0x06001634 RID: 5684 RVA: 0x0005E6FA File Offset: 0x0005C8FA
		[NonEvent]
		public static void Enter(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06001635 RID: 5685 RVA: 0x0005E724 File Offset: 0x0005C924
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0})", NetEventSource.Format(arg0)));
			}
		}

		// Token: 0x06001636 RID: 5686 RVA: 0x0005E74E File Offset: 0x0005C94E
		[NonEvent]
		public static void Enter(object thisOrContextObject, object arg0, object arg1, object arg2, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Enter(NetEventSource.IdOf(thisOrContextObject), memberName, string.Format("({0}, {1}, {2})", NetEventSource.Format(arg0), NetEventSource.Format(arg1), NetEventSource.Format(arg2)));
			}
		}

		// Token: 0x06001637 RID: 5687 RVA: 0x0005E785 File Offset: 0x0005C985
		[Event(1, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void Enter(string thisOrContextObject, string memberName, string parameters)
		{
			base.WriteEvent(1, thisOrContextObject, memberName ?? "(?)", parameters);
		}

		// Token: 0x06001638 RID: 5688 RVA: 0x0005E79A File Offset: 0x0005C99A
		[NonEvent]
		public static void Exit(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x06001639 RID: 5689 RVA: 0x0005E7C4 File Offset: 0x0005C9C4
		[NonEvent]
		public static void Exit(object thisOrContextObject, object arg0, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Exit(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(arg0).ToString());
			}
		}

		// Token: 0x0600163A RID: 5690 RVA: 0x0005E7E9 File Offset: 0x0005C9E9
		[Event(2, Level = EventLevel.Informational, Keywords = (EventKeywords)4L)]
		private void Exit(string thisOrContextObject, string memberName, string result)
		{
			base.WriteEvent(2, thisOrContextObject, memberName ?? "(?)", result);
		}

		// Token: 0x0600163B RID: 5691 RVA: 0x0005E7FE File Offset: 0x0005C9FE
		[NonEvent]
		public static void Info(object thisOrContextObject, FormattableString formattableString = null, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Info(NetEventSource.IdOf(thisOrContextObject), memberName, (formattableString != null) ? NetEventSource.Format(formattableString) : "");
			}
		}

		// Token: 0x0600163C RID: 5692 RVA: 0x0005E828 File Offset: 0x0005CA28
		[NonEvent]
		public static void Info(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Info(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x0600163D RID: 5693 RVA: 0x0005E84D File Offset: 0x0005CA4D
		[Event(4, Level = EventLevel.Informational, Keywords = (EventKeywords)1L)]
		private void Info(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(4, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x0600163E RID: 5694 RVA: 0x0005E862 File Offset: 0x0005CA62
		[NonEvent]
		public static void Error(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.ErrorMessage(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x0600163F RID: 5695 RVA: 0x0005E887 File Offset: 0x0005CA87
		[Event(5, Level = EventLevel.Warning, Keywords = (EventKeywords)1L)]
		private void ErrorMessage(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(5, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06001640 RID: 5696 RVA: 0x0005E89C File Offset: 0x0005CA9C
		[NonEvent]
		public static void Fail(object thisOrContextObject, object message, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.CriticalFailure(NetEventSource.IdOf(thisOrContextObject), memberName, NetEventSource.Format(message).ToString());
			}
		}

		// Token: 0x06001641 RID: 5697 RVA: 0x0005E8C1 File Offset: 0x0005CAC1
		[Event(6, Level = EventLevel.Critical, Keywords = (EventKeywords)2L)]
		private void CriticalFailure(string thisOrContextObject, string memberName, string message)
		{
			base.WriteEvent(6, thisOrContextObject, memberName ?? "(?)", message);
		}

		// Token: 0x06001642 RID: 5698 RVA: 0x0005E8D6 File Offset: 0x0005CAD6
		[NonEvent]
		public static void Associate(object first, object second, [CallerMemberName] string memberName = null)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Log.Associate(NetEventSource.IdOf(first), memberName, NetEventSource.IdOf(first), NetEventSource.IdOf(second));
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0005E8FC File Offset: 0x0005CAFC
		[Event(3, Level = EventLevel.Informational, Keywords = (EventKeywords)1L, Message = "[{2}]<-->[{3}]")]
		private void Associate(string thisOrContextObject, string memberName, string first, string second)
		{
			this.WriteEvent(3, thisOrContextObject, memberName ?? "(?)", first, second);
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x06001644 RID: 5700 RVA: 0x0005E913 File Offset: 0x0005CB13
		public new static bool IsEnabled
		{
			get
			{
				return NetEventSource.Log.IsEnabled();
			}
		}

		// Token: 0x06001645 RID: 5701 RVA: 0x0005E920 File Offset: 0x0005CB20
		[NonEvent]
		public static string IdOf(object value)
		{
			if (value == null)
			{
				return "(null)";
			}
			return value.GetType().Name + "#" + NetEventSource.GetHashCode(value).ToString();
		}

		// Token: 0x06001646 RID: 5702 RVA: 0x0005E959 File Offset: 0x0005CB59
		[NonEvent]
		public static int GetHashCode(object value)
		{
			if (value == null)
			{
				return 0;
			}
			return value.GetHashCode();
		}

		// Token: 0x06001647 RID: 5703 RVA: 0x0005E968 File Offset: 0x0005CB68
		[NonEvent]
		public static object Format(object value)
		{
			if (value == null)
			{
				return "(null)";
			}
			string text = null;
			if (text != null)
			{
				return text;
			}
			Array array = value as Array;
			if (array != null)
			{
				return string.Format("{0}[{1}]", array.GetType().GetElementType(), ((Array)value).Length);
			}
			ICollection collection = value as ICollection;
			if (collection != null)
			{
				return string.Format("{0}({1})", collection.GetType().Name, collection.Count);
			}
			SafeHandle safeHandle = value as SafeHandle;
			if (safeHandle != null)
			{
				return string.Format("{0}:{1}(0x{2:X})", safeHandle.GetType().Name, safeHandle.GetHashCode(), safeHandle.DangerousGetHandle());
			}
			if (value is IntPtr)
			{
				return string.Format("0x{0:X}", value);
			}
			string text2 = value.ToString();
			if (text2 == null || text2 == value.GetType().FullName)
			{
				return NetEventSource.IdOf(value);
			}
			return value;
		}

		// Token: 0x06001648 RID: 5704 RVA: 0x0005EA54 File Offset: 0x0005CC54
		[NonEvent]
		private static string Format(FormattableString s)
		{
			switch (s.ArgumentCount)
			{
			case 0:
				return s.Format;
			case 1:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)));
			case 2:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)), NetEventSource.Format(s.GetArgument(1)));
			case 3:
				return string.Format(s.Format, NetEventSource.Format(s.GetArgument(0)), NetEventSource.Format(s.GetArgument(1)), NetEventSource.Format(s.GetArgument(2)));
			default:
			{
				object[] arguments = s.GetArguments();
				object[] array = new object[arguments.Length];
				for (int i = 0; i < arguments.Length; i++)
				{
					array[i] = NetEventSource.Format(arguments[i]);
				}
				return string.Format(s.Format, array);
			}
			}
		}

		// Token: 0x06001649 RID: 5705 RVA: 0x0005EB28 File Offset: 0x0005CD28
		[NonEvent]
		private unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3, string arg4)
		{
			if (base.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				if (arg4 == null)
				{
					arg4 = "";
				}
				fixed (string text = arg1)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					fixed (string text2 = arg2)
					{
						char* ptr2 = text2;
						if (ptr2 != null)
						{
							ptr2 += RuntimeHelpers.OffsetToStringData / 2;
						}
						fixed (string text3 = arg3)
						{
							char* ptr3 = text3;
							if (ptr3 != null)
							{
								ptr3 += RuntimeHelpers.OffsetToStringData / 2;
							}
							fixed (string text4 = arg4)
							{
								char* ptr4 = text4;
								if (ptr4 != null)
								{
									ptr4 += RuntimeHelpers.OffsetToStringData / 2;
								}
								EventSource.EventData* ptr5;
								checked
								{
									ptr5 = stackalloc EventSource.EventData[unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData)];
								}
								*ptr5 = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr),
									Size = (arg1.Length + 1) * 2
								};
								ptr5[1] = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr2),
									Size = (arg2.Length + 1) * 2
								};
								ptr5[2] = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr3),
									Size = (arg3.Length + 1) * 2
								};
								ptr5[3] = new EventSource.EventData
								{
									DataPointer = (IntPtr)((void*)ptr4),
									Size = (arg4.Length + 1) * 2
								};
								base.WriteEventCore(eventId, 4, ptr5);
							}
						}
					}
				}
			}
		}

		// Token: 0x04000D4E RID: 3406
		public static readonly NetEventSource Log = new NetEventSource();

		// Token: 0x0200037F RID: 895
		public class Keywords
		{
			// Token: 0x04000D4F RID: 3407
			public const EventKeywords Default = (EventKeywords)1L;

			// Token: 0x04000D50 RID: 3408
			public const EventKeywords Debug = (EventKeywords)2L;

			// Token: 0x04000D51 RID: 3409
			public const EventKeywords EnterExit = (EventKeywords)4L;
		}
	}
}
