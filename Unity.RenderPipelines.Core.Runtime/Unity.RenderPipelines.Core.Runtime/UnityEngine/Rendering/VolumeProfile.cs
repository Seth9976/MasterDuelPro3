using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200021C RID: 540
	public sealed class VolumeProfile : ScriptableObject
	{
		// Token: 0x06000E7B RID: 3707 RVA: 0x000349F0 File Offset: 0x00032BF0
		private void OnEnable()
		{
			this.components.RemoveAll((VolumeComponent x) => x == null);
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x00034A20 File Offset: 0x00032C20
		internal void OnDisable()
		{
			if (this.components == null)
			{
				return;
			}
			for (int i = 0; i < this.components.Count; i++)
			{
				if (this.components[i] != null)
				{
					this.components[i].Release();
				}
			}
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x00034A71 File Offset: 0x00032C71
		public void Reset()
		{
			this.isDirty = true;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x00034A7A File Offset: 0x00032C7A
		public T Add<T>(bool overrides = false) where T : VolumeComponent
		{
			return (T)((object)this.Add(typeof(T), overrides));
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00034A94 File Offset: 0x00032C94
		public VolumeComponent Add(Type type, bool overrides = false)
		{
			if (this.Has(type))
			{
				throw new InvalidOperationException("Component already exists in the volume");
			}
			VolumeComponent component = (VolumeComponent)ScriptableObject.CreateInstance(type);
			component.SetAllOverridesTo(overrides);
			this.components.Add(component);
			this.isDirty = true;
			return component;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00034ADC File Offset: 0x00032CDC
		public void Remove<T>() where T : VolumeComponent
		{
			this.Remove(typeof(T));
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x00034AF0 File Offset: 0x00032CF0
		public void Remove(Type type)
		{
			int toRemove = -1;
			for (int i = 0; i < this.components.Count; i++)
			{
				if (this.components[i].GetType() == type)
				{
					toRemove = i;
					break;
				}
			}
			if (toRemove >= 0)
			{
				this.components.RemoveAt(toRemove);
				this.isDirty = true;
			}
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x00034B49 File Offset: 0x00032D49
		public bool Has<T>() where T : VolumeComponent
		{
			return this.Has(typeof(T));
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x00034B5C File Offset: 0x00032D5C
		public bool Has(Type type)
		{
			using (List<VolumeComponent>.Enumerator enumerator = this.components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType() == type)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x00034BBC File Offset: 0x00032DBC
		public bool HasSubclassOf(Type type)
		{
			using (List<VolumeComponent>.Enumerator enumerator = this.components.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.GetType().IsSubclassOf(type))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x00034C1C File Offset: 0x00032E1C
		public bool TryGet<T>(out T component) where T : VolumeComponent
		{
			return this.TryGet<T>(typeof(T), out component);
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x00034C30 File Offset: 0x00032E30
		public bool TryGet<T>(Type type, out T component) where T : VolumeComponent
		{
			component = default(T);
			foreach (VolumeComponent comp in this.components)
			{
				if (comp.GetType() == type)
				{
					component = (T)((object)comp);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x00034CA4 File Offset: 0x00032EA4
		public bool TryGetSubclassOf<T>(Type type, out T component) where T : VolumeComponent
		{
			component = default(T);
			foreach (VolumeComponent comp in this.components)
			{
				if (comp.GetType().IsSubclassOf(type))
				{
					component = (T)((object)comp);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x00034D18 File Offset: 0x00032F18
		public bool TryGetAllSubclassOf<T>(Type type, List<T> result) where T : VolumeComponent
		{
			int count = result.Count;
			foreach (VolumeComponent comp in this.components)
			{
				if (comp.GetType().IsSubclassOf(type))
				{
					result.Add((T)((object)comp));
				}
			}
			return count != result.Count;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x00034D94 File Offset: 0x00032F94
		public override int GetHashCode()
		{
			int hash = 17;
			for (int i = 0; i < this.components.Count; i++)
			{
				hash = hash * 23 + this.components[i].GetHashCode();
			}
			return hash;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x00034DD4 File Offset: 0x00032FD4
		internal int GetComponentListHashCode()
		{
			int hash = 17;
			for (int i = 0; i < this.components.Count; i++)
			{
				hash = hash * 23 + this.components[i].GetType().GetHashCode();
			}
			return hash;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x00034E18 File Offset: 0x00033018
		internal void Sanitize()
		{
			for (int i = this.components.Count - 1; i >= 0; i--)
			{
				if (this.components[i] == null)
				{
					this.components.RemoveAt(i);
				}
			}
		}

		// Token: 0x0400096E RID: 2414
		public List<VolumeComponent> components = new List<VolumeComponent>();

		// Token: 0x0400096F RID: 2415
		[NonSerialized]
		public bool isDirty = true;
	}
}
