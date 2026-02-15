using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace System.CodeDom.Compiler
{
	/// <summary>Provides a base class for <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementations. This class is abstract. </summary>
	// Token: 0x0200022B RID: 555
	public abstract class CodeDomProvider : Component
	{
		// Token: 0x06000CFE RID: 3326 RVA: 0x0003BF68 File Offset: 0x0003A168
		static CodeDomProvider()
		{
			CodeDomProvider.AddCompilerInfo(new CompilerInfo(new CompilerParameters
			{
				WarningLevel = 4
			}, typeof(CSharpCodeProvider).FullName)
			{
				_compilerLanguages = new string[] { "c#", "cs", "csharp" },
				_compilerExtensions = new string[] { ".cs", "cs" }
			});
			CodeDomProvider.AddCompilerInfo(new CompilerInfo(new CompilerParameters
			{
				WarningLevel = 4
			}, typeof(VBCodeProvider).FullName)
			{
				_compilerLanguages = new string[] { "vb", "vbs", "visualbasic", "vbscript" },
				_compilerExtensions = new string[] { ".vb", "vb" }
			});
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0003C074 File Offset: 0x0003A274
		private static void AddCompilerInfo(CompilerInfo compilerInfo)
		{
			foreach (string text in compilerInfo._compilerLanguages)
			{
				CodeDomProvider.s_compilerLanguages[text] = compilerInfo;
			}
			foreach (string text2 in compilerInfo._compilerExtensions)
			{
				CodeDomProvider.s_compilerExtensions[text2] = compilerInfo;
			}
			CodeDomProvider.s_allCompilerInfo.Add(compilerInfo);
		}

		/// <summary>Gets the default file name extension to use for source code files in the current language.</summary>
		/// <returns>A file name extension corresponding to the extension of the source files of the current language. The base implementation always returns <see cref="F:System.String.Empty" />.</returns>
		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0003C0D6 File Offset: 0x0003A2D6
		public virtual string FileExtension
		{
			get
			{
				return string.Empty;
			}
		}

		/// <summary>When overridden in a derived class, creates a new code generator.</summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> that can be used to generate <see cref="N:System.CodeDom" /> based source code representations.</returns>
		// Token: 0x06000D01 RID: 3329
		[Obsolete("Callers should not use the ICodeGenerator interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public abstract ICodeGenerator CreateGenerator();

		/// <summary>When overridden in a derived class, creates a new code compiler. </summary>
		/// <returns>An <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> that can be used for compilation of <see cref="N:System.CodeDom" /> based source code representations. </returns>
		// Token: 0x06000D02 RID: 3330
		[Obsolete("Callers should not use the ICodeCompiler interface and should instead use the methods directly on the CodeDomProvider class. Those inheriting from CodeDomProvider must still implement this interface, and should exclude this warning or also obsolete this method.")]
		public abstract ICodeCompiler CreateCompiler();

		/// <summary>Compiles an assembly from the specified array of strings containing source code, using the specified compiler settings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerResults" /> object that indicates the results of compilation.</returns>
		/// <param name="options">A <see cref="T:System.CodeDom.Compiler.CompilerParameters" /> object that indicates the compiler settings for this compilation. </param>
		/// <param name="sources">An array of source code strings to compile. </param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateCompiler" /> method is overridden in a derived class.</exception>
		// Token: 0x06000D03 RID: 3331 RVA: 0x0003C0DD File Offset: 0x0003A2DD
		public virtual CompilerResults CompileAssemblyFromSource(CompilerParameters options, params string[] sources)
		{
			return this.CreateCompilerHelper().CompileAssemblyFromSourceBatch(options, sources);
		}

		/// <summary>Creates an escaped identifier for the specified value.</summary>
		/// <returns>The escaped identifier for the value.</returns>
		/// <param name="value">The string for which to create an escaped identifier.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06000D04 RID: 3332 RVA: 0x0003C0EC File Offset: 0x0003A2EC
		public virtual string CreateEscapedIdentifier(string value)
		{
			return this.CreateGeneratorHelper().CreateEscapedIdentifier(value);
		}

		/// <summary>Returns a value indicating whether the specified code generation support is provided.</summary>
		/// <returns>true if the specified code generation support is provided; otherwise, false.</returns>
		/// <param name="generatorSupport">A <see cref="T:System.CodeDom.Compiler.GeneratorSupport" /> object that indicates the type of code generation support to verify.</param>
		/// <exception cref="T:System.NotImplementedException">Neither this method nor the <see cref="M:System.CodeDom.Compiler.CodeDomProvider.CreateGenerator" /> method is overridden in a derived class.</exception>
		// Token: 0x06000D05 RID: 3333 RVA: 0x0003C0FA File Offset: 0x0003A2FA
		public virtual bool Supports(GeneratorSupport generatorSupport)
		{
			return this.CreateGeneratorHelper().Supports(generatorSupport);
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0003C108 File Offset: 0x0003A308
		private ICodeCompiler CreateCompilerHelper()
		{
			ICodeCompiler codeCompiler = this.CreateCompiler();
			if (codeCompiler == null)
			{
				throw new NotImplementedException("This CodeDomProvider does not support this method.");
			}
			return codeCompiler;
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0003C11E File Offset: 0x0003A31E
		private ICodeGenerator CreateGeneratorHelper()
		{
			ICodeGenerator codeGenerator = this.CreateGenerator();
			if (codeGenerator == null)
			{
				throw new NotImplementedException("This CodeDomProvider does not support this method.");
			}
			return codeGenerator;
		}

		// Token: 0x0400092D RID: 2349
		private static readonly Dictionary<string, CompilerInfo> s_compilerLanguages = new Dictionary<string, CompilerInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400092E RID: 2350
		private static readonly Dictionary<string, CompilerInfo> s_compilerExtensions = new Dictionary<string, CompilerInfo>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400092F RID: 2351
		private static readonly List<CompilerInfo> s_allCompilerInfo = new List<CompilerInfo>();
	}
}
