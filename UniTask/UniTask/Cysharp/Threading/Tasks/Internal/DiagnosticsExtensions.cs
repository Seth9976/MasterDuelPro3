using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cysharp.Threading.Tasks.Internal
{
	// Token: 0x0200022F RID: 559
	internal static class DiagnosticsExtensions
	{
		// Token: 0x06000CB2 RID: 3250 RVA: 0x0002C2F4 File Offset: 0x0002A4F4
		public static string CleanupAsyncStackTrace(this StackTrace stackTrace)
		{
			if (stackTrace == null)
			{
				return "";
			}
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < stackTrace.FrameCount; i++)
			{
				StackFrame sf = stackTrace.GetFrame(i);
				MethodBase mb = sf.GetMethod();
				if (!DiagnosticsExtensions.IgnoreLine(mb))
				{
					if (DiagnosticsExtensions.IsAsync(mb))
					{
						sb.Append("async ");
						Type decType;
						DiagnosticsExtensions.TryResolveStateMachineMethod(ref mb, out decType);
					}
					MethodInfo mi = mb as MethodInfo;
					if (mi != null)
					{
						sb.Append(DiagnosticsExtensions.BeautifyType(mi.ReturnType, false));
						sb.Append(" ");
					}
					sb.Append(DiagnosticsExtensions.BeautifyType(mb.DeclaringType, false));
					if (!mb.IsConstructor)
					{
						sb.Append(".");
					}
					sb.Append(mb.Name);
					if (mb.IsGenericMethod)
					{
						sb.Append("<");
						foreach (Type item in mb.GetGenericArguments())
						{
							sb.Append(DiagnosticsExtensions.BeautifyType(item, true));
						}
						sb.Append(">");
					}
					sb.Append("(");
					sb.Append(string.Join(", ", from p in mb.GetParameters()
						select DiagnosticsExtensions.BeautifyType(p.ParameterType, true) + " " + p.Name));
					sb.Append(")");
					if (DiagnosticsExtensions.displayFilenames && sf.GetILOffset() != -1)
					{
						string fileName = null;
						try
						{
							fileName = sf.GetFileName();
						}
						catch (NotSupportedException)
						{
							DiagnosticsExtensions.displayFilenames = false;
						}
						catch (SecurityException)
						{
							DiagnosticsExtensions.displayFilenames = false;
						}
						if (fileName != null)
						{
							sb.Append(' ');
							sb.AppendFormat(CultureInfo.InvariantCulture, "(at {0})", DiagnosticsExtensions.AppendHyperLink(fileName, sf.GetFileLineNumber().ToString()));
						}
					}
					sb.AppendLine();
				}
			}
			return sb.ToString();
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0002C4F0 File Offset: 0x0002A6F0
		private static bool IsAsync(MethodBase methodInfo)
		{
			Type declareType = methodInfo.DeclaringType;
			return typeof(IAsyncStateMachine).IsAssignableFrom(declareType);
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002C514 File Offset: 0x0002A714
		private static bool TryResolveStateMachineMethod(ref MethodBase method, out Type declaringType)
		{
			declaringType = method.DeclaringType;
			Type parentType = declaringType.DeclaringType;
			if (parentType == null)
			{
				return false;
			}
			MethodInfo[] methods = parentType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (methods == null)
			{
				return false;
			}
			foreach (MethodInfo candidateMethod in methods)
			{
				IEnumerable<StateMachineAttribute> attributes = candidateMethod.GetCustomAttributes(false);
				if (attributes != null)
				{
					foreach (StateMachineAttribute asma in attributes)
					{
						if (asma.StateMachineType == declaringType)
						{
							method = candidateMethod;
							declaringType = candidateMethod.DeclaringType;
							return asma is IteratorStateMachineAttribute;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0002C5D8 File Offset: 0x0002A7D8
		private static string BeautifyType(Type t, bool shortName)
		{
			string builtin;
			if (DiagnosticsExtensions.builtInTypeNames.TryGetValue(t, out builtin))
			{
				return builtin;
			}
			if (t.IsGenericParameter)
			{
				return t.Name;
			}
			if (t.IsArray)
			{
				return DiagnosticsExtensions.BeautifyType(t.GetElementType(), shortName) + "[]";
			}
			string fullName = t.FullName;
			if (fullName != null && fullName.StartsWith("System.ValueTuple"))
			{
				return "(" + string.Join(", ", from x in t.GetGenericArguments()
					select DiagnosticsExtensions.BeautifyType(x, true)) + ")";
			}
			if (!t.IsGenericType)
			{
				string text;
				if (!shortName)
				{
					if ((text = t.FullName.Replace("Cysharp.Threading.Tasks.Triggers.", "").Replace("Cysharp.Threading.Tasks.Internal.", "").Replace("Cysharp.Threading.Tasks.", "")) == null)
					{
						return t.Name;
					}
				}
				else
				{
					text = t.Name;
				}
				return text;
			}
			string innerFormat = string.Join(", ", from x in t.GetGenericArguments()
				select DiagnosticsExtensions.BeautifyType(x, true));
			string genericType = t.GetGenericTypeDefinition().FullName;
			if (genericType == "System.Threading.Tasks.Task`1")
			{
				genericType = "Task";
			}
			return DiagnosticsExtensions.typeBeautifyRegex.Replace(genericType, "").Replace("Cysharp.Threading.Tasks.Triggers.", "").Replace("Cysharp.Threading.Tasks.Internal.", "")
				.Replace("Cysharp.Threading.Tasks.", "") + "<" + innerFormat + ">";
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0002C774 File Offset: 0x0002A974
		private static bool IgnoreLine(MethodBase methodInfo)
		{
			string declareType = methodInfo.DeclaringType.FullName;
			return declareType == "System.Threading.ExecutionContext" || declareType.StartsWith("System.Runtime.CompilerServices") || declareType.StartsWith("Cysharp.Threading.Tasks.CompilerServices") || declareType == "System.Threading.Tasks.AwaitTaskContinuation" || declareType.StartsWith("System.Threading.Tasks.Task") || declareType.StartsWith("Cysharp.Threading.Tasks.UniTaskCompletionSourceCore") || declareType.StartsWith("Cysharp.Threading.Tasks.AwaiterActions");
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0002C7F8 File Offset: 0x0002A9F8
		private static string AppendHyperLink(string path, string line)
		{
			FileInfo fi = new FileInfo(path);
			if (fi.Directory == null)
			{
				return fi.Name;
			}
			string fname = fi.FullName.Replace(Path.DirectorySeparatorChar, '/').Replace(PlayerLoopHelper.ApplicationDataPath, "");
			string withAssetsPath = "Assets/" + fname;
			return string.Concat(new string[] { "<a href=\"", withAssetsPath, "\" line=\"", line, "\">", withAssetsPath, ":", line, "</a>" });
		}

		// Token: 0x04000666 RID: 1638
		private static bool displayFilenames = true;

		// Token: 0x04000667 RID: 1639
		private static readonly Regex typeBeautifyRegex = new Regex("`.+$", RegexOptions.Compiled);

		// Token: 0x04000668 RID: 1640
		private static readonly Dictionary<Type, string> builtInTypeNames = new Dictionary<Type, string>
		{
			{
				typeof(void),
				"void"
			},
			{
				typeof(bool),
				"bool"
			},
			{
				typeof(byte),
				"byte"
			},
			{
				typeof(char),
				"char"
			},
			{
				typeof(decimal),
				"decimal"
			},
			{
				typeof(double),
				"double"
			},
			{
				typeof(float),
				"float"
			},
			{
				typeof(int),
				"int"
			},
			{
				typeof(long),
				"long"
			},
			{
				typeof(object),
				"object"
			},
			{
				typeof(sbyte),
				"sbyte"
			},
			{
				typeof(short),
				"short"
			},
			{
				typeof(string),
				"string"
			},
			{
				typeof(uint),
				"uint"
			},
			{
				typeof(ulong),
				"ulong"
			},
			{
				typeof(ushort),
				"ushort"
			},
			{
				typeof(Task),
				"Task"
			},
			{
				typeof(UniTask),
				"UniTask"
			},
			{
				typeof(UniTaskVoid),
				"UniTaskVoid"
			}
		};
	}
}
