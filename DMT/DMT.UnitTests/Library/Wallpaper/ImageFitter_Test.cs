#region copyright
// This file is part of Dual Monitor Tools which is a set of tools to assist
// users with multiple monitor setups.
// Copyright (C) 2017  Gerald Evans
// 
// Dual Monitor Tools is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DMT.Library.Wallpaper;


namespace DMT.UnitTests.Library.Wallpaper
{
	[TestFixture]
	public class ImageFitter_Test
	{
		// lets make to easier it identify all of the new fit values 
		const StretchType.Fit Fit_08_____ = StretchType.Fit.NewFit;
		const StretchType.Fit Fit_18____A = StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio;
		const StretchType.Fit Fit_28___E_ = StretchType.Fit.NewFit | StretchType.Fit.AllowEnlarge;
		const StretchType.Fit Fit_38___EA = StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge;
		const StretchType.Fit Fit_48__S__ = StretchType.Fit.NewFit | StretchType.Fit.AllowShrink;
		const StretchType.Fit Fit_58__S_A = StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowShrink;
		const StretchType.Fit Fit_68__SE_ = StretchType.Fit.NewFit | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink;
		const StretchType.Fit Fit_78__SEA = StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink;

		const StretchType.Fit Fit_88_C___ = StretchType.Fit.NewFit | StretchType.Fit.ClipImage;
		const StretchType.Fit Fit_98_C__A = StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.ClipImage;
		const StretchType.Fit Fit_A8_C_E_ = StretchType.Fit.NewFit | StretchType.Fit.AllowEnlarge | StretchType.Fit.ClipImage;
		const StretchType.Fit Fit_B8_C_EA = StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge | StretchType.Fit.ClipImage;
		const StretchType.Fit Fit_C8_CS__ = StretchType.Fit.NewFit | StretchType.Fit.AllowShrink | StretchType.Fit.ClipImage;
		const StretchType.Fit Fit_D8_CS_A = StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowShrink | StretchType.Fit.ClipImage;
		const StretchType.Fit Fit_E8_CSE_ = StretchType.Fit.NewFit | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink | StretchType.Fit.ClipImage;
		const StretchType.Fit Fit_F8_CSEA = StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink | StretchType.Fit.ClipImage;

		// common rectangle sizes that we are going to use - values chosen to simplify testing

		static Rectangle Screen1Rect = new Rectangle(0, 0, 2000, 1000);
		static Rectangle Screen4Rect = new Rectangle(2000, 1000, 2000, 1000);	// 4 monitors in a square, origin at TLHC, and this is monitor at bottom right
		static Size BigWideSize = new Size(5000, 2000);
		static Size BigTallSize = new Size(3000, 2000);
		static Size SmallWideImage = new Size(1000, 400);
		static Size SmallTallImage = new Size(1000, 600);
		static Size ScreenSize = new Size(2000, 1000);
		static Size WideSize = new Size(4000, 500); // wider but shorter than screen
		static Size TallSize = new Size(1000, 2000); // taller but narrower than screen


		static object[] NewFitCases =
		{
			new object[] /* 0x08 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit },
			new object[] /* 0x18 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio },	
			new object[] /* 0x28 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.AllowEnlarge },
			new object[] /* 0x38 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge },
			new object[] /* 0x48 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.AllowShrink },
			new object[] /* 0x58 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowShrink },
			new object[] /* 0x68 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink },
			new object[] /* 0x78 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink },

			new object[] /* 0x88 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.ClipImage },
			new object[] /* 0x98 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.ClipImage },
			new object[] /* 0xA8 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.AllowEnlarge | StretchType.Fit.ClipImage },
			new object[] /* 0xB8 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge | StretchType.Fit.ClipImage },
			new object[] /* 0xC8 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.AllowShrink | StretchType.Fit.ClipImage },
			new object[] /* 0xD8 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowShrink | StretchType.Fit.ClipImage },
			new object[] /* 0xE8 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink | StretchType.Fit.ClipImage },
			new object[] /* 0xF8 */ { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink | StretchType.Fit.ClipImage },
		};


		static object[] OldFitCases =
		{
			// old fit styles that need mapping
			new object[] { StretchType.Fit.OverStretch, StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink | StretchType.Fit.ClipImage },
			new object[] { StretchType.Fit.UnderStretch, StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink },
			new object[] { StretchType.Fit.Center, StretchType.Fit.NewFit | StretchType.Fit.MaintainAspectRatio },
			new object[] { StretchType.Fit.StretchToFit, StretchType.Fit.NewFit | StretchType.Fit.AllowEnlarge | StretchType.Fit.AllowShrink },
		};

		[TestCaseSource("OldFitCases")]
		public void CanUpgradeOldFitValues(StretchType.Fit oldFit, StretchType.Fit expected)
		{
			StretchType.Fit actual = ImageFitter.UpgradeFit(oldFit);
			Assert.AreEqual(expected, actual);
		}

		static IEnumerable<StretchType.Fit> GetNewFitCases()
		{
			// 4 bits -> 16 combinations
			for (int n = 0; n < 16; n++)
			{
				yield return (StretchType.Fit)(n << 4) | StretchType.Fit.NewFit;
			}
		}

		[Test, TestCaseSource("GetNewFitCases")]
		public void NewFitValuesNotUpgraded(StretchType.Fit newFit)
		{
			StretchType.Fit actual = ImageFitter.UpgradeFit(newFit);
			Assert.AreEqual(newFit, actual);
		}

		static object[] ImageEqualToTargetCases =
		{
			new object[] { Fit_08_____, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_18____A, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_28___E_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_38___EA, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_48__S__, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_58__S_A, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_68__SE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_78__SEA, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_88_C___, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_98_C__A, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_A8_C_E_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_B8_C_EA, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_C8_CS__, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_D8_CS_A, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_E8_CSE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_F8_CSEA, new Rectangle(0, 0, 2000, 1000) },
		};

		[TestCaseSource("ImageEqualToTargetCases")]
		public void ImageEqualToTarget(StretchType.Fit fit, Rectangle expectedRect)
		{
			Size imageSize = ScreenSize;
			Rectangle targetRect = Screen1Rect;

			Rectangle actual = ImageFitter.FitImage(imageSize, fit, targetRect);
			Assert.AreEqual(expectedRect, actual);
		}


		static object[] WideImageLargerThanTargetCases =
		{
			new object[] { Fit_08_____, new Rectangle(-1500, -500, 5000, 2000) },
			new object[] { Fit_18____A, new Rectangle(-1500, -500, 5000, 2000) },
			new object[] { Fit_28___E_, new Rectangle(-1500, -500, 5000, 2000) },
			new object[] { Fit_38___EA, new Rectangle(-1500, -500, 5000, 2000) },
			new object[] { Fit_48__S__, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_58__S_A, new Rectangle(0, 100, 2000, 800) },
			new object[] { Fit_68__SE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_78__SEA, new Rectangle(0, 100, 2000, 800) },
			new object[] { Fit_88_C___, new Rectangle(-1500, -500, 5000, 2000) },
			new object[] { Fit_98_C__A, new Rectangle(-1500, -500, 5000, 2000) },
			new object[] { Fit_A8_C_E_, new Rectangle(-1500, -500, 5000, 2000) },
			new object[] { Fit_B8_C_EA, new Rectangle(-1500, -500, 5000, 2000) },
			new object[] { Fit_C8_CS__, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_D8_CS_A, new Rectangle(-250, 0, 2500, 1000) },
			new object[] { Fit_E8_CSE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_F8_CSEA, new Rectangle(-250, 0, 2500, 1000) },
		};

		[TestCaseSource("WideImageLargerThanTargetCases")]
		public void WideImageLargerThanTarget(StretchType.Fit fit, Rectangle expectedRect)
		{
			Size imageSize = BigWideSize;
			Rectangle targetRect = Screen1Rect;

			Rectangle actual = ImageFitter.FitImage(imageSize, fit, targetRect);
			Assert.AreEqual(expectedRect, actual);
		}


		static object[] TallImageLargerThanTargetCases =
		{
			new object[] { Fit_08_____, new Rectangle(-500, -500, 3000, 2000) },
			new object[] { Fit_18____A, new Rectangle(-500, -500, 3000, 2000) },
			new object[] { Fit_28___E_, new Rectangle(-500, -500, 3000, 2000) },
			new object[] { Fit_38___EA, new Rectangle(-500, -500, 3000, 2000) },
			new object[] { Fit_48__S__, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_58__S_A, new Rectangle(250, 0, 1500, 1000) },
			new object[] { Fit_68__SE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_78__SEA, new Rectangle(250, 0, 1500, 1000) },
			new object[] { Fit_88_C___, new Rectangle(-500, -500, 3000, 2000) },
			new object[] { Fit_98_C__A, new Rectangle(-500, -500, 3000, 2000) },
			new object[] { Fit_A8_C_E_, new Rectangle(-500, -500, 3000, 2000) },
			new object[] { Fit_B8_C_EA, new Rectangle(-500, -500, 3000, 2000) },
			new object[] { Fit_C8_CS__, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_D8_CS_A, new Rectangle(0, -166, 2000, 1333) },
			new object[] { Fit_E8_CSE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_F8_CSEA, new Rectangle(0, -166, 2000, 1333) },
		};

		[TestCaseSource("TallImageLargerThanTargetCases")]
		public void TallImageLargerThanTarget(StretchType.Fit fit, Rectangle expectedRect)
		{
			Size imageSize = BigTallSize;
			Rectangle targetRect = Screen1Rect;

			Rectangle actual = ImageFitter.FitImage(imageSize, fit, targetRect);
			Assert.AreEqual(expectedRect, actual);
		}

		static object[] WideImageSmallerThanTargetCases =
		{
			new object[] { Fit_08_____, new Rectangle(500, 300, 1000, 400) },
			new object[] { Fit_18____A, new Rectangle(500, 300, 1000, 400) },
			new object[] { Fit_28___E_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_38___EA, new Rectangle(0, 100, 2000, 800) },
			new object[] { Fit_48__S__, new Rectangle(500, 300, 1000, 400) },
			new object[] { Fit_58__S_A, new Rectangle(500, 300, 1000, 400) },
			new object[] { Fit_68__SE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_78__SEA, new Rectangle(0, 100, 2000, 800) },
			new object[] { Fit_88_C___, new Rectangle(500, 300, 1000, 400) },
			new object[] { Fit_98_C__A, new Rectangle(500, 300, 1000, 400) },
			new object[] { Fit_A8_C_E_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_B8_C_EA, new Rectangle(-250, 0, 2500, 1000) },
			new object[] { Fit_C8_CS__, new Rectangle(500, 300, 1000, 400) },
			new object[] { Fit_D8_CS_A, new Rectangle(500, 300, 1000, 400) },
			new object[] { Fit_E8_CSE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_F8_CSEA, new Rectangle(-250, 0, 2500, 1000) },
		};

		[TestCaseSource("WideImageSmallerThanTargetCases")]
		public void WideImageSmallerThanTarget(StretchType.Fit fit, Rectangle expectedRect)
		{
			Size imageSize = SmallWideImage;
			Rectangle targetRect = Screen1Rect;

			Rectangle actual = ImageFitter.FitImage(imageSize, fit, targetRect);
			Assert.AreEqual(expectedRect, actual);
		}

		static object[] TallImageSmallerThanTargetCases =
		{
			new object[] { Fit_08_____, new Rectangle(500, 200, 1000, 600) },
			new object[] { Fit_18____A, new Rectangle(500, 200, 1000, 600) },
			new object[] { Fit_28___E_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_38___EA, new Rectangle(167, 0, 1666, 1000) },
			new object[] { Fit_48__S__, new Rectangle(500, 200, 1000, 600) },
			new object[] { Fit_58__S_A, new Rectangle(500, 200, 1000, 600) },
			new object[] { Fit_68__SE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_78__SEA, new Rectangle(167, 0, 1666, 1000) },
			new object[] { Fit_88_C___, new Rectangle(500, 200, 1000, 600) },
			new object[] { Fit_98_C__A, new Rectangle(500, 200, 1000, 600) },
			new object[] { Fit_A8_C_E_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_B8_C_EA, new Rectangle(0, -100, 2000, 1200) },
			new object[] { Fit_C8_CS__, new Rectangle(500, 200, 1000, 600) },
			new object[] { Fit_D8_CS_A, new Rectangle(500, 200, 1000, 600) },
			new object[] { Fit_E8_CSE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_F8_CSEA, new Rectangle(0, -100, 2000, 1200) },
		};

		[TestCaseSource("TallImageSmallerThanTargetCases")]
		public void TallImageSmallerThanTarget(StretchType.Fit fit, Rectangle expectedRect)
		{
			Size imageSize = SmallTallImage;
			Rectangle targetRect = Screen1Rect;

			Rectangle actual = ImageFitter.FitImage(imageSize, fit, targetRect);
			Assert.AreEqual(expectedRect, actual);
		}

		static object[] ImageWiderThanTargetCases =
		{
			new object[] { Fit_08_____, new Rectangle(-1000, 250, 4000, 500) },
			new object[] { Fit_18____A, new Rectangle(-1000, 250, 4000, 500) },
			new object[] { Fit_28___E_, new Rectangle(-1000, 0, 4000, 1000) },
			new object[] { Fit_38___EA, new Rectangle(-1000, 250, 4000, 500) },
			new object[] { Fit_48__S__, new Rectangle(0, 250, 2000, 500) },
			new object[] { Fit_58__S_A, new Rectangle(0, 375, 2000, 250) },
			new object[] { Fit_68__SE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_78__SEA, new Rectangle(0, 375, 2000, 250) },
			new object[] { Fit_88_C___, new Rectangle(-1000, 250, 4000, 500) },
			new object[] { Fit_98_C__A, new Rectangle(-1000, 250, 4000, 500) },
			new object[] { Fit_A8_C_E_, new Rectangle(-1000, 0, 4000, 1000) },
			new object[] { Fit_B8_C_EA, new Rectangle(-3000, 0, 8000, 1000) },
			new object[] { Fit_C8_CS__, new Rectangle(0, 250, 2000, 500) },
			new object[] { Fit_D8_CS_A, new Rectangle(-1000, 250, 4000, 500) },
			new object[] { Fit_E8_CSE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_F8_CSEA, new Rectangle(-3000, 0, 8000, 1000) },
		};

		[TestCaseSource("ImageWiderThanTargetCases")]
		public void ImageWiderThanTarget(StretchType.Fit fit, Rectangle expectedRect)
		{
			Size imageSize = WideSize;
			Rectangle targetRect = Screen1Rect;

			Rectangle actual = ImageFitter.FitImage(imageSize, fit, targetRect);
			Assert.AreEqual(expectedRect, actual);
		}

		static object[] ImageTallerThanTargetCases =
		{
			new object[] { Fit_08_____, new Rectangle(500, -500, 1000, 2000) },
			new object[] { Fit_18____A, new Rectangle(500, -500, 1000, 2000) },
			new object[] { Fit_28___E_, new Rectangle(0, -500, 2000, 2000) },
			new object[] { Fit_38___EA, new Rectangle(500, -500, 1000, 2000) },
			new object[] { Fit_48__S__, new Rectangle(500, 0, 1000, 1000) },
			new object[] { Fit_58__S_A, new Rectangle(750, 0, 500, 1000) },
			new object[] { Fit_68__SE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_78__SEA, new Rectangle(750, 0, 500, 1000) },
			new object[] { Fit_88_C___, new Rectangle(500, -500, 1000, 2000) },
			new object[] { Fit_98_C__A, new Rectangle(500, -500, 1000, 2000) },
			new object[] { Fit_A8_C_E_, new Rectangle(0, -500, 2000, 2000) },
			new object[] { Fit_B8_C_EA, new Rectangle(0, -1500, 2000, 4000) },
			new object[] { Fit_C8_CS__, new Rectangle(500, 0, 1000, 1000) },
			new object[] { Fit_D8_CS_A, new Rectangle(500, -500, 1000, 2000) },
			new object[] { Fit_E8_CSE_, new Rectangle(0, 0, 2000, 1000) },
			new object[] { Fit_F8_CSEA, new Rectangle(0, -1500, 2000, 4000) },
		};

		[TestCaseSource("ImageTallerThanTargetCases")]
		public void ImageTallerThanTarget(StretchType.Fit fit, Rectangle expectedRect)
		{
			Size imageSize = TallSize;
			Rectangle targetRect = Screen1Rect;

			Rectangle actual = ImageFitter.FitImage(imageSize, fit, targetRect);
			Assert.AreEqual(expectedRect, actual);
		}


		// test cases for when monitor not at x = 0 or y = 0
		static object[] MonitorNotAtZeroZeroCases =
		{
			new object[] { Fit_08_____, new Rectangle(2500, 1300, 1000, 400) },
			new object[] { Fit_18____A, new Rectangle(2500, 1300, 1000, 400) },
			new object[] { Fit_28___E_, new Rectangle(2000, 1000, 2000, 1000) },
			new object[] { Fit_38___EA, new Rectangle(2000, 1100, 2000, 800) },
			new object[] { Fit_48__S__, new Rectangle(2500, 1300, 1000, 400) },
			new object[] { Fit_58__S_A, new Rectangle(2500, 1300, 1000, 400) },
			new object[] { Fit_68__SE_, new Rectangle(2000, 1000, 2000, 1000) },
			new object[] { Fit_78__SEA, new Rectangle(2000, 1100, 2000, 800) },
			new object[] { Fit_88_C___, new Rectangle(2500, 1300, 1000, 400) },
			new object[] { Fit_98_C__A, new Rectangle(2500, 1300, 1000, 400) },
			new object[] { Fit_A8_C_E_, new Rectangle(2000, 1000, 2000, 1000) },
			new object[] { Fit_B8_C_EA, new Rectangle(1750, 1000, 2500, 1000) },
			new object[] { Fit_C8_CS__, new Rectangle(2500, 1300, 1000, 400) },
			new object[] { Fit_D8_CS_A, new Rectangle(2500, 1300, 1000, 400) },
			new object[] { Fit_E8_CSE_, new Rectangle(2000, 1000, 2000, 1000) },
			new object[] { Fit_F8_CSEA, new Rectangle(1750, 1000, 2500, 1000) },
		};

		[TestCaseSource("MonitorNotAtZeroZeroCases")]
		public void MonitorNotAtZeroZero(StretchType.Fit fit, Rectangle expectedRect)
		{
			Size imageSize = SmallWideImage;
			Rectangle targetRect = Screen4Rect;

			Rectangle actual = ImageFitter.FitImage(imageSize, fit, targetRect);
			Assert.AreEqual(expectedRect, actual);
		}

	}
}
