using System;
using System.Collections.Generic;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace DG.Tweening.Plugins.Core
{
	// Token: 0x02000097 RID: 151
	internal static class PluginsManager
	{
		// Token: 0x0600037E RID: 894 RVA: 0x0000EE5C File Offset: 0x0000D05C
		internal static ABSTweenPlugin<T1, T2, TPlugOptions> GetDefaultPlugin<T1, T2, TPlugOptions>() where TPlugOptions : struct, IPlugOptions
		{
			Type typeFromHandle = typeof(T1);
			Type typeFromHandle2 = typeof(T2);
			ITweenPlugin tweenPlugin = null;
			if (typeFromHandle == typeof(Vector3) && typeFromHandle == typeFromHandle2)
			{
				if (PluginsManager._vector3Plugin == null)
				{
					PluginsManager._vector3Plugin = new Vector3Plugin();
				}
				tweenPlugin = PluginsManager._vector3Plugin;
			}
			else if (typeFromHandle == typeof(Vector3) && typeFromHandle2 == typeof(Vector3[]))
			{
				if (PluginsManager._vector3ArrayPlugin == null)
				{
					PluginsManager._vector3ArrayPlugin = new Vector3ArrayPlugin();
				}
				tweenPlugin = PluginsManager._vector3ArrayPlugin;
			}
			else if (typeFromHandle == typeof(Quaternion))
			{
				if (typeFromHandle2 == typeof(Quaternion))
				{
					Debugger.LogError("Quaternion tweens require a Vector3 endValue", null);
				}
				else
				{
					if (PluginsManager._quaternionPlugin == null)
					{
						PluginsManager._quaternionPlugin = new QuaternionPlugin();
					}
					tweenPlugin = PluginsManager._quaternionPlugin;
				}
			}
			else if (typeFromHandle == typeof(Vector2))
			{
				if (PluginsManager._vector2Plugin == null)
				{
					PluginsManager._vector2Plugin = new Vector2Plugin();
				}
				tweenPlugin = PluginsManager._vector2Plugin;
			}
			else if (typeFromHandle == typeof(float))
			{
				if (PluginsManager._floatPlugin == null)
				{
					PluginsManager._floatPlugin = new FloatPlugin();
				}
				tweenPlugin = PluginsManager._floatPlugin;
			}
			else if (typeFromHandle == typeof(Color))
			{
				if (PluginsManager._colorPlugin == null)
				{
					PluginsManager._colorPlugin = new ColorPlugin();
				}
				tweenPlugin = PluginsManager._colorPlugin;
			}
			else if (typeFromHandle == typeof(int))
			{
				if (PluginsManager._intPlugin == null)
				{
					PluginsManager._intPlugin = new IntPlugin();
				}
				tweenPlugin = PluginsManager._intPlugin;
			}
			else if (typeFromHandle == typeof(Vector4))
			{
				if (PluginsManager._vector4Plugin == null)
				{
					PluginsManager._vector4Plugin = new Vector4Plugin();
				}
				tweenPlugin = PluginsManager._vector4Plugin;
			}
			else if (typeFromHandle == typeof(Rect))
			{
				if (PluginsManager._rectPlugin == null)
				{
					PluginsManager._rectPlugin = new RectPlugin();
				}
				tweenPlugin = PluginsManager._rectPlugin;
			}
			else if (typeFromHandle == typeof(RectOffset))
			{
				if (PluginsManager._rectOffsetPlugin == null)
				{
					PluginsManager._rectOffsetPlugin = new RectOffsetPlugin();
				}
				tweenPlugin = PluginsManager._rectOffsetPlugin;
			}
			else if (typeFromHandle == typeof(uint))
			{
				if (PluginsManager._uintPlugin == null)
				{
					PluginsManager._uintPlugin = new UintPlugin();
				}
				tweenPlugin = PluginsManager._uintPlugin;
			}
			else if (typeFromHandle == typeof(string))
			{
				if (PluginsManager._stringPlugin == null)
				{
					PluginsManager._stringPlugin = new StringPlugin();
				}
				tweenPlugin = PluginsManager._stringPlugin;
			}
			else if (typeFromHandle == typeof(Color2))
			{
				if (PluginsManager._color2Plugin == null)
				{
					PluginsManager._color2Plugin = new Color2Plugin();
				}
				tweenPlugin = PluginsManager._color2Plugin;
			}
			else if (typeFromHandle == typeof(long))
			{
				if (PluginsManager._longPlugin == null)
				{
					PluginsManager._longPlugin = new LongPlugin();
				}
				tweenPlugin = PluginsManager._longPlugin;
			}
			else if (typeFromHandle == typeof(ulong))
			{
				if (PluginsManager._ulongPlugin == null)
				{
					PluginsManager._ulongPlugin = new UlongPlugin();
				}
				tweenPlugin = PluginsManager._ulongPlugin;
			}
			else if (typeFromHandle == typeof(double))
			{
				if (PluginsManager._doublePlugin == null)
				{
					PluginsManager._doublePlugin = new DoublePlugin();
				}
				tweenPlugin = PluginsManager._doublePlugin;
			}
			if (tweenPlugin != null)
			{
				return tweenPlugin as ABSTweenPlugin<T1, T2, TPlugOptions>;
			}
			return null;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0000F13C File Offset: 0x0000D33C
		public static ABSTweenPlugin<T1, T2, TPlugOptions> GetCustomPlugin<TPlugin, T1, T2, TPlugOptions>() where TPlugin : ITweenPlugin, new() where TPlugOptions : struct, IPlugOptions
		{
			Type typeFromHandle = typeof(TPlugin);
			ITweenPlugin tweenPlugin;
			if (PluginsManager._customPlugins == null)
			{
				PluginsManager._customPlugins = new Dictionary<Type, ITweenPlugin>(20);
			}
			else if (PluginsManager._customPlugins.TryGetValue(typeFromHandle, ref tweenPlugin))
			{
				return tweenPlugin as ABSTweenPlugin<T1, T2, TPlugOptions>;
			}
			tweenPlugin = new TPlugin();
			PluginsManager._customPlugins.Add(typeFromHandle, tweenPlugin);
			return tweenPlugin as ABSTweenPlugin<T1, T2, TPlugOptions>;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0000F19C File Offset: 0x0000D39C
		internal static void PurgeAll()
		{
			PluginsManager._floatPlugin = null;
			PluginsManager._intPlugin = null;
			PluginsManager._uintPlugin = null;
			PluginsManager._longPlugin = null;
			PluginsManager._ulongPlugin = null;
			PluginsManager._vector2Plugin = null;
			PluginsManager._vector3Plugin = null;
			PluginsManager._vector4Plugin = null;
			PluginsManager._quaternionPlugin = null;
			PluginsManager._colorPlugin = null;
			PluginsManager._rectPlugin = null;
			PluginsManager._rectOffsetPlugin = null;
			PluginsManager._stringPlugin = null;
			PluginsManager._vector3ArrayPlugin = null;
			PluginsManager._color2Plugin = null;
			if (PluginsManager._customPlugins != null)
			{
				PluginsManager._customPlugins.Clear();
			}
		}

		// Token: 0x04000192 RID: 402
		private static ITweenPlugin _floatPlugin;

		// Token: 0x04000193 RID: 403
		private static ITweenPlugin _doublePlugin;

		// Token: 0x04000194 RID: 404
		private static ITweenPlugin _intPlugin;

		// Token: 0x04000195 RID: 405
		private static ITweenPlugin _uintPlugin;

		// Token: 0x04000196 RID: 406
		private static ITweenPlugin _longPlugin;

		// Token: 0x04000197 RID: 407
		private static ITweenPlugin _ulongPlugin;

		// Token: 0x04000198 RID: 408
		private static ITweenPlugin _vector2Plugin;

		// Token: 0x04000199 RID: 409
		private static ITweenPlugin _vector3Plugin;

		// Token: 0x0400019A RID: 410
		private static ITweenPlugin _vector4Plugin;

		// Token: 0x0400019B RID: 411
		private static ITweenPlugin _quaternionPlugin;

		// Token: 0x0400019C RID: 412
		private static ITweenPlugin _colorPlugin;

		// Token: 0x0400019D RID: 413
		private static ITweenPlugin _rectPlugin;

		// Token: 0x0400019E RID: 414
		private static ITweenPlugin _rectOffsetPlugin;

		// Token: 0x0400019F RID: 415
		private static ITweenPlugin _stringPlugin;

		// Token: 0x040001A0 RID: 416
		private static ITweenPlugin _vector3ArrayPlugin;

		// Token: 0x040001A1 RID: 417
		private static ITweenPlugin _color2Plugin;

		// Token: 0x040001A2 RID: 418
		private const int _MaxCustomPlugins = 20;

		// Token: 0x040001A3 RID: 419
		private static Dictionary<Type, ITweenPlugin> _customPlugins;
	}
}
