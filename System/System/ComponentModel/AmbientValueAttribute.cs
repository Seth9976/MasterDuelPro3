using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the value to pass to a property to cause the property to get its value from another source. This is known as ambience. This class cannot be inherited.</summary>
	// Token: 0x02000252 RID: 594
	[AttributeUsage(AttributeTargets.All)]
	public sealed class AmbientValueAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given a string for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000E30 RID: 3632 RVA: 0x0003EB3E File Offset: 0x0003CD3E
		public AmbientValueAttribute(string value)
		{
			this.Value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.AmbientValueAttribute" /> class, given an object for its value.</summary>
		/// <param name="value">The value of this attribute. </param>
		// Token: 0x06000E31 RID: 3633 RVA: 0x0003EB3E File Offset: 0x0003CD3E
		public AmbientValueAttribute(object value)
		{
			this.Value = value;
		}

		/// <summary>Gets the object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>The object that is the value of this <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0003EB4D File Offset: 0x0003CD4D
		public object Value { get; }

		/// <summary>Determines whether the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.AmbientValueAttribute" /> is equal to the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.AmbientValueAttribute" /> to compare with the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</param>
		// Token: 0x06000E33 RID: 3635 RVA: 0x0003EB58 File Offset: 0x0003CD58
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			AmbientValueAttribute ambientValueAttribute = obj as AmbientValueAttribute;
			if (ambientValueAttribute == null)
			{
				return false;
			}
			if (this.Value == null)
			{
				return ambientValueAttribute.Value == null;
			}
			return this.Value.Equals(ambientValueAttribute.Value);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.AmbientValueAttribute" />.</returns>
		// Token: 0x06000E34 RID: 3636 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
