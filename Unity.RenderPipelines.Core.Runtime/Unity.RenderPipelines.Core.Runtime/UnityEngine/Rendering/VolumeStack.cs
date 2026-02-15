using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x0200021E RID: 542
	public sealed class VolumeStack : IDisposable
	{
		// Token: 0x06000E90 RID: 3728 RVA: 0x00034E83 File Offset: 0x00033083
		internal VolumeStack()
		{
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x00034EA4 File Offset: 0x000330A4
		internal void Clear()
		{
			foreach (KeyValuePair<Type, VolumeComponent> component in this.components)
			{
				CoreUtils.Destroy(component.Value);
			}
			this.components.Clear();
			this.parameters = null;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x00034F10 File Offset: 0x00033110
		internal void Reload(Type[] componentTypes)
		{
			this.Clear();
			this.requiresReset = true;
			this.requiresResetForAllProperties = true;
			List<VolumeParameter> parametersList = new List<VolumeParameter>();
			foreach (Type type in componentTypes)
			{
				VolumeComponent component = (VolumeComponent)ScriptableObject.CreateInstance(type);
				this.components.Add(type, component);
				parametersList.AddRange(component.parameters);
			}
			this.parameters = parametersList.ToArray();
			this.isValid = true;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x00034F86 File Offset: 0x00033186
		public T GetComponent<T>() where T : VolumeComponent
		{
			return (T)((object)this.GetComponent(typeof(T)));
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x00034FA0 File Offset: 0x000331A0
		public VolumeComponent GetComponent(Type type)
		{
			VolumeComponent comp;
			this.components.TryGetValue(type, out comp);
			return comp;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x00034FBD File Offset: 0x000331BD
		public void Dispose()
		{
			this.Clear();
			this.isValid = false;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x00034FCC File Offset: 0x000331CC
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x00034FD4 File Offset: 0x000331D4
		public bool isValid { get; private set; }

		// Token: 0x04000972 RID: 2418
		internal readonly Dictionary<Type, VolumeComponent> components = new Dictionary<Type, VolumeComponent>();

		// Token: 0x04000973 RID: 2419
		internal VolumeParameter[] parameters;

		// Token: 0x04000974 RID: 2420
		internal bool requiresReset = true;

		// Token: 0x04000975 RID: 2421
		internal bool requiresResetForAllProperties = true;
	}
}
