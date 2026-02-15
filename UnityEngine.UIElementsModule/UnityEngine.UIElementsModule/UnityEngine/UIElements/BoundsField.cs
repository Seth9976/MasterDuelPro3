using System;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x0200008E RID: 142
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class BoundsField : BaseField<Bounds>
	{
		// Token: 0x06000563 RID: 1379 RVA: 0x0001AB97 File Offset: 0x00018D97
		public BoundsField()
			: this(null)
		{
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0001ABA4 File Offset: 0x00018DA4
		public BoundsField(string label)
			: base(label, null)
		{
			base.delegatesFocus = false;
			base.visualInput.focusable = false;
			base.AddToClassList(BoundsField.ussClassName);
			base.visualInput.AddToClassList(BoundsField.inputUssClassName);
			base.labelElement.AddToClassList(BoundsField.labelUssClassName);
			this.m_CenterField = new Vector3Field("Center");
			this.m_CenterField.name = "unity-m_Center-input";
			this.m_CenterField.delegatesFocus = true;
			this.m_CenterField.AddToClassList(BoundsField.centerFieldUssClassName);
			this.m_CenterField.RegisterValueChangedCallback(delegate(ChangeEvent<Vector3> e)
			{
				Bounds current = this.value;
				current.center = e.newValue;
				this.value = current;
			});
			base.visualInput.hierarchy.Add(this.m_CenterField);
			this.m_ExtentsField = new Vector3Field("Extents");
			this.m_ExtentsField.name = "unity-m_Extent-input";
			this.m_ExtentsField.delegatesFocus = true;
			this.m_ExtentsField.AddToClassList(BoundsField.extentsFieldUssClassName);
			this.m_ExtentsField.RegisterValueChangedCallback(delegate(ChangeEvent<Vector3> e)
			{
				Bounds current2 = this.value;
				current2.extents = e.newValue;
				this.value = current2;
			});
			base.visualInput.hierarchy.Add(this.m_ExtentsField);
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001ACE0 File Offset: 0x00018EE0
		public override void SetValueWithoutNotify(Bounds newValue)
		{
			base.SetValueWithoutNotify(newValue);
			this.m_CenterField.SetValueWithoutNotify(base.rawValue.center);
			this.m_ExtentsField.SetValueWithoutNotify(base.rawValue.extents);
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001AD2A File Offset: 0x00018F2A
		protected override void UpdateMixedValueContent()
		{
			this.m_CenterField.showMixedValue = base.showMixedValue;
			this.m_ExtentsField.showMixedValue = base.showMixedValue;
		}

		// Token: 0x0400032C RID: 812
		public new static readonly string ussClassName = "unity-bounds-field";

		// Token: 0x0400032D RID: 813
		public new static readonly string labelUssClassName = BoundsField.ussClassName + "__label";

		// Token: 0x0400032E RID: 814
		public new static readonly string inputUssClassName = BoundsField.ussClassName + "__input";

		// Token: 0x0400032F RID: 815
		public static readonly string centerFieldUssClassName = BoundsField.ussClassName + "__center-field";

		// Token: 0x04000330 RID: 816
		public static readonly string extentsFieldUssClassName = BoundsField.ussClassName + "__extents-field";

		// Token: 0x04000331 RID: 817
		private Vector3Field m_CenterField;

		// Token: 0x04000332 RID: 818
		private Vector3Field m_ExtentsField;

		// Token: 0x0200008F RID: 143
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<BoundsField, BoundsField.UxmlTraits>
		{
		}

		// Token: 0x02000090 RID: 144
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : BaseField<Bounds>.UxmlTraits
		{
			// Token: 0x0600056B RID: 1387 RVA: 0x0001AE1C File Offset: 0x0001901C
			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
			{
				base.Init(ve, bag, cc);
				BoundsField f = (BoundsField)ve;
				f.SetValueWithoutNotify(new Bounds(new Vector3(this.m_CenterXValue.GetValueFromBag(bag, cc), this.m_CenterYValue.GetValueFromBag(bag, cc), this.m_CenterZValue.GetValueFromBag(bag, cc)), new Vector3(this.m_ExtentsXValue.GetValueFromBag(bag, cc), this.m_ExtentsYValue.GetValueFromBag(bag, cc), this.m_ExtentsZValue.GetValueFromBag(bag, cc))));
			}

			// Token: 0x04000333 RID: 819
			private UxmlFloatAttributeDescription m_CenterXValue = new UxmlFloatAttributeDescription
			{
				name = "cx"
			};

			// Token: 0x04000334 RID: 820
			private UxmlFloatAttributeDescription m_CenterYValue = new UxmlFloatAttributeDescription
			{
				name = "cy"
			};

			// Token: 0x04000335 RID: 821
			private UxmlFloatAttributeDescription m_CenterZValue = new UxmlFloatAttributeDescription
			{
				name = "cz"
			};

			// Token: 0x04000336 RID: 822
			private UxmlFloatAttributeDescription m_ExtentsXValue = new UxmlFloatAttributeDescription
			{
				name = "ex"
			};

			// Token: 0x04000337 RID: 823
			private UxmlFloatAttributeDescription m_ExtentsYValue = new UxmlFloatAttributeDescription
			{
				name = "ey"
			};

			// Token: 0x04000338 RID: 824
			private UxmlFloatAttributeDescription m_ExtentsZValue = new UxmlFloatAttributeDescription
			{
				name = "ez"
			};
		}
	}
}
