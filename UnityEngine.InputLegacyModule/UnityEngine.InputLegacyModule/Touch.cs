using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000005 RID: 5
	[NativeHeader("Runtime/Input/InputBindings.h")]
	public struct Touch
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public int fingerId
		{
			get
			{
				return this.m_FingerId;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		// (set) Token: 0x06000003 RID: 3 RVA: 0x00002080 File Offset: 0x00000280
		public Vector2 position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				this.m_Position = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x0000208C File Offset: 0x0000028C
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000020A4 File Offset: 0x000002A4
		public Vector2 rawPosition
		{
			get
			{
				return this.m_RawPosition;
			}
			set
			{
				this.m_RawPosition = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020B0 File Offset: 0x000002B0
		// (set) Token: 0x06000007 RID: 7 RVA: 0x000020C8 File Offset: 0x000002C8
		public Vector2 deltaPosition
		{
			get
			{
				return this.m_PositionDelta;
			}
			set
			{
				this.m_PositionDelta = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000008 RID: 8 RVA: 0x000020D4 File Offset: 0x000002D4
		public float deltaTime
		{
			get
			{
				return this.m_TimeDelta;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020EC File Offset: 0x000002EC
		public int tapCount
		{
			get
			{
				return this.m_TapCount;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002104 File Offset: 0x00000304
		public TouchPhase phase
		{
			get
			{
				return this.m_Phase;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600000B RID: 11 RVA: 0x0000211C File Offset: 0x0000031C
		public float pressure
		{
			get
			{
				return this.m_Pressure;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002134 File Offset: 0x00000334
		public float maximumPossiblePressure
		{
			get
			{
				return this.m_maximumPossiblePressure;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000214C File Offset: 0x0000034C
		public TouchType type
		{
			get
			{
				return this.m_Type;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002164 File Offset: 0x00000364
		public float altitudeAngle
		{
			get
			{
				return this.m_AltitudeAngle;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000217C File Offset: 0x0000037C
		public float azimuthAngle
		{
			get
			{
				return this.m_AzimuthAngle;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002194 File Offset: 0x00000394
		public float radius
		{
			get
			{
				return this.m_Radius;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000021AC File Offset: 0x000003AC
		public float radiusVariance
		{
			get
			{
				return this.m_RadiusVariance;
			}
		}

		// Token: 0x0400000F RID: 15
		private int m_FingerId;

		// Token: 0x04000010 RID: 16
		private Vector2 m_Position;

		// Token: 0x04000011 RID: 17
		private Vector2 m_RawPosition;

		// Token: 0x04000012 RID: 18
		private Vector2 m_PositionDelta;

		// Token: 0x04000013 RID: 19
		private float m_TimeDelta;

		// Token: 0x04000014 RID: 20
		private int m_TapCount;

		// Token: 0x04000015 RID: 21
		private TouchPhase m_Phase;

		// Token: 0x04000016 RID: 22
		private TouchType m_Type;

		// Token: 0x04000017 RID: 23
		private float m_Pressure;

		// Token: 0x04000018 RID: 24
		private float m_maximumPossiblePressure;

		// Token: 0x04000019 RID: 25
		private float m_Radius;

		// Token: 0x0400001A RID: 26
		private float m_RadiusVariance;

		// Token: 0x0400001B RID: 27
		private float m_AltitudeAngle;

		// Token: 0x0400001C RID: 28
		private float m_AzimuthAngle;
	}
}
