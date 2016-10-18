echo off
cls

scopy /?

if %errorLevel% == 0 (
	echo Operation Successful
) else (
	echo Error code - %errorLevel%
)


pause