"C:\Program Files (x86)\Wix Toolset v3.11\bin\Candle" DualMonitorTools.wks -ext WixNetFxExtension
"C:\Program Files (x86)\Wix Toolset v3.11\bin\Light" DualMonitorTools.wixobj -ext WixNetFxExtension -ext WixUIExtension
cscript TweakExitDialog.js DualMonitorTools.msi
