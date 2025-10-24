shader_type spatial;

uniform vec4 color;
uniform mat3 transform;
uniform vec3 cell_center;

void vertex() {
	VERTEX = (MODEL_MATRIX * vec4(VERTEX, 1.0)).xyz;
	VERTEX = transform * VERTEX;
	VERTEX += cell_center;
	VERTEX = (inverse(MODEL_MATRIX) * vec4(VERTEX, 1.0)).xyz;
	
	NORMAL = transform * NORMAL;
}

void fragment() {
	ALBEDO = color.rgb;
}
