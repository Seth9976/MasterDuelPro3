using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>The FieldToken struct is an object representation of a token that represents a field.</summary>
	// Token: 0x02000666 RID: 1638
	[ComVisible(true)]
	[Serializable]
	public readonly struct FieldToken : IEquatable<FieldToken>
	{
		// Token: 0x06003203 RID: 12803 RVA: 0x000BB3F6 File Offset: 0x000B95F6
		internal FieldToken(int val)
		{
			this.tokValue = val;
		}

		/// <summary>Determines if an object is an instance of FieldToken and is equal to this instance.</summary>
		/// <returns>Returns true if <paramref name="obj" /> is an instance of FieldToken and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare to this FieldToken. </param>
		// Token: 0x06003204 RID: 12804 RVA: 0x000BB400 File Offset: 0x000B9600
		public override bool Equals(object obj)
		{
			bool flag = obj is FieldToken;
			if (flag)
			{
				FieldToken fieldToken = (FieldToken)obj;
				flag = this.tokValue == fieldToken.tokValue;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.FieldToken" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.FieldToken" /> to compare to the current instance.</param>
		// Token: 0x06003205 RID: 12805 RVA: 0x000BB431 File Offset: 0x000B9631
		public bool Equals(FieldToken obj)
		{
			return this.tokValue == obj.tokValue;
		}

		/// <summary>Generates the hash code for this field.</summary>
		/// <returns>Returns the hash code for this instance.</returns>
		// Token: 0x06003206 RID: 12806 RVA: 0x000BB441 File Offset: 0x000B9641
		public override int GetHashCode()
		{
			return this.tokValue;
		}

		/// <summary>Retrieves the metadata token for this field.</summary>
		/// <returns>Read-only. Retrieves the metadata token of this field.</returns>
		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000BB441 File Offset: 0x000B9641
		public int Token
		{
			get
			{
				return this.tokValue;
			}
		}

		// Token: 0x04001968 RID: 6504
		internal readonly int tokValue;
	}
}
