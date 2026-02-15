using System;

namespace System.Diagnostics.SymbolStore
{
	/// <summary>The <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure is an object representation of a token that represents symbolic information.</summary>
	// Token: 0x020006E2 RID: 1762
	public readonly struct SymbolToken
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> structure when given a value.</summary>
		/// <param name="val">The value to be used for the token. </param>
		// Token: 0x060037AA RID: 14250 RVA: 0x000DB59C File Offset: 0x000D979C
		public SymbolToken(int val)
		{
			this._token = val;
		}

		/// <summary>Generates the hash code for the current token.</summary>
		/// <returns>The hash code for the current token.</returns>
		// Token: 0x060037AB RID: 14251 RVA: 0x000DB5A5 File Offset: 0x000D97A5
		public override int GetHashCode()
		{
			return this._token;
		}

		/// <summary>Determines whether <paramref name="obj" /> is an instance of <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> and is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">The object to check. </param>
		// Token: 0x060037AC RID: 14252 RVA: 0x000DB5AD File Offset: 0x000D97AD
		public override bool Equals(object obj)
		{
			return obj is SymbolToken && this.Equals((SymbolToken)obj);
		}

		/// <summary>Determines whether <paramref name="obj" /> is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Diagnostics.SymbolStore.SymbolToken" /> to check.</param>
		// Token: 0x060037AD RID: 14253 RVA: 0x000DB5C5 File Offset: 0x000D97C5
		public bool Equals(SymbolToken obj)
		{
			return obj._token == this._token;
		}

		// Token: 0x04001E0B RID: 7691
		private readonly int _token;
	}
}
