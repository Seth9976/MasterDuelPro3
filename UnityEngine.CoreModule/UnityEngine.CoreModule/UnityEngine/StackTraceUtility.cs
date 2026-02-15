using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001B6 RID: 438
	public static class StackTraceUtility
	{
		// Token: 0x06001114 RID: 4372 RVA: 0x000244CC File Offset: 0x000226CC
		[RequiredByNativeCode]
		internal static void SetProjectFolder(string folder)
		{
			StackTraceUtility.projectFolder = folder;
			bool flag = !string.IsNullOrEmpty(StackTraceUtility.projectFolder);
			if (flag)
			{
				StackTraceUtility.projectFolder = StackTraceUtility.projectFolder.Replace("\\", "/");
			}
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x0002450C File Offset: 0x0002270C
		[RequiredByNativeCode]
		public unsafe static string ExtractStackTrace()
		{
			int bufMax = 16384;
			byte* buf = stackalloc byte[(UIntPtr)bufMax];
			int quickSize = Debug.ExtractStackTraceNoAlloc(buf, bufMax, StackTraceUtility.projectFolder);
			bool flag = quickSize > 0;
			string text;
			if (flag)
			{
				text = new string((sbyte*)buf, 0, quickSize, Encoding.UTF8);
			}
			else
			{
				StackTrace trace = new StackTrace(1, true);
				string traceString = StackTraceUtility.ExtractFormattedStackTrace(trace);
				text = traceString;
			}
			return text;
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x00024568 File Offset: 0x00022768
		[RequiredByNativeCode]
		internal static void ExtractStringFromExceptionInternal(object exceptiono, out string message, out string stackTrace)
		{
			bool flag = exceptiono == null;
			if (flag)
			{
				throw new ArgumentException("ExtractStringFromExceptionInternal called with null exception");
			}
			Exception exception = exceptiono as Exception;
			bool flag2 = exception == null;
			if (flag2)
			{
				throw new ArgumentException("ExtractStringFromExceptionInternal called with an exceptoin that was not of type System.Exception");
			}
			StringBuilder sb = new StringBuilder((exception.StackTrace == null) ? 512 : (exception.StackTrace.Length * 2));
			message = "";
			string traceString = "";
			while (exception != null)
			{
				bool flag3 = traceString.Length == 0;
				if (flag3)
				{
					traceString = exception.StackTrace;
				}
				else
				{
					traceString = exception.StackTrace + "\n" + traceString;
				}
				string thisMessage = exception.GetType().Name;
				string exceptionMessage = "";
				bool flag4 = exception.Message != null;
				if (flag4)
				{
					exceptionMessage = exception.Message;
				}
				bool flag5 = exceptionMessage.Trim().Length != 0;
				if (flag5)
				{
					thisMessage += ": ";
					thisMessage += exceptionMessage;
				}
				message = thisMessage;
				bool flag6 = exception.InnerException != null;
				if (flag6)
				{
					traceString = "Rethrow as " + thisMessage + "\n" + traceString;
				}
				exception = exception.InnerException;
			}
			sb.Append(traceString + "\n");
			StackTrace trace = new StackTrace(1, true);
			sb.Append(StackTraceUtility.ExtractFormattedStackTrace(trace));
			stackTrace = sb.ToString();
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000246D0 File Offset: 0x000228D0
		internal static string ExtractFormattedStackTrace(StackTrace stackTrace)
		{
			StringBuilder sb = new StringBuilder(255);
			for (int iIndex = 0; iIndex < stackTrace.FrameCount; iIndex++)
			{
				StackFrame frame = stackTrace.GetFrame(iIndex);
				MethodBase mb = frame.GetMethod();
				bool flag = mb == null;
				if (!flag)
				{
					Type classType = mb.DeclaringType;
					bool flag2 = classType == null;
					if (!flag2)
					{
						string ns = classType.Namespace;
						bool flag3 = !string.IsNullOrEmpty(ns);
						if (flag3)
						{
							sb.Append(ns);
							sb.Append(".");
						}
						sb.Append(classType.Name);
						sb.Append(":");
						sb.Append(mb.Name);
						sb.Append("(");
						int i = 0;
						ParameterInfo[] pi = mb.GetParameters();
						bool fFirstParam = true;
						while (i < pi.Length)
						{
							bool flag4 = !fFirstParam;
							if (flag4)
							{
								sb.Append(", ");
							}
							else
							{
								fFirstParam = false;
							}
							sb.Append(pi[i].ParameterType.Name);
							i++;
						}
						sb.Append(")");
						string path = frame.GetFileName();
						bool flag5 = path != null;
						if (flag5)
						{
							bool shouldStripLineNumbers = (classType.Name == "Debug" && classType.Namespace == "UnityEngine") || (classType.Name == "Logger" && classType.Namespace == "UnityEngine") || (classType.Name == "DebugLogHandler" && classType.Namespace == "UnityEngine") || (classType.Name == "Assert" && classType.Namespace == "UnityEngine.Assertions") || (mb.Name == "print" && classType.Name == "MonoBehaviour" && classType.Namespace == "UnityEngine");
							bool flag6 = !shouldStripLineNumbers;
							if (flag6)
							{
								sb.Append(" (at ");
								bool flag7 = !string.IsNullOrEmpty(StackTraceUtility.projectFolder);
								if (flag7)
								{
									bool flag8 = path.Replace("\\", "/").StartsWith(StackTraceUtility.projectFolder);
									if (flag8)
									{
										path = path.Substring(StackTraceUtility.projectFolder.Length, path.Length - StackTraceUtility.projectFolder.Length);
									}
								}
								sb.Append(path);
								sb.Append(":");
								sb.Append(frame.GetFileLineNumber().ToString());
								sb.Append(")");
							}
						}
						sb.Append("\n");
					}
				}
			}
			return sb.ToString();
		}

		// Token: 0x04000683 RID: 1667
		private static string projectFolder = "";
	}
}
