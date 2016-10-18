echo off
cls

scopy "d:\dir y"

if %errorLevel% == 0 (
	echo Operation Successful
) else (
	echo Error code - %errorLevel%
)

pause