using System;
using System.ComponentModel.Design;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Specifies the class used to implement design-time services for a component.</summary>
	// Token: 0x020002BB RID: 699
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the name of the type that provides design-time services.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		// Token: 0x06001090 RID: 4240 RVA: 0x0004524C File Offset: 0x0004344C
		public DesignerAttribute(string designerTypeName)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = typeof(IDesigner).FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the type that provides design-time services.</summary>
		/// <param name="designerType">A <see cref="T:System.Type" /> that represents the class that provides design-time services for the component this attribute is bound to. </param>
		// Token: 0x06001091 RID: 4241 RVA: 0x0004527C File Offset: 0x0004347C
		public DesignerAttribute(Type designerType)
		{
			this.designerTypeName = designerType.AssemblyQualifiedName;
			this.designerBaseTypeName = typeof(IDesigner).FullName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class using the designer type and the base class for the designer.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		/// <param name="designerBaseTypeName">The fully qualified name of the base class to associate with the designer class. </param>
		// Token: 0x06001092 RID: 4242 RVA: 0x000452A5 File Offset: 0x000434A5
		public DesignerAttribute(string designerTypeName, string designerBaseTypeName)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = designerBaseTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DesignerAttribute" /> class, using the name of the designer class and the base class for the designer.</summary>
		/// <param name="designerTypeName">The concatenation of the fully qualified name of the type that provides design-time services for the component this attribute is bound to, and the name of the assembly this type resides in. </param>
		/// <param name="designerBaseType">A <see cref="T:System.Type" /> that represents the base class to associate with the <paramref name="designerTypeName" />. </param>
		// Token: 0x06001093 RID: 4243 RVA: 0x000452C7 File Offset: 0x000434C7
		public DesignerAttribute(string designerTypeName, Type designerBaseType)
		{
			designerTypeName.ToUpper(CultureInfo.InvariantCulture);
			this.designerTypeName = designerTypeName;
			this.designerBaseTypeName = designerBaseType.AssemblyQualifiedName;
		}

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x000452F0 File Offset: 0x000434F0
		public override object TypeId
		{
			get
			{
				if (this.typeId == null)
				{
					string text = this.designerBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this.typeId = base.GetType().FullName + text;
				}
				return this.typeId;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DesignerAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06001095 RID: 4245 RVA: 0x00045340 File Offset: 0x00043540
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DesignerAttribute designerAttribute = obj as DesignerAttribute;
			return designerAttribute != null && designerAttribute.designerBaseTypeName == this.designerBaseTypeName && designerAttribute.designerTypeName == this.designerTypeName;
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x00045383 File Offset: 0x00043583
		public override int GetHashCode()
		{
			return this.designerTypeName.GetHashCode() ^ this.designerBaseTypeName.GetHashCode();
		}

		// Token: 0x04000A6C RID: 2668
		private readonly string designerTypeName;

		// Token: 0x04000A6D RID: 2669
		private readonly string designerBaseTypeName;

		// Token: 0x04000A6E RID: 2670
		private string typeId;
	}
}
