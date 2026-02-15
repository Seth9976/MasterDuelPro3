using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x0200009C RID: 156
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class RectField : BaseCompositeField<Rect, FloatField, float>
	{
		// Token: 0x06000590 RID: 1424 RVA: 0x0001B918 File Offset: 0x00019B18
		internal override BaseCompositeField<Rect, FloatField, float>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<Rect, FloatField, float>.FieldDescription[] array = new BaseCompositeField<Rect, FloatField, float>.FieldDescription[4];
			array[0] = new BaseCompositeField<Rect, FloatField, float>.FieldDescription("X", "unity-x-input", (Rect r) => r.x, delegate(ref Rect r, float v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<Rect, FloatField, float>.FieldDescription("Y", "unity-y-input", (Rect r) => r.y, delegate(ref Rect r, float v)
			{
				r.y = v;
			});
			array[2] = new BaseCompositeField<Rect, FloatField, float>.FieldDescription("W", "unity-width-input", (Rect r) => r.width, delegate(ref Rect r, float v)
			{
				r.width = v;
			});
			array[3] = new BaseCompositeField<Rect, FloatField, float>.FieldDescription("H", "unity-height-input", (Rect r) => r.height, delegate(ref Rect r, float v)
			{
				r.height = v;
			});
			return array;
		}

		// Token: 0x06000591 RID: 1425 RVA: 0x0001BA80 File Offset: 0x00019C80
		public RectField()
			: this(null)
		{
		}

		// Token: 0x06000592 RID: 1426 RVA: 0x0001BA8C File Offset: 0x00019C8C
		public RectField(string label)
			: base(label, 2)
		{
			base.AddToClassList(RectField.ussClassName);
			base.AddToClassList(BaseCompositeField<Rect, FloatField, float>.twoLinesVariantUssClassName);
			base.labelElement.AddToClassList(RectField.labelUssClassName);
			base.visualInput.AddToClassList(RectField.inputUssClassName);
		}

		// Token: 0x04000354 RID: 852
		public new static readonly string ussClassName = "unity-rect-field";

		// Token: 0x04000355 RID: 853
		public new static readonly string labelUssClassName = RectField.ussClassName + "__label";

		// Token: 0x04000356 RID: 854
		public new static readonly string inputUssClassName = RectField.ussClassName + "__input";

		// Token: 0x0200009D RID: 157
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<RectField, RectField.UxmlTraits>
		{
		}

		// Token: 0x0200009E RID: 158
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Rect>.UxmlTraits
		{
			// Token: 0x06000595 RID: 1429 RVA: 0x0001BB1C File Offset: 0x00019D1C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RectField r = (RectField)ve;
				r.SetValueWithoutNotify(new Rect(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc), this.m_WValue.GetValueFromBag(bag, cc), this.m_HValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x04000357 RID: 855
			private UxmlFloatAttributeDescription m_XValue = new UxmlFloatAttributeDescription
			{
				name = "x"
			};

			// Token: 0x04000358 RID: 856
			private UxmlFloatAttributeDescription m_YValue = new UxmlFloatAttributeDescription
			{
				name = "y"
			};

			// Token: 0x04000359 RID: 857
			private UxmlFloatAttributeDescription m_WValue = new UxmlFloatAttributeDescription
			{
				name = "w"
			};

			// Token: 0x0400035A RID: 858
			private UxmlFloatAttributeDescription m_HValue = new UxmlFloatAttributeDescription
			{
				name = "h"
			};
		}
	}
}
