using System;
using System.Collections.Generic;

namespace UnityEngine.Rendering
{
	// Token: 0x02000069 RID: 105
	public abstract class DebugDisplaySettings<T> : IDebugDisplaySettings where T : IDebugDisplaySettings, new()
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00009540 File Offset: 0x00007740
		public static T Instance
		{
			get
			{
				return DebugDisplaySettings<T>.s_Instance.Value;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x0000954C File Offset: 0x0000774C
		public virtual bool AreAnySettingsActive
		{
			get
			{
				using (HashSet<IDebugDisplaySettingsData>.Enumerator enumerator = this.m_Settings.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.AreAnySettingsActive)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x000095A8 File Offset: 0x000077A8
		public virtual bool IsPostProcessingAllowed
		{
			get
			{
				bool postProcessingAllowed = true;
				foreach (IDebugDisplaySettingsData setting in this.m_Settings)
				{
					postProcessingAllowed &= setting.IsPostProcessingAllowed;
				}
				return postProcessingAllowed;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060004F6 RID: 1270 RVA: 0x00009600 File Offset: 0x00007800
		public virtual bool IsLightingActive
		{
			get
			{
				bool lightingActive = true;
				foreach (IDebugDisplaySettingsData setting in this.m_Settings)
				{
					lightingActive &= setting.IsLightingActive;
				}
				return lightingActive;
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00009658 File Offset: 0x00007858
		protected TData Add<TData>(TData newData) where TData : IDebugDisplaySettingsData
		{
			this.m_Settings.Add(newData);
			return newData;
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000966D File Offset: 0x0000786D
		IDebugDisplaySettingsData IDebugDisplaySettings.Add(IDebugDisplaySettingsData newData)
		{
			this.m_Settings.Add(newData);
			return newData;
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00009680 File Offset: 0x00007880
		public void ForEach(Action<IDebugDisplaySettingsData> onExecute)
		{
			foreach (IDebugDisplaySettingsData setting in this.m_Settings)
			{
				onExecute(setting);
			}
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x000096D4 File Offset: 0x000078D4
		public virtual void Reset()
		{
			this.m_Settings.Clear();
		}

		// Token: 0x060004FB RID: 1275 RVA: 0x000096E4 File Offset: 0x000078E4
		public virtual bool TryGetScreenClearColor(ref Color color)
		{
			using (HashSet<IDebugDisplaySettingsData>.Enumerator enumerator = this.m_Settings.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.TryGetScreenClearColor(ref color))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x04000145 RID: 325
		protected readonly HashSet<IDebugDisplaySettingsData> m_Settings = new HashSet<IDebugDisplaySettingsData>(new DebugDisplaySettings<T>.IDebugDisplaySettingsDataComparer());

		// Token: 0x04000146 RID: 326
		private static readonly Lazy<T> s_Instance = new Lazy<T>(delegate
		{
			T instance = new T();
			instance.Reset();
			return instance;
		});

		// Token: 0x0200006A RID: 106
		private class IDebugDisplaySettingsDataComparer : IEqualityComparer<IDebugDisplaySettingsData>
		{
			// Token: 0x060004FE RID: 1278 RVA: 0x00009774 File Offset: 0x00007974
			public bool Equals(IDebugDisplaySettingsData x, IDebugDisplaySettingsData y)
			{
				return x == y || (x != null && y != null && x.GetType() == y.GetType());
			}

			// Token: 0x060004FF RID: 1279 RVA: 0x00009795 File Offset: 0x00007995
			public int GetHashCode(IDebugDisplaySettingsData obj)
			{
				return 17 * 23 + obj.GetType().GetHashCode();
			}
		}
	}
}
