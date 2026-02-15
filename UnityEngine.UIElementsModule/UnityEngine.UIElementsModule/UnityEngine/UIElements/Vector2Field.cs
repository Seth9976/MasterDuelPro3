using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A4 RID: 164
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class Vector2Field : BaseCompositeField<Vector2, FloatField, float>
	{
		// Token: 0x060005B2 RID: 1458 RVA: 0x0001BF70 File Offset: 0x0001A170
		internal override BaseCompositeField<Vector2, FloatField, float>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<Vector2, FloatField, float>.FieldDescription[] array = new BaseCompositeField<Vector2, FloatField, float>.FieldDescription[2];
			array[0] = new BaseCompositeField<Vector2, FloatField, float>.FieldDescription("X", "unity-x-input", (Vector2 r) => r.x, delegate(ref Vector2 r, float v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<Vector2, FloatField, float>.FieldDescription("Y", "unity-y-input", (Vector2 r) => r.y, delegate(ref Vector2 r, float v)
			{
				r.y = v;
			});
			return array;
		}

		// Token: 0x060005B3 RID: 1459 RVA: 0x0001C030 File Offset: 0x0001A230
		public Vector2Field()
			: this(null)
		{
		}

		// Token: 0x060005B4 RID: 1460 RVA: 0x0001C03B File Offset: 0x0001A23B
		public Vector2Field(string label)
			: base(label, 2)
		{
			base.AddToClassList(Vector2Field.ussClassName);
			base.labelElement.AddToClassList(Vector2Field.labelUssClassName);
			base.visualInput.AddToClassList(Vector2Field.inputUssClassName);
		}

		// Token: 0x04000374 RID: 884
		public new static readonly string ussClassName = "unity-vector2-field";

		// Token: 0x04000375 RID: 885
		public new static readonly string labelUssClassName = Vector2Field.ussClassName + "__label";

		// Token: 0x04000376 RID: 886
		public new static readonly string inputUssClassName = Vector2Field.ussClassName + "__input";

		// Token: 0x020000A5 RID: 165
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<Vector2Field, Vector2Field.UxmlTraits>
		{
		}

		// Token: 0x020000A6 RID: 166
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Vector2>.UxmlTraits
		{
			// Token: 0x060005B7 RID: 1463 RVA: 0x0001C0B4 File Offset: 0x0001A2B4
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				Vector2Field f = (Vector2Field)ve;
				f.SetValueWithoutNotify(new Vector2(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x04000377 RID: 887
			private UxmlFloatAttributeDescription m_XValue = new UxmlFloatAttributeDescription
			{
				name = "x"
			};

			// Token: 0x04000378 RID: 888
			private UxmlFloatAttributeDescription m_YValue = new UxmlFloatAttributeDescription
			{
				name = "y"
			};
		}
	}
}
