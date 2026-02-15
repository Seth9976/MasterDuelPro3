using System;

namespace Spine
{
	// Token: 0x0200006F RID: 111
	public class PhysicsConstraintData : ConstraintData
	{
		// Token: 0x060003AE RID: 942 RVA: 0x000108B7 File Offset: 0x0000EAB7
		public PhysicsConstraintData(string name)
			: base(name)
		{
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003AF RID: 943 RVA: 0x000108C0 File Offset: 0x0000EAC0
		public BoneData Bone
		{
			get
			{
				return this.bone;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003B0 RID: 944 RVA: 0x000108C8 File Offset: 0x0000EAC8
		// (set) Token: 0x060003B1 RID: 945 RVA: 0x000108D0 File Offset: 0x0000EAD0
		public float Step
		{
			get
			{
				return this.step;
			}
			set
			{
				this.step = value;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003B2 RID: 946 RVA: 0x000108D9 File Offset: 0x0000EAD9
		// (set) Token: 0x060003B3 RID: 947 RVA: 0x000108E1 File Offset: 0x0000EAE1
		public float X
		{
			get
			{
				return this.x;
			}
			set
			{
				this.x = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x000108EA File Offset: 0x0000EAEA
		// (set) Token: 0x060003B5 RID: 949 RVA: 0x000108F2 File Offset: 0x0000EAF2
		public float Y
		{
			get
			{
				return this.y;
			}
			set
			{
				this.y = value;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003B6 RID: 950 RVA: 0x000108FB File Offset: 0x0000EAFB
		// (set) Token: 0x060003B7 RID: 951 RVA: 0x00010903 File Offset: 0x0000EB03
		public float Rotate
		{
			get
			{
				return this.rotate;
			}
			set
			{
				this.rotate = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060003B8 RID: 952 RVA: 0x0001090C File Offset: 0x0000EB0C
		// (set) Token: 0x060003B9 RID: 953 RVA: 0x00010914 File Offset: 0x0000EB14
		public float ScaleX
		{
			get
			{
				return this.scaleX;
			}
			set
			{
				this.scaleX = value;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001091D File Offset: 0x0000EB1D
		// (set) Token: 0x060003BB RID: 955 RVA: 0x00010925 File Offset: 0x0000EB25
		public float ShearX
		{
			get
			{
				return this.shearX;
			}
			set
			{
				this.shearX = value;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0001092E File Offset: 0x0000EB2E
		// (set) Token: 0x060003BD RID: 957 RVA: 0x00010936 File Offset: 0x0000EB36
		public float Limit
		{
			get
			{
				return this.limit;
			}
			set
			{
				this.limit = value;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003BE RID: 958 RVA: 0x0001093F File Offset: 0x0000EB3F
		// (set) Token: 0x060003BF RID: 959 RVA: 0x00010947 File Offset: 0x0000EB47
		public float Inertia
		{
			get
			{
				return this.inertia;
			}
			set
			{
				this.inertia = value;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00010950 File Offset: 0x0000EB50
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x00010958 File Offset: 0x0000EB58
		public float Strength
		{
			get
			{
				return this.strength;
			}
			set
			{
				this.strength = value;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00010961 File Offset: 0x0000EB61
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x00010969 File Offset: 0x0000EB69
		public float Damping
		{
			get
			{
				return this.damping;
			}
			set
			{
				this.damping = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00010972 File Offset: 0x0000EB72
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0001097A File Offset: 0x0000EB7A
		public float MassInverse
		{
			get
			{
				return this.massInverse;
			}
			set
			{
				this.massInverse = value;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00010983 File Offset: 0x0000EB83
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x0001098B File Offset: 0x0000EB8B
		public float Wind
		{
			get
			{
				return this.wind;
			}
			set
			{
				this.wind = value;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003C8 RID: 968 RVA: 0x00010994 File Offset: 0x0000EB94
		// (set) Token: 0x060003C9 RID: 969 RVA: 0x0001099C File Offset: 0x0000EB9C
		public float Gravity
		{
			get
			{
				return this.gravity;
			}
			set
			{
				this.gravity = value;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003CA RID: 970 RVA: 0x000109A5 File Offset: 0x0000EBA5
		// (set) Token: 0x060003CB RID: 971 RVA: 0x000109AD File Offset: 0x0000EBAD
		public float Mix
		{
			get
			{
				return this.mix;
			}
			set
			{
				this.mix = value;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060003CC RID: 972 RVA: 0x000109B6 File Offset: 0x0000EBB6
		// (set) Token: 0x060003CD RID: 973 RVA: 0x000109BE File Offset: 0x0000EBBE
		public bool InertiaGlobal
		{
			get
			{
				return this.inertiaGlobal;
			}
			set
			{
				this.inertiaGlobal = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000109C7 File Offset: 0x0000EBC7
		// (set) Token: 0x060003CF RID: 975 RVA: 0x000109CF File Offset: 0x0000EBCF
		public bool StrengthGlobal
		{
			get
			{
				return this.strengthGlobal;
			}
			set
			{
				this.strengthGlobal = value;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000109D8 File Offset: 0x0000EBD8
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x000109E0 File Offset: 0x0000EBE0
		public bool DampingGlobal
		{
			get
			{
				return this.dampingGlobal;
			}
			set
			{
				this.dampingGlobal = value;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x000109E9 File Offset: 0x0000EBE9
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x000109F1 File Offset: 0x0000EBF1
		public bool MassGlobal
		{
			get
			{
				return this.massGlobal;
			}
			set
			{
				this.massGlobal = value;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x000109FA File Offset: 0x0000EBFA
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00010A02 File Offset: 0x0000EC02
		public bool WindGlobal
		{
			get
			{
				return this.windGlobal;
			}
			set
			{
				this.windGlobal = value;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x00010A0B File Offset: 0x0000EC0B
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00010A13 File Offset: 0x0000EC13
		public bool GravityGlobal
		{
			get
			{
				return this.gravityGlobal;
			}
			set
			{
				this.gravityGlobal = value;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060003D8 RID: 984 RVA: 0x00010A1C File Offset: 0x0000EC1C
		// (set) Token: 0x060003D9 RID: 985 RVA: 0x00010A24 File Offset: 0x0000EC24
		public bool MixGlobal
		{
			get
			{
				return this.mixGlobal;
			}
			set
			{
				this.mixGlobal = value;
			}
		}

		// Token: 0x04000228 RID: 552
		internal BoneData bone;

		// Token: 0x04000229 RID: 553
		internal float x;

		// Token: 0x0400022A RID: 554
		internal float y;

		// Token: 0x0400022B RID: 555
		internal float rotate;

		// Token: 0x0400022C RID: 556
		internal float scaleX;

		// Token: 0x0400022D RID: 557
		internal float shearX;

		// Token: 0x0400022E RID: 558
		internal float limit;

		// Token: 0x0400022F RID: 559
		internal float step;

		// Token: 0x04000230 RID: 560
		internal float inertia;

		// Token: 0x04000231 RID: 561
		internal float strength;

		// Token: 0x04000232 RID: 562
		internal float damping;

		// Token: 0x04000233 RID: 563
		internal float massInverse;

		// Token: 0x04000234 RID: 564
		internal float wind;

		// Token: 0x04000235 RID: 565
		internal float gravity;

		// Token: 0x04000236 RID: 566
		internal float mix;

		// Token: 0x04000237 RID: 567
		internal bool inertiaGlobal;

		// Token: 0x04000238 RID: 568
		internal bool strengthGlobal;

		// Token: 0x04000239 RID: 569
		internal bool dampingGlobal;

		// Token: 0x0400023A RID: 570
		internal bool massGlobal;

		// Token: 0x0400023B RID: 571
		internal bool windGlobal;

		// Token: 0x0400023C RID: 572
		internal bool gravityGlobal;

		// Token: 0x0400023D RID: 573
		internal bool mixGlobal;
	}
}
