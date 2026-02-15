using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the installer for a type that installs components.</summary>
	// Token: 0x02000283 RID: 643
	[AttributeUsage(AttributeTargets.Class)]
	public class InstallerTypeAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InstallerTypeAttribute" /> class, when given a <see cref="T:System.Type" /> that represents the installer for a component.</summary>
		/// <param name="installerType">A <see cref="T:System.Type" /> that represents the installer for the component this attribute is bound to. This class must implement <see cref="T:System.ComponentModel.Design.IDesigner" />. </param>
		// Token: 0x06000F4A RID: 3914 RVA: 0x00041E92 File Offset: 0x00040092
		public InstallerTypeAttribute(Type installerType)
		{
			this._typeName = installerType.AssemblyQualifiedName;
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.InstallerTypeAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000F4B RID: 3915 RVA: 0x00041EA8 File Offset: 0x000400A8
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			InstallerTypeAttribute installerTypeAttribute = obj as InstallerTypeAttribute;
			return installerTypeAttribute != null && installerTypeAttribute._typeName == this._typeName;
		}

		/// <summary>Returns the hashcode for this object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.InstallerTypeAttribute" />.</returns>
		// Token: 0x06000F4C RID: 3916 RVA: 0x0003DD7E File Offset: 0x0003BF7E
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000A01 RID: 2561
		private string _typeName;
	}
}
