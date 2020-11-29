@echo off

IF NOT EXIST BIN (
MKDIR BIN
)

XCOPY /Y Core\bin\x86\Release\net40\Core.dll BIN
XCOPY /Y Cards\bin\x86\Release\net40\Cards.exe BIN
XCOPY /Y Registration\bin\x86\Release\net40\Registration.exe BIN
XCOPY /Y Settings\bin\x86\Release\net40\Settings.exe BIN
