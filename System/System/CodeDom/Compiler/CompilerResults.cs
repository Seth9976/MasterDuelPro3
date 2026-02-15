using System;
using System.Collections.Specialized;
using System.Reflection;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the results of compilation that are returned from a compiler.</summary>
	// Token: 0x02000232 RID: 562
	[Serializable]
	public class CompilerResults
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.Compiler.CompilerResults" /> class that uses the specified temporary files.</summary>
		/// <param name="tempFiles">A <see cref="T:System.CodeDom.Compiler.TempFileCollection" /> with which to manage and store references to intermediate files generated during compilation. </param>
		// Token: 0x06000D91 RID: 3473 RVA: 0x0003DB75 File Offset: 0x0003BD75
		public CompilerResults(TempFileCollection tempFiles)
		{
			this._tempFiles = tempFiles;
		}

		/// <summary>Gets or sets the compiled assembly.</summary>
		/// <returns>An <see cref="T:System.Reflection.Assembly" /> that indicates the compiled assembly.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000D92 RID: 3474 RVA: 0x0003DB9A File Offset: 0x0003BD9A
		// (set) Token: 0x06000D93 RID: 3475 RVA: 0x0003DBD4 File Offset: 0x0003BDD4
		public Assembly CompiledAssembly
		{
			get
			{
				if (this._compiledAssembly == null && this.PathToAssembly != null)
				{
					this._compiledAssembly = Assembly.Load(new AssemblyName
					{
						CodeBase = this.PathToAssembly
					});
				}
				return this._compiledAssembly;
			}
			set
			{
				this._compiledAssembly = value;
			}
		}

		/// <summary>Gets the collection of compiler errors and warnings.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" /> that indicates the errors and warnings resulting from compilation, if any.</returns>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06000D94 RID: 3476 RVA: 0x0003DBDD File Offset: 0x0003BDDD
		public CompilerErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		/// <summary>Gets the compiler output messages.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> that contains the output messages.</returns>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06000D95 RID: 3477 RVA: 0x0003DBE5 File Offset: 0x0003BDE5
		public StringCollection Output
		{
			get
			{
				return this._output;
			}
		}

		/// <summary>Gets or sets the path of the compiled assembly.</summary>
		/// <returns>The path of the assembly, or null if the assembly was generated in memory.</returns>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0003DBED File Offset: 0x0003BDED
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x0003DBF5 File Offset: 0x0003BDF5
		public string PathToAssembly { get; set; }

		/// <summary>Gets or sets the compiler's return value.</summary>
		/// <returns>The compiler's return value.</returns>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0003DBFE File Offset: 0x0003BDFE
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x0003DC06 File Offset: 0x0003BE06
		public int NativeCompilerReturnValue { get; set; }

		// Token: 0x0400094C RID: 2380
		private readonly CompilerErrorCollection _errors = new CompilerErrorCollection();

		// Token: 0x0400094D RID: 2381
		private readonly StringCollection _output = new StringCollection();

		// Token: 0x0400094E RID: 2382
		private Assembly _compiledAssembly;

		// Token: 0x0400094F RID: 2383
		private TempFileCollection _tempFiles;
	}
}
