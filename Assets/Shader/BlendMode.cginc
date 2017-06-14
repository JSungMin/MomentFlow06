fixed4 Screen (fixed4 a, fixed4 b) { return (1-(1-a)*(1-b)); }
fixed4 Multiply (fixed4 a, fixed4 b) { return (a * b); }
fixed4 Darken (fixed4 a, fixed4 b) { return fixed4(min(a.rgb, b.rgb), 1); }
fixed4 LinearBurn (fixed4 a, fixed4 b) { return (a+b-1); }
fixed4 Lighten (fixed4 a, fixed4 b) { return fixed4(max(a.rgb, b.rgb), 1); }
fixed4 LinearDodge (fixed4 a, fixed4 b) { return (a+b); }
fixed ColorBurn (fixed a, fixed b)
{
	return (b==0) ? b : max(0, (1-(1-a)/b));
}
fixed ColorDodge (fixed a, fixed b)
{
	return (b==1) ? b : min(1, (a/(1-b)));
}
fixed4 Overlay (fixed4 a, fixed4 b) 
{
    fixed4 ret = fixed4(0,0,0,1);

    ret.r = (b.r <= 0.5) ?  (2*a.r)*b.r : (1-(1-2*(a.r-0.5))*(1-b.r)) ;
	ret.g = (b.g <= 0.5) ?  (2*a.g)*b.g : (1-(1-2*(a.g-0.5))*(1-b.g)) ;
	ret.b = (b.b <= 0.5) ?  (2*a.b)*b.b : (1-(1-2*(a.b-0.5))*(1-b.b)) ;

    return ret;
}

fixed4 SoftLight(fixed4 a, fixed4 b)
{
	fixed4 ret = fixed4(0,0,0,1);

	ret.r = (b.r <= 0.5) ?  a.r*(b.r+0.5) : (1-(1-a.r)*(1-(b.r-0.5))) ;
	ret.g = (b.g <= 0.5) ?  a.g*(b.g+0.5) : (1-(1-a.g)*(1-(b.g-0.5))) ;
	ret.b = (b.b <= 0.5) ?  a.b*(b.b+0.5) : (1-(1-a.b)*(1-(b.b-0.5))) ;

	return ret;
}

fixed4 LinearLight (fixed4 a, fixed4 b) 
{
    fixed4 ret = fixed4(0,0,0,1);

    ret.r = (b.r <= 0.5) ?  (a.r+2*b.r -1) : (a.r+2*(b.r-0.5)) ;
	ret.g = (b.g <= 0.5) ?  (a.g+2*b.g -1) : (a.g+2*(b.g-0.5)) ;
	ret.b = (b.b <= 0.5) ?  (a.b+2*b.b -1) : (a.b+2*(b.b-0.5)) ;
 
    return ret;
}


fixed4 VividLight (fixed4 a, fixed4 b) 
{
    fixed4 ret = fixed4(0,0,0,1);

    ret.r = (b.r <= 0.5) ?  ColorBurn(a.r, b.r*2) : ColorDodge(a.r, 2*(b.r-0.5)) ;
	ret.g = (b.g <= 0.5) ?  ColorBurn(a.g, b.g*2) : ColorDodge(a.g, 2*(b.g-0.5)) ;
	ret.b = (b.b <= 0.5) ?  ColorBurn(a.b, b.b*2) : ColorDodge(a.b, 2*(b.b-0.5)) ;

    return ret;
}