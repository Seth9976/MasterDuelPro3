using System;
using System.Globalization;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.UIElements
{
	// Token: 0x020000EE RID: 238
	[MovedFrom(true, "UnityEditor.UIElements", "UnityEditor.UIElementsModule", null)]
	public class IntegerField : TextValueField<int>
	{
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x00024363 File Offset: 0x00022563
		private IntegerField.IntegerInput integerInput
		{
			get
			{
				return (IntegerField.IntegerInput)base.textInputBase;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00024370 File Offset: 0x00022570
		protected override string ValueToString(int v)
		{
			return v.ToString(base.formatString, CultureInfo.InvariantCulture.NumberFormat);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0002439C File Offset: 0x0002259C
		protected override int StringToValue(string str)
		{
			int v;
			return UINumericFieldsUtils.TryConvertStringToInt(str, base.textInputBase.originalText, out v) ? v : base.rawValue;
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x000243CE File Offset: 0x000225CE
		public IntegerField()
			: this(null, 1000)
		{
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x000243E0 File Offset: 0x000225E0
		public IntegerField(string label, int maxLength = 1000)
			: base(label, maxLength, new IntegerField.IntegerInput())
		{
			base.AddToClassList(IntegerField.ussClassName);
			base.labelElement.AddToClassList(IntegerField.labelUssClassName);
			base.visualInput.AddToClassList(IntegerField.inputUssClassName);
			base.AddLabelDragger<int>();
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00024434 File Offset: 0x00022634
		internal override bool CanTryParse(string textString)
		{
			int num;
			return int.TryParse(textString, out num);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00024449 File Offset: 0x00022649
		public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, int startValue)
		{
			this.integerInput.ApplyInputDeviceDelta(delta, speed, startValue);
		}

		// Token: 0x040004A7 RID: 1191
		public new static readonly string ussClassName = "unity-integer-field";

		// Token: 0x040004A8 RID: 1192
		public new static readonly string labelUssClassName = IntegerField.ussClassName + "__label";

		// Token: 0x040004A9 RID: 1193
		public new static readonly string inputUssClassName = IntegerField.ussClassName + "__input";

		// Token: 0x020000EF RID: 239
		[Obsolete("UxmlFactory is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlFactory : UxmlFactory<IntegerField, IntegerField.UxmlTraits>
		{
		}

		// Token: 0x020000F0 RID: 240
		[Obsolete("UxmlTraits is deprecated and will be removed. Use UxmlElementAttribute instead.", false)]
		public new class UxmlTraits : TextValueFieldTraits<int, UxmlIntAttributeDescription>
		{
		}

		// Token: 0x020000F1 RID: 241
		private class IntegerInput : TextValueField<int>.TextValueInput
		{
			// Token: 0x17000133 RID: 307
			// (get) Token: 0x06000783 RID: 1923 RVA: 0x000244A1 File Offset: 0x000226A1
			private IntegerField parentIntegerField
			{
				get
				{
					return (IntegerField)base.parent;
				}
			}

			// Token: 0x06000784 RID: 1924 RVA: 0x000244AE File Offset: 0x000226AE
			internal IntegerInput()
			{
				base.formatString = UINumericFieldsUtils.k_IntFieldFormatString;
			}

			// Token: 0x17000134 RID: 308
			// (get) Token: 0x06000785 RID: 1925 RVA: 0x000244C4 File Offset: 0x000226C4
			protected override string allowedCharacters
			{
				get
				{
					return UINumericFieldsUtils.k_AllowedCharactersForInt;
				}
			}

			// Token: 0x06000786 RID: 1926 RVA: 0x000244CC File Offset: 0x000226CC
			public override void ApplyInputDeviceDelta(Vector3 delta, DeltaSpeed speed, int startValue)
			{
				double sensitivity = (double)NumericFieldDraggerUtility.CalculateIntDragSensitivity((long)startValue);
				float acceleration = NumericFieldDraggerUtility.Acceleration(speed == DeltaSpeed.Fast, speed == DeltaSpeed.Slow);
				long v = (long)this.StringToValue(base.text);
				v += (long)Math.Round((double)NumericFieldDraggerUtility.NiceDelta(delta, acceleration) * sensitivity);
				bool isDelayed = this.parentIntegerField.isDelayed;
				if (isDelayed)
				{
					base.text = this.ValueToString(Mathf.ClampToInt(v));
				}
				else
				{
					this.parentIntegerField.value = Mathf.ClampToInt(v);
				}
			}

			// Token: 0x06000787 RID: 1927 RVA: 0x00024554 File Offset: 0x00022754
			protected override string ValueToString(int v)
			{
				return v.ToString(base.formatString);
			}

			// Token: 0x06000788 RID: 1928 RVA: 0x00024574 File Offset: 0x00022774
			protected override int StringToValue(string str)
			{
				int v;
				UINumericFieldsUtils.TryConvertStringToInt(str, base.originalText, out v);
				return v;
			}
		}
	}
}
