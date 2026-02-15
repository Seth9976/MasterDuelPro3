using System;

namespace System.CodeDom
{
	/// <summary>Represents a reference to the value of a property.</summary>
	// Token: 0x0200020E RID: 526
	[Serializable]
	public class CodePropertyReferenceExpression : CodeExpression
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class.</summary>
		// Token: 0x06000C81 RID: 3201 RVA: 0x0003A53C File Offset: 0x0003873C
		public CodePropertyReferenceExpression()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.CodeDom.CodePropertyReferenceExpression" /> class using the specified target object and property name.</summary>
		/// <param name="targetObject">A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference. </param>
		/// <param name="propertyName">The name of the property to reference. </param>
		// Token: 0x06000C82 RID: 3202 RVA: 0x0003B559 File Offset: 0x00039759
		public CodePropertyReferenceExpression(CodeExpression targetObject, string propertyName)
		{
			this.TargetObject = targetObject;
			this.PropertyName = propertyName;
		}

		/// <summary>Gets or sets the object that contains the property to reference.</summary>
		/// <returns>A <see cref="T:System.CodeDom.CodeExpression" /> that indicates the object that contains the property to reference.</returns>
		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0003B56F File Offset: 0x0003976F
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x0003B577 File Offset: 0x00039777
		public CodeExpression TargetObject { get; set; }

		/// <summary>Gets or sets the name of the property to reference.</summary>
		/// <returns>The name of the property to reference.</returns>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0003B580 File Offset: 0x00039780
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x0003B591 File Offset: 0x00039791
		public string PropertyName
		{
			get
			{
				return this._propertyName ?? string.Empty;
			}
			set
			{
				this._propertyName = value;
			}
		}

		// Token: 0x040008EB RID: 2283
		private string _propertyName;
	}
}
