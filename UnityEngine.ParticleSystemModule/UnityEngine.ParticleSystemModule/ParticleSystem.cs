using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000002 RID: 2
	[RequireComponent(typeof(Transform))]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	[NativeHeader("Modules/ParticleSystem/ParticleSystemGeometryJob.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Modules/ParticleSystem/ParticleSystem.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemScriptBindings.h")]
	[NativeHeader("ParticleSystemScriptingClasses.h")]
	[NativeHeader("Modules/ParticleSystem/ScriptBindings/ParticleSystemModulesScriptBindings.h")]
	public sealed class ParticleSystem : Component
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[Obsolete("Emit with specific parameters is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(Vector3 position, Vector3 velocity, float size, float lifetime, Color32 color)
		{
			ParticleSystem.Particle particle = default(ParticleSystem.Particle);
			particle.position = position;
			particle.velocity = velocity;
			particle.lifetime = lifetime;
			particle.startLifetime = lifetime;
			particle.startSize = size;
			particle.rotation3D = Vector3.zero;
			particle.angularVelocity3D = Vector3.zero;
			particle.startColor = color;
			particle.randomSeed = 5U;
			this.EmitOld_Internal(ref particle);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020CB File Offset: 0x000002CB
		[Obsolete("Emit with a single particle structure is deprecated. Pass a ParticleSystem.EmitParams parameter instead, which allows you to override some/all of the emission properties", false)]
		public void Emit(ParticleSystem.Particle particle)
		{
			this.EmitOld_Internal(ref particle);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020D8 File Offset: 0x000002D8
		public float time
		{
			[NativeName("SyncJobs(false)->GetSecPosition")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_time_Injected(intPtr);
			}
		}

		// Token: 0x17000002 RID: 2
		// (set) Token: 0x06000004 RID: 4 RVA: 0x000020FC File Offset: 0x000002FC
		public uint randomSeed
		{
			[NativeName("SyncJobs(false)->SetRandomSeed")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.set_randomSeed_Injected(intPtr, value);
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002120 File Offset: 0x00000320
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002144 File Offset: 0x00000344
		public bool useAutoRandomSeed
		{
			[NativeName("GetAutoRandomSeed")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return ParticleSystem.get_useAutoRandomSeed_Injected(intPtr);
			}
			[NativeName("SyncJobs(false)->SetAutoRandomSeed")]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				ParticleSystem.set_useAutoRandomSeed_Injected(intPtr, value);
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002168 File Offset: 0x00000368
		[FreeFunction(Name = "ParticleSystemScriptBindings::Simulate", HasExplicitThis = true)]
		public void Simulate(float t, [DefaultValue("true")] bool withChildren, [DefaultValue("true")] bool restart, [DefaultValue("true")] bool fixedTimeStep)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Simulate_Injected(intPtr, t, withChildren, restart, fixedTimeStep);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002190 File Offset: 0x00000390
		[FreeFunction(Name = "ParticleSystemScriptBindings::Play", HasExplicitThis = true)]
		public void Play([DefaultValue("true")] bool withChildren)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Play_Injected(intPtr, withChildren);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021B3 File Offset: 0x000003B3
		public void Play()
		{
			this.Play(true);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C0 File Offset: 0x000003C0
		[FreeFunction(Name = "ParticleSystemScriptBindings::Stop", HasExplicitThis = true)]
		public void Stop([DefaultValue("true")] bool withChildren, [DefaultValue("ParticleSystemStopBehavior.StopEmitting")] ParticleSystemStopBehavior stopBehavior)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Stop_Injected(intPtr, withChildren, stopBehavior);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021E4 File Offset: 0x000003E4
		public void Stop([DefaultValue("true")] bool withChildren)
		{
			this.Stop(withChildren, ParticleSystemStopBehavior.StopEmitting);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021F0 File Offset: 0x000003F0
		public void Stop()
		{
			this.Stop(true);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000021FB File Offset: 0x000003FB
		[RequiredByNativeCode]
		public void Emit(int count)
		{
			this.Emit_Internal(count);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002208 File Offset: 0x00000408
		[NativeName("SyncJobs()->Emit")]
		private void Emit_Internal(int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Emit_Internal_Injected(intPtr, count);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000222C File Offset: 0x0000042C
		[NativeName("SyncJobs()->EmitParticlesExternal")]
		public void Emit(ParticleSystem.EmitParams emitParams, int count)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.Emit_Injected(intPtr, ref emitParams, count);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002254 File Offset: 0x00000454
		[NativeName("SyncJobs()->EmitParticleExternal")]
		private void EmitOld_Internal(ref ParticleSystem.Particle particle)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<ParticleSystem>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			ParticleSystem.EmitOld_Internal_Injected(intPtr, ref particle);
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002278 File Offset: 0x00000478
		public ParticleSystem.MainModule main
		{
			get
			{
				return new ParticleSystem.MainModule(this);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002290 File Offset: 0x00000490
		public ParticleSystem.SubEmittersModule subEmitters
		{
			get
			{
				return new ParticleSystem.SubEmittersModule(this);
			}
		}

		// Token: 0x06000013 RID: 19
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_time_Injected(IntPtr _unity_self);

		// Token: 0x06000014 RID: 20
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_randomSeed_Injected(IntPtr _unity_self, uint value);

		// Token: 0x06000015 RID: 21
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_useAutoRandomSeed_Injected(IntPtr _unity_self);

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_useAutoRandomSeed_Injected(IntPtr _unity_self, bool value);

		// Token: 0x06000017 RID: 23
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Simulate_Injected(IntPtr _unity_self, float t, [DefaultValue("true")] bool withChildren, [DefaultValue("true")] bool restart, [DefaultValue("true")] bool fixedTimeStep);

		// Token: 0x06000018 RID: 24
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Play_Injected(IntPtr _unity_self, [DefaultValue("true")] bool withChildren);

		// Token: 0x06000019 RID: 25
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self, [DefaultValue("true")] bool withChildren, [DefaultValue("ParticleSystemStopBehavior.StopEmitting")] ParticleSystemStopBehavior stopBehavior);

		// Token: 0x0600001A RID: 26
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Emit_Internal_Injected(IntPtr _unity_self, int count);

		// Token: 0x0600001B RID: 27
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Emit_Injected(IntPtr _unity_self, [In] ref ParticleSystem.EmitParams emitParams, int count);

		// Token: 0x0600001C RID: 28
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void EmitOld_Internal_Injected(IntPtr _unity_self, ref ParticleSystem.Particle particle);

		// Token: 0x02000003 RID: 3
		public struct MainModule
		{
			// Token: 0x0600001D RID: 29 RVA: 0x000022A8 File Offset: 0x000004A8
			internal MainModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x17000006 RID: 6
			// (get) Token: 0x0600001E RID: 30
			public extern float duration
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
			}

			// Token: 0x17000007 RID: 7
			// (get) Token: 0x0600001F RID: 31
			public extern bool loop
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
			}

			// Token: 0x17000008 RID: 8
			// (set) Token: 0x06000020 RID: 32
			public extern float simulationSpeed
			{
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x17000009 RID: 9
			// (set) Token: 0x06000021 RID: 33
			public extern bool playOnAwake
			{
				[NativeThrows]
				[MethodImpl(MethodImplOptions.InternalCall)]
				set;
			}

			// Token: 0x04000001 RID: 1
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000004 RID: 4
		public struct SubEmittersModule
		{
			// Token: 0x06000022 RID: 34 RVA: 0x000022B2 File Offset: 0x000004B2
			internal SubEmittersModule(ParticleSystem particleSystem)
			{
				this.m_ParticleSystem = particleSystem;
			}

			// Token: 0x1700000A RID: 10
			// (get) Token: 0x06000023 RID: 35
			public extern int subEmittersCount
			{
				[MethodImpl(MethodImplOptions.InternalCall)]
				get;
			}

			// Token: 0x06000024 RID: 36 RVA: 0x000022BC File Offset: 0x000004BC
			[NativeThrows]
			public ParticleSystem GetSubEmitterSystem(int index)
			{
				return Unmarshal.UnmarshalUnityObject<ParticleSystem>(ParticleSystem.SubEmittersModule.GetSubEmitterSystem_Injected(ref this, index));
			}

			// Token: 0x06000025 RID: 37
			[MethodImpl(MethodImplOptions.InternalCall)]
			private static extern IntPtr GetSubEmitterSystem_Injected(ref ParticleSystem.SubEmittersModule _unity_self, int index);

			// Token: 0x04000002 RID: 2
			internal ParticleSystem m_ParticleSystem;
		}

		// Token: 0x02000005 RID: 5
		[RequiredByNativeCode("particleSystemParticle", Optional = true)]
		public struct Particle
		{
			// Token: 0x1700000B RID: 11
			// (set) Token: 0x06000026 RID: 38 RVA: 0x000022D5 File Offset: 0x000004D5
			[Obsolete("Please use Particle.remainingLifetime instead. (UnityUpgradable) -> UnityEngine.ParticleSystem/Particle.remainingLifetime", false)]
			public float lifetime
			{
				set
				{
					this.remainingLifetime = value;
				}
			}

			// Token: 0x1700000C RID: 12
			// (set) Token: 0x06000027 RID: 39 RVA: 0x000022E0 File Offset: 0x000004E0
			public Vector3 position
			{
				set
				{
					this.m_Position = value;
				}
			}

			// Token: 0x1700000D RID: 13
			// (set) Token: 0x06000028 RID: 40 RVA: 0x000022EA File Offset: 0x000004EA
			public Vector3 velocity
			{
				set
				{
					this.m_Velocity = value;
				}
			}

			// Token: 0x1700000E RID: 14
			// (set) Token: 0x06000029 RID: 41 RVA: 0x000022F4 File Offset: 0x000004F4
			public float remainingLifetime
			{
				set
				{
					this.m_Lifetime = value;
				}
			}

			// Token: 0x1700000F RID: 15
			// (set) Token: 0x0600002A RID: 42 RVA: 0x000022FE File Offset: 0x000004FE
			public float startLifetime
			{
				set
				{
					this.m_StartLifetime = value;
				}
			}

			// Token: 0x17000010 RID: 16
			// (set) Token: 0x0600002B RID: 43 RVA: 0x00002308 File Offset: 0x00000508
			public Color32 startColor
			{
				set
				{
					this.m_StartColor = value;
				}
			}

			// Token: 0x17000011 RID: 17
			// (set) Token: 0x0600002C RID: 44 RVA: 0x00002312 File Offset: 0x00000512
			public uint randomSeed
			{
				set
				{
					this.m_RandomSeed = value;
				}
			}

			// Token: 0x17000012 RID: 18
			// (set) Token: 0x0600002D RID: 45 RVA: 0x0000231C File Offset: 0x0000051C
			public float startSize
			{
				set
				{
					this.m_StartSize = new Vector3(value, value, value);
				}
			}

			// Token: 0x17000013 RID: 19
			// (set) Token: 0x0600002E RID: 46 RVA: 0x0000232D File Offset: 0x0000052D
			public Vector3 rotation3D
			{
				set
				{
					this.m_Rotation = value * 0.017453292f;
					this.m_Flags |= 2U;
				}
			}

			// Token: 0x17000014 RID: 20
			// (set) Token: 0x0600002F RID: 47 RVA: 0x0000234F File Offset: 0x0000054F
			public Vector3 angularVelocity3D
			{
				set
				{
					this.m_AngularVelocity = value * 0.017453292f;
					this.m_Flags |= 2U;
				}
			}

			// Token: 0x04000003 RID: 3
			private Vector3 m_Position;

			// Token: 0x04000004 RID: 4
			private Vector3 m_Velocity;

			// Token: 0x04000005 RID: 5
			private Vector3 m_AnimatedVelocity;

			// Token: 0x04000006 RID: 6
			private Vector3 m_InitialVelocity;

			// Token: 0x04000007 RID: 7
			private Vector3 m_AxisOfRotation;

			// Token: 0x04000008 RID: 8
			private Vector3 m_Rotation;

			// Token: 0x04000009 RID: 9
			private Vector3 m_AngularVelocity;

			// Token: 0x0400000A RID: 10
			private Vector3 m_StartSize;

			// Token: 0x0400000B RID: 11
			private Color32 m_StartColor;

			// Token: 0x0400000C RID: 12
			private uint m_RandomSeed;

			// Token: 0x0400000D RID: 13
			private uint m_ParentRandomSeed;

			// Token: 0x0400000E RID: 14
			private float m_Lifetime;

			// Token: 0x0400000F RID: 15
			private float m_StartLifetime;

			// Token: 0x04000010 RID: 16
			private int m_MeshIndex;

			// Token: 0x04000011 RID: 17
			private float m_EmitAccumulator0;

			// Token: 0x04000012 RID: 18
			private float m_EmitAccumulator1;

			// Token: 0x04000013 RID: 19
			private uint m_Flags;
		}

		// Token: 0x02000006 RID: 6
		[RequiredByNativeCode]
		[NativeType(CodegenOptions.Custom, "MonoMinMaxCurve", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		[Serializable]
		internal struct MinMaxCurveBlittable
		{
			// Token: 0x04000014 RID: 20
			private ParticleSystemCurveMode m_Mode;

			// Token: 0x04000015 RID: 21
			private float m_CurveMultiplier;

			// Token: 0x04000016 RID: 22
			private IntPtr m_CurveMin;

			// Token: 0x04000017 RID: 23
			private IntPtr m_CurveMax;

			// Token: 0x04000018 RID: 24
			internal float m_ConstantMin;

			// Token: 0x04000019 RID: 25
			internal float m_ConstantMax;
		}

		// Token: 0x02000007 RID: 7
		[RequiredByNativeCode]
		[NativeType(CodegenOptions.Custom, "MonoMinMaxGradient", Header = "Runtime/Scripting/ScriptingCommonStructDefinitions.h")]
		[Serializable]
		internal struct MinMaxGradientBlittable
		{
			// Token: 0x0400001A RID: 26
			private ParticleSystemGradientMode m_Mode;

			// Token: 0x0400001B RID: 27
			private IntPtr m_GradientMin;

			// Token: 0x0400001C RID: 28
			private IntPtr m_GradientMax;

			// Token: 0x0400001D RID: 29
			private Color m_ColorMin;

			// Token: 0x0400001E RID: 30
			private Color m_ColorMax;
		}

		// Token: 0x02000008 RID: 8
		public struct EmitParams
		{
			// Token: 0x0400001F RID: 31
			[NativeName("particle")]
			private ParticleSystem.Particle m_Particle;

			// Token: 0x04000020 RID: 32
			[NativeName("positionSet")]
			private bool m_PositionSet;

			// Token: 0x04000021 RID: 33
			[NativeName("velocitySet")]
			private bool m_VelocitySet;

			// Token: 0x04000022 RID: 34
			[NativeName("axisOfRotationSet")]
			private bool m_AxisOfRotationSet;

			// Token: 0x04000023 RID: 35
			[NativeName("rotationSet")]
			private bool m_RotationSet;

			// Token: 0x04000024 RID: 36
			[NativeName("rotationalSpeedSet")]
			private bool m_AngularVelocitySet;

			// Token: 0x04000025 RID: 37
			[NativeName("startSizeSet")]
			private bool m_StartSizeSet;

			// Token: 0x04000026 RID: 38
			[NativeName("startColorSet")]
			private bool m_StartColorSet;

			// Token: 0x04000027 RID: 39
			[NativeName("randomSeedSet")]
			private bool m_RandomSeedSet;

			// Token: 0x04000028 RID: 40
			[NativeName("startLifetimeSet")]
			private bool m_StartLifetimeSet;

			// Token: 0x04000029 RID: 41
			[NativeName("meshIndexSet")]
			private bool m_MeshIndexSet;

			// Token: 0x0400002A RID: 42
			[NativeName("applyShapeToPosition")]
			private bool m_ApplyShapeToPosition;
		}
	}
}
