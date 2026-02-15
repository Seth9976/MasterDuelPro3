using System;
using System.Collections.Generic;
using UnityEngine;

namespace YgomGame.Duel
{
	// Token: 0x02000C9A RID: 3226
	public class BezierMotionSetting : ScriptableObject
	{
		// Token: 0x06005C54 RID: 23636 RVA: 0x000F4E6C File Offset: 0x000F306C
		public Vector3 GetStartPosition(Vector3 origin_position, Vector3 target_position, Camera camera = null)
		{
			return default(Vector3);
		}

		// Token: 0x06005C55 RID: 23637 RVA: 0x000F4E84 File Offset: 0x000F3084
		public Vector3 GetViaPosition(Vector3 origin_position, Vector3 target_position, Camera camera = null)
		{
			return default(Vector3);
		}

		// Token: 0x06005C56 RID: 23638 RVA: 0x000F4E9C File Offset: 0x000F309C
		public Vector3 GetEndPosition(Vector3 origin_position, Vector3 target_position, Camera camera = null)
		{
			return default(Vector3);
		}

		// Token: 0x06005C57 RID: 23639 RVA: 0x000F4EB4 File Offset: 0x000F30B4
		public ValueTuple<Vector3, Quaternion> Get(Vector3 origin_position, Quaternion origin_rotation, Vector3 target_position, Quaternion target_rotation, float current_time, Camera camera = null)
		{
			return default(ValueTuple<Vector3, Quaternion>);
		}

		// Token: 0x06005C58 RID: 23640 RVA: 0x000F4ECC File Offset: 0x000F30CC
		private Quaternion GetRotation(Vector3 origin_position, Quaternion origin_rotation, Vector3 target_position, Quaternion target_rotation, Vector3 start_position, Vector3 via_position, Vector3 end_position, float current_time, Camera camera = null)
		{
			return default(Quaternion);
		}

		// Token: 0x06005C59 RID: 23641 RVA: 0x000F4EE4 File Offset: 0x000F30E4
		private Quaternion GetAditionalTurn(float current_time)
		{
			return default(Quaternion);
		}

		// Token: 0x06005C5A RID: 23642 RVA: 0x000F4EFC File Offset: 0x000F30FC
		public ValueTuple<Vector3, Quaternion> GetByClampedTime(Vector3 origin_position, Quaternion origin_rotation, Vector3 target_position, Quaternion target_rotation, float clamped_time, Camera camera = null)
		{
			return default(ValueTuple<Vector3, Quaternion>);
		}

		// Token: 0x06005C5B RID: 23643 RVA: 0x0000216A File Offset: 0x0000036A
		public BezierMotionSetting Clone()
		{
			return null;
		}

		// Token: 0x0400978D RID: 38797
		public BezierMotionSetting.InterporationType type;

		// Token: 0x0400978E RID: 38798
		public BezierMotionSetting.PositionOperation startPositionOperation;

		// Token: 0x0400978F RID: 38799
		public BezierMotionSetting.PositionOperation viaPositionOperation;

		// Token: 0x04009790 RID: 38800
		public BezierMotionSetting.PositionOperation endPositionOperation;

		// Token: 0x04009791 RID: 38801
		public AnimationCurve accelerationCurve;

		// Token: 0x04009792 RID: 38802
		public BezierMotionSetting.LookSettingType lookSetting;

		// Token: 0x04009793 RID: 38803
		public BezierMotionSetting.LookElement lookElement1;

		// Token: 0x04009794 RID: 38804
		public BezierMotionSetting.LookElement lookElement2;

		// Token: 0x04009795 RID: 38805
		public BezierMotionSetting.LookElement lookElement3;

		// Token: 0x04009796 RID: 38806
		public AnimationCurve rotationAccelerationCurve;

		// Token: 0x04009797 RID: 38807
		public int aditionalTurnNum;

		// Token: 0x04009798 RID: 38808
		public Vector3 aditionalTurnDirection;

		// Token: 0x04009799 RID: 38809
		public AnimationCurve aditionalTurnAccelerationCurve;

		// Token: 0x0400979A RID: 38810
		public float animationTime;

		// Token: 0x02000C9B RID: 3227
		public enum InterporationType
		{
			// Token: 0x0400979C RID: 38812
			Bezier3Point,
			// Token: 0x0400979D RID: 38813
			Lerp2Point
		}

		// Token: 0x02000C9C RID: 3228
		[Serializable]
		public class PositionOperation
		{
			// Token: 0x06005C5D RID: 23645 RVA: 0x000F4F14 File Offset: 0x000F3114
			public Vector3 Get(Vector3 origin_position, Vector3 target_position, Camera camera = null)
			{
				return default(Vector3);
			}

			// Token: 0x06005C5E RID: 23646 RVA: 0x0000216A File Offset: 0x0000036A
			public BezierMotionSetting.PositionOperation Clone()
			{
				return null;
			}

			// Token: 0x0400979E RID: 38814
			public BezierMotionSetting.BasePositionSetting basePosition;

			// Token: 0x0400979F RID: 38815
			public List<BezierMotionSetting.OffsetSetting> offsetList;

			// Token: 0x040097A0 RID: 38816
			public List<BezierMotionSetting.OverrideSetting> overrideList;
		}

		// Token: 0x02000C9D RID: 3229
		[Serializable]
		public class BasePositionSetting
		{
			// Token: 0x06005C60 RID: 23648 RVA: 0x0000216A File Offset: 0x0000036A
			public BezierMotionSetting.BasePositionSetting Clone()
			{
				return null;
			}

			// Token: 0x040097A1 RID: 38817
			public BezierMotionSetting.BaseVectorValueType type;

			// Token: 0x040097A2 RID: 38818
			public Vector3 designation;
		}

		// Token: 0x02000C9E RID: 3230
		public enum BaseVectorValueType
		{
			// Token: 0x040097A4 RID: 38820
			OriginPosition,
			// Token: 0x040097A5 RID: 38821
			TargetPosition,
			// Token: 0x040097A6 RID: 38822
			Screen,
			// Token: 0x040097A7 RID: 38823
			Designation
		}

		// Token: 0x02000C9F RID: 3231
		[Serializable]
		public class OffsetSetting
		{
			// Token: 0x06005C62 RID: 23650 RVA: 0x0000216A File Offset: 0x0000036A
			public BezierMotionSetting.OffsetSetting Clone()
			{
				return null;
			}

			// Token: 0x040097A8 RID: 38824
			public BezierMotionSetting.OffsetVectorValueType type;

			// Token: 0x040097A9 RID: 38825
			public Vector3 designation;

			// Token: 0x040097AA RID: 38826
			public Vector3 rotationCorrection;

			// Token: 0x040097AB RID: 38827
			public BezierMotionSetting.OffsetIntensityType intensityType;

			// Token: 0x040097AC RID: 38828
			public float intensity;
		}

		// Token: 0x02000CA0 RID: 3232
		public enum OffsetVectorValueType
		{
			// Token: 0x040097AE RID: 38830
			NormalizedDeltaTargetOrigin,
			// Token: 0x040097AF RID: 38831
			Designation,
			// Token: 0x040097B0 RID: 38832
			NormalizedDeltaTargetBase
		}

		// Token: 0x02000CA1 RID: 3233
		public enum OffsetIntensityType
		{
			// Token: 0x040097B2 RID: 38834
			StartToTarget01,
			// Token: 0x040097B3 RID: 38835
			Designation
		}

		// Token: 0x02000CA2 RID: 3234
		[Serializable]
		public class OverrideSetting
		{
			// Token: 0x06005C64 RID: 23652 RVA: 0x0000216A File Offset: 0x0000036A
			public BezierMotionSetting.OverrideSetting Clone()
			{
				return null;
			}

			// Token: 0x040097B4 RID: 38836
			public BezierMotionSetting.OverrideType type;

			// Token: 0x040097B5 RID: 38837
			public BezierMotionSetting.OverrideDirection directionFlag;

			// Token: 0x040097B6 RID: 38838
			public Vector3 designation;
		}

		// Token: 0x02000CA3 RID: 3235
		public enum OverrideType
		{
			// Token: 0x040097B8 RID: 38840
			OriginPosition,
			// Token: 0x040097B9 RID: 38841
			TargetPosition,
			// Token: 0x040097BA RID: 38842
			Screen,
			// Token: 0x040097BB RID: 38843
			Designation
		}

		// Token: 0x02000CA4 RID: 3236
		public enum OverrideDirection
		{
			// Token: 0x040097BD RID: 38845
			X = 1,
			// Token: 0x040097BE RID: 38846
			Y,
			// Token: 0x040097BF RID: 38847
			Z = 4
		}

		// Token: 0x02000CA5 RID: 3237
		[Serializable]
		public class LookElement
		{
			// Token: 0x06005C66 RID: 23654 RVA: 0x0000216A File Offset: 0x0000036A
			public BezierMotionSetting.LookElement Clone()
			{
				return null;
			}

			// Token: 0x06005C67 RID: 23655 RVA: 0x000F4F2C File Offset: 0x000F312C
			public Quaternion Get(Vector3 origin_position, Quaternion origin_rotation, Vector3 target_position, Quaternion target_rotation, Vector3 start_position, Vector3 via_position, Vector3 end_position, Camera camera = null)
			{
				return default(Quaternion);
			}

			// Token: 0x040097C0 RID: 38848
			public BezierMotionSetting.LookElementType type;

			// Token: 0x040097C1 RID: 38849
			public BezierMotionSetting.LookType fromType;

			// Token: 0x040097C2 RID: 38850
			public Vector3 fromDesignationPosition;

			// Token: 0x040097C3 RID: 38851
			public BezierMotionSetting.LookType toType;

			// Token: 0x040097C4 RID: 38852
			public Vector3 toDesignationPosition;

			// Token: 0x040097C5 RID: 38853
			public Vector3 rotationAngle;
		}

		// Token: 0x02000CA6 RID: 3238
		public enum LookSettingType
		{
			// Token: 0x040097C7 RID: 38855
			Single,
			// Token: 0x040097C8 RID: 38856
			Lerp2,
			// Token: 0x040097C9 RID: 38857
			Bezier3
		}

		// Token: 0x02000CA7 RID: 3239
		public enum LookElementType
		{
			// Token: 0x040097CB RID: 38859
			OriginRotation,
			// Token: 0x040097CC RID: 38860
			TargetRotation,
			// Token: 0x040097CD RID: 38861
			CameraRotation,
			// Token: 0x040097CE RID: 38862
			PositionDelta
		}

		// Token: 0x02000CA8 RID: 3240
		public enum LookType
		{
			// Token: 0x040097D0 RID: 38864
			OriginPosition,
			// Token: 0x040097D1 RID: 38865
			TargetPosition,
			// Token: 0x040097D2 RID: 38866
			StartPosition,
			// Token: 0x040097D3 RID: 38867
			ViaPosition,
			// Token: 0x040097D4 RID: 38868
			EndPosition,
			// Token: 0x040097D5 RID: 38869
			CameraPosition,
			// Token: 0x040097D6 RID: 38870
			DesignationPosition
		}
	}
}
