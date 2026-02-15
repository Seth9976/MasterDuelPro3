using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace UnityEngine.Rendering
{
	// Token: 0x02000218 RID: 536
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ObjectParameter<T> : VolumeParameter<T>
	{
		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x000347DA File Offset: 0x000329DA
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x000347E2 File Offset: 0x000329E2
		internal ReadOnlyCollection<VolumeParameter> parameters { get; private set; }

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x000104EC File Offset: 0x0000E6EC
		// (set) Token: 0x06000E6B RID: 3691 RVA: 0x000347EB File Offset: 0x000329EB
		public sealed override bool overrideState
		{
			get
			{
				return true;
			}
			set
			{
				this.m_OverrideState = true;
			}
		}

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00033E2B File Offset: 0x0003202B
		// (set) Token: 0x06000E6D RID: 3693 RVA: 0x000347F4 File Offset: 0x000329F4
		public sealed override T value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
				if (this.m_Value == null)
				{
					this.parameters = null;
					return;
				}
				this.parameters = (from t in this.m_Value.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public)
					where t.FieldType.IsSubclassOf(typeof(VolumeParameter))
					orderby t.MetadataToken
					select (VolumeParameter)t.GetValue(this.m_Value)).ToList<VolumeParameter>().AsReadOnly();
			}
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x0003489E File Offset: 0x00032A9E
		public ObjectParameter(T value)
		{
			this.m_OverrideState = true;
			this.value = value;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x000348B4 File Offset: 0x00032AB4
		internal override void Interp(VolumeParameter from, VolumeParameter to, float t)
		{
			if (this.m_Value == null)
			{
				return;
			}
			ReadOnlyCollection<VolumeParameter> paramOrigin = this.parameters;
			ReadOnlyCollection<VolumeParameter> paramFrom = ((ObjectParameter<T>)from).parameters;
			ReadOnlyCollection<VolumeParameter> paramTo = ((ObjectParameter<T>)to).parameters;
			for (int i = 0; i < paramFrom.Count; i++)
			{
				paramOrigin[i].overrideState = paramTo[i].overrideState;
				if (paramTo[i].overrideState)
				{
					paramOrigin[i].Interp(paramFrom[i], paramTo[i], t);
				}
			}
		}
	}
}
