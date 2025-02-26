// HitTestCodes.cs
// Copyright 2002, Rama Krishna 
//
namespace org.jiechan.service.Balloon
{
	public enum HitTestCodes : int
	{
		Error             = (-2),
		Transparent       = (-1),
		Nowhere           = 0,
		Client            = 1,
		Caption           = 2,
		SysMenu           = 3,
		GrowBox           = 4,
		Size              = GrowBox,
		Menu              = 5,
		HScroll           = 6,
		VScroll           = 7,
		MinButton         = 8,
		MaxButton         = 9,
		Left              = 10,
		Right             = 11,
		Top               = 12,
		TopLeft           = 13,
		TopRight          = 14,
		Bottom            = 15,
		BottomLeft        = 16,
		BottomRight       = 17,
		Border            = 18,
		Reduce            = MinButton,
		Zoom              = MaxButton,
		SizeFirst         = Left,
		SizeLast          = BottomRight,
		Object            = 19,
		Close             = 20,
		Help              = 21,
	}
}