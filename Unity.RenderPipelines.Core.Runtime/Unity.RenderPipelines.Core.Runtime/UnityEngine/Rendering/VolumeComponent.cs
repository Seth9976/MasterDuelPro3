using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace UnityEngine.Rendering
{
	// Token: 0x020001EB RID: 491
	[Serializable]
	public class VolumeComponent : ScriptableObject
	{
		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x06000DE4 RID: 3556 RVA: 0x000339E2 File Offset: 0x00031BE2
		// (set) Token: 0x06000DE5 RID: 3557 RVA: 0x000339EA File Offset: 0x00031BEA
		public string displayName { get; protected set; } = "";

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x06000DE6 RID: 3558 RVA: 0x000339F3 File Offset: 0x00031BF3
		public ReadOnlyCollection<VolumeParameter> parameters
		{
			get
			{
				if (this.m_ParameterReadOnlyCollection == null)
				{
					this.m_ParameterReadOnlyCollection = this.parameterList.AsReadOnly();
				}
				return this.m_ParameterReadOnlyCollection;
			}
		}

		// Token: 0x06000DE7 RID: 3559 RVA: 0x00033A14 File Offset: 0x00031C14
		internal static void FindParameters(object o, List<VolumeParameter> parameters, Func<FieldInfo, bool> filter = null)
		{
			if (o == null)
			{
				return;
			}
			foreach (FieldInfo field in from t in o.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
				orderby t.MetadataToken
				select t)
			{
				if (field.FieldType.IsSubclassOf(typeof(VolumeParameter)))
				{
					if (filter == null || filter(field))
					{
						VolumeParameter volumeParameter = (VolumeParameter)field.GetValue(o);
						parameters.Add(volumeParameter);
					}
				}
				else if (!field.FieldType.IsArray && field.FieldType.IsClass)
				{
					VolumeComponent.FindParameters(field.GetValue(o), parameters, filter);
				}
			}
		}

		// Token: 0x06000DE8 RID: 3560 RVA: 0x00033AF0 File Offset: 0x00031CF0
		protected virtual void OnEnable()
		{
			this.parameterList.Clear();
			VolumeComponent.FindParameters(this, this.parameterList, null);
			foreach (VolumeParameter parameter in this.parameterList)
			{
				if (parameter != null)
				{
					parameter.OnEnable();
				}
				else
				{
					Debug.LogWarning("Volume Component " + base.GetType().Name + " contains a null parameter; please make sure all parameters are initialized to a default value. Until this is fixed the null parameters will not be considered by the system.");
				}
			}
		}

		// Token: 0x06000DE9 RID: 3561 RVA: 0x00033B80 File Offset: 0x00031D80
		protected virtual void OnDisable()
		{
			foreach (VolumeParameter parameter in this.parameterList)
			{
				if (parameter != null)
				{
					parameter.OnDisable();
				}
			}
		}

		// Token: 0x06000DEA RID: 3562 RVA: 0x00033BD8 File Offset: 0x00031DD8
		public virtual void Override(VolumeComponent state, float interpFactor)
		{
			int count = this.parameterList.Count;
			for (int i = 0; i < count; i++)
			{
				VolumeParameter stateParam = state.parameterList[i];
				VolumeParameter toParam = this.parameterList[i];
				if (toParam.overrideState)
				{
					stateParam.overrideState = toParam.overrideState;
					stateParam.Interp(stateParam, toParam, interpFactor);
				}
			}
		}

		// Token: 0x06000DEB RID: 3563 RVA: 0x00033C34 File Offset: 0x00031E34
		public void SetAllOverridesTo(bool state)
		{
			this.SetOverridesTo(this.parameterList, state);
		}

		// Token: 0x06000DEC RID: 3564 RVA: 0x00033C44 File Offset: 0x00031E44
		internal void SetOverridesTo(IEnumerable<VolumeParameter> enumerable, bool state)
		{
			foreach (VolumeParameter prop in enumerable)
			{
				prop.overrideState = state;
				Type t = prop.GetType();
				if (VolumeParameter.IsObjectParameter(t))
				{
					ReadOnlyCollection<VolumeParameter> innerParams = (ReadOnlyCollection<VolumeParameter>)t.GetProperty("parameters", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(prop, null);
					if (innerParams != null)
					{
						this.SetOverridesTo(innerParams, state);
					}
				}
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x00033CC0 File Offset: 0x00031EC0
		public override int GetHashCode()
		{
			int hash = 17;
			for (int i = 0; i < this.parameterList.Count; i++)
			{
				hash = hash * 23 + this.parameterList[i].GetHashCode();
			}
			return hash;
		}

		// Token: 0x06000DEE RID: 3566 RVA: 0x00033D00 File Offset: 0x00031F00
		public bool AnyPropertiesIsOverridden()
		{
			for (int i = 0; i < this.parameterList.Count; i++)
			{
				if (this.parameterList[i].overrideState)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000DEF RID: 3567 RVA: 0x00033D39 File Offset: 0x00031F39
		protected virtual void OnDestroy()
		{
			this.Release();
		}

		// Token: 0x06000DF0 RID: 3568 RVA: 0x00033D44 File Offset: 0x00031F44
		public void Release()
		{
			if (this.parameterList == null)
			{
				return;
			}
			for (int i = 0; i < this.parameterList.Count; i++)
			{
				if (this.parameterList[i] != null)
				{
					this.parameterList[i].Release();
				}
			}
		}

		// Token: 0x04000941 RID: 2369
		public bool active = true;

		// Token: 0x04000943 RID: 2371
		internal readonly List<VolumeParameter> parameterList = new List<VolumeParameter>();

		// Token: 0x04000944 RID: 2372
		private ReadOnlyCollection<VolumeParameter> m_ParameterReadOnlyCollection;

		// Token: 0x020001EC RID: 492
		public sealed class Indent : PropertyAttribute
		{
			// Token: 0x06000DF2 RID: 3570 RVA: 0x00033DB4 File Offset: 0x00031FB4
			public Indent(int relativeAmount = 1)
			{
				this.relativeAmount = relativeAmount;
			}

			// Token: 0x04000945 RID: 2373
			public readonly int relativeAmount;
		}
	}
}
