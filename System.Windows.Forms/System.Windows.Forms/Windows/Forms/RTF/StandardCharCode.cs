using System;

namespace System.Windows.Forms.RTF
{
	// Token: 0x0200038B RID: 907
	internal enum StandardCharCode
	{
		// Token: 0x04001B5B RID: 7003
		nothing,
		// Token: 0x04001B5C RID: 7004
		space,
		// Token: 0x04001B5D RID: 7005
		exclam,
		// Token: 0x04001B5E RID: 7006
		quotedbl,
		// Token: 0x04001B5F RID: 7007
		numbersign,
		// Token: 0x04001B60 RID: 7008
		dollar,
		// Token: 0x04001B61 RID: 7009
		percent,
		// Token: 0x04001B62 RID: 7010
		ampersand,
		// Token: 0x04001B63 RID: 7011
		quoteright,
		// Token: 0x04001B64 RID: 7012
		parenleft,
		// Token: 0x04001B65 RID: 7013
		parenright,
		// Token: 0x04001B66 RID: 7014
		asterisk,
		// Token: 0x04001B67 RID: 7015
		plus,
		// Token: 0x04001B68 RID: 7016
		comma,
		// Token: 0x04001B69 RID: 7017
		hyphen,
		// Token: 0x04001B6A RID: 7018
		period,
		// Token: 0x04001B6B RID: 7019
		slash,
		// Token: 0x04001B6C RID: 7020
		zero,
		// Token: 0x04001B6D RID: 7021
		one,
		// Token: 0x04001B6E RID: 7022
		two,
		// Token: 0x04001B6F RID: 7023
		three,
		// Token: 0x04001B70 RID: 7024
		four,
		// Token: 0x04001B71 RID: 7025
		five,
		// Token: 0x04001B72 RID: 7026
		six,
		// Token: 0x04001B73 RID: 7027
		seven,
		// Token: 0x04001B74 RID: 7028
		eight,
		// Token: 0x04001B75 RID: 7029
		nine,
		// Token: 0x04001B76 RID: 7030
		colon,
		// Token: 0x04001B77 RID: 7031
		semicolon,
		// Token: 0x04001B78 RID: 7032
		less,
		// Token: 0x04001B79 RID: 7033
		equal,
		// Token: 0x04001B7A RID: 7034
		greater,
		// Token: 0x04001B7B RID: 7035
		question,
		// Token: 0x04001B7C RID: 7036
		at,
		// Token: 0x04001B7D RID: 7037
		A,
		// Token: 0x04001B7E RID: 7038
		B,
		// Token: 0x04001B7F RID: 7039
		C,
		// Token: 0x04001B80 RID: 7040
		D,
		// Token: 0x04001B81 RID: 7041
		E,
		// Token: 0x04001B82 RID: 7042
		F,
		// Token: 0x04001B83 RID: 7043
		G,
		// Token: 0x04001B84 RID: 7044
		H,
		// Token: 0x04001B85 RID: 7045
		I,
		// Token: 0x04001B86 RID: 7046
		J,
		// Token: 0x04001B87 RID: 7047
		K,
		// Token: 0x04001B88 RID: 7048
		L,
		// Token: 0x04001B89 RID: 7049
		M,
		// Token: 0x04001B8A RID: 7050
		N,
		// Token: 0x04001B8B RID: 7051
		O,
		// Token: 0x04001B8C RID: 7052
		P,
		// Token: 0x04001B8D RID: 7053
		Q,
		// Token: 0x04001B8E RID: 7054
		R,
		// Token: 0x04001B8F RID: 7055
		S,
		// Token: 0x04001B90 RID: 7056
		T,
		// Token: 0x04001B91 RID: 7057
		U,
		// Token: 0x04001B92 RID: 7058
		V,
		// Token: 0x04001B93 RID: 7059
		W,
		// Token: 0x04001B94 RID: 7060
		X,
		// Token: 0x04001B95 RID: 7061
		Y,
		// Token: 0x04001B96 RID: 7062
		Z,
		// Token: 0x04001B97 RID: 7063
		bracketleft,
		// Token: 0x04001B98 RID: 7064
		backslash,
		// Token: 0x04001B99 RID: 7065
		bracketright,
		// Token: 0x04001B9A RID: 7066
		asciicircum,
		// Token: 0x04001B9B RID: 7067
		underscore,
		// Token: 0x04001B9C RID: 7068
		quoteleft,
		// Token: 0x04001B9D RID: 7069
		a,
		// Token: 0x04001B9E RID: 7070
		b,
		// Token: 0x04001B9F RID: 7071
		c,
		// Token: 0x04001BA0 RID: 7072
		d,
		// Token: 0x04001BA1 RID: 7073
		e,
		// Token: 0x04001BA2 RID: 7074
		f,
		// Token: 0x04001BA3 RID: 7075
		g,
		// Token: 0x04001BA4 RID: 7076
		h,
		// Token: 0x04001BA5 RID: 7077
		i,
		// Token: 0x04001BA6 RID: 7078
		j,
		// Token: 0x04001BA7 RID: 7079
		k,
		// Token: 0x04001BA8 RID: 7080
		l,
		// Token: 0x04001BA9 RID: 7081
		m,
		// Token: 0x04001BAA RID: 7082
		n,
		// Token: 0x04001BAB RID: 7083
		o,
		// Token: 0x04001BAC RID: 7084
		p,
		// Token: 0x04001BAD RID: 7085
		q,
		// Token: 0x04001BAE RID: 7086
		r,
		// Token: 0x04001BAF RID: 7087
		s,
		// Token: 0x04001BB0 RID: 7088
		t,
		// Token: 0x04001BB1 RID: 7089
		u,
		// Token: 0x04001BB2 RID: 7090
		v,
		// Token: 0x04001BB3 RID: 7091
		w,
		// Token: 0x04001BB4 RID: 7092
		x,
		// Token: 0x04001BB5 RID: 7093
		y,
		// Token: 0x04001BB6 RID: 7094
		z,
		// Token: 0x04001BB7 RID: 7095
		braceleft,
		// Token: 0x04001BB8 RID: 7096
		bar,
		// Token: 0x04001BB9 RID: 7097
		braceright,
		// Token: 0x04001BBA RID: 7098
		asciitilde,
		// Token: 0x04001BBB RID: 7099
		exclamdown,
		// Token: 0x04001BBC RID: 7100
		cent,
		// Token: 0x04001BBD RID: 7101
		sterling,
		// Token: 0x04001BBE RID: 7102
		fraction,
		// Token: 0x04001BBF RID: 7103
		yen,
		// Token: 0x04001BC0 RID: 7104
		florin,
		// Token: 0x04001BC1 RID: 7105
		section,
		// Token: 0x04001BC2 RID: 7106
		currency,
		// Token: 0x04001BC3 RID: 7107
		quotedblleft,
		// Token: 0x04001BC4 RID: 7108
		guillemotleft,
		// Token: 0x04001BC5 RID: 7109
		guilsinglleft,
		// Token: 0x04001BC6 RID: 7110
		guilsinglright,
		// Token: 0x04001BC7 RID: 7111
		fi,
		// Token: 0x04001BC8 RID: 7112
		fl,
		// Token: 0x04001BC9 RID: 7113
		endash,
		// Token: 0x04001BCA RID: 7114
		dagger,
		// Token: 0x04001BCB RID: 7115
		daggerdbl,
		// Token: 0x04001BCC RID: 7116
		periodcentered,
		// Token: 0x04001BCD RID: 7117
		paragraph,
		// Token: 0x04001BCE RID: 7118
		bullet,
		// Token: 0x04001BCF RID: 7119
		quotesinglbase,
		// Token: 0x04001BD0 RID: 7120
		quotedblbase,
		// Token: 0x04001BD1 RID: 7121
		quotedblright,
		// Token: 0x04001BD2 RID: 7122
		guillemotright,
		// Token: 0x04001BD3 RID: 7123
		ellipsis,
		// Token: 0x04001BD4 RID: 7124
		perthousand,
		// Token: 0x04001BD5 RID: 7125
		questiondown,
		// Token: 0x04001BD6 RID: 7126
		grave,
		// Token: 0x04001BD7 RID: 7127
		acute,
		// Token: 0x04001BD8 RID: 7128
		circumflex,
		// Token: 0x04001BD9 RID: 7129
		tilde,
		// Token: 0x04001BDA RID: 7130
		macron,
		// Token: 0x04001BDB RID: 7131
		breve,
		// Token: 0x04001BDC RID: 7132
		dotaccent,
		// Token: 0x04001BDD RID: 7133
		dieresis,
		// Token: 0x04001BDE RID: 7134
		ring,
		// Token: 0x04001BDF RID: 7135
		cedilla,
		// Token: 0x04001BE0 RID: 7136
		hungarumlaut,
		// Token: 0x04001BE1 RID: 7137
		ogonek,
		// Token: 0x04001BE2 RID: 7138
		caron,
		// Token: 0x04001BE3 RID: 7139
		emdash,
		// Token: 0x04001BE4 RID: 7140
		AE,
		// Token: 0x04001BE5 RID: 7141
		ordfeminine,
		// Token: 0x04001BE6 RID: 7142
		Lslash,
		// Token: 0x04001BE7 RID: 7143
		Oslash,
		// Token: 0x04001BE8 RID: 7144
		OE,
		// Token: 0x04001BE9 RID: 7145
		ordmasculine,
		// Token: 0x04001BEA RID: 7146
		ae,
		// Token: 0x04001BEB RID: 7147
		dotlessi,
		// Token: 0x04001BEC RID: 7148
		lslash,
		// Token: 0x04001BED RID: 7149
		oslash,
		// Token: 0x04001BEE RID: 7150
		oe,
		// Token: 0x04001BEF RID: 7151
		germandbls,
		// Token: 0x04001BF0 RID: 7152
		Aacute,
		// Token: 0x04001BF1 RID: 7153
		Acircumflex,
		// Token: 0x04001BF2 RID: 7154
		Adieresis,
		// Token: 0x04001BF3 RID: 7155
		Agrave,
		// Token: 0x04001BF4 RID: 7156
		Aring,
		// Token: 0x04001BF5 RID: 7157
		Atilde,
		// Token: 0x04001BF6 RID: 7158
		Ccedilla,
		// Token: 0x04001BF7 RID: 7159
		Eacute,
		// Token: 0x04001BF8 RID: 7160
		Ecircumflex,
		// Token: 0x04001BF9 RID: 7161
		Edieresis,
		// Token: 0x04001BFA RID: 7162
		Egrave,
		// Token: 0x04001BFB RID: 7163
		Eth,
		// Token: 0x04001BFC RID: 7164
		Iacute,
		// Token: 0x04001BFD RID: 7165
		Icircumflex,
		// Token: 0x04001BFE RID: 7166
		Idieresis,
		// Token: 0x04001BFF RID: 7167
		Igrave,
		// Token: 0x04001C00 RID: 7168
		Ntilde,
		// Token: 0x04001C01 RID: 7169
		Oacute,
		// Token: 0x04001C02 RID: 7170
		Ocircumflex,
		// Token: 0x04001C03 RID: 7171
		Odieresis,
		// Token: 0x04001C04 RID: 7172
		Ograve,
		// Token: 0x04001C05 RID: 7173
		Otilde,
		// Token: 0x04001C06 RID: 7174
		Scaron,
		// Token: 0x04001C07 RID: 7175
		Thorn,
		// Token: 0x04001C08 RID: 7176
		Uacute,
		// Token: 0x04001C09 RID: 7177
		Ucircumflex,
		// Token: 0x04001C0A RID: 7178
		Udieresis,
		// Token: 0x04001C0B RID: 7179
		Ugrave,
		// Token: 0x04001C0C RID: 7180
		Yacute,
		// Token: 0x04001C0D RID: 7181
		Ydieresis,
		// Token: 0x04001C0E RID: 7182
		aacute,
		// Token: 0x04001C0F RID: 7183
		acircumflex,
		// Token: 0x04001C10 RID: 7184
		adieresis,
		// Token: 0x04001C11 RID: 7185
		agrave,
		// Token: 0x04001C12 RID: 7186
		aring,
		// Token: 0x04001C13 RID: 7187
		atilde,
		// Token: 0x04001C14 RID: 7188
		brokenbar,
		// Token: 0x04001C15 RID: 7189
		ccedilla,
		// Token: 0x04001C16 RID: 7190
		copyright,
		// Token: 0x04001C17 RID: 7191
		degree,
		// Token: 0x04001C18 RID: 7192
		divide,
		// Token: 0x04001C19 RID: 7193
		eacute,
		// Token: 0x04001C1A RID: 7194
		ecircumflex,
		// Token: 0x04001C1B RID: 7195
		edieresis,
		// Token: 0x04001C1C RID: 7196
		egrave,
		// Token: 0x04001C1D RID: 7197
		eth,
		// Token: 0x04001C1E RID: 7198
		iacute,
		// Token: 0x04001C1F RID: 7199
		icircumflex,
		// Token: 0x04001C20 RID: 7200
		idieresis,
		// Token: 0x04001C21 RID: 7201
		igrave,
		// Token: 0x04001C22 RID: 7202
		logicalnot,
		// Token: 0x04001C23 RID: 7203
		minus,
		// Token: 0x04001C24 RID: 7204
		multiply,
		// Token: 0x04001C25 RID: 7205
		ntilde,
		// Token: 0x04001C26 RID: 7206
		oacute,
		// Token: 0x04001C27 RID: 7207
		ocircumflex,
		// Token: 0x04001C28 RID: 7208
		odieresis,
		// Token: 0x04001C29 RID: 7209
		ograve,
		// Token: 0x04001C2A RID: 7210
		onehalf,
		// Token: 0x04001C2B RID: 7211
		onequarter,
		// Token: 0x04001C2C RID: 7212
		onesuperior,
		// Token: 0x04001C2D RID: 7213
		otilde,
		// Token: 0x04001C2E RID: 7214
		plusminus,
		// Token: 0x04001C2F RID: 7215
		registered,
		// Token: 0x04001C30 RID: 7216
		thorn,
		// Token: 0x04001C31 RID: 7217
		threequarters,
		// Token: 0x04001C32 RID: 7218
		threesuperior,
		// Token: 0x04001C33 RID: 7219
		trademark,
		// Token: 0x04001C34 RID: 7220
		twosuperior,
		// Token: 0x04001C35 RID: 7221
		uacute,
		// Token: 0x04001C36 RID: 7222
		ucircumflex,
		// Token: 0x04001C37 RID: 7223
		udieresis,
		// Token: 0x04001C38 RID: 7224
		ugrave,
		// Token: 0x04001C39 RID: 7225
		yacute,
		// Token: 0x04001C3A RID: 7226
		ydieresis,
		// Token: 0x04001C3B RID: 7227
		Alpha,
		// Token: 0x04001C3C RID: 7228
		Beta,
		// Token: 0x04001C3D RID: 7229
		Chi,
		// Token: 0x04001C3E RID: 7230
		Delta,
		// Token: 0x04001C3F RID: 7231
		Epsilon,
		// Token: 0x04001C40 RID: 7232
		Phi,
		// Token: 0x04001C41 RID: 7233
		Gamma,
		// Token: 0x04001C42 RID: 7234
		Eta,
		// Token: 0x04001C43 RID: 7235
		Iota,
		// Token: 0x04001C44 RID: 7236
		Kappa,
		// Token: 0x04001C45 RID: 7237
		Lambda,
		// Token: 0x04001C46 RID: 7238
		Mu,
		// Token: 0x04001C47 RID: 7239
		Nu,
		// Token: 0x04001C48 RID: 7240
		Omicron,
		// Token: 0x04001C49 RID: 7241
		Pi,
		// Token: 0x04001C4A RID: 7242
		Theta,
		// Token: 0x04001C4B RID: 7243
		Rho,
		// Token: 0x04001C4C RID: 7244
		Sigma,
		// Token: 0x04001C4D RID: 7245
		Tau,
		// Token: 0x04001C4E RID: 7246
		Upsilon,
		// Token: 0x04001C4F RID: 7247
		varUpsilon,
		// Token: 0x04001C50 RID: 7248
		Omega,
		// Token: 0x04001C51 RID: 7249
		Xi,
		// Token: 0x04001C52 RID: 7250
		Psi,
		// Token: 0x04001C53 RID: 7251
		Zeta,
		// Token: 0x04001C54 RID: 7252
		alpha,
		// Token: 0x04001C55 RID: 7253
		beta,
		// Token: 0x04001C56 RID: 7254
		chi,
		// Token: 0x04001C57 RID: 7255
		delta,
		// Token: 0x04001C58 RID: 7256
		epsilon,
		// Token: 0x04001C59 RID: 7257
		phi,
		// Token: 0x04001C5A RID: 7258
		varphi,
		// Token: 0x04001C5B RID: 7259
		gamma,
		// Token: 0x04001C5C RID: 7260
		eta,
		// Token: 0x04001C5D RID: 7261
		iota,
		// Token: 0x04001C5E RID: 7262
		kappa,
		// Token: 0x04001C5F RID: 7263
		lambda,
		// Token: 0x04001C60 RID: 7264
		mu,
		// Token: 0x04001C61 RID: 7265
		nu,
		// Token: 0x04001C62 RID: 7266
		omicron,
		// Token: 0x04001C63 RID: 7267
		pi,
		// Token: 0x04001C64 RID: 7268
		varpi,
		// Token: 0x04001C65 RID: 7269
		theta,
		// Token: 0x04001C66 RID: 7270
		vartheta,
		// Token: 0x04001C67 RID: 7271
		rho,
		// Token: 0x04001C68 RID: 7272
		sigma,
		// Token: 0x04001C69 RID: 7273
		varsigma,
		// Token: 0x04001C6A RID: 7274
		tau,
		// Token: 0x04001C6B RID: 7275
		upsilon,
		// Token: 0x04001C6C RID: 7276
		omega,
		// Token: 0x04001C6D RID: 7277
		xi,
		// Token: 0x04001C6E RID: 7278
		psi,
		// Token: 0x04001C6F RID: 7279
		zeta,
		// Token: 0x04001C70 RID: 7280
		nobrkspace,
		// Token: 0x04001C71 RID: 7281
		nobrkhyphen,
		// Token: 0x04001C72 RID: 7282
		lessequal,
		// Token: 0x04001C73 RID: 7283
		greaterequal,
		// Token: 0x04001C74 RID: 7284
		infinity,
		// Token: 0x04001C75 RID: 7285
		integral,
		// Token: 0x04001C76 RID: 7286
		notequal,
		// Token: 0x04001C77 RID: 7287
		radical,
		// Token: 0x04001C78 RID: 7288
		radicalex,
		// Token: 0x04001C79 RID: 7289
		approxequal,
		// Token: 0x04001C7A RID: 7290
		apple,
		// Token: 0x04001C7B RID: 7291
		partialdiff,
		// Token: 0x04001C7C RID: 7292
		opthyphen,
		// Token: 0x04001C7D RID: 7293
		formula,
		// Token: 0x04001C7E RID: 7294
		lozenge,
		// Token: 0x04001C7F RID: 7295
		universal,
		// Token: 0x04001C80 RID: 7296
		existential,
		// Token: 0x04001C81 RID: 7297
		suchthat,
		// Token: 0x04001C82 RID: 7298
		congruent,
		// Token: 0x04001C83 RID: 7299
		therefore,
		// Token: 0x04001C84 RID: 7300
		perpendicular,
		// Token: 0x04001C85 RID: 7301
		minute,
		// Token: 0x04001C86 RID: 7302
		club,
		// Token: 0x04001C87 RID: 7303
		diamond,
		// Token: 0x04001C88 RID: 7304
		heart,
		// Token: 0x04001C89 RID: 7305
		spade,
		// Token: 0x04001C8A RID: 7306
		arrowboth,
		// Token: 0x04001C8B RID: 7307
		arrowleft,
		// Token: 0x04001C8C RID: 7308
		arrowup,
		// Token: 0x04001C8D RID: 7309
		arrowright,
		// Token: 0x04001C8E RID: 7310
		arrowdown,
		// Token: 0x04001C8F RID: 7311
		second,
		// Token: 0x04001C90 RID: 7312
		proportional,
		// Token: 0x04001C91 RID: 7313
		equivalence,
		// Token: 0x04001C92 RID: 7314
		arrowvertex,
		// Token: 0x04001C93 RID: 7315
		arrowhorizex,
		// Token: 0x04001C94 RID: 7316
		carriagereturn,
		// Token: 0x04001C95 RID: 7317
		aleph,
		// Token: 0x04001C96 RID: 7318
		Ifraktur,
		// Token: 0x04001C97 RID: 7319
		Rfraktur,
		// Token: 0x04001C98 RID: 7320
		weierstrass,
		// Token: 0x04001C99 RID: 7321
		circlemultiply,
		// Token: 0x04001C9A RID: 7322
		circleplus,
		// Token: 0x04001C9B RID: 7323
		emptyset,
		// Token: 0x04001C9C RID: 7324
		intersection,
		// Token: 0x04001C9D RID: 7325
		union,
		// Token: 0x04001C9E RID: 7326
		propersuperset,
		// Token: 0x04001C9F RID: 7327
		reflexsuperset,
		// Token: 0x04001CA0 RID: 7328
		notsubset,
		// Token: 0x04001CA1 RID: 7329
		propersubset,
		// Token: 0x04001CA2 RID: 7330
		reflexsubset,
		// Token: 0x04001CA3 RID: 7331
		element,
		// Token: 0x04001CA4 RID: 7332
		notelement,
		// Token: 0x04001CA5 RID: 7333
		angle,
		// Token: 0x04001CA6 RID: 7334
		gradient,
		// Token: 0x04001CA7 RID: 7335
		product,
		// Token: 0x04001CA8 RID: 7336
		logicaland,
		// Token: 0x04001CA9 RID: 7337
		logicalor,
		// Token: 0x04001CAA RID: 7338
		arrowdblboth,
		// Token: 0x04001CAB RID: 7339
		arrowdblleft,
		// Token: 0x04001CAC RID: 7340
		arrowdblup,
		// Token: 0x04001CAD RID: 7341
		arrowdblright,
		// Token: 0x04001CAE RID: 7342
		arrowdbldown,
		// Token: 0x04001CAF RID: 7343
		angleleft,
		// Token: 0x04001CB0 RID: 7344
		registersans,
		// Token: 0x04001CB1 RID: 7345
		copyrightsans,
		// Token: 0x04001CB2 RID: 7346
		trademarksans,
		// Token: 0x04001CB3 RID: 7347
		angleright,
		// Token: 0x04001CB4 RID: 7348
		mathplus,
		// Token: 0x04001CB5 RID: 7349
		mathminus,
		// Token: 0x04001CB6 RID: 7350
		mathasterisk,
		// Token: 0x04001CB7 RID: 7351
		mathnumbersign,
		// Token: 0x04001CB8 RID: 7352
		dotmath,
		// Token: 0x04001CB9 RID: 7353
		mathequal,
		// Token: 0x04001CBA RID: 7354
		mathtilde,
		// Token: 0x04001CBB RID: 7355
		MaxChar
	}
}
