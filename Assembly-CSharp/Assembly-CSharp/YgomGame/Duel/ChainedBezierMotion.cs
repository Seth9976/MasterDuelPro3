using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000D17 RID: 3351
	public class ChainedBezierMotion
	{
		// Token: 0x17000AD2 RID: 2770
		// (get) Token: 0x060060AF RID: 24751 RVA: 0x000029CC File Offset: 0x00000BCC
		public int motionNum
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060060B0 RID: 24752 RVA: 0x000029C5 File Offset: 0x00000BC5
		public float totalAnimationTime
		{
			get
			{
				return 0f;
			}
		}

		// Token: 0x060060B1 RID: 24753 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize()
		{
		}

		// Token: 0x060060B2 RID: 24754 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(BezierMotionSetting[] motionList)
		{
		}

		// Token: 0x060060B3 RID: 24755 RVA: 0x0000216D File Offset: 0x0000036D
		public void Initialize(List<BezierMotionSetting> motionList)
		{
		}

		// Token: 0x060060B4 RID: 24756 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddMotion(BezierMotionSetting motion)
		{
		}

		// Token: 0x060060B5 RID: 24757 RVA: 0x0000216D File Offset: 0x0000036D
		public void AddMotion(BezierMotionSetting[] motion)
		{
		}

		// Token: 0x060060B6 RID: 24758 RVA: 0x0000216D File Offset: 0x0000036D
		public void Begin(Vector3 originPosition, Quaternion originRotation, Vector3 targetPosition, Quaternion targetRotation)
		{
		}

		// Token: 0x060060B7 RID: 24759 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetOrigin(Vector3 originPosition, Quaternion originRotation)
		{
		}

		// Token: 0x060060B8 RID: 24760 RVA: 0x0000216D File Offset: 0x0000036D
		public void SetTarget(Vector3 targetPosition, Quaternion targetRotation)
		{
		}

		// Token: 0x060060B9 RID: 24761 RVA: 0x000F543C File Offset: 0x000F363C
		public ValueTuple<Vector3, Quaternion> Update(float time)
		{
			return default(ValueTuple<Vector3, Quaternion>);
		}

		// Token: 0x060060BA RID: 24762 RVA: 0x000F5454 File Offset: 0x000F3654
		public ValueTuple<Vector3, Quaternion> UpdateByClampedTime(float clampedTime)
		{
			return default(ValueTuple<Vector3, Quaternion>);
		}

		// Token: 0x060060BB RID: 24763 RVA: 0x000F546C File Offset: 0x000F366C
		public ValueTuple<Vector3, Quaternion> Get(float time, Camera camera = null)
		{
			return default(ValueTuple<Vector3, Quaternion>);
		}

		// Token: 0x060060BC RID: 24764 RVA: 0x000F5484 File Offset: 0x000F3684
		public ValueTuple<Vector3, Quaternion> GetByClampedTime(float clampedTime, Camera camera = null)
		{
			return default(ValueTuple<Vector3, Quaternion>);
		}

		// Token: 0x060060BD RID: 24765 RVA: 0x000F549C File Offset: 0x000F369C
		public Vector3 GetStartPosition(int index)
		{
			return default(Vector3);
		}

		// Token: 0x060060BE RID: 24766 RVA: 0x000F54B4 File Offset: 0x000F36B4
		public Vector3 GetStartPosition()
		{
			return default(Vector3);
		}

		// Token: 0x060060BF RID: 24767 RVA: 0x000F54CC File Offset: 0x000F36CC
		public Vector3 GetEndPosition(int index)
		{
			return default(Vector3);
		}

		// Token: 0x060060C0 RID: 24768 RVA: 0x000F54E4 File Offset: 0x000F36E4
		public Vector3 GetEndPosition()
		{
			return default(Vector3);
		}

		// Token: 0x060060C1 RID: 24769 RVA: 0x000F54FC File Offset: 0x000F36FC
		public Vector3 GetViaPosition(int index)
		{
			return default(Vector3);
		}

		// Token: 0x060060C2 RID: 24770 RVA: 0x000F5514 File Offset: 0x000F3714
		public ValueTuple<int, float> GetBezierMotionInfo(float time)
		{
			return default(ValueTuple<int, float>);
		}

		// Token: 0x04009C0C RID: 39948
		public List<BezierMotionSetting> motionList;

		// Token: 0x04009C0D RID: 39949
		private int currentMotionIndex;

		// Token: 0x04009C0E RID: 39950
		private Vector3 currentOriginPosition;

		// Token: 0x04009C0F RID: 39951
		private Quaternion currentOriginRotation;

		// Token: 0x04009C10 RID: 39952
		private Vector3 originPosition;

		// Token: 0x04009C11 RID: 39953
		private Quaternion originRotation;

		// Token: 0x04009C12 RID: 39954
		private Vector3 targetPosition;

		// Token: 0x04009C13 RID: 39955
		private Quaternion targetRotation;
	}
}
