using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	/// <summary>Represents a label in the instruction stream. Label is used in conjunction with the <see cref="T:System.Reflection.Emit.ILGenerator" /> class.</summary>
	// Token: 0x02000671 RID: 1649
	[ComVisible(true)]
	[Serializable]
	public readonly struct Label : IEquatable<Label>
	{
		// Token: 0x06003285 RID: 12933 RVA: 0x000BDA7C File Offset: 0x000BBC7C
		internal Label(int val)
		{
			this.label = val;
		}

		/// <summary>Checks if the given object is an instance of Label and is equal to this instance.</summary>
		/// <returns>Returns true if <paramref name="obj" /> is an instance of Label and is equal to this object; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this Label instance. </param>
		// Token: 0x06003286 RID: 12934 RVA: 0x000BDA88 File Offset: 0x000BBC88
		public override bool Equals(object obj)
		{
			bool flag = obj is Label;
			if (flag)
			{
				Label label = (Label)obj;
				flag = this.label == label.label;
			}
			return flag;
		}

		/// <summary>Indicates whether the current instance is equal to the specified <see cref="T:System.Reflection.Emit.Label" />.</summary>
		/// <returns>true if the value of <paramref name="obj" /> is equal to the value of the current instance; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Reflection.Emit.Label" /> to compare to the current instance.</param>
		// Token: 0x06003287 RID: 12935 RVA: 0x000BDAB9 File Offset: 0x000BBCB9
		public bool Equals(Label obj)
		{
			return this.label == obj.label;
		}

		/// <summary>Generates a hash code for this instance.</summary>
		/// <returns>Returns a hash code for this instance.</returns>
		// Token: 0x06003288 RID: 12936 RVA: 0x000BDAC9 File Offset: 0x000BBCC9
		public override int GetHashCode()
		{
			return this.label.GetHashCode();
		}

		// Token: 0x040019A3 RID: 6563
		internal readonly int label;
	}
}
