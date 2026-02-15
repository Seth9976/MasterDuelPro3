using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Represents a stack trace, which is an ordered collection of one or more stack frames.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006DE RID: 1758
	[ComVisible(true)]
	[MonoTODO("Serialized objects are not compatible with .NET")]
	[Serializable]
	public class StackTrace
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame.</summary>
		// Token: 0x06003798 RID: 14232 RVA: 0x000DAE97 File Offset: 0x000D9097
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace()
		{
			this.init_frames(0, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, optionally capturing source information.</summary>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		// Token: 0x06003799 RID: 14233 RVA: 0x000DAEA7 File Offset: 0x000D90A7
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace(bool fNeedFileInfo)
		{
			this.init_frames(0, fNeedFileInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, skipping the specified number of frames and optionally capturing source information.</summary>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative. </exception>
		// Token: 0x0600379A RID: 14234 RVA: 0x000DAEB7 File Offset: 0x000D90B7
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackTrace(int skipFrames, bool fNeedFileInfo)
		{
			this.init_frames(skipFrames, fNeedFileInfo);
		}

		// Token: 0x0600379B RID: 14235 RVA: 0x000DAEC8 File Offset: 0x000D90C8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void init_frames(int skipFrames, bool fNeedFileInfo)
		{
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			List<StackFrame> list = new List<StackFrame>();
			skipFrames += 2;
			StackFrame stackFrame;
			while ((stackFrame = new StackFrame(skipFrames, fNeedFileInfo)) != null && stackFrame.GetMethod() != null)
			{
				list.Add(stackFrame);
				skipFrames++;
			}
			this.debug_info = fNeedFileInfo;
			this.frames = list.ToArray();
		}

		// Token: 0x0600379C RID: 14236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern StackFrame[] get_trace(Exception e, int skipFrames, bool fNeedFileInfo);

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class, using the provided exception object and optionally capturing source information.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is null. </exception>
		// Token: 0x0600379D RID: 14237 RVA: 0x000DAF2E File Offset: 0x000D912E
		public StackTrace(Exception e, bool fNeedFileInfo)
			: this(e, 0, fNeedFileInfo)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object, skipping the specified number of frames and optionally capturing source information.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative. </exception>
		// Token: 0x0600379E RID: 14238 RVA: 0x000DAF3C File Offset: 0x000D913C
		public StackTrace(Exception e, int skipFrames, bool fNeedFileInfo)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			this.frames = StackTrace.get_trace(e, skipFrames, fNeedFileInfo);
			this.captured_traces = e.captured_traces;
		}

		// Token: 0x0600379F RID: 14239 RVA: 0x000DAF8B File Offset: 0x000D918B
		internal StackTrace(StackFrame[] frames)
		{
			this.frames = frames;
		}

		/// <summary>Gets the number of frames in the stack trace.</summary>
		/// <returns>The number of frames in the stack trace. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000DAF9A File Offset: 0x000D919A
		public virtual int FrameCount
		{
			get
			{
				if (this.frames != null)
				{
					return this.frames.Length;
				}
				return 0;
			}
		}

		/// <summary>Gets the specified stack frame.</summary>
		/// <returns>The specified stack frame.</returns>
		/// <param name="index">The index of the stack frame requested. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037A1 RID: 14241 RVA: 0x000DAFAE File Offset: 0x000D91AE
		public virtual StackFrame GetFrame(int index)
		{
			if (index < 0 || index >= this.FrameCount)
			{
				return null;
			}
			return this.frames[index];
		}

		/// <summary>Returns a copy of all stack frames in the current stack trace.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.StackFrame" /> representing the function calls in the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037A2 RID: 14242 RVA: 0x000DAFC8 File Offset: 0x000D91C8
		[ComVisible(false)]
		public virtual StackFrame[] GetFrames()
		{
			if (this.captured_traces == null)
			{
				return this.frames;
			}
			List<StackFrame> list = new List<StackFrame>();
			foreach (StackTrace stackTrace in this.captured_traces)
			{
				for (int j = 0; j < stackTrace.FrameCount; j++)
				{
					list.Add(stackTrace.GetFrame(j));
				}
			}
			list.AddRange(this.frames);
			return list.ToArray();
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000DB038 File Offset: 0x000D9238
		private static string GetAotId()
		{
			if (!StackTrace.isAotidSet)
			{
				byte[] aotId = RuntimeAssembly.GetAotId();
				if (aotId != null)
				{
					StackTrace.aotid = new Guid(aotId).ToString("N");
				}
				StackTrace.isAotidSet = true;
			}
			return StackTrace.aotid;
		}

		// Token: 0x060037A4 RID: 14244 RVA: 0x000DB078 File Offset: 0x000D9278
		private bool AddFrames(StringBuilder sb, bool separator, out bool isAsync)
		{
			isAsync = false;
			bool flag = false;
			int i = 0;
			while (i < this.FrameCount)
			{
				StackFrame frame = this.GetFrame(i);
				if (frame.GetMethod() == null)
				{
					if (flag || separator)
					{
						sb.Append(Environment.NewLine);
					}
					sb.Append("  at ");
					string internalMethodName = frame.GetInternalMethodName();
					if (internalMethodName != null)
					{
						sb.Append(internalMethodName);
						goto IL_0180;
					}
					sb.AppendFormat("<0x{0:x5} + 0x{1:x5}> <unknown method>", frame.GetMethodAddress(), frame.GetNativeOffset());
					goto IL_0180;
				}
				else
				{
					bool flag2;
					this.GetFullNameForStackTrace(sb, frame.GetMethod(), flag || separator, out flag2, out isAsync);
					if (!flag2)
					{
						if (frame.GetILOffset() == -1)
						{
							sb.AppendFormat(" <0x{0:x5} + 0x{1:x5}>", frame.GetMethodAddress(), frame.GetNativeOffset());
							if (frame.GetMethodIndex() != 16777215U)
							{
								sb.AppendFormat(" {0}", frame.GetMethodIndex());
							}
						}
						else
						{
							sb.AppendFormat(" [0x{0:x5}]", frame.GetILOffset());
						}
						string text = frame.GetSecureFileName();
						if (text[0] == '<')
						{
							string text2 = frame.GetMethod().Module.ModuleVersionId.ToString("N");
							string aotId = StackTrace.GetAotId();
							if (frame.GetILOffset() != -1 || aotId == null)
							{
								text = string.Format("<{0}>", text2);
							}
							else
							{
								text = string.Format("<{0}#{1}>", text2, aotId);
							}
						}
						sb.AppendFormat(" in {0}:{1} ", text, frame.GetFileLineNumber());
						goto IL_0180;
					}
				}
				IL_0182:
				i++;
				continue;
				IL_0180:
				flag = true;
				goto IL_0182;
			}
			return flag;
		}

		// Token: 0x060037A5 RID: 14245 RVA: 0x000DB218 File Offset: 0x000D9418
		private void GetFullNameForStackTrace(StringBuilder sb, MethodBase mi, bool needsNewLine, out bool skipped, out bool isAsync)
		{
			Type type = mi.DeclaringType;
			if (type.IsGenericType && !type.IsGenericTypeDefinition)
			{
				type = type.GetGenericTypeDefinition();
				foreach (MethodInfo methodInfo in type.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
				{
					if (methodInfo.MetadataToken == mi.MetadataToken)
					{
						mi = methodInfo;
						break;
					}
				}
			}
			isAsync = typeof(IAsyncStateMachine).IsAssignableFrom(type);
			skipped = mi.IsDefined(typeof(StackTraceHiddenAttribute)) || type.IsDefined(typeof(StackTraceHiddenAttribute));
			if (skipped)
			{
				return;
			}
			if (isAsync)
			{
				StackTrace.ConvertAsyncStateMachineMethod(ref mi, ref type);
			}
			if (needsNewLine)
			{
				sb.Append(Environment.NewLine);
			}
			sb.Append("  at ");
			sb.Append(type.ToString());
			sb.Append(".");
			sb.Append(mi.Name);
			if (mi.IsGenericMethod)
			{
				mi = ((MethodInfo)mi).GetGenericMethodDefinition();
				Type[] genericArguments = mi.GetGenericArguments();
				sb.Append("[");
				for (int j = 0; j < genericArguments.Length; j++)
				{
					if (j > 0)
					{
						sb.Append(",");
					}
					sb.Append(genericArguments[j].Name);
				}
				sb.Append("]");
			}
			ParameterInfo[] parameters = mi.GetParameters();
			sb.Append(" (");
			for (int k = 0; k < parameters.Length; k++)
			{
				if (k > 0)
				{
					sb.Append(", ");
				}
				Type type2 = parameters[k].ParameterType;
				if (type2.IsGenericType && !type2.IsGenericTypeDefinition)
				{
					type2 = type2.GetGenericTypeDefinition();
				}
				sb.Append(type2.ToString());
				if (parameters[k].Name != null)
				{
					sb.Append(" ");
					sb.Append(parameters[k].Name);
				}
			}
			sb.Append(")");
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000DB40C File Offset: 0x000D960C
		private static void ConvertAsyncStateMachineMethod(ref MethodBase method, ref Type declaringType)
		{
			Type declaringType2 = declaringType.DeclaringType;
			if (declaringType2 == null)
			{
				return;
			}
			MethodInfo[] methods = declaringType2.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (methods == null)
			{
				return;
			}
			foreach (MethodInfo methodInfo in methods)
			{
				IEnumerable<AsyncStateMachineAttribute> customAttributes = methodInfo.GetCustomAttributes<AsyncStateMachineAttribute>();
				if (customAttributes != null)
				{
					using (IEnumerator<AsyncStateMachineAttribute> enumerator = customAttributes.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (enumerator.Current.StateMachineType == declaringType)
							{
								method = methodInfo;
								declaringType = methodInfo.DeclaringType;
								return;
							}
						}
					}
				}
			}
		}

		/// <summary>Builds a readable representation of the stack trace.</summary>
		/// <returns>A readable representation of the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060037A7 RID: 14247 RVA: 0x000DB4B0 File Offset: 0x000D96B0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			if (this.captured_traces != null)
			{
				StackTrace[] array = this.captured_traces;
				for (int i = 0; i < array.Length; i++)
				{
					bool flag2;
					flag = array[i].AddFrames(stringBuilder, flag, out flag2);
					if (flag && !flag2)
					{
						stringBuilder.Append(Environment.NewLine);
						stringBuilder.Append("--- End of stack trace from previous location where exception was thrown ---");
						stringBuilder.Append(Environment.NewLine);
					}
				}
			}
			bool flag3;
			this.AddFrames(stringBuilder, flag, out flag3);
			return stringBuilder.ToString();
		}

		// Token: 0x060037A8 RID: 14248 RVA: 0x00040770 File Offset: 0x0003E970
		internal string ToString(StackTrace.TraceFormat traceFormat)
		{
			return this.ToString();
		}

		// Token: 0x04001DFB RID: 7675
		private StackFrame[] frames;

		// Token: 0x04001DFC RID: 7676
		private readonly StackTrace[] captured_traces;

		// Token: 0x04001DFD RID: 7677
		private bool debug_info;

		// Token: 0x04001DFE RID: 7678
		private static bool isAotidSet;

		// Token: 0x04001DFF RID: 7679
		private static string aotid;

		// Token: 0x020006DF RID: 1759
		internal enum TraceFormat
		{
			// Token: 0x04001E01 RID: 7681
			Normal,
			// Token: 0x04001E02 RID: 7682
			TrailingNewLine,
			// Token: 0x04001E03 RID: 7683
			NoResourceLookup
		}
	}
}
