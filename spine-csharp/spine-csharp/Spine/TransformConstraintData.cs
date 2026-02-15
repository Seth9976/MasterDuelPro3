using System;

namespace Spine
{
	// Token: 0x02000085 RID: 133
	public class TransformConstraintData : ConstraintData
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001CE97 File Offset: 0x0001B097
		public ExposedList<BoneData> Bones
		{
			get
			{
				return this.bones;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0001CE9F File Offset: 0x0001B09F
		// (set) Token: 0x06000522 RID: 1314 RVA: 0x0001CEA7 File Offset: 0x0001B0A7
		public BoneData Target
		{
			get
			{
				return this.target;
			}
			set
			{
				this.target = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0001CEB0 File Offset: 0x0001B0B0
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x0001CEB8 File Offset: 0x0001B0B8
		public float MixRotate
		{
			get
			{
				return this.mixRotate;
			}
			set
			{
				this.mixRotate = value;
			}
		}

		// Token: 0x17000192 RID: 402
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0001CEC1 File Offset: 0x0001B0C1
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0001CEC9 File Offset: 0x0001B0C9
		public float MixX
		{
			get
			{
				return this.mixX;
			}
			set
			{
				this.mixX = value;
			}
		}

		// Token: 0x17000193 RID: 403
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x0001CED2 File Offset: 0x0001B0D2
		// (set) Token: 0x06000528 RID: 1320 RVA: 0x0001CEDA File Offset: 0x0001B0DA
		public float MixY
		{
			get
			{
				return this.mixY;
			}
			set
			{
				this.mixY = value;
			}
		}

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0001CEE3 File Offset: 0x0001B0E3
		// (set) Token: 0x0600052A RID: 1322 RVA: 0x0001CEEB File Offset: 0x0001B0EB
		public float MixScaleX
		{
			get
			{
				return this.mixScaleX;
			}
			set
			{
				this.mixScaleX = value;
			}
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		// (set) Token: 0x0600052C RID: 1324 RVA: 0x0001CEFC File Offset: 0x0001B0FC
		public float MixScaleY
		{
			get
			{
				return this.mixScaleY;
			}
			set
			{
				this.mixScaleY = value;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0001CF05 File Offset: 0x0001B105
		// (set) Token: 0x0600052E RID: 1326 RVA: 0x0001CF0D File Offset: 0x0001B10D
		public float MixShearY
		{
			get
			{
				return this.mixShearY;
			}
			set
			{
				this.mixShearY = value;
			}
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0001CF16 File Offset: 0x0001B116
		// (set) Token: 0x06000530 RID: 1328 RVA: 0x0001CF1E File Offset: 0x0001B11E
		public float OffsetRotation
		{
			get
			{
				return this.offsetRotation;
			}
			set
			{
				this.offsetRotation = value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000531 RID: 1329 RVA: 0x0001CF27 File Offset: 0x0001B127
		// (set) Token: 0x06000532 RID: 1330 RVA: 0x0001CF2F File Offset: 0x0001B12F
		public float OffsetX
		{
			get
			{
				return this.offsetX;
			}
			set
			{
				this.offsetX = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000533 RID: 1331 RVA: 0x0001CF38 File Offset: 0x0001B138
		// (set) Token: 0x06000534 RID: 1332 RVA: 0x0001CF40 File Offset: 0x0001B140
		public float OffsetY
		{
			get
			{
				return this.offsetY;
			}
			set
			{
				this.offsetY = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0001CF49 File Offset: 0x0001B149
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x0001CF51 File Offset: 0x0001B151
		public float OffsetScaleX
		{
			get
			{
				return this.offsetScaleX;
			}
			set
			{
				this.offsetScaleX = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0001CF5A File Offset: 0x0001B15A
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0001CF62 File Offset: 0x0001B162
		public float OffsetScaleY
		{
			get
			{
				return this.offsetScaleY;
			}
			set
			{
				this.offsetScaleY = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0001CF6B File Offset: 0x0001B16B
		// (set) Token: 0x0600053A RID: 1338 RVA: 0x0001CF73 File Offset: 0x0001B173
		public float OffsetShearY
		{
			get
			{
				return this.offsetShearY;
			}
			set
			{
				this.offsetShearY = value;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0001CF7C File Offset: 0x0001B17C
		// (set) Token: 0x0600053C RID: 1340 RVA: 0x0001CF84 File Offset: 0x0001B184
		public bool Relative
		{
			get
			{
				return this.relative;
			}
			set
			{
				this.relative = value;
			}
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0001CF8D File Offset: 0x0001B18D
		// (set) Token: 0x0600053E RID: 1342 RVA: 0x0001CF95 File Offset: 0x0001B195
		public bool Local
		{
			get
			{
				return this.local;
			}
			set
			{
				this.local = value;
			}
		}

		// Token: 0x0600053F RID: 1343 RVA: 0x0001CF9E File Offset: 0x0001B19E
		public TransformConstraintData(string name)
			: base(name)
		{
		}

		// Token: 0x040002EA RID: 746
		internal ExposedList<BoneData> bones = new ExposedList<BoneData>();

		// Token: 0x040002EB RID: 747
		internal BoneData target;

		// Token: 0x040002EC RID: 748
		internal float mixRotate;

		// Token: 0x040002ED RID: 749
		internal float mixX;

		// Token: 0x040002EE RID: 750
		internal float mixY;

		// Token: 0x040002EF RID: 751
		internal float mixScaleX;

		// Token: 0x040002F0 RID: 752
		internal float mixScaleY;

		// Token: 0x040002F1 RID: 753
		internal float mixShearY;

		// Token: 0x040002F2 RID: 754
		internal float offsetRotation;

		// Token: 0x040002F3 RID: 755
		internal float offsetX;

		// Token: 0x040002F4 RID: 756
		internal float offsetY;

		// Token: 0x040002F5 RID: 757
		internal float offsetScaleX;

		// Token: 0x040002F6 RID: 758
		internal float offsetScaleY;

		// Token: 0x040002F7 RID: 759
		internal float offsetShearY;

		// Token: 0x040002F8 RID: 760
		internal bool relative;

		// Token: 0x040002F9 RID: 761
		internal bool local;
	}
}
