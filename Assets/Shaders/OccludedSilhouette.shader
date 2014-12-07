Shader "Custom/OccludedSilhouette" {
	Properties {
		_Color ("Silhouette Color", Color) = (0.25, 0.25, 0.25, 1.0)
	}
	SubShader {
		Tags { "Queue" = "Geometry+1" }
		LOD 200
		
		Pass {
			ZWrite Off
			ZTest Greater
			Blend SrcAlpha OneMinusSrcAlpha
			Color [_Color]
		}
	} 
	FallBack "Diffuse"
}
