using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates the base serializer to use for a root designer object. This class cannot be inherited.</summary>
	// Token: 0x020002F3 RID: 755
	[Obsolete("This attribute has been deprecated. Use DesignerSerializerAttribute instead.  For example, to specify a root designer for CodeDom, use DesignerSerializerAttribute(...,typeof(TypeCodeDomSerializer)).  https://go.microsoft.com/fwlink/?linkid=14202")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class RootDesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.RootDesignerSerializerAttribute" /> class using the specified attributes.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerTypeName">The name of the base type of the serializer. A class can include multiple serializers as they all have different base types. </param>
		/// <param name="reloadable">true if this serializer supports dynamic reloading of the document; otherwise, false. </param>
		// Token: 0x0600122B RID: 4651 RVA: 0x0005242A File Offset: 0x0005062A
		public RootDesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName, bool reloadable)
		{
			this.<SerializerTypeName>k__BackingField = serializerTypeName;
			this.SerializerBaseTypeName = baseSerializerTypeName;
			this.<Reloadable>k__BackingField = reloadable;
		}

		/// <summary>Gets the fully qualified type name of the base type of the serializer.</summary>
		/// <returns>The name of the base type of the serializer.</returns>
		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x0600122C RID: 4652 RVA: 0x00052447 File Offset: 0x00050647
		public string SerializerBaseTypeName { get; }

		/// <summary>Gets a unique ID for this attribute type.</summary>
		/// <returns>An object containing a unique ID for this attribute type.</returns>
		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00052450 File Offset: 0x00050650
		public override object TypeId
		{
			get
			{
				if (this._typeId == null)
				{
					string text = this.SerializerBaseTypeName;
					int num = text.IndexOf(',');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					this._typeId = base.GetType().FullName + text;
				}
				return this._typeId;
			}
		}

		// Token: 0x04000B38 RID: 2872
		private string _typeId;
	}
}
