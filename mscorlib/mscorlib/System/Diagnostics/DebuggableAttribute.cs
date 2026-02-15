using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Modifies code generation for runtime just-in-time (JIT) debugging. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006D6 RID: 1750
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module, AllowMultiple = false)]
	public sealed class DebuggableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.DebuggableAttribute" /> class, using the specified debugging modes for the just-in-time (JIT) compiler. </summary>
		/// <param name="modes">A bitwise combination of the <see cref="T:System.Diagnostics.DebuggableAttribute.DebuggingModes" />  values specifying the debugging mode for the JIT compiler.</param>
		// Token: 0x0600377F RID: 14207 RVA: 0x000DABA6 File Offset: 0x000D8DA6
		public DebuggableAttribute(DebuggableAttribute.DebuggingModes modes)
		{
			this.m_debuggingModes = modes;
		}

		// Token: 0x04001DE0 RID: 7648
		private DebuggableAttribute.DebuggingModes m_debuggingModes;

		/// <summary>Specifies the debugging mode for the just-in-time (JIT) compiler.</summary>
		// Token: 0x020006D7 RID: 1751
		[Flags]
		[ComVisible(true)]
		public enum DebuggingModes
		{
			/// <summary>In the .NET Framework version 2.0, JIT tracking information is always generated, and this flag has the same effect as <see cref="F:System.Diagnostics.DebuggableAttribute.DebuggingModes.Default" /> with the exception of the <see cref="P:System.Diagnostics.DebuggableAttribute.IsJITTrackingEnabled" /> property being false, which has no meaning in version 2.0.</summary>
			// Token: 0x04001DE2 RID: 7650
			None = 0,
			/// <summary>Instructs the just-in-time (JIT) compiler to use its default behavior, which includes enabling optimizations, disabling Edit and Continue support, and using symbol store sequence points if present. In the .NET Framework version 2.0, JIT tracking information, the Microsoft intermediate language (MSIL) offset to the native-code offset within a method, is always generated.</summary>
			// Token: 0x04001DE3 RID: 7651
			Default = 1,
			/// <summary>Disable optimizations performed by the compiler to make your output file smaller, faster, and more efficient. Optimizations result in code rearrangement in the output file, which can make debugging difficult. Typically optimization should be disabled while debugging. </summary>
			// Token: 0x04001DE4 RID: 7652
			DisableOptimizations = 256,
			/// <summary>Use the implicit MSIL sequence points, not the program database (PDB) sequence points. The symbolic information normally includes at least one Microsoft intermediate language (MSIL) offset for each source line. When the just-in-time (JIT) compiler is about to compile a method, it asks the profiling services for a list of MSIL offsets that should be preserved. These MSIL offsets are called sequence points.</summary>
			// Token: 0x04001DE5 RID: 7653
			IgnoreSymbolStoreSequencePoints = 2,
			/// <summary>Enable edit and continue. Edit and continue enables you to make changes to your source code while your program is in break mode. The ability to edit and continue is compiler dependent. </summary>
			// Token: 0x04001DE6 RID: 7654
			EnableEditAndContinue = 4
		}
	}
}
