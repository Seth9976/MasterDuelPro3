using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines an interface for invoking compilation of source code or a CodeDOM tree using a specific compiler.</summary>
	// Token: 0x02000234 RID: 564
	public interface ICodeCompiler
	{
		/// <summary>Compiles an assembly from the specified array of strings containing source code, using the specified compiler settings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the settings for compilation. </param>
		/// <param name="sources">The source code strings to compile. </param>
		// Token: 0x06000D9A RID: 3482
		CompilerResults CompileAssemblyFromSourceBatch(CompilerParameters options, string[] sources);
	}
}
