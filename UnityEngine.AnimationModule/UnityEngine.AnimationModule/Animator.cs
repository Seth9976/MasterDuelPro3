using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Playables;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000019 RID: 25
	[NativeHeader("Modules/Animation/Animator.h")]
	[NativeHeader("Modules/Animation/ScriptBindings/AnimatorControllerParameter.bindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/Animation/ScriptBindings/Animator.bindings.h")]
	public class Animator : Behaviour
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002E20 File Offset: 0x00001020
		public bool isOptimizable
		{
			[NativeMethod("IsOptimizable")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_isOptimizable_Injected(intPtr);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002E44 File Offset: 0x00001044
		public bool isHuman
		{
			[NativeMethod("IsHuman")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_isHuman_Injected(intPtr);
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002E68 File Offset: 0x00001068
		public bool hasRootMotion
		{
			[NativeMethod("HasRootMotion")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_hasRootMotion_Injected(intPtr);
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000070 RID: 112 RVA: 0x00002E8C File Offset: 0x0000108C
		internal bool isRootPositionOrRotationControlledByCurves
		{
			[NativeMethod("IsRootTranslationOrRotationControllerByCurves")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_isRootPositionOrRotationControlledByCurves_Injected(intPtr);
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002EB0 File Offset: 0x000010B0
		public float humanScale
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_humanScale_Injected(intPtr);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002ED4 File Offset: 0x000010D4
		public bool isInitialized
		{
			[NativeMethod("IsInitialized")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_isInitialized_Injected(intPtr);
			}
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00002EF8 File Offset: 0x000010F8
		public float GetFloat(string name)
		{
			return this.GetFloatString(name);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00002F14 File Offset: 0x00001114
		public float GetFloat(int id)
		{
			return this.GetFloatID(id);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00002F2D File Offset: 0x0000112D
		public void SetFloat(string name, float value)
		{
			this.SetFloatString(name, value);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002F39 File Offset: 0x00001139
		public void SetFloat(string name, float value, float dampTime, float deltaTime)
		{
			this.SetFloatStringDamp(name, value, dampTime, deltaTime);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002F48 File Offset: 0x00001148
		public void SetFloat(int id, float value)
		{
			this.SetFloatID(id, value);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002F54 File Offset: 0x00001154
		public void SetFloat(int id, float value, float dampTime, float deltaTime)
		{
			this.SetFloatIDDamp(id, value, dampTime, deltaTime);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002F64 File Offset: 0x00001164
		public bool GetBool(string name)
		{
			return this.GetBoolString(name);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002F80 File Offset: 0x00001180
		public bool GetBool(int id)
		{
			return this.GetBoolID(id);
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00002F99 File Offset: 0x00001199
		public void SetBool(string name, bool value)
		{
			this.SetBoolString(name, value);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002FA5 File Offset: 0x000011A5
		public void SetBool(int id, bool value)
		{
			this.SetBoolID(id, value);
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002FB4 File Offset: 0x000011B4
		public int GetInteger(string name)
		{
			return this.GetIntegerString(name);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002FD0 File Offset: 0x000011D0
		public int GetInteger(int id)
		{
			return this.GetIntegerID(id);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00002FE9 File Offset: 0x000011E9
		public void SetInteger(string name, int value)
		{
			this.SetIntegerString(name, value);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00002FF5 File Offset: 0x000011F5
		public void SetInteger(int id, int value)
		{
			this.SetIntegerID(id, value);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003001 File Offset: 0x00001201
		public void SetTrigger(string name)
		{
			this.SetTriggerString(name);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000300C File Offset: 0x0000120C
		public void SetTrigger(int id)
		{
			this.SetTriggerID(id);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003017 File Offset: 0x00001217
		public void ResetTrigger(string name)
		{
			this.ResetTriggerString(name);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003022 File Offset: 0x00001222
		public void ResetTrigger(int id)
		{
			this.ResetTriggerID(id);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003030 File Offset: 0x00001230
		public bool IsParameterControlledByCurve(string name)
		{
			return this.IsParameterControlledByCurveString(name);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000304C File Offset: 0x0000124C
		public bool IsParameterControlledByCurve(int id)
		{
			return this.IsParameterControlledByCurveID(id);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00003068 File Offset: 0x00001268
		public Vector3 deltaPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_deltaPosition_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00003090 File Offset: 0x00001290
		public Quaternion deltaRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Animator.get_deltaRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000030B8 File Offset: 0x000012B8
		public Vector3 velocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_velocity_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600008A RID: 138 RVA: 0x000030E0 File Offset: 0x000012E0
		public Vector3 angularVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_angularVelocity_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00003108 File Offset: 0x00001308
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00003130 File Offset: 0x00001330
		public Vector3 rootPosition
		{
			[NativeMethod("GetAvatarPosition")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_rootPosition_Injected(intPtr, out vector);
				return vector;
			}
			[NativeMethod("SetAvatarPosition")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_rootPosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00003154 File Offset: 0x00001354
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000317C File Offset: 0x0000137C
		public Quaternion rootRotation
		{
			[NativeMethod("GetAvatarRotation")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Animator.get_rootRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
			[NativeMethod("SetAvatarRotation")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_rootRotation_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600008F RID: 143 RVA: 0x000031A0 File Offset: 0x000013A0
		// (set) Token: 0x06000090 RID: 144 RVA: 0x000031C4 File Offset: 0x000013C4
		public bool applyRootMotion
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_applyRootMotion_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_applyRootMotion_Injected(intPtr, value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000091 RID: 145 RVA: 0x000031E8 File Offset: 0x000013E8
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000320C File Offset: 0x0000140C
		[Obsolete("Animator.linearVelocityBlending is no longer used and has been deprecated.")]
		public bool linearVelocityBlending
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_linearVelocityBlending_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_linearVelocityBlending_Injected(intPtr, value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003230 File Offset: 0x00001430
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003254 File Offset: 0x00001454
		public bool animatePhysics
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_animatePhysics_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_animatePhysics_Injected(intPtr, value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003278 File Offset: 0x00001478
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000329C File Offset: 0x0000149C
		public AnimatorUpdateMode updateMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_updateMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_updateMode_Injected(intPtr, value);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000032C0 File Offset: 0x000014C0
		public bool hasTransformHierarchy
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_hasTransformHierarchy_Injected(intPtr);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000032E4 File Offset: 0x000014E4
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003308 File Offset: 0x00001508
		internal bool allowConstantClipSamplingOptimization
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_allowConstantClipSamplingOptimization_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_allowConstantClipSamplingOptimization_Injected(intPtr, value);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000332C File Offset: 0x0000152C
		public float gravityWeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_gravityWeight_Injected(intPtr);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003350 File Offset: 0x00001550
		// (set) Token: 0x0600009C RID: 156 RVA: 0x0000336F File Offset: 0x0000156F
		public Vector3 bodyPosition
		{
			get
			{
				this.CheckIfInIKPass();
				return this.bodyPositionInternal;
			}
			set
			{
				this.CheckIfInIKPass();
				this.bodyPositionInternal = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00003384 File Offset: 0x00001584
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000033AC File Offset: 0x000015AC
		internal Vector3 bodyPositionInternal
		{
			[NativeMethod("GetBodyPosition")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_bodyPositionInternal_Injected(intPtr, out vector);
				return vector;
			}
			[NativeMethod("SetBodyPosition")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_bodyPositionInternal_Injected(intPtr, ref value);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009F RID: 159 RVA: 0x000033D0 File Offset: 0x000015D0
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x000033EF File Offset: 0x000015EF
		public Quaternion bodyRotation
		{
			get
			{
				this.CheckIfInIKPass();
				return this.bodyRotationInternal;
			}
			set
			{
				this.CheckIfInIKPass();
				this.bodyRotationInternal = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003404 File Offset: 0x00001604
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000342C File Offset: 0x0000162C
		internal Quaternion bodyRotationInternal
		{
			[NativeMethod("GetBodyRotation")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Animator.get_bodyRotationInternal_Injected(intPtr, out quaternion);
				return quaternion;
			}
			[NativeMethod("SetBodyRotation")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_bodyRotationInternal_Injected(intPtr, ref value);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00003450 File Offset: 0x00001650
		public Vector3 GetIKPosition(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalPosition(goal);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00003470 File Offset: 0x00001670
		private Vector3 GetGoalPosition(AvatarIKGoal goal)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Animator.GetGoalPosition_Injected(intPtr, goal, out vector);
			return vector;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00003496 File Offset: 0x00001696
		public void SetIKPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			this.CheckIfInIKPass();
			this.SetGoalPosition(goal, goalPosition);
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000034AC File Offset: 0x000016AC
		private void SetGoalPosition(AvatarIKGoal goal, Vector3 goalPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetGoalPosition_Injected(intPtr, goal, ref goalPosition);
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000034D4 File Offset: 0x000016D4
		public Quaternion GetIKRotation(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalRotation(goal);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000034F4 File Offset: 0x000016F4
		private Quaternion GetGoalRotation(AvatarIKGoal goal)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Quaternion quaternion;
			Animator.GetGoalRotation_Injected(intPtr, goal, out quaternion);
			return quaternion;
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x0000351A File Offset: 0x0000171A
		public void SetIKRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			this.CheckIfInIKPass();
			this.SetGoalRotation(goal, goalRotation);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00003530 File Offset: 0x00001730
		private void SetGoalRotation(AvatarIKGoal goal, Quaternion goalRotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetGoalRotation_Injected(intPtr, goal, ref goalRotation);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003558 File Offset: 0x00001758
		public float GetIKPositionWeight(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalWeightPosition(goal);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003578 File Offset: 0x00001778
		private float GetGoalWeightPosition(AvatarIKGoal goal)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetGoalWeightPosition_Injected(intPtr, goal);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000359B File Offset: 0x0000179B
		public void SetIKPositionWeight(AvatarIKGoal goal, float value)
		{
			this.CheckIfInIKPass();
			this.SetGoalWeightPosition(goal, value);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000035B0 File Offset: 0x000017B0
		private void SetGoalWeightPosition(AvatarIKGoal goal, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetGoalWeightPosition_Injected(intPtr, goal, value);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000035D4 File Offset: 0x000017D4
		public float GetIKRotationWeight(AvatarIKGoal goal)
		{
			this.CheckIfInIKPass();
			return this.GetGoalWeightRotation(goal);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x000035F4 File Offset: 0x000017F4
		private float GetGoalWeightRotation(AvatarIKGoal goal)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetGoalWeightRotation_Injected(intPtr, goal);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003617 File Offset: 0x00001817
		public void SetIKRotationWeight(AvatarIKGoal goal, float value)
		{
			this.CheckIfInIKPass();
			this.SetGoalWeightRotation(goal, value);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000362C File Offset: 0x0000182C
		private void SetGoalWeightRotation(AvatarIKGoal goal, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetGoalWeightRotation_Injected(intPtr, goal, value);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003650 File Offset: 0x00001850
		public Vector3 GetIKHintPosition(AvatarIKHint hint)
		{
			this.CheckIfInIKPass();
			return this.GetHintPosition(hint);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003670 File Offset: 0x00001870
		private Vector3 GetHintPosition(AvatarIKHint hint)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Vector3 vector;
			Animator.GetHintPosition_Injected(intPtr, hint, out vector);
			return vector;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00003696 File Offset: 0x00001896
		public void SetIKHintPosition(AvatarIKHint hint, Vector3 hintPosition)
		{
			this.CheckIfInIKPass();
			this.SetHintPosition(hint, hintPosition);
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000036AC File Offset: 0x000018AC
		private void SetHintPosition(AvatarIKHint hint, Vector3 hintPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetHintPosition_Injected(intPtr, hint, ref hintPosition);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000036D4 File Offset: 0x000018D4
		public float GetIKHintPositionWeight(AvatarIKHint hint)
		{
			this.CheckIfInIKPass();
			return this.GetHintWeightPosition(hint);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000036F4 File Offset: 0x000018F4
		private float GetHintWeightPosition(AvatarIKHint hint)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetHintWeightPosition_Injected(intPtr, hint);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003717 File Offset: 0x00001917
		public void SetIKHintPositionWeight(AvatarIKHint hint, float value)
		{
			this.CheckIfInIKPass();
			this.SetHintWeightPosition(hint, value);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x0000372C File Offset: 0x0000192C
		private void SetHintWeightPosition(AvatarIKHint hint, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetHintWeightPosition_Injected(intPtr, hint, value);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003750 File Offset: 0x00001950
		public void SetLookAtPosition(Vector3 lookAtPosition)
		{
			this.CheckIfInIKPass();
			this.SetLookAtPositionInternal(lookAtPosition);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003764 File Offset: 0x00001964
		[NativeMethod("SetLookAtPosition")]
		private void SetLookAtPositionInternal(Vector3 lookAtPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetLookAtPositionInternal_Injected(intPtr, ref lookAtPosition);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003788 File Offset: 0x00001988
		public void SetLookAtWeight(float weight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, 0f, 1f, 0f, 0.5f);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000037AE File Offset: 0x000019AE
		public void SetLookAtWeight(float weight, float bodyWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, 1f, 0f, 0.5f);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000037D0 File Offset: 0x000019D0
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, 0f, 0.5f);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000037EE File Offset: 0x000019EE
		public void SetLookAtWeight(float weight, float bodyWeight, float headWeight, float eyesWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, 0.5f);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003809 File Offset: 0x00001A09
		public void SetLookAtWeight(float weight, [UnityEngine.Internal.DefaultValue("0.0f")] float bodyWeight, [UnityEngine.Internal.DefaultValue("1.0f")] float headWeight, [UnityEngine.Internal.DefaultValue("0.0f")] float eyesWeight, [UnityEngine.Internal.DefaultValue("0.5f")] float clampWeight)
		{
			this.CheckIfInIKPass();
			this.SetLookAtWeightInternal(weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00003824 File Offset: 0x00001A24
		[NativeMethod("SetLookAtWeight")]
		private void SetLookAtWeightInternal(float weight, float bodyWeight, float headWeight, float eyesWeight, float clampWeight)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetLookAtWeightInternal_Injected(intPtr, weight, bodyWeight, headWeight, eyesWeight, clampWeight);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x0000384D File Offset: 0x00001A4D
		public void SetBoneLocalRotation(HumanBodyBones humanBoneId, Quaternion rotation)
		{
			this.CheckIfInIKPass();
			this.SetBoneLocalRotationInternal(HumanTrait.GetBoneIndexFromMono((int)humanBoneId), rotation);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003868 File Offset: 0x00001A68
		[NativeMethod("SetBoneLocalRotation")]
		private void SetBoneLocalRotationInternal(int humanBoneId, Quaternion rotation)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetBoneLocalRotationInternal_Injected(intPtr, humanBoneId, ref rotation);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003890 File Offset: 0x00001A90
		private ScriptableObject GetBehaviour([NotNull] Type type)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<ScriptableObject>(Animator.GetBehaviour_Injected(intPtr, type));
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x000038C8 File Offset: 0x00001AC8
		public T GetBehaviour<T>() where T : StateMachineBehaviour
		{
			return this.GetBehaviour(typeof(T)) as T;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x000038F4 File Offset: 0x00001AF4
		private static T[] ConvertStateMachineBehaviour<T>(ScriptableObject[] rawObjects) where T : StateMachineBehaviour
		{
			bool flag = rawObjects == null;
			T[] array;
			if (flag)
			{
				array = null;
			}
			else
			{
				T[] typedObjects = new T[rawObjects.Length];
				for (int i = 0; i < typedObjects.Length; i++)
				{
					typedObjects[i] = (T)((object)rawObjects[i]);
				}
				array = typedObjects;
			}
			return array;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003940 File Offset: 0x00001B40
		public T[] GetBehaviours<T>() where T : StateMachineBehaviour
		{
			return Animator.ConvertStateMachineBehaviour<T>(this.InternalGetBehaviours(typeof(T)));
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00003968 File Offset: 0x00001B68
		[FreeFunction(Name = "AnimatorBindings::InternalGetBehaviours", HasExplicitThis = true)]
		internal ScriptableObject[] InternalGetBehaviours([NotNull] Type type)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.InternalGetBehaviours_Injected(intPtr, type);
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000399C File Offset: 0x00001B9C
		public StateMachineBehaviour[] GetBehaviours(int fullPathHash, int layerIndex)
		{
			return this.InternalGetBehavioursByKey(fullPathHash, layerIndex, typeof(StateMachineBehaviour)) as StateMachineBehaviour[];
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000039C8 File Offset: 0x00001BC8
		[FreeFunction(Name = "AnimatorBindings::InternalGetBehavioursByKey", HasExplicitThis = true)]
		internal ScriptableObject[] InternalGetBehavioursByKey(int fullPathHash, int layerIndex, [NotNull] Type type)
		{
			if (type == null)
			{
				ThrowHelper.ThrowArgumentNullException(type, "type");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.InternalGetBehavioursByKey_Injected(intPtr, fullPathHash, layerIndex, type);
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000039FC File Offset: 0x00001BFC
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00003A20 File Offset: 0x00001C20
		public bool stabilizeFeet
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_stabilizeFeet_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_stabilizeFeet_Injected(intPtr, value);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003A44 File Offset: 0x00001C44
		public int layerCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_layerCount_Injected(intPtr);
			}
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003A68 File Offset: 0x00001C68
		public string GetLayerName(int layerIndex)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Animator.GetLayerName_Injected(intPtr, layerIndex, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003AA8 File Offset: 0x00001CA8
		public unsafe int GetLayerIndex(string layerName)
		{
			int layerIndex_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(layerName, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = layerName.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				layerIndex_Injected = Animator.GetLayerIndex_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return layerIndex_Injected;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003B10 File Offset: 0x00001D10
		public float GetLayerWeight(int layerIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetLayerWeight_Injected(intPtr, layerIndex);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003B34 File Offset: 0x00001D34
		public void SetLayerWeight(int layerIndex, float weight)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetLayerWeight_Injected(intPtr, layerIndex, weight);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003B58 File Offset: 0x00001D58
		private void GetAnimatorStateInfo(int layerIndex, StateInfoIndex stateInfoIndex, out AnimatorStateInfo info)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.GetAnimatorStateInfo_Injected(intPtr, layerIndex, stateInfoIndex, out info);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003B80 File Offset: 0x00001D80
		public AnimatorStateInfo GetCurrentAnimatorStateInfo(int layerIndex)
		{
			AnimatorStateInfo info;
			this.GetAnimatorStateInfo(layerIndex, StateInfoIndex.CurrentState, out info);
			return info;
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003BA0 File Offset: 0x00001DA0
		public AnimatorStateInfo GetNextAnimatorStateInfo(int layerIndex)
		{
			AnimatorStateInfo info;
			this.GetAnimatorStateInfo(layerIndex, StateInfoIndex.NextState, out info);
			return info;
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00003BC0 File Offset: 0x00001DC0
		private void GetAnimatorTransitionInfo(int layerIndex, out AnimatorTransitionInfo info)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.GetAnimatorTransitionInfo_Injected(intPtr, layerIndex, out info);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public AnimatorTransitionInfo GetAnimatorTransitionInfo(int layerIndex)
		{
			AnimatorTransitionInfo info;
			this.GetAnimatorTransitionInfo(layerIndex, out info);
			return info;
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003C04 File Offset: 0x00001E04
		internal int GetAnimatorClipInfoCount(int layerIndex, bool current)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetAnimatorClipInfoCount_Injected(intPtr, layerIndex, current);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00003C28 File Offset: 0x00001E28
		public int GetCurrentAnimatorClipInfoCount(int layerIndex)
		{
			return this.GetAnimatorClipInfoCount(layerIndex, true);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003C44 File Offset: 0x00001E44
		public int GetNextAnimatorClipInfoCount(int layerIndex)
		{
			return this.GetAnimatorClipInfoCount(layerIndex, false);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003C60 File Offset: 0x00001E60
		[FreeFunction(Name = "AnimatorBindings::GetCurrentAnimatorClipInfo", HasExplicitThis = true)]
		[return: Unmarshalled]
		public AnimatorClipInfo[] GetCurrentAnimatorClipInfo(int layerIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetCurrentAnimatorClipInfo_Injected(intPtr, layerIndex);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003C84 File Offset: 0x00001E84
		[FreeFunction(Name = "AnimatorBindings::GetNextAnimatorClipInfo", HasExplicitThis = true)]
		[return: Unmarshalled]
		public AnimatorClipInfo[] GetNextAnimatorClipInfo(int layerIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetNextAnimatorClipInfo_Injected(intPtr, layerIndex);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public void GetCurrentAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			this.GetAnimatorClipInfoInternal(layerIndex, true, clips);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003CD4 File Offset: 0x00001ED4
		[FreeFunction(Name = "AnimatorBindings::GetAnimatorClipInfoInternal", HasExplicitThis = true)]
		private void GetAnimatorClipInfoInternal(int layerIndex, bool isCurrent, object clips)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.GetAnimatorClipInfoInternal_Injected(intPtr, layerIndex, isCurrent, clips);
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003CFC File Offset: 0x00001EFC
		public void GetNextAnimatorClipInfo(int layerIndex, List<AnimatorClipInfo> clips)
		{
			bool flag = clips == null;
			if (flag)
			{
				throw new ArgumentNullException("clips");
			}
			this.GetAnimatorClipInfoInternal(layerIndex, false, clips);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003D28 File Offset: 0x00001F28
		public bool IsInTransition(int layerIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.IsInTransition_Injected(intPtr, layerIndex);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003D4C File Offset: 0x00001F4C
		public AnimatorControllerParameter[] parameters
		{
			[FreeFunction(Name = "AnimatorBindings::GetParameters", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_parameters_Injected(intPtr);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003D70 File Offset: 0x00001F70
		public int parameterCount
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_parameterCount_Injected(intPtr);
			}
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003D94 File Offset: 0x00001F94
		[FreeFunction(Name = "AnimatorBindings::GetParameterInternal", HasExplicitThis = true)]
		private AnimatorControllerParameter GetParameterInternal(int index)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetParameterInternal_Injected(intPtr, index);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003DB8 File Offset: 0x00001FB8
		public AnimatorControllerParameter GetParameter(int index)
		{
			AnimatorControllerParameter parameter = this.GetParameterInternal(index);
			bool flag = parameter.m_Type == (AnimatorControllerParameterType)0;
			if (flag)
			{
				throw new IndexOutOfRangeException("Index must be between 0 and " + this.parameterCount.ToString());
			}
			return parameter;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003E00 File Offset: 0x00002000
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00003E24 File Offset: 0x00002024
		public float feetPivotActive
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_feetPivotActive_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_feetPivotActive_Injected(intPtr, value);
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003E48 File Offset: 0x00002048
		public float pivotWeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_pivotWeight_Injected(intPtr);
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x00003E6C File Offset: 0x0000206C
		public Vector3 pivotPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_pivotPosition_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00003E94 File Offset: 0x00002094
		private void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, int targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime, bool completeMatch)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.MatchTarget_Injected(intPtr, ref matchPosition, ref matchRotation, targetBodyPart, ref weightMask, startNormalizedTime, targetNormalizedTime, completeMatch);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00003EC3 File Offset: 0x000020C3
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, 1f, true);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003EDA File Offset: 0x000020DA
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [UnityEngine.Internal.DefaultValue("1")] float targetNormalizedTime)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, targetNormalizedTime, true);
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00003EEE File Offset: 0x000020EE
		public void MatchTarget(Vector3 matchPosition, Quaternion matchRotation, AvatarTarget targetBodyPart, MatchTargetWeightMask weightMask, float startNormalizedTime, [UnityEngine.Internal.DefaultValue("1")] float targetNormalizedTime, [UnityEngine.Internal.DefaultValue("true")] bool completeMatch)
		{
			this.MatchTarget(matchPosition, matchRotation, (int)targetBodyPart, weightMask, startNormalizedTime, targetNormalizedTime, completeMatch);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00003F03 File Offset: 0x00002103
		public void InterruptMatchTarget()
		{
			this.InterruptMatchTarget(true);
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00003F10 File Offset: 0x00002110
		public void InterruptMatchTarget([UnityEngine.Internal.DefaultValue("true")] bool completeMatch)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.InterruptMatchTarget_Injected(intPtr, completeMatch);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003F34 File Offset: 0x00002134
		public bool isMatchingTarget
		{
			[NativeMethod("IsMatchingTarget")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_isMatchingTarget_Injected(intPtr);
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003F58 File Offset: 0x00002158
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00003F7C File Offset: 0x0000217C
		public float speed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_speed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_speed_Injected(intPtr, value);
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003F9F File Offset: 0x0000219F
		[Obsolete("ForceStateNormalizedTime is deprecated. Please use Play or CrossFade instead.")]
		public void ForceStateNormalizedTime(float normalizedTime)
		{
			this.Play(0, 0, normalizedTime);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003FAC File Offset: 0x000021AC
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			int layer = -1;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003FDC File Offset: 0x000021DC
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004008 File Offset: 0x00002208
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x0000402E File Offset: 0x0000222E
		public void CrossFadeInFixedTime(string stateName, float fixedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float fixedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			this.CrossFadeInFixedTime(Animator.StringToHash(stateName), fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004044 File Offset: 0x00002244
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer, float fixedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004068 File Offset: 0x00002268
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004090 File Offset: 0x00002290
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float fixedTimeOffset = 0f;
			int layer = -1;
			this.CrossFadeInFixedTime(stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000040B8 File Offset: 0x000022B8
		[FreeFunction(Name = "AnimatorBindings::CrossFadeInFixedTime", HasExplicitThis = true)]
		public void CrossFadeInFixedTime(int stateHashName, float fixedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float fixedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.CrossFadeInFixedTime_Injected(intPtr, stateHashName, fixedTransitionDuration, layer, fixedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000040E4 File Offset: 0x000022E4
		[FreeFunction(Name = "AnimatorBindings::WriteDefaultValues", HasExplicitThis = true)]
		public void WriteDefaultValues()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.WriteDefaultValues_Injected(intPtr);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004108 File Offset: 0x00002308
		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060000FD RID: 253 RVA: 0x0000412C File Offset: 0x0000232C
		public void CrossFade(string stateName, float normalizedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			this.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004154 File Offset: 0x00002354
		public void CrossFade(string stateName, float normalizedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			int layer = -1;
			this.CrossFade(stateName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x060000FF RID: 255 RVA: 0x0000417C File Offset: 0x0000237C
		public void CrossFade(string stateName, float normalizedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			this.CrossFade(Animator.StringToHash(stateName), normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004194 File Offset: 0x00002394
		[FreeFunction(Name = "AnimatorBindings::CrossFade", HasExplicitThis = true)]
		public void CrossFade(int stateHashName, float normalizedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.CrossFade_Injected(intPtr, stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x000041C0 File Offset: 0x000023C0
		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer, float normalizedTimeOffset)
		{
			float normalizedTransitionTime = 0f;
			this.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000041E4 File Offset: 0x000023E4
		public void CrossFade(int stateHashName, float normalizedTransitionDuration, int layer)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			this.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000420C File Offset: 0x0000240C
		public void CrossFade(int stateHashName, float normalizedTransitionDuration)
		{
			float normalizedTransitionTime = 0f;
			float normalizedTimeOffset = float.NegativeInfinity;
			int layer = -1;
			this.CrossFade(stateHashName, normalizedTransitionDuration, layer, normalizedTimeOffset, normalizedTransitionTime);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004234 File Offset: 0x00002434
		public void PlayInFixedTime(string stateName, int layer)
		{
			float fixedTime = float.NegativeInfinity;
			this.PlayInFixedTime(stateName, layer, fixedTime);
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004254 File Offset: 0x00002454
		public void PlayInFixedTime(string stateName)
		{
			float fixedTime = float.NegativeInfinity;
			int layer = -1;
			this.PlayInFixedTime(stateName, layer, fixedTime);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004274 File Offset: 0x00002474
		public void PlayInFixedTime(string stateName, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			this.PlayInFixedTime(Animator.StringToHash(stateName), layer, fixedTime);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004288 File Offset: 0x00002488
		[FreeFunction(Name = "AnimatorBindings::PlayInFixedTime", HasExplicitThis = true)]
		public void PlayInFixedTime(int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float fixedTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.PlayInFixedTime_Injected(intPtr, stateNameHash, layer, fixedTime);
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000042B0 File Offset: 0x000024B0
		public void PlayInFixedTime(int stateNameHash, int layer)
		{
			float fixedTime = float.NegativeInfinity;
			this.PlayInFixedTime(stateNameHash, layer, fixedTime);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000042D0 File Offset: 0x000024D0
		public void PlayInFixedTime(int stateNameHash)
		{
			float fixedTime = float.NegativeInfinity;
			int layer = -1;
			this.PlayInFixedTime(stateNameHash, layer, fixedTime);
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000042F0 File Offset: 0x000024F0
		public void Play(string stateName, int layer)
		{
			float normalizedTime = float.NegativeInfinity;
			this.Play(stateName, layer, normalizedTime);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004310 File Offset: 0x00002510
		public void Play(string stateName)
		{
			float normalizedTime = float.NegativeInfinity;
			int layer = -1;
			this.Play(stateName, layer, normalizedTime);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004330 File Offset: 0x00002530
		public void Play(string stateName, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			this.Play(Animator.StringToHash(stateName), layer, normalizedTime);
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00004344 File Offset: 0x00002544
		[FreeFunction(Name = "AnimatorBindings::Play", HasExplicitThis = true)]
		public void Play(int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.Play_Injected(intPtr, stateNameHash, layer, normalizedTime);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000436C File Offset: 0x0000256C
		public void Play(int stateNameHash, int layer)
		{
			float normalizedTime = float.NegativeInfinity;
			this.Play(stateNameHash, layer, normalizedTime);
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000438C File Offset: 0x0000258C
		public void Play(int stateNameHash)
		{
			float normalizedTime = float.NegativeInfinity;
			int layer = -1;
			this.Play(stateNameHash, layer, normalizedTime);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000043AC File Offset: 0x000025AC
		public void SetTarget(AvatarTarget targetIndex, float targetNormalizedTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetTarget_Injected(intPtr, targetIndex, targetNormalizedTime);
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000043D0 File Offset: 0x000025D0
		public Vector3 targetPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				Animator.get_targetPosition_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000112 RID: 274 RVA: 0x000043F8 File Offset: 0x000025F8
		public Quaternion targetRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Quaternion quaternion;
				Animator.get_targetRotation_Injected(intPtr, out quaternion);
				return quaternion;
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004420 File Offset: 0x00002620
		[Obsolete("Use mask and layers to control subset of transfroms in a skeleton.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool IsControlled(Transform transform)
		{
			return false;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00004434 File Offset: 0x00002634
		internal bool IsBoneTransform(Transform transform)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.IsBoneTransform_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Transform>(transform));
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000445C File Offset: 0x0000265C
		public Transform avatarRoot
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Transform>(Animator.get_avatarRoot_Injected(intPtr));
			}
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004484 File Offset: 0x00002684
		public Transform GetBoneTransform(HumanBodyBones humanBoneId)
		{
			bool flag = this.avatar == null;
			if (flag)
			{
				throw new InvalidOperationException("Avatar is null.");
			}
			bool flag2 = !this.avatar.isValid;
			if (flag2)
			{
				throw new InvalidOperationException("Avatar is not valid.");
			}
			bool flag3 = !this.avatar.isHuman;
			if (flag3)
			{
				throw new InvalidOperationException("Avatar is not of type humanoid.");
			}
			bool flag4 = humanBoneId < HumanBodyBones.Hips || humanBoneId >= HumanBodyBones.LastBone;
			if (flag4)
			{
				throw new IndexOutOfRangeException("humanBoneId must be between 0 and " + HumanBodyBones.LastBone.ToString());
			}
			return this.GetBoneTransformInternal(HumanTrait.GetBoneIndexFromMono((int)humanBoneId));
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004530 File Offset: 0x00002730
		[NativeMethod("GetBoneTransform")]
		internal Transform GetBoneTransformInternal(int humanBoneId)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Transform>(Animator.GetBoneTransformInternal_Injected(intPtr, humanBoneId));
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00004558 File Offset: 0x00002758
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000457C File Offset: 0x0000277C
		public AnimatorCullingMode cullingMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_cullingMode_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_cullingMode_Injected(intPtr, value);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x000045A0 File Offset: 0x000027A0
		public void StartPlayback()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.StartPlayback_Injected(intPtr);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000045C4 File Offset: 0x000027C4
		public void StopPlayback()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.StopPlayback_Injected(intPtr);
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600011C RID: 284 RVA: 0x000045E8 File Offset: 0x000027E8
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000460C File Offset: 0x0000280C
		public float playbackTime
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_playbackTime_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_playbackTime_Injected(intPtr, value);
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004630 File Offset: 0x00002830
		public void StartRecording(int frameCount)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.StartRecording_Injected(intPtr, frameCount);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004654 File Offset: 0x00002854
		public void StopRecording()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.StopRecording_Injected(intPtr);
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00004678 File Offset: 0x00002878
		// (set) Token: 0x06000121 RID: 289 RVA: 0x00002050 File Offset: 0x00000250
		public float recorderStartTime
		{
			get
			{
				return this.GetRecorderStartTime();
			}
			set
			{
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004690 File Offset: 0x00002890
		private float GetRecorderStartTime()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetRecorderStartTime_Injected(intPtr);
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000123 RID: 291 RVA: 0x000046B4 File Offset: 0x000028B4
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00002050 File Offset: 0x00000250
		public float recorderStopTime
		{
			get
			{
				return this.GetRecorderStopTime();
			}
			set
			{
			}
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000046CC File Offset: 0x000028CC
		private float GetRecorderStopTime()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetRecorderStopTime_Injected(intPtr);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000126 RID: 294 RVA: 0x000046F0 File Offset: 0x000028F0
		public AnimatorRecorderMode recorderMode
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_recorderMode_Injected(intPtr);
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00004714 File Offset: 0x00002914
		// (set) Token: 0x06000128 RID: 296 RVA: 0x0000473C File Offset: 0x0000293C
		public RuntimeAnimatorController runtimeAnimatorController
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<RuntimeAnimatorController>(Animator.get_runtimeAnimatorController_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_runtimeAnimatorController_Injected(intPtr, Object.MarshalledUnityObject.Marshal<RuntimeAnimatorController>(value));
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00004764 File Offset: 0x00002964
		public bool hasBoundPlayables
		{
			[NativeMethod("HasBoundPlayables")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_hasBoundPlayables_Injected(intPtr);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004788 File Offset: 0x00002988
		internal void ClearInternalControllerPlayable()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.ClearInternalControllerPlayable_Injected(intPtr);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000047AC File Offset: 0x000029AC
		public bool HasState(int layerIndex, int stateID)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.HasState_Injected(intPtr, layerIndex, stateID);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000047D0 File Offset: 0x000029D0
		[NativeMethod(Name = "ScriptingStringToCRC32", IsThreadSafe = true)]
		public unsafe static int StringToHash(string name)
		{
			int num;
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				num = Animator.StringToHash_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return num;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00004828 File Offset: 0x00002A28
		// (set) Token: 0x0600012E RID: 302 RVA: 0x00004850 File Offset: 0x00002A50
		public Avatar avatar
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Unmarshal.UnmarshalUnityObject<Avatar>(Animator.get_avatar_Injected(intPtr));
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_avatar_Injected(intPtr, Object.MarshalledUnityObject.Marshal<Avatar>(value));
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004878 File Offset: 0x00002A78
		internal string GetStats()
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Animator.GetStats_Injected(intPtr, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000130 RID: 304 RVA: 0x000048B8 File Offset: 0x00002AB8
		public PlayableGraph playableGraph
		{
			get
			{
				PlayableGraph graph = default(PlayableGraph);
				this.GetCurrentGraph(ref graph);
				return graph;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x000048DC File Offset: 0x00002ADC
		[FreeFunction(Name = "AnimatorBindings::GetCurrentGraph", HasExplicitThis = true)]
		private void GetCurrentGraph(ref PlayableGraph graph)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.GetCurrentGraph_Injected(intPtr, ref graph);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00004900 File Offset: 0x00002B00
		private void CheckIfInIKPass()
		{
			bool flag = this.logWarnings && !this.IsInIKPass();
			if (flag)
			{
				Debug.LogWarning("Setting and getting Body Position/Rotation, IK Goals, Lookat and BoneLocalRotation should only be done in OnAnimatorIK or OnStateIK");
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004934 File Offset: 0x00002B34
		private bool IsInIKPass()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.IsInIKPass_Injected(intPtr);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004958 File Offset: 0x00002B58
		[FreeFunction(Name = "AnimatorBindings::SetFloatString", HasExplicitThis = true)]
		private unsafe void SetFloatString(string name, float value)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.SetFloatString_Injected(intPtr, ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x000049C0 File Offset: 0x00002BC0
		[FreeFunction(Name = "AnimatorBindings::SetFloatID", HasExplicitThis = true)]
		private void SetFloatID(int id, float value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetFloatID_Injected(intPtr, id, value);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000049E4 File Offset: 0x00002BE4
		[FreeFunction(Name = "AnimatorBindings::GetFloatString", HasExplicitThis = true)]
		private unsafe float GetFloatString(string name)
		{
			float floatString_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				floatString_Injected = Animator.GetFloatString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return floatString_Injected;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00004A4C File Offset: 0x00002C4C
		[FreeFunction(Name = "AnimatorBindings::GetFloatID", HasExplicitThis = true)]
		private float GetFloatID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetFloatID_Injected(intPtr, id);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00004A70 File Offset: 0x00002C70
		[FreeFunction(Name = "AnimatorBindings::SetBoolString", HasExplicitThis = true)]
		private unsafe void SetBoolString(string name, bool value)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.SetBoolString_Injected(intPtr, ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00004AD8 File Offset: 0x00002CD8
		[FreeFunction(Name = "AnimatorBindings::SetBoolID", HasExplicitThis = true)]
		private void SetBoolID(int id, bool value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetBoolID_Injected(intPtr, id, value);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004AFC File Offset: 0x00002CFC
		[FreeFunction(Name = "AnimatorBindings::GetBoolString", HasExplicitThis = true)]
		private unsafe bool GetBoolString(string name)
		{
			bool boolString_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				boolString_Injected = Animator.GetBoolString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return boolString_Injected;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x00004B64 File Offset: 0x00002D64
		[FreeFunction(Name = "AnimatorBindings::GetBoolID", HasExplicitThis = true)]
		private bool GetBoolID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetBoolID_Injected(intPtr, id);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00004B88 File Offset: 0x00002D88
		[FreeFunction(Name = "AnimatorBindings::SetIntegerString", HasExplicitThis = true)]
		private unsafe void SetIntegerString(string name, int value)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.SetIntegerString_Injected(intPtr, ref managedSpanWrapper, value);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00004BF0 File Offset: 0x00002DF0
		[FreeFunction(Name = "AnimatorBindings::SetIntegerID", HasExplicitThis = true)]
		private void SetIntegerID(int id, int value)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetIntegerID_Injected(intPtr, id, value);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00004C14 File Offset: 0x00002E14
		[FreeFunction(Name = "AnimatorBindings::GetIntegerString", HasExplicitThis = true)]
		private unsafe int GetIntegerString(string name)
		{
			int integerString_Injected;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				integerString_Injected = Animator.GetIntegerString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return integerString_Injected;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00004C7C File Offset: 0x00002E7C
		[FreeFunction(Name = "AnimatorBindings::GetIntegerID", HasExplicitThis = true)]
		private int GetIntegerID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.GetIntegerID_Injected(intPtr, id);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00004CA0 File Offset: 0x00002EA0
		[FreeFunction(Name = "AnimatorBindings::SetTriggerString", HasExplicitThis = true)]
		private unsafe void SetTriggerString(string name)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.SetTriggerString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00004D04 File Offset: 0x00002F04
		[FreeFunction(Name = "AnimatorBindings::SetTriggerID", HasExplicitThis = true)]
		private void SetTriggerID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetTriggerID_Injected(intPtr, id);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x00004D28 File Offset: 0x00002F28
		[FreeFunction(Name = "AnimatorBindings::ResetTriggerString", HasExplicitThis = true)]
		private unsafe void ResetTriggerString(string name)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.ResetTriggerString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00004D8C File Offset: 0x00002F8C
		[FreeFunction(Name = "AnimatorBindings::ResetTriggerID", HasExplicitThis = true)]
		private void ResetTriggerID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.ResetTriggerID_Injected(intPtr, id);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x00004DB0 File Offset: 0x00002FB0
		[FreeFunction(Name = "AnimatorBindings::IsParameterControlledByCurveString", HasExplicitThis = true)]
		private unsafe bool IsParameterControlledByCurveString(string name)
		{
			bool flag;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				flag = Animator.IsParameterControlledByCurveString_Injected(intPtr, ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
			return flag;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00004E18 File Offset: 0x00003018
		[FreeFunction(Name = "AnimatorBindings::IsParameterControlledByCurveID", HasExplicitThis = true)]
		private bool IsParameterControlledByCurveID(int id)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Animator.IsParameterControlledByCurveID_Injected(intPtr, id);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x00004E3C File Offset: 0x0000303C
		[FreeFunction(Name = "AnimatorBindings::SetFloatStringDamp", HasExplicitThis = true)]
		private unsafe void SetFloatStringDamp(string name, float value, float dampTime, float deltaTime)
		{
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(name, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = name.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				Animator.SetFloatStringDamp_Injected(intPtr, ref managedSpanWrapper, value, dampTime, deltaTime);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x00004EA4 File Offset: 0x000030A4
		[FreeFunction(Name = "AnimatorBindings::SetFloatIDDamp", HasExplicitThis = true)]
		private void SetFloatIDDamp(int id, float value, float dampTime, float deltaTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.SetFloatIDDamp_Injected(intPtr, id, value, dampTime, deltaTime);
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000148 RID: 328 RVA: 0x00004ECC File Offset: 0x000030CC
		// (set) Token: 0x06000149 RID: 329 RVA: 0x00004EF0 File Offset: 0x000030F0
		public bool layersAffectMassCenter
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_layersAffectMassCenter_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_layersAffectMassCenter_Injected(intPtr, value);
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600014A RID: 330 RVA: 0x00004F14 File Offset: 0x00003114
		public float leftFeetBottomHeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_leftFeetBottomHeight_Injected(intPtr);
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00004F38 File Offset: 0x00003138
		public float rightFeetBottomHeight
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_rightFeetBottomHeight_Injected(intPtr);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00004F5C File Offset: 0x0000315C
		[NativeConditional("UNITY_EDITOR")]
		internal bool supportsOnAnimatorMove
		{
			[NativeMethod("SupportsOnAnimatorMove")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_supportsOnAnimatorMove_Injected(intPtr);
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00004F80 File Offset: 0x00003180
		[NativeConditional("UNITY_EDITOR")]
		internal void OnUpdateModeChanged()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.OnUpdateModeChanged_Injected(intPtr);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00004FA4 File Offset: 0x000031A4
		[NativeConditional("UNITY_EDITOR")]
		internal void OnCullingModeChanged()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.OnCullingModeChanged_Injected(intPtr);
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00004FC8 File Offset: 0x000031C8
		[NativeConditional("UNITY_EDITOR")]
		internal void WriteDefaultPose()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.WriteDefaultPose_Injected(intPtr);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00004FEC File Offset: 0x000031EC
		[NativeMethod("UpdateWithDelta")]
		public void Update(float deltaTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.Update_Injected(intPtr, deltaTime);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000500F File Offset: 0x0000320F
		public void Rebind()
		{
			this.Rebind(true);
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000501C File Offset: 0x0000321C
		private void Rebind(bool writeDefaultValues)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.Rebind_Injected(intPtr, writeDefaultValues);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00005040 File Offset: 0x00003240
		public void ApplyBuiltinRootMotion()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.ApplyBuiltinRootMotion_Injected(intPtr);
		}

		// Token: 0x06000154 RID: 340 RVA: 0x00005062 File Offset: 0x00003262
		[NativeConditional("UNITY_EDITOR")]
		internal void EvaluateController()
		{
			this.EvaluateController(0f);
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00005074 File Offset: 0x00003274
		private void EvaluateController(float deltaTime)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			Animator.EvaluateController_Injected(intPtr, deltaTime);
		}

		// Token: 0x06000156 RID: 342 RVA: 0x00005098 File Offset: 0x00003298
		[NativeConditional("UNITY_EDITOR")]
		internal string GetCurrentStateName(int layerIndex)
		{
			return this.GetAnimatorStateName(layerIndex, true);
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000050B4 File Offset: 0x000032B4
		[NativeConditional("UNITY_EDITOR")]
		internal string GetNextStateName(int layerIndex)
		{
			return this.GetAnimatorStateName(layerIndex, false);
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000050D0 File Offset: 0x000032D0
		[NativeConditional("UNITY_EDITOR")]
		private string GetAnimatorStateName(int layerIndex, bool current)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Animator.GetAnimatorStateName_Injected(intPtr, layerIndex, current, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x00005114 File Offset: 0x00003314
		internal string ResolveHash(int hash)
		{
			string stringAndDispose;
			try
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ManagedSpanWrapper managedSpanWrapper;
				Animator.ResolveHash_Injected(intPtr, hash, out managedSpanWrapper);
			}
			finally
			{
				ManagedSpanWrapper managedSpanWrapper;
				stringAndDispose = OutStringMarshaller.GetStringAndDispose(managedSpanWrapper);
			}
			return stringAndDispose;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00005154 File Offset: 0x00003354
		// (set) Token: 0x0600015B RID: 347 RVA: 0x00005178 File Offset: 0x00003378
		public bool logWarnings
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_logWarnings_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_logWarnings_Injected(intPtr, value);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000519C File Offset: 0x0000339C
		// (set) Token: 0x0600015D RID: 349 RVA: 0x000051C0 File Offset: 0x000033C0
		public bool fireEvents
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_fireEvents_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_fireEvents_Injected(intPtr, value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600015E RID: 350 RVA: 0x000051E4 File Offset: 0x000033E4
		// (set) Token: 0x0600015F RID: 351 RVA: 0x000051FC File Offset: 0x000033FC
		[Obsolete("keepAnimatorControllerStateOnDisable is deprecated, use keepAnimatorStateOnDisable instead. (UnityUpgradable) -> keepAnimatorStateOnDisable", false)]
		public bool keepAnimatorControllerStateOnDisable
		{
			get
			{
				return this.keepAnimatorStateOnDisable;
			}
			set
			{
				this.keepAnimatorStateOnDisable = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00005208 File Offset: 0x00003408
		// (set) Token: 0x06000161 RID: 353 RVA: 0x0000522C File Offset: 0x0000342C
		public bool keepAnimatorStateOnDisable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_keepAnimatorStateOnDisable_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_keepAnimatorStateOnDisable_Injected(intPtr, value);
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00005250 File Offset: 0x00003450
		// (set) Token: 0x06000163 RID: 355 RVA: 0x00005274 File Offset: 0x00003474
		public bool writeDefaultValuesOnDisable
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return Animator.get_writeDefaultValuesOnDisable_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<Animator>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Animator.set_writeDefaultValuesOnDisable_Injected(intPtr, value);
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00005298 File Offset: 0x00003498
		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(string name)
		{
			return Vector3.zero;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000052B0 File Offset: 0x000034B0
		[Obsolete("GetVector is deprecated.")]
		public Vector3 GetVector(int id)
		{
			return Vector3.zero;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("SetVector is deprecated.")]
		public void SetVector(string name, Vector3 value)
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("SetVector is deprecated.")]
		public void SetVector(int id, Vector3 value)
		{
		}

		// Token: 0x06000168 RID: 360 RVA: 0x000052C8 File Offset: 0x000034C8
		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(string name)
		{
			return Quaternion.identity;
		}

		// Token: 0x06000169 RID: 361 RVA: 0x000052E0 File Offset: 0x000034E0
		[Obsolete("GetQuaternion is deprecated.")]
		public Quaternion GetQuaternion(int id)
		{
			return Quaternion.identity;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(string name, Quaternion value)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("SetQuaternion is deprecated.")]
		public void SetQuaternion(int id, Quaternion value)
		{
		}

		// Token: 0x0600016D RID: 365
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isOptimizable_Injected(IntPtr _unity_self);

		// Token: 0x0600016E RID: 366
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isHuman_Injected(IntPtr _unity_self);

		// Token: 0x0600016F RID: 367
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasRootMotion_Injected(IntPtr _unity_self);

		// Token: 0x06000170 RID: 368
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isRootPositionOrRotationControlledByCurves_Injected(IntPtr _unity_self);

		// Token: 0x06000171 RID: 369
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_humanScale_Injected(IntPtr _unity_self);

		// Token: 0x06000172 RID: 370
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isInitialized_Injected(IntPtr _unity_self);

		// Token: 0x06000173 RID: 371
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_deltaPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000174 RID: 372
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_deltaRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x06000175 RID: 373
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_velocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000176 RID: 374
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_angularVelocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000177 RID: 375
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rootPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000178 RID: 376
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rootPosition_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000179 RID: 377
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_rootRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x0600017A RID: 378
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_rootRotation_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x0600017B RID: 379
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_applyRootMotion_Injected(IntPtr _unity_self);

		// Token: 0x0600017C RID: 380
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_applyRootMotion_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600017D RID: 381
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_linearVelocityBlending_Injected(IntPtr _unity_self);

		// Token: 0x0600017E RID: 382
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_linearVelocityBlending_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600017F RID: 383
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_animatePhysics_Injected(IntPtr _unity_self);

		// Token: 0x06000180 RID: 384
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_animatePhysics_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000181 RID: 385
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorUpdateMode get_updateMode_Injected(IntPtr _unity_self);

		// Token: 0x06000182 RID: 386
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updateMode_Injected(IntPtr _unity_self, AnimatorUpdateMode value);

		// Token: 0x06000183 RID: 387
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasTransformHierarchy_Injected(IntPtr _unity_self);

		// Token: 0x06000184 RID: 388
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_allowConstantClipSamplingOptimization_Injected(IntPtr _unity_self);

		// Token: 0x06000185 RID: 389
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_allowConstantClipSamplingOptimization_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000186 RID: 390
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_gravityWeight_Injected(IntPtr _unity_self);

		// Token: 0x06000187 RID: 391
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bodyPositionInternal_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x06000188 RID: 392
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bodyPositionInternal_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x06000189 RID: 393
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_bodyRotationInternal_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x0600018A RID: 394
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_bodyRotationInternal_Injected(IntPtr _unity_self, [In] ref Quaternion value);

		// Token: 0x0600018B RID: 395
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGoalPosition_Injected(IntPtr _unity_self, AvatarIKGoal goal, out Vector3 ret);

		// Token: 0x0600018C RID: 396
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGoalPosition_Injected(IntPtr _unity_self, AvatarIKGoal goal, [In] ref Vector3 goalPosition);

		// Token: 0x0600018D RID: 397
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetGoalRotation_Injected(IntPtr _unity_self, AvatarIKGoal goal, out Quaternion ret);

		// Token: 0x0600018E RID: 398
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGoalRotation_Injected(IntPtr _unity_self, AvatarIKGoal goal, [In] ref Quaternion goalRotation);

		// Token: 0x0600018F RID: 399
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetGoalWeightPosition_Injected(IntPtr _unity_self, AvatarIKGoal goal);

		// Token: 0x06000190 RID: 400
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGoalWeightPosition_Injected(IntPtr _unity_self, AvatarIKGoal goal, float value);

		// Token: 0x06000191 RID: 401
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetGoalWeightRotation_Injected(IntPtr _unity_self, AvatarIKGoal goal);

		// Token: 0x06000192 RID: 402
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetGoalWeightRotation_Injected(IntPtr _unity_self, AvatarIKGoal goal, float value);

		// Token: 0x06000193 RID: 403
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetHintPosition_Injected(IntPtr _unity_self, AvatarIKHint hint, out Vector3 ret);

		// Token: 0x06000194 RID: 404
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetHintPosition_Injected(IntPtr _unity_self, AvatarIKHint hint, [In] ref Vector3 hintPosition);

		// Token: 0x06000195 RID: 405
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetHintWeightPosition_Injected(IntPtr _unity_self, AvatarIKHint hint);

		// Token: 0x06000196 RID: 406
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetHintWeightPosition_Injected(IntPtr _unity_self, AvatarIKHint hint, float value);

		// Token: 0x06000197 RID: 407
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLookAtPositionInternal_Injected(IntPtr _unity_self, [In] ref Vector3 lookAtPosition);

		// Token: 0x06000198 RID: 408
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLookAtWeightInternal_Injected(IntPtr _unity_self, float weight, float bodyWeight, float headWeight, float eyesWeight, float clampWeight);

		// Token: 0x06000199 RID: 409
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoneLocalRotationInternal_Injected(IntPtr _unity_self, int humanBoneId, [In] ref Quaternion rotation);

		// Token: 0x0600019A RID: 410
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBehaviour_Injected(IntPtr _unity_self, Type type);

		// Token: 0x0600019B RID: 411
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScriptableObject[] InternalGetBehaviours_Injected(IntPtr _unity_self, Type type);

		// Token: 0x0600019C RID: 412
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScriptableObject[] InternalGetBehavioursByKey_Injected(IntPtr _unity_self, int fullPathHash, int layerIndex, Type type);

		// Token: 0x0600019D RID: 413
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_stabilizeFeet_Injected(IntPtr _unity_self);

		// Token: 0x0600019E RID: 414
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stabilizeFeet_Injected(IntPtr _unity_self, bool value);

		// Token: 0x0600019F RID: 415
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_layerCount_Injected(IntPtr _unity_self);

		// Token: 0x060001A0 RID: 416
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLayerName_Injected(IntPtr _unity_self, int layerIndex, out ManagedSpanWrapper ret);

		// Token: 0x060001A1 RID: 417
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetLayerIndex_Injected(IntPtr _unity_self, ref ManagedSpanWrapper layerName);

		// Token: 0x060001A2 RID: 418
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetLayerWeight_Injected(IntPtr _unity_self, int layerIndex);

		// Token: 0x060001A3 RID: 419
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerWeight_Injected(IntPtr _unity_self, int layerIndex, float weight);

		// Token: 0x060001A4 RID: 420
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAnimatorStateInfo_Injected(IntPtr _unity_self, int layerIndex, StateInfoIndex stateInfoIndex, out AnimatorStateInfo info);

		// Token: 0x060001A5 RID: 421
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAnimatorTransitionInfo_Injected(IntPtr _unity_self, int layerIndex, out AnimatorTransitionInfo info);

		// Token: 0x060001A6 RID: 422
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetAnimatorClipInfoCount_Injected(IntPtr _unity_self, int layerIndex, bool current);

		// Token: 0x060001A7 RID: 423
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorClipInfo[] GetCurrentAnimatorClipInfo_Injected(IntPtr _unity_self, int layerIndex);

		// Token: 0x060001A8 RID: 424
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorClipInfo[] GetNextAnimatorClipInfo_Injected(IntPtr _unity_self, int layerIndex);

		// Token: 0x060001A9 RID: 425
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAnimatorClipInfoInternal_Injected(IntPtr _unity_self, int layerIndex, bool isCurrent, object clips);

		// Token: 0x060001AA RID: 426
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInTransition_Injected(IntPtr _unity_self, int layerIndex);

		// Token: 0x060001AB RID: 427
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorControllerParameter[] get_parameters_Injected(IntPtr _unity_self);

		// Token: 0x060001AC RID: 428
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_parameterCount_Injected(IntPtr _unity_self);

		// Token: 0x060001AD RID: 429
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorControllerParameter GetParameterInternal_Injected(IntPtr _unity_self, int index);

		// Token: 0x060001AE RID: 430
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_feetPivotActive_Injected(IntPtr _unity_self);

		// Token: 0x060001AF RID: 431
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_feetPivotActive_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001B0 RID: 432
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_pivotWeight_Injected(IntPtr _unity_self);

		// Token: 0x060001B1 RID: 433
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pivotPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060001B2 RID: 434
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void MatchTarget_Injected(IntPtr _unity_self, [In] ref Vector3 matchPosition, [In] ref Quaternion matchRotation, int targetBodyPart, [In] ref MatchTargetWeightMask weightMask, float startNormalizedTime, float targetNormalizedTime, bool completeMatch);

		// Token: 0x060001B3 RID: 435
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InterruptMatchTarget_Injected(IntPtr _unity_self, [UnityEngine.Internal.DefaultValue("true")] bool completeMatch);

		// Token: 0x060001B4 RID: 436
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isMatchingTarget_Injected(IntPtr _unity_self);

		// Token: 0x060001B5 RID: 437
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_speed_Injected(IntPtr _unity_self);

		// Token: 0x060001B6 RID: 438
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_speed_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001B7 RID: 439
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CrossFadeInFixedTime_Injected(IntPtr _unity_self, int stateHashName, float fixedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float fixedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime);

		// Token: 0x060001B8 RID: 440
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteDefaultValues_Injected(IntPtr _unity_self);

		// Token: 0x060001B9 RID: 441
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CrossFade_Injected(IntPtr _unity_self, int stateHashName, float normalizedTransitionDuration, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTimeOffset, [UnityEngine.Internal.DefaultValue("0.0f")] float normalizedTransitionTime);

		// Token: 0x060001BA RID: 442
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PlayInFixedTime_Injected(IntPtr _unity_self, int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float fixedTime);

		// Token: 0x060001BB RID: 443
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(IntPtr _unity_self, int stateNameHash, [UnityEngine.Internal.DefaultValue("-1")] int layer, [UnityEngine.Internal.DefaultValue("float.NegativeInfinity")] float normalizedTime);

		// Token: 0x060001BC RID: 444
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTarget_Injected(IntPtr _unity_self, AvatarTarget targetIndex, float targetNormalizedTime);

		// Token: 0x060001BD RID: 445
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_targetPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060001BE RID: 446
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_targetRotation_Injected(IntPtr _unity_self, out Quaternion ret);

		// Token: 0x060001BF RID: 447
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsBoneTransform_Injected(IntPtr _unity_self, IntPtr transform);

		// Token: 0x060001C0 RID: 448
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_avatarRoot_Injected(IntPtr _unity_self);

		// Token: 0x060001C1 RID: 449
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetBoneTransformInternal_Injected(IntPtr _unity_self, int humanBoneId);

		// Token: 0x060001C2 RID: 450
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorCullingMode get_cullingMode_Injected(IntPtr _unity_self);

		// Token: 0x060001C3 RID: 451
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_cullingMode_Injected(IntPtr _unity_self, AnimatorCullingMode value);

		// Token: 0x060001C4 RID: 452
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartPlayback_Injected(IntPtr _unity_self);

		// Token: 0x060001C5 RID: 453
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopPlayback_Injected(IntPtr _unity_self);

		// Token: 0x060001C6 RID: 454
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_playbackTime_Injected(IntPtr _unity_self);

		// Token: 0x060001C7 RID: 455
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_playbackTime_Injected(IntPtr _unity_self, float value);

		// Token: 0x060001C8 RID: 456
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StartRecording_Injected(IntPtr _unity_self, int frameCount);

		// Token: 0x060001C9 RID: 457
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void StopRecording_Injected(IntPtr _unity_self);

		// Token: 0x060001CA RID: 458
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetRecorderStartTime_Injected(IntPtr _unity_self);

		// Token: 0x060001CB RID: 459
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetRecorderStopTime_Injected(IntPtr _unity_self);

		// Token: 0x060001CC RID: 460
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AnimatorRecorderMode get_recorderMode_Injected(IntPtr _unity_self);

		// Token: 0x060001CD RID: 461
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_runtimeAnimatorController_Injected(IntPtr _unity_self);

		// Token: 0x060001CE RID: 462
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_runtimeAnimatorController_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060001CF RID: 463
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasBoundPlayables_Injected(IntPtr _unity_self);

		// Token: 0x060001D0 RID: 464
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ClearInternalControllerPlayable_Injected(IntPtr _unity_self);

		// Token: 0x060001D1 RID: 465
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool HasState_Injected(IntPtr _unity_self, int layerIndex, int stateID);

		// Token: 0x060001D2 RID: 466
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int StringToHash_Injected(ref ManagedSpanWrapper name);

		// Token: 0x060001D3 RID: 467
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr get_avatar_Injected(IntPtr _unity_self);

		// Token: 0x060001D4 RID: 468
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_avatar_Injected(IntPtr _unity_self, IntPtr value);

		// Token: 0x060001D5 RID: 469
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetStats_Injected(IntPtr _unity_self, out ManagedSpanWrapper ret);

		// Token: 0x060001D6 RID: 470
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCurrentGraph_Injected(IntPtr _unity_self, ref PlayableGraph graph);

		// Token: 0x060001D7 RID: 471
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsInIKPass_Injected(IntPtr _unity_self);

		// Token: 0x060001D8 RID: 472
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, float value);

		// Token: 0x060001D9 RID: 473
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatID_Injected(IntPtr _unity_self, int id, float value);

		// Token: 0x060001DA RID: 474
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060001DB RID: 475
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetFloatID_Injected(IntPtr _unity_self, int id);

		// Token: 0x060001DC RID: 476
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoolString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, bool value);

		// Token: 0x060001DD RID: 477
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetBoolID_Injected(IntPtr _unity_self, int id, bool value);

		// Token: 0x060001DE RID: 478
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060001DF RID: 479
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetBoolID_Injected(IntPtr _unity_self, int id);

		// Token: 0x060001E0 RID: 480
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntegerString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, int value);

		// Token: 0x060001E1 RID: 481
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetIntegerID_Injected(IntPtr _unity_self, int id, int value);

		// Token: 0x060001E2 RID: 482
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntegerString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060001E3 RID: 483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetIntegerID_Injected(IntPtr _unity_self, int id);

		// Token: 0x060001E4 RID: 484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTriggerString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060001E5 RID: 485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetTriggerID_Injected(IntPtr _unity_self, int id);

		// Token: 0x060001E6 RID: 486
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetTriggerString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060001E7 RID: 487
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetTriggerID_Injected(IntPtr _unity_self, int id);

		// Token: 0x060001E8 RID: 488
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsParameterControlledByCurveString_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name);

		// Token: 0x060001E9 RID: 489
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsParameterControlledByCurveID_Injected(IntPtr _unity_self, int id);

		// Token: 0x060001EA RID: 490
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatStringDamp_Injected(IntPtr _unity_self, ref ManagedSpanWrapper name, float value, float dampTime, float deltaTime);

		// Token: 0x060001EB RID: 491
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetFloatIDDamp_Injected(IntPtr _unity_self, int id, float value, float dampTime, float deltaTime);

		// Token: 0x060001EC RID: 492
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_layersAffectMassCenter_Injected(IntPtr _unity_self);

		// Token: 0x060001ED RID: 493
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_layersAffectMassCenter_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060001EE RID: 494
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_leftFeetBottomHeight_Injected(IntPtr _unity_self);

		// Token: 0x060001EF RID: 495
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_rightFeetBottomHeight_Injected(IntPtr _unity_self);

		// Token: 0x060001F0 RID: 496
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_supportsOnAnimatorMove_Injected(IntPtr _unity_self);

		// Token: 0x060001F1 RID: 497
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OnUpdateModeChanged_Injected(IntPtr _unity_self);

		// Token: 0x060001F2 RID: 498
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void OnCullingModeChanged_Injected(IntPtr _unity_self);

		// Token: 0x060001F3 RID: 499
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteDefaultPose_Injected(IntPtr _unity_self);

		// Token: 0x060001F4 RID: 500
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Update_Injected(IntPtr _unity_self, float deltaTime);

		// Token: 0x060001F5 RID: 501
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Rebind_Injected(IntPtr _unity_self, bool writeDefaultValues);

		// Token: 0x060001F6 RID: 502
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ApplyBuiltinRootMotion_Injected(IntPtr _unity_self);

		// Token: 0x060001F7 RID: 503
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EvaluateController_Injected(IntPtr _unity_self, float deltaTime);

		// Token: 0x060001F8 RID: 504
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAnimatorStateName_Injected(IntPtr _unity_self, int layerIndex, bool current, out ManagedSpanWrapper ret);

		// Token: 0x060001F9 RID: 505
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResolveHash_Injected(IntPtr _unity_self, int hash, out ManagedSpanWrapper ret);

		// Token: 0x060001FA RID: 506
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_logWarnings_Injected(IntPtr _unity_self);

		// Token: 0x060001FB RID: 507
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_logWarnings_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060001FC RID: 508
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_fireEvents_Injected(IntPtr _unity_self);

		// Token: 0x060001FD RID: 509
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_fireEvents_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060001FE RID: 510
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_keepAnimatorStateOnDisable_Injected(IntPtr _unity_self);

		// Token: 0x060001FF RID: 511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_keepAnimatorStateOnDisable_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000200 RID: 512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_writeDefaultValuesOnDisable_Injected(IntPtr _unity_self);

		// Token: 0x06000201 RID: 513
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_writeDefaultValuesOnDisable_Injected(IntPtr _unity_self, bool value);
	}
}
