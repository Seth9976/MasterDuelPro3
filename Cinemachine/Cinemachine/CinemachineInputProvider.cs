using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

namespace Cinemachine
{
	// Token: 0x020000A9 RID: 169
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.cinemachine@2.9/manual/CinemachineAlternativeInput.html")]
	public class CinemachineInputProvider : MonoBehaviour, AxisState.IInputAxisProvider
	{
		// Token: 0x060003E2 RID: 994 RVA: 0x00016924 File Offset: 0x00014B24
		public virtual float GetAxisValue(int axis)
		{
			if (base.enabled)
			{
				InputAction action = this.ResolveForPlayer(axis, (axis == 2) ? this.ZAxis : this.XYAxis);
				if (action != null)
				{
					switch (axis)
					{
					case 0:
						return action.ReadValue<Vector2>().x;
					case 1:
						return action.ReadValue<Vector2>().y;
					case 2:
						return action.ReadValue<float>();
					}
				}
			}
			return 0f;
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00016990 File Offset: 0x00014B90
		protected InputAction ResolveForPlayer(int axis, InputActionReference actionRef)
		{
			if (axis < 0 || axis >= 3)
			{
				return null;
			}
			if (actionRef == null || actionRef.action == null)
			{
				return null;
			}
			if (this.m_cachedActions == null || this.m_cachedActions.Length != 3)
			{
				this.m_cachedActions = new InputAction[3];
			}
			if (this.m_cachedActions[axis] != null && actionRef.action.id != this.m_cachedActions[axis].id)
			{
				this.m_cachedActions[axis] = null;
			}
			if (this.m_cachedActions[axis] == null)
			{
				this.m_cachedActions[axis] = actionRef.action;
				if (this.PlayerIndex != -1)
				{
					InputAction[] cachedActions = this.m_cachedActions;
					InputUser inputUser = InputUser.all[this.PlayerIndex];
					cachedActions[axis] = CinemachineInputProvider.<ResolveForPlayer>g__GetFirstMatch|7_0(in inputUser, actionRef);
				}
				if (this.AutoEnableInputs && actionRef != null && actionRef.action != null)
				{
					actionRef.action.Enable();
				}
			}
			if (this.m_cachedActions[axis] != null && this.m_cachedActions[axis].enabled != actionRef.action.enabled)
			{
				if (actionRef.action.enabled)
				{
					this.m_cachedActions[axis].Enable();
				}
				else
				{
					this.m_cachedActions[axis].Disable();
				}
			}
			return this.m_cachedActions[axis];
		}

		// Token: 0x060003E4 RID: 996 RVA: 0x00016AC9 File Offset: 0x00014CC9
		protected virtual void OnDisable()
		{
			this.m_cachedActions = null;
		}

		// Token: 0x060003E6 RID: 998 RVA: 0x00016AE8 File Offset: 0x00014CE8
		[CompilerGenerated]
		internal static InputAction <ResolveForPlayer>g__GetFirstMatch|7_0(in InputUser user, InputActionReference aRef)
		{
			InputUser inputUser = user;
			return inputUser.actions.First((InputAction x) => x.id == aRef.action.id);
		}

		// Token: 0x04000377 RID: 887
		[Tooltip("Leave this at -1 for single-player games.  For multi-player games, set this to be the player index, and the actions will be read from that player's controls")]
		public int PlayerIndex = -1;

		// Token: 0x04000378 RID: 888
		[Tooltip("If set, Input Actions will be auto-enabled at start")]
		public bool AutoEnableInputs = true;

		// Token: 0x04000379 RID: 889
		[Tooltip("Vector2 action for XY movement")]
		public InputActionReference XYAxis;

		// Token: 0x0400037A RID: 890
		[Tooltip("Float action for Z movement")]
		public InputActionReference ZAxis;

		// Token: 0x0400037B RID: 891
		private const int NUM_AXES = 3;

		// Token: 0x0400037C RID: 892
		private InputAction[] m_cachedActions;
	}
}
