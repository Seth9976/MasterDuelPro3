using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000AC RID: 172
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class Vector4Field : BaseCompositeField<Vector4, FloatField, float>
	{
		// Token: 0x060005CE RID: 1486 RVA: 0x0001C3E4 File Offset: 0x0001A5E4
		internal override BaseCompositeField<Vector4, FloatField, float>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<Vector4, FloatField, float>.FieldDescription[] array = new BaseCompositeField<Vector4, FloatField, float>.FieldDescription[4];
			array[0] = new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("X", "unity-x-input", (Vector4 r) => r.x, delegate(ref Vector4 r, float v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("Y", "unity-y-input", (Vector4 r) => r.y, delegate(ref Vector4 r, float v)
			{
				r.y = v;
			});
			array[2] = new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("Z", "unity-z-input", (Vector4 r) => r.z, delegate(ref Vector4 r, float v)
			{
				r.z = v;
			});
			array[3] = new BaseCompositeField<Vector4, FloatField, float>.FieldDescription("W", "unity-w-input", (Vector4 r) => r.w, delegate(ref Vector4 r, float v)
			{
				r.w = v;
			});
			return array;
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x0001C54C File Offset: 0x0001A74C
		public Vector4Field()
			: this(null)
		{
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0001C557 File Offset: 0x0001A757
		public Vector4Field(string label)
			: base(label, 4)
		{
			base.AddToClassList(Vector4Field.ussClassName);
			base.labelElement.AddToClassList(Vector4Field.labelUssClassName);
			base.visualInput.AddToClassList(Vector4Field.inputUssClassName);
		}

		// Token: 0x0400038B RID: 907
		public new static readonly string ussClassName = "unity-vector4-field";

		// Token: 0x0400038C RID: 908
		public new static readonly string labelUssClassName = Vector4Field.ussClassName + "__label";

		// Token: 0x0400038D RID: 909
		public new static readonly string inputUssClassName = Vector4Field.ussClassName + "__input";

		// Token: 0x020000AD RID: 173
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Vector4Field, Vector4Field.UxmlTraits>
		{
		}

		// Token: 0x020000AE RID: 174
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Vector4>.UxmlTraits
		{
			// Token: 0x060005D3 RID: 1491 RVA: 0x0001C5D0 File Offset: 0x0001A7D0
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Vector4Field f = (Vector4Field)ve;
				f.SetValueWithoutNotify(new Vector4(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc), this.m_ZValue.GetValueFromBag(bag, cc), this.m_WValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x0400038E RID: 910
			private UxmlFloatAttributeDescription m_XValue = new UxmlFloatAttributeDescription
			{
				name = "x"
			};

			// Token: 0x0400038F RID: 911
			private UxmlFloatAttributeDescription m_YValue = new UxmlFloatAttributeDescription
			{
				name = "y"
			};

			// Token: 0x04000390 RID: 912
			private UxmlFloatAttributeDescription m_ZValue = new UxmlFloatAttributeDescription
			{
				name = "z"
			};

			// Token: 0x04000391 RID: 913
			private UxmlFloatAttributeDescription m_WValue = new UxmlFloatAttributeDescription
			{
				name = "w"
			};
		}
	}
}
