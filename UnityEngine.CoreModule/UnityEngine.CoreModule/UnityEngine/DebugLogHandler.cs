using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020000B8 RID: 184
	[NativeHeader("Runtime/Export/Debug/Debug.bindings.h")]
	internal sealed class DebugLogHandler : ILogHandler
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x00009100 File Offset: 0x00007300
		[ThreadAndSerializationSafe]
		internal unsafe static void Internal_Log(LogType level, LogOption options, string msg, Object obj)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(msg, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = msg.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				DebugLogHandler.Internal_Log_Injected(level, options, ref managedSpanWrapper, Object.MarshalledUnityObject.Marshal<Object>(obj));
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x0000915C File Offset: 0x0000735C
		[ThreadAndSerializationSafe]
		internal static void Internal_LogException(Exception ex, Object obj)
		{
			DebugLogHandler.Internal_LogException_Injected(ex, Object.MarshalledUnityObject.Marshal<Object>(obj));
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00009175 File Offset: 0x00007375
		public void LogFormat(LogType logType, Object context, string format, params object[] args)
		{
			DebugLogHandler.Internal_Log(logType, LogOption.None, string.Format(format, args), context);
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00009189 File Offset: 0x00007389
		public void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
		{
			DebugLogHandler.Internal_Log(logType, logOptions, string.Format(format, args), context);
		}

		// Token: 0x0600047B RID: 1147 RVA: 0x000091A0 File Offset: 0x000073A0
		public void LogException(Exception exception, Object context)
		{
			bool flag = exception == null;
			if (flag)
			{
				throw new ArgumentNullException("exception");
			}
			DebugLogHandler.Internal_LogException(exception, context);
		}

		// Token: 0x0600047D RID: 1149
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Log_Injected(LogType level, LogOption options, ref ManagedSpanWrapper msg, IntPtr obj);

		// Token: 0x0600047E RID: 1150
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_LogException_Injected(Exception ex, IntPtr obj);
	}
}
