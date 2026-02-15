using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents the Token returned by the metadata to represent a type.</summary>
	// Token: 0x02000684 RID: 1668
	[ComVisible(true)]
	[Serializable]
	public readonly struct TypeToken : IEquatable<TypeToken>
	{
		// Token: 0x0600343A RID: 13370 RVA: 0x000C44E0 File Offset: 0x000C26E0
		internal TypeToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Checks if the given object is an instance of TypeToken and is equal to this instance.</summary>
		/// <returns>true if <paramref name="obj" /> is an instance of TypeToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this TypeToken. </param>
		// Token: 0x0600343B RID: 13371 RVA: 0x000C44EC File Offset: 0x000C26EC
		public override bool Equals(object obj)
		{
			bool flag = obj is TypeToken;
			if (flag)
			{
				TypeToken typeToken = (TypeToken)obj;
				flag = this.tokValue == typeToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.TypeToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.TypeToken" /> to compare to the current instance.</param>
		// Token: 0x0600343C RID: 13372 RVA: 0x000C451D File Offset: 0x000C271D
		public bool Equals(TypeToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Generates the hash code for this type.</summary>
		/// <returns>Returns the hash code for this type.</returns>
		// Token: 0x0600343D RID: 13373 RVA: 0x000C452D File Offset: 0x000C272D
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		// Token: 0x04001B23 RID: 6947
		internal readonly int tokValue;
	}
}
