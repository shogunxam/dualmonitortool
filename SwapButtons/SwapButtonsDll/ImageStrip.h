#pragma once
#include "SwapButtonsDll.h"

#include <Uxtheme.h>

class LOCALMODE_API CImageStrip
{
public:
	CImageStrip();
	~CImageStrip(void);

	void SetImageStrip(HBITMAP hbmStrip, int nImages, const MARGINS& Margins, bool bVerticalLayout);
	void Draw(int nImage, HDC hDC, const RECT& DestRect);

private:
	HBITMAP m_hbmStrip;
	int m_nImages;
	MARGINS m_Margins;
	bool m_bVerticalLayout;

	// size of each individual image in the image strip
	int m_nSrcWidth;
	int m_nSrcHeight;
};

