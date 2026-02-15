using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the property that an implementer of <see cref="T:System.ComponentModel.IExtenderProvider" /> offers to other components. This class cannot be inherited</summary>
	// Token: 0x02000293 RID: 659
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ProvidePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and its <see cref="T:System.Type" />.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type. </param>
		/// <param name="receiverType">The <see cref="T:System.Type" /> of the data type of the object that can receive the property. </param>
		// Token: 0x06000FDB RID: 4059 RVA: 0x0004323C File Offset: 0x0004143C
		public ProvidePropertyAttribute(string propertyName, Type receiverType)
		{
			this.PropertyName = propertyName;
			this.ReceiverTypeName = receiverType.AssemblyQualifiedName;
		}

		/// <summary>Gets the name of a property that this class provides.</summary>
		/// <returns>The name of a property that this class provides.</returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x00043257 File Offset: 0x00041457
		public string PropertyName { get; }

		/// <summary>Gets the name of the data type this property can extend.</summary>
		/// <returns>The name of the data type this property can extend.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0004325F File Offset: 0x0004145F
		public string ReceiverTypeName { get; }

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000FDE RID: 4062 RVA: 0x00043268 File Offset: 0x00041468
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ProvidePropertyAttribute providePropertyAttribute = obj as ProvidePropertyAttribute;
			return providePropertyAttribute != null && providePropertyAttribute.PropertyName == this.PropertyName && providePropertyAttribute.ReceiverTypeName == this.ReceiverTypeName;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</returns>
		// Token: 0x06000FDF RID: 4063 RVA: 0x000432AB File Offset: 0x000414AB
		public override int GetHashCode()
		{
			return this.PropertyName.GetHashCode() ^ this.ReceiverTypeName.GetHashCode();
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x000432C4 File Offset: 0x000414C4
		public override object TypeId
		{
			get
			{
				return base.GetType().FullName + this.PropertyName;
			}
		}
	}
}
