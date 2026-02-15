using System;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000261 RID: 609
	internal sealed class SavedStructState<T> : ISavedState where T : struct
	{
		// Token: 0x06001620 RID: 5664 RVA: 0x00063E68 File Offset: 0x00062068
		internal SavedStructState(ref T state, SavedStructState<T>.TypedRestore restoreAction, Action staticDisposeCurrentState = null)
		{
			this.m_State = state;
			this.m_RestoreAction = restoreAction;
			this.m_StaticDisposeCurrentState = staticDisposeCurrentState;
		}

		// Token: 0x06001621 RID: 5665 RVA: 0x00063E8A File Offset: 0x0006208A
		public void StaticDisposeCurrentState()
		{
			if (this.m_StaticDisposeCurrentState != null)
			{
				this.m_StaticDisposeCurrentState();
				this.m_StaticDisposeCurrentState = null;
			}
		}

		// Token: 0x06001622 RID: 5666 RVA: 0x00063EA6 File Offset: 0x000620A6
		public void RestoreSavedState()
		{
			this.m_RestoreAction(ref this.m_State);
			this.m_RestoreAction = null;
		}

		// Token: 0x04000CAC RID: 3244
		private T m_State;

		// Token: 0x04000CAD RID: 3245
		private SavedStructState<T>.TypedRestore m_RestoreAction;

		// Token: 0x04000CAE RID: 3246
		private Action m_StaticDisposeCurrentState;

		// Token: 0x02000262 RID: 610
		// (Invoke) Token: 0x06001624 RID: 5668
		public delegate void TypedRestore(ref T state);
	}
}
