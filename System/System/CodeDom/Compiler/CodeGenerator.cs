using System;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides an example implementation of the <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> interface. This class is abstract.</summary>
	// Token: 0x0200022C RID: 556
	public abstract class CodeGenerator
	{
		/// <summary>Gets a value indicating whether the specified string is a valid identifier.</summary>
		/// <returns>true if the specified string is a valid identifier; otherwise, false.</returns>
		/// <param name="value">The string to test for validity. </param>
		// Token: 0x06000D09 RID: 3337 RVA: 0x0003C134 File Offset: 0x0003A334
		public static bool IsValidLanguageIndependentIdentifier(string value)
		{
			return CSharpHelpers.IsValidTypeNameOrIdentifier(value, false);
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0003C13D File Offset: 0x0003A33D
		internal static bool IsValidLanguageIndependentTypeName(string value)
		{
			return CSharpHelpers.IsValidTypeNameOrIdentifier(value, true);
		}

		/// <summary>Attempts to validate each identifier field contained in the specified <see cref="T:System.CodeDom.CodeObject" /> or <see cref="N:System.CodeDom" /> tree.</summary>
		/// <param name="e">An object to test for invalid identifiers. </param>
		/// <exception cref="T:System.ArgumentException">The specified <see cref="T:System.CodeDom.CodeObject" /> contains an invalid identifier. </exception>
		// Token: 0x06000D0B RID: 3339 RVA: 0x0003C146 File Offset: 0x0003A346
		public static void ValidateIdentifiers(CodeObject e)
		{
			new CodeValidator().ValidateIdentifiers(e);
		}
	}
}
