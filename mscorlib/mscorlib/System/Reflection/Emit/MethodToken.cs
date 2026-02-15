using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The MethodToken struct is an object representation of a token that represents a method.</summary>
	// Token: 0x02000675 RID: 1653
	[ComVisible(true)]
	[Serializable]
	public readonly struct MethodToken : IEquatable<MethodToken>
	{
		// Token: 0x060032E4 RID: 13028 RVA: 0x000BE5DA File Offset: 0x000BC7DA
		internal MethodToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Tests whether the given object is equal to this MethodToken object.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of MethodToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this object. </param>
		// Token: 0x060032E5 RID: 13029 RVA: 0x000BE5E4 File Offset: 0x000BC7E4
		public override bool Equals(object obj)
		{
			bool flag = obj is MethodToken;
			if (flag)
			{
				MethodToken methodToken = (MethodToken)obj;
				flag = this.tokValue == methodToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.MethodToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.MethodToken" /> to compare to the current instance.</param>
		// Token: 0x060032E6 RID: 13030 RVA: 0x000BE615 File Offset: 0x000BC815
		public bool Equals(MethodToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Returns the generated hash code for this method.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x060032E7 RID: 13031 RVA: 0x000BE625 File Offset: 0x000BC825
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Returns the metadata token for this method.</summary>
		/// <returns>Read-only. Returns the metadata token for this method.</returns>
		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x000BE625 File Offset: 0x000BC825
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x040019C7 RID: 6599
		internal readonly int tokValue;
	}
}
