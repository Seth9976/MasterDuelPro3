using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.Diagnostics
{
	/// <summary>Provides information about a <see cref="T:System.Diagnostics.StackFrame" />, which represents a function call on the call stack for the current thread.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006DD RID: 1757
	[ComVisible(true)]
	[MonoTODO("Serialized objects are not compatible with MS.NET")]
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public class StackFrame
	{
		// Token: 0x0600378A RID: 14218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_frame_info(int skip, bool needFileInfo, out MethodBase method, out int iloffset, out int native_offset, out string file, out int line, out int column);

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class.</summary>
		// Token: 0x0600378B RID: 14219 RVA: 0x000DAC54 File Offset: 0x000D8E54
		public StackFrame()
		{
			bool flag = StackFrame.get_frame_info(2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip. </param>
		// Token: 0x0600378C RID: 14220 RVA: 0x000DACA4 File Offset: 0x000D8EA4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(int skipFrames)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, false, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackFrame" /> class that corresponds to a frame above the current stack frame, optionally capturing source information.</summary>
		/// <param name="skipFrames">The number of frames up the stack to skip. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number of the stack frame; otherwise, false. </param>
		// Token: 0x0600378D RID: 14221 RVA: 0x000DACF4 File Offset: 0x000D8EF4
		[MethodImpl(MethodImplOptions.NoInlining)]
		public StackFrame(int skipFrames, bool fNeedFileInfo)
		{
			bool flag = StackFrame.get_frame_info(skipFrames + 2, fNeedFileInfo, out this.methodBase, out this.ilOffset, out this.nativeOffset, out this.fileName, out this.lineNumber, out this.columnNumber);
		}

		/// <summary>Gets the line number in the file that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file line number, or 0 (zero) if the file line number cannot be determined.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600378E RID: 14222 RVA: 0x000DAD43 File Offset: 0x000D8F43
		public virtual int GetFileLineNumber()
		{
			return this.lineNumber;
		}

		/// <summary>Gets the file name that contains the code that is executing. This information is typically extracted from the debugging symbols for the executable.</summary>
		/// <returns>The file name, or null if the file name cannot be determined.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600378F RID: 14223 RVA: 0x000DAD4B File Offset: 0x000D8F4B
		public virtual string GetFileName()
		{
			return this.fileName;
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000DAD54 File Offset: 0x000D8F54
		internal string GetSecureFileName()
		{
			string text = "<filename unknown>";
			if (this.fileName == null)
			{
				return text;
			}
			try
			{
				text = this.GetFileName();
			}
			catch (SecurityException)
			{
			}
			return text;
		}

		/// <summary>Gets the offset from the start of the Microsoft intermediate language (MSIL) code for the method that is executing. This offset might be an approximation depending on whether or not the just-in-time (JIT) compiler is generating debugging code. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" />.</summary>
		/// <returns>The offset from the start of the MSIL code for the method that is executing.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003791 RID: 14225 RVA: 0x000DAD90 File Offset: 0x000D8F90
		public virtual int GetILOffset()
		{
			return this.ilOffset;
		}

		/// <summary>Gets the method in which the frame is executing.</summary>
		/// <returns>The method in which the frame is executing.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003792 RID: 14226 RVA: 0x000DAD98 File Offset: 0x000D8F98
		public virtual MethodBase GetMethod()
		{
			return this.methodBase;
		}

		/// <summary>Gets the offset from the start of the native just-in-time (JIT)-compiled code for the method that is being executed. The generation of this debugging information is controlled by the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class.</summary>
		/// <returns>The offset from the start of the JIT-compiled code for the method that is being executed.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06003793 RID: 14227 RVA: 0x000DADA0 File Offset: 0x000D8FA0
		public virtual int GetNativeOffset()
		{
			return this.nativeOffset;
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000DADA8 File Offset: 0x000D8FA8
		internal long GetMethodAddress()
		{
			return this.methodAddress;
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000DADB0 File Offset: 0x000D8FB0
		internal uint GetMethodIndex()
		{
			return this.methodIndex;
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000DADB8 File Offset: 0x000D8FB8
		internal string GetInternalMethodName()
		{
			return this.internalMethodName;
		}

		/// <summary>Builds a readable representation of the stack trace.</summary>
		/// <returns>A readable representation of the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06003797 RID: 14231 RVA: 0x000DADC0 File Offset: 0x000D8FC0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this.methodBase == null)
			{
				stringBuilder.Append(Locale.GetText("<unknown method>"));
			}
			else
			{
				stringBuilder.Append(this.methodBase.Name);
			}
			stringBuilder.Append(Locale.GetText(" at "));
			if (this.ilOffset == -1)
			{
				stringBuilder.Append(Locale.GetText("<unknown offset>"));
			}
			else
			{
				stringBuilder.Append(Locale.GetText("offset "));
				stringBuilder.Append(this.ilOffset);
			}
			stringBuilder.Append(Locale.GetText(" in file:line:column "));
			stringBuilder.Append(this.GetSecureFileName());
			stringBuilder.AppendFormat(":{0}:{1}", this.lineNumber, this.columnNumber);
			return stringBuilder.ToString();
		}

		/// <summary>Defines the value that is returned from the <see cref="M:System.Diagnostics.StackFrame.GetNativeOffset" /> or <see cref="M:System.Diagnostics.StackFrame.GetILOffset" /> method when the native or Microsoft intermediate language (MSIL) offset is unknown. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04001DF1 RID: 7665
		public const int OFFSET_UNKNOWN = -1;

		// Token: 0x04001DF2 RID: 7666
		private int ilOffset = -1;

		// Token: 0x04001DF3 RID: 7667
		private int nativeOffset = -1;

		// Token: 0x04001DF4 RID: 7668
		private long methodAddress;

		// Token: 0x04001DF5 RID: 7669
		private uint methodIndex;

		// Token: 0x04001DF6 RID: 7670
		private MethodBase methodBase;

		// Token: 0x04001DF7 RID: 7671
		private string fileName;

		// Token: 0x04001DF8 RID: 7672
		private int lineNumber;

		// Token: 0x04001DF9 RID: 7673
		private int columnNumber;

		// Token: 0x04001DFA RID: 7674
		private string internalMethodName;
	}
}
