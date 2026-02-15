using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace Microsoft.CSharp
{
	/// <summary>Provides access to instances of the C# code generator and code compiler.</summary>
	// Token: 0x020000E6 RID: 230
	public class CSharpCodeProvider : CodeDomProvider
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.CSharp.CSharpCodeProvider" /> class. </summary>
		// Token: 0x06000444 RID: 1092 RVA: 0x0000FFA6 File Offset: 0x0000E1A6
		public CSharpCodeProvider()
		{
			this._generator = new CSharpCodeGenerator();
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.CSharp.CSharpCodeProvider" /> class by using the specified provider options. </summary>
		/// <param name="providerOptions">A <see cref="T:System.Collections.Generic.IDictionary`2" /> object that contains the provider options from the configuration file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerOptions" /> is null.</exception>
		// Token: 0x06000445 RID: 1093 RVA: 0x0000FFB9 File Offset: 0x0000E1B9
		public CSharpCodeProvider(IDictionary<string, string> providerOptions)
		{
			if (providerOptions == null)
			{
				throw new ArgumentNullException("providerOptions");
			}
			this._generator = new CSharpCodeGenerator(providerOptions);
		}

		/// <summary>Gets the file name extension to use when creating source code files.</summary>
		/// <returns>The file name extension to use for generated source code files.</returns>
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000446 RID: 1094 RVA: 0x0000FFDB File Offset: 0x0000E1DB
		public override string FileExtension
		{
			get
			{
				return "cs";
			}
		}

		/// <summary>Gets an instance of the C# code generator.</summary>
		/// <returns>An instance of the C# <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> implementation.</returns>
		// Token: 0x06000447 RID: 1095 RVA: 0x0000FFE2 File Offset: 0x0000E1E2
		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeGenerator CreateGenerator()
		{
			return this._generator;
		}

		/// <summary>Gets an instance of the C# code compiler.</summary>
		/// <returns>An instance of the C# <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> implementation.</returns>
		// Token: 0x06000448 RID: 1096 RVA: 0x0000FFE2 File Offset: 0x0000E1E2
		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class.")]
		public override ICodeCompiler CreateCompiler()
		{
			return this._generator;
		}

		// Token: 0x04000380 RID: 896
		private readonly CSharpCodeGenerator _generator;
	}
}
