using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000A0 RID: 160
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class RectIntField : BaseCompositeField<RectInt, IntegerField, int>
	{
		// Token: 0x060005A1 RID: 1441 RVA: 0x0001BC44 File Offset: 0x00019E44
		internal override BaseCompositeField<RectInt, IntegerField, int>.FieldDescription[] DescribeFields()
		{
			BaseCompositeField<RectInt, IntegerField, int>.FieldDescription[] array = new BaseCompositeField<RectInt, IntegerField, int>.FieldDescription[4];
			array[0] = new BaseCompositeField<RectInt, IntegerField, int>.FieldDescription("X", "unity-x-input", (RectInt r) => r.x, delegate(ref RectInt r, int v)
			{
				r.x = v;
			});
			array[1] = new BaseCompositeField<RectInt, IntegerField, int>.FieldDescription("Y", "unity-y-input", (RectInt r) => r.y, delegate(ref RectInt r, int v)
			{
				r.y = v;
			});
			array[2] = new BaseCompositeField<RectInt, IntegerField, int>.FieldDescription("W", "unity-width-input", (RectInt r) => r.width, delegate(ref RectInt r, int v)
			{
				r.width = v;
			});
			array[3] = new BaseCompositeField<RectInt, IntegerField, int>.FieldDescription("H", "unity-height-input", (RectInt r) => r.height, delegate(ref RectInt r, int v)
			{
				r.height = v;
			});
			return array;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x0001BDAC File Offset: 0x00019FAC
		public RectIntField()
			: this(null)
		{
		}

		// Token: 0x060005A3 RID: 1443 RVA: 0x0001BDB8 File Offset: 0x00019FB8
		public RectIntField(string label)
			: base(label, 2)
		{
			base.AddToClassList(RectIntField.ussClassName);
			base.AddToClassList(BaseCompositeField<RectInt, IntegerField, int>.twoLinesVariantUssClassName);
			base.labelElement.AddToClassList(RectIntField.labelUssClassName);
			base.visualInput.AddToClassList(RectIntField.inputUssClassName);
		}

		// Token: 0x04000364 RID: 868
		public new static readonly string ussClassName = "unity-rect-int-field";

		// Token: 0x04000365 RID: 869
		public new static readonly string labelUssClassName = RectIntField.ussClassName + "__label";

		// Token: 0x04000366 RID: 870
		public new static readonly string inputUssClassName = RectIntField.ussClassName + "__input";

		// Token: 0x020000A1 RID: 161
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<RectIntField, RectIntField.UxmlTraits>
		{
		}

		// Token: 0x020000A2 RID: 162
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<RectInt>.UxmlTraits
		{
			// Token: 0x060005A6 RID: 1446 RVA: 0x0001BE48 File Offset: 0x0001A048
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				RectIntField r = (RectIntField)ve;
				r.SetValueWithoutNotify(new RectInt(this.m_XValue.GetValueFromBag(bag, cc), this.m_YValue.GetValueFromBag(bag, cc), this.m_WValue.GetValueFromBag(bag, cc), this.m_HValue.GetValueFromBag(bag, cc)));
			}

			// Token: 0x04000367 RID: 871
			private UxmlIntAttributeDescription m_XValue = new UxmlIntAttributeDescription
			{
				name = "x"
			};

			// Token: 0x04000368 RID: 872
			private UxmlIntAttributeDescription m_YValue = new UxmlIntAttributeDescription
			{
				name = "y"
			};

			// Token: 0x04000369 RID: 873
			private UxmlIntAttributeDescription m_WValue = new UxmlIntAttributeDescription
			{
				name = "w"
			};

			// Token: 0x0400036A RID: 874
			private UxmlIntAttributeDescription m_HValue = new UxmlIntAttributeDescription
			{
				name = "h"
			};
		}
	}
}
