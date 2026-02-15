using System;
using System.Collections.Generic;
using UnityEngine.Playables;
using UnityEngine.Serialization;

namespace UnityEngine.Timeline
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	public class TimelineClip : ICurvesOwner, ISerializationCallbackReceiver
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000038A3 File Offset: 0x00001AA3
		private void UpgradeToLatestVersion()
		{
			if (this.m_Version < 1)
			{
				TimelineClip.TimelineClipUpgrade.UpgradeClipInFromGlobalToLocal(this);
			}
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x000038B4 File Offset: 0x00001AB4
		internal TimelineClip(TrackAsset parent)
		{
			this.SetParentTrack_Internal(parent);
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000038F0 File Offset: 0x00001AF0
		public bool hasPreExtrapolation
		{
			get
			{
				return this.m_PreExtrapolationMode != TimelineClip.ClipExtrapolation.None && this.m_PreExtrapolationTime > 0.0;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000390D File Offset: 0x00001B0D
		public bool hasPostExtrapolation
		{
			get
			{
				return this.m_PostExtrapolationMode != TimelineClip.ClipExtrapolation.None && this.m_PostExtrapolationTime > 0.0;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000392A File Offset: 0x00001B2A
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00003960 File Offset: 0x00001B60
		public double timeScale
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.SpeedMultiplier))
				{
					return 1.0;
				}
				return Math.Max(TimelineClip.kTimeScaleMin, Math.Min(this.m_TimeScale, TimelineClip.kTimeScaleMax));
			}
			set
			{
				this.UpdateDirty(this.m_TimeScale, value);
				this.m_TimeScale = (this.clipCaps.HasAny(ClipCaps.SpeedMultiplier) ? Math.Max(TimelineClip.kTimeScaleMin, Math.Min(value, TimelineClip.kTimeScaleMax)) : 1.0);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000039AE File Offset: 0x00001BAE
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000039B8 File Offset: 0x00001BB8
		public double start
		{
			get
			{
				return this.m_Start;
			}
			set
			{
				this.UpdateDirty(value, this.m_Start);
				double newValue = Math.Max(TimelineClip.SanitizeTimeValue(value, this.m_Start), 0.0);
				if (this.m_ParentTrack != null && this.m_Start != newValue)
				{
					this.m_ParentTrack.OnClipMove();
				}
				this.m_Start = newValue;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00003A16 File Offset: 0x00001C16
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00003A1E File Offset: 0x00001C1E
		public double duration
		{
			get
			{
				return this.m_Duration;
			}
			set
			{
				this.UpdateDirty(this.m_Duration, value);
				this.m_Duration = Math.Max(TimelineClip.SanitizeTimeValue(value, this.m_Duration), double.Epsilon);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003A4D File Offset: 0x00001C4D
		public double end
		{
			get
			{
				return this.m_Start + this.m_Duration;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003A5C File Offset: 0x00001C5C
		// (set) Token: 0x060000BC RID: 188 RVA: 0x00003A7C File Offset: 0x00001C7C
		public double clipIn
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.ClipIn))
				{
					return 0.0;
				}
				return this.m_ClipIn;
			}
			set
			{
				this.UpdateDirty(this.m_ClipIn, value);
				this.m_ClipIn = (this.clipCaps.HasAny(ClipCaps.ClipIn) ? Math.Max(Math.Min(TimelineClip.SanitizeTimeValue(value, this.m_ClipIn), TimelineClip.kMaxTimeValue), 0.0) : 0.0);
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003AD9 File Offset: 0x00001CD9
		// (set) Token: 0x060000BE RID: 190 RVA: 0x00003AE1 File Offset: 0x00001CE1
		public string displayName
		{
			get
			{
				return this.m_DisplayName;
			}
			set
			{
				this.m_DisplayName = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003AEC File Offset: 0x00001CEC
		public double clipAssetDuration
		{
			get
			{
				IPlayableAsset playableAsset = this.m_Asset as IPlayableAsset;
				if (playableAsset == null)
				{
					return double.MaxValue;
				}
				return playableAsset.duration;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00003B18 File Offset: 0x00001D18
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003B20 File Offset: 0x00001D20
		public AnimationClip curves
		{
			get
			{
				return this.m_AnimationCurves;
			}
			internal set
			{
				this.m_AnimationCurves = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00003B29 File Offset: 0x00001D29
		string ICurvesOwner.defaultCurvesName
		{
			get
			{
				return TimelineClip.kDefaultCurvesName;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00003B30 File Offset: 0x00001D30
		public bool hasCurves
		{
			get
			{
				return this.m_AnimationCurves != null && !this.m_AnimationCurves.empty;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003B50 File Offset: 0x00001D50
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003B58 File Offset: 0x00001D58
		public Object asset
		{
			get
			{
				return this.m_Asset;
			}
			set
			{
				this.m_Asset = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00003B61 File Offset: 0x00001D61
		Object ICurvesOwner.assetOwner
		{
			get
			{
				return this.GetParentTrack();
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00003B61 File Offset: 0x00001D61
		TrackAsset ICurvesOwner.targetTrack
		{
			get
			{
				return this.GetParentTrack();
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00003B69 File Offset: 0x00001D69
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00002811 File Offset: 0x00000A11
		[Obsolete("underlyingAsset property is obsolete. Use asset property instead", true)]
		public Object underlyingAsset
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003B6C File Offset: 0x00001D6C
		// (set) Token: 0x060000CB RID: 203 RVA: 0x00003B74 File Offset: 0x00001D74
		[Obsolete("parentTrack is deprecated and will be removed in a future release. Use GetParentTrack() and TimelineClipExtensions::MoveToTrack() or TimelineClipExtensions::TryMoveToTrack() instead.", false)]
		public TrackAsset parentTrack
		{
			get
			{
				return this.m_ParentTrack;
			}
			set
			{
				this.SetParentTrack_Internal(value);
			}
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00003B6C File Offset: 0x00001D6C
		public TrackAsset GetParentTrack()
		{
			return this.m_ParentTrack;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003B80 File Offset: 0x00001D80
		internal void SetParentTrack_Internal(TrackAsset newParentTrack)
		{
			if (this.m_ParentTrack == newParentTrack)
			{
				return;
			}
			if (this.m_ParentTrack != null)
			{
				this.m_ParentTrack.RemoveClip(this);
			}
			this.m_ParentTrack = newParentTrack;
			if (this.m_ParentTrack != null)
			{
				this.m_ParentTrack.AddClip(this);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00003BD8 File Offset: 0x00001DD8
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00003C38 File Offset: 0x00001E38
		public double easeInDuration
		{
			get
			{
				double availableDuration = (this.hasBlendOut ? (this.duration - this.m_BlendOutDuration) : this.duration);
				if (!this.clipCaps.HasAny(ClipCaps.Blending))
				{
					return 0.0;
				}
				return Math.Min(Math.Max(this.m_EaseInDuration, 0.0), availableDuration);
			}
			set
			{
				double availableDuration = (this.hasBlendOut ? (this.duration - this.m_BlendOutDuration) : this.duration);
				this.m_EaseInDuration = (this.clipCaps.HasAny(ClipCaps.Blending) ? Math.Max(0.0, Math.Min(TimelineClip.SanitizeTimeValue(value, this.m_EaseInDuration), availableDuration)) : 0.0);
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003CA4 File Offset: 0x00001EA4
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00003D04 File Offset: 0x00001F04
		public double easeOutDuration
		{
			get
			{
				double availableDuration = (this.hasBlendIn ? (this.duration - this.m_BlendInDuration) : this.duration);
				if (!this.clipCaps.HasAny(ClipCaps.Blending))
				{
					return 0.0;
				}
				return Math.Min(Math.Max(this.m_EaseOutDuration, 0.0), availableDuration);
			}
			set
			{
				double availableDuration = (this.hasBlendIn ? (this.duration - this.m_BlendInDuration) : this.duration);
				this.m_EaseOutDuration = (this.clipCaps.HasAny(ClipCaps.Blending) ? Math.Max(0.0, Math.Min(TimelineClip.SanitizeTimeValue(value, this.m_EaseOutDuration), availableDuration)) : 0.0);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003D6F File Offset: 0x00001F6F
		[Obsolete("Use easeOutTime instead (UnityUpgradable) -> easeOutTime", true)]
		public double eastOutTime
		{
			get
			{
				return this.duration - this.easeOutDuration + this.m_Start;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00003D6F File Offset: 0x00001F6F
		public double easeOutTime
		{
			get
			{
				return this.duration - this.easeOutDuration + this.m_Start;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00003D85 File Offset: 0x00001F85
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00003DA6 File Offset: 0x00001FA6
		public double blendInDuration
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.Blending))
				{
					return 0.0;
				}
				return this.m_BlendInDuration;
			}
			set
			{
				this.m_BlendInDuration = (this.clipCaps.HasAny(ClipCaps.Blending) ? TimelineClip.SanitizeTimeValue(value, this.m_BlendInDuration) : 0.0);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00003DD4 File Offset: 0x00001FD4
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00003DF5 File Offset: 0x00001FF5
		public double blendOutDuration
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.Blending))
				{
					return 0.0;
				}
				return this.m_BlendOutDuration;
			}
			set
			{
				this.m_BlendOutDuration = (this.clipCaps.HasAny(ClipCaps.Blending) ? TimelineClip.SanitizeTimeValue(value, this.m_BlendOutDuration) : 0.0);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00003E23 File Offset: 0x00002023
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x00003E2B File Offset: 0x0000202B
		public TimelineClip.BlendCurveMode blendInCurveMode
		{
			get
			{
				return this.m_BlendInCurveMode;
			}
			set
			{
				this.m_BlendInCurveMode = value;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00003E34 File Offset: 0x00002034
		// (set) Token: 0x060000DB RID: 219 RVA: 0x00003E3C File Offset: 0x0000203C
		public TimelineClip.BlendCurveMode blendOutCurveMode
		{
			get
			{
				return this.m_BlendOutCurveMode;
			}
			set
			{
				this.m_BlendOutCurveMode = value;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000DC RID: 220 RVA: 0x00003E45 File Offset: 0x00002045
		public bool hasBlendIn
		{
			get
			{
				return this.clipCaps.HasAny(ClipCaps.Blending) && this.m_BlendInDuration > 0.0;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000DD RID: 221 RVA: 0x00003E69 File Offset: 0x00002069
		public bool hasBlendOut
		{
			get
			{
				return this.clipCaps.HasAny(ClipCaps.Blending) && this.m_BlendOutDuration > 0.0;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000DE RID: 222 RVA: 0x00003E8D File Offset: 0x0000208D
		// (set) Token: 0x060000DF RID: 223 RVA: 0x00003EB6 File Offset: 0x000020B6
		public AnimationCurve mixInCurve
		{
			get
			{
				if (this.m_MixInCurve == null || this.m_MixInCurve.length < 2)
				{
					this.m_MixInCurve = TimelineClip.GetDefaultMixInCurve();
				}
				return this.m_MixInCurve;
			}
			set
			{
				this.m_MixInCurve = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E0 RID: 224 RVA: 0x00003EBF File Offset: 0x000020BF
		public float mixInPercentage
		{
			get
			{
				return (float)(this.mixInDuration / this.duration);
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x00003ECF File Offset: 0x000020CF
		public double mixInDuration
		{
			get
			{
				if (!this.hasBlendIn)
				{
					return this.easeInDuration;
				}
				return this.blendInDuration;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000E2 RID: 226 RVA: 0x00003EE6 File Offset: 0x000020E6
		// (set) Token: 0x060000E3 RID: 227 RVA: 0x00003F0F File Offset: 0x0000210F
		public AnimationCurve mixOutCurve
		{
			get
			{
				if (this.m_MixOutCurve == null || this.m_MixOutCurve.length < 2)
				{
					this.m_MixOutCurve = TimelineClip.GetDefaultMixOutCurve();
				}
				return this.m_MixOutCurve;
			}
			set
			{
				this.m_MixOutCurve = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003F18 File Offset: 0x00002118
		public double mixOutTime
		{
			get
			{
				return this.duration - this.mixOutDuration + this.m_Start;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00003F2E File Offset: 0x0000212E
		public double mixOutDuration
		{
			get
			{
				if (!this.hasBlendOut)
				{
					return this.easeOutDuration;
				}
				return this.blendOutDuration;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00003F45 File Offset: 0x00002145
		public float mixOutPercentage
		{
			get
			{
				return (float)(this.mixOutDuration / this.duration);
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00003F55 File Offset: 0x00002155
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00003F5D File Offset: 0x0000215D
		public bool recordable
		{
			get
			{
				return this.m_Recordable;
			}
			internal set
			{
				this.m_Recordable = value;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00003F68 File Offset: 0x00002168
		[Obsolete("exposedParameter is deprecated and will be removed in a future release", true)]
		public List<string> exposedParameters
		{
			get
			{
				List<string> list;
				if ((list = this.m_ExposedParameterNames) == null)
				{
					list = (this.m_ExposedParameterNames = new List<string>());
				}
				return list;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00003F90 File Offset: 0x00002190
		public ClipCaps clipCaps
		{
			get
			{
				ITimelineClipAsset clipAsset = this.asset as ITimelineClipAsset;
				if (clipAsset == null)
				{
					return TimelineClip.kDefaultClipCaps;
				}
				return clipAsset.clipCaps;
			}
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00003FB8 File Offset: 0x000021B8
		internal int Hash()
		{
			int hashCode = this.m_Start.GetHashCode();
			int hashCode2 = this.m_Duration.GetHashCode();
			int hashCode3 = this.m_TimeScale.GetHashCode();
			int hashCode4 = this.m_ClipIn.GetHashCode();
			int num = (int)this.m_PreExtrapolationMode;
			int hashCode5 = num.GetHashCode();
			num = (int)this.m_PostExtrapolationMode;
			return HashUtility.CombineHash(hashCode, hashCode2, hashCode3, hashCode4, hashCode5, num.GetHashCode());
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00004014 File Offset: 0x00002214
		public float EvaluateMixOut(double time)
		{
			if (!this.clipCaps.HasAny(ClipCaps.Blending))
			{
				return 1f;
			}
			if (this.mixOutDuration > (double)Mathf.Epsilon)
			{
				float perc = (float)(time - this.mixOutTime) / (float)this.mixOutDuration;
				return Mathf.Clamp01(this.mixOutCurve.Evaluate(perc));
			}
			return 1f;
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00004070 File Offset: 0x00002270
		public float EvaluateMixIn(double time)
		{
			if (!this.clipCaps.HasAny(ClipCaps.Blending))
			{
				return 1f;
			}
			if (this.mixInDuration > (double)Mathf.Epsilon)
			{
				float perc = (float)(time - this.m_Start) / (float)this.mixInDuration;
				return Mathf.Clamp01(this.mixInCurve.Evaluate(perc));
			}
			return 1f;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000040CB File Offset: 0x000022CB
		private static AnimationCurve GetDefaultMixInCurve()
		{
			return AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000040E6 File Offset: 0x000022E6
		private static AnimationCurve GetDefaultMixOutCurve()
		{
			return AnimationCurve.EaseInOut(0f, 1f, 1f, 0f);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00004104 File Offset: 0x00002304
		public double ToLocalTime(double time)
		{
			if (time < 0.0)
			{
				return time;
			}
			if (this.IsPreExtrapolatedTime(time))
			{
				time = TimelineClip.GetExtrapolatedTime(time - this.m_Start, this.m_PreExtrapolationMode, this.m_Duration);
			}
			else if (this.IsPostExtrapolatedTime(time))
			{
				time = TimelineClip.GetExtrapolatedTime(time - this.m_Start, this.m_PostExtrapolationMode, this.m_Duration);
			}
			else
			{
				time -= this.m_Start;
			}
			time *= this.timeScale;
			time += this.clipIn;
			return time;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000418A File Offset: 0x0000238A
		public double ToLocalTimeUnbound(double time)
		{
			return (time - this.m_Start) * this.timeScale + this.clipIn;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000041A2 File Offset: 0x000023A2
		internal double FromLocalTimeUnbound(double time)
		{
			return (time - this.clipIn) / this.timeScale + this.m_Start;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000041BC File Offset: 0x000023BC
		public AnimationClip animationClip
		{
			get
			{
				if (this.m_Asset == null)
				{
					return null;
				}
				AnimationPlayableAsset playableAsset = this.m_Asset as AnimationPlayableAsset;
				if (!(playableAsset != null))
				{
					return null;
				}
				return playableAsset.clip;
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x000041F6 File Offset: 0x000023F6
		private static double SanitizeTimeValue(double value, double defaultValue)
		{
			if (double.IsInfinity(value) || double.IsNaN(value))
			{
				Debug.LogError("Invalid time value assigned");
				return defaultValue;
			}
			return Math.Max(-TimelineClip.kMaxTimeValue, Math.Min(TimelineClip.kMaxTimeValue, value));
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000422A File Offset: 0x0000242A
		// (set) Token: 0x060000F6 RID: 246 RVA: 0x00004242 File Offset: 0x00002442
		public TimelineClip.ClipExtrapolation postExtrapolationMode
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.Extrapolation))
				{
					return TimelineClip.ClipExtrapolation.None;
				}
				return this.m_PostExtrapolationMode;
			}
			internal set
			{
				this.m_PostExtrapolationMode = (this.clipCaps.HasAny(ClipCaps.Extrapolation) ? value : TimelineClip.ClipExtrapolation.None);
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000425C File Offset: 0x0000245C
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00004274 File Offset: 0x00002474
		public TimelineClip.ClipExtrapolation preExtrapolationMode
		{
			get
			{
				if (!this.clipCaps.HasAny(ClipCaps.Extrapolation))
				{
					return TimelineClip.ClipExtrapolation.None;
				}
				return this.m_PreExtrapolationMode;
			}
			internal set
			{
				this.m_PreExtrapolationMode = (this.clipCaps.HasAny(ClipCaps.Extrapolation) ? value : TimelineClip.ClipExtrapolation.None);
			}
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000428E File Offset: 0x0000248E
		internal void SetPostExtrapolationTime(double time)
		{
			this.m_PostExtrapolationTime = time;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00004297 File Offset: 0x00002497
		internal void SetPreExtrapolationTime(double time)
		{
			this.m_PreExtrapolationTime = time;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000042A0 File Offset: 0x000024A0
		public bool IsExtrapolatedTime(double sequenceTime)
		{
			return this.IsPreExtrapolatedTime(sequenceTime) || this.IsPostExtrapolatedTime(sequenceTime);
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000042B4 File Offset: 0x000024B4
		public bool IsPreExtrapolatedTime(double sequenceTime)
		{
			return this.preExtrapolationMode != TimelineClip.ClipExtrapolation.None && sequenceTime < this.m_Start && sequenceTime >= this.m_Start - this.m_PreExtrapolationTime;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000042DC File Offset: 0x000024DC
		public bool IsPostExtrapolatedTime(double sequenceTime)
		{
			return this.postExtrapolationMode != TimelineClip.ClipExtrapolation.None && sequenceTime > this.end && sequenceTime - this.end < this.m_PostExtrapolationTime;
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00004301 File Offset: 0x00002501
		public double extrapolatedStart
		{
			get
			{
				if (this.m_PreExtrapolationMode != TimelineClip.ClipExtrapolation.None)
				{
					return this.m_Start - this.m_PreExtrapolationTime;
				}
				return this.m_Start;
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00004320 File Offset: 0x00002520
		public double extrapolatedDuration
		{
			get
			{
				double length = this.m_Duration;
				if (this.m_PostExtrapolationMode != TimelineClip.ClipExtrapolation.None)
				{
					length += Math.Min(this.m_PostExtrapolationTime, TimelineClip.kMaxTimeValue);
				}
				if (this.m_PreExtrapolationMode != TimelineClip.ClipExtrapolation.None)
				{
					length += this.m_PreExtrapolationTime;
				}
				return length;
			}
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004364 File Offset: 0x00002564
		private static double GetExtrapolatedTime(double time, TimelineClip.ClipExtrapolation mode, double duration)
		{
			if (duration == 0.0)
			{
				return 0.0;
			}
			switch (mode)
			{
			case TimelineClip.ClipExtrapolation.Hold:
				if (time < 0.0)
				{
					return 0.0;
				}
				if (time > duration)
				{
					return duration;
				}
				break;
			case TimelineClip.ClipExtrapolation.Loop:
				if (time < 0.0)
				{
					time = duration - -time % duration;
				}
				else if (time > duration)
				{
					time %= duration;
				}
				break;
			case TimelineClip.ClipExtrapolation.PingPong:
				if (time < 0.0)
				{
					time = duration * 2.0 - -time % (duration * 2.0);
					time = duration - Math.Abs(time - duration);
				}
				else
				{
					time %= duration * 2.0;
					time = duration - Math.Abs(time - duration);
				}
				break;
			}
			return time;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004435 File Offset: 0x00002635
		public void CreateCurves(string curvesClipName)
		{
			if (this.m_AnimationCurves != null)
			{
				return;
			}
			this.m_AnimationCurves = TimelineCreateUtilities.CreateAnimationClipForTrack(string.IsNullOrEmpty(curvesClipName) ? TimelineClip.kDefaultCurvesName : curvesClipName, this.GetParentTrack(), true);
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004468 File Offset: 0x00002668
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
			this.m_Version = 1;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004471 File Offset: 0x00002671
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			if (this.m_Version < 1)
			{
				this.UpgradeToLatestVersion();
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004484 File Offset: 0x00002684
		public override string ToString()
		{
			return UnityString.Format("{0} ({1:F2}, {2:F2}):{3:F2} | {4}", new object[]
			{
				this.displayName,
				this.start,
				this.end,
				this.clipIn,
				this.GetParentTrack()
			});
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000044E0 File Offset: 0x000026E0
		public void ConformEaseValues()
		{
			if (this.m_EaseInDuration + this.m_EaseOutDuration > this.duration)
			{
				double ratio = TimelineClip.CalculateEasingRatio(this.m_EaseInDuration, this.m_EaseOutDuration);
				this.m_EaseInDuration = this.duration * ratio;
				this.m_EaseOutDuration = this.duration * (1.0 - ratio);
			}
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000453C File Offset: 0x0000273C
		private static double CalculateEasingRatio(double easeIn, double easeOut)
		{
			if (Math.Abs(easeIn - easeOut) < TimeUtility.kTimeEpsilon)
			{
				return 0.5;
			}
			if (easeIn == 0.0)
			{
				return 0.0;
			}
			if (easeOut == 0.0)
			{
				return 1.0;
			}
			return easeIn / (easeIn + easeOut);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002811 File Offset: 0x00000A11
		private void UpdateDirty(double oldValue, double newValue)
		{
		}

		// Token: 0x04000060 RID: 96
		private const int k_LatestVersion = 1;

		// Token: 0x04000061 RID: 97
		[SerializeField]
		[HideInInspector]
		private int m_Version;

		// Token: 0x04000062 RID: 98
		public static readonly ClipCaps kDefaultClipCaps = ClipCaps.Blending;

		// Token: 0x04000063 RID: 99
		public static readonly float kDefaultClipDurationInSeconds = 5f;

		// Token: 0x04000064 RID: 100
		public static readonly double kTimeScaleMin = 0.001;

		// Token: 0x04000065 RID: 101
		public static readonly double kTimeScaleMax = 1000.0;

		// Token: 0x04000066 RID: 102
		internal static readonly string kDefaultCurvesName = "Clip Parameters";

		// Token: 0x04000067 RID: 103
		internal static readonly double kMinDuration = 0.016666666666666666;

		// Token: 0x04000068 RID: 104
		internal static readonly double kMaxTimeValue = 1000000.0;

		// Token: 0x04000069 RID: 105
		[SerializeField]
		private double m_Start;

		// Token: 0x0400006A RID: 106
		[SerializeField]
		private double m_ClipIn;

		// Token: 0x0400006B RID: 107
		[SerializeField]
		private Object m_Asset;

		// Token: 0x0400006C RID: 108
		[SerializeField]
		[FormerlySerializedAs("m_HackDuration")]
		private double m_Duration;

		// Token: 0x0400006D RID: 109
		[SerializeField]
		private double m_TimeScale = 1.0;

		// Token: 0x0400006E RID: 110
		[SerializeField]
		private TrackAsset m_ParentTrack;

		// Token: 0x0400006F RID: 111
		[SerializeField]
		private double m_EaseInDuration;

		// Token: 0x04000070 RID: 112
		[SerializeField]
		private double m_EaseOutDuration;

		// Token: 0x04000071 RID: 113
		[SerializeField]
		private double m_BlendInDuration = -1.0;

		// Token: 0x04000072 RID: 114
		[SerializeField]
		private double m_BlendOutDuration = -1.0;

		// Token: 0x04000073 RID: 115
		[SerializeField]
		private AnimationCurve m_MixInCurve;

		// Token: 0x04000074 RID: 116
		[SerializeField]
		private AnimationCurve m_MixOutCurve;

		// Token: 0x04000075 RID: 117
		[SerializeField]
		private TimelineClip.BlendCurveMode m_BlendInCurveMode;

		// Token: 0x04000076 RID: 118
		[SerializeField]
		private TimelineClip.BlendCurveMode m_BlendOutCurveMode;

		// Token: 0x04000077 RID: 119
		[SerializeField]
		private List<string> m_ExposedParameterNames;

		// Token: 0x04000078 RID: 120
		[SerializeField]
		private AnimationClip m_AnimationCurves;

		// Token: 0x04000079 RID: 121
		[SerializeField]
		private bool m_Recordable;

		// Token: 0x0400007A RID: 122
		[SerializeField]
		private TimelineClip.ClipExtrapolation m_PostExtrapolationMode;

		// Token: 0x0400007B RID: 123
		[SerializeField]
		private TimelineClip.ClipExtrapolation m_PreExtrapolationMode;

		// Token: 0x0400007C RID: 124
		[SerializeField]
		private double m_PostExtrapolationTime;

		// Token: 0x0400007D RID: 125
		[SerializeField]
		private double m_PreExtrapolationTime;

		// Token: 0x0400007E RID: 126
		[SerializeField]
		private string m_DisplayName;

		// Token: 0x02000019 RID: 25
		private enum Versions
		{
			// Token: 0x04000080 RID: 128
			Initial,
			// Token: 0x04000081 RID: 129
			ClipInFromGlobalToLocal
		}

		// Token: 0x0200001A RID: 26
		private static class TimelineClipUpgrade
		{
			// Token: 0x06000109 RID: 265 RVA: 0x000045F4 File Offset: 0x000027F4
			public static void UpgradeClipInFromGlobalToLocal(TimelineClip clip)
			{
				if (clip.m_ClipIn > 0.0 && clip.m_TimeScale > 1.401298464324817E-45)
				{
					clip.m_ClipIn *= clip.m_TimeScale;
				}
			}
		}

		// Token: 0x0200001B RID: 27
		public enum ClipExtrapolation
		{
			// Token: 0x04000083 RID: 131
			None,
			// Token: 0x04000084 RID: 132
			Hold,
			// Token: 0x04000085 RID: 133
			Loop,
			// Token: 0x04000086 RID: 134
			PingPong,
			// Token: 0x04000087 RID: 135
			Continue
		}

		// Token: 0x0200001C RID: 28
		public enum BlendCurveMode
		{
			// Token: 0x04000089 RID: 137
			Auto,
			// Token: 0x0400008A RID: 138
			Manual
		}
	}
}
