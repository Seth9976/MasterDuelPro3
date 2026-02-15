using System;
using DG.Tweening.Core.Enums;
using DG.Tweening.Plugins.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Core
{
	// Token: 0x020000B8 RID: 184
	public class TweenerCore<T1, T2, TPlugOptions> : Tweener where TPlugOptions : struct, IPlugOptions
	{
		// Token: 0x06000458 RID: 1112 RVA: 0x000133A4 File Offset: 0x000115A4
		internal TweenerCore()
		{
			this.typeofT1 = typeof(T1);
			this.typeofT2 = typeof(T2);
			this.typeofTPlugOptions = typeof(TPlugOptions);
			this.tweenType = TweenType.Tweener;
			this.Reset();
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x00013414 File Offset: 0x00011614
		public override Tweener ChangeStartValue(object newStartValue, float newDuration = -1f)
		{
			if (this.isSequenced)
			{
				Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			Type type = newStartValue.GetType();
			bool flag;
			if (!this.ValidateChangeValueType(type, out flag))
			{
				string[] array = new string[5];
				array[0] = "ChangeStartValue: incorrect newStartValue type (is ";
				int num = 1;
				Type type2 = type;
				array[num] = ((type2 != null) ? type2.ToString() : null);
				array[2] = ", should be ";
				int num2 = 3;
				Type typeofT = this.typeofT2;
				array[num2] = ((typeofT != null) ? typeofT.ToString() : null);
				array[4] = ")";
				Debugger.LogError(string.Concat(array), this);
				return this;
			}
			if (flag)
			{
				return Tweener.DoChangeStartValue<T1, T2, TPlugOptions>(this, (T2)((object)(Color32)newStartValue), newDuration);
			}
			return Tweener.DoChangeStartValue<T1, T2, TPlugOptions>(this, (T2)((object)newStartValue), newDuration);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000134C5 File Offset: 0x000116C5
		public override Tweener ChangeEndValue(object newEndValue, bool snapStartValue)
		{
			return this.ChangeEndValue(newEndValue, -1f, snapStartValue);
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000134D4 File Offset: 0x000116D4
		public override Tweener ChangeEndValue(object newEndValue, float newDuration = -1f, bool snapStartValue = false)
		{
			if (this.isSequenced)
			{
				Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			Type type = newEndValue.GetType();
			bool flag;
			if (!this.ValidateChangeValueType(type, out flag))
			{
				string[] array = new string[5];
				array[0] = "ChangeEndValue: incorrect newEndValue type (is ";
				int num = 1;
				Type type2 = type;
				array[num] = ((type2 != null) ? type2.ToString() : null);
				array[2] = ", should be ";
				int num2 = 3;
				Type typeofT = this.typeofT2;
				array[num2] = ((typeofT != null) ? typeofT.ToString() : null);
				array[4] = ")";
				Debugger.LogError(string.Concat(array), this);
				return this;
			}
			if (flag)
			{
				return Tweener.DoChangeEndValue<T1, T2, TPlugOptions>(this, (T2)((object)(Color32)newEndValue), newDuration, snapStartValue);
			}
			return Tweener.DoChangeEndValue<T1, T2, TPlugOptions>(this, (T2)((object)newEndValue), newDuration, snapStartValue);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x00013588 File Offset: 0x00011788
		public override Tweener ChangeValues(object newStartValue, object newEndValue, float newDuration = -1f)
		{
			if (this.isSequenced)
			{
				Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			Type type = newStartValue.GetType();
			Type type2 = newEndValue.GetType();
			bool flag;
			if (!this.ValidateChangeValueType(type, out flag))
			{
				string[] array = new string[5];
				array[0] = "ChangeValues: incorrect value type (is ";
				int num = 1;
				Type type3 = type;
				array[num] = ((type3 != null) ? type3.ToString() : null);
				array[2] = ", should be ";
				int num2 = 3;
				Type typeofT = this.typeofT2;
				array[num2] = ((typeofT != null) ? typeofT.ToString() : null);
				array[4] = ")";
				Debugger.LogError(string.Concat(array), this);
				return this;
			}
			if (!this.ValidateChangeValueType(type2, out flag))
			{
				string[] array2 = new string[5];
				array2[0] = "ChangeValues: incorrect value type (is ";
				int num3 = 1;
				Type type4 = type2;
				array2[num3] = ((type4 != null) ? type4.ToString() : null);
				array2[2] = ", should be ";
				int num4 = 3;
				Type typeofT2 = this.typeofT2;
				array2[num4] = ((typeofT2 != null) ? typeofT2.ToString() : null);
				array2[4] = ")";
				Debugger.LogError(string.Concat(array2), this);
				return this;
			}
			if (flag)
			{
				return Tweener.DoChangeValues<T1, T2, TPlugOptions>(this, (T2)((object)(Color32)newStartValue), (T2)((object)(Color32)newEndValue), newDuration);
			}
			return Tweener.DoChangeValues<T1, T2, TPlugOptions>(this, (T2)((object)newStartValue), (T2)((object)newEndValue), newDuration);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000136B6 File Offset: 0x000118B6
		public TweenerCore<T1, T2, TPlugOptions> ChangeStartValue(T2 newStartValue, float newDuration = -1f)
		{
			if (this.isSequenced)
			{
				Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			return Tweener.DoChangeStartValue<T1, T2, TPlugOptions>(this, newStartValue, newDuration);
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000136D5 File Offset: 0x000118D5
		public TweenerCore<T1, T2, TPlugOptions> ChangeEndValue(T2 newEndValue, bool snapStartValue)
		{
			return this.ChangeEndValue(newEndValue, -1f, snapStartValue);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000136E4 File Offset: 0x000118E4
		public TweenerCore<T1, T2, TPlugOptions> ChangeEndValue(T2 newEndValue, float newDuration = -1f, bool snapStartValue = false)
		{
			if (this.isSequenced)
			{
				Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			return Tweener.DoChangeEndValue<T1, T2, TPlugOptions>(this, newEndValue, newDuration, snapStartValue);
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00013704 File Offset: 0x00011904
		public TweenerCore<T1, T2, TPlugOptions> ChangeValues(T2 newStartValue, T2 newEndValue, float newDuration = -1f)
		{
			if (this.isSequenced)
			{
				Debugger.LogError("You cannot change the values of a tween contained inside a Sequence", this);
				return this;
			}
			return Tweener.DoChangeValues<T1, T2, TPlugOptions>(this, newStartValue, newEndValue, newDuration);
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x00013724 File Offset: 0x00011924
		internal override Tweener SetFrom(bool relative)
		{
			this.tweenPlugin.SetFrom(this, relative);
			this.hasManuallySetStartValue = true;
			return this;
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x0001373B File Offset: 0x0001193B
		internal Tweener SetFrom(T2 fromValue, bool setImmediately, bool relative)
		{
			this.tweenPlugin.SetFrom(this, fromValue, setImmediately, relative);
			this.hasManuallySetStartValue = true;
			return this;
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00013754 File Offset: 0x00011954
		internal sealed override void Reset()
		{
			base.Reset();
			if (this.tweenPlugin != null)
			{
				this.tweenPlugin.Reset(this);
			}
			this.plugOptions.Reset();
			this.getter = null;
			this.setter = null;
			this.hasManuallySetStartValue = false;
			this.isFromAllowed = true;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000137A8 File Offset: 0x000119A8
		internal override bool Validate()
		{
			try
			{
				this.getter();
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000137DC File Offset: 0x000119DC
		private bool ValidateChangeValueType(Type newType, out bool isColor32ToColor)
		{
			if (newType == this.typeofT2)
			{
				isColor32ToColor = false;
				return true;
			}
			if (this.typeofT2 == this._colorType && newType == this._color32Type)
			{
				isColor32ToColor = true;
				return true;
			}
			isColor32ToColor = false;
			return false;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0001380C File Offset: 0x00011A0C
		internal override float UpdateDelay(float elapsed)
		{
			return Tweener.DoUpdateDelay<T1, T2, TPlugOptions>(this, elapsed);
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00013815 File Offset: 0x00011A15
		internal override bool Startup()
		{
			return Tweener.DoStartup<T1, T2, TPlugOptions>(this);
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x00013820 File Offset: 0x00011A20
		internal override bool ApplyTween(float prevPosition, int prevCompletedLoops, int newCompletedSteps, bool useInversePosition, UpdateMode updateMode, UpdateNotice updateNotice)
		{
			if (this.isInverted)
			{
				useInversePosition = !useInversePosition;
			}
			float num = (useInversePosition ? (this.duration - base.position) : base.position);
			if (DOTween.useSafeMode)
			{
				try
				{
					this.tweenPlugin.EvaluateAndApply(this.plugOptions, this, base.isRelative, this.getter, this.setter, num, this.startValue, this.changeValue, this.duration, useInversePosition, newCompletedSteps, updateNotice);
					return false;
				}
				catch (Exception ex)
				{
					if (Debugger.ShouldLogSafeModeCapturedError())
					{
						Debugger.LogSafeModeCapturedError(string.Format("Target or field is missing/null ({0}) ► {1}\n\n{2}\n\n", ex.TargetSite, ex.Message, ex.StackTrace), this);
					}
					DOTween.safeModeReport.Add(SafeModeReport.SafeModeReportType.TargetOrFieldMissing);
					return true;
				}
			}
			this.tweenPlugin.EvaluateAndApply(this.plugOptions, this, base.isRelative, this.getter, this.setter, num, this.startValue, this.changeValue, this.duration, useInversePosition, newCompletedSteps, updateNotice);
			return false;
		}

		// Token: 0x0400024C RID: 588
		public T2 startValue;

		// Token: 0x0400024D RID: 589
		public T2 endValue;

		// Token: 0x0400024E RID: 590
		public T2 changeValue;

		// Token: 0x0400024F RID: 591
		public TPlugOptions plugOptions;

		// Token: 0x04000250 RID: 592
		public DOGetter<T1> getter;

		// Token: 0x04000251 RID: 593
		public DOSetter<T1> setter;

		// Token: 0x04000252 RID: 594
		internal ABSTweenPlugin<T1, T2, TPlugOptions> tweenPlugin;

		// Token: 0x04000253 RID: 595
		private const string _TxtCantChangeSequencedValues = "You cannot change the values of a tween contained inside a Sequence";

		// Token: 0x04000254 RID: 596
		private Type _colorType = typeof(Color);

		// Token: 0x04000255 RID: 597
		private Type _color32Type = typeof(Color32);
	}
}
