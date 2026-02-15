using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace YgomSystem
{
	// Token: 0x020004A3 RID: 1187
	public class GamePad_PC : GamePad
	{
		// Token: 0x170001CA RID: 458
		// (set) Token: 0x06002659 RID: 9817 RVA: 0x0000216D File Offset: 0x0000036D
		protected string axisNotFoundError
		{
			set
			{
			}
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x0000216D File Offset: 0x0000036D
		public static void InitializePCGamePadSystem()
		{
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x000029CC File Offset: 0x00000BCC
		private bool getKeyOn(int Type)
		{
			return false;
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x000F1685 File Offset: 0x000EF885
		public GamePad_PC(int iPadID)
			: base(0)
		{
		}

		// Token: 0x0600265D RID: 9821 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetupButtonsAndAxes()
		{
		}

		// Token: 0x0600265E RID: 9822 RVA: 0x0000216D File Offset: 0x0000036D
		protected void SetupVibrations()
		{
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x0000216D File Offset: 0x0000036D
		public override void UpdateFrame()
		{
		}

		// Token: 0x06002660 RID: 9824 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool GetKey(int Type)
		{
			return false;
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000029CC File Offset: 0x00000BCC
		public override bool GetKeyDown(int Type)
		{
			return false;
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000029C5 File Offset: 0x00000BC5
		public override float GetAnalog(int Type)
		{
			return 0f;
		}

		// Token: 0x06002663 RID: 9827 RVA: 0x0000216D File Offset: 0x0000036D
		public override void Vibrate(GamePad.VIBRATION Id)
		{
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x0000216D File Offset: 0x0000036D
		public override void StopVibration()
		{
		}

		// Token: 0x06002665 RID: 9829 RVA: 0x0000216D File Offset: 0x0000036D
		private void UpdateVibration()
		{
		}

		// Token: 0x06002666 RID: 9830 RVA: 0x000029CC File Offset: 0x00000BCC
		protected override int resolveFuncButton(int Type)
		{
			return 0;
		}

		// Token: 0x06002667 RID: 9831 RVA: 0x0000216D File Offset: 0x0000036D
		protected void VerifyBtnMapping()
		{
		}

		// Token: 0x0400277E RID: 10110
		private Dictionary<GamePad.VIBRATION, List<GamePad_PC.VibrationSetting>> vibrationSettings;

		// Token: 0x0400277F RID: 10111
		private List<GamePad_PC.VibrationSetting> vibrationList;

		// Token: 0x04002780 RID: 10112
		private float vibrationTime;

		// Token: 0x04002781 RID: 10113
		private int vibrationIdx;

		// Token: 0x04002782 RID: 10114
		protected static readonly int[] buttonTypes;

		// Token: 0x04002783 RID: 10115
		protected Dictionary<int, GamePad_PC.ButtonInputState> buttonInputStates;

		// Token: 0x04002784 RID: 10116
		protected Gamepad currentPad;

		// Token: 0x04002785 RID: 10117
		protected Dictionary<int, AxisControl> analogAxisControls;

		// Token: 0x04002786 RID: 10118
		private bool _isAxisNotFoundErrorOccured;

		// Token: 0x020004A4 RID: 1188
		public class VibrationSetting
		{
			// Token: 0x06002668 RID: 9832 RVA: 0x00002739 File Offset: 0x00000939
			public VibrationSetting(float l, float h, float t)
			{
			}

			// Token: 0x04002787 RID: 10119
			public float lowFrequency;

			// Token: 0x04002788 RID: 10120
			public float highFrequency;

			// Token: 0x04002789 RID: 10121
			public float time;
		}

		// Token: 0x020004A5 RID: 1189
		protected class ButtonInputState
		{
			// Token: 0x0400278A RID: 10122
			public int type;

			// Token: 0x0400278B RID: 10123
			public ButtonControl buttonControl;

			// Token: 0x0400278C RID: 10124
			public bool nowOn;

			// Token: 0x0400278D RID: 10125
			public bool prevOn;
		}
	}
}
