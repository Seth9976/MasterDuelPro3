using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Indicates a serializer for the serialization manager to use to serialize the values of the type this attribute is applied to. This class cannot be inherited.</summary>
	// Token: 0x020002F1 RID: 753
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
	public sealed class DesignerSerializerAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.DesignerSerializerAttribute" /> class.</summary>
		/// <param name="serializerTypeName">The fully qualified name of the data type of the serializer. </param>
		/// <param name="baseSerializerTypeName">The fully qualified name of the base data type of the serializer. Multiple serializers can be supplied for a class as long as the serializers have different base types. </param>
		// Token: 0x06001223 RID: 4643 RVA: 0x0005217A File Offset: 0x0005037A
		public DesignerSerializerAttribute(string serializerTypeName, string baseSerializerTypeName)
		{
			this.<SerializerTypeName>k__BackingField = serializerTypeName;
			this.SerializerBaseTypeName = baseSerializerTypeName;
		}

		/// <summary>Gets the fully qualified type name of the serializer base type.</summary>
		/// <returns>The fully qualified type name of the serializer base type.</returns>
		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x00052190 File Offset: 0x00050390
		public string SerializerBaseTypeName { get; }

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00052198 File Offset: 0x00050398
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

		// Token: 0x04000B32 RID: 2866
		private string _typeId;
	}
}
