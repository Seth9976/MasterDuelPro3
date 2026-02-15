using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000313 RID: 787
	public struct ScriptPlayable<T> : IPlayable, IEquatable<ScriptPlayable<T>> where T : class, IPlayableBehaviour, new()
	{
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060015E8 RID: 5608 RVA: 0x0002DDA8 File Offset: 0x0002BFA8
		public static ScriptPlayable<T> Null
		{
			get
			{
				return ScriptPlayable<T>.m_NullPlayable;
			}
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x0002DDC0 File Offset: 0x0002BFC0
		public static ScriptPlayable<T> Create(PlayableGraph graph, int inputCount = 0)
		{
			PlayableHandle handle = ScriptPlayable<T>.CreateHandle(graph, default(T), inputCount);
			return new ScriptPlayable<T>(handle);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x0002DDEC File Offset: 0x0002BFEC
		public static ScriptPlayable<T> Create(PlayableGraph graph, T template, int inputCount = 0)
		{
			PlayableHandle handle = ScriptPlayable<T>.CreateHandle(graph, template, inputCount);
			return new ScriptPlayable<T>(handle);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x0002DE10 File Offset: 0x0002C010
		private static PlayableHandle CreateHandle(PlayableGraph graph, T template, int inputCount)
		{
			bool flag = template == null;
			object scriptInstance;
			if (flag)
			{
				scriptInstance = ScriptPlayable<T>.CreateScriptInstance();
			}
			else
			{
				scriptInstance = ScriptPlayable<T>.CloneScriptInstance(template);
			}
			bool flag2 = scriptInstance == null;
			PlayableHandle playableHandle;
			if (flag2)
			{
				string text = "Could not create a ScriptPlayable of Type ";
				Type typeFromHandle = typeof(T);
				Debug.LogError(text + ((typeFromHandle != null) ? typeFromHandle.ToString() : null));
				playableHandle = PlayableHandle.Null;
			}
			else
			{
				PlayableHandle handle = graph.CreatePlayableHandle();
				bool flag3 = !handle.IsValid();
				if (flag3)
				{
					playableHandle = PlayableHandle.Null;
				}
				else
				{
					handle.SetInputCount(inputCount);
					handle.SetScriptInstance(scriptInstance);
					playableHandle = handle;
				}
			}
			return playableHandle;
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x0002DEB8 File Offset: 0x0002C0B8
		private static object CreateScriptInstance()
		{
			bool flag = typeof(ScriptableObject).IsAssignableFrom(typeof(T));
			IPlayableBehaviour data;
			if (flag)
			{
				data = ScriptableObject.CreateInstance(typeof(T)) as T;
			}
			else
			{
				data = new T();
			}
			return data;
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x0002DF18 File Offset: 0x0002C118
		private static object CloneScriptInstance(IPlayableBehaviour source)
		{
			Object engineObject = source as Object;
			bool flag = engineObject != null;
			object obj;
			if (flag)
			{
				obj = ScriptPlayable<T>.CloneScriptInstanceFromEngineObject(engineObject);
			}
			else
			{
				ICloneable cloneableObject = source as ICloneable;
				bool flag2 = cloneableObject != null;
				if (flag2)
				{
					obj = ScriptPlayable<T>.CloneScriptInstanceFromIClonable(cloneableObject);
				}
				else
				{
					obj = null;
				}
			}
			return obj;
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x0002DF60 File Offset: 0x0002C160
		private static object CloneScriptInstanceFromEngineObject(Object source)
		{
			Object scriptPlayable = Object.Instantiate(source);
			bool flag = scriptPlayable != null;
			if (flag)
			{
				scriptPlayable.hideFlags |= HideFlags.DontSave;
			}
			return scriptPlayable;
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x0002DF98 File Offset: 0x0002C198
		private static object CloneScriptInstanceFromIClonable(ICloneable source)
		{
			return source.Clone();
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0002DFB0 File Offset: 0x0002C1B0
		internal ScriptPlayable(PlayableHandle handle)
		{
			bool flag = handle.IsValid();
			if (flag)
			{
				bool flag2 = !typeof(T).IsAssignableFrom(handle.GetPlayableType());
				if (flag2)
				{
					throw new InvalidCastException(string.Format("Incompatible handle: Trying to assign a playable data of type `{0}` that is not compatible with the PlayableBehaviour of type `{1}`.", handle.GetPlayableType(), typeof(T)));
				}
			}
			this.m_Handle = handle;
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x0002E010 File Offset: 0x0002C210
		public PlayableHandle GetHandle()
		{
			return this.m_Handle;
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x0002E028 File Offset: 0x0002C228
		public T GetBehaviour()
		{
			return this.m_Handle.GetObject<T>();
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x0002E048 File Offset: 0x0002C248
		public static implicit operator Playable(ScriptPlayable<T> playable)
		{
			return new Playable(playable.GetHandle());
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x0002E068 File Offset: 0x0002C268
		public static explicit operator ScriptPlayable<T>(Playable playable)
		{
			return new ScriptPlayable<T>(playable.GetHandle());
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x0002E088 File Offset: 0x0002C288
		public bool Equals(ScriptPlayable<T> other)
		{
			return this.GetHandle() == other.GetHandle();
		}

		// Token: 0x0400082F RID: 2095
		private PlayableHandle m_Handle;

		// Token: 0x04000830 RID: 2096
		private static readonly ScriptPlayable<T> m_NullPlayable = new ScriptPlayable<T>(PlayableHandle.Null);
	}
}
