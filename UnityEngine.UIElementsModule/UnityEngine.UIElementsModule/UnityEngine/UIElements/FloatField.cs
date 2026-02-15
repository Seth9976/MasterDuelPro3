using System;
using System.Globalization;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000C4 RID: 196
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class FloatField : TextValueField<float>
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001D5F2 File Offset: 0x0001B7F2
		private FloatField.FloatInput floatInput
		{
			get
			{
				return (FloatField.FloatInput)base.textInputBase;
			}
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x0001D600 File Offset: 0x0001B800
		protected override string ValueToString(float v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001D62C File Offset: 0x0001B82C
		protected override float StringToValue(string str)
		{
			float v;
			return UINumericFieldsUtils.TryConvertStringToFloat(str, base.textInputBase.originalText, out v) ? v : base.rawValue;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001D65E File Offset: 0x0001B85E
		public FloatField()
			: this(null, 1000)
		{
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001D670 File Offset: 0x0001B870
		public FloatField(string label, int maxLength = 1000)
			: base(label, maxLength, new FloatField.FloatInput())
		{
			base.AddToClassList(FloatField.ussClassName);
			base.labelElement.AddToClassList(FloatField.labelUssClassName);
			base.visualInput.AddToClassList(FloatField.inputUssClassName);
			base.AddLabelDragger<float>();
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
		internal override bool CanTryParse(string textString)
		{
			float num;
			return float.TryParse(textString, out num);
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001D6D9 File Offset: 0x0001B8D9
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, float startValue)
		{
			this.floatInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x040003CB RID: 971
		public new static readonly string ussClassName = "unity-float-field";

		// Token: 0x040003CC RID: 972
		public new static readonly string labelUssClassName = FloatField.ussClassName + "__label";

		// Token: 0x040003CD RID: 973
		public new static readonly string inputUssClassName = FloatField.ussClassName + "__input";

		// Token: 0x020000C5 RID: 197
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<FloatField, FloatField.UxmlTraits>
		{
		}

		// Token: 0x020000C6 RID: 198
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<float, UxmlFloatAttributeDescription>
		{
		}

		// Token: 0x020000C7 RID: 199
		private class FloatInput : TextValueField<float>.TextValueInput
		{
			// Token: 0x170000E4 RID: 228
			// (get) Token: 0x06000636 RID: 1590 RVA: 0x0001D731 File Offset: 0x0001B931
			private FloatField parentFloatField
			{
				get
				{
					return (FloatField)base.parent;
				}
			}

			// Token: 0x06000637 RID: 1591 RVA: 0x0001D73E File Offset: 0x0001B93E
			internal FloatInput()
			{
				base.formatString = UINumericFieldsUtils.k_FloatFieldFormatString;
			}

			// Token: 0x170000E5 RID: 229
			// (get) Token: 0x06000638 RID: 1592 RVA: 0x0001CCD0 File Offset: 0x0001AED0
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForFloat;
				}
			}

			// Token: 0x06000639 RID: 1593 RVA: 0x0001D754 File Offset: 0x0001B954
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, float startValue)
			{
				double sensitivity = NumericFieldDraggerUtility.CalculateFloatDragSensitivity((double)startValue);
				float acceleration = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				double v = (double)this.StringToValue(base.text);
				v += (double)NumericFieldDraggerUtility.NiceDelta(delta, acceleration) * sensitivity;
				v = Mathf.RoundBasedOnMinimumDifference(v, sensitivity);
				bool isDelayed = this.parentFloatField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(Mathf.ClampToFloat(v));
				}
				else
				{
					this.parentFloatField.value = Mathf.ClampToFloat(v);
				}
			}

			// Token: 0x0600063A RID: 1594 RVA: 0x0001D7DC File Offset: 0x0001B9DC
			protected override string ValueToString(float v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x0600063B RID: 1595 RVA: 0x0001D7FC File Offset: 0x0001B9FC
			protected override float StringToValue(string str)
			{
				float v;
				UINumericFieldsUtils.TryConvertStringToFloat(str, base.originalText, out v);
				return v;
			}
		}
	}
}
