shader_type canvas_item;

void vertex() {
	// Called for every vertex the material is visible on.
}

void fragment() {
	// Called for every pixel the material is visible on.
	vec4 tex_color = texture(TEXTURE, UV);
    // 计算当前像素距离纹理中心的比例（假设纹理是正方形，如果不是正方形需要调整）
    vec2 center = vec2(0.5, 0.5);
    float radius = 0.5;
    float dist = distance(UV, center);
    if (dist > radius) {
        discard; // 丢弃圆外的像素
    }
    COLOR = tex_color;
}

//void light() {
//	// Called for every pixel for every light affecting the CanvasItem.
//	// Uncomment to replace the default light processing function with this one.
//}
