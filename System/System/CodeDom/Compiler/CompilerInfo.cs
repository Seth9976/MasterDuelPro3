using System;
using System.Collections.Generic;

namespace System.CodeDom.Compiler
{
	/// <summary>Represents the configuration settings of a language provider. This class cannot be inherited.</summary>
	// Token: 0x02000230 RID: 560
	public sealed class CompilerInfo
	{
		/// <summary>Gets the type of the configured <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> implementation.</summary>
		/// <returns>A read-only <see cref="T:System.Type" /> instance that represents the configured language provider type.</returns>
		/// <exception cref="T:System.Configuration.ConfigurationException">The language provider is not configured on this computer. </exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">Cannot locate the type because it is a null or empty string.-or-Cannot locate the type because the name for the <see cref="T:System.CodeDom.Compiler.CodeDomProvider" /> cannot be found in the configuration file.</exception>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0003D908 File Offset: 0x0003BB08
		public Type CodeDomProviderType
		{
			get
			{
				if (this._type == null)
				{
					lock (this)
					{
						if (this._type == null)
						{
							this._type = Type.GetType(this._codeDomProviderTypeName);
						}
					}
				}
				return this._type;
			}
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x0003D970 File Offset: 0x0003BB70
		internal CompilerInfo(CompilerParameters compilerParams, string codeDomProviderTypeName)
		{
			this._codeDomProviderTypeName = codeDomProviderTypeName;
			this._compilerParams = compilerParams ?? new CompilerParameters();
		}

		/// <summary>Returns the hash code for the current instance.</summary>
		/// <returns>A 32-bit signed integer hash code for the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> instance, suitable for use in hashing algorithms and data structures such as a hash table. </returns>
		// Token: 0x06000D77 RID: 3447 RVA: 0x0003D99A File Offset: 0x0003BB9A
		public override int GetHashCode()
		{
			return this._codeDomProviderTypeName.GetHashCode();
		}

		/// <summary>Determines whether the specified object represents the same language provider and compiler settings as the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" />.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.CodeDom.Compiler.CompilerInfo" /> object and its value is the same as this instance; otherwise, false.</returns>
		/// <param name="o">The object to compare with the current <see cref="T:System.CodeDom.Compiler.CompilerInfo" />. </param>
		// Token: 0x06000D78 RID: 3448 RVA: 0x0003D9A8 File Offset: 0x0003BBA8
		public override bool Equals(object o)
		{
			CompilerInfo compilerInfo = o as CompilerInfo;
			return compilerInfo != null && (this.CodeDomProviderType == compilerInfo.CodeDomProviderType && this.CompilerParams.WarningLevel == compilerInfo.CompilerParams.WarningLevel && this.CompilerParams.IncludeDebugInformation == compilerInfo.CompilerParams.IncludeDebugInformation) && this.CompilerParams.CompilerOptions == compilerInfo.CompilerParams.CompilerOptions;
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000D79 RID: 3449 RVA: 0x0003DA21 File Offset: 0x0003BC21
		internal CompilerParameters CompilerParams
		{
			get
			{
				return this._compilerParams;
			}
		}

		// Token: 0x04000938 RID: 2360
		internal readonly IDictionary<string, string> _providerOptions = new Dictionary<string, string>();

		// Token: 0x04000939 RID: 2361
		internal string _codeDomProviderTypeName;

		// Token: 0x0400093A RID: 2362
		internal CompilerParameters _compilerParams;

		// Token: 0x0400093B RID: 2363
		internal string[] _compilerLanguages;

		// Token: 0x0400093C RID: 2364
		internal string[] _compilerExtensions;

		// Token: 0x0400093D RID: 2365
		private Type _type;
	}
}
