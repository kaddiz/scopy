echo off
cls

scopy "*.doc" "New Dir" /y 

if %errorLevel% == 0 (
	echo Operation Successful
) else (
	echo Error code - %errorLevel%
)

xcopy "*.doc" "New Dir" /y 

if %errorLevel% == 0 (
	echo Operation Successful
) else (
	echo Error code - %errorLevel%
)

pause