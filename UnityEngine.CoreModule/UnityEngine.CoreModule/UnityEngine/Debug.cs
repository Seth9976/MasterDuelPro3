using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020000B9 RID: 185
	[NativeHeader("Runtime/Export/Debug/Debug.bindings.h")]
	[NativeHeader("Runtime/Diagnostics/Validation.h")]
	[NativeHeader("Runtime/Diagnostics/IntegrityCheck.h")]
	public class Debug
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x000091C9 File Offset: 0x000073C9
		public static ILogger unityLogger
		{
			get
			{
				return Debug.s_Logger;
			}
		}

		// Token: 0x06000480 RID: 1152 RVA: 0x000091D0 File Offset: 0x000073D0
		[ExcludeFromDocs]
		public static void DrawLine(Vector3 start, Vector3 end, Color color)
		{
			bool depthTest = true;
			float duration = 0f;
			Debug.DrawLine(start, end, color, duration, depthTest);
		}

		// Token: 0x06000481 RID: 1153 RVA: 0x000091F4 File Offset: 0x000073F4
		[FreeFunction("DebugDrawLine", IsThreadSafe = true)]
		public static void DrawLine(Vector3 start, Vector3 end, [DefaultValue("Color.white")] Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest)
		{
			Debug.DrawLine_Injected(ref start, ref end, ref color, duration, depthTest);
		}

		// Token: 0x06000482 RID: 1154 RVA: 0x00009210 File Offset: 0x00007410
		[ThreadSafe]
		public unsafe static int ExtractStackTraceNoAlloc(byte* buffer, int bufferMax, string projectFolder)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(projectFolder, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = projectFolder.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = Debug.ExtractStackTraceNoAlloc_Injected(buffer, bufferMax, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x00009268 File Offset: 0x00007468
		public static void Log(object message)
		{
			Debug.unityLogger.Log(LogType.Log, message);
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x00009278 File Offset: 0x00007478
		public static void Log(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Log, message, context);
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x00009289 File Offset: 0x00007489
		public static void LogFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Log, format, args);
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000929C File Offset: 0x0000749C
		public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format, params object[] args)
		{
			DebugLogHandler i = Debug.unityLogger.logHandler as DebugLogHandler;
			bool flag = i == null;
			if (flag)
			{
				Debug.unityLogger.LogFormat(logType, context, format, args);
			}
			else
			{
				bool flag2 = Debug.unityLogger.IsLogTypeAllowed(logType);
				if (flag2)
				{
					i.LogFormat(logType, logOptions, context, format, args);
				}
			}
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x000092F0 File Offset: 0x000074F0
		public static void LogError(object message)
		{
			Debug.unityLogger.Log(LogType.Error, message);
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x00009300 File Offset: 0x00007500
		public static void LogError(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Error, message, context);
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x00009311 File Offset: 0x00007511
		public static void LogErrorFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, format, args);
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x00009322 File Offset: 0x00007522
		public static void LogErrorFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Error, context, format, args);
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x00009334 File Offset: 0x00007534
		public static void LogException(Exception exception)
		{
			Debug.unityLogger.LogException(exception, null);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00009344 File Offset: 0x00007544
		public static void LogException(Exception exception, Object context)
		{
			Debug.unityLogger.LogException(exception, context);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x00009354 File Offset: 0x00007554
		public static void LogWarning(object message)
		{
			Debug.unityLogger.Log(LogType.Warning, message);
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x00009364 File Offset: 0x00007564
		public static void LogWarning(object message, Object context)
		{
			Debug.unityLogger.Log(LogType.Warning, message, context);
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00009375 File Offset: 0x00007575
		public static void LogWarningFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, format, args);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00009386 File Offset: 0x00007586
		public static void LogWarningFormat(Object context, string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Warning, context, format, args);
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x00009398 File Offset: 0x00007598
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, "Assertion failed");
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000093C0 File Offset: 0x000075C0
		[Conditional("UNITY_ASSERTIONS")]
		public static void Assert(bool condition, string message)
		{
			bool flag = !condition;
			if (flag)
			{
				Debug.unityLogger.Log(LogType.Assert, message);
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x000093E3 File Offset: 0x000075E3
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertion(object message)
		{
			Debug.unityLogger.Log(LogType.Assert, message);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x000093F3 File Offset: 0x000075F3
		[Conditional("UNITY_ASSERTIONS")]
		public static void LogAssertionFormat(string format, params object[] args)
		{
			Debug.unityLogger.LogFormat(LogType.Assert, format, args);
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000495 RID: 1173
		public static extern bool isDebugBuild
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00009404 File Offset: 0x00007604
		[RequiredByNativeCode]
		internal static bool CallOverridenDebugHandler(Exception exception, Object obj)
		{
			bool flag = Debug.unityLogger.logHandler is DebugLogHandler;
			bool flag2;
			if (flag)
			{
				flag2 = false;
			}
			else
			{
				try
				{
					Debug.unityLogger.LogException(exception, obj);
				}
				catch (Exception ex)
				{
					Debug.s_DefaultLogger.LogError(string.Format("Invalid exception thrown from custom {0}.LogException(). Message: {1}", Debug.unityLogger.logHandler.GetType(), ex), obj);
					return false;
				}
				flag2 = true;
			}
			return flag2;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00009480 File Offset: 0x00007680
		[RequiredByNativeCode]
		internal static bool IsLoggingEnabled()
		{
			bool flag = Debug.unityLogger.logHandler is DebugLogHandler;
			bool flag2;
			if (flag)
			{
				flag2 = Debug.unityLogger.logEnabled;
			}
			else
			{
				flag2 = Debug.s_DefaultLogger.logEnabled;
			}
			return flag2;
		}

		// Token: 0x06000499 RID: 1177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void DrawLine_Injected([In] ref Vector3 start, [In] ref Vector3 end, [DefaultValue("Color.white")] [In] ref Color color, [DefaultValue("0.0f")] float duration, [DefaultValue("true")] bool depthTest);

		// Token: 0x0600049A RID: 1178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern int ExtractStackTraceNoAlloc_Injected(byte* buffer, int bufferMax, ref ManagedSpanWrapper projectFolder);

		// Token: 0x0400023F RID: 575
		internal static readonly ILogger s_DefaultLogger = new Logger(new DebugLogHandler());

		// Token: 0x04000240 RID: 576
		internal static ILogger s_Logger = new Logger(new DebugLogHandler());
	}
}
