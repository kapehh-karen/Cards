@echo off

IF NOT EXIST BIN (
MKDIR BIN
)

XCOPY /Y Core\bin\x86\Debug\Core.dll BIN
XCOPY /Y Cards\bin\x86\Debug\Cards.exe BIN
XCOPY /Y Registration\bin\x86\Debug\Registration.exe BIN
XCOPY /Y Settings\bin\x86\Debug\Settings.exe BIN

pause