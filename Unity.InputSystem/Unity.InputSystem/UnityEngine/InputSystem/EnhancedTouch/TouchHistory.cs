using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem.LowLevel;

namespace UnityEngine.InputSystem.EnhancedTouch
{
	// Token: 0x02000154 RID: 340
	public struct TouchHistory : IReadOnlyList<Touch>, IEnumerable<Touch>, IEnumerable, IReadOnlyCollection<Touch>
	{
		// Token: 0x06000ED7 RID: 3799 RVA: 0x0004B808 File Offset: 0x00049A08
		internal TouchHistory(Finger finger, InputStateHistory<TouchState> history, int startIndex = -1, int count = -1)
		{
			this.m_Finger = finger;
			this.m_History = history;
			this.m_Version = history.version;
			this.m_Count = ((count >= 0) ? count : this.m_History.Count);
			this.m_StartIndex = ((startIndex >= 0) ? startIndex : (this.m_History.Count - 1));
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0004B863 File Offset: 0x00049A63
		public IEnumerator<Touch> GetEnumerator()
		{
			return new TouchHistory.Enumerator(this);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0004B870 File Offset: 0x00049A70
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000EDA RID: 3802 RVA: 0x0004B878 File Offset: 0x00049A78
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x170003FE RID: 1022
		public Touch this[int index]
		{
			get
			{
				this.CheckValid();
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException(string.Format("Index {0} is out of range for history with {1} entries", index, this.Count), "index");
				}
				return new Touch(this.m_Finger, this.m_History[this.m_StartIndex - index]);
			}
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0004B8E4 File Offset: 0x00049AE4
		internal void CheckValid()
		{
			if (this.m_Finger == null || this.m_History == null)
			{
				throw new InvalidOperationException("Touch history not initialized");
			}
			if (this.m_History.version != this.m_Version)
			{
				throw new InvalidOperationException("Touch history is no longer valid; the recorded history has been changed");
			}
		}

		// Token: 0x04000875 RID: 2165
		private readonly InputStateHistory<TouchState> m_History;

		// Token: 0x04000876 RID: 2166
		private readonly Finger m_Finger;

		// Token: 0x04000877 RID: 2167
		private readonly int m_Count;

		// Token: 0x04000878 RID: 2168
		private readonly int m_StartIndex;

		// Token: 0x04000879 RID: 2169
		private readonly uint m_Version;

		// Token: 0x02000155 RID: 341
		private class Enumerator : IEnumerator<Touch>, IEnumerator, IDisposable
		{
			// Token: 0x06000EDD RID: 3805 RVA: 0x0004B91F File Offset: 0x00049B1F
			internal Enumerator(TouchHistory owner)
			{
				this.m_Owner = owner;
				this.m_Index = -1;
			}

			// Token: 0x06000EDE RID: 3806 RVA: 0x0004B938 File Offset: 0x00049B38
			public bool MoveNext()
			{
				if (this.m_Index >= this.m_Owner.Count - 1)
				{
					return false;
				}
				this.m_Index++;
				return true;
			}

			// Token: 0x06000EDF RID: 3807 RVA: 0x0004B96E File Offset: 0x00049B6E
			public void Reset()
			{
				this.m_Index = -1;
			}

			// Token: 0x170003FF RID: 1023
			// (get) Token: 0x06000EE0 RID: 3808 RVA: 0x0004B978 File Offset: 0x00049B78
			public Touch Current
			{
				get
				{
					return this.m_Owner[this.m_Index];
				}
			}

			// Token: 0x17000400 RID: 1024
			// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x0004B999 File Offset: 0x00049B99
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x06000EE2 RID: 3810 RVA: 0x000049FE File Offset: 0x00002BFE
			public void Dispose()
			{
			}

			// Token: 0x0400087A RID: 2170
			private readonly TouchHistory m_Owner;

			// Token: 0x0400087B RID: 2171
			private int m_Index;
		}
	}
}
