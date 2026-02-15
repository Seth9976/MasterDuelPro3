using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Defines an interface for generating code.</summary>
	// Token: 0x02000235 RID: 565
	public interface ICodeGenerator
	{
		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <returns>The escaped identifier for the value.</returns>
		/// <param name="value">The string to create an escaped identifier for. </param>
		// Token: 0x06000D9B RID: 3483
		string CreateEscapedIdentifier(string value);

		/// <summary>Gets a value indicating whether the generator provides support for the language features represented by the specified <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> object.</summary>
		/// <returns>true if the specified capabilities are supported; otherwise, false.</returns>
		/// <param name="supports">The capabilities to test the generator for. </param>
		// Token: 0x06000D9C RID: 3484
		bool Supports(GeneratorSupport supports);
	}
}
