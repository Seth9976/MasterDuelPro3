using System;
using System.CodeDom.Compiler;

namespace Microsoft.VisualBasic
{
	/// <summary>Provides access to instances of the Visual Basic code generator and code compiler.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000E3 RID: 227
	public class VBCodeProvider : CodeDomProvider
	{
		/// <summary>Gets an instance of the Visual Basic code generator.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> implementation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000433 RID: 1075 RVA: 0x0000F2A6 File Offset: 0x0000D4A6
		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeGenerator CreateGenerator()
		{
			return this._generator;
		}

		/// <summary>Gets an instance of the Visual Basic code compiler.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> implementation.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000434 RID: 1076 RVA: 0x0000F2A6 File Offset: 0x0000D4A6
		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeCompiler CreateCompiler()
		{
			return this._generator;
		}

		// Token: 0x04000378 RID: 888
		private VBCodeGenerator _generator;
	}
}
