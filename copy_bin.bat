@echo off

IF NOT EXIST BIN (
MKDIR BIN
)

XCOPY /Y Core\bin\x86\Release\Core.dll BIN
XCOPY /Y Cards\bin\x86\Release\Cards.exe BIN
XCOPY /Y Registration\bin\x86\Release\Registration.exe BIN
XCOPY /Y Settings\bin\x86\Release\Settings.exe BIN
