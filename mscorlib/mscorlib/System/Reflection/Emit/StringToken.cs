using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents a token that represents a string.</summary>
	// Token: 0x02000681 RID: 1665
	[ComVisible(true)]
	[Serializable]
	public readonly struct StringToken : IEquatable<StringToken>
	{
		// Token: 0x06003393 RID: 13203 RVA: 0x000C22A9 File Offset: 0x000C04A9
		internal StringToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of StringToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of StringToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this StringToken. </param>
		// Token: 0x06003394 RID: 13204 RVA: 0x000C22B4 File Offset: 0x000C04B4
		public override bool Equals(object obj)
		{
			bool flag = obj is StringToken;
			if (flag)
			{
				StringToken stringToken = (StringToken)obj;
				flag = this.tokValue == stringToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.StringToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.StringToken" /> to compare to the current instance.</param>
		// Token: 0x06003395 RID: 13205 RVA: 0x000C22E5 File Offset: 0x000C04E5
		public bool Equals(StringToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Returns the hash code for this string.</summary>
		/// <returns>Returns the underlying string token.</returns>
		// Token: 0x06003396 RID: 13206 RVA: 0x000C22F5 File Offset: 0x000C04F5
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x04001B01 RID: 6913
		internal readonly int tokValue;
	}
}
