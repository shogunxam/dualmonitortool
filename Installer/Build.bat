"C:\Program Files (x86)\Wix Toolset v3.10\bin\Candle" DualMonitorTools.wks -ext WixNetFxExtension
"C:\Program Files (x86)\Wix Toolset v3.10\bin\Light" DualMonitorTools.wixobj -ext WixNetFxExtension -ext WixUIExtension
cscript TweakExitDialog.js DualMonitorTools.msi
