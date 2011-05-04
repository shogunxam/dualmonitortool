#include "StdAfx.h"
#include "BitmapBuffer.h"


CBitmapBuffer::CBitmapBuffer()
	: m_nWidth(0),
	  m_nHeight(0),
	  m_pdwBits(NULL)
{
}

CBitmapBuffer::CBitmapBuffer(int nWidth, int nHeight)
	: m_nWidth(nWidth),
	  m_nHeight(nHeight)
{
	m_pdwBits = new DWORD(nWidth * nHeight);
}

CBitmapBuffer::~CBitmapBuffer(void)
{
	delete [] m_pdwBits;
}


void CBitmapBuffer::Init(int nWidth, int nHeight)
{
	if (nWidth * nHeight != m_nWidth * m_nHeight)
	{
		// need a new buffer
		delete [] m_pdwBits;
		m_pdwBits = NULL;

		m_pdwBits = new DWORD[nWidth * nHeight];
		m_nWidth = nWidth;
		m_nHeight = nHeight;
	}

	// note: contents of buffer are not set/cleared
}

int CBitmapBuffer::GetWidth() const
{
	return m_nWidth;
}

int CBitmapBuffer::GetHeight() const
{
	return m_nHeight;
}

const DWORD* CBitmapBuffer::GetBits() const
{
	return m_pdwBits;
}

void CBitmapBuffer::Fill(int x1, int y1, int x2, int y2, DWORD dwColour)
{
	if (!IsValidX(x1) || !IsValidY(y1) || !IsValidX(x2) || !IsValidY(y2))
	{
		throw new invalid_argument("Fill");
	}

	for (int y = y1; y <= y2; y++)
	{
		for (int x = x1; x <= x2; x++)
		{
			InlineSet(x, y, dwColour);
		}
	}
}

void CBitmapBuffer::DrawHLine(int x1, int x2, int y, DWORD dwColour)
{
	if (!IsValidX(x1) || !IsValidX(x2) || !IsValidY(y))
	{
		throw new invalid_argument("DrawHLine");
	}

	for (int x = x1; x <= x2; x++)
	{
		InlineSet(x, y, dwColour);
	}
}

void CBitmapBuffer::DrawVLine(int x, int y1, int y2, DWORD dwColour)
{
	if (!IsValidX(x) || !IsValidY(y1) || !IsValidY(y2))
	{
		throw new invalid_argument("DrawVLine");
	}

	for (int y = y1; y <= y2; y++)
	{
		InlineSet(x, y, dwColour);
	}
}

void CBitmapBuffer::Set(int x, int y, DWORD dwColour)
{
	if (!IsValidX(x) || !IsValidY(y))
	{
		throw new invalid_argument("Set");
	}

	InlineSet(x, y, dwColour);
}

//void CBitmapBuffer::CenterTransparentBitmap(HBITMAP hbm, int left, int top, int right, int bottom, DWORD dwTransparent)
//{
//	BITMAP bm;
//	GetObject(hbm, sizeof(BITMAP), &bm);
//	BITMAPINFO bi;
//	memset(&bi, 0, sizeof(bi));
//	bi.bmiHeader.biSize = sizeof(bi.bmiHeader);
//	int ret = GetDIBits(hDC, hbm, 0, bm.bmHeight, NULL, &bi, DIB_RGB_COLORS);
//	if (ret != 0)
//	{
//		DWORD* pBits = NULL;
//		try
//		{
//			pBits = new DWORD[bm.bmWidth * bm.bmHeight];
//			bi.bmiHeader.biSize = sizeof(bi.bmiHeader);
//			bi.bmiHeader.biBitCount = 32;
//			bi.bmiHeader.biCompression  = BI_RGB;
//			int ret = GetDIBits(hDC, hbm, 0, bm.bmHeight, pBits, &bi, DIB_RGB_COLORS);
//			if (ret != 0)
//			{
//				// add glyph to our bitmap
//				int xOffset = left + (right - left + 1 - bm.bmWidth) / 2;
//				int yOffset = top + (bottom - top + 1 - bm.bmHeight) / 2;
//				for (int y = 0; y < bm.bmHeight; y++)
//				{
//					for (int x = 0; x < bm.bmWidth; x++)
//					{
//						// remember a bitmap is upside down (with y=0 at the bottom)
//						DWORD dwRGB = pBits[(bm.bmHeight - y - 1) * bm.bmWidth + x];
//						// green is transparent
//						if (dwRGB != dwTransparent)
//						{
//							//DWORD dwARGB = 0xFF000000 | dwRGB;	// TODO: ordering
//							Set(x + xOffset, y + yOffset, dwRGB);
//						}
//					}
//				}
//			}
//		}
//		catch (...)
//		{
//		}
//		delete [] pBits;
//
//}

// private
inline void CBitmapBuffer::InlineSet(int x, int y, DWORD dwColour)
{
	// As a bitmap has y=0 at the bottom, we represent the data in our array the same
	// (but the interface has y=0 at the top)
	//m_pdwBits[(m_nHeight - y - 1) * m_nWidth + x] = dwColour;

	// buffer now stores with y=0 at the top
	m_pdwBits[y * m_nWidth + x] = dwColour;
}

// private
bool CBitmapBuffer::IsValidX(int x)
{
	return x >= 0 && x < m_nWidth;
}

// private
bool CBitmapBuffer::IsValidY(int y)
{
	return y >= 0 && y < m_nHeight;
}

