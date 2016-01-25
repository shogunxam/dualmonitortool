//
// This code was taken from
// http://stackoverflow.com/questions/471424/wix-tricks-and-tips
// and moves the 'Run app on exit' checkbox from the bitmap area
// down to the bottom where the background colour used is the
// controls background colour.
//
var msiOpenDatabaseModeTransact = 1;
var filespec = WScript.Arguments(0);
var installer = new ActiveXObject("WindowsInstaller.Installer");
var database = installer.OpenDatabase(filespec, msiOpenDatabaseModeTransact);
var sql = "UPDATE `Control` SET `Control`.`Height` = '18', `Control`.`Width` = '170'," +
          " `Control`.`Y`='243', `Control`.`X`='10' " +
          "WHERE `Control`.`Dialog_`='ExitDialog' AND " + 
          "  `Control`.`Control`='OptionalCheckBox'";
var view = database.OpenView(sql);
view.Execute();
view.Close();
database.Commit();
