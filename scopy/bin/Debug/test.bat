echo off
cls

if exist TestDir (
  rmdir /s /q TestDir
)

if exist Dest (
  rmdir /s /q Dest
)

mkdir TestDir && cd TestDir
type nul > text1.txt
type nul > 2.txt
type nul > img.bmp

mkdir SubDir && cd SubDir
type nul > sub.txt
type nul > sub.bmp

cd ..
cd ..

echo off
cls

scopy /?
pause
cls

echo Start program without parameters
echo scopy

scopy

if %errorLevel% == 0 (
	echo Operation Successful - Test is OK
) else (
	echo Exit code: %errorLevel% - Test is not OK
)

pause
cls

echo Start program without source but with key /S
echo scopy /S

scopy /s

if %errorLevel% == 0 (
  echo Operation Successful - Test is OK
) else (
  echo Exit code: %errorLevel% - Test is not OK
)

pause
cls

echo Start program with source directory parameter
echo scopy TestDir

scopy TestDir

if %errorLevel% == 0 (
  echo Operation Successful - Test is OK
) else (
  echo Exit code: %errorLevel% - Test is not OK
)

pause
cls

echo Start program with source directory parameter and destination parameter
echo scopy TestDir Dest

scopy TestDir Dest 

if %errorLevel% == 0 (
  echo Operation Successful - Test is OK
) else (
  echo Exit code: %errorLevel% - Test is not OK
)

pause
cls

echo Start program with keys /Y and /S
echo scopy TestDir Dest /Y /S

scopy TestDir Dest /S /Y

if %errorLevel% == 0 (
  echo Operation Successful - Test is OK
) else (
  echo Exit code: %errorLevel% - Test is not OK
)

pause
cls

echo Start program with 2 sources and keys /Y and /S
echo scopy TestDir\*.txt+*.exe Dest /Y /S

scopy TestDir\*.txt+*.exe Dest /S /Y

if %errorLevel% == 0 (
  echo Operation Successful - Test is OK
) else (
  echo Exit code: %errorLevel% - Test is not OK
)

pause
cls